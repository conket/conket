using Ivs.Core.Common;

namespace Ivs.Core.Data
{
    public class CultureInfo
    {
        private string _langId;

        /// <summary>
        /// I18n for Date</summary>
        /// <value>
        /// Long Date Format String </value>
        public string LongDateFormatPattern
        { get; set; }

        /// <summary>
        /// I18n for Date</summary>
        /// <value>
        /// Short Date Format String </value>
        public string ShortDateFormatPattern
        {
            get
            {
                switch (_langId)
                {
                    case CommonData.Language.English: return CommonData.DateFormat.MMddyyyy;
                    case CommonData.Language.Japanese: return CommonData.DateFormat.Yyyy_MM_dd;
                    case CommonData.Language.VietNamese: return CommonData.DateFormat.DdMMyyyy;
                    default: return CommonData.DateFormat.MMddyyyy;
                }
            }
        }

        public string ShortDateTimeFormatPattern
        {
            get
            {
                switch (_langId)
                {
                    case CommonData.Language.English: return CommonData.DateFormat.MMddyyyyHHmmss;
                    case CommonData.Language.Japanese: return CommonData.DateFormat.Yyyy_MM_ddHHmmss;
                    case CommonData.Language.VietNamese: return CommonData.DateFormat.MMddyyyyHHmmss;
                    default: return CommonData.DateFormat.MMddyyyyHHmmss;
                }
            }
        }

        public string NameFormatPattern
        {
            get
            {
                switch (_langId)
                {
                    case CommonData.Language.English: return CommonKey.Name1;
                    case CommonData.Language.Japanese: return CommonKey.Name2;
                    case CommonData.Language.VietNamese: return CommonKey.Name3;
                    default: return CommonKey.Name1;
                }
            }
        }

        public string NameCultureInfo
        {
            get
            {
                switch (_langId)
                {
                    case CommonData.Language.English: return "en-US";
                    case CommonData.Language.Japanese: return "ja-JP";
                    case CommonData.Language.VietNamese: return "vi-VN";
                    default: return "en-US";
                }
            }
        }

        public CultureInfo(string langId)
        {
            _langId = langId;
        }

        public CultureInfo()
        {
            _langId = UserSession.LangId;
        }

        public string GetNameCultureInfo(string language)
        {
            switch (language)
            {
                case CommonData.Language.English: return "en-US";
                case CommonData.Language.Japanese: return "ja-JP";
                case CommonData.Language.VietNamese: return "vi-VN";
                default: return "en-US";
            }
        }
    }
}