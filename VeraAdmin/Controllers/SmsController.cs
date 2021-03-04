using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VeraAdmin.Models;
using VeraDAL.DB;

namespace VeraAdmin.Controllers
{
    public class SmsController : BaseController
    {
        public IActionResult Index()
        {
            var model = new SmsViewModel()
            {
                UserList = UserDB.GetInstance().GetAllUsersWhichHasTelephone(userObj),
                DeviceList = DeviceDB.GetInstance().GetAllDevicesPhoneNumber(userObj)
            };
            return View(model);
        }
        public IActionResult SendSms(string [] _selectedPhoneNumbers,string _message) {
            try
            {
                var smsResult = SmsOperations.SendSms(_selectedPhoneNumbers, _message);
                return smsResult == true ? Json(true) : Json(false); 
            }
            catch (Exception exc)
            {

                throw exc;
            }
             
        }
        public IActionResult GetUserByUserName(string _userNameToSearch) {
            try
            {
                if (_userNameToSearch == null || _userNameToSearch == "")
                    return Json(false);
                var userList = UserDB.GetInstance().GetUserByUserName(_userNameToSearch);
                if (userList != null && userList.Count > 0)
                    return Json(userList);
                return Json(false);
            }
            catch (Exception exc)
            { 

                throw exc;
            }
        }
        public IActionResult GetDeviceByCarPlateNumber(string _deviceCarPlateToSearch) {
            try
            {
                var deviceList = DeviceDB.GetInstance().GetAllDevicesPhoneNumber(userObj, _deviceCarPlateToSearch);
                if (deviceList != null && deviceList.Count > 0)
                    return Json(deviceList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public static string controlgsmno(string gsmno, bool isforeignkey)
        {
            if (!isnumericchar(gsmno))
                return "";

            string cgsm = getcurrentgsm(gsmno);
            if (cgsm.Length < 4)
                return "";

            string sfirstfour = cgsm.Substring(0, 4);
            if (!isforeignkey && (cgsm.Length != 12 || (sfirstfour != "9050" && sfirstfour != "9053" &&
                sfirstfour != "9054" && sfirstfour != "9055")))
            {
                return "";
            }
            else if (isforeignkey && cgsm.Length < 8 && cgsm.Length > 17)
            {
                return "";
            }
            return cgsm;
        } 
        private static bool isnumericchar(string gsmno)
        {
            string strnumbers = "0123456789";
            for (int i = 0; i < gsmno.Length; i++)
            {
                if (strnumbers.IndexOf(gsmno[i]) < 0)
                    return false;
            }
            return true;

        } 
        private static string getcurrentgsm(string sgsm)
        {
            if (sgsm == "" || sgsm.Length < 2)
                return sgsm.Trim();
            if (sgsm.Substring(0, 2) == "90")
                return sgsm.Trim();
            else if (sgsm.Substring(0, 1) == "0")
                return "9" + sgsm.Trim();
            else
                return "90" + sgsm.Trim();
        }
        public IActionResult GetAllUsers()
        {
            try
            {
                var userList = UserDB.GetInstance().GetAllUsersWhichHasTelephone(userObj);
                if (userList != null && userList.Count > 0)
                    return Json(userList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult GetAllDevices() {
            try
            {
                var deviceList = DeviceDB.GetInstance().GetAllDevicesPhoneNumber(userObj);
                if (deviceList != null && deviceList.Count > 0)
                    return Json(deviceList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}