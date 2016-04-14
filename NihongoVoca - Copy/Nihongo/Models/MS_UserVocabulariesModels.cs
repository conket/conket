using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_UserVocabulariesModels
    {
        public int Point { get; set; }
        public string IsDone { get; set; }
        public int No { get; set; }
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrlDisplay { get; set; }
        public string CategoryDescription { get; set; }
        public int? RequiredTimePerVoca { get; set; }
        public int VocaSetID { get; set; }
        public string VocaSetCode { get; set; }
        public string VocaSetName { get; set; }
        public string VocaSetDescription { get; set; }
        public string VocaSetUrlDisplay { get; set; }
        public string VocaSetUrlImage { get; set; }
        public decimal VocaSetFee { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string VocabularyCode { get; set; }
        public int? Level { get; set; }
        public DateTime? Update_Date { get; set; }
        public int? LineNumber { get; set; }
        public string Romaji { get; set; }
        public string Romaji_Katakana { get; set; }
        public string Romaji_Kanji { get; set; }
        public string Hiragana { get; set; }
        public string Katakana { get; set; }
        public string Kanji { get; set; }
        public string HasLearnt { get; set; }
        public string HasMarked { get; set; }
        public string IsIgnore { get; set; }
        public int NumOfWrong { get; set; }
        public string VocaGetType { get; set; }
        public string Pinyin { get; set; }
        public string Remembering { get; set; }
        public string OnReading { get; set; }
        public string OnUrlAudio { get; set; }
        public string OnRomaji { get; set; }
        public string OnReading2 { get; set; }
        public string OnUrlAudio2 { get; set; }
        public string OnRomaji2 { get; set; }

        public string ExKanji1 { get; set; }
        public string ExKanji2 { get; set; }
        public string ExKanji3 { get; set; }
        public string ExKanji4 { get; set; }
        public string ExReading1 { get; set; }
        public string ExReading2 { get; set; }
        public string ExReading3 { get; set; }
        public string ExReading4 { get; set; }
        public string ExVMeaning4 { get; set; }

        public string KunReading { get; set; }
        public string KunUrlAudio { get; set; }
        public string KunRomaji { get; set; }
        public string KunReading2 { get; set; }
        public string KunUrlAudio2 { get; set; }
        public string KunRomaji2 { get; set; }

        public string VMeaning { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string UrlAudio { get; set; }
        public string UrlAudio_Katakana { get; set; }
        public string UrlAudio_Kanji { get; set; }
        public string Type { get; set; }
        public bool HasDiacritic { get; set; }
        public bool HasCombination { get; set; }
        public bool HasTsu { get; set; }
        public bool  HasNormal { get; set; }
        public bool HasLongSound { get; set; }
        public string ExRomaji1 { get; set; }
        public string ExHiragana1 { get; set; }
        public string ExKatakana1 { get; set; }
        public string ExVMeaning1 { get; set; }
        public string ExEMeaning1 { get; set; }
        public string ExUrlAudio1 { get; set; }

        public string ExRomaji2 { get; set; }
        public string ExHiragana2 { get; set; }
        public string ExKatakana2 { get; set; }
        public string ExVMeaning2 { get; set; }
        public string ExEMeaning2 { get; set; }
        public string ExUrlAudio2 { get; set; }

        public string ExRomaji3 { get; set; }
        public string ExHiragana3 { get; set; }
        public string ExKatakana3 { get; set; }
        public string ExVMeaning3 { get; set; }
        public string ExEMeaning3 { get; set; }
        public string ExUrlAudio3 { get; set; }

        public string Result1 { get; set; }
        public string Hiragana1 { get; set; }
        public string Katakana1 { get; set; }
        public string Kanji1 { get; set; }
        public string ResultUrlAudio1 { get; set; }
        public string Result2 { get; set; }
        public string Hiragana2 { get; set; }
        public string Katakana2 { get; set; }
        public string Kanji2 { get; set; }
        public string ResultUrlAudio2 { get; set; }
        public string Result3 { get; set; }
        public string Hiragana3 { get; set; }
        public string Katakana3 { get; set; }
        public string Kanji3 { get; set; }
        public string ResultUrlAudio3 { get; set; }
        public string Result4 { get; set; }
        public string Hiragana4 { get; set; }
        public string Katakana4 { get; set; }
        public string Kanji4 { get; set; }
        public string ResultUrlAudio4 { get; set; }
        public int CorrectResult { get; set; }
        public string CorrectUrlAudio { get; set; }
        public string DisplayText
        {
            get
            {
                string result = string.Empty;
                switch (DisplayType)
                {
                    case "1":
                        result = Hiragana;
                        break;
                    case "2":
                        result = Katakana;
                        break;
                    case "3":
                        result = Kanji;
                        break;
                    default:
                        result = Hiragana;
                        break;
                }

                return result;
            }
        }

        public string IsKanji { get; set; }
        public int CompletedTime { get; set; }
        public int NumOfWeakVoca { get; set; }
        public string DisplayType { get; set; }
        public string DisplayVoca
        {
            get
            {
                string text = string.Empty;
                //Hiragana
                if (DisplayType == "1")
                {
                    text = Hiragana;
                }
                else if (DisplayType == "2")
                {
                    text = Katakana;
                }
                else
                {
                    text = Kanji;
                }

                return text;
            }
           
        }

        public string DisplayUrlAudio
        {
            get
            {
                string text = string.Empty;
                //Hiragana
                if (DisplayType == "1")
                {
                    text = UrlAudio;
                }
                else if (DisplayType == "2")
                {
                    text = UrlAudio_Katakana;
                }
                else
                {
                    text = UrlAudio;
                }

                return text;
            }

        }

        public string TestType { get; set; }
        public string TestSkill { get; set; }
        public string IsCorrect { get; set; }
        public string SelectedValue { get; set; }
    }

    public class MS_UserVocaSet
    {
        public bool HasRegis { get; set; }
        public int ID { get; set; }
        public int VocaSetID { get; set; }
        public string VocaSetName { get; set; }
        public string VocaSetDescription { get; set; }
        public string VocaSetUrlImage { get; set; }
        public string VocaSetUrlDisplay { get; set; }
        public int NumOfHasLearnt { get; set; }
        public int NumOfWeak { get; set; }
        public int NumOfVoca { get; set; }
        public int NumOfRegistedPerson { get; set; }
        public int NumOfCategories { get; set; }
        public int NumOfFinishedPerson { get; set; }
        public decimal PercentHasLearnt
        {
            get
            {
                return NumOfVoca == 0 ? 0 : (Convert.ToDecimal(NumOfHasLearnt) / Convert.ToDecimal(NumOfVoca) * 100);
            }
        }
    }
}