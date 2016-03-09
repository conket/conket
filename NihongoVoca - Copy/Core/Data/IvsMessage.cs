using Ivs.Core.Common;

namespace Ivs.Core.Data
{
    public class IvsMessage
    {
        private string _messageId;
        private string _messageText;
        private string _messageIcon = CommonData.MessageIcon.Infomation;
        private int _parameter = -1;
        private string _parameterS = CommonData.StringEmpty;
        private object[] _parameterString = null;

        public object[] ParameterString
        {
            get { return _parameterString; }
            set { _parameterString = value; }
        }

        public string MessageId
        {
            get
            {
                return _messageId;
            }
        }

        public string MessageText
        {
            get
            {
                return _messageText;
            }
              set
            {
                _messageText = value;
            }
        }

        public string MessageIcon
        {
            get
            {
                return _messageIcon;
            }
            set
            {
                _messageIcon = value;
            }
        }
        public IvsMessage() { }
        //public CommonData.MessageIcon MessageIcon { get; set; }

        //Plco start 20131118
        public IvsMessage(string messageId)
        {
            _messageId = messageId;

            this.SplitMessage(LanguageUltility.GetString(messageId), ref _messageText, ref _messageIcon);
            //_messageText = LanguageUltility.GetString(messageId);
            //if (!string.IsNullOrEmpty(_messageText))
            //{
            //    string messIconTemp = _messageText.Substring(0, 2);
            //    if (messIconTemp == CommonData.MessageIcon.Infomation || messIconTemp == CommonData.MessageIcon.Error || messIconTemp == CommonData.MessageIcon.Warning)
            //    {
            //        _messageIcon = messIconTemp;
            //        _messageText = _messageText.Remove(0, 3);
            //    }
            //}
        }
        //public IvsMessage(string messageText, string messageIcon)
        //{
        //    _messageText = messageText;
        //    _messageIcon = messageIcon;
        //}
        //public IvsMessage(string messageId, string messageIcon)
        //    : this(messageId)
        //{
        //    //this._messageIcon = messageIcon;
        //}

        public void SetMessage(string messageText, string messageIcon)
        {
            _messageText = messageText;
            _messageIcon = messageIcon;
        }

        public IvsMessage(string messageId, params object[] parameters)
        {
            try
            {
                _messageId = messageId;
                _parameterString = parameters;
                string message = string.Format(LanguageUltility.GetString(messageId), parameters);
                this.SplitMessage(message, ref _messageText, ref _messageIcon);

                //_messageText = !string.IsNullOrEmpty(message) ? string.Format(message, parameters) : string.Empty;
                //if (!string.IsNullOrEmpty(_messageText))
                //{
                //    string messIconTemp = _messageText.Substring(0, 2);
                //    if (messIconTemp == CommonData.MessageIcon.Infomation || messIconTemp == CommonData.MessageIcon.Error || messIconTemp == CommonData.MessageIcon.Warning)
                //    {
                //        _messageIcon = messIconTemp;
                //        _messageText = _messageText.Remove(0, 3);
                //    }
                //}
            }
            catch
            {
                _messageText = string.Empty;
                _messageIcon = CommonData.MessageIcon.Infomation;
            }
        }

        private bool SplitMessage(string resourceText, ref string messText, ref string messIcon)
        {
            try
            {
                messText = resourceText;
                if (!string.IsNullOrEmpty(resourceText))
                {
                    string messIconTemp = resourceText.Substring(0, 2);
                    if (messIconTemp == CommonData.MessageIcon.Infomation 
                        || messIconTemp == CommonData.MessageIcon.Error 
                        || messIconTemp == CommonData.MessageIcon.Warning
                        || messIconTemp == CommonData.MessageIcon.Question
                        || messIconTemp == CommonData.MessageIcon.OK
                        )
                    {
                        messIcon = messIconTemp;
                        messText = _messageText.Remove(0, 3);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public IvsMessage(string messageId, string messageIcon, params object[] parameters)
        //    : this(messageId, parameters)
        //{
        //    //this._messageIcon = messageIcon; 
        //}

        //end

        //public IvsMessage(string messageId)
        //{
        //    //create I18n class object
        //    I18n i18n = new I18n();

        //    _messageId = messageId;
        //    _messageText = i18n.GetMessage(messageId);
        //}

        //public IvsMessage(string messageId, int para)
        //{
        //    //create I18n class object
        //    I18n i18n = new I18n();

        //    _messageId = messageId;
        //    _parameter = para;
        //    _messageText = i18n.GetMessage(messageId);

        //    // replace ky tu $ trong _messageText bang para
        //    if (para >= 0)
        //        _messageText = _messageText.Replace("$", para.ToString());
        //}

        //public IvsMessage(string messageId, string para)
        //{
        //    //create I18n class object
        //    I18n i18n = new I18n();

        //    _messageId = messageId;
        //    _parameterS = para;
        //    _messageText = i18n.GetMessage(messageId);

        //    // replace ky tu $ trong _messageText bang para
        //    if (!string.IsNullOrEmpty(para))
        //        _messageText = _messageText.Replace("$", para);
        //}

        //public IvsMessage(string messageId, string[] para)
        //{
        //    //create I18n class object
        //    I18n i18n = new I18n();

        //    _messageId = messageId;

        //    _parameterString = para;

        //    _messageText = i18n.GetMessage(messageId);

        //    if (!string.IsNullOrEmpty(_messageText))
        //    {
        //        string[] messTmp = _messageText.Split('$');
        //        _messageText = CommonData.StringEmpty;
        //        for (int i = 0; i < messTmp.Length; i++)
        //        {
        //            try
        //            {
        //                _messageText += messTmp[i] + " " + para[i];
        //            }
        //            catch
        //            {
        //                _messageText += messTmp[i];
        //            }
        //        }
        //    }
        //}

        public void ChangeLanguage(string langId)
        {
            string message = LanguageUltility.GetString(_messageId);
            if (_parameterString != null && _parameterString.Length > 0 && !string.IsNullOrEmpty(message))
                message = string.Format(message, _parameterString);
            this.SplitMessage(message, ref _messageText, ref _messageIcon);
            //_messageText = message;

            //create I18n class object
            //I18n i18n = new I18n();
            //_messageText = i18n.GetMessage(MessageId, langId);

            //// replace ky tu $ trong _messageText bang _parameter
            //if (_parameter >= 0)
            //    _messageText = _messageText.Replace("$", _parameter.ToString());
            //if (!string.IsNullOrEmpty(_parameterS))
            //    _messageText = _messageText.Replace("$", _parameterS.ToString());
            //else if (_parameterString != null && _parameterString.Length == 1)
            //{
            //    _messageText = _messageText.Replace("$", _parameterString[0]);
            //}
            //else if (_parameterString != null && _parameterString.Length > 1)
            //{
            //    string[] messTmp = _messageText.Split('$');
            //    _messageText = CommonData.StringEmpty;
            //    for (int i = 0; i < messTmp.Length; i++)
            //    {
            //        try
            //        {
            //            _messageText += messTmp[i] + "" + _parameterString[i];
            //        }
            //        catch
            //        {
            //            _messageText += messTmp[i];
            //        }
            //    }
            //}
        }
    }
}