using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PULSEImport
{
    public static class SafeType
    {
        public static string SafeString(object s)
        {
            if (s == null)
                return string.Empty;

            return string.IsNullOrEmpty(s.ToString()) ? string.Empty : s.ToString();
        }
    }
}
