using f14.AspNetCore.Rest;
using f14.EFCore.Abstractions.Reporting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mvc
{
    /// <summary>
    /// Extended controller with logger and other usefull methods.
    /// </summary>
    public class ExtendedController : Controller
    {
        /// <summary>
        /// Returns a controller logger.
        /// </summary>
        protected ILogger Log { get; private set; }
        /// <summary>
        /// Creates new isntance of the controller with logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected ExtendedController(ILogger logger)
        {
            Log = logger;
        }

        #region JsonResponse

        /// <summary>
        /// Creates <see cref="JsonResult"/> using <see cref="JsonResponse"/> as data source with error flag.
        /// </summary>
        /// <param name="msg">The error message.</param>
        /// <returns>The json result.</returns>
        protected JsonResult JsonError(string msg) => Json(JsonResponse.Make(true, msg));
        /// <summary>
        /// Creates <see cref="JsonResult"/> using <see cref="JsonResponse"/> as data source with error flag.
        /// </summary>
        /// <param name="ex">The exception object.</param>
        /// <returns>The json result.</returns>
        protected JsonResult JsonError(Exception ex) => Json(JsonResponse.Make(true, ex.Message));
        /// <summary>
        /// Creates <see cref="JsonResult"/> using <see cref="JsonResponse"/> as data source.
        /// </summary>
        /// <param name="payload">The payload object.</param>
        /// <returns>The json result.</returns>
        protected JsonResult JsonSuccess(object payload) => Json(JsonResponse.Make(payload));
        /// <summary>
        /// Creates <see cref="JsonResult"/> using <see cref="JsonResponse"/> as data source.
        /// </summary>
        /// <param name="msg">The operation message or another description.</param>
        /// <returns>The json result.</returns>
        protected JsonResult JsonSuccess(string msg) => Json(JsonResponse.Make(msg));

        #endregion

        #region OperationReport

        /// <summary>
        /// Writes <see cref="IOperationReport"/> error to the log.
        /// </summary>
        /// <param name="report">Some operation report.</param>
        protected void LogError(IOperationReport report) => Log.LogError(report.Error, report.Message);
        /// <summary>
        /// Writes <see cref="IOperationReport"/> error to the log.
        /// </summary>
        /// <param name="report">Some operation report.</param>
        /// <param name="message">The custom error description.</param>
        protected void LogError(IOperationReport report, string message) => Log.LogError(report.Error, message);
        /// <summary>
        /// Handles operation report and create json result with <see cref="JsonResponse"/> object format.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function which will call if operation report does not have any errors. This function returns payload object.</param>
        /// <returns>The json result.</returns>
        protected JsonResult ProcessReportToJson<T>(T report, Func<T, object> success) where T : IOperationReport => ProcessReportToJson(report, success, r => r.Message);
        /// <summary>
        /// Handles operation report and create json result with <see cref="JsonResponse"/> object format.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function, which will call, if operation report does not have error. This function returns payload object.</param>
        /// <param name="fail">The function, which will call, if operation report contains an error. This function returns message string.</param>
        /// <returns>The json result.</returns>
        protected JsonResult ProcessReportToJson<T>(T report, Func<T, object> success, Func<T, string> fail) where T : IOperationReport
        {
            if (report.IsError)
            {
                LogError(report);
                return JsonError(fail(report));
            }
            else
            {
                return JsonSuccess(success(report));
            }
        }
        /// <summary>
        /// Handles operation report and create json result with <see cref="JsonResponse"/> object format.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function which will call if operation report does not have any errors. This function returns message string.</param>
        /// <returns>The json result.</returns>
        protected JsonResult ProcessReportToJson<T>(T report, Func<T, string> success) where T : IOperationReport => ProcessReportToJson(report, success, r => r.Message);
        /// <summary>
        /// Handles operation report and create json result with <see cref="JsonResponse"/> object format.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function, which will call, if operation report does not have error. This function returns message string.</param>
        /// <param name="fail">The function, which will call, if operation report contains an error. This function returns message string.</param>
        /// <returns>The json result.</returns>
        protected JsonResult ProcessReportToJson<T>(T report, Func<T, string> success, Func<T, string> fail) where T : IOperationReport
        {
            if (report.IsError)
            {
                LogError(report);
                return JsonError(fail(report));
            }
            else
            {
                return JsonSuccess(success(report));
            }
        }
        /// <summary>
        /// Handles operation report and allows to generate a action results.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function, which will call, if operation report does not have error.</param>
        /// <returns>The final action result.</returns>
        protected IActionResult ProcessReportToAction<T>(T report, Func<T, IActionResult> success) where T : IOperationReport
             => ProcessReportToAction(report, success, fail: r => StatusCode(StatusCodes.Status500InternalServerError, report.Error.Message));
        /// <summary>
        /// Handles operation report and allows to generate a action results.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function, which will call, if operation report does not have error.</param>
        /// <param name="fail">The function, which will call, if operation report contains an error.</param>
        /// <returns>The final action result.</returns>
        protected IActionResult ProcessReportToAction<T>(T report, Func<T, IActionResult> success, Func<T, IActionResult> fail) where T : IOperationReport
        {
            if (report.IsError)
            {
                LogError(report);
                return fail(report);
            }
            else
            {
                return success(report);
            }
        }
        /// <summary>
        /// Handles operation report and allows to generate a action results. The operation report error will be added into ModelState errors.
        /// </summary>
        /// <typeparam name="T">The operation report type.</typeparam>
        /// <param name="report">The operation report instance.</param>
        /// <param name="success">The function, which will call, if operation report does not have error.</param>
        /// <param name="fail">The function, which will call, if operation report contains an error.</param>
        /// <returns>The final action result.</returns>
        protected IActionResult ProcessReportToModelState<T>(T report, Func<T, IActionResult> success, Func<T, IActionResult> fail) where T : IOperationReport
        {
            if (report.IsError)
            {
                LogError(report);
                ModelState.AddModelError(string.Empty, report.Error.Message);
            }

            if (ModelState.ErrorCount > 0)
            {
                return fail(report);
            }
            else
            {
                return success(report);
            }
        }

        #endregion
    }
}
