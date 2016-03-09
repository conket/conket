using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Ivs.Core.Common
{
    public static class LanguageUltility
    {
        //Language
        public static string Language { get; set; }

        //Language Dictionary
        public static Dictionary<string, string> LanguageDitionary { get; set; }

        //Current Culture
        private static CultureInfo CurrentLocale = Thread.CurrentThread.CurrentUICulture;

        /// <summary>
        /// Set language for control
        /// </summary>
        /// <param name="lang"></param>
        public static void ChangeLanguage(string baseName, Assembly assembly, System.Windows.Forms.Form frm, Dictionary<object, string> lstControls, string lang, bool isSetCulture = true)
        {
            //Set Language Property
            Language = lang;
            //Set Language for Form Display
            if (isSetCulture)
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            ComponentResourceManager resources = new ComponentResourceManager(frm.GetType());

            if (lstControls != null)
            {
                foreach (var ctrl in lstControls)
                {

                    var propertyInfo = ctrl.Key.GetType().GetProperty("Location");
                    if (propertyInfo != null)
                    {
                        var location = propertyInfo.GetValue(ctrl.Key, null);
                        resources.ApplyResources(ctrl.Key, ctrl.Value, new System.Globalization.CultureInfo(lang));
                        propertyInfo.SetValue(ctrl.Key, location, null);
                    }
                    else
                        resources.ApplyResources(ctrl.Key, ctrl.Value, new System.Globalization.CultureInfo(lang));

                }
            }

            resources = new ComponentResourceManager(frm.GetType().BaseType);

            if (lstControls != null && IsExistResource(resources, lstControls))
            {
                foreach (var ctrl in lstControls)
                {

                    var propertyInfo = ctrl.Key.GetType().GetProperty("Location");
                    if (propertyInfo != null)
                    {
                        var location = propertyInfo.GetValue(ctrl.Key, null);
                        resources.ApplyResources(ctrl.Key, ctrl.Value, new System.Globalization.CultureInfo(lang));
                        propertyInfo.SetValue(ctrl.Key, location, null);
                    }
                    else
                        resources.ApplyResources(ctrl.Key, ctrl.Value, new System.Globalization.CultureInfo(lang));
                }
            }

            //Set Language for Message
            SetMessageLanguage(baseName, assembly, lang);
        }



        /// <summary>
        /// Check exist resource for control list
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="lstControls"></param>
        /// <returns></returns>
        private static bool IsExistResource(ComponentResourceManager resources, Dictionary<object, string> lstControls)
        {
            try
            {
                if (lstControls != null)
                {
                    foreach (var ctrl in lstControls)
                    {
                        var propertyInfo = ctrl.Key.GetType().GetProperty("Location");
                        if (propertyInfo != null)
                        {
                            var location = propertyInfo.GetValue(ctrl.Key, null);
                            resources.ApplyResources(ctrl.Key, ctrl.Value);
                            propertyInfo.SetValue(ctrl.Key, location, null);
                        }
                        else
                            resources.ApplyResources(ctrl.Key, ctrl.Value);
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Set language for control
        /// </summary>
        /// <param name="lang"></param>
        private static void ChangeLanguage(string baseName, Assembly assembly, System.Windows.Forms.Form frm, string lang)
        {
            //Set Language Property
            Language = lang;
            //Set Language for Form Display
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            CurrentLocale = Thread.CurrentThread.CurrentUICulture;

            foreach (Control c in frm.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(frm.GetType());
                resources.ApplyResources(c, c.Name, CurrentLocale);
                RefreshResources(frm, resources);
            }

            //Set Language for Message
            SetMessageLanguage(baseName, assembly, lang);
        }

        /// <summary>
        /// RefreshResources
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="res"></param>
        private static void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            res.ApplyResources(ctrl, ctrl.Name, CurrentLocale);
            foreach (Control control in ctrl.Controls)
            {
                RefreshResources(control, res); // recursion
            }
            ctrl.ResumeLayout(false);
        }

        /// <summary>
        /// Transfer Message Resource To Dictionary
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        public static void SetMessageLanguage(string baseName, Assembly assembly, string lang)
        {
            //Set Language Property
            Language = lang;
            //Get Message Resource
            //+ baseName, eg: Iwms.WinForm.Properties.Message_{0}
            ResourceManager resxMessage = new ResourceManager(String.Format(baseName, lang), assembly);

            var resourceSet = resxMessage.GetResourceSet(new System.Globalization.CultureInfo(lang), true, true);
            Dictionary<string, string> resourceDictionary = resourceSet.Cast<DictionaryEntry>()
                                    .ToDictionary(r => r.Key.ToString(),
                                                  r => r.Value.ToString());

            LanguageDitionary = resourceDictionary;
        }

        /// <summary>
        /// Get string from resource file.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public static string GetString(string resourceKey, string defaultValue = "")
        {
            if (LanguageDitionary != null && LanguageDitionary.ContainsKey(resourceKey))
            {
                return LanguageDitionary[resourceKey];
            }
            return defaultValue;
        }

    }
}