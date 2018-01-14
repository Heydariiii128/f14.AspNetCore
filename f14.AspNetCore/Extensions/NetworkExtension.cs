﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace f14.AspNetCore.Extensions
{
    /// <summary>
    /// Provides an extension methods for network.
    /// </summary>
    public static class NetworkExtension
    {
        /// <summary>
        /// Local host ip value.
        /// </summary>
        private const string LocalHostIp = "::1";

        /// <summary>
        /// Determine whether the given ip address is local.
        /// </summary>
        /// <param name="ipAddress">The ip address to check.</param>
        /// <returns>True - if given ip address is local; False - otherwise.</returns>
        public static bool IsLocal(this IPAddress ipAddress)
        {
            ExHelper.NotNull(() => ipAddress);
            return string.Equals(ipAddress.ToString(), LocalHostIp, StringComparison.Ordinal);
        }
    }
}
