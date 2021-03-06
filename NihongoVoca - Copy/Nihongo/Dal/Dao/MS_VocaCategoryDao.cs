﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_VocaCategoryDao : BaseDao
    {
        public int SelectData(MS_VocaCategoriesModels model, out List<MS_VocaCategoriesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaCategoriesModels>();

            try
            {
                var query = this.ms_vocacategories.AsQueryable();
                var vocaSet = this.ms_vocasets.AsQueryable();
                if (model.ID > 0)
                {
                    query = query.Where(ss => ss.ID == model.ID);
                }
                if (!CommonMethod.IsNullOrEmpty(model.Code))
                {
                    query = query.Where(ss => ss.Code == model.Code);
                }

                if (!CommonMethod.IsNullOrEmpty(model.VocaSetID))
                {
                    query = query.Where(ss => ss.VocaSetID == model.VocaSetID);
                }

                results = query.Select(ss => new MS_VocaCategoriesModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Name1 = ss.Name1,
                    UrlImage = ss.UrlImage,
                    UrlDisplay = ss.UrlDisplay,
                    Description = ss.Description,
                    NumOfVocas = ss.NumOfVocas,
                    IsTrial = ss.IsTrial,
                })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectVocaCateByID(int id, out MS_VocaCategoriesModels result)
        {
            int returnCode = 0;
            result = new MS_VocaCategoriesModels();

            try
            {
                var query = this.ms_vocacategories.Where(ss => ss.ID == id).AsQueryable();

                result = (from ss in query
                          join vs in this.ms_vocasets on ss.VocaSetID equals vs.ID
                          select new MS_VocaCategoriesModels
                  {
                      ID = ss.ID,
                      Code = ss.Code,
                      Name1 = ss.Name1,
                      UrlImage = ss.UrlImage,
                      UrlDisplay = ss.UrlDisplay,
                      Description = ss.Description,
                      NumOfVocas = ss.NumOfVocas,
                      IsTrial = ss.IsTrial,
                      RequiredTimePerVoca = ss.RequiredTimePerVoca,
                      VocaSet = vs.Code,
                      VocaSetID = vs.ID,
                      VocaSetName1 = vs.Name1,
                      VocaSetUrlDisplay = vs.UrlDisplay,
                      VocaSetFee = vs.Fee,
                      IsKanji = vs.IsKanji,
                  })
                    .FirstOrDefault();

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }


        internal int SelectPracticeLessonCate(int id, int userID, out MS_VocaSetsModels result)
        {
            int returnCode = 0;
            result = new MS_VocaSetsModels();

            try
            {
                result = (from ss in this.ms_vocacategories
                          where ss.ID == id
                          select new MS_VocaSetsModels
                          {
                              ID = ss.ms_vocasets.ID,
                              Code = ss.ms_vocasets.Code,
                              Name1 = ss.ms_vocasets.Name1,
                              UrlImage = ss.ms_vocasets.UrlImage,
                              UrlDisplay = ss.ms_vocasets.UrlDisplay,
                              Description = ss.ms_vocasets.Description,
                              NumOfVocas = ss.ms_vocasets.NumOfVocas,
                              IsKanji = ss.ms_vocasets.IsKanji,
                              NumOfRegistedPerson = ss.ms_vocasets.NumOfRegistedPerson,
                          })
                    .FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                this.Rollback();
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectVocaSetByID(int id, int userID, out MS_VocaSetsModels result)
        {
            int returnCode = 0;
            result = new MS_VocaSetsModels();

            try
            {
                result = (from ss in this.ms_vocasets
                          where ss.ID == id
                          select new MS_VocaSetsModels
                  {
                      ID = ss.ID,
                      Code = ss.Code,
                      Name1 = ss.Name1,
                      UrlImage = ss.UrlImage,
                      UrlDisplay = ss.UrlDisplay,
                      Description = ss.Description,
                      NumOfVocas = ss.NumOfVocas,
                      IsKanji = ss.IsKanji,
                      NumOfRegistedPerson = ss.NumOfRegistedPerson,
                  })
                    .FirstOrDefault();


                if (result != null)
                {
                    this.BeginTransaction();

                    //create user vocaset
                    var userSet = this.ms_uservocasets.FirstOrDefault(ss => ss.UserID == userID && ss.VocaSetID == id);
                    if (userSet == null)
                    {
                        userSet = new Mapping.ms_uservocasets();
                        userSet.VocaSetID = id;
                        userSet.UserID = userID;
                        userSet.StartDate = DateTime.Now.Date.AddDays(-1);
                        userSet.EndDate = DateTime.Now.Date.AddYears(1);
                        userSet.HasLearnt = CommonData.Status.Disable;
                        userSet.HasMarked = CommonData.Status.Disable;
                        userSet.IsIgnore = CommonData.Status.Disable;
                        userSet.UpdatedDate = DateTime.Now;

                        ms_uservocasets.AddObject(userSet);

                        var vocaCates = ms_vocacategories.Where(ss => ss.VocaSetID == id);
                        foreach (var vocaCate in vocaCates)
                        {
                            Mapping.ms_usercategories userCate = new Mapping.ms_usercategories();
                            userCate.CategoryID = vocaCate.ID;
                            userCate.UserID = userID;
                            userCate.StartDate = DateTime.Now.Date.AddDays(-1);
                            userCate.EndDate = DateTime.Now.Date.AddYears(1);
                            userCate.HasLearnt = CommonData.Status.Disable;
                            userCate.HasMarked = CommonData.Status.Disable;
                            userCate.IsIgnore = CommonData.Status.Disable;
                            userCate.UpdatedDate = DateTime.Now;

                            ms_usercategories.AddObject(userCate);
                        }
                    }
                    else
                    {
                        userSet.UpdatedDate = DateTime.Now;
                    }

                    //create user vocacategories

                    returnCode = this.Saves();

                    if (returnCode == CommonData.DbReturnCode.Succeed)
                    {
                        bool hasRegis = this.ms_uservocabularies.Any(ss => ss.ms_vocabularydetails.ms_vocacategories.ms_vocasets.ID == id
                                        && ss.UserID == userID);
                        if (!hasRegis)
                        {
                            //register for user
                            var vocaDetails = from vcd in this.ms_vocabularydetails
                                              join vc in this.ms_vocacategories on vcd.CategoryID equals vc.ID
                                              join vs in this.ms_vocasets on vc.VocaSetID equals vs.ID
                                              where vs.ID == id
                                              select vcd;
                            foreach (var vocaDetail in vocaDetails)
                            {
                                Nihongo.Dal.Mapping.ms_uservocabularies //usVoca = ms_uservocabularies.FirstOrDefault(ss => ss.UserID == userID && ss.VocaDetailID == vocaDetail.ID);
                                    //if (usVoca == null)
                                    //{
                                    usVoca = new Mapping.ms_uservocabularies()
                                    {
                                        UserID = userID,
                                        VocaDetailID = vocaDetail.ID,
                                        HasLearnt = CommonData.Status.Disable,
                                        HasMarked = CommonData.Status.Disable,
                                        IsIgnore = CommonData.Status.Disable,
                                        Level = 0,
                                        StartDate = DateTime.Now.Date.AddDays(-1),
                                        EndDate = DateTime.Now.Date.AddYears(1),
                                        NumOfWrong = 0,
                                    };
                                ms_uservocabularies.AddObject(usVoca);
                            }
                            //else
                            //{
                            //    hasRegis = true;
                            //result.NumOfRegistedPerson += 1;
                            var vocaset = ms_vocasets.FirstOrDefault(ss => ss.ID == id);
                            if (vocaset != null)
                            {
                                vocaset.NumOfRegistedPerson = (vocaset.NumOfRegistedPerson ?? 0) + 1;
                            }

                            returnCode = this.Saves();
                        }

                        if (returnCode == CommonData.DbReturnCode.Succeed)
                        {
                            returnCode = this.Commit();
                        }
                        else
                        {
                            this.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }


        internal int CheckCompletedPreCate(int id, int userID, out bool isOK)
        {
            int returnCode = 0;
            isOK = false;

            try
            {
                var vocaCate = (from ca in this.ms_vocacategories
                                join se in this.ms_vocasets on ca.VocaSetID equals se.ID
                                where ca.ID == id
                                select new { CatePreCode = ca.PreviousCode, CateLineNumber = ca.LineNumber, IsSequence = se.IsSequence, SetCode = se.Code }).FirstOrDefault();

                if (vocaCate != null)
                {
                    if (vocaCate.IsSequence == CommonData.Status.Disable)
                    {
                        isOK = true;
                    }
                    else
                    {
                        if (vocaCate.CateLineNumber == 1)
                        {
                            isOK = true;
                        }
                        else
                        {
                            //isOK = this.ms_testresults.Any(ss => ss.CategoryID == vocaCate.CatePreCode
                            //    && ss.UserID == userID
                            //    && ss.IsPass == CommonData.Status.Enable);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int CheckHasBuyVocaSet(int id, out List<MS_VocaSetsModels> vocaSets)
        {
            int returnCode = 0;
            vocaSets = new List<MS_VocaSetsModels>();
            try
            {
                var vocaCate = this.ms_vocacategories.Where(ss => ss.ID == id);
                vocaSets = (from ca in vocaCate
                            join se in this.ms_vocasets on ca.VocaSetID equals se.ID
                            join re in this.ms_registedvocasets on se.ID equals re.VocaSetID into regis
                            from re in regis.DefaultIfEmpty()
                            select new MS_VocaSetsModels
                            {
                                ID = se.ID,
                                UrlDisplay = se.UrlDisplay,
                                Fee = se.Fee,
                                StartDate = re == null ? null : re.StartDate,
                                EndDate = re == null ? null : re.EndDate,
                            })
                           .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int CheckCompletedCate(int id, int userID, out bool isOK)
        {
            int returnCode = 0;
            isOK = false;

            try
            {
                var vocaCate = (from ca in this.ms_vocacategories
                                join se in this.ms_vocasets on ca.VocaSetID equals se.ID
                                where ca.ID == id
                                select new { ID = ca.ID, Code = ca.Code, CateLineNumber = ca.LineNumber, IsSequence = se.IsSequence, SetCode = se.Code }).FirstOrDefault();

                if (vocaCate != null)
                {
                    isOK = this.ms_testresults.Any(ss => ss.CategoryID == vocaCate.ID
                                && ss.UserID == userID
                                && ss.IsPass == CommonData.Status.Enable);
                }

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectVocaCategoryBySet(MS_VocaCategoriesModels model, int userID, out List<MS_VocaCategoriesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaCategoriesModels>();

            try
            {
                if (model.VocaSetID > 0)
                {
                    results = (from ss in this.ms_vocacategories
                               join vs in this.ms_vocasets on ss.VocaSetID equals vs.ID
                               
                               join uvc in this.ms_usercategories.Where(ss => ss.UserID == userID) on ss.ID equals uvc.CategoryID into userCate
                               from uvc in userCate.DefaultIfEmpty()

                               where vs.ID == model.VocaSetID
                               select new MS_VocaCategoriesModels
                   {
                       ID = ss.ID,
                       Code = ss.Code,
                       Name1 = ss.Name1,
                       UrlImage = ss.UrlImage,
                       UrlDisplay = ss.UrlDisplay,
                       Description = ss.Description,
                       NumOfVocas = ss.NumOfVocas,
                       IsTrial = ss.IsTrial,
                       
                       IsIgnore = uvc == null ? CommonData.Status.Disable : uvc.IsIgnore,
                       HasMarked = uvc == null ? CommonData.Status.Disable : uvc.HasMarked,
                       HasLearnt = uvc == null ? CommonData.Status.Disable : uvc.HasLearnt,
                   })
                   .OrderBy(ss => ss.ID)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectUserCategoryBySet(int id, int userID, out List<MS_UserCategoriesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserCategoriesModels>();

            try
            {
                if (id > 0)
                {
                    results = (from ss in this.ms_vocacategories
                               join vs in this.ms_vocasets on ss.VocaSetID equals vs.ID

                               join uvc in this.ms_usercategories.Where(ss => ss.UserID == userID) on ss.ID equals uvc.CategoryID into userCate
                               from uvc in userCate.DefaultIfEmpty()

                               where ss.VocaSetID == id
                               select new MS_UserCategoriesModels
                               {
                                    VocaSetID = ss.VocaSetID,
                                    VocaSetName1 = vs.Name1,
                                    VocaSetNumOfVoca = vs.NumOfVocas,

                                   CategoryID = ss.ID,
                                   CategoryCode = ss.Code,
                                   CategoryName1 = ss.Name1,
                                   CategoryUrlImage = ss.UrlImage,
                                   CategoryUrlDisplay = ss.UrlDisplay,
                                   Description = ss.Description,
                                   NumOfVocas = ss.NumOfVocas,
                                   IsTrial = ss.IsTrial,

                                   IsIgnore = uvc == null ? CommonData.Status.Disable : uvc.IsIgnore,
                                   HasMarked = uvc == null ? CommonData.Status.Disable : uvc.HasMarked,
                                   HasLearnt = uvc == null ? CommonData.Status.Disable : uvc.HasLearnt,

                               })
                   .OrderBy(ss => ss.CategoryID)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }


        internal int CreateVocaCategory(List<MS_VocabularyDetailModel> models)
        {
            int returnCode = 0;

            try
            {
                if (models.Count > 0)
                {
                    var cates = this.ms_vocacategories.ToList();
                    var vocas = this.ms_vocabularies.ToList();
                    var kanjis = this.ms_kanjis.ToList();

                    foreach (var model in models)
                    {
                        var category = cates.FirstOrDefault(ss => ss.Code == model.CategoryCode);
                        if (category != null)
                        {
                            model.CategoryID = category.ID;
                        }
                        var voca = vocas.FirstOrDefault(ss => ss.Code == model.VocabularyCode);
                        if (voca != null)
                        {
                            model.VocabularyID = voca.ID;
                        }
                        var kanji = kanjis.FirstOrDefault(ss => ss.Code == model.KanjiCode);
                        if (kanji != null)
                        {
                            model.KanjiID = kanji.ID;
                        }

                        if (category != null && (voca != null || kanji != null))
                        {
                            Mapping.ms_vocabularydetails detail = this.ms_vocabularydetails
                                .FirstOrDefault(ss => ss.CategoryID == model.CategoryID
                                                && (model.VocabularyID == null ? ss.VocabularyID == null : ss.VocabularyID == model.VocabularyID)
                                                && (model.KanjiID == null ? ss.KanjiID == null : ss.KanjiID == model.KanjiID));
                            if (detail == null)
                            {
                                detail = new Mapping.ms_vocabularydetails()
                               {
                                   CategoryID = model.CategoryID,
                                   VocabularyID = model.VocabularyID,
                                   KanjiID = model.KanjiID,
                                   LineNumber = model.LineNumber,
                               };

                                this.ms_vocabularydetails.AddObject(detail);
                            }
                            else
                            {
                                detail.LineNumber = model.LineNumber;
                            }
                        }
                    }

                    returnCode = this.Saves();

                    if (returnCode == 0)
                    {
                        //update num of voca
                        var vocaSets = ms_vocasets.AsQueryable();
                        foreach (var vocaSet in vocaSets)
                        {
                            var vocaCates = vocaSet.ms_vocacategories.AsQueryable();
                            foreach (var vocaCate in vocaCates)
                            {
                                vocaCate.NumOfVocas = vocaCate.ms_vocabularydetails.Count;
                            }
                            vocaSet.NumOfCategories = vocaSet.ms_vocacategories.Count;
                            vocaSet.NumOfVocas = vocaSet.ms_vocacategories.SelectMany(ss => ss.ms_vocabularydetails).Count();
                        }
                    }
                    returnCode = this.Saves();

                    if (returnCode == 0)
                    {
                        //update user vocas
                        var users = this.ms_users.AsQueryable();
                        var vocadetails = this.ms_vocabularydetails.AsQueryable();
                        foreach (var vocadetail in vocadetails)
                        {
                            foreach (var user in users)
                            {
                                var uservoca = this.ms_uservocabularies.FirstOrDefault(ss => ss.VocaDetailID == vocadetail.ID && ss.UserID == user.ID);
                                if (uservoca == null)
                                {
                                    uservoca = new Mapping.ms_uservocabularies()
                                    {
                                        UserID = user.ID,
                                        VocaDetailID = vocadetail.ID,
                                        UpdatedDate = DateTime.Now,

                                        Level = 0,
                                        HasLearnt = CommonData.Status.Disable,
                                        HasMarked = CommonData.Status.Disable,
                                        IsIgnore = CommonData.Status.Disable,
                                        StartDate = DateTime.Now.AddDays(-1),
                                        EndDate = DateTime.Now.AddYears(1),
                                    };

                                    this.ms_uservocabularies.AddObject(uservoca);
                                }
                            }
                        }

                        returnCode = this.Saves();
                    }
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int CreateKanjiExample(List<MS_KanjiExampleModel> models)
        {
            int returnCode = 0;

            try
            {
                if (models.Count > 0)
                {
                    var kanjis = this.ms_kanjis.ToList();
                    var vocas = this.ms_vocabularies.ToList();

                    foreach (var model in models)
                    {
                        var voca = vocas.FirstOrDefault(ss => ss.Code == model.VocabularyCode);
                        if (voca != null)
                        {
                            model.VocabularyID = voca.ID;
                        }
                        var kanji = kanjis.FirstOrDefault(ss => ss.Code == model.KanjiCode);
                        if (kanji != null)
                        {
                            model.KanjiID = kanji.ID;
                        }

                        if (kanji != null)
                        {
                            var detail = this.ms_kanjiexamples
                                .FirstOrDefault(ss => ss.KanjiID == model.KanjiID
                                                && ss.Kanji == model.Kanji
                                                && ss.Hiragana == model.Hiragana);
                            if (detail == null)
                            {
                                detail = new Mapping.ms_kanjiexamples()
                                {
                                    VocabularyID = model.VocabularyID,
                                    KanjiID = model.KanjiID,
                                    Kanji = model.Kanji,
                                    Pinyin = model.Pinyin,
                                    VMeaning = model.VMeaning,
                                    Hiragana = model.Hiragana,
                                };

                                this.ms_kanjiexamples.AddObject(detail);
                            }
                            else
                            {
                                detail.VocabularyID = model.VocabularyID;
                                detail.Kanji = model.Kanji;
                                detail.Pinyin = model.Pinyin;
                                detail.VMeaning = model.VMeaning;
                                detail.Hiragana = model.Hiragana;
                            }
                        }
                    }

                    returnCode = this.Saves();

                    if (returnCode == 0)
                    {
                        //update num of voca
                        var vocaSets = ms_vocasets.AsQueryable();
                        foreach (var vocaSet in vocaSets)
                        {
                            var vocaCates = vocaSet.ms_vocacategories.AsQueryable();
                            foreach (var vocaCate in vocaCates)
                            {
                                vocaCate.NumOfVocas = vocaCate.ms_vocabularydetails.Count;
                            }
                            vocaSet.NumOfCategories = vocaSet.ms_vocacategories.Count;
                            vocaSet.NumOfVocas = vocaSet.ms_vocacategories.SelectMany(ss => ss.ms_vocabularydetails).Count();
                        }
                    }
                    returnCode = this.Saves();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int CheckCompletedVoca(int id, int userID, out bool isOK)
        {
            int returnCode = 0;
            isOK = false;

            try
            {
                isOK = this.ms_uservocabularies.Any(ss => ss.VocaDetailID == id && ss.UserID == userID && ss.HasLearnt == CommonData.Status.Enable);
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int CheckIsIgnoreCate(int id, int userID, out bool isOK)
        {
            int returnCode = 0;
            isOK = false;

            try
            {
                isOK = this.ms_usercategories.Any(ss => ss.CategoryID == id && ss.UserID == userID && ss.IsIgnore == CommonData.Status.Enable);
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectCategoryBySet(int vocaSetID, int userID, out List<MS_VocaCategoriesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaCategoriesModels>();

            try
            {

                results = this.ms_usercategories.Where(ss => ss.UserID == userID
                    && ss.ms_vocacategories.VocaSetID == vocaSetID
                    && (ss.HasLearnt == CommonData.Status.Enable || ss.IsIgnore == CommonData.Status.Enable)
                    )
                    .Select(s => new MS_VocaCategoriesModels
                    {
                        ID = s.CategoryID,
                        Name1 = s.ms_vocacategories.Name1,
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

    }
}