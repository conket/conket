using System;
using System.Resources;
using Ivs.Core.Common;

namespace Ivs.Core.Data
{
    public class I18n
    {
        private ResourceManager resxScreenDisplay;
        private ResourceManager resxMessage;
        private string functionID;

        public I18n(string functionID)
        {
            this.functionID = functionID.Contains("Form") ? functionID : functionID + "Form";
            GetScreenDisplayResource();
        }

        public I18n()
        {
            GetMessageResource();
        }

        /// <summary>
        /// Get resource file
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        private void GetScreenDisplayResource()
        {
            string langID = string.IsNullOrEmpty(UserSession.LangId) ? CommonData.Language.English : UserSession.LangId;
            resxScreenDisplay = new ResourceManager(String.Format("ScreenDisplay_{0}", langID), typeof(I18n).Assembly);
        }

        /// <summary>
        /// Get resource file
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        public void GetScreenDisplayResource(string _langID)
        {
            //ResourceManager resxScreenDisplay;
            string langID = string.IsNullOrEmpty(_langID) ? CommonData.Language.English : _langID;
            resxScreenDisplay = new ResourceManager(String.Format("ScreenDisplay_{0}", langID), typeof(I18n).Assembly);
        }

        /// <summary>
        /// Get resource file
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        private void GetMessageResource()
        {
            string langID = string.IsNullOrEmpty(UserSession.LangId) ? CommonData.Language.English : UserSession.LangId;
            resxMessage = new ResourceManager(String.Format("Message_{0}", langID), typeof(I18n).Assembly);
        }

        public string GetString(string controlID)
        {
            string key = functionID + "_" + controlID;
            return "";
        }

        public string GetMessage(string key)
        {
            //return resxMessage.GetString(key);
            return string.Empty;
        }

        public string GetMessage(string key, string languageID)
        {
            //ResourceManager resxMessage;
            //string langID = string.IsNullOrEmpty(languageID) ? CommonData.Language.English : languageID;
            //resxMessage = new ResourceManager(String.Format("Message_{0}", langID), typeof(I18n).Assembly);
            //return resxMessage.GetString(key);
            return string.Empty;
        }
    }
}