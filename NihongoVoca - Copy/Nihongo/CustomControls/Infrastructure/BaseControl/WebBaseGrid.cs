using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Mvc;
using Ivs.Core.Common;

namespace Ivs.Controls.CustomControls.Infrastructure.BaseControl
{
    public class WebBaseGrid<TModel> : WebBaseControl
    {
        public List<WebGridColumn> GridColumns { get; protected set; }
        public IEnumerable<TModel> DataSource { get; protected set; }

        private bool _allowSort = true;
        [DefaultValue(true)]
        public bool AllowSort
        {
            get
            {
                return _allowSort;
            }
            private set
            {
                _allowSort = value;
            }
        }

        private bool _genCheckedColumn = true;
        [DefaultValue(true)]
        public bool GenCheckedColumn
        {
            get
            {
                return _genCheckedColumn;
            }
            private set
            {
                _genCheckedColumn = value;
            }
        }

        public bool IsPaging { get; protected set; }

        public WebBaseGrid<TModel> Columns(Action<WebBaseGrid<TModel>> columns)
        {
            columns(this);
            return this;
        }

        public WebBaseGrid<TModel> SetSortable(bool sortable = true)
        {
            this.AllowSort = sortable;
            return this;
        }

        public WebBaseGrid<TModel> GenerateName(string name)
        {
            this.Name = name;
            return this;
        }

        public WebBaseGrid<TModel> SetPagination(bool isPaging = true)
        {
            //this.RecordsPerPage = recordsPerPage;
            this.IsPaging = isPaging;
            return this;
        }

        public WebBaseGrid<TModel> GenerateCheckedColumn(bool genCheckedColumn = true)
        {
            this.GenCheckedColumn = genCheckedColumn;
            if (genCheckedColumn)
            {
                WebGridColumn col = new WebGridColumn("Select", CommonData.StringEmpty, true);
                this.GridColumns.Insert(0, col);
            }
            return this;
        }

        public WebGridColumn Add<TColumn>(Expression<Func<TModel, TColumn>> expression)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadataProviders.Current.GetMetadataForProperty(() => Activator.CreateInstance<TModel>(), typeof(TModel), name);

            var text = CommonMethod.IsNullOrEmpty(metadata.DisplayName)
                            ? metadata.PropertyName
                            : metadata.DisplayName;

            WebGridColumn col = new WebGridColumn(base.Helper, name, text);
            this.GridColumns.Add(col);
            return col;
        }
    }
}
