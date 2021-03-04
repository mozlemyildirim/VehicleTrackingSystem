using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeraDAL.DB;
using VeraDAL.Entities;
using VeraFrontSide.Models;

namespace VeraFrontSide.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            LoginViewModel model = new LoginViewModel()
            {
                Question = QuestionDB.GetInstance().GetAllQuestiones()
            };
            return View(model);
        }

        public JsonResult ControlLogin(string _userCode, string _userPassword)
        {
            try
            {
                var user = UserDB.GetInstance().ControlUser(_userCode, _userPassword);

                if (user != null && user.UserTypeId == 1)
                {
                    HttpContext.Session.SetString("FrontSideActiveUser", JsonConvert.SerializeObject(user));
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
            HttpContext.Session.Remove("FrontSideActiveUser");
            return RedirectToAction("Index");
        }
        public JsonResult SendUserPass(string _usernameFP,int _questionFP,string _answerFP)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var userName = _usernameFP.Trim().ToLower();
                    bool isUserExist = UserDB.GetInstance().CheckIfUserExist(userName);
                    User user = null;
                    if(isUserExist == true){
                        user = context.User.FirstOrDefault(x=>x.UserCode.Trim().ToLower()== userName);
                    }
                    if (user != null) {
                        bool isQuestionTrue = UserQuestionAnswerDB.GetInstance().CheckUserQuestionAnswerByUserQuAnsId(user.Id, _questionFP, _answerFP);
                        if (isQuestionTrue == true) {
                            Random rnd = new Random();
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < 3; i++)
                            {
                                int ascii = rnd.Next(97, 121);
                                char karakter = Convert.ToChar(ascii);
                                sb.Append(karakter);
                                
                            }
                            var sayi=rnd.Next(100,999);
                            sb.Append(sayi);
                            var newpass = sb.ToString();
                            user.Password = Convert.ToString(newpass);
                            UserDB.GetInstance().UpdateUser(user,1);
                            return Json(newpass);
                        }
                    }
                    return Json(false);
                   
                }
            }
            catch (Exception)
            {
                throw;
            }

        }





    }
}