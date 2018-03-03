using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using f14.AspNetCore.Mvc.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ViewRenderSample.Models;

namespace ViewRenderSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RenderView([FromServices] ViewRenderService viewRender)
        {
            ViewData["Type"] = 1;
            string result = await viewRender.RenderAsync("TestView", new TestViewModel { Title = "Test view" }, ViewData);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RenderCustomView([FromServices] IHostingEnvironment env, [FromServices] ViewRenderService viewRender)
        {
            ViewData["Type"] = 2;
            string result = await viewRender.RenderFromFileAsync("~/CustomViews/CustomTestView.cshtml", new TestViewModel { Title = "Test view" }, ViewData);
            return Content(result);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
