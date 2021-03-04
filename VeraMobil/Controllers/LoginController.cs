using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeraDAL.DB;

namespace VeraMobil.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env) : base(httpContextAccessor, env)
        {
        }

        public IActionResult Index()
        {
            var cookie = GetCookie("VeraMobilLoggedInUserId");

            if (cookie != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public JsonResult ControlLogin(string _userName, string _password)
        {
            try
            {
                var user = UserDB.GetInstance().ControlUser(_userName, _password);

                if (user != null && user.UserTypeId == 1)
                {
                    SetCookie("VeraMobilLoggedInUserId", user.Id.ToString(), DateTime.Now.AddYears(1));
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public IActionResult Logout()
        {
            RemoveCookie("VeraMobilLoggedInUserId");
            return Redirect("~/Login/Index");
        }
    }
}