using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nihongo.Models;
using Ivs.Core.Common;

namespace Nihongo.Dal.Dao
{
    public class MS_UserVocabularyDao : BaseDao
    {

        internal int SelectSessionUserVocaData(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                if (model.IsKanji == CommonData.Status.Enable)
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_kanjis on de.KanjiID equals ss.ID

                               where vs.ID == model.VocaSetID
                                    && us.UserID == model.UserID
                                    && us.IsIgnore == CommonData.Status.Disable
                                    //&& us.HasLearnt == CommonData.Status.Disable

                               orderby us.HasLearnt ascending, us.Level ascending, vc.LineNumber ascending, de.LineNumber ascending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   HasMarked = us.HasMarked,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,
                                   
                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
                                   RequiredTimePerVoca = vc.RequiredTimePerVoca,
                                   IsKanji = vc.IsKanji,

                                   //kanji
                                   VocabularyCode = ss.Code,
                                   Kanji = ss.Kanji,
                                   Pinyin = ss.Pinyin,
                                   VMeaning = ss.VMeaning,
                                   Description = ss.Description,
                                   UrlAudio = ss.UrlAudio,
                                   UrlImage = ss.UrlImage,
                                   OnReading = ss.OnReading,
                                   KunReading = ss.KunReading,
                                   DisplayType = ss.DisplayType,
                                   Remembering = ss.Remembering,

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,

                                   KanjiExamples = (from ex in ms_kanjiexamples
                                                   where ex.KanjiID == ss.ID
                                                   select new MS_KanjiExampleModel
                                                   {
                                                       KanjiID = ex.KanjiID,
                                                       Kanji = ex.Kanji,
                                                       Pinyin = ex.Pinyin,
                                                       Hiragana = ex.Hiragana,
                                                       VMeaning = ex.VMeaning,
                                                   }).AsEnumerable(),
                               })
                               .Take(5)
                               .ToList();
                }
                else
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_vocabularies on de.VocabularyID equals ss.ID

                               where vs.ID == model.VocaSetID 
                                    //&& ss.Type == model.Type
                                    && us.UserID == model.UserID
                                    && us.IsIgnore == CommonData.Status.Disable
                                    //&& us.HasLearnt == CommonData.Status.Disable

                               orderby us.HasLearnt ascending, us.Level ascending, vc.LineNumber ascending, de.LineNumber ascending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   HasMarked = us.HasMarked,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,


                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
                                   RequiredTimePerVoca = vc.RequiredTimePerVoca,
                                   IsKanji = vc.IsKanji,

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

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,
                               })
                               .Take(5)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectPracticeSessionUserVocaData(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                if (model.IsKanji == CommonData.Status.Enable)
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_kanjis on de.KanjiID equals ss.ID

                               where vs.ID == model.VocaSetID 
                                    && us.UserID == model.UserID
                                    && us.IsIgnore == CommonData.Status.Disable
                                    && us.HasLearnt == CommonData.Status.Enable

                               orderby us.Level ascending, us.UpdatedDate ascending, us.NumOfWrong descending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   HasMarked = us.HasMarked,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,

                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
                                   RequiredTimePerVoca = vc.RequiredTimePerVoca,

                                   //voca
                                   VocabularyCode = ss.Code,
                                   Kanji = ss.Kanji,
                                   Pinyin = ss.Pinyin,
                                   VMeaning = ss.VMeaning,
                                   Description = ss.Description,
                                   UrlAudio = ss.UrlAudio,
                                   UrlImage = ss.UrlImage,
                                   OnReading = ss.OnReading,
                                   KunReading = ss.KunReading,
                                   DisplayType = ss.DisplayType,
                                   Remembering = ss.Remembering,

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,

                                   KanjiExamples = (from ex in ms_kanjiexamples
                                                    where ex.KanjiID == ss.ID
                                                    select new MS_KanjiExampleModel
                                                    {
                                                        KanjiID = ex.KanjiID,
                                                        Kanji = ex.Kanji,
                                                        Pinyin = ex.Pinyin,
                                                        Hiragana = ex.Hiragana,
                                                        VMeaning = ex.VMeaning,
                                                    }).AsEnumerable(),
                               })
                               .Take(10)
                               .ToList();
                }
                else
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_vocabularies on de.VocabularyID equals ss.ID

                               where vs.ID == model.VocaSetID 
                                    //&& ss.Type == model.Type
                                    && us.UserID == model.UserID
                                    && us.IsIgnore == CommonData.Status.Disable
                                    && us.HasLearnt == CommonData.Status.Enable

                               orderby us.Level ascending, us.UpdatedDate ascending, us.NumOfWrong descending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   HasMarked = us.HasMarked,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,

                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
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

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,
                               })
                               .Take(10)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectNotebookSessionUserVocaData(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                if (model.IsKanji == CommonData.Status.Enable)
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_kanjis on de.KanjiID equals ss.ID

                               where vs.ID == model.VocaSetID
                                    && us.UserID == model.UserID
                                    && us.IsIgnore == CommonData.Status.Disable
                                    && us.HasMarked == CommonData.Status.Enable

                               orderby us.Level ascending, us.UpdatedDate ascending, us.NumOfWrong descending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   HasMarked = us.HasMarked,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,

                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
                                   RequiredTimePerVoca = vc.RequiredTimePerVoca,

                                   //voca
                                   VocabularyCode = ss.Code,
                                   Kanji = ss.Kanji,
                                   Pinyin = ss.Pinyin,
                                   VMeaning = ss.VMeaning,
                                   Description = ss.Description,
                                   UrlAudio = ss.UrlAudio,
                                   UrlImage = ss.UrlImage,
                                   OnReading = ss.OnReading,
                                   KunReading = ss.KunReading,
                                   DisplayType = ss.DisplayType,
                                   Remembering = ss.Remembering,

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,

                                   KanjiExamples = (from ex in ms_kanjiexamples
                                                    where ex.KanjiID == ss.ID
                                                    select new MS_KanjiExampleModel
                                                    {
                                                        KanjiID = ex.KanjiID,
                                                        Kanji = ex.Kanji,
                                                        Pinyin = ex.Pinyin,
                                                        Hiragana = ex.Hiragana,
                                                        VMeaning = ex.VMeaning,
                                                    }).AsEnumerable(),
                               })
                               .Take(10)
                               .ToList();
                }
                else
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_vocabularies on de.VocabularyID equals ss.ID

                               where vs.ID == model.VocaSetID 
                                    //&& ss.Type == model.Type
                                    && us.UserID == model.UserID
                                    && us.IsIgnore == CommonData.Status.Disable
                                    && us.HasMarked == CommonData.Status.Enable

                               orderby us.Level ascending, us.UpdatedDate ascending, us.NumOfWrong descending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   HasMarked = us.HasMarked,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,

                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
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

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,
                               })
                               .Take(10)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }


        internal int SelectPracticeSessionCateVocas(MS_UserVocabulariesModels model, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                if (model.IsKanji == CommonData.Status.Enable)
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_kanjis on de.KanjiID equals ss.ID

                               where vc.ID == model.CategoryID
                                    && us.UserID == model.UserID

                               orderby us.Level ascending, us.UpdatedDate ascending, us.NumOfWrong descending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   HasMarked = us.HasMarked,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,

                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
                                   RequiredTimePerVoca = vc.RequiredTimePerVoca,

                                   //voca
                                   VocabularyCode = ss.Code,
                                   Kanji = ss.Kanji,
                                   Pinyin = ss.Pinyin,
                                   VMeaning = ss.VMeaning,
                                   Description = ss.Description,
                                   UrlAudio = ss.UrlAudio,
                                   UrlImage = ss.UrlImage,
                                   OnReading = ss.OnReading,
                                   KunReading = ss.KunReading,
                                   DisplayType = ss.DisplayType,
                                   Remembering = ss.Remembering,

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,

                                   KanjiExamples = (from ex in ms_kanjiexamples
                                                    where ex.KanjiID == ss.ID
                                                    select new MS_KanjiExampleModel
                                                    {
                                                        KanjiID = ex.KanjiID,
                                                        Kanji = ex.Kanji,
                                                        Pinyin = ex.Pinyin,
                                                        Hiragana = ex.Hiragana,
                                                        VMeaning = ex.VMeaning,
                                                    }).AsEnumerable(),
                               })
                               .Take(10)
                               .ToList();
                }
                else
                {
                    results = (from us in ms_uservocabularies
                               join de in ms_vocabularydetails on us.VocaDetailID equals de.ID
                               join vc in ms_vocacategories on de.CategoryID equals vc.ID
                               join vs in ms_vocasets on vc.VocaSetID equals vs.ID
                               join ss in ms_vocabularies on de.VocabularyID equals ss.ID

                               where vc.ID == model.CategoryID
                                   //&& ss.Type == model.Type
                                    && us.UserID == model.UserID

                               orderby us.Level ascending, us.UpdatedDate ascending, us.NumOfWrong descending

                               select new MS_UserVocabulariesModels
                               {
                                   //user voca
                                   ID = us.ID,
                                   UserID = us.UserID,
                                   Level = us.Level,
                                   HasLearnt = us.HasLearnt,
                                   IsIgnore = us.IsIgnore,
                                   //Update_Date = us.Update_Date,
                                   NumOfWrong = us.NumOfWrong ?? 0,
                                   HasMarked = us.HasMarked,
                                   UserDefine = us.UserDefine,

                                   //voca detail
                                   LineNumber = de.LineNumber,

                                   //voca set
                                   VocaSetID = vs.ID,
                                   VocaSetName = vs.Name1,
                                   VocaSetUrlDisplay = vs.UrlDisplay,
                                   VocaSetFee = vs.Fee ?? 0,

                                   //category
                                   CategoryID = vc.ID,
                                   CategoryCode = vc.Code,
                                   CategoryName = vc.Name1,
                                   CategoryDescription = vc.Description,
                                   CategoryUrlDisplay = vc.UrlDisplay,
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

                                   Point = 0,
                                   IsDone = CommonData.Status.Disable,
                               })
                               .Take(10)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        public int SelectUserVocaData(int vocaCateId, int userID, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                //var vocaSet = this.ms_vocasets.AsQueryable();
                //var vocaCate = this.ms_vocacategories.AsQueryable();//.Where(ss => ss.ID == vocaCateId);
                //var voca = this.ms_vocabularies.AsQueryable();
                //var vocaDetail = this.ms_vocabularydetails.AsQueryable();
                //var userVoca = this.ms_uservocabularies.AsQueryable();//.Where(ss => ss.UserID == userID).AsQueryable();

                //var xx = this.ms_vocabularydetails.Where(ss => ss.ms_vocabularies.ID == vocaCateId).ToList();
                var cate = this.ms_vocacategories.FirstOrDefault(ss => ss.ID == vocaCateId);
                if (cate != null)
                {
                    results = cate.ms_vocabularydetails
                        .OrderBy(s => s.LineNumber)
                        .Select(ss => new MS_UserVocabulariesModels
                        {
                            ID = ss.ID,
                            LineNumber = ss.LineNumber,

                            //voca set
                            VocaSetID = ss.ms_vocacategories.VocaSetID ?? 0,
                            //IsKanji = vs.IsKanji,

                            //category
                            CategoryID = ss.ms_vocacategories.ID,
                            CategoryCode = ss.ms_vocacategories.Code,
                            CategoryName = ss.ms_vocacategories.Name1,
                            CategoryDescription = ss.ms_vocacategories.Description,
                            IsKanji = ss.ms_vocacategories.IsKanji,

                            //voca
                            VocabularyID = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.ID : ss.ms_vocabularies.ID,
                            VocabularyCode = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.Code : ss.ms_vocabularies.Code,
                            Romaji = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.Romaji,
                            Romaji_Katakana = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.Romaji_Katakana,
                            Katakana = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.Katakana,
                            Hiragana = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.Hiragana,
                            UrlAudio = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.UrlAudio,
                            UrlAudio_Katakana = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.UrlAudio_Katakana,
                            DisplayType = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.DisplayType,
                            UrlImage = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.UrlImage : ss.ms_vocabularies.UrlImage,
                            Type = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? string.Empty : ss.ms_vocabularies.Type,

                            Kanji = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.Kanji : ss.ms_vocabularies.Kanji,
                            Pinyin = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.Pinyin : string.Empty,
                            VMeaning = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.VMeaning : ss.ms_vocabularies.VMeaning,
                            Description = ss.ms_vocacategories.IsKanji == CommonData.Status.Enable ? ss.ms_kanjis.Description : ss.ms_vocabularies.Description,
                            
                        })
                        .ToList();
                }
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
                var userVoca = this.ms_uservocabularies.Where(ss => ss.UserID == model.UserID).AsQueryable();
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
                           join vc in vocaCate on de.CategoryID equals vc.ID
                           join vs in vocaSet on vc.VocaSetID equals vs.ID
                           join ss in voca on de.VocabularyID equals ss.ID
                           orderby de.LineNumber
                           select new MS_UserVocabulariesModels
                           {
                               //user voca
                               ID = us.ID,
                               UserID = us.UserID,
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
                               //Pinyin = ss.Pinyin,
                               //OnReading = ss.OnReading,
                               //OnRomaji = ss.OnRomaji,
                               //OnUrlAudio = ss.OnUrlAudio,
                               //OnReading2 = ss.OnReading2,
                               //OnRomaji2 = ss.OnRomaji2,
                               //OnUrlAudio2 = ss.OnUrlAudio2,
                               //Remembering = ss.Remembering,

                               //ExKanji1 = ss.ExKanji1,
                               //ExKanji2 = ss.ExKanji2,
                               //ExKanji3 = ss.ExKanji3,
                               //ExKanji4 = ss.ExKanji4,
                               //ExReading1 = ss.ExReading1,
                               //ExReading2 = ss.ExReading2,
                               //ExReading3 = ss.ExReading3,
                               //ExReading4 = ss.ExReading4,
                               //ExVMeaning1 = ss.ExVMeaning1,
                               //ExVMeaning2 = ss.ExVMeaning2,
                               //ExVMeaning3 = ss.ExVMeaning3,
                               //ExVMeaning4 = ss.ExVMeaning4,

                               //KunReading = ss.KunReading,
                               //KunRomaji = ss.KunRomaji,
                               //KunUrlAudio = ss.KunUrlAudio,
                               //KunReading2 = ss.KunReading2,
                               //KunRomaji2 = ss.KunRomaji2,
                               //KunUrlAudio2 = ss.KunUrlAudio2,

                           })
                                .ToList();
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectNotebookVocas(int userID, out List<MS_UserVocabulariesModels> results)
        {
            int returnCode = 0;
            results = new List<MS_UserVocabulariesModels>();

            try
            {
                var vocaSet = this.ms_vocasets.AsQueryable();
                var vocaCate = this.ms_vocacategories.AsQueryable();
                var voca = this.ms_vocabularies.AsQueryable();
                var vocaDetail = this.ms_vocabularydetails.AsQueryable();
                var userVoca = this.ms_uservocabularies.Where(ss => ss.UserID == userID && ss.HasMarked == CommonData.Status.Enable).AsQueryable();

                results = (from us in userVoca
                           join de in vocaDetail on us.VocaDetailID equals de.ID
                           join vc in vocaCate on de.CategoryID equals vc.ID
                           join vs in vocaSet on vc.VocaSetID equals vs.ID
                           join ss in voca on de.VocabularyID equals ss.ID
                           orderby de.LineNumber
                           select new MS_UserVocabulariesModels
                           {
                               //user voca
                               ID = us.ID,
                               UserID = us.UserID,
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
                               //Pinyin = ss.Pinyin,
                               //OnReading = ss.OnReading,
                               //OnRomaji = ss.OnRomaji,
                               //OnUrlAudio = ss.OnUrlAudio,
                               //OnReading2 = ss.OnReading2,
                               //OnRomaji2 = ss.OnRomaji2,
                               //OnUrlAudio2 = ss.OnUrlAudio2,

                               //KunReading = ss.KunReading,
                               //KunRomaji = ss.KunRomaji,
                               //KunUrlAudio = ss.KunUrlAudio,
                               //KunReading2 = ss.KunReading2,
                               //KunRomaji2 = ss.KunRomaji2,
                               //KunUrlAudio2 = ss.KunUrlAudio2,

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
                            voca.UpdatedDate = DateTime.Now;
                        }
                    }

                    //create test result
                    var firstVoca = vocas.FirstOrDefault();
                    var vocaCate = this.ms_vocacategories.FirstOrDefault(ss => ss.ID == firstVoca.CategoryID);
                    var vocaSet = this.ms_vocasets.FirstOrDefault(ss => ss.ID == vocaCate.VocaSetID);
                    var numOfCorrectVocas = vocas.Count(ss => ss.IsCorrect == CommonData.Status.Enable);

                    #region Diem tich luy
                    if (CommonMethod.ParseDecimal(vocaSet.Fee) > 0)
                    {
                        var isExsitResult = ms_testresults.Any(ss => ss.CategoryID == vocaCate.ID && ss.UserID == firstVoca.UserID);
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
                    var user = this.ms_users.FirstOrDefault(ss => ss.ID == firstVoca.UserID);
                    if (user != null)
                    {
                        user.AccumulatedPoint = CommonMethod.ParseInt(user.AccumulatedPoint) + accumulatedPoint;
                    }

                    #endregion

                    Nihongo.Dal.Mapping.ms_testresults test = new Mapping.ms_testresults();
                    test.Code = firstVoca.UserID + "_" + vocaCate.Code + "_" + (DateTime.Now.ToString(CommonData.DateFormat.YyyyMMddHHmmss));
                    test.CategoryID = vocaCate.ID;
                    test.UserID = firstVoca.UserID;
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
                    vo.UpdatedDate = DateTime.Now;

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
                if (!CommonMethod.IsNullOrEmpty(voca.UserID))
                {

                    var us = this.ms_users.FirstOrDefault(ss => ss.ID == voca.UserID);
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



        internal int UpdateUserVocas(int userID, List<MS_UserVocabulariesModels> vocas)
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
                        voca.UpdatedDate = DateTime.Now;

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
                    if (!CommonMethod.IsNullOrEmpty(userID))
                    {

                        var us = this.ms_users.FirstOrDefault(ss => ss.ID == userID);
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
                this.BeginTransaction();

                var vocaDb = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == voca.ID);
                if (vocaDb != null)
                {
                    vocaDb.HasMarked = voca.HasMarked;

                    returnCode = this.Saves();
                }

                //if (returnCode == CommonData.DbReturnCode.Succeed)
                //{
                //    var userCate = this.ms_usercategories.FirstOrDefault(ss => ss.UserID == voca.UserID && ss.CategoryID == voca.CategoryID);
                //    if (userCate != null)
                //    {
                //        var cate = this.ms_vocacategories.FirstOrDefault(ss => ss.ID == voca.CategoryID);
                //        var numOfHasMarked = this.ms_uservocabularies.Count(s => s.UserID == voca.UserID && s.ms_vocabularydetails.CategoryID == voca.CategoryID && s.HasMarked == CommonData.Status.Enable);
                //        if (cate != null && cate.NumOfVocas == numOfHasMarked)
                //        {
                //            userCate.HasMarked = CommonData.Status.Enable;
                //        }
                //        else
                //        {
                //            userCate.HasMarked = CommonData.Status.Disable;
                //        }
                //    }

                //    returnCode = this.Saves();
                //}

                //if (returnCode == CommonData.DbReturnCode.Succeed)
                //{
                //    var userSet = this.ms_uservocasets.FirstOrDefault(ss => ss.UserID == voca.UserID && ss.VocaSetID == voca.VocaSetID);
                //    if (userSet != null)
                //    {
                //        var set = this.ms_vocasets.FirstOrDefault(ss => ss.ID == voca.VocaSetID);
                //        var numOfHasMarked = this.ms_uservocabularies.Count(s => s.UserID == voca.UserID && s.ms_vocabularydetails.ms_vocacategories.VocaSetID == voca.VocaSetID && s.HasMarked == CommonData.Status.Enable);
                //        if (set != null && set.NumOfVocas == numOfHasMarked)
                //        {
                //            userSet.HasMarked = CommonData.Status.Enable;
                //        }
                //        else
                //        {
                //            userSet.HasMarked = CommonData.Status.Disable;
                //        }
                //    }

                //    returnCode = this.Saves();
                //}

                if (returnCode == CommonData.DbReturnCode.Succeed)
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

        internal int SelectUserHomePageData1(int userID, out MS_UsersModels userModel)
        {
            int returnCode = 0;
            userModel = null;
            try
            {
                userModel = ms_users.Where(ss => ss.ID == userID)
                    .Select(ss => new MS_UsersModels
                    {
                        ID = ss.ID,
                        DisplayName = ss.DisplayName,
                        LastVisitedDate = ss.LastVisitedDate,
                        Point = ss.Point ?? 0,
                        UrlImage = ss.UrlImage,
                        UserName = ss.UserName,
                        NumOfLearntVoca = ss.NumOfLearntVoca ?? 0,

                    })
                    .FirstOrDefault();
                if (userModel != null)
                {
                    //update level
                    var usVocas = this.ms_uservocabularies.Where(ss => ss.UserID == userID);
                    foreach (var usVoca in usVocas)
                    {
                        if (usVoca.UpdatedDate.HasValue)
                        {
                            if (usVoca.Level > 0)
                            {
                                int minutes = (DateTime.Now - usVoca.UpdatedDate.Value).Minutes;
                                int hours = minutes / 60;
                                usVoca.Level -= (hours / 12);
                                if (usVoca.Level < 0)
                                {
                                    usVoca.Level = 0;
                                }
                            }
                        }
                    }

                    returnCode = this.Saves();

                    var userVocaSets = (from us in this.ms_uservocabularies.Where(ss => ss.UserID == userID)
                                        group us by us.ms_vocabularydetails.ms_vocacategories.ms_vocasets.ID into userVoca

                                        join uvs in ms_uservocasets.Where(ss => ss.UserID == userID) on userVoca.Key equals uvs.VocaSetID into userSet
                                        from uvs in userSet.DefaultIfEmpty()

                                        join vs in ms_vocasets on userVoca.Key equals vs.ID

                                        select new MS_UserVocaSet
                                        {
                                            VocaSetID = vs.ID,
                                            VocaSetName = vs.Name1,
                                            VocaSetDescription = vs.Description,
                                            UpdateDate = uvs == null ? null : uvs.UpdatedDate,

                                            VocaSetUrlDisplay = vs.UrlDisplay,
                                            VocaSetUrlImage = vs.UrlImage,
                                            NumOfVoca = vs.NumOfVocas ?? 0,
                                            NumOfHasMarked = userVoca.Count(s => s.HasMarked == CommonData.Status.Enable),
                                            NumOfHasLearnt = userVoca.Count(s => s.HasLearnt == CommonData.Status.Enable),
                                            NumOfWeak = userVoca.Count(s => s.IsIgnore == CommonData.Status.Disable 
                                                //&& s.ms_vocabularydetails.ms_vocabularies.Type == CommonData.VocaType.Word
                                                                    && s.HasLearnt == CommonData.Status.Enable && s.Level < 9),
                                            HasRegis = true,
                                        }).ToList();

                    if (userVocaSets.Count == 0)
                    {
                        //select all voca sets
                        userVocaSets = (from vs in this.ms_vocasets
                                        select new MS_UserVocaSet
                                        {
                                            VocaSetID = vs.ID,
                                            VocaSetName = vs.Name1,
                                            VocaSetDescription = vs.Description,
                                            VocaSetUrlDisplay = vs.UrlDisplay,
                                            VocaSetUrlImage = vs.UrlImage,
                                            NumOfVoca = vs.NumOfVocas ?? 0,
                                            NumOfRegistedPerson = vs.NumOfRegistedPerson ?? 0,
                                            NumOfCategories = vs.NumOfCategories ?? 0,
                                            NumOfFinishedPerson = vs.NumOfFinishedPerson ?? 0,
                                            HasRegis = false,
                                        }).ToList();
                    }

                    userModel.UserVocaSets = userVocaSets;
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }


        internal int UpdateSessionResult(int userID, List<MS_UserVocabulariesModels> vocas, out int numOfVocaOfSet, out int numOfHasLearntOfSet, out int numOfHasMarkedOfSet, out int numOfWeakOfSet, out int hasLearntCateID, out string hasLearntCateName)
        {
            int returnCode = 0;
            numOfVocaOfSet = 0;
            numOfHasMarkedOfSet = 0;
            numOfHasLearntOfSet = 0;
            numOfWeakOfSet = 0;
            hasLearntCateName = string.Empty;
            hasLearntCateID = 0;
            try
            {
                if (vocas.Count > 0)
                {
                    this.BeginTransaction();

                    #region Update data

                    int point = 0;
                    //update user vocas
                    foreach (var vo in vocas)
                    {
                        var voca = this.ms_uservocabularies.FirstOrDefault(ss => ss.ID == vo.ID);
                        if (voca != null)
                        {
                            voca.HasLearnt = CommonData.Status.Enable;
                            voca.HasMarked = vo.HasMarked;
                            voca.IsIgnore = vo.IsIgnore;
                            voca.UpdatedDate = DateTime.Now;
                            voca.Level = vo.Level;
                            voca.NumOfWrong = vo.NumOfWrong < 0 ? 0 : vo.NumOfWrong;
                            voca.UserDefine = vo.UserDefine;

                            point += vo.Point;
                        }
                    }

                    returnCode = this.Saves();

                    //update user info
                    if (returnCode == CommonData.DbReturnCode.Succeed)
                    {
                        var user = ms_users.FirstOrDefault(ss => ss.ID == userID);
                        if (user != null)
                        {
                            user.NumOfLearntVoca = this.ms_uservocabularies.Count(ss => ss.UserID == userID && ss.HasLearnt == CommonData.Status.Enable);
                            user.LastVisitedDate = DateTime.Now;
                            user.Point += point;
                        }

                        returnCode = this.Saves();
                    }

                    //update user category
                    if (returnCode == CommonData.DbReturnCode.Succeed)
                    {
                        if (vocas.Count > 0)
                        {
                            var userCates = vocas.GroupBy(ss => new { ss.UserID, ss.CategoryID })
                                .Select(ss => new
                                {
                                    ss.Key.UserID,
                                    ss.Key.CategoryID,
                                });

                            foreach (var userCate in userCates)
                            {
                                Mapping.ms_usercategories userCateDb = this.ms_usercategories.FirstOrDefault(ss => ss.UserID == userCate.UserID && ss.CategoryID == userCate.CategoryID);
                                var userVocas = (from us in this.ms_uservocabularies
                                                      join vd in this.ms_vocabularydetails on us.VocaDetailID equals vd.ID
                                                      join vc in this.ms_vocacategories on vd.CategoryID equals vc.ID
                                                      where us.UserID == userCate.UserID
                                                             && vc.ID == userCate.CategoryID
                                                      select us);
                                Mapping.ms_vocacategories cate = this.ms_vocacategories.FirstOrDefault(ss => ss.ID == userCate.CategoryID);

                                int numOfHasLearnt = userVocas.Count(ss => ss.HasLearnt == CommonData.Status.Enable);
                                int numOfIgnore = userVocas.Count(ss => ss.IsIgnore == CommonData.Status.Enable);
                                int numOfHasMarked = userVocas.Count(ss => ss.HasMarked == CommonData.Status.Enable);

                                if (userCateDb != null)
                                {
                                    if (hasLearntCateID == 0)
                                    {
                                        if (userCateDb.HasLearnt == CommonData.Status.Disable)
                                        {
                                            hasLearntCateID = (cate.NumOfVocas == numOfHasLearnt) ? cate.ID : 0;
                                            hasLearntCateName = (cate.NumOfVocas == numOfHasLearnt) ? cate.Name1 : string.Empty;
                                        }
                                    }

                                    userCateDb.IsIgnore = (cate.NumOfVocas == numOfIgnore) ? CommonData.Status.Enable : CommonData.Status.Disable;
                                    userCateDb.HasLearnt = (cate.NumOfVocas == numOfHasLearnt) ? CommonData.Status.Enable : CommonData.Status.Disable;
                                    userCateDb.HasMarked = (cate.NumOfVocas == numOfHasMarked) ? CommonData.Status.Enable : CommonData.Status.Disable;
                                    userCateDb.UpdatedDate = DateTime.Now;
                                }
                                else
                                {
                                    if (hasLearntCateID == 0)
                                    {
                                        hasLearntCateID = (cate.NumOfVocas == numOfHasLearnt) ? cate.ID : 0;
                                        hasLearntCateName = (cate.NumOfVocas == numOfHasLearnt) ? cate.Name1 : string.Empty;
                                    }

                                    userCateDb = new Mapping.ms_usercategories();
                                    userCateDb.CategoryID = userCate.CategoryID;
                                    userCateDb.UserID = userCate.UserID;
                                    userCateDb.IsIgnore = (cate.NumOfVocas == numOfIgnore) ? CommonData.Status.Enable : CommonData.Status.Disable;
                                    userCateDb.HasLearnt = (cate.NumOfVocas == numOfHasLearnt) ? CommonData.Status.Enable : CommonData.Status.Disable;
                                    userCateDb.HasMarked = (cate.NumOfVocas == numOfHasMarked) ? CommonData.Status.Enable : CommonData.Status.Disable;
                                    userCateDb.UpdatedDate = DateTime.Now;

                                    ms_usercategories.AddObject(userCateDb);
                                }
                            }

                            returnCode = this.Saves();
                        }
                    }


                    //update test result
                    //if (returnCode == CommonData.DbReturnCode.Succeed)
                    //{
                    //    if (vocas.Count > 0)
                    //    {
                    //        var userCates = vocas.GroupBy(ss => new { ss.UserID, ss.CategoryID })
                    //            .Select(ss => new
                    //            {
                    //                ss.Key.UserID, ss.Key.CategoryID,
                    //            });
                    //        foreach (var userCate in userCates)
                    //        {
                    //            Nihongo.Dal.Mapping.ms_testresults test = this.ms_testresults
                    //                .FirstOrDefault(ss => ss.UserID == userCate.UserID && ss.CategoryID == userCate.CategoryID);
                    //            if (test == null)
                    //            {
                    //                var numOfHasLearnt = (from us in this.ms_uservocabularies
                    //                                      join vd in this.ms_vocabularydetails on us.VocaDetailID equals vd.ID
                    //                                      join vc in this.ms_vocacategories on vd.CategoryID equals vc.ID
                    //                                      where us.UserID == userCate.UserID
                    //                                            && us.HasLearnt == CommonData.Status.Enable
                    //                                             && vc.ID == userCate.CategoryID
                    //                                      select us).Count();
                                                             
                    //                //create test result
                    //                //var firstVoca = vocas.FirstOrDefault();
                    //                //if (firstVoca != null)
                    //                //{
                    //                var vocaCate = this.ms_vocacategories.FirstOrDefault(ss => ss.ID == userCate.CategoryID);
                    //                //var vocaSet = this.ms_vocasets.FirstOrDefault(ss => ss.ID == vocaCate.VocaSetID);
                    //                //var numOfCorrectVocas = vocas.Count(ss => ss.IsCorrect == CommonData.Status.Enable);
                    //                if (numOfHasLearnt == vocaCate.NumOfVocas)
                    //                {
                    //                    test = new Mapping.ms_testresults();
                    //                    test.Code = userCate.UserID + "_" + vocaCate.Code + "_" + (DateTime.Now.ToString(CommonData.DateFormat.YyyyMMddHHmmss));
                    //                    test.CategoryID = userCate.CategoryID;
                    //                    test.UserID = userCate.UserID;
                    //                    test.CreateDate = DateTime.Now;
                    //                    test.NumOfVocas = vocaCate.NumOfVocas;
                    //                    test.NumOfCorrectVocas = vocaCate.NumOfVocas;
                    //                    test.IsPass = CommonData.Status.Enable;//(numOfCorrectVocas >= (vocas.Count * 8 / 10)) ? CommonData.Status.Enable : CommonData.Status.Disable;
                    //                    test.RequiredTimePerVoca = vocaCate.RequiredTimePerVoca;
                    //                    test.TotalRequiredTime = vocaCate.RequiredTimePerVoca * vocas.Count;
                    //                    //test.CompletedTime = firstVoca.CompletedTime;
                    //                    test.Status = CommonData.Status.Enable;
                    //                    //test.Description = (numOfCorrectVocas >= (vocas.Count * 8 / 10)) ? "Chúc mừng bạn đã vượt qua được bài kiểm tra" : "Bạn đã không vượt qua được bài kiểm tra. Hãy ôn lại";

                    //                    ms_testresults.AddObject(test);
                    //                    returnCode = this.Saves();
                    //                }
                    //            }
                    //            //}
                    //        }
                            
                    //    }
                    //}

                    if (returnCode == CommonData.DbReturnCode.Succeed)
                    {
                        returnCode = this.Commit();
                    }
                    else
                    {
                        this.Rollback();
                    }

                    #endregion

                    var first = vocas.FirstOrDefault();
                    if (first != null)
                    {
                        var userVocaSets = (from us in this.ms_uservocabularies.Where(ss => ss.UserID == userID && ss.ms_vocabularydetails.ms_vocacategories.VocaSetID == first.VocaSetID)
                                            group us by us.ms_vocabularydetails.ms_vocacategories.ms_vocasets.ID into userVoca

                                            //join uvs in ms_uservocasets.Where(ss => ss.UserID == userID) on userVoca.Key equals uvs.VocaSetID into userSet
                                            //from uvs in userSet.DefaultIfEmpty()

                                            join vs in ms_vocasets on userVoca.Key equals vs.ID

                                            select new MS_UserVocaSet
                                            {
                                                //VocaSetID = vs.ID,
                                                //VocaSetName = vs.Name1,
                                                //VocaSetDescription = vs.Description,
                                                ////UpdateDate = uvs == null ? null : uvs.UpdatedDate,

                                                //VocaSetUrlDisplay = vs.UrlDisplay,
                                                //VocaSetUrlImage = vs.UrlImage,
                                                NumOfVoca = vs.NumOfVocas ?? 0,
                                                NumOfHasMarked = userVoca.Count(s => s.HasMarked == CommonData.Status.Enable),
                                                NumOfHasLearnt = userVoca.Count(s => s.HasLearnt == CommonData.Status.Enable),
                                                NumOfWeak = userVoca.Count(s => s.IsIgnore == CommonData.Status.Disable 
                                                    //&& s.ms_vocabularydetails.ms_vocabularies.Type == CommonData.VocaType.Word
                                                                        && s.HasLearnt == CommonData.Status.Enable && s.Level < 9),
                                                //HasRegis = true,
                                            }).ToList();
                        if (userVocaSets.Count > 0)
                        {
                            numOfVocaOfSet = userVocaSets.FirstOrDefault().NumOfVoca;
                            numOfHasMarkedOfSet = userVocaSets.FirstOrDefault().NumOfHasMarked;
                            numOfHasLearntOfSet = userVocaSets.FirstOrDefault().NumOfHasLearnt;
                            numOfWeakOfSet = userVocaSets.FirstOrDefault().NumOfWeak;
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

        internal int SelectUserHomePageData(int userID, out MS_UsersModels userModel)
        {
            int returnCode = 0;
            userModel = null;
            try
            {
                userModel = ms_users.Where(ss => ss.ID == userID)
                    .Select(ss => new MS_UsersModels
                    {
                        ID = ss.ID,
                        DisplayName = ss.DisplayName,
                        LastVisitedDate = ss.LastVisitedDate,
                        Point = ss.Point ?? 0,
                        UrlImage = ss.UrlImage,
                        UserName = ss.UserName,
                        NumOfLearntVoca = ss.NumOfLearntVoca ?? 0,

                    })
                    .FirstOrDefault();
                if (userModel != null)
                {
                    //update level
                    var usVocas = this.ms_uservocabularies.Where(ss => ss.UserID == userID);
                    foreach (var usVoca in usVocas)
                    {
                        if (usVoca.UpdatedDate.HasValue)
                        {
                            if (usVoca.Level > 0)
                            {
                                int minutes = (DateTime.Now - usVoca.UpdatedDate.Value).Minutes;
                                int hours = minutes / 60;
                                usVoca.Level -= (hours / 12);
                                if (usVoca.Level < 0)
                                {
                                    usVoca.Level = 0;
                                }
                            }
                        }
                    }

                    returnCode = this.Saves();

                    var userVocaSets = (from us in this.ms_uservocabularies.Where(ss => ss.UserID == userID)
                                        group us by us.ms_vocabularydetails.ms_vocacategories.ms_vocasets.ID into userVoca
                                        
                                        join uvs in ms_uservocasets.Where(ss => ss.UserID == userID) on userVoca.Key equals uvs.VocaSetID into userSet
                                        from uvs in userSet.DefaultIfEmpty()

                                        join vs in ms_vocasets on userVoca.Key equals vs.ID
                                        
                                        select new MS_UserVocaSet
                                        {
                                            VocaSetID = vs.ID,
                                            VocaSetName = vs.Name1,
                                            VocaSetDescription = vs.Description,
                                            UpdateDate = uvs == null ? null : uvs.UpdatedDate,

                                            VocaSetUrlDisplay = vs.UrlDisplay,
                                            VocaSetUrlImage = vs.UrlImage,
                                            NumOfVoca = vs.NumOfVocas ?? 0,
                                            NumOfHasMarked = userVoca.Count(s => s.HasMarked == CommonData.Status.Enable),
                                            NumOfHasLearnt = userVoca.Count(s => s.HasLearnt == CommonData.Status.Enable),
                                            NumOfWeak = userVoca.Count(s => s.IsIgnore == CommonData.Status.Disable 
                                                //&& s.ms_vocabularydetails.ms_vocabularies.Type == CommonData.VocaType.Word 
                                                                    && s.HasLearnt == CommonData.Status.Enable && s.Level < 9),
                                            HasRegis = true,
                                        }).ToList();

                    if (userVocaSets.Count == 0)
                    {
                        //select all voca sets
                        userVocaSets = (from vs in this.ms_vocasets
                                        select new MS_UserVocaSet
                                        {
                                            VocaSetID = vs.ID,
                                            VocaSetName = vs.Name1,
                                            VocaSetDescription = vs.Description,
                                            VocaSetUrlDisplay = vs.UrlDisplay,
                                            VocaSetUrlImage = vs.UrlImage,
                                            NumOfVoca = vs.NumOfVocas ?? 0,
                                            NumOfRegistedPerson = vs.NumOfRegistedPerson ?? 0,
                                            NumOfCategories = vs.NumOfCategories ?? 0,
                                            NumOfFinishedPerson = vs.NumOfFinishedPerson ?? 0,
                                            HasRegis = false,
                                        }).ToList();
                    }

                    userModel.UserVocaSets = userVocaSets;
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }

        internal int SelectUserVocaCateData(int userID, out MS_UsersModels userModel)
        {
            int returnCode = 0;
            userModel = null;
            try
            {
                userModel = ms_users.Where(ss => ss.ID == userID)
                    .Select(ss => new MS_UsersModels
                    {
                        ID = ss.ID,
                        DisplayName = ss.DisplayName,
                        LastVisitedDate = ss.LastVisitedDate,
                        Point = ss.Point ?? 0,
                        UrlImage = ss.UrlImage,
                        UserName = ss.UserName,
                        NumOfLearntVoca = ss.NumOfLearntVoca ?? 0,

                    })
                    .FirstOrDefault();
                if (userModel != null)
                {
                    //select all voca sets
                    var userVocaSets = (from vs in this.ms_vocasets
                                    select new MS_UserVocaSet
                                    {
                                        VocaSetID = vs.ID,
                                        VocaSetName = vs.Name1,
                                        VocaSetDescription = vs.Description,
                                        VocaSetUrlDisplay = vs.UrlDisplay,
                                        VocaSetUrlImage = vs.UrlImage,
                                        NumOfVoca = vs.NumOfVocas ?? 0,
                                        NumOfRegistedPerson = vs.NumOfRegistedPerson ?? 0,
                                        NumOfCategories = vs.NumOfCategories ?? 0,
                                        NumOfFinishedPerson = vs.NumOfFinishedPerson ?? 0,
                                        HasRegis = false,
                                    }).ToList();

                    userModel.UserVocaSets = userVocaSets;
                }
            }
            catch (Exception ex)
            {
                returnCode = ProcessDbException(ex);
            }

            return returnCode;
        }



        internal int IgnoreCategory(int userID, MS_VocaCategoriesModels voca)
        {
            int returnCode = 0;
            try
            {
                var userCate = this.ms_usercategories.FirstOrDefault(ss => ss.UserID == userID && ss.CategoryID == voca.ID);
                if (userCate != null)
                {
                    userCate.IsIgnore = voca.IsIgnore;

                    var userVocas = this.ms_uservocabularies.Where(ss => ss.UserID == userID
                    && ss.ms_vocabularydetails.ms_vocacategories.ID == voca.ID);
                    foreach (var userVoca in userVocas)
                    {
                        userVoca.IsIgnore = voca.IsIgnore;
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

    }
}