using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Helpers
{
    public static class CodeHelper
    {
        public static string GetPrefix(string entityName)
        {
            if (string.IsNullOrWhiteSpace(entityName))
                throw new ArgumentException("Entity name is required");

            return entityName.Length >= 3
                ? entityName.Substring(0, 3).ToUpper()
                : entityName.ToUpper().PadRight(3, 'X'); // fallback
        }

        public static string Generate(string prefix, int number, int length = 3)
        {
            return $"{prefix}{number.ToString().PadLeft(length, '0')}";
        }
    }
}
