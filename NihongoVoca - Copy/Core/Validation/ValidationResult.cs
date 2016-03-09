using System.ComponentModel;
using Ivs.Core.Common;
using Ivs.Core.Data;

namespace Ivs.Core.Validation
{
    public class ValidationResult
    {
        #region Private Variables

        protected int _rowIndex = -1;
        protected string _fieldName;
        protected CommonData.IsValid _isValid;
        protected IvsMessage _message;

        #endregion Private Variables

        #region Properties

        [DefaultValue(-1)]
        public int RowIndex
        {
            get
            {
                return _rowIndex;
            }
        }

        public string FieldName
        {
            get
            {
                return _fieldName;
            }
        }

        public CommonData.IsValid IsValid
        {
            get
            {
                return _isValid;
            }
        }

        public IvsMessage Message
        {
            get
            {
                return _message;
            }
        }

        #endregion Properties

        public ValidationResult(string fieldName, CommonData.IsValid isValid, IvsMessage message)
        {
            this._fieldName = fieldName;
            this._isValid = isValid;
            this._message = message;
        }

        public ValidationResult(string fieldName, CommonData.IsValid isValid, string messageId)
        {
            this._fieldName = fieldName;
            this._isValid = isValid;
            this._message = new IvsMessage(messageId);
        }

        public ValidationResult(int rowIndex, string fieldName, CommonData.IsValid isValid, IvsMessage message)
        {
            this._rowIndex = rowIndex;
            this._fieldName = fieldName;
            this._isValid = isValid;
            this._message = message;
        }

        public ValidationResult(int rowIndex, string fieldName, CommonData.IsValid isValid, string messageId)
        {
            this._rowIndex = rowIndex;
            this._fieldName = fieldName;
            this._isValid = isValid;
            this._message = new IvsMessage(messageId);
        }
    }
}