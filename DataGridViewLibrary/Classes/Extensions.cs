using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewLibrary.Classes
{
    public static class Extensions
    {
        public static string GetStringSafe(this IDataReader pReader, string pField) 
            => ((pReader[pField] is DBNull) ? null : pReader[pField].ToString());
    }
}
