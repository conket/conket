using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nihongo.Models
{
    public class MS_VocabulariesModels
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int? LineNumber { get; set; }
        public string Romaji { get; set; }
        public string Hiragana { get; set; }
        public string Katakana { get; set; }
        public string Kanji { get; set; }
        public string DisplayType { get; set; }
        public string LessonCode { get; set; }
        public string VMeaning { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string UrlAudio { get; set; }
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
        public string ResultUrlAudio1 { get; set; }
        public string Result2 { get; set; }
        public string ResultUrlAudio2 { get; set; }
        public string Result3 { get; set; }
        public string ResultUrlAudio3 { get; set; }
        public string Result4 { get; set; }
        public string ResultUrlAudio4 { get; set; }
        public int CorrectResult { get; set; }
    }
}