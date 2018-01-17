using f14;
using f14.IO;
using System;
using System.Text;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Provides an extensions methods for Http classes.
    /// </summary>
    public static class HttpExtension
    {
        /// <summary>
        /// Reads http request body stream.
        /// </summary>
        /// <param name="request">Http request.</param>
        /// <returns>The body content string.</returns>
        public static string ReadBody(this HttpRequest request)
        {            
            ExHelper.NotNull(() => request);
            
            string result = FileIO.ReadStream(request.Body, Encoding.UTF8, true, 1024, true);
            return result;
        }
    }
}
