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
        /// <summary>
        /// Enumerates build type.
        /// </summary>
        public enum BuildType
        {
            Development,
            Staging,
            Production
        }

        /// <summary>
        /// Gets build type based on environment var = ASPNETCORE_ENVIRONMENT.
        /// </summary>
        /// <returns></returns>
        public static BuildType CurrentBuildType()
        {
            string value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(value))
                return BuildType.Development;
            else
                return (BuildType)Enum.Parse(typeof(BuildType), value);
        }
        /// <summary>
        /// Returns env value.
        /// </summary>
        public static string ASPNETCORE_ENVIRONMENT => CurrentBuildType().ToString();

        public static bool IsDevelopmentBuild => CurrentBuildType() == BuildType.Development;
        public static bool IsStagingBuild => CurrentBuildType() == BuildType.Staging;
        public static bool IsProductionBuild => CurrentBuildType() == BuildType.Production;
    }
}
