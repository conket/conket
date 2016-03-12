using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;
using System.Collections;

namespace Nihongo.Dal.Dao
{
    public class MS_VocabulariesDao : BaseDao
    {
        public int SelectData(MS_VocabulariesModels model, out List<MS_VocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_VocabulariesModels>();

            try
            {
                var query = this.ms_vocabularies.AsQueryable();

                if (!CommonMethod.IsNullOrEmpty(model.Code))
                {
                    query = query.Where(ss => ss.Code == model.Code);
                }
                if (!CommonMethod.IsNullOrEmpty(model.Romaji))
                {
                    query = query.Where(ss => ss.Romaji == model.Romaji);
                }
                //if (!CommonMethod.IsNullOrEmpty(model.LessonCode))
                //{
                //    query = query.Where(ss => ss.LessonCode == model.LessonCode);
                //}
                if (!CommonMethod.IsNullOrEmpty(model.Type))
                {
                    query = query.Where(ss => ss.Type == model.Type);
                }

                List<MS_VocabulariesModels> normals = new List<MS_VocabulariesModels>();
                if (model.HasNormal)
                {
                    normals = query.Where(ss => ss.HasDiacritic == CommonData.Status.Disable
                        && ss.HasLongSound == CommonData.Status.Disable
                        && ss.HasTsu == CommonData.Status.Disable
                        && ss.HasCombination == CommonData.Status.Disable
                        )
                        .Select(ss => new MS_VocabulariesModels
                        {
                            ID = ss.ID,
                            Code = ss.Code,
                            Romaji = ss.Romaji,
                            Katakana = ss.Katakana,
                            Hiragana = ss.Hiragana,
                            //LessonCode = ss.LessonCode,
                            Type = ss.Type,
                            VMeaning = ss.VMeaning,
                            Description = ss.Description,
                        })
                        .ToList();
                }
                
                if ((model.HasDiacritic && model.HasLongSound&& model.HasTsu && model.HasCombination))
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasLongSound == CommonData.Status.Enable
                        || ss.HasTsu == CommonData.Status.Enable
                        || ss.HasCombination == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic && model.HasLongSound && model.HasTsu)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasLongSound == CommonData.Status.Enable
                        || ss.HasTsu == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic && model.HasLongSound && model.HasCombination)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasLongSound == CommonData.Status.Enable
                        || ss.HasCombination == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic && model.HasCombination && model.HasTsu)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasCombination == CommonData.Status.Enable
                        || ss.HasTsu == CommonData.Status.Enable
                        );
                }
                else if (model.HasCombination && model.HasLongSound && model.HasTsu)
                {
                    query = query.Where(ss => ss.HasCombination == CommonData.Status.Enable
                        || ss.HasLongSound == CommonData.Status.Enable
                        || ss.HasTsu == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic && model.HasLongSound)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasLongSound == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic && model.HasTsu)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasTsu == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic && model.HasCombination)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable
                        || ss.HasCombination == CommonData.Status.Enable
                        );
                }
                else if (model.HasLongSound && model.HasTsu)
                {
                    query = query.Where(ss => ss.HasLongSound == CommonData.Status.Enable
                        || ss.HasTsu == CommonData.Status.Enable
                        );
                }
                else if (model.HasLongSound && model.HasCombination)
                {
                    query = query.Where(ss => ss.HasLongSound == CommonData.Status.Enable
                        || ss.HasCombination == CommonData.Status.Enable
                        );
                }
                else if (model.HasTsu && model.HasCombination)
                {
                    query = query.Where(ss => ss.HasTsu == CommonData.Status.Enable
                        || ss.HasCombination == CommonData.Status.Enable
                        );
                }
                else if (model.HasDiacritic)
                {
                    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Enable);
                }
                else if (model.HasLongSound)
                {
                    query = query.Where(ss => ss.HasLongSound == CommonData.Status.Enable);
                }
                else if (model.HasTsu)
                {
                    query = query.Where(ss => ss.HasTsu == CommonData.Status.Enable);
                }
                else if (model.HasCombination)
                {
                    query = query.Where(ss => ss.HasCombination == CommonData.Status.Enable);
                }

                //if (!model.HasNormal)
                //{
                //    query = query.Where(ss => ss.HasCombination == CommonData.Status.Enable 
                //                        || ss.HasDiacritic == CommonData.Status.Enable 
                //                        || ss.HasTsu == CommonData.Status.Enable
                //                        || ss.HasLongSound == CommonData.Status.Enable);
                //}

                //if (!model.HasDiacritic)
                //{
                //    query = query.Where(ss => ss.HasDiacritic == CommonData.Status.Disable);
                //}
                //if (!model.HasLongSound)
                //{
                //    query = query.Where(ss => ss.HasLongSound == CommonData.Status.Disable);
                //}
                //if (!model.HasCombination)
                //{
                //    query = query.Where(ss => ss.HasCombination == CommonData.Status.Disable);
                //}
                //if (!model.HasTsu)
                //{
                //    query = query.Where(ss => ss.HasTsu == CommonData.Status.Disable);
                //}

                results = query.Select(ss => new MS_VocabulariesModels
                {
                    ID = ss.ID,
                    Code = ss.Code,
                    Romaji = ss.Romaji,
                    Katakana = ss.Katakana,
                    Hiragana = ss.Hiragana,
                    //LessonCode = ss.LessonCode,
                    Type = ss.Type,
                    VMeaning = ss.VMeaning,
                    Description = ss.Description,
                })
                .ToList();

                results = results.Union(normals).ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectDataForLesson(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var query = this.ms_vocabularies.AsQueryable();

                if (!CommonMethod.IsNullOrEmpty(model.VocabularyCode))
                {
                    query = query.Where(ss => ss.Code == model.VocabularyCode);
                }
                if (!CommonMethod.IsNullOrEmpty(model.Romaji))
                {
                    query = query.Where(ss => ss.Romaji == model.Romaji);
                }
                //if (!CommonMethod.IsNullOrEmpty(model.LessonCode))
                //{
                //    query = query.Where(ss => ss.LessonCode == model.LessonCode);
                //}
                if (!CommonMethod.IsNullOrEmpty(model.Type))
                {
                    query = query.Where(ss => ss.Type == model.Type);
                }

                results = query.Select(ss => new MS_UserVocabulariesModels
                {
                    ID = ss.ID,
                    VocabularyCode = ss.Code,
                    //LineNumber = ss.LineNumber,
                    Romaji = ss.Romaji,
                    Katakana = ss.Katakana,
                    Hiragana = ss.Hiragana,
                    Kanji = ss.Kanji,
                    //LessonCode = ss.LessonCode,
                    Type = ss.Type,
                    VMeaning = ss.VMeaning,
                    Description = ss.Description,
                    UrlAudio = ss.UrlAudio,
                    UrlImage = ss.UrlImage,
                })
                .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectVocaForLesson(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var voca = this.ms_vocabularies.AsQueryable();
                var query = this.ms_uservocabularies.AsQueryable();

                //if (!CommonMethod.IsNullOrEmpty(model.LessonCode))
                //{
                //    voca = voca.Where(ss => ss.LessonCode == model.LessonCode);
                //}

                if (!CommonMethod.IsNullOrEmpty(model.UserID))
                {
                    query = query.Where(ss => ss.UserID == model.UserID);
                }

                if (!CommonMethod.IsNullOrEmpty(model.HasLearnt) && model.HasLearnt == CommonData.Status.Enable)
                {
                    query = query.Where(ss => ss.HasLearnt == model.HasLearnt);
                }

                //results = (from us in query
                //           join ss in voca on us.VocabularyCode equals ss.Code
                //           select new MS_UserVocabulariesModels
                //           {
                //               ID = us.ID,
                //               //UserID = us.UserID,
                //               Level = us.Level,
                //               //Update_Date = us.Update_Date,
                //               VocabularyCode = ss.Code,
                //               LineNumber = ss.LineNumber,
                //               Romaji = ss.Romaji,
                //               Katakana = ss.Katakana,
                //               Hiragana = ss.Hiragana,
                //               Kanji = ss.Kanji,
                //               //LessonCode = ss.LessonCode,
                //               //Type = ss.Type,
                //               VMeaning = ss.VMeaning,
                //               Description = ss.Description,
                //               UrlAudio = ss.UrlAudio,
                //               UrlImage = ss.UrlImage,
                //               ExRomaji1 = ss.ExRomaji1,
                //               ExRomaji2 = ss.ExRomaji2,
                //               ExRomaji3 = ss.ExRomaji3,
                //               ExHiragana1 = ss.ExHiragana1,
                //               ExHiragana2 = ss.ExHiragana2,
                //               ExHiragana3 = ss.ExHiragana3,
                //               ExKatakana1 = ss.ExKatakana1,
                //               ExKatakana2 = ss.ExKatakana2,
                //               ExKatakana3 = ss.ExKatakana3,
                //               ExVMeaning1 = ss.ExVMeaning1,
                //               ExVMeaning2 = ss.ExVMeaning2,
                //               ExVMeaning3 = ss.ExVMeaning3,
                //           })
                //                .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }


        internal int SelectWeakVocaSummary(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                //var voca = from vo in this.ms_vocabularies
                //           join le in this.ms_lessons on vo.LessonCode equals le.Code
                //           where le.Type == "2"
                //           select new { Voca = vo, Lesson = le };

                
                var currentDate = DateTime.Now.Date;
                var query = this.ms_uservocabularies.Where(ss => ss.Level < 10
                                                            && (ss.StartDate <= currentDate && currentDate <= ss.EndDate)).AsQueryable();

                if (!CommonMethod.IsNullOrEmpty(model.UserID))
                {
                    query = query.Where(ss => ss.UserID == model.UserID);
                }

                var userVoca = from vs in this.ms_vocasets
                                join vc in this.ms_vocacategories on vs.ID equals vc.VocaSetID
                               join vd in this.ms_vocabularydetails on vc.ID equals vd.CategoryID
                               join qu in query on vd.ID equals qu.VocaDetailID
                               select new { VocaSetName = vs.Name1, VocaCateID = vc.ID, VocaCateCode = vc.Code, VocaCateName1 = vc.Name1, VocaCateUrlDisplay = vc.UrlDisplay };


                //var userVoca = (from us in query
                //                join vo in voca on us.VocabularyCode equals vo.Voca.Code
                //                select new MS_UserVocabulariesModels
                //                {
                //                    LessonCode = vo.Voca.LessonCode,
                //                    LessonName1 = vo.Lesson.Name1,
                //                })
                //          .ToList();

                //Group by Lesson
                results = userVoca.GroupBy(ss => new { ss.VocaSetName, ss.VocaCateID, ss.VocaCateCode, ss.VocaCateName1, ss.VocaCateUrlDisplay })
                            .Select(ss => new MS_UserVocabulariesModels
                            {
                                VocaSetName = ss.Key.VocaSetName,
                                CategoryID = ss.Key.VocaCateID,
                                CategoryCode = ss.Key.VocaCateCode,
                                CategoryName = ss.Key.VocaCateName1,
                                CategoryUrlDisplay = ss.Key.VocaCateUrlDisplay,
                                NumOfWeakVoca = ss.Count(),
                            })
                            .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectWeakVocaForLesson(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var voca = this.ms_vocabularies.AsQueryable();
                var query = this.ms_uservocabularies.Where(ss => ss.Level < 10).AsQueryable();

                //if (!CommonMethod.IsNullOrEmpty(model.LessonCode))
                //{
                //    voca = voca.Where(ss => ss.LessonCode == model.LessonCode);
                //}

                if (!CommonMethod.IsNullOrEmpty(model.UserID))
                {
                    query = query.Where(ss => ss.UserID == model.UserID);
                }

                //results = (from us in query
                //           join ss in voca on us.VocabularyCode equals ss.Code
                //           select new MS_UserVocabulariesModels
                //           {
                //               ID = us.ID,
                //               //UserID = us.UserID,
                //               Level = us.Level,
                //               //Update_Date = us.Update_Date,
                //               VocabularyCode = ss.Code,
                //               LineNumber = ss.LineNumber,
                //               Romaji = ss.Romaji,
                //               Katakana = ss.Katakana,
                //               Hiragana = ss.Hiragana,
                //               Kanji = ss.Kanji,
                //               //LessonCode = ss.LessonCode,
                //               Type = ss.Type,
                //               VMeaning = ss.VMeaning,
                //               Description = ss.Description,
                //               UrlAudio = ss.UrlAudio,
                //               UrlImage = ss.UrlImage,
                //               ExRomaji1 = ss.ExRomaji1,
                //               ExRomaji2 = ss.ExRomaji2,
                //               ExRomaji3 = ss.ExRomaji3,
                //               ExHiragana1 = ss.ExHiragana1,
                //               ExHiragana2 = ss.ExHiragana2,
                //               ExHiragana3 = ss.ExHiragana3,
                //               ExKatakana1 = ss.ExKatakana1,
                //               ExKatakana2 = ss.ExKatakana2,
                //               ExKatakana3 = ss.ExKatakana3,
                //               ExVMeaning1 = ss.ExVMeaning1,
                //               ExVMeaning2 = ss.ExVMeaning2,
                //               ExVMeaning3 = ss.ExVMeaning3,
                //           })
                //                .ToList();
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
                        voca.UpdatedDate = DateTime.Now;
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

        internal int UpdateHasLearntVocas(MS_UserVocabulariesModels voca)
        {
            int returnCode = 0;

            try
            {
                var vo = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == voca.ID);
                if (vo != null)
                {
                    vo.HasLearnt = voca.HasLearnt;
                }

                returnCode = this.Saves();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }
    }
}