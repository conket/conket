using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;
using Ivs.Core.Interface;

namespace Nihongo.Dal.Dao
{
    public class MS_TestResultDao : BaseDao
    {
        internal int SelectTopCompleted(MS_TestResultModels model, out List<MS_TestResultModels> results)
        {
            int returnCode = 0;
            results = new List<MS_TestResultModels>();
            try
            {
                var testResult = this.ms_testresults.Where(ss => ss.IsPass == CommonData.Status.Enable).AsQueryable();
                var vocaCate = this.ms_vocacategories.Where(ss => ss.ID == model.CategoryID).AsQueryable();

                results = (from ss in testResult
                           join ca in vocaCate on ss.CategoryCode equals ca.Code
                           join us in ms_users on ss.UserName equals us.UserName
                           orderby new { ss.NumOfVocas, ss.CompletedTime}
                           select new MS_TestResultModels
                       {
                           ID = ss.ID,
                           CategoryCode = ss.CategoryCode,
                           CompletedTime = ss.CompletedTime.Value,
                           CreateDate = ss.CreateDate.Value,
                           NumOfCorrectVocas = ss.NumOfCorrectVocas.Value,
                           NumOfVocas = ss.NumOfVocas.Value,

                           UserName = us.UserName,
                           UserUrlImage = us.UrlImage,
                           UserDisplayName = us.DisplayName,
                       })
                    .ToList()
                    .GroupBy(ss => new { ss.UserName, ss.UserUrlImage, ss.UserDisplayName })
                    .Select(ss => new MS_TestResultModels
                    {
                        ID = ss.FirstOrDefault().ID,
                        CategoryCode = ss.FirstOrDefault().CategoryCode,
                        CompletedTime = ss.FirstOrDefault().CompletedTime.Value,
                        CreateDate = ss.FirstOrDefault().CreateDate,
                        NumOfCorrectVocas = ss.FirstOrDefault().NumOfCorrectVocas,
                        NumOfVocas = ss.FirstOrDefault().NumOfVocas,

                        UserName = ss.Key.UserName,
                        UserUrlImage = ss.Key.UserUrlImage,
                        UserDisplayName = ss.Key.UserDisplayName,
                    })
                    .Take(10)
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectHightestPointData(MS_UsersModels model, int pageIndex, int pageSize, out IPagedList<MS_UsersModels> results)
        {
            int returnCode = 0;
            results = null;
            try
            {
                //var testResult = this.ms_testresults.Where(ss => ss.IsPass == CommonData.Status.Enable).AsQueryable();

                //results = (from ss in testResult
                //           //join ca in this.ms_vocacategories on ss.CategoryCode equals ca.Code
                //           join us in ms_users on ss.UserName equals us.UserName
                //           orderby ss.CompletedTime
                //           select new MS_TestResultModels
                //           {
                //               ID = ss.ID,
                //               CategoryCode = ss.CategoryCode,
                //               CompletedTime = ss.CompletedTime.Value,
                //               CreateDate = ss.CreateDate.Value,
                //               NumOfCorrectVocas = ss.NumOfCorrectVocas.Value,
                //               NumOfVocas = ss.NumOfVocas.Value,

                //               UserName = us.UserName,
                //               UserUrlImage = us.UrlImage,
                //               UserDisplayName = us.DisplayName,
                //           })
                results = this.ms_users.Where(ss => ss.Status == CommonData.Status.Enable)
                            .OrderByDescending(ss => ss.Point)
                            .Select(ss => new MS_UsersModels
                            {
                                ID = ss.ID,
                                UserName = ss.UserName,
                                Status = ss.Status,
                                LoginState = ss.LoginState,
                                DisplayName = ss.DisplayName,
                                UrlImage = ss.UrlImage,
                                Point = ss.Point ?? 0,
                            })
                            .ToPagedList(pageIndex, pageSize);
                    //.ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectHightestAccPointData(MS_UsersModels model, int pageIndex, int pageSize, out IPagedList<MS_UsersModels> results)
        {
            int returnCode = 0;
            results = null;
            try
            {
                results = this.ms_users.Where(ss => ss.Status == CommonData.Status.Enable)
                            .OrderByDescending(ss => ss.AccumulatedPoint)
                            .Select(ss => new MS_UsersModels
                            {
                                ID = ss.ID,
                                UserName = ss.UserName,
                                Status = ss.Status,
                                LoginState = ss.LoginState,
                                DisplayName = ss.DisplayName,
                                UrlImage = ss.UrlImage,
                                Point = ss.Point ?? 0,
                                AccumulatedPoint= ss.AccumulatedPoint ?? 0
                            })
                            .ToPagedList(pageIndex, pageSize);
                //.ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }
    }
}