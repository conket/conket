﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;
using Ivs.Core.Interface;

namespace Nihongo.Dal.Dao
{
    public class MS_UsersDao : BaseDao
    {
        public int SelectData(MS_UsersModels model, out List<MS_UsersModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UsersModels>();

            try
            {
                var query = this.ms_users.AsQueryable();

                if (!CommonMethod.IsNullOrEmpty(model.UserName))
                {
                    query = query.Where(ss => ss.UserName == model.UserName);
                }

                if (!CommonMethod.IsNullOrEmpty(model.Status))
                {
                    query = query.Where(ss => ss.Status == model.Status);
                }

                results = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,
                    LoginState = ss.LoginState,
                    LastVisitedDate = ss.LastVisitedDate,
                })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        public int InsertData(MS_UsersModels insertModel)
        {
            int returnCode = 0;
            try
            {
                //Create new user
                Nihongo.Dal.Mapping.ms_users user = new Mapping.ms_users()
                {
                    UserName = insertModel.UserName,
                    Email = insertModel.Email,
                    Password = insertModel.Password,
                    DisplayName = insertModel.DisplayName,
                    IsAdmin = CommonData.Status.Disable,
                    Status = CommonData.Status.Enable,
                    SystemData = CommonData.Status.Disable,
                    LoginState = CommonData.Status.Disable,
                };

                ms_users.AddObject(user);

                if (insertModel.CreateVoca)
                {
                    var vocas = this.ms_vocabularydetails.Where(ss => ss.Status == CommonData.Status.Enable);
                    foreach (var voca in vocas)
                    {
                        Nihongo.Dal.Mapping.ms_uservocabularies usv = new Mapping.ms_uservocabularies()
                        {
                            UserName = insertModel.UserName,
                            VocaDetailID = voca.ID,
                            Update_Date = DateTime.Now,
                            Level = 10,
                            HasLearnt = CommonData.Status.Disable,
                            StartDate = DateTime.Now.AddDays(-1),
                            EndDate = DateTime.Now.AddDays(365),
                        };

                        ms_uservocabularies.AddObject(usv);
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

        internal int SelectDataByUserName(MS_UsersModels model, out MS_UsersModels user)
        {
            int returnCode = 0;
            user = new MS_UsersModels();

            try
            {
                var query = this.ms_users.Where(ss => ss.UserName == model.UserName && ss.Password == model.Password);
                user = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,
                    
                })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectDataByUserName(string UserName, string Password, out MS_UsersModels user)
        {
            int returnCode = 0;
            user = new MS_UsersModels();

            try
            {
                var query = this.ms_users.Where(ss => ss.UserName == UserName && ss.Password == Password);

                user = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,
                    
                })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int UpdateState(MS_UsersModels user)
        {
            int returnCode = 0;
            try
            {
                var ur = this.ms_users.FirstOrDefault(ss => ss.UserName == user.UserName);
                if (ur != null)
                {
                    ur.LoginState = user.LoginState;
                    ur.LastVisitedDate = user.LastVisitedDate;

                    returnCode = this.Saves();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int ProcessBatch()
        {
            int returnCode = 0;
            try
            {
                //this.ExecuteStoreCommand("DELETE FROM ms_uservocabularies");

                #region Update vocabularies for users

                //var users = this.ms_users.ToList();
                //var vocas = this.ms_vocabularies.ToList();
                //foreach (var user in users)
                //{
                //    foreach (var voca in vocas)
                //    {
                //        var userVoca = this.ms_uservocabularies.FirstOrDefault(ss => ss.UserName == user.UserName && ss.VocabularyCode == voca.Code);
                //        if (userVoca == null)
                //        {
                //            Mapping.ms_uservocabularies urVoca = new Mapping.ms_uservocabularies();
                //            urVoca.VocabularyCode = voca.Code;
                //            urVoca.UserName = user.UserName;
                //            urVoca.Level = 9;
                //            urVoca.Update_Date = DateTime.Now;

                //            ms_uservocabularies.AddObject(urVoca);
                //        }
                //    }
                //}

                //returnCode = this.Saves();

                #endregion

                #region Update level

                //3 ngày chưa ôn giảm 1 level
                DateTime date = (DateTime.Now.AddDays(-3));
                var usrVocas = this.ms_uservocabularies.Where(ss => ss.Update_Date <= date).AsQueryable();
                foreach (var usrVoca in usrVocas)
                {
                    int numOfDecreaseLevel = (DateTime.Now - usrVoca.Update_Date.Value).Days/3;
                    usrVoca.Level = (usrVoca.Level - numOfDecreaseLevel) < 0 ? 0 : (usrVoca.Level - numOfDecreaseLevel);
                    usrVoca.Update_Date = DateTime.Now;
                }

                returnCode = this.Saves();
                
                #endregion

                

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int ChangePassword(MS_UsersModels user)
        {
            int returnCode = 0;
            try
            {
                var ur = this.ms_users.FirstOrDefault(ss => ss.UserName == user.UserName);
                if (ur != null)
                {
                    ur.Password = user.Password;

                    returnCode = this.Saves();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectDataByCode(MS_UsersModels user, out MS_UsersModels result)
        {
            int returnCode = 0;
            result = new MS_UsersModels();
            try
            {
                result = this.ms_users
                    .Where(ss => ss.UserName == user.UserName && ss.Status == user.Status)
                    .Select(ss => new MS_UsersModels
                    {
                        UserName = ss.UserName,
                        DisplayName = ss.DisplayName,
                        ID = ss.ID,
                        IsAdmin = ss.IsAdmin,
                        Status = ss.Status,
                        SystemData = ss.SystemData,
                    })
                    .FirstOrDefault();
                
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
                                AccumulatedPoint = ss.AccumulatedPoint ?? 0
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

        internal int SelectSmartestData(MS_UsersModels model, int pageIndex, int pageSize, out IPagedList<MS_UsersModels> results)
        {
            int returnCode = 0;
            results = null;
            try
            {
                var groupUserVoca = this.ms_uservocabularies.Where(ss => ss.HasLearnt == CommonData.Status.Enable)
                    .GroupBy(ss => ss.UserName)
                    .Select(ss => new
                    {
                        UserName = ss.Key,
                        TotalHasLearnt = ss.Count(),
                    });
                    //.ToList();

                results = this.ms_users.Where(ss => ss.Status == CommonData.Status.Enable)//.ToList()
                            .Join(groupUserVoca, us => us.UserName, uv => uv.UserName, (us, uv) => new { User = us, UserVoca = uv })
                            .Select(ss => new MS_UsersModels
                            {
                                ID = ss.User.ID,
                                UserName = ss.User.UserName,
                                Status = ss.User.Status,
                                LoginState = ss.User.LoginState,
                                DisplayName = ss.User.DisplayName,
                                UrlImage = ss.User.UrlImage,
                                TotalHasLearnt = ss.UserVoca.TotalHasLearnt,
                            })
                            .OrderByDescending(ss => ss.TotalHasLearnt)
                            .ToPagedList(pageIndex, pageSize);
                //.ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int FLogin(MS_UsersModels model, out MS_UsersModels user)
        {
            int returnCode = 0;
            user = null;
            try
            {
                var query = this.ms_users.Where(ss => ss.UserName == model.UserName && ss.Password == model.Password);
                user = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,

                })
                    .FirstOrDefault();

                if (user == null)
                {
                    returnCode = InsertData(model);

                    user = new MS_UsersModels()
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Status = CommonData.Status.Enable,
                        DisplayName = model.DisplayName,
                        SystemData = CommonData.Status.Disable,
                        IsAdmin = CommonData.Status.Disable,
                    };
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