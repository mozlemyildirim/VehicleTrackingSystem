using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using VeraDAL.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using VeraDAL.DB;

namespace VeraFrontSide.Controllers
{

    public class BaseController : Controller
    {
        public string userJson = "";
        public User userObj = null;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Session.GetString("FrontSideActiveUser") == null)
            {
                filterContext.Result = new RedirectResult("/Login");
                return;
            }  
            userJson = HttpContext.Session.GetString("FrontSideActiveUser");
            userObj = JsonConvert.DeserializeObject<User>(userJson);
            ViewBag.ActiveUser = userObj; 
            ViewBag.CompanyUser = CompanyUserDB.GetInstance().GetCompanyUserByUserId(userObj.Id);
            
            base.OnActionExecuting(filterContext);
        } 
        protected ICompositeViewEngine viewEngine;
        protected IHostingEnvironment _env;
        
        public BaseController(IHostingEnvironment env, ICompositeViewEngine viewEngine)
        {
            this.viewEngine = viewEngine;
            this._env = env;
        }
        protected string RenderViewAsString(string viewName, object model)
        {
            viewName = viewName ?? ControllerContext.ActionDescriptor.ActionName;
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                IView view = viewEngine.FindView(ControllerContext, viewName, true).View;
                ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, sw, new HtmlHelperOptions());

                view.RenderAsync(viewContext).Wait();

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
