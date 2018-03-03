using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mvc.Utils
{
    /// <summary>
    /// Provides methods for render a view to a string.
    /// Main code taken from: <see cref="https://ppolyzos.com/2016/09/09/asp-net-core-render-view-to-string/"/>.
    /// </summary>
    public class ViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Create new instance of the service.
        /// </summary>
        /// <param name="razorViewEngine">The razor view engine.</param>
        /// <param name="tempDataProvider">The data provider.</param>
        /// <param name="serviceProvider">The services.</param>
        public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        #region Absolute path based

        /// <summary>
        /// Renders the view to a string using a absolute view path.
        /// </summary>
        /// <param name="viewPath">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderFromFileAsync(string viewPath, object model) => RenderFromFileAsync(viewPath, model, null);
        /// <summary>
        /// Renders the view to a string using a absolute view path.
        /// </summary>
        /// <param name="viewPath">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderFromFileAsync(string viewPath, object model, IDictionary<string, object> viewData) => RenderFromFileAsync(new DefaultHttpContext { RequestServices = _serviceProvider }, viewPath, model, viewData);
        /// <summary>
        /// Renders the view to a string using a absolute view path.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <param name="viewPath">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderFromFileAsync(HttpContext httpContext, string viewPath, object model, IDictionary<string, object> viewData)
        {
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var viewResult = _razorViewEngine.GetView(null, viewPath, false);
            return RenderToStringAsync(actionContext, viewResult, model, viewData);
        }

        #endregion

        #region ViewLocation path based
        /// <summary>
        /// Renders the view to a string using a view location.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderAsync(string viewName, object model) => RenderAsync(viewName, model, null);
        /// <summary>
        /// Renders the view to a string using a view location.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderAsync(string viewName, object model, IDictionary<string, object> viewData) => RenderAsync(new DefaultHttpContext { RequestServices = _serviceProvider }, viewName, model, viewData);
        /// <summary>
        /// Renders the view to a string using a view location.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderAsync(HttpContext httpContext, string viewName, object model, IDictionary<string, object> viewData)
        {
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
            return RenderToStringAsync(actionContext, viewResult, model, viewData);
        }

        #endregion

        #region Core

        /// <summary>
        /// The core render method. 
        /// </summary>
        /// <param name="actionContext">Action context.</param>
        /// <param name="viewResult">View engine result.</param>
        /// <param name="model"></param>
        /// <param name="viewData"></param>
        /// <returns>The action task.</returns>
        public async Task<string> RenderToStringAsync(ActionContext actionContext, ViewEngineResult viewResult, object model, IDictionary<string, object> viewData)
        {
            using (var sw = new StringWriter())
            {
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewResult.ViewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                if (viewData != null && viewData.Count > 0)
                {
                    foreach (var kv in viewData)
                    {
                        viewDictionary[kv.Key] = kv.Value;
                    }
                }

                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        #endregion
    }
}
