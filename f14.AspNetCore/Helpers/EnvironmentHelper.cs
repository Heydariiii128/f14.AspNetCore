using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides a helper methods for work with <see cref="Environment"/>.
    /// </summary>
    public static class EnvironmentHelper
    {
        public static bool HasEnvironmentVariable(string varName) => Environment.GetEnvironmentVariable(varName) != null;        
    }
}
