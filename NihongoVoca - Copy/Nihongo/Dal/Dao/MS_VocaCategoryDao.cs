using System;
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

                if (!CommonMethod.IsNullOrEmpty(model.VocaSet))
                {
                    query = query.Where(ss => ss.VocaSet == model.VocaSet);
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
                          join vs in this.ms_vocasets on ss.VocaSet equals vs.Code
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

        internal int CheckCompletedPreCate(int id, string userName, out bool isOK)
        {
            int returnCode = 0;
            isOK = false;

            try
            {
                var vocaCate = (from ca in this.ms_vocacategories
                                join se in this.ms_vocasets on ca.VocaSet equals se.Code
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
                            isOK = this.ms_testresults.Any(ss => ss.CategoryCode == vocaCate.CatePreCode 
                                && ss.UserName == userName 
                                && ss.IsPass == CommonData.Status.Enable);
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
                           join se in this.ms_vocasets on ca.VocaSet equals se.Code
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

        internal int CheckCompletedCate(int id, string userName, out bool isOK)
        {
            int returnCode = 0;
            isOK = false;

            try
            {
                var vocaCate = (from ca in this.ms_vocacategories
                                join se in this.ms_vocasets on ca.VocaSet equals se.Code
                                where ca.ID == id
                                select new { Code = ca.Code, CateLineNumber = ca.LineNumber, IsSequence = se.IsSequence, SetCode = se.Code }).FirstOrDefault();

                if (vocaCate != null)
                {
                    isOK = this.ms_testresults.Any(ss => ss.CategoryCode == vocaCate.Code
                                && ss.UserName == userName
                                && ss.IsPass == CommonData.Status.Enable);
                }

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectVocaCategoryBySet(MS_VocaCategoriesModels model, out List<MS_VocaCategoriesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaCategoriesModels>();

            try
            {
                if (model.VocaSetID > 0)
                {
                    results = (from ss in this.ms_vocacategories
                               join vs in this.ms_vocasets on ss.VocaSet equals vs.Code
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
    }
}