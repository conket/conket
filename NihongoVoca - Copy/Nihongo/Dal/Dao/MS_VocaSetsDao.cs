using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_VocaSetsDao : BaseDao
    {
        public int SelectData(MS_VocaSetsModels model, out List<MS_VocaSetsModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaSetsModels>();

            try
            {
                var query = this.ms_vocasets.AsQueryable();

                if (!CommonMethod.IsNullOrEmpty(model.Code))
                {
                    query = query.Where(ss => ss.Code == model.Code);
                }

                query = query.Where(ss => ss.Status == CommonData.Status.Enable);

                results = query.Select(ss => new MS_VocaSetsModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Name1 = ss.Name1,
                    Fee = ss.Fee,
                    PriorityLevel = ss.PriorityLevel,
                    UrlImage = ss.UrlImage,
                    UrlDisplay = ss.UrlDisplay,
                    UsefulLife = ss.UsefulLife,
                    Description = ss.Description,
                    Status = ss.Status,
                    NumOfCategories = ss.NumOfCategories,
                    NumOfVocas = ss.NumOfVocas,
                    NumOfFinishedPerson = ss.NumOfFinishedPerson,
                    NumOfRegistedPerson = ss.NumOfRegistedPerson,
                })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectMostPopularVocaSetsData(MS_VocaSetsModels model, out List<MS_VocaSetsModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaSetsModels>();

            try
            {
                var query = this.ms_vocasets.AsQueryable();

                if (!CommonMethod.IsNullOrEmpty(model.Code))
                {
                    query = query.Where(ss => ss.Code == model.Code);
                }

                query = query.Where(ss => ss.Status == CommonData.Status.Enable);
                query = query.OrderByDescending(ss => ss.NumOfRegistedPerson);
                results = query.Select(ss => new MS_VocaSetsModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Name1 = ss.Name1,
                    Fee = ss.Fee,
                    PriorityLevel = ss.PriorityLevel,
                    UrlImage = ss.UrlImage,
                    UrlDisplay = ss.UrlDisplay,
                    UsefulLife = ss.UsefulLife,
                    Description = ss.Description,
                    Status = ss.Status,
                    NumOfCategories = ss.NumOfCategories,
                    NumOfVocas = ss.NumOfVocas,
                    
                })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectVocaSetByID(int id, out MS_VocaSetsModels result)
        {
            int returnCode = 0;
            result = new MS_VocaSetsModels();

            try
            {
                var registedSet = this.ms_registedvocasets.Where(ss => ss.VocaSetID == id);
                var sets = this.ms_vocasets.Where(ss => ss.ID == id).AsQueryable();

                result = (from ss in sets
                         join re in registedSet on ss.ID equals re.VocaSetID into regis
                         from re in regis.DefaultIfEmpty()
                        select new MS_VocaSetsModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Name1 = ss.Name1,
                    Fee = ss.Fee,
                    UrlImage = ss.UrlImage,
                    UrlDisplay = ss.UrlDisplay,
                    UsefulLife = ss.UsefulLife,
                    Description = ss.Description,
                    NumOfCategories = ss.NumOfCategories,
                    NumOfVocas = ss.NumOfVocas,
                    IsSequence = ss.IsSequence,
                    StartDate = re == null ? null : re.StartDate,
                    EndDate = re == null ? null : re.EndDate,
                    IsKanji = ss.IsKanji,
                }).FirstOrDefault();

                //if (result != null)
                //{
                //    string vocaSetCode = result.Code;
                //    result.VocaCategories = this.ms_vocacategories.Where(ss => ss.VocaSet == vocaSetCode)
                //        .Select(ss => new MS_VocaCategoriesModels
                //        {
                //            ID = ss.ID,
                //            Code = ss.Code,
                //            Name1 = ss.Name1,
                //            NumOfVocas = ss.NumOfVocas,
                //            UrlDisplay = ss.UrlDisplay,
                //            UrlImage = ss.UrlImage,
                //            VocaSet = ss.VocaSet,
                //            Description = ss.Description,
                //        });
                //}
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectFreeVocaSetsData(MS_VocaSetsModels model, out List<MS_VocaSetsModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaSetsModels>();
            try
            {
                var query = this.ms_vocasets.AsQueryable();

                query = query.Where(ss => ss.Status == CommonData.Status.Enable && ss.Fee == 0);
                
                results = query.Select(ss => new MS_VocaSetsModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Name1 = ss.Name1,
                    Fee = ss.Fee,
                    PriorityLevel = ss.PriorityLevel,
                    UrlImage = ss.UrlImage,
                    UrlDisplay = ss.UrlDisplay,
                    UsefulLife = ss.UsefulLife,
                    Description = ss.Description,
                    Status = ss.Status,
                    NumOfCategories = ss.NumOfCategories,
                    NumOfVocas = ss.NumOfVocas,

                })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectKanjiVocaSetsData(MS_VocaSetsModels model, out List<MS_VocaSetsModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocaSetsModels>();
            try
            {
                var query = this.ms_vocasets.AsQueryable();

                query = query.Where(ss => ss.Status == CommonData.Status.Enable && ss.IsKanji == CommonData.Status.Enable);

                results = query.Select(ss => new MS_VocaSetsModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Name1 = ss.Name1,
                    Fee = ss.Fee,
                    PriorityLevel = ss.PriorityLevel,
                    UrlImage = ss.UrlImage,
                    UrlDisplay = ss.UrlDisplay,
                    UsefulLife = ss.UsefulLife,
                    Description = ss.Description,
                    Status = ss.Status,
                    NumOfCategories = ss.NumOfCategories,
                    NumOfVocas = ss.NumOfVocas,

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