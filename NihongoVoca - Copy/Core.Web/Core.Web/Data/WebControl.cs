using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ivs.Core.Interface;
using Ivs.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Ivs.Core.Web.Data
{
    public class WebControl : BaseWebControl
    {
        #region Properties

        public virtual string Caption { get; protected set; }
        public virtual string IconName { get; protected set; }
        public virtual CommonData.ButtonCategory ButtonCategory { get; protected set; }
        public virtual CommonData.ButtonWebType ButtonType { get; protected set; }

        private HtmlHelper _helper;
        public override HtmlHelper Helper 
        {
            get
            {
                return _helper;
            }
            protected set
            {
                _helper = value;

                this.SetValidationAttributes();
                this.SetCustomAttributes();
            }
        }

        private string _name;
        public override string Name 
        {
            get { return _name; }
            protected set
            {
                _name = value;

                this.SetValidationAttributes();
                this.SetCustomAttributes();
            }
        }

        private bool _isRequire;
        public virtual bool IsRequire
        {
            get { return _isRequire; }
            protected set
            {
                _isRequire = value;
            }
        }

        private int _maxLength;
        public virtual int MaxLength
        {
            get { return _maxLength; }
            protected set
            {
                _maxLength = value;
            }
        }

        public virtual object Value { get; protected set; }
        public virtual object HtmlAttributes { get; protected set; }
        public virtual bool Readonly { get; protected set; }
        public virtual bool IsUnique { get; protected set; }
        public virtual bool IsPrimary { get; protected set; }
        public virtual bool IsFocus { get; protected set; }
        public virtual CommonData.Width Width { get; protected set; }
        public virtual CommonData.Mode Mode { get; protected set; }

        private IDictionary<string, object> _validatioinAttributes = new Dictionary<string, object>();
        public virtual IDictionary<string, object> ValidationAttributes
        {
            get { return _validatioinAttributes; }
            set { _validatioinAttributes = value; }
        }

        #endregion

        public void SetValidationAttributes()
        {
            if (this.Helper != null && !CommonMethod.IsNullOrEmpty(this.Name))
            {
                if (this.ValidationAttributes.Count == 0)
                {
                    ModelMetadata modelMetadata = ModelMetadata.FromStringExpression(this.Name, this.Helper.ViewData);
                    this.ValidationAttributes = this.Helper.GetUnobtrusiveValidationAttributes(this.Name, modelMetadata);
                }
            }
        }

        public void SetCustomAttributes()
        {
            if (this.Helper != null && !CommonMethod.IsNullOrEmpty(this.Name))
            {
                ModelMetadata modelMetadata = ModelMetadata.FromStringExpression(this.Name, this.Helper.ViewData);

                if (!CommonMethod.IsNullOrEmpty(modelMetadata.PropertyName))
                {
                    var requireAttr = modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName)
                                       .GetCustomAttributes(typeof(RequiredAttribute), false)
                                       .FirstOrDefault() as RequiredAttribute;
                    if (requireAttr != null)
                    {
                        this.IsRequire = !requireAttr.AllowEmptyStrings;
                    }

                    var lengthAttr = modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName)
                                       .GetCustomAttributes(typeof(StringLengthAttribute), false)
                                       .FirstOrDefault() as StringLengthAttribute;
                    if (lengthAttr != null)
                    {
                        this.MaxLength = lengthAttr.MaximumLength;
                    }
                }
            }
        }
    }
}
