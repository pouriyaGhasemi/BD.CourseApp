using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.CourseApp.Infrastructures.Data.SqlServer.Extentions
{
    public class QueryBuilder
    {
        private string _select="";
        private StringBuilder _where;
        private string _orderby="";
        private string _pageby="";

        public QueryBuilder()
        {
            _where = new StringBuilder();
        }

        public QueryBuilder Select(string select)
        {
            _select = select;
            return this;
        }

        public QueryBuilder Where(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return this;
            if (_where.Length == 0)
                _where.Append("where ");
            else
                _where.Append("and ");

            _where.Append($"{condition} ");
            return this;
        }
        public QueryBuilder like(string name, string? filtter)
        {
            if (string.IsNullOrEmpty(filtter)) return this;
            if (_where.Length == 0)
                _where.Append("where ");
            else
                _where.Append("and ");

            _where.Append($" {name} like % {filtter} %");
            return this;
        }

        public QueryBuilder OrderBy(string columns)
        {
            _orderby = $"order by {columns} ";
            return this;
        }

        public QueryBuilder PageBy(int pageNumber, int pageSize, string orderByColumns)
        {
            //ToDo:Check input conditions
            ArgumentNullException.ThrowIfNullOrEmpty(orderByColumns);
            _orderby = $"order by {orderByColumns} ";
            _pageby += $"offset {(pageNumber-1) * pageSize} rows fetch next {pageSize} rows only ";
            return this;
        }

        public string Build()
        {
            return $"{_select.Trim()} {_where.ToString().Trim()} {_orderby.Trim()} {_pageby.Trim()}".Trim();
        }
    }
}
