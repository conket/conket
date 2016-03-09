using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ivs.Core.Common;

namespace Ivs.Core.Common
{
    public class GridSetting
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }

        public GridSetting()
        {
        }

        public GridSetting(int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.SortColumn = sortColumn;
            this.SortOrder = sortOrder;
        }

        public GridSetting(ControllerContext controllerContext)
        {
            var request = controllerContext.HttpContext.Request;
            this.PageIndex = CommonMethod.ParseInt(request["page"] ?? "1");
            this.PageSize = CommonMethod.ParseInt(request["rows"] ?? "20");
            this.SortColumn = request["sidx"] ?? "";
            this.SortOrder = request["sord"] ?? "asc";
        }

        public IQueryable<T> LoadGridData<T>(IEnumerable<T> dataSource)
        {
            IQueryable<T> query = dataSource.AsQueryable();
            if (!CommonMethod.IsNullOrEmpty(this.SortColumn) && !CommonMethod.IsNullOrEmpty(this.SortOrder))
            {
                query = query.OrderBy<T>(this.SortColumn, this.SortOrder).AsQueryable();
                if (this.PageIndex < 1)
                    this.PageIndex = 1;
            }
            return query;
        }

        public IQueryable<T> LoadGridData<T>(IEnumerable<T> dataSource, bool processOnSouce)
        {
            IQueryable<T> query = dataSource.AsQueryable();
            if (!CommonMethod.IsNullOrEmpty(this.SortColumn) && !CommonMethod.IsNullOrEmpty(this.SortOrder))
            {
                query = query.OrderBy<T>(this.SortColumn, this.SortOrder, processOnSouce).AsQueryable();
                if (this.PageIndex < 1)
                    this.PageIndex = 1;
            }
            return query;
        }
    }
}