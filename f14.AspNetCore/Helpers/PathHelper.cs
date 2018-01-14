using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides helper methods for work with path strings.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Cuts the path to the specified number of sections. The path section is considered as part of the way separated by slash (/).
        /// </summary>
        /// <param name="path">The path string.</param>
        /// <param name="skip">Number of sections of the path for skipping, starting at the end of the path.</param>
        /// <returns>A new path line, a new path at the beginning and at the end does not contain a slash.</returns>
        public static string RemoveSection(string path, int skip)
        {
            string[] parts = path.Split('/').Except(new string[] { " ", "" }).ToArray();
            int lng = parts.Length;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < lng; i++)
            {
                var p = parts[i];

                if (string.IsNullOrWhiteSpace(p))
                    continue;

                if (i < lng - skip)
                {
                    sb.Append("/").Append(p);
                }
                else
                {
                    break;
                }
            }

            string result = sb.ToString();
            if (result.StartsWith("/"))
            {
                result = result.Remove(0, 1);
            }

            return result;
        }
    }
}
