using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeraDAL.DB;

namespace VeraAdmin.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public JsonResult ControlLogin(string _userCode, string _userPassword)
		{
			try
			{
				var user = UserDB.GetInstance().ControlUser(_userCode, _userPassword);

				if (user != null && user.UserTypeId != 1)
				{
					HttpContext.Session.SetString("ActiveUser", JsonConvert.SerializeObject(user));
					return Json(true);
				}
				return Json(false);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public ActionResult Logout()
		{
			HttpContext.Session.Remove("ActiveUser"); 
			return RedirectToAction("Index");
		}

	}
}