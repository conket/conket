﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;
using Ivs.Core.Interface;
using Ivs.Core.Data;

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

        public int InsertData(MS_UsersModels insertModel, out int id)
        {
            int returnCode = 0;
            id = -1;
            try
            {
                this.BeginTransaction();

                //Create new user
                Nihongo.Dal.Mapping.ms_users user = new Mapping.ms_users()
                {
                    UserName = insertModel.UserName,
                    Email = insertModel.Email,
                    UrlImage = insertModel.UrlImage,
                    Password = insertModel.Password,
                    DisplayName = insertModel.DisplayName,
                    IsAdmin = CommonData.Status.Disable,
                    Status = CommonData.Status.Enable,
                    SystemData = CommonData.Status.Disable,
                    LoginState = CommonData.Status.Enable,
                    VocaPerLearn = 5,
                    VocaPerReview = 10,
                    SoundEffect = CommonData.Status.Enable,
                };

                ms_users.AddObject(user);
                returnCode = this.Saves();

                if (returnCode == 0)
                {
                    id = user.ID;
                    if (insertModel.CreateVoca)
                    {
                        var vocas = this.ms_vocabularydetails.AsQueryable();
                        foreach (var voca in vocas)
                        {
                            Nihongo.Dal.Mapping.ms_uservocabularies usv = new Mapping.ms_uservocabularies()
                            {
                                UserID = user.ID,
                                VocaDetailID = voca.ID,
                                UpdatedDate = DateTime.Now,

                                Level = 0,
                                HasLearnt = CommonData.Status.Disable,
                                HasMarked = CommonData.Status.Disable,
                                IsIgnore = CommonData.Status.Disable,
                                NumOfWrong = 0,

                                StartDate = DateTime.Now.AddDays(-1),
                                EndDate = DateTime.Now.AddYears(1),
                            };

                            ms_uservocabularies.AddObject(usv);
                        }
                    }
                    returnCode = this.Saves();
                }

                if (returnCode == 0)
                {
                    returnCode = this.Commit();
                }
                else
                {
                    this.Rollback();
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
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

        internal int SelectDataByUserName(string UserName, string Password, out MS_UsersModels user, out string error)
        {
            int returnCode = 0;
            user = new MS_UsersModels();
            error = string.Empty;
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
                    UrlImage = ss.UrlImage,
                    VocaPerLearn = ss.VocaPerLearn ?? 5,
                    VocaPerReview = ss.VocaPerReview ?? 10,
                    SoundEffect = ss.SoundEffect,
                })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
                error = ex.Message;
            }

            return returnCode;
        }

        internal int UpdateState(ref MS_UsersModels user)
        {
            int returnCode = 0;
            try
            {
                int userID = user.ID;
                var ur = this.ms_users.FirstOrDefault(ss => ss.ID == userID);
                if (ur != null)
                {
                    ur.LoginState = user.LoginState;
                    ur.LastVisitedDate = user.LastVisitedDate;

                    returnCode = this.Saves();

                    user.DisplayName = ur.DisplayName;
                    user.Email = ur.Email;
                    user.IsAdmin = ur.IsAdmin;
                    user.LastVisitedDate = ur.LastVisitedDate;
                    user.LoginState = ur.LoginState;
                    user.Password = ur.Password;
                    user.Status = ur.Status;
                    user.SystemData = ur.SystemData;
                    user.UrlImage = ur.UrlImage;
                    user.UserName = ur.UserName;
                    user.VocaPerLearn = ur.VocaPerLearn ?? 5;
                    user.VocaPerReview = ur.VocaPerReview ?? 10;
                    user.SoundEffect = ur.SoundEffect;
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
                var usrVocas = this.ms_uservocabularies.Where(ss => ss.UpdatedDate <= date).AsQueryable();
                foreach (var usrVoca in usrVocas)
                {
                    int numOfDecreaseLevel = (DateTime.Now - usrVoca.UpdatedDate.Value).Days/3;
                    usrVoca.Level = (usrVoca.Level - numOfDecreaseLevel) < 0 ? 0 : (usrVoca.Level - numOfDecreaseLevel);
                    usrVoca.UpdatedDate = DateTime.Now;
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
                var ur = this.ms_users.FirstOrDefault(ss => ss.ID == user.ID);
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
                    .Where(ss => ss.ID == user.ID && ss.Status == user.Status)
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
                    .GroupBy(ss => ss.UserID)
                    .Select(ss => new
                    {
                        UserID = ss.Key,
                        TotalHasLearnt = ss.Count(),
                    });
                    //.ToList();

                results = this.ms_users.Where(ss => ss.Status == CommonData.Status.Enable)//.ToList()
                            .Join(groupUserVoca, us => us.ID, uv => uv.UserID, (us, uv) => new { User = us, UserVoca = uv })
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
                var query = this.ms_users.Where(ss => ss.Email == model.Email);
                user = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    Email = ss.Email,
                    UrlImage = ss.UrlImage,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,
                    VocaPerReview = ss.VocaPerReview ?? 5,
                    VocaPerLearn = ss.VocaPerLearn ?? 10,
                    SoundEffect = ss.SoundEffect,
                })
                    .FirstOrDefault();

                if (user == null)
                {
                    int userID = -1;
                    returnCode = InsertData(model, out userID);

                    user = new MS_UsersModels()
                    {
                        ID = userID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        UrlImage = model.UrlImage,
                        Status = CommonData.Status.Enable,
                        DisplayName = model.DisplayName,
                        SystemData = CommonData.Status.Disable,
                        IsAdmin = CommonData.Status.Disable,
                        VocaPerReview = 5,
                        VocaPerLearn = 10,
                        SoundEffect = CommonData.Status.Enable,
                    };
                }
                else
                {
                    //user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.UrlImage = model.UrlImage;
                    user.DisplayName = model.DisplayName;
                    user.LoginState = CommonData.Status.Enable;
                    
                    returnCode = this.Saves();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }

        internal int Register(MS_UsersModels model, out MS_UsersModels user)
        {
            int returnCode = 0;
            user = null;
            try
            {
                var query = this.ms_users.Where(ss => ss.Email == model.Email);
                user = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    UrlImage = ss.UrlImage,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,
                    VocaPerReview = ss.VocaPerReview ?? 5,
                    VocaPerLearn = ss.VocaPerLearn ?? 10,
                    SoundEffect = ss.SoundEffect,
                })
                    .FirstOrDefault();

                if (user == null)
                {
                    int userID = -1;
                    returnCode = InsertData(model, out userID);

                    user = new MS_UsersModels()
                    {
                        ID = userID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        Status = CommonData.Status.Enable,
                        DisplayName = model.DisplayName,
                        SystemData = CommonData.Status.Disable,
                        IsAdmin = CommonData.Status.Disable,
                        VocaPerReview = 5,
                        VocaPerLearn = 10,
                        SoundEffect = CommonData.Status.Enable,
                    };
                }
                else
                {
                    returnCode = CommonData.DbReturnCode.DuplicateKey;
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }

        internal int GLogin(MS_UsersModels model, out MS_UsersModels user)
        {
            int returnCode = 0;
            user = null;
            try
            {
                var query = this.ms_users.Where(ss => ss.Email == model.Email);
                user = query.Select(ss => new MS_UsersModels
                {
                    ID = ss.ID,
                    UserName = ss.UserName,
                    Password = ss.Password,
                    UrlImage = ss.UrlImage,
                    Status = ss.Status,
                    DisplayName = ss.DisplayName,
                    SystemData = ss.SystemData,
                    IsAdmin = ss.IsAdmin,
                    VocaPerReview = ss.VocaPerReview ?? 5,
                    VocaPerLearn = ss.VocaPerLearn ?? 10,
                    SoundEffect = ss.SoundEffect,
                })
                    .FirstOrDefault();

                if (user == null)
                {
                    int userID = -1;
                    returnCode = InsertData(model, out userID);

                    user = new MS_UsersModels()
                    {
                        ID = userID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        Status = CommonData.Status.Enable,
                        DisplayName = model.DisplayName,
                        SystemData = CommonData.Status.Disable,
                        IsAdmin = CommonData.Status.Disable,
                        VocaPerReview = 5,
                        VocaPerLearn = 10,
                        SoundEffect = CommonData.Status.Enable,
                    };
                }
                else
                {
                    //user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.UrlImage = model.UrlImage;
                    user.DisplayName = model.DisplayName;
                    user.LoginState = CommonData.Status.Enable;

                    returnCode = this.Saves();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }


        internal int SelectUsersData(int userID, out List<MS_UsersModels> users)
        {
            int returnCode = 0;
            users = new List<MS_UsersModels>();
            try
            {
                users = this.ms_users
                    .Where(s => s.ID != userID)
                    .OrderByDescending(ss => ss.Point).ThenByDescending(ss => ss.NumOfLearntVoca)
                    .Select(s => new MS_UsersModels
                    {
                        ID = s.ID,
                        DisplayName = s.DisplayName,
                        LastVisitedDate = s.LastVisitedDate,
                        LoginState = s.LoginState,
                        NumOfLearntVoca = s.NumOfLearntVoca ?? 0,
                        Point = s.Point ?? 0,
                        UrlImage = s.UrlImage,

                        Followers = s.ms_userfollowings.Count(ss => ss.FollowerID == s.ID),
                        Followings = s.ms_userfollowings1.Count(ss => ss.UserID == s.ID),
                        Followed = s.ms_userfollowings.Any(ss => ss.UserID == userID),
                    })
                    .Take(20)
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }


        internal int UpdateSetting(int userID, SettingModel setting)
        {
            int returnCode = 0;
            try
            {
                var user = this.ms_users.FirstOrDefault(ss => ss.ID == userID);
                if (user != null)
                {
                    user.VocaPerReview = setting.VocaPerReview;
                    user.VocaPerLearn = setting.VocaPerLearn;
                    user.SoundEffect = setting.SoundEffect ? CommonData.Status.Enable : CommonData.Status.Disable;
                    returnCode = this.Saves();
                }

            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }
            return returnCode;
        }

        internal int SelectUserByID(ref MS_UsersModels user)
        {
            int returnCode = 0;
            try
            {
                int id = user.ID;
                user = this.ms_users.Where(ss => ss.ID == id)
                    .Select(ss => new MS_UsersModels
                    {
                        ID = ss.ID,
                        VocaPerReview = ss.VocaPerReview ?? 5,
                        VocaPerLearn = ss.VocaPerLearn ?? 10,
                        SoundEffect = ss.SoundEffect,
                    })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        public int Follow(int userID, int followerID, bool follow)
        {
            int returnCode = 0;
            try
            {
                Mapping.ms_userfollowings followVal = ms_userfollowings.FirstOrDefault(ss => ss.UserID == userID && ss.FollowerID == followerID);
                if (followVal == null)
                {
                    if (follow)
                    {
                        followVal = new Mapping.ms_userfollowings();
                        followVal.UserID = userID;
                        followVal.FollowerID = followerID;
                        followVal.UpdatedDate = DateTime.Now;
                        ms_userfollowings.AddObject(followVal);
                    }
                    returnCode = this.Saves();
                }
                else
                {
                    if (!follow)
                    {
                        ms_userfollowings.DeleteObject(followVal);
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

        internal int SelectFollowersByUser(int id, out List<MS_UsersModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UsersModels>();
            try
            {
                results = ms_userfollowings.Where(ss => ss.FollowerID == id)
                    .Select(ss => new MS_UsersModels
                    {
                        ID = ss.ms_users1.ID,
                        DisplayName = ss.ms_users1.DisplayName,
                        UrlImage = ss.ms_users1.UrlImage,
                        NumOfLearntVoca = ss.ms_users1.NumOfLearntVoca ?? 0,
                        Point = ss.ms_users1.Point ?? 0,
                        Followers = ss.ms_users1.ms_userfollowings.Count(),
                        Followings = ss.ms_users1.ms_userfollowings1.Count(),
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }
        internal int SelectFollowingsByUser(int id, out List<MS_UsersModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UsersModels>();
            try
            {
                results = ms_userfollowings.Where(ss => ss.UserID == id)
                    .Select(ss => new MS_UsersModels
                    {
                        ID = ss.ms_users.ID,
                        DisplayName = ss.ms_users.DisplayName,
                        UrlImage = ss.ms_users.UrlImage,
                        NumOfLearntVoca = ss.ms_users.NumOfLearntVoca ?? 0,
                        Point = ss.ms_users.Point ?? 0,
                        Followers = ss.ms_users.ms_userfollowings.Count(),
                        Followings = ss.ms_users.ms_userfollowings1.Count(),
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectActivities(int id, out List<MS_TestResultModels> results)
        {
            int returnCode = 0;
            results = new List<MS_TestResultModels>();
            try
            {
                //var users = from fo in ms_userfollowings
                //            join te in ms_testresults on fo.Fo
                results = (from te in ms_testresults
                           join fo in ms_userfollowings on te.UserID equals fo.FollowerID
                           join us in ms_users on fo.FollowerID equals us.ID
                           //join ca in ms_vocacategories on te.CategoryID equals ca.ID
                           //join vs in ms_vocasets on ca.VocaSetID equals vs.ID
                           where fo.UserID == id
                           orderby te.CreateDate descending
                           select new MS_TestResultModels
                               {
                                   ID = te.ID,
                                   CategoryID = te.CategoryID,
                                   UserID = te.UserID,
                                   UserDisplayName = us.DisplayName,
                                   UserUrlImage = us.UrlImage,
                                   CreateDate = te.CreateDate,
                                   Description = te.Description,
                               })
                    .Take(20)
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