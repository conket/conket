﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_UserVocabularyDao : BaseDao
    {
        public int SelectUserVocaData(int vocaCateId, string userName, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var vocaSet = this.ms_vocasets.AsQueryable();
                var vocaCate = this.ms_vocacategories.Where(ss => ss.ID == vocaCateId);
                var voca = this.ms_vocabularies.AsQueryable();
                var vocaDetail = this.ms_vocabularydetails.AsQueryable();
                var userVoca = this.ms_uservocabularies.Where(ss => ss.UserName == userName).AsQueryable();

                results = (from us in userVoca
                           join de in vocaDetail on us.VocaDetailID equals de.ID
                           join vc in vocaCate on de.CategoryCode equals vc.Code
                           join vs in vocaSet on vc.VocaSet equals vs.Code
                           join ss in voca on de.VocabularyCode equals ss.Code
                           orderby de.LineNumber
                           select new MS_UserVocabulariesModels
                           {
                               //user voca
                               ID = us.ID,
                               UserName = us.UserName,
                               Level = us.Level,
                               HasLearnt = us.HasLearnt,
                               //Update_Date = us.Update_Date,
                               HasMarked = us.HasMarked,
                               //voca detail
                               LineNumber = de.LineNumber,

                               //voca set
                               VocaSetID = vs.ID,
                               IsKanji = vs.IsKanji,

                               //category
                               CategoryID = vc.ID,
                               CategoryCode = vc.Code,
                               CategoryName = vc.Name1,
                               CategoryDescription = vc.Description,

                               //voca
                               VocabularyCode = ss.Code,
                               Romaji = ss.Romaji,
                               Romaji_Katakana = ss.Romaji_Katakana,
                               Katakana = ss.Katakana,
                               Hiragana = ss.Hiragana,
                               Kanji = ss.Kanji,
                               VMeaning = ss.VMeaning,
                               Description = ss.Description,
                               UrlAudio = ss.UrlAudio,
                               UrlAudio_Katakana = ss.UrlAudio_Katakana,
                               DisplayType = ss.DisplayType,
                               UrlImage = ss.UrlImage,
                               Type = ss.Type,

                               //kanji
                               Pinyin = ss.Pinyin,
                               OnReading = ss.OnReading,
                               OnRomaji = ss.OnRomaji,
                               OnUrlAudio = ss.OnUrlAudio,
                               OnReading2 = ss.OnReading2,
                               OnRomaji2 = ss.OnRomaji2,
                               OnUrlAudio2 = ss.OnUrlAudio2,
                               Remembering = ss.Remembering,

                               KunReading = ss.KunReading,
                               KunRomaji = ss.KunRomaji,
                               KunUrlAudio = ss.KunUrlAudio,
                               KunReading2 = ss.KunReading2,
                               KunRomaji2 = ss.KunRomaji2,
                               KunUrlAudio2 = ss.KunUrlAudio2,
                           })
                                .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        public int SelectUserVocaData(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var vocaSet = this.ms_vocasets.AsQueryable();
                var vocaCate = this.ms_vocacategories.Where(ss => ss.ID == model.CategoryID);
                var voca = this.ms_vocabularies.AsQueryable();
                var vocaDetail = this.ms_vocabularydetails.AsQueryable();
                var userVoca = this.ms_uservocabularies.Where(ss => ss.UserName == model.UserName).AsQueryable();
                if (!CommonMethod.IsNullOrEmpty(model.HasLearnt))
                {
                    userVoca = userVoca.Where(ss => ss.HasLearnt == model.HasLearnt);
                }
                //2: weak
                if (!CommonMethod.IsNullOrEmpty(model.VocaGetType) && model.VocaGetType == "2")
                {
                    userVoca = userVoca.Where(ss => ss.Level < 10);
                }
                //1: alphabet
                //2: word
                //3: sentence
                //if (!CommonMethod.IsNullOrEmpty(model.Type))
                //{
                //    voca = voca.Where(ss => ss.Type == model.Type);
                //}

                results = (from us in userVoca
                           join de in vocaDetail on us.VocaDetailID equals de.ID
                           join vc in vocaCate on de.CategoryCode equals vc.Code
                           join vs in vocaSet on vc.VocaSet equals vs.Code
                           join ss in voca on de.VocabularyCode equals ss.Code
                           orderby de.LineNumber
                           select new MS_UserVocabulariesModels
                           {
                               //user voca
                               ID = us.ID,
                               UserName = us.UserName,
                               Level = us.Level,
                               HasLearnt = us.HasLearnt,
                               //Update_Date = us.Update_Date,
                               HasMarked = us.HasMarked,
                               //voca detail
                               LineNumber = de.LineNumber,

                               //voca set
                               VocaSetID = vs.ID,

                               //category
                               CategoryID = vc.ID,
                               CategoryCode = vc.Code,
                               CategoryName = vc.Name1,
                               CategoryDescription = vc.Description,
                               RequiredTimePerVoca = vc.RequiredTimePerVoca,

                               //voca
                               VocabularyCode = ss.Code,
                               Romaji = ss.Romaji,
                               Romaji_Katakana = ss.Romaji_Katakana,
                               Katakana = ss.Katakana,
                               Hiragana = ss.Hiragana,
                               Kanji = ss.Kanji,
                               VMeaning = ss.VMeaning,
                               Description = ss.Description,
                               UrlAudio = ss.UrlAudio,
                               UrlAudio_Katakana = ss.UrlAudio_Katakana,
                               DisplayType = ss.DisplayType,
                               UrlImage = ss.UrlImage,
                               Type = ss.Type,

                               //kanji
                               Pinyin = ss.Pinyin,
                               OnReading = ss.OnReading,
                               OnRomaji = ss.OnRomaji,
                               OnUrlAudio = ss.OnUrlAudio,
                               OnReading2 = ss.OnReading2,
                               OnRomaji2 = ss.OnRomaji2,
                               OnUrlAudio2 = ss.OnUrlAudio2,
                               Remembering = ss.Remembering,

                               ExKanji1 = ss.ExKanji1,
                               ExKanji2 = ss.ExKanji2,
                               ExKanji3 = ss.ExKanji3,
                               ExKanji4 = ss.ExKanji4,
                               ExReading1 = ss.ExReading1,
                               ExReading2 = ss.ExReading2,
                               ExReading3 = ss.ExReading3,
                               ExReading4 = ss.ExReading4,
                               ExVMeaning1 = ss.ExVMeaning1,
                               ExVMeaning2 = ss.ExVMeaning2,
                               ExVMeaning3 = ss.ExVMeaning3,
                               ExVMeaning4 = ss.ExVMeaning4,

                               KunReading = ss.KunReading,
                               KunRomaji = ss.KunRomaji,
                               KunUrlAudio = ss.KunUrlAudio,
                               KunReading2 = ss.KunReading2,
                               KunRomaji2 = ss.KunRomaji2,
                               KunUrlAudio2 = ss.KunUrlAudio2,
                               
                           })
                                .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectNotebookVocas(string userName, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var vocaSet = this.ms_vocasets.AsQueryable();
                var vocaCate = this.ms_vocacategories.AsQueryable();
                var voca = this.ms_vocabularies.AsQueryable();
                var vocaDetail = this.ms_vocabularydetails.AsQueryable();
                var userVoca = this.ms_uservocabularies.Where(ss => ss.UserName == userName && ss.HasMarked == CommonData.Status.Enable).AsQueryable();

                results = (from us in userVoca
                           join de in vocaDetail on us.VocaDetailID equals de.ID
                           join vc in vocaCate on de.CategoryCode equals vc.Code
                           join vs in vocaSet on vc.VocaSet equals vs.Code
                           join ss in voca on de.VocabularyCode equals ss.Code
                           orderby de.LineNumber
                           select new MS_UserVocabulariesModels
                           {
                               //user voca
                               ID = us.ID,
                               UserName = us.UserName,
                               Level = us.Level,
                               HasLearnt = us.HasLearnt,
                               //Update_Date = us.Update_Date,
                               HasMarked = us.HasMarked,
                               //voca detail
                               LineNumber = de.LineNumber,

                               //voca set
                               VocaSetID = vs.ID,

                               //category
                               CategoryID = vc.ID,
                               CategoryCode = vc.Code,
                               CategoryName = vc.Name1,
                               CategoryDescription = vc.Description,

                               //voca
                               VocabularyCode = ss.Code,
                               Romaji = ss.Romaji,
                               Romaji_Katakana = ss.Romaji_Katakana,
                               Katakana = ss.Katakana,
                               Hiragana = ss.Hiragana,
                               Kanji = ss.Kanji,
                               VMeaning = ss.VMeaning,
                               Description = ss.Description,
                               UrlAudio = ss.UrlAudio,
                               UrlAudio_Katakana = ss.UrlAudio_Katakana,
                               DisplayType = ss.DisplayType,
                               UrlImage = ss.UrlImage,
                               Type = ss.Type,

                               //kanji
                               Pinyin = ss.Pinyin,
                               OnReading = ss.OnReading,
                               OnRomaji = ss.OnRomaji,
                               OnUrlAudio = ss.OnUrlAudio,
                               OnReading2 = ss.OnReading2,
                               OnRomaji2 = ss.OnRomaji2,
                               OnUrlAudio2 = ss.OnUrlAudio2,

                               KunReading = ss.KunReading,
                               KunRomaji = ss.KunRomaji,
                               KunUrlAudio = ss.KunUrlAudio,
                               KunReading2 = ss.KunReading2,
                               KunRomaji2 = ss.KunRomaji2,
                               KunUrlAudio2 = ss.KunUrlAudio2,
                               
                           })
                                .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int UpdateLevelData(List<MS_UserVocabulariesModels> vocas)
        {
            int returnCode = 0;

            try
            {
                foreach (var vo in vocas)
                {
                    var voca = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == vo.ID);
                    if (voca != null)
                    {
                        voca.Level = voca.Level;
                        voca.Update_Date = DateTime.Now;
                    }
                }

                returnCode = this.Saves();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int UpdateTestResult(List<MS_UserVocabulariesModels> vocas, out int accumulatedPoint)
        {
            int returnCode = 0;
            accumulatedPoint = 1;
            try
            {
                if (vocas.Count > 0)
                {
                    this.BeginTransaction();

                    //update user vocas
                    foreach (var vo in vocas)
                    {
                        var voca = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == vo.ID);
                        if (voca != null)
                        {
                            voca.HasLearnt = vo.IsCorrect;
                            voca.Update_Date = DateTime.Now;
                        }
                    }

                    //create test result
                    var firstVoca = vocas.FirstOrDefault();
                    var vocaCate = this.ms_vocacategories.FirstOrDefault(ss => ss.ID == firstVoca.CategoryID);
                    var vocaSet = this.ms_vocasets.FirstOrDefault(ss => ss.Code == vocaCate.VocaSet);
                    var numOfCorrectVocas = vocas.Count(ss => ss.IsCorrect == CommonData.Status.Enable);

                    #region Diem tich luy
                    if (CommonMethod.ParseDecimal(vocaSet.Fee) > 0)
                    {
                        var isExsitResult = ms_testresults.Any(ss => ss.CategoryCode == vocaCate.Code && ss.UserName == firstVoca.UserName);
                        if (!isExsitResult)
                        {
                            //pass
                            if (numOfCorrectVocas >= (vocas.Count * 8 / 10))
                            {
                                accumulatedPoint = 2;
                            }
                            //pass & 100% correct
                            if (numOfCorrectVocas == vocas.Count)
                            {
                                accumulatedPoint = 5;
                            }
                            //pass & 100% correct & completed time = 1/2 required time
                            if (numOfCorrectVocas == vocas.Count && (firstVoca.CompletedTime <= (vocaCate.RequiredTimePerVoca * vocas.Count / 2)))
                            {
                                accumulatedPoint = 10;
                            }
                        }
                    }

                    //update for user
                    var user = this.ms_users.FirstOrDefault(ss => ss.UserName == firstVoca.UserName);
                    if (user != null)
                    {
                        user.AccumulatedPoint = CommonMethod.ParseInt(user.AccumulatedPoint) + accumulatedPoint;
                    }

                    #endregion

                    Nihongo.Dal.Mapping.ms_testresults test = new Mapping.ms_testresults();
                    test.Code = firstVoca.UserName + "_" + vocaCate.Code + "_" + (DateTime.Now.ToString(CommonData.DateFormat.YyyyMMddHHmmss));
                    test.CategoryCode = vocaCate.Code;
                    test.UserName = firstVoca.UserName;
                    test.CreateDate = DateTime.Now;
                    test.NumOfVocas = vocas.Count;
                    test.NumOfCorrectVocas = numOfCorrectVocas;
                    test.IsPass = (numOfCorrectVocas >= (vocas.Count * 8 / 10)) ? CommonData.Status.Enable : CommonData.Status.Disable;
                    test.RequiredTimePerVoca = vocaCate.RequiredTimePerVoca;
                    test.TotalRequiredTime = vocaCate.RequiredTimePerVoca * vocas.Count;
                    test.CompletedTime = firstVoca.CompletedTime;
                    test.Status = CommonData.Status.Enable;
                    test.Description = (numOfCorrectVocas >= (vocas.Count * 8 / 10)) ? "Chúc mừng bạn đã vượt qua được bài kiểm tra" : "Bạn đã không vượt qua được bài kiểm tra. Hãy ôn lại";

                    ms_testresults.AddObject(test);

                    //update vocaset
                    
                    if (vocaSet != null)
                    {
                        vocaSet.NumOfFinishedPerson = CommonMethod.ParseInt(vocaSet.NumOfFinishedPerson) + 1;
                    }

                    returnCode = this.Saves();
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
            catch (Exception ex)
            {
                this.Rollback();
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int UpdateFastTestVoca(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;

            try
            {
                //update has learnt
                var vo = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == voca.ID);
                if (voca != null)
                {
                    vo.HasLearnt = voca.IsCorrect;
                    vo.Update_Date = DateTime.Now;

                    if (voca.IsCorrect == CommonData.Status.Enable)
                    {
                        vo.Level += 1;
                        if (vo.Level > 10)
                        {
                            vo.Level = 10;
                        }
                    }
                    else
                    {
                        vo.Level -= 2;
                        if (vo.Level < 0)
                        {
                            vo.Level = 0;
                        }
                    }
                }

                //update user's point
                if (!CommonMethod.IsNullOrEmpty(voca.UserName))
                {

                    var us = this.ms_users.FirstOrDefault(ss => ss.UserName == voca.UserName);
                    if (us != null)
                    {
                        if (voca.IsCorrect == CommonData.Status.Enable)
                        {
                            us.Point = CommonMethod.ParseInt(us.Point) + 1;
                        }
                    }
                }

                returnCode = this.Saves();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }



        internal int UpdateUserVocas(string userName, List<MS_UserVocabulariesModels> vocas)
        {
            int returnCode = 0;

            try
            {
                foreach (var vo in vocas)
                {
                    var voca = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == vo.ID);
                    if (voca != null)
                    {
                        voca.HasLearnt = vo.IsCorrect;
                        voca.Update_Date = DateTime.Now;

                        if (vo.IsCorrect == CommonData.Status.Enable)
                        {
                            voca.Level += 1;
                            if (voca.Level > 10)
                            {
                                voca.Level = 10;
                            }
                        }
                        else
                        {
                            voca.Level -= 2;
                            if (voca.Level < 0)
                            {
                                voca.Level = 0;
                            }
                        }
                    }

                    //update user's point
                    if (!CommonMethod.IsNullOrEmpty(userName))
                    {

                        var us = this.ms_users.FirstOrDefault(ss => ss.UserName == userName);
                        if (us != null)
                        {
                            if (vo.IsCorrect == CommonData.Status.Enable)
                            {
                                us.Point = CommonMethod.ParseInt(us.Point) + 1;
                            }
                        }
                    }
                }

                returnCode = this.Saves();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int UpdateHasMarked(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;

            try
            {
                var vocaDb = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == voca.ID);
                if (vocaDb != null)
                {
                    vocaDb.HasMarked = voca.HasMarked;

                    returnCode = this.Saves();
                }
            }

            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

    }
}