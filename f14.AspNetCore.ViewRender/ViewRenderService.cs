using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace f14.AspNetCore.Mvc
{
    /// <summary>
    /// Provides methods for render a view to a string.
    /// Main code taken from: <see cref="https://ppolyzos.com/2016/09/09/asp-net-core-render-view-to-string/"/>.
    /// </summary>
    public sealed class ViewRenderService
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

        #region Public API

        /// <summary>
        /// Renders the view to a string using a view location.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="fromCustomPath">Determines whether the service should search views in non default locations.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderAsync(string viewName, object model, bool fromCustomPath = false) => RenderAsync(viewName, model, null, fromCustomPath);
        /// <summary>
        /// Renders the view to a string using a view location.
        /// </summary>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="viewData">The view data.</param>
        /// <param name="fromCustomPath">Determines whether the service should search views in non default locations.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderAsync(string viewName, object model, IDictionary<string, object> viewData, bool fromCustomPath = false)
        {
            HttpContext httpContext;
            var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor != null)
            {
                httpContext = httpContextAccessor.HttpContext;
            }
            else
            {
                httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            }
            return RenderAsync(httpContext, viewName, model, viewData, fromCustomPath);
        }
        /// <summary>
        /// Renders the view to a string using a view location.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The view model.</param>
        /// <param name="viewData">The view data.</param>
        /// <param name="fromCustomPath">Determines whether the service should search views in non default locations.</param>
        /// <returns>The action task.</returns>
        public Task<string> RenderAsync(HttpContext httpContext, string viewName, object model, IDictionary<string, object> viewData, bool fromCustomPath)
        {
            var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor());
            var viewResult = fromCustomPath
                ? _razorViewEngine.GetView(null, viewName, false)
                : _razorViewEngine.FindView(actionContext, viewName, false);
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
        private async Task<string> RenderToStringAsync(ActionContext actionContext, ViewEngineResult viewResult, object model, IDictionary<string, object> viewData)
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
