using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurantDAL
{
    internal class QueryHelper
    {
        public static string GetSelectQuery(string DataTableName)
        {
            return GetSelectQuery(DataTableName, "*");
        }

        public static string GetSelectQuery(string DataTableName, string column)
        {
            return "SELECT " + column + " FROM " + DataTableName;
        }

        public static string GetPartialOrderedSelectQuery(string OrderColumn, bool desc)
        {
            return " ORDER BY " + OrderColumn + (desc ? " DESC" : "");
        }

        public static string GetIntegerFilteredSelectQuery(string FilterName, int Value)
        {
            return " WHERE " + FilterName + " = " + Value;
        }

        public static string GetStringFilteredSelectQuery(string FilterName, string Value)
        {
            if (FilterName == null || Value == null) return "";
            return " WHERE " + FilterName + " LIKE '%" + Value + "%'";
        }
    }
}
