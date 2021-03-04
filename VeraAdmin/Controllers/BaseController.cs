using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using VeraDAL.Entities;

namespace VeraAdmin.Controllers
{
	public class BaseController : Controller
	{
		public string userJson = "";
		public User userObj = null;
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (HttpContext.Session.GetString("ActiveUser") == null)
			{
				filterContext.Result = new RedirectResult("/Login");
				return;
			}

			userJson = HttpContext.Session.GetString("ActiveUser");
			userObj = JsonConvert.DeserializeObject<User>(userJson);
			ViewBag.ActiveUser = userObj;

			base.OnActionExecuting(filterContext);
		}
	}
}