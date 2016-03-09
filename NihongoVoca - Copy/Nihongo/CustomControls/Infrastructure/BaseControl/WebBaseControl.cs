using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ivs.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Ivs.Controls.CustomControls.Infrastructure.BaseControl
{
    public class WebBaseControl
    {
        private HtmlHelper _helper;
        public virtual HtmlHelper Helper
        {
            get
            {
                return _helper;
            }
            protected set
            {
                _helper = value;

                HelperOnchaged();
            }
        }

        private string _name;
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                NameOnchanged();
            }
        }

        public virtual string FormatString { get; protected set; }
        public virtual bool IsRequire { get; protected set; }
        public virtual int MaxLength { get; protected set; }
        public virtual CommonData.Mode Mode { get; protected set; }
        public virtual object HtmlAttributes { get; protected set; }
        private IDictionary<string, object> _validatioinAttributes = new Dictionary<string, object>();
        public virtual IDictionary<string, object> ValidationAttributes
        {
            get { return _validatioinAttributes; }
            set { _validatioinAttributes = value; }
        }

        public WebBaseControl SetName(string name)
        {
            this.Name = name;
            return this;
        }

        protected virtual void HelperOnchaged()
        {
            this.SetModeFromModelMetadata();
            this.SetCustomAttributes();
            this.SetValidationAttributes();
        }

        protected virtual void NameOnchanged()
        {
            this.SetCustomAttributes();
            this.SetValidationAttributes();
        }


        private void SetModeFromModelMetadata()
        {
            if (this.Helper != null)
            {
                if (this.Helper.ViewBag.Mode != null)
                {
                    this.Mode = (CommonData.Mode)this.Helper.ViewBag.Mode;
                }
            }
        }

        private void SetCustomAttributes()
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

                    var displayFormatAttr = modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName)
                                       .GetCustomAttributes(typeof(DisplayFormatAttribute), false)
                                       .FirstOrDefault() as DisplayFormatAttribute;
                    if (displayFormatAttr != null)
                    {
                        this.FormatString = displayFormatAttr.DataFormatString;
                    }
                }
            }
        }

        private void SetValidationAttributes()
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
    }
}
