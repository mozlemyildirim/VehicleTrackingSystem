using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeraDAL.DB;

namespace VeraWebService.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string GetDevicesInformation(string _username, string _password)
        {

            try
            {
                var userObj = UserDB.GetInstance().ControlUser(_username, _password);
                //var result = DeviceDB.GetInstance().GetDevicesForFrontSide(userObj);
                var result = DeviceDB.GetInstance().GetDevicesForFrontSide(userObj);

                foreach (var item in result)
                {
                    item.Konum = item.Konum == null ? "" : item.Konum;
                    item.KmPerHour = item.KmPerHour == null ? "0" : item.KmPerHour;
                    item.IoStatus = item.IoStatus != null && item.IoStatus.EndsWith("1") ? "1" : "0";
                    item.ActivityTime = Convert.ToInt32(item.ActivityTime) > (DateTime.Now - Convert.ToDateTime(item.LastDeviceDataCreateDate)).TotalMinutes ? "Aktif" : "Pasif";
                    item.LastLocationTime = item.LastLocationTime == null ? "" : item.LastLocationTime;
                    item.LastDeviceDataCreateDate = item.LastDeviceDataCreateDate.Replace("T", " ").Substring(0, 19);
                    item.Employee = item.Employee == null ? "" : item.Employee;
                    item.VehicleType = item.VehicleType == null ? "0" : item.VehicleType;
                }

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != null ? "Success" : "Error",
                    Message = result != null ? JsonConvert.SerializeObject(result) : "",
                    Status = result != null
                });
            }
            catch (Exception exc)
            {
                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "Error",
                    Message = exc.Message + "," + (exc.InnerException != null ? exc.InnerException.ToString() : ""),
                    Status = false
                });
            }
        }
    }
    public class ApiResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }

}