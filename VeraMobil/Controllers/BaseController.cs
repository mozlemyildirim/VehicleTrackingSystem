using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VeraMobil.Controllers
{
    public class BaseController : Controller
    {
        public IHttpContextAccessor _httpContextAccessor;
        public IHostingEnvironment _env;

        public BaseController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env)
        {
            this._httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        public string GetCookie(string key)
        {
            return Request.Cookies[key];
        }

        public void SetCookie(string key, string value, DateTime dt)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = dt;

            Response.Cookies.Append(key, value, option);
        }

        public void RemoveCookie(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}