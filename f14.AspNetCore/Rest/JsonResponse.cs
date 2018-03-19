using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Rest
{
    /// <summary>
    /// Provides a common json object for http requests.
    /// </summary>
    public class JsonResponse
    {
        /// <summary>
        /// Determines whether error has occurred or not.
        /// </summary>
        public bool? IsError { get; set; }
        /// <summary>
        /// Any response message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The payload object.
        /// </summary>
        public object Payload { get; set; }

        /// <summary>
        /// Creates new instance of json response object.
        /// </summary>
        /// <param name="message">The response message.</param>
        /// <returns>New <see cref="JsonResponse"/>.</returns>
        public static JsonResponse Make(string message) => new JsonResponse { Message = message };
        /// <summary>
        /// Creates new instance of json response object.
        /// </summary>
        /// <param name="payload">The payload object.</param>
        /// <returns>New <see cref="JsonResponse"/>.</returns>
        public static JsonResponse Make(object payload) => new JsonResponse { Payload = payload };
        /// <summary>
        /// Creates new instance of json response object.
        /// </summary>
        /// <param name="message">The response message.</param>
        /// <param name="payload">The payload object.</param>
        /// <returns>New <see cref="JsonResponse"/>.</returns>
        public static JsonResponse Make(string message, object payload) => new JsonResponse { Message = message, Payload = payload };
        /// <summary>
        /// Creates new instance of json response object.
        /// </summary>
        /// <param name="isError">The error flag.</param>
        /// <param name="message">The response message.</param>
        /// <returns>New <see cref="JsonResponse"/>.</returns>
        public static JsonResponse Make(bool isError, string message) => new JsonResponse { IsError = isError, Message = message };
        /// <summary>
        /// Creates new instance of json response object.
        /// </summary>
        /// <param name="isError">The error flag.</param>
        /// <param name="message">The response message.</param>
        /// <param name="payload">The payload object.</param>
        /// <returns>New <see cref="JsonResponse"/>.</returns>
        public static JsonResponse Make(bool isError, string message, object payload) => new JsonResponse { IsError = isError, Message = message, Payload = payload };
    }
}
