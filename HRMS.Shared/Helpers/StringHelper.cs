using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Helpers
{
    public static class StringHelper
    {
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
