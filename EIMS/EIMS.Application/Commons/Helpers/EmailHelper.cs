using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Helpers
{
    public static class EmailHelper
    {
        public static string ReplacePlaceholders(string templateText, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(templateText))
                return string.Empty;

            foreach (var item in replacements)
            {
                templateText = templateText.Replace(item.Key, item.Value);
            }

            return templateText;
        }
    }
}
