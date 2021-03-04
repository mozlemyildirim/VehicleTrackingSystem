using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using VeraDAL.DB;
using VeraDAL.Entities;
using VeraDAL.Models;
using VeraFrontSide.Models;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Hosting;
//using SelectPdf;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Cryptography;

namespace VeraFrontSide.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHostingEnvironment env, ICompositeViewEngine viewEngine) : base(env, viewEngine) { }
        public IActionResult Index()
        {
            int myCompanyId = UserDB.GetInstance().GetCompanyIdByUserObj(userObj);
            HomeViewModel model = new HomeViewModel()
            {
                Groups = GroupDB.GetInstance().GetAllCompanyGroupsByUserId(userObj),
                GroupDevicesToLeft = GroupDeviceDB.GetInstance().GetAllGroupDevicesByUserIdToLeft(userObj),
                Company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj),
                UserTypes = UserTypeDB.GetInstance().GetAllUserTypes(),
                GeographicalAuthorities = GeographicalAuthorityDB.GetInstance().GetAllGeographicalAuthoritys(),
                User = UserDB.GetInstance().GetUserByUserObj(userObj),
                UserList = CompanyUserDB.GetInstance().GetUsersByCompanyId(myCompanyId),
                UsingStatuses = UsingStatusDB.GetInstance().GetAllUsingStatuses(),
                Question = QuestionDB.GetInstance().GetAllQuestiones(),
                Areas = AreaDB.GetInstance().GetAreaList(userObj),
                Vehicle = VehicleDB.GetInstance().GetAllVehicles(),
                UsingStatus = UsingStatusDB.GetInstance().GetAllUsingStatuses(),
                Settings = SettingsDB.GetInstance().GetSettingsById(1),
                //Route = RouteDB.GetInstance().GetAllRoutees()
            };
            return View(model);
        }
        public IActionResult SetAllReports()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var allUsers = context.User.ToList();
                    var allReports = context.Report.ToList();
                    var listOfUserReports = new List<UserReport>();
                    foreach (var item in allUsers)
                    {
                        foreach (var report in allReports)
                        {
                            listOfUserReports.Add(new UserReport()
                            {
                                ReportId = report.Id,
                                UserId = item.Id,
                                UserReportType = 2,
                                AutomaticReportingStatus = 0,
                                AutomaticReportingVehicles = null
                            });
                        }
                    }
                    context.UserReport.AddRange(listOfUserReports);
                    context.SaveChanges();
                    return View();
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetUserListToAppend(int _companyId)
        {
            try
            {
                var userList = CompanyUserDB.GetInstance().GetUserListToAppend(_companyId);
                if (userList != null && userList.Count > 0)
                    return Json(userList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult FillUserInformationFields()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var user = context.User.FirstOrDefault(x => x.Id == userObj.Id);
                    return Json(user);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult FillDeviceInformationFields(int _fillDeviceLast)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var device = context.Device.FirstOrDefault(x => x.Id == _fillDeviceLast);
                    return Json(device);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult AddNewCompanyGroup(string _groupName)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    Group group = new Group();
                    group.Name = _groupName;
                    group.Status = 1;
                    GroupDB.GetInstance().AddNewGroup(group);
                    CompanyGroup cGroup = new CompanyGroup();
                    cGroup.GroupId = group.Id;
                    cGroup.CompanyId = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id).CompanyId;
                    cGroup.Status = 1;
                    CompanyGroupDB.GetInstance().AddNewCompanyGroup(cGroup);
                    return Json(true);

                }

            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
        }
        public JsonResult UpdateGroup(int _id, string _groupNameToUpdate)
        {
            try
            {
                var group = GroupDB.GetInstance().GetGroupById(_id);
                if (group.Name != _groupNameToUpdate)
                {
                    group.Name = _groupNameToUpdate;
                    GroupDB.GetInstance().UpdateGroup(group);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult DeleteGroup(int _id)
        {
            try
            {
                var result = GroupDB.GetInstance().DeleteGroup(_id);
                if (result == true)
                {
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult DeleteAracAlarm(int _id)
        {
            try
            {
                var result = AlarmDB.GetInstance().DeleteAlarm(_id);
                var result2 = UserAlarmDB.GetInstance().DeleteUserAlarm(_id);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetDeviceIdsByGroupId(int _groupId)
        {
            try
            {
                if (_groupId == 0)
                    return Json(new List<GroupDevice>());

                var result = GroupDeviceDB.GetInstance().GetGroupDeviceIdsByGroupId(_groupId);
                return Json(result);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult GetDeviceIdsByUsername(int _usercode)
        {
            try
            {
                if (_usercode == 0)
                    return Json(new List<Device>());
                var result = DeviceDB.GetInstance().GetDeviceById(_usercode);
                return Json(result);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult SaveGroupDevice(int _groupId, List<int> _selectedDeviceIds)
        {
            try
            {
                var deleteResult = GroupDeviceDB.GetInstance().DeleteAllGroupDeviceByGroupId(_groupId);


                foreach (var deviceId in _selectedDeviceIds)
                {
                    var groupDevice = new GroupDevice()
                    {
                        DeviceId = deviceId,
                        GroupId = _groupId,
                        Status = 1
                    };

                    var insertResult = GroupDeviceDB.GetInstance().AddNewGroupDevice(groupDevice);

                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);



            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult GetGroupsWithVehicles(string _groupNameToFilter, string _carPlateNumberToFilter)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var groupDevices = new List<GroupDevice>();
                    var group = new List<Group>();
                    var groupsWithCarPlates = new List<GroupDeviceRepo>();

                    var myCompanyId = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id).CompanyId;
                    var myCompanyGroups = context.CompanyGroup.Where(x => x.CompanyId == myCompanyId).ToList();
                    var myCompanyGroupGroupIds = myCompanyGroups.Select(x => x.GroupId).ToList();

                    group = context.Group.Where(x => myCompanyGroupGroupIds.Contains(x.Id)).ToList();

                    if (String.IsNullOrEmpty(_groupNameToFilter) && String.IsNullOrEmpty(_carPlateNumberToFilter))
                    {
                        var groupIds = group.Select(x => x.Id).ToList();
                        groupDevices = context.GroupDevice.Where(x => groupIds.Contains(x.GroupId)).ToList();
                        var groupDeviceDeviceIds = groupDevices.Select(x => x.DeviceId).ToList();
                        var allDevices = DeviceDB.GetInstance().GetAllDBDevices();
                        allDevices = allDevices.Where(x => groupDeviceDeviceIds.Contains(x.Id)).ToList();

                        foreach (var item in groupDevices)
                        {
                            var carPlateNumber = allDevices.FirstOrDefault(x => x.Id == item.DeviceId).CarPlateNumber;
                            var groupName = group.FirstOrDefault(x => x.Id == item.GroupId).Name;
                            groupsWithCarPlates.Add(new GroupDeviceRepo
                            {
                                CarPlateNumber = carPlateNumber,
                                GroupName = groupName,
                                DeviceId = item.DeviceId,
                                GroupId = item.GroupId,
                                Id = item.Id,
                                Status = item.Status
                            });
                        }

                        return Json(groupsWithCarPlates);


                    }
                    if (!String.IsNullOrEmpty(_groupNameToFilter) || !String.IsNullOrEmpty(_carPlateNumberToFilter))
                    {

                        if (!String.IsNullOrEmpty(_groupNameToFilter))
                        {
                            group = GroupDB.GetInstance().GetGroupsByGroupName(userObj, _groupNameToFilter);
                            if (group == null)
                            {
                                return Json(false);
                            }
                        }

                        var groupIds = group.Select(x => x.Id).ToList();
                        groupDevices = context.GroupDevice.Where(x => groupIds.Contains(x.GroupId)).ToList();
                        var groupDeviceDeviceIds = groupDevices.Select(x => x.DeviceId).ToList();
                        var allDevices = DeviceDB.GetInstance().GetAllDBDevices();
                        allDevices = allDevices.Where(x => groupDeviceDeviceIds.Contains(x.Id)).ToList();

                        if (!String.IsNullOrEmpty(_carPlateNumberToFilter))
                        {
                            allDevices = allDevices.Where(x => x.CarPlateNumber.StartsWith(_carPlateNumberToFilter) || x.CarPlateNumber.Contains(_carPlateNumberToFilter)).ToList();
                            if (allDevices == null)
                            {
                                return Json(false);
                            }
                        }

                        foreach (var item in groupDevices)
                        {
                            var carPlateNumber = allDevices.FirstOrDefault(x => x.Id == item.DeviceId).CarPlateNumber;
                            var groupName = group.FirstOrDefault(x => x.Id == item.GroupId).Name;
                            groupsWithCarPlates.Add(new GroupDeviceRepo
                            {
                                CarPlateNumber = carPlateNumber,
                                GroupName = groupName,
                                DeviceId = item.DeviceId,
                                GroupId = item.GroupId,
                                Id = item.Id,
                                Status = item.Status
                            });
                        }
                        return Json(groupsWithCarPlates);
                    }
                }
                return Json(false);

            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public JsonResult SaveUser(int _userIdTAM, string _usercodeTAM, string _passwordTAM, string _passwordControlTAM, string _usernameTAM, string _userSurnameTAM,
            int _userAuthorityTAM, int _userGeographicalAuthorityIdTAM, int _userAuthorityIdTAM, string _userEmailTAM)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    if (_userIdTAM == 0)
                    {
                        User user = new User()
                        {
                            Mail = _userEmailTAM,
                            Name = _usernameTAM,
                            Password = _passwordTAM,
                            Status = 1,
                            Surname = _usernameTAM,
                            UserCode = _usercodeTAM,
                            UserTypeId = 1,
                            GeographicalAuthorityId = _userGeographicalAuthorityIdTAM == 0 ? 1 : _userGeographicalAuthorityIdTAM,
                            HomepageRefreshTime = 60000
                        };
                        UserDB.GetInstance().AddNewUser(user);
                        var myCompanyId = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id).CompanyId;
                        CompanyUser compUser = new CompanyUser()
                        {
                            CompanyId = myCompanyId,
                            Status = 1,
                            UserId = user.Id,
                            IsCompanyAdmin = _userAuthorityIdTAM == 1 ? true : false,
                        };
                        var result = CompanyUserDB.GetInstance().AddNewCompanyUser(compUser);
                        return Json(result != null);
                    }
                    else if (_userIdTAM != 0)
                    {
                        var userToUpdate = UserDB.GetInstance().GetUserById(_userIdTAM);
                        var isPasswordGoingToBeDifferent = 0;
                        if (userToUpdate.Password.Trim().ToLower() == _passwordTAM.Trim().ToLower())
                            isPasswordGoingToBeDifferent = 0;
                        if (userToUpdate.Password.Trim().ToLower() != _passwordTAM.Trim().ToLower())
                        {
                            userToUpdate.Password = _passwordTAM;
                            isPasswordGoingToBeDifferent = 1;
                        }
                        userToUpdate.Id = _userIdTAM;
                        userToUpdate.GeographicalAuthorityId = _userGeographicalAuthorityIdTAM == 0 ? 1 : _userGeographicalAuthorityIdTAM;
                        userToUpdate.Mail = _userEmailTAM;
                        userToUpdate.Name = _usernameTAM;

                        userToUpdate.Status = 1;
                        userToUpdate.Surname = _userSurnameTAM;
                        userToUpdate.UserCode = _usercodeTAM;
                        userToUpdate.UserTypeId = 1;
                        var result = UserDB.GetInstance().UpdateUser(userToUpdate, isPasswordGoingToBeDifferent);
                        return Json(result != null);
                    }
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult CheckIfUserExist(string _userCode)
        {
            try
            {
                bool isUserExit = UserDB.GetInstance().CheckIfUserExist(_userCode);
                return Json(isUserExit);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GetUserList(string _userCodeTF, string _userNameTF, string _userSurnameTF, int _userAuthorityTF)
        {
            try
            {
                _userCodeTF = _userCodeTF == null ? "" : _userCodeTF;
                _userNameTF = _userNameTF == null ? "" : _userNameTF;
                _userSurnameTF = _userSurnameTF == null ? "" : _userSurnameTF;

                var userList = UserDB.GetInstance().GetAllUserList(userObj, _userCodeTF, _userNameTF, _userSurnameTF, _userAuthorityTF);
                return PartialView("_CompanyUserList", userList);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetUserIdForDisplaying(int _id)
        {
            try
            {
                var user = UserDB.GetInstance().GetUserByIdUserRepo(_id);
                return Json(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult DeleteUser(int _id)
        {
            try
            {
                var result = UserDB.GetInstance().DeleteUser(_id);
                return Json(true);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public JsonResult GetPastLocationList(string _startDateTLPL, string _endDateTLPL, int _lastIdTLPL, int _deviceIdTLPL)
        {
            try
            {
                _startDateTLPL = _startDateTLPL == null ? "" : _startDateTLPL;
                _endDateTLPL = _endDateTLPL == null ? "" : _endDateTLPL;
                var pastLocationList = DeviceDB.GetInstance().GetPastLocationList(_startDateTLPL, _endDateTLPL, _lastIdTLPL, _deviceIdTLPL);
                if (pastLocationList == null)
                {
                    return Json(null);
                }
                var latLonList = pastLocationList.Select(x => x.Enlem + "|" + x.Boylam).ToList().Distinct().ToList();

                foreach (var item in latLonList)
                {
                    var lat = item.Split('|')[0];
                    var lon = item.Split('|')[1];

                    //var konum = DeviceDB.GetInstance().GetLocationFromLatLon2(lat.Trim(), lon.Trim());

                    var items = pastLocationList.Where(x => x.Enlem == lat && x.Boylam == lon).ToList();

                    foreach (var item2 in items)
                    {
                        item2.Zaman = item2.Zaman.Replace("T", " ");
                    }
                }

                return Json(pastLocationList);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public string GetLocationFromLatLon2(string lat, string lon)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var latD = Convert.ToDecimal(lat.Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR"));
                    var lonD = Convert.ToDecimal(lon.Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR"));

                    var konum = context.LatLonLocation.FirstOrDefault(x => x.Latitude == latD && x.Longtitude == lonD);

                    return konum != null ? konum.Location : "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetDevicesForFrontSide()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var deviceList = DeviceDB.GetInstance().GetDevicesForFrontSide(userObj);

                    if (deviceList != null)
                    {
                        foreach (var item in deviceList)
                        {
                            //item.Konum = GetLocationFromLatLon2(item.Latitude.ToString(), item.Longtitude.ToString()).ToString();
                            item.KmPerHour = item.KmPerHour == null ? "0" : item.KmPerHour;
                            item.IoStatus = item.IoStatus != null && item.IoStatus.EndsWith("1") ? "1" : "0";
                            item.ActivityTime = Convert.ToInt32(item.ActivityTime) > (DateTime.Now - Convert.ToDateTime(item.LastDeviceDataCreateDate)).TotalMinutes ? "Aktif" : "Pasif";
                            //item.LastLocationTime = ReportDB.GetInstance().TotalHoursToHumanReadableString(((DateTime.Now.AddMinutes(3)) - Convert.ToDateTime(item.LastLocationTime)).TotalHours.ToString()).Replace("Saat","s").Replace("Dakika","dk").Replace("Saniye","sn");
                            item.LastLocationTime = item.LastLocationTime == null ? "" : item.LastLocationTime;
                            item.LastDeviceDataCreateDate = item.LastDeviceDataCreateDate.Replace("T", " ").Substring(0, 19);
                            item.Employee = item.Employee == null ? "" : item.Employee;
                            item.VehicleType = item.VehicleType == null || item.VehicleType == "0" ? "0" : item.VehicleType;
                            item.TotalKmDaily = item.TotalKmDaily == null ? "0" : item.TotalKmDaily;
                        }
                        var locAndDevices = new List<LocationAndDevice>();
                        var sameDeviceIds = new List<int>();

                        foreach (var item in deviceList)
                        {
                            if (deviceList.Where(x => x.LocationId == item.LocationId).ToList().Count > 1)
                                locAndDevices.Add(new LocationAndDevice(item.LocationId, item.LastDeviceId));
                        }
                        var differentLocIds = locAndDevices.Select(x => x.LocationId).ToList();
                        //differentLocIds = deviceList.Where(x => !differentLocIds.Contains(x.LocationId)).Select(x=>x.LocationId).ToList();
                        //deviceList = deviceList.Where(x => differentLocIds.Contains(x.LocationId) && x.LocationId != 0).ToList();
                        differentLocIds = differentLocIds.OrderBy(x => x).ToList();
                        var decimalNum = (decimal)0.0015; 
                        for (int i = 0; i < differentLocIds.Count; i++)
                        {
                            var item = differentLocIds[i];
                            if (i > 0 && i + 1 < differentLocIds.Count)
                            {
                                var before = differentLocIds[i - 1];
                                var after = differentLocIds[i + 1];
                                if (item != after)
                                {
                                    decimalNum = (decimal)0.000015;
                                }
                            }
                            //Random rnd = new Random();
                            //var randomDec = (decimal)GetRandomNumber(0.000015, 0.00010);
                            var device = deviceList.FirstOrDefault(x => x.LocationId == item);
                            deviceList.Remove(device);
                            //device.Latitude += decimalNum;
                            device.Longtitude += decimalNum;
                            deviceList.Add(device);
                            decimalNum += 0.00003m;
                        }
                        ////foreach (var item in deviceList)
                        //{
                        //    if (latLongList.Where(x => x.Latitude == item.Latitude && x.Longtitude == item.Longtitude).ToList().Count == 0)
                        //        latLongList.Add(new LatLong(item.Latitude, item.Longtitude)); 
                        //        //foreach (var item2 in latLongList)
                        //        //{
                        //        //    if (item.Latitude == item2.Latitude && item.Longtitude == item2.Longtitude)
                        //        //        sameDeviceIds.Add(item.LastDeviceId);
                        //        //} 
                        //} 

                        //foreach (var item in deviceList)
                        //{
                        //    var latLong = new LatLong(item.Latitude, item.Longtitude);

                        //    foreach (var item2 in latLongList)
                        //    {
                        //        if (item.Latitude != item2.Latitude && item.Longtitude != item2.Longtitude)
                        //            sameDeviceIds.Add(item.LastDeviceId);
                        //    }
                        //}
                        //sameDeviceIds = sameDeviceIds.Select(x => x).Distinct().ToList();
                        //if (sameDeviceIds.Count > 0)
                        //{
                        //    foreach (var item in deviceList.Where(x => sameDeviceIds.Contains(x.LastDeviceId)).ToList())
                        //    {
                        //        item.Latitude += 0.10m;
                        //        item.Longtitude += 0.10m;
                        //    }
                        //}

                        return Json(deviceList.OrderBy(x=>x.CarPlateNumber));
                    }
                    return Json(false);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public JsonResult GetLastLocationTimeOfDevicesWithGroup(int _groupIdTLPLT)
        {
            try
            {
                var deviceInfoList = DeviceDB.GetInstance().GetLastLocationTimeOfDevicesWithGroup(_groupIdTLPLT, userObj);
                if (deviceInfoList != null)
                {
                    foreach (var item in deviceInfoList)
                    {
                        //item.LastLocationTime = ReportDB.GetInstance().TotalHoursToHumanReadableString((DateTime.Now - Convert.ToDateTime(item.LastLocationTime)).TotalHours.ToString());
                        item.LastLocationTime = Convert.ToDateTime(item.LastLocationTime).ToString("yyyy-MM-dd HH:mm:ss").Replace("T", "");
                    }
                    return Json(deviceInfoList);
                }

                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetDevicesInformationForFrontSide(int _lastDeviceId)
        {
            try
            {
                using (var context = new VeraEntities())
                {

                    var deviceList = DeviceDB.GetInstance().GetDevicesInformationForFrontSide(userObj, _lastDeviceId);
                    if (deviceList != null)
                    {
                        foreach (var item in deviceList)
                        {
                            item.ActivityTime = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item.LastDeviceDataCreateDate)).TotalMinutes) > Convert.ToInt32(item.ActivityTime) ? "Kapali" : "Acik";
                            item.Konum = item.Konum == null ? "" : item.Konum;
                            item.IoStatus = item.IoStatus == null || item.IoStatus.EndsWith("1") ? "1" : "0";
                            item.LastLocationTime = item.LastLocationTime == null || item.LastLocationTime == "" ? "" : item.LastLocationTime.Replace("T", " ").Replace(":", ".").Substring(0, 19);
                            //item.Latitude = item.Latitude == null ? "" : item.Latitude;
                            //item.Longtitude = item.Longtitude == null ? "" : item.Longtitude;
                            item.CarPlateNumber = item.CarPlateNumber == null ? "" : item.CarPlateNumber;
                            //item.DirectionDegree = item.DirectionDegree == null ? "" : item.DirectionDegree;
                            item.Mail = item.Mail == null ? "" : item.Mail;
                        }
                        return Json(deviceList);
                    }
                    return Json(false);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public JsonResult GetDevicesByCompany(int _aracAtamaCompanyId)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    //var companies = context.Company.Where(x => x.MainCompanyId == _aracAtamaCompanyId || x.Id == _aracAtamaCompanyId).ToList();
                    //var companyIds = companies.Select(x => x.Id).ToList();
                    //var companyDevices = context.Device.Where(x => companyIds.Contains(x.CompanyId) || x.CompanyId == _aracAtamaCompanyId).ToList();
                    //var companyDevicesIds = companyDevices.Select(x => x.DeviceId).ToList();
                    //var device = DeviceDB.GetInstance().GetAllDBDevices();
                    //var devices = device.Where(x => companyDevicesIds.Contains(x.DeviceId) && x.LastDeviceDataId != null).ToList();
                    //return Json(devices);
                    var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                    var subCompaniesAndOwnCompanyCompanyIds = context.Company.Where(x => x.MainCompanyId == company.Id).Select(x => x.Id).ToList();
                    subCompaniesAndOwnCompanyCompanyIds.Add(company.Id);
                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    var devices = DeviceDB.GetInstance().GetAllDBDevices().Where(x => x.LastDeviceDataId != null).ToList();
                    var assignedDevicesDeviceIds = context.UserDevice.Where(x => x.UserId == userObj.Id).Select(x => x.DeviceId).ToList();
                    devices = devices.Where(x => assignedDevicesDeviceIds.Contains(x.Id)).ToList();
                    if (devices != null && devices.Count > 0)
                        return Json(devices);
                    return Json(false);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetDevicesByCompany2(int _aracAtamaCompanyId)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var companies = context.Company.Where(x => x.MainCompanyId == _aracAtamaCompanyId || x.Id == _aracAtamaCompanyId).ToList();
                    var companyIds = companies.Select(x => x.Id).ToList();
                    var companyDevices = context.Device.Where(x => companyIds.Contains(x.CompanyId) || x.CompanyId == _aracAtamaCompanyId).ToList();
                    var companyDevicesIds = companyDevices.Select(x => x.DeviceId).ToList();
                    var device = DeviceDB.GetInstance().GetAllDBDevices();
                    var devices = device.Where(x => companyDevicesIds.Contains(x.DeviceId) && x.LastDeviceDataId != null).ToList();
                    return Json(devices);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetDevicesByCompanyForAR(int _reportId)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var userReport = context.UserReport.FirstOrDefault(x => x.Id == _reportId && x.UserId == userObj.Id);
                    var vehicleIds = userReport.AutomaticReportingVehicles == null ? "" : userReport.AutomaticReportingVehicles;

                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    var devices = DeviceDB.GetInstance().GetAllDBDevices();
                    if (companyUser.IsCompanyAdmin)
                    {
                        var companies = context.Company.Where(x => x.MainCompanyId == companyUser.CompanyId).ToList();
                        var companyIds = companies.Select(x => x.Id).ToList();
                        var companyDevices = devices.Where(x => companyIds.Contains(x.CompanyId) || x.CompanyId == companyUser.CompanyId).ToList();

                        var companyDevicesIds = companyDevices.Select(x => x.Id).ToList();
                        devices = devices.Where(x => companyDevicesIds.Contains(x.Id) && !vehicleIds.Contains(x.Id.ToString()) && x.LastDeviceDataId != null).ToList();
                        return Json(devices);
                    }
                    else
                    {
                        var userDeviceIds = context.UserDevice.Where(x => x.UserId == userObj.Id).Select(x => x.DeviceId).ToList();
                        devices = context.Device.Where(x => userDeviceIds.Contains(x.Id)).ToList();
                        devices = devices.Where(x => !vehicleIds.Contains(x.Id.ToString()) && x.LastDeviceDataId != null).ToList();
                        return Json(devices);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetDevicesByCompanyForRightAR(int _reportId)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var userReport = context.UserReport.FirstOrDefault(x => x.Id == _reportId && x.UserId == userObj.Id);
                    var vehicleIds = userReport.AutomaticReportingVehicles == null ? "" : userReport.AutomaticReportingVehicles;
                    var devices = DeviceDB.GetInstance().GetAllDBDevices();
                    var intVehicleIds = new List<int>();
                    var vehicleIdsSplitted = new List<string>();
                    if (vehicleIds.Length > 0)
                        vehicleIdsSplitted = vehicleIds.Split(",").ToList();

                    if (vehicleIdsSplitted.Count > 0)
                    {
                        for (int i = 0; i < vehicleIdsSplitted.Count; i++)
                        {
                            intVehicleIds.Add(Convert.ToInt32(vehicleIdsSplitted[i]));
                        }

                    }
                    devices = devices.Where(x => intVehicleIds.Contains(x.Id) && x.Status == 1).ToList();
                    return Json(devices);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetAllReports()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var reports = ReportDB.GetInstance().GetAllReportes().ToList();
                    return Json(reports);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetAllRoutes()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var routes = RouteDB.GetInstance().GetAllRoutees().ToList();
                    return Json(routes);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetPercentage(int _deviceId,int _type)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var percentages = DeviceDataActionDB.GetInstance().GetPercentage(_deviceId,_type);
                    return Json(percentages);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetAllRoutesByUser()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var routes = RouteDB.GetInstance().GetRouteByUserId(userObj.Id).ToList();
                    return Json(routes);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetAllCompanyArea()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var user = UserDB.GetInstance().GetUserByUserObj(userObj);
                    var companyId = context.CompanyUser.FirstOrDefault(x => x.UserId == user.Id).CompanyId.ToString();
                    var areaIds = context.CompanyArea.Where(x => x.CompanyId == Convert.ToInt32(companyId)).Select(x => x.AreaId).ToList();
                    var Areas = context.Area.Where(x => areaIds.Contains(x.Id) && x.Status == 1).ToList();
                    return Json(Areas);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetAllCompanyRoute()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var user = UserDB.GetInstance().GetUserByUserObj(userObj);
                    var companyId = context.CompanyUser.FirstOrDefault(x => x.UserId == user.Id).CompanyId.ToString();
                    var routeIds = context.CompanyRoute.Where(x => x.CompanyId == Convert.ToInt32(companyId)).Select(x => x.RouteId).ToList();
                    var Routes = context.Route.Where(x => routeIds.Contains(x.Id) && x.Status == 1).ToList();
                    return Json(Routes);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetAllReportsByUser()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var userReport = UserReportDB.GetInstance().GetUserReportByUser(userObj);
                    return Json(userReport);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GroupDevicesToLeft()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    //var devices = GroupDeviceDB.GetInstance().GetAllGroupDevicesByUserIdToLeft(userObj);
                    var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                    var subCompaniesAndOwnCompanyCompanyIds = context.Company.Where(x => x.MainCompanyId == company.Id).Select(x => x.Id).ToList();
                    subCompaniesAndOwnCompanyCompanyIds.Add(company.Id);
                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    var devices = DeviceDB.GetInstance().GetAllDBDevices().Where(x => x.LastDeviceDataId != null).ToList();
                    //if (companyUser.IsCompanyAdmin)
                    //    devices = devices.Where(x => subCompaniesAndOwnCompanyCompanyIds.Contains(x.CompanyId)).ToList();
                    //if (!companyUser.IsCompanyAdmin)
                    //{
                    var assignedDevicesDeviceIds = context.UserDevice.Where(x => x.UserId == userObj.Id).Select(x => x.DeviceId).ToList();
                    devices = devices.Where(x => assignedDevicesDeviceIds.Contains(x.Id)).ToList();
                    return Json(devices);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetDevicesByGroupId(int _groupIdToSearchDevice, int _vehicleFiltering)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var deviceReturnList = new List<DeviceObjectForFrontSide>();
                    var deviceIdsReturnList = new List<int>();
                    var groupDevicesDeviceIds = context.GroupDevice.Where(x => x.GroupId == _groupIdToSearchDevice).Select(x => x.DeviceId).ToList();
                    if (groupDevicesDeviceIds == null || groupDevicesDeviceIds.Count == 0)
                    {
                        return Json(false);

                    }
                    //_vehicleFiltering =1 ? Aktif
                    //_vehicleFiltering =2 ? Pasif
                    //_vehicleFiltering =3 ? SinyalYok 
                    List<Device> devices = null;
                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    List<int> userDevicesDeviceIds = null;

                    devices = context.Device.Where(x => groupDevicesDeviceIds.Contains(x.Id)).ToList();
                    if (companyUser.IsCompanyAdmin == false)
                    {
                        userDevicesDeviceIds = context.UserDevice.Where(x => x.UserId == userObj.Id).Select(x => x.DeviceId).ToList();
                        if (userDevicesDeviceIds != null && userDevicesDeviceIds.Count > 0)
                            devices = devices.Where(x => userDevicesDeviceIds.Contains(x.Id)).ToList();
                        if (userDevicesDeviceIds == null || userDevicesDeviceIds.Count == 0)
                            return Json(false);
                    }
                    var deviceIds = devices.Select(x => x.Id).ToList();
                    if (deviceIds != null && deviceIds.Count > 0)
                    {
                        foreach (var item in devices)
                        {
                            if (_vehicleFiltering == 0)
                            {
                                //var lastDeviceData = context.DeviceData.LastOrDefault(x => x.Id == item.LastDeviceDataId);
                                deviceReturnList.Add(new DeviceObjectForFrontSide()
                                {
                                    //CarPlateNumber = item.CarPlateNumber,
                                    //IoStatus = lastDeviceData == null ? "" : lastDeviceData.IoStatus,
                                    //KmPerHour = lastDeviceData == null ? "" : Convert.ToString(lastDeviceData.KmPerHour),
                                    LastDeviceId = /*lastDeviceData == null ? 0 :*/ item.Id,
                                    //LastLocationTime = lastDeviceData == null ? "" : lastDeviceData.CreateDate.ToString().Replace("T", " ").Replace("-", "."),
                                    //Konum = lastDeviceData == null ? "" : DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.Latitude.ToString(), lastDeviceData.Longtitude.ToString()),
                                    //ActivityTime = item.ActivityTime > (DateTime.Now - lastDeviceData.CreateDate).TotalMinutes ? "Aktif" : "SinyalYok"
                                });
                            }

                            if (_vehicleFiltering == 1)
                            {
                                if (item.LastDeviceDataId == null)
                                {
                                    continue;
                                }
                                var lastDeviceData = context.DeviceData.LastOrDefault(x => x.Id == item.LastDeviceDataId);
                                if (item.ActivityTime > (DateTime.Now - lastDeviceData.CreateDate).TotalMinutes)
                                {
                                    if (lastDeviceData.IoStatus.EndsWith("1"))
                                    {
                                        deviceReturnList.Add(new DeviceObjectForFrontSide()
                                        {
                                            //CarPlateNumber = item.CarPlateNumber,
                                            //IoStatus = lastDeviceData == null ? "" : lastDeviceData.IoStatus,
                                            //KmPerHour = lastDeviceData == null ? "" : Convert.ToString(lastDeviceData.KmPerHour),
                                            LastDeviceId = lastDeviceData == null ? 0 : item.Id,
                                            //LastLocationTime = lastDeviceData == null ? "" : lastDeviceData.CreateDate.ToString().Replace("T", " ").Replace("-", "."),
                                            //Konum = lastDeviceData == null ? "" : DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.Latitude.ToString(), lastDeviceData.Longtitude.ToString()),
                                            //ActivityTime = "Aktif"
                                        });
                                    }
                                }
                            }
                            else if (_vehicleFiltering == 2)
                            {
                                var lastDeviceData = context.DeviceData.LastOrDefault(x => x.Id == item.LastDeviceDataId);
                                if (item.ActivityTime > (DateTime.Now - lastDeviceData.CreateDate).TotalMinutes)
                                {
                                    if (lastDeviceData.IoStatus.EndsWith("0"))
                                    {
                                        deviceReturnList.Add(new DeviceObjectForFrontSide()
                                        {
                                            //CarPlateNumber = item.CarPlateNumber,
                                            //IoStatus = lastDeviceData == null ? "" : lastDeviceData.IoStatus,
                                            //KmPerHour = lastDeviceData == null ? "" : Convert.ToString(lastDeviceData.KmPerHour),
                                            LastDeviceId = lastDeviceData == null ? 0 : item.Id,
                                            //LastLocationTime = lastDeviceData == null ? "" : lastDeviceData.CreateDate.ToString().Replace("T", " ").Replace("-", "."),
                                            //Konum = lastDeviceData == null ? "" : DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.Latitude.ToString(), lastDeviceData.Longtitude.ToString()),
                                            //ActivityTime = "Aktif"
                                        });
                                    }
                                }
                            }
                            else if (_vehicleFiltering == 3)
                            {
                                var lastDeviceData = context.DeviceData.LastOrDefault(x => x.Id == item.LastDeviceDataId);
                                if (item.ActivityTime < (DateTime.Now - lastDeviceData.CreateDate).TotalMinutes)
                                {
                                    deviceReturnList.Add(new DeviceObjectForFrontSide()
                                    {
                                        //CarPlateNumber = item.CarPlateNumber,
                                        //IoStatus = lastDeviceData == null ? "" : lastDeviceData.IoStatus,
                                        //KmPerHour = lastDeviceData == null ? "" : Convert.ToString(lastDeviceData.KmPerHour),
                                        LastDeviceId = lastDeviceData == null ? 0 : item.Id,
                                        //LastLocationTime = lastDeviceData == null ? "" : lastDeviceData.CreateDate.ToString().Replace("T", " ").Replace("-", "."),
                                        //Konum = lastDeviceData == null ? "" : DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.Latitude.ToString(), lastDeviceData.Longtitude.ToString()),
                                        //ActivityTime = "SinyalYok"
                                    });
                                }
                            }
                        }
                    }
                    //foreach (var item in devices)
                    //{ 

                    //    //DeviceData lastDeviceData = null;
                    //    //var aktifOrPasif = "";
                    //    //if (item.LastDeviceDataId != null)
                    //    //{
                    //    //    lastDeviceData = context.DeviceData.FirstOrDefault(x => x.Id == item.LastDeviceDataId);
                    //    //    aktifOrPasif = Convert.ToInt32(item.ActivityTime) > (DateTime.Now - Convert.ToDateTime(lastDeviceData.CreateDate)).TotalMinutes ? "Aktif" : "Pasif";
                    //    //}
                    //    //if (lastDeviceData == null)
                    //    //    aktifOrPasif = "Pasif";

                    //    //deviceReturnList.Add(new DeviceObjectForFrontSide()
                    //    //{
                    //    //    CarPlateNumber = item.CarPlateNumber,
                    //    //    IoStatus = lastDeviceData == null ? "" : lastDeviceData.IoStatus,
                    //    //    KmPerHour = lastDeviceData == null ? "" : Convert.ToString(lastDeviceData.KmPerHour),
                    //    //    LastDeviceId = lastDeviceData == null ? 0 : item.Id,
                    //    //    LastLocationTime = lastDeviceData == null ? "" : lastDeviceData.CreateDate.ToString().Replace("T", " ").Replace("-", "."),
                    //    //    Konum = lastDeviceData == null ? "" : DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.Latitude.ToString(), lastDeviceData.Longtitude.ToString()),
                    //    //    ActivityTime = aktifOrPasif
                    //    //});
                    //}
                    if (deviceReturnList != null && deviceReturnList.Count > 0)
                    {
                        var returnDeviceIds = deviceReturnList.Select(x => x.LastDeviceId).ToList();
                        deviceIdsReturnList.AddRange(returnDeviceIds);
                        return Json(deviceIdsReturnList);
                    }
                    return Json(false);

                }
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult SaveCompanyShift(int _companyShiftId, string _startDateCS, string _endDateCS, string _startHourCS, string _endHourCS, string _shiftNameCS, bool _dataProcessCS, bool _shiftSendPZTCS, bool _shiftSendSALCS, bool _shiftSendCARCS, bool _shiftSendPERCS, bool _shiftSendCUMCS, bool _shiftSendCMTCS, bool _shiftSendPZRCS)
        {
            try
            {
                if (_companyShiftId == 0)
                {
                    Shift shift = new Shift()
                    {
                        StartDate = Convert.ToDateTime(_startDateCS),
                        EndDate = Convert.ToDateTime(_endDateCS),
                        StartHour = _startHourCS,
                        EndHour = _endHourCS,
                        Name = _shiftNameCS,
                        DataProcess = _dataProcessCS,
                        Status = 1,
                        Pazartesi = _shiftSendPZTCS,
                        Sali = _shiftSendSALCS,
                        Carsamba = _shiftSendCARCS,
                        Persembe = _shiftSendPERCS,
                        Cuma = _shiftSendCUMCS,
                        Cumartesi = _shiftSendCMTCS,
                        Pazar = _shiftSendPZRCS,
                        CreationDate = DateTime.Now
                    };
                    ShiftDB.GetInstance().AddNewShift(shift);
                    var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                    CompanyShift companyShift = new CompanyShift()
                    {
                        CompanyId = company.Id,
                        ShiftId = shift.Id,
                        Status = 1
                    };
                    var result = CompanyShiftDB.GetInstance().AddNewCompanyShift(companyShift);
                    return Json(result != null);
                }
                else if (_companyShiftId != 0)
                {
                    var companyShift = CompanyShiftDB.GetInstance().GetCompanyShiftById(_companyShiftId);
                    var shift = ShiftDB.GetInstance().GetShiftById(companyShift.ShiftId);
                    shift.Id = _companyShiftId;
                    shift.StartDate = Convert.ToDateTime(_startDateCS);
                    shift.EndDate = Convert.ToDateTime(_endDateCS);
                    shift.StartHour = _startHourCS;
                    shift.EndHour = _endHourCS;
                    shift.Name = _shiftNameCS;
                    shift.DataProcess = _dataProcessCS;
                    shift.Status = 1;
                    shift.Pazartesi = _shiftSendPZTCS;
                    shift.Sali = _shiftSendSALCS;
                    shift.Carsamba = _shiftSendCARCS;
                    shift.Persembe = _shiftSendPERCS;
                    shift.Cuma = _shiftSendCUMCS;
                    shift.Cumartesi = _shiftSendCMTCS;
                    shift.Pazar = _shiftSendPZRCS;
                    shift.CreationDate = DateTime.Now;
                    var result = ShiftDB.GetInstance().UpdateShift(shift);
                    return Json(result != null);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public ActionResult GetCompanyShiftList()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                    var companyShifts = context.CompanyShift.Where(x => x.CompanyId == company.Id);
                    var companyShiftsShiftIds = companyShifts.Select(x => x.ShiftId).ToList();
                    var shifts = context.Shift.Where(x => companyShiftsShiftIds.Contains(x.Id) && x.Status == 1).ToList();
                    if (shifts != null)
                    {
                        return PartialView("_CompanyShiftList", shifts);
                    }
                }
                return null;
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public IActionResult ExportCompanyShift()
        {
            using (var context = new VeraEntities())
            {
                var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                var companyShifts = context.CompanyShift.Where(x => x.CompanyId == company.Id);
                var companyShiftsShiftIds = companyShifts.Select(x => x.ShiftId).ToList();
                var shifts = context.Shift.Where(x => companyShiftsShiftIds.Contains(x.Id) && x.Status == 1).ToList();
                foreach (var item in shifts)
                {
                    item.StartDate = Convert.ToDateTime(item.StartDate);
                    item.EndDate = Convert.ToDateTime(item.EndDate);
                    item.CreationDate = Convert.ToDateTime(item.CreationDate);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(shifts));
                dt.Columns.Add("Baslangıc", typeof(string));
                dt.Columns.Add("Bitis", typeof(string));
                dt.Columns.Add("Creation", typeof(string));
                for (int i = 0; i < shifts.Count; i++)
                {
                    var sDate = dt.Rows[i]["StartDate"].ToString();
                    dt.Rows[i]["Baslangıc"] = sDate;
                    var bDate = dt.Rows[i]["EndDate"].ToString();
                    dt.Rows[i]["Bitis"] = bDate;
                    var cDate = dt.Rows[i]["CreationDate"].ToString();
                    dt.Rows[i]["Creation"] = cDate;
                }
                dt.Columns.Remove("Id");
                dt.Columns.Remove("DataProcess");
                dt.Columns.Remove("Status");
                dt.Columns.Remove("StartDate");
                dt.Columns.Remove("EndDate");
                dt.Columns.Remove("CreationDate");
                dt.Columns["Name"].ColumnName = "Çizelge Adı";
                dt.Columns["Baslangıc"].ColumnName = "Başlangıç Tarihi";
                dt.Columns["Bitis"].ColumnName = "Bitiş Tarihi";
                dt.Columns["StartHour"].ColumnName = "Başlangıç Saati";
                dt.Columns["EndHour"].ColumnName = "Bitiş Saati";
                dt.Columns["Creation"].ColumnName = "Tanım Zamanı";
                dt.Columns["Sali"].ColumnName = "Salı";
                dt.Columns["Carsamba"].ColumnName = "Çarşamba";
                dt.Columns["Persembe"].ColumnName = "Perşembe";
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Mesai Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"MesaiBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public JsonResult DeleteCompanyShift(int _id)
        {
            try
            {
                var companyShift = CompanyShiftDB.GetInstance().GetCompanyShiftById(_id);
                var deleteCompanyShift = CompanyShiftDB.GetInstance().DeleteCompanyShift(_id);
                var deleteShift = ShiftDB.GetInstance().DeleteShift(companyShift.ShiftId);
                if (deleteCompanyShift == true && deleteShift == true)
                {
                    return Json(true);
                }

                return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetDeviceListTSV(int _lastDeviceIdTSV, string _carPlateNumberTSV, int _deviceActivityTSV, int _usingStatusIdTSV)
        {
            try
            {
                var deviceList = DeviceDB.GetInstance().GetDeviceListTSV(userObj, _lastDeviceIdTSV, _carPlateNumberTSV, _deviceActivityTSV, _usingStatusIdTSV);
                if (deviceList != null && deviceList.Count > 0)
                {
                    if (_carPlateNumberTSV != null && _carPlateNumberTSV != "")
                        deviceList = deviceList.Where(x => x.CarPlateNumber.Trim().ToLower().StartsWith(_carPlateNumberTSV.Trim().ToLower())).ToList();
                    foreach (var item in deviceList)
                    {
                        string totalHours = item.ActivityTime == null ? "" : (DateTime.Now - Convert.ToDateTime(item.ActivityTime)).TotalHours.ToString();
                        item.OperationTime = item.OperationTime == null ? "" : item.OperationTime.Replace("T", " ").Substring(0, 19);
                        item.ActivityTime = item.ActivityTime == null ? "" : ReportDB.GetInstance().TotalHoursToHumanReadableString(totalHours.ToString());
                        item.MontageData = item.MontageData == null ? "-" : item.MontageData;
                        item.EngineBlockStatus = item.EngineBlockStatus == "false" ? "Kapalı" : "Açık";
                        item.VehicleType = item.VehicleType == null ? "" : item.VehicleType;
                    }
                    return Json(deviceList);
                }
                if (deviceList == null)
                {
                    return Json(false);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult GetDeviceListAP(int _groupIdAP)
        {
            try
            {
                var deviceList = DeviceDB.GetInstance().GetDeviceListAP(userObj, _groupIdAP);
                if (deviceList != null && deviceList.Count > 0)
                {
                    foreach (var item in deviceList)
                    {
                        item.LocationStatus = item.PositionTime == null ? "Programlanmadı" : "Programlandı";
                        item.RolantiStatus = item.RolantiTime == null ? "Programlanmadı" : "Programlandı";
                        item.SpeedStatus = item.SpeedLimit == null ? "Programlanmadı" : "Programlandı";
                    }
                    return Json(deviceList);
                }
                if (deviceList == null)
                {
                    return Json(false);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult ChangePassword(string _usernewpass)
        {
            try
            {
                var user = UserDB.GetInstance().GetUserById(userObj.Id);
                user.Password = _usernewpass;
                var result = UserDB.GetInstance().UpdateUser(user, 1);
                return Json(result != null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult UpdateUserInf(string _userNameTU, string _userLastNameTU, string _emailTU, int _homepageRefreshTimeTU)
        {
            try
            {
                var user = UserDB.GetInstance().GetUserById(userObj.Id);
                user.Name = _userNameTU;
                user.Surname = _userLastNameTU;
                user.Mail = _emailTU;
                user.HomepageRefreshTime = _homepageRefreshTimeTU;
                var result = UserDB.GetInstance().UpdateUser(user);
                return Json(result != null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult UpdateUserMail(string _UpdateMailAR)
        {
            try
            {
                var user = UserDB.GetInstance().GetUserById(userObj.Id);
                user.Mail = _UpdateMailAR;
                var result = UserDB.GetInstance().UpdateUser(user);
                return Json(result != null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult UpdateAutomaticReporting(int _userReportId, string _lastReportStatus, string _lastReportType)
        {
            try
            {
                var userReport = UserReportDB.GetInstance().GetUserReportById(_userReportId);
                userReport.AutomaticReportingStatus = _lastReportStatus == "Aktif" ? 1 : 0;
                userReport.UserReportType = _lastReportType == "PDF" ? 2 : 1;
                var result = UserReportDB.GetInstance().UpdateUserReport(userReport);
                return Json(result != null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetUserReportById(int _userReportId)
        {
            try
            {
                var userReport = UserReportDB.GetInstance().GetUserReportById(_userReportId);
                if (userReport.AutomaticReportingVehicles == null || userReport.AutomaticReportingVehicles == "")
                {
                    return Json(false);
                }
                return Json(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult UpdateDeviceInf(int _fillDeviceLast, string _carPlateNumberAG, string _vehicleTypeAG, string _usingStatusAG, string _descriptionAG, string _waitingTimeAG, string _speedTypeAG)
        {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceById(_fillDeviceLast);
                device.CarPlateNumber = _carPlateNumberAG == null ? "" : _carPlateNumberAG;
                device.VehicleTypeId = Convert.ToInt32(_vehicleTypeAG);
                device.UsingStatusId = Convert.ToInt32(_usingStatusAG);
                device.TechnicalNote = _descriptionAG == null ? "" : _descriptionAG;
                device.ProgrammedWaitingTime = Convert.ToInt32(_waitingTimeAG);
                device.ProgrammedSpeedLimit = Convert.ToInt32(_speedTypeAG);
                var result = DeviceDB.GetInstance().UpdateDevice(device);
                return Json(result != null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult UpdateAP(int _fillDeviceLast, string _positionTimeAP, string _positionDistanceAP, string _speedLimitAP, string _rolantiTimeAP)
        {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceById(_fillDeviceLast);
                device.ProgrammedLocationSendTime = Convert.ToInt32(_positionTimeAP);
                device.ProgrammedLocationDistance = Convert.ToInt32(_positionDistanceAP);
                device.ProgrammedSpeedLimit = Convert.ToInt32(_speedLimitAP);
                device.ProgrammedWaitingTime = Convert.ToInt32(_rolantiTimeAP);
                var result = DeviceDB.GetInstance().UpdateDevice(device);
                return Json(result != null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult SaveUserQuestionAnswer(int _questionIdTRP, string _questionAnswerTRP)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var userHasQuestion = context.UserQuestionAnswer.FirstOrDefault(x => x.UserId == userObj.Id);
                    if (userHasQuestion == null)
                    {
                        var userQuestionAnswer = new UserQuestionAnswer()
                        {
                            Answer = _questionAnswerTRP,
                            QuestionId = _questionIdTRP,
                            UserId = userObj.Id
                        };
                        var result = UserQuestionAnswerDB.GetInstance().AddNewUserQuestionAnswer(userQuestionAnswer);
                        return Json(result != null);
                    }
                    else
                    {
                        var userQuestion = context.UserQuestionAnswer.FirstOrDefault(x => x.UserId == userObj.Id);
                        userQuestion.QuestionId = _questionIdTRP;
                        userQuestion.Answer = _questionAnswerTRP;
                        var result = UserQuestionAnswerDB.GetInstance().UpdateUserQuestionAnswer(userQuestion);
                        return Json(result != null);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult SaveAlarm(string[] _vehicleIdYAA, int _alarmTypeIdYAA, string[] _chooseDayYAA, DateTime _startDateYAA, DateTime _endDateYAA, string _emailYAA)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var deviceIds = "";
                    for (int i = 0; i < _vehicleIdYAA.Length; i++)
                    {
                        deviceIds += _vehicleIdYAA[i] + ",";
                    }
                    var days = "";
                    for (int i = 0; i < _chooseDayYAA.Length; i++)
                    {
                        days += _chooseDayYAA[i] + ",";
                    }
                    var alarm = new Alarm()
                    {
                        DeviceIds = deviceIds,
                        Days = days,
                        StartDate = _startDateYAA,
                        EndDate = _endDateYAA,
                        Email = _emailYAA,
                        AlarmTypeId = _alarmTypeIdYAA,
                        UserId = userObj.Id,
                        Status = 1
                    };
                    var result = AlarmDB.GetInstance().AddNewAlarm(alarm);
                    if(result != null)
                    {
                        var userAlarm = new UserAlarm()
                        {
                            AlarmId = result.Id,
                            UserId = userObj.Id
                        };
                        var result2 = UserAlarmDB.GetInstance().AddNewUserAlarm(userAlarm);
                    }
                    return Json(result != null);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetUserQuestionAndAnswer()
        {
            try
            {
                var questionAndAnswer = UserQuestionAnswerDB.GetInstance().GetUserQuestionAnswerByUserId(userObj.Id);
                return Json(questionAndAnswer);
            }
            catch (Exception)
            {

                throw;
            }


        }
        public JsonResult GetAllCompaniesByUserId()
        {
            try
            {
                var company = CompanyDB.GetInstance().GetAllCompaniesByUserId(userObj);
                return Json(company);
            }
            catch (Exception)
            {

                throw;
            }


        }
        public JsonResult AssignVehicleToUser(int _aracatamauserid, List<int> _selectedDeviceIds)
        {

            try
            {
                var deleteVehicles = UserDeviceDB.GetInstance().DeleteUserDevice(_aracatamauserid);


                foreach (var deviceId in _selectedDeviceIds)
                {
                    var userDevice = new UserDevice()
                    {
                        DeviceId = deviceId,
                        UserId = _aracatamauserid,

                    };

                    var insertResult = UserDeviceDB.GetInstance().AddNewUserDevice(userDevice);

                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult AssignUserReport()
        {

            try
            {
                var users = UserDB.GetInstance().GetAllUsers().ToList();


                using (var context = new VeraEntities())
                {
                    foreach (var user in users)
                    {
                        var userReport = new UserReport()
                        {
                            UserId = user.Id,
                            ReportId = 1011,
                            UserReportType = 2,
                            AutomaticReportingStatus = 1,
                            AutomaticReportingVehicles = "",
                        };
                        UserReportDB.GetInstance().AddNewUserReport(userReport);
                    }

                }

                return Json(true);


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult AssignAreaToDevice(int _vehicleId, List<int> _selectedAreaIds)
        {

            try
            {
                var deleteVehicles = AreaDeviceDB.GetInstance().DeleteAllAreaDevices(_vehicleId);


                foreach (var areaId in _selectedAreaIds)
                {
                    var areaDevice = new AreaDevice()
                    {
                        DeviceId = _vehicleId,
                        AreaId = areaId,
                        Status = 1
                    };

                    var insertResult = AreaDeviceDB.GetInstance().AddNewAreaDevice(areaDevice);

                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult AssignRouteToDevice(int _vehicleId, List<int> _selectedRouteIds)
        {

            try
            {
                var deleteVehicles = RouteDeviceDB.GetInstance().DeleteAllRouteDevices(_vehicleId);


                foreach (var routeId in _selectedRouteIds)
                {
                    var routeDevice = new RouteDevice()
                    {
                        DeviceId = _vehicleId,
                        RouteId = routeId,
                        Status = 1
                    };

                    var insertResult = RouteDeviceDB.GetInstance().AddNewRouteDevice(routeDevice);

                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult AssignVehicleToReport(int _userReportId, List<int> _selectedDeviceIds)
        {

            try
            {
                using (var context = new VeraEntities())
                {
                    var Ids = "";
                    for (int i = 0; i < _selectedDeviceIds.Count; i++)
                    {
                        if (i < _selectedDeviceIds.Count - 1)
                        {
                            Ids += _selectedDeviceIds[i] + ",";
                        }
                        if (i + 1 == _selectedDeviceIds.Count)
                        {
                            Ids += _selectedDeviceIds[i];
                        }

                    }
                    var userRe = context.UserReport.FirstOrDefault(x => x.Id == _userReportId);
                    var userReport = new UserReport()
                    {
                        Id = userRe.Id,
                        AutomaticReportingVehicles = Ids,
                        ReportId = userRe.ReportId,
                        UserId = userRe.UserId,
                        AutomaticReportingStatus = userRe.AutomaticReportingStatus,
                        UserReportType = userRe.UserReportType

                    };
                    var insertResult = UserReportDB.GetInstance().UpdateUserReport(userReport);
                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                    return Json(true);
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult AssignReportToUser(int _raporatamauserid, List<int> _selectedReportIds)
        {

            try
            {
                var deleteReports = UserReportDB.GetInstance().DeleteUserReport(_raporatamauserid);


                foreach (var reportId in _selectedReportIds)
                {
                    var userReport = new UserReport()
                    {
                        UserId = _raporatamauserid,
                        ReportId = reportId,
                        AutomaticReportingStatus = 0,
                        UserReportType = 1
                    };

                    var insertResult = UserReportDB.GetInstance().AddNewUserReport(userReport);

                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult GetUserDevicesByUserId(int _aracatamauserid)

        {
            try
            {
                if (_aracatamauserid == 0)
                {
                    return Json(false);
                }
                var userDevicesList = UserDeviceDB.GetInstance().GetUserDevicesByUserId(_aracatamauserid);
                return Json(userDevicesList);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetAppendUserDeviceList(int _aracatamauserid)
        {
            try
            {
                var deviceList = UserDeviceDB.GetInstance().GetAppendUserDeviceList(_aracatamauserid);
                if (deviceList != null && deviceList.Count > 0)
                    return Json(deviceList);
                return Json(false);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetUserReportsByUserId(int _raporatamauserid)

        {
            try
            {
                var userReportsList = UserReportDB.GetInstance().GetUserReportByUserId(_raporatamauserid);
                return Json(userReportsList);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetDeviceAreasByDeviceId(int _deviceId)

        {
            try
            {
                var deviceAreaList = AreaDeviceDB.GetInstance().GetAreaVehiclesByDeviceId(_deviceId);
                return Json(deviceAreaList);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetDeviceRoutesByDeviceId(int _deviceId)

        {
            try
            {
                var deviceRouteList = RouteDeviceDB.GetInstance().GetRouteVehiclesByDeviceId(_deviceId);
                return Json(deviceRouteList);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetCompanyUser(int _aracAtamaCompanyId)

        {
            try
            {
                var CompanyUserList = CompanyUserDB.GetInstance().GetCompanyUser(_aracAtamaCompanyId);
                return Json(CompanyUserList);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetAllDBDevicesOfCompany(int _aracatamauserid)

        {
            try
            {
                var companyDevicesList = UserDeviceDB.GetInstance().GetUserDevicesByUserId(_aracatamauserid);
                return Json(companyDevicesList);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetAlarmList()

        {
            try
            {
                var getAlarmList = AlarmDB.GetInstance().GetAlarmList(userObj);
                return Json(getAlarmList);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult ListeleAlarmToSearch(int _alarmTypeId, int _alarmVehiclesId)

        {
            try
            {
                var searchAlarm = AlarmDB.GetInstance().GetAlarmToSearch(_alarmTypeId, _alarmVehiclesId,userObj);
                return Json(searchAlarm);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetGroupDevicesByGroupId(int _vehicleGroupId, string _vehicleSearch)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                    var subCompaniesAndOwnCompanyCompanyIds = context.Company.Where(x => x.MainCompanyId == company.Id).Select(x => x.Id).ToList();
                    subCompaniesAndOwnCompanyCompanyIds.Add(company.Id);
                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    var devices = DeviceDB.GetInstance().GetAllDBDevices().Where(x => x.LastDeviceDataId != null).ToList();
                    //if (companyUser.IsCompanyAdmin)
                    //    devices = devices.Where(x => subCompaniesAndOwnCompanyCompanyIds.Contains(x.CompanyId)).ToList();
                    //if (!companyUser.IsCompanyAdmin)
                    //{
                    var assignedDevicesDeviceIds = context.UserDevice.Where(x => x.UserId == userObj.Id).Select(x => x.DeviceId).ToList();
                    devices = devices.Where(x => assignedDevicesDeviceIds.Contains(x.Id)).ToList();

                    // }

                    if (_vehicleGroupId == 0 && String.IsNullOrEmpty(_vehicleSearch))
                    {
                        return Json(devices);
                    }
                    if (_vehicleGroupId == 0 && !String.IsNullOrEmpty(_vehicleSearch))
                    {
                        devices = devices.Where(x => x.CarPlateNumber.Trim().ToLower().Contains(_vehicleSearch.Trim().ToLower())).ToList();
                        return Json(devices);
                    }
                    if (_vehicleGroupId != 0)
                    {
                        var group = GroupDB.GetInstance().GetGroupById(_vehicleGroupId);
                        var groupDevices = context.GroupDevice.Where(x => x.GroupId == group.Id).ToList();
                        var groupDevicesDeviceIds = groupDevices.Select(x => x.DeviceId).ToList();
                        devices = devices.Where(x => groupDevicesDeviceIds.Contains(x.Id)).ToList();
                        if (_vehicleSearch != null && _vehicleSearch != "")
                        {
                            devices = devices.Where(x => x.CarPlateNumber.Trim().ToLower().Contains(_vehicleSearch.Trim().ToLower())).ToList();
                        }
                        return Json(devices);
                    }
                    return Json(false);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }


        }
        public JsonResult DisplayVehiclesOnMap(int _stopOrContinue)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    if (_stopOrContinue == 1)
                    {
                        var devices = UserDeviceDB.GetInstance().GetUserDevicesByUserId(userObj.Id);
                        if (devices != null)
                        {
                            foreach (var item in devices)
                            {
                                item.ActivityTime = Convert.ToInt32(item.ActivityTime) > (DateTime.Now - Convert.ToDateTime(item.Date)).TotalMinutes ? 1 : 0;
                            }
                            return Json(devices);
                        }

                    }
                    return Json(false);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult DisplayPastLocationOfDevicesOnMap(int _deviceId, int _time)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var list = new List<string>();
                    var device = context.Device.FirstOrDefault(x => x.Id == _deviceId);
                    List<DeviceInfoForMap> latLongList = new List<DeviceInfoForMap>();
                    int time = 0;

                    //Saat 
                    if (_time == 1) // 1 ise 1 saat 
                        time = 1;
                    if (_time == 2) //2 ise 6 saat 
                        time = 6;
                    if (_time == 3) // 3 ise 24 saat
                        time = 24;
                    if (_time == 4) // 4 ise 1 hafta
                        time = 168;

                    var lastDeviceDatasList = context.DeviceData.Where(x => x.DeviceId == _deviceId && x.CreateDate >= DateTime.Now.AddHours(-time) && x.CreateDate <= DateTime.Now && x.IoStatus.EndsWith("1")).ToList();
                    if (lastDeviceDatasList == null || lastDeviceDatasList.Count < 1)
                    {
                        var ex = context.DeviceData.LastOrDefault(x => x.DeviceId == _deviceId && x.CreateDate >= DateTime.Now.AddHours(-time) && x.CreateDate <= DateTime.Now);
                        latLongList.Add(new DeviceInfoForMap()
                        {
                            lat = ex.Latitude,
                            lng = ex.Longtitude,
                            DirectionDegree = ex.DirectionDegree,
                            IoStatus = ex.IoStatus.EndsWith("1") ? "1" : "0",
                            Date = Convert.ToDateTime(ex.CreateDate).ToString(),
                            KmPerHour = ex.KmPerHour
                        });
                        if (latLongList != null && latLongList.Count > 0)
                            return Json(latLongList.OrderBy(x => x.Date));
                        return Json(false);
                    }
                    else
                    {
                        foreach (var item in lastDeviceDatasList)
                        {
                            latLongList.Add(new DeviceInfoForMap()
                            {
                                lat = item.Latitude,
                                lng = item.Longtitude,
                                DirectionDegree = item.DirectionDegree,
                                IoStatus = item.IoStatus.EndsWith("1") ? "1" : "0",
                                Date = Convert.ToDateTime(item.CreateDate).ToString(),
                                KmPerHour = item.KmPerHour
                            });
                        }
                        if (latLongList != null && latLongList.Count > 0)
                            return Json(latLongList.OrderBy(x => x.Date));
                        return Json(false);
                    }

                }
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult DisplayDeviceOnMapByDeviceId(int _id)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var device = context.Device.FirstOrDefault(x => x.Id == _id);
                    var lastDeviceData = context.DeviceData.FirstOrDefault(x => x.Id == device.LastDeviceDataId);
                    if (lastDeviceData != null)
                    {
                        DeviceInfoForMap deviceInfo = new DeviceInfoForMap()
                        {
                            DeviceId = device.Id,
                            lat = lastDeviceData.Latitude,
                            lng = lastDeviceData.Longtitude,
                            CarPlateNumber = device.CarPlateNumber,
                            Altitude = lastDeviceData.Altitude,
                            Date = Convert.ToString(lastDeviceData.CreateDate),
                            IoStatus = lastDeviceData.IoStatus.EndsWith("1") ? "1" : "0",
                            TotalKmDaily = lastDeviceData.TotalKm,
                            KmPerHour = lastDeviceData.KmPerHour,
                            Location = DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.LocationId),
                            DirectionDegree = Convert.ToInt16(lastDeviceData.DirectionDegree),
                            ActivityTime = device.ActivityTime > (DateTime.Now - lastDeviceData.CreateDate).TotalMinutes ? "Aktif" : "Pasif"
                        };
                        return Json(deviceInfo);
                    }
                    return Json(false);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult SaveCompanyMapArea(int _companyAreaId, string _areaName, int _areaType, string[] _polygonArray)
        {
            try
            {
                CompanyUser compUser = CompanyUserDB.GetInstance().GetCompanyUserById(userObj.Id);
                if (_companyAreaId == 0)
                {
                    if (compUser.IsCompanyAdmin == true)
                    {
                        if (!String.IsNullOrEmpty(_areaName))
                        {
                            string latsLongs = "";
                            for (int i = 0; i < _polygonArray.Length; i++)
                            {
                                _polygonArray[i] = _polygonArray[i].Trim().Replace(" ", "") + ",";
                                latsLongs += _polygonArray[i];
                            }
                            latsLongs = latsLongs.Substring(0, latsLongs.Length - 1);
                            Area area = new Area()
                            {
                                IsRestricted = _areaType == 1 ? true : false,
                                LatsLongs = latsLongs,
                                Name = _areaName,
                                Status = 1

                            };
                            AreaDB.GetInstance().AddNewArea(area);
                            CompanyArea compArea = new CompanyArea()
                            {
                                AreaId = area.Id,
                                CompanyId = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj).Id,
                                Status = 1
                            };
                            CompanyAreaDB.GetInstance().AddNewCompanyArea(compArea);
                            return Json(true);
                        }
                    }
                }
                if (_companyAreaId != 0)
                {
                    var area = AreaDB.GetInstance().GetAreaById(_companyAreaId);
                    area.IsRestricted = _areaType == 1 ? true : false;
                    area.Name = _areaName;
                    var result = AreaDB.GetInstance().UpdateArea(area);
                    if (result != null)
                        return Json(true);
                }
                return Json(false);


            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult SaveCompanyMapRoute(int _companyRouteId, string _routeName, string[] _polylineArray)
        {
            try
            {
                CompanyUser compUser = CompanyUserDB.GetInstance().GetCompanyUserById(userObj.Id);
                if (_companyRouteId == 0)
                {
                    if (compUser.IsCompanyAdmin == true)
                    {
                        if (!String.IsNullOrEmpty(_routeName))
                        {
                            string latsLongs = "";
                            for (int i = 0; i < _polylineArray.Length; i++)
                            {
                                _polylineArray[i] = _polylineArray[i].Trim().Replace(" ", "") + ",";
                                latsLongs += _polylineArray[i];
                            }
                            latsLongs = latsLongs.Substring(0, latsLongs.Length - 1);
                            Route route = new Route()
                            {
                                LatsLongs = latsLongs,
                                Name = _routeName,
                                Status = 1
                            };
                            RouteDB.GetInstance().AddNewRoute(route);
                            CompanyRoute compRoute = new CompanyRoute()
                            {
                                RouteId = route.Id,
                                CompanyId = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj).Id,
                                Status = 1
                            };
                            CompanyRouteDB.GetInstance().AddNewCompanyRoute(compRoute);
                            return Json(true);
                        }
                    }
                }
                if (_companyRouteId != 0)
                {
                    var route = RouteDB.GetInstance().GetRouteById(_companyRouteId);
                    route.Name = _routeName;
                    var result = RouteDB.GetInstance().UpdateRoute(route);
                    if (result != null)
                        return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        /// REPORTS 
        //public string HtmlToPdf(string _html)
        //{
        //    HtmlToPdf converter = new HtmlToPdf();
        //    converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
        //    converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Landscape;
        //    converter.Options.MarginLeft = 10;
        //    converter.Options.MarginRight = 0;
        //    converter.Options.MarginTop = 10;
        //    converter.Options.MarginBottom = 10;

        //    var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";

        //    SelectPdf.PdfDocument doc = converter.ConvertHtmlString(_html);
        //    doc.Save(Path.Combine(_env.WebRootPath, "Content", fileName));
        //    doc.Close();

        //    return Path.Combine(_env.WebRootPath, "Content", fileName);
        //}

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportSeferOzetiRaporu(string _startDateSOR, string _endDateSOR, string[] _vehiclesSOR, int _fileTypeSOR, int _downloadOrMail, string _emailToSendReport)
        {
            try
            {
                if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
                {
                    return Json(false);
                }
                // File Type== 1: Excel , 2 :PDF
                //Download Or Mail == 1:download , 2 :mail
                var reportList = ReportDB.GetInstance().SeferOzetiRaporu(_startDateSOR, _endDateSOR, _vehiclesSOR);
                if (reportList == null)
                {
                    reportList = new Dictionary<SORKeyClass, List<SeferOzetiRaporuObjectRepo>>();
                }
                if (_fileTypeSOR == 1)
                {
                    List<SeferOzetiRaporuObjectRepo> returnList2 = new List<SeferOzetiRaporuObjectRepo>();
                    if (reportList.Count != 0)
                    {
                        foreach (var item in reportList)
                        {
                            foreach (var item2 in item.Value)
                            {
                                returnList2.Add(item2);
                            }
                        }
                    }
                    if (returnList2.Count == 0)
                    {
                        var bosSeferOzetiObjectRepo = new SeferOzetiRaporuObjectRepo();
                        returnList2.Add(bosSeferOzetiObjectRepo);
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                    dt.Columns["Arac"].ColumnName = "Araç Plakası";
                    dt.Columns["KalkisZamani"].ColumnName = "Kalkış Zamanı";
                    dt.Columns["VarisZamani"].ColumnName = "Varış Zamanı";
                    dt.Columns["KalkisPozisyon"].ColumnName = "Kalkış Pozisyonu";
                    dt.Columns["Sure"].ColumnName = "Süre";
                    //dt.Columns["OrtHiz"].ColumnName = "Ortalama Hız";
                    //dt.Columns["MaxHiz"].ColumnName = "Maximum Hız";
                    dt.Columns["VarisPozisyon"].ColumnName = "Varış Pozisyonu";
                    var stream = new MemoryStream();
                    var fileName = $"SeferOzetiRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("SeferOzetiRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        sheet.Protection.IsProtected = false;
                        sheet.Protection.AllowSelectLockedCells = false;
                        package.SaveAs(new FileInfo(rootPath));
                    }
                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        var url = "/Content/" + fileName;
                        return Json(url);
                        //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }

                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailExcel(dt, "SeferOzetiRaporu", fileName, _emailToSendReport);
                        return Json(true);
                    }
                }

                //if (_showOrDownload == 2)
                //{
                //    return PartialView("_SeferOzetiRaporuPDF", reportList);
                //}
                //var html = RenderViewAsString("_SeferOzetiRaporuPDF", reportList);
                //var file = HtmlToPdf(html);
                //byte[] fileBytes = System.IO.File.ReadAllBytes(file);
                //if (_showOrDownload == 1)
                //{
                //    return File(fileBytes, "application/pdf", file);
                //}
                //return File(file, "application/pdf");
                if (_fileTypeSOR == 2)
                {
                    var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                    ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                    ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateSOR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateSOR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailPDF(reportList, "SeferOzeti", "_SeferOzetiRaporuPDF", _emailToSendReport);
                        return Json(true);
                    }
                    return PartialView("_SeferOzetiRaporuPDF", reportList);
                }
                return View();
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportSeferOzetiRaporu2(string _startDateSOR, string _endDateSOR, string[] _vehiclesSOR, int _fileTypeSOR, int _downloadOrMail, string _emailToSendReport)
        {
            try
            {
                if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
                {
                    return Json(false);
                }
                // File Type== 1: Excel , 2 :PDF
                //Download Or Mail == 1:download , 2 :mail
                var reportList = ReportDB.GetInstance().SeferOzetiRaporu2(_startDateSOR, _endDateSOR, _vehiclesSOR);
                if (reportList == null)
                {
                    reportList = new Dictionary<SORKeyClass, List<SeferOzetiRaporuObjectRepo2>>();
                }
                if (_fileTypeSOR == 1)
                {
                    List<SeferOzetiRaporuObjectRepo2> returnList2 = new List<SeferOzetiRaporuObjectRepo2>();
                    if (reportList.Count != 0)
                    {
                        foreach (var item in reportList)
                        {
                            foreach (var item2 in item.Value)
                            {
                                returnList2.Add(item2);
                            }
                        }
                    }
                    if (returnList2.Count == 0)
                    {
                        var bosSeferOzetiObjectRepo = new SeferOzetiRaporuObjectRepo2();
                        returnList2.Add(bosSeferOzetiObjectRepo);
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                    dt.Columns["CarPlateNumber"].ColumnName = "Araç Plakası";
                    dt.Columns["StartDate"].ColumnName = "Kalkış Zamanı";
                    dt.Columns["EndDate"].ColumnName = "Varış Zamanı";
                    dt.Columns["StartLoc"].ColumnName = "Kalkış Pozisyonu";
                    dt.Columns["Distance"].ColumnName = "Mesafe";
                    //dt.Columns["OrtHiz"].ColumnName = "Ortalama Hız";
                    //dt.Columns["MaxHiz"].ColumnName = "Maximum Hız";
                    dt.Columns["EndLoc"].ColumnName = "Varış Pozisyonu";
                    var stream = new MemoryStream();
                    var fileName = $"SeferOzetiRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("SeferOzetiRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        sheet.Protection.IsProtected = false;
                        sheet.Protection.AllowSelectLockedCells = false;
                        package.SaveAs(new FileInfo(rootPath));
                    }
                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        var url = "/Content/" + fileName;
                        return Json(url);
                        //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }

                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailExcel(dt, "SeferOzetiRaporu", fileName, _emailToSendReport);
                        return Json(true);
                    }
                }

                //if (_showOrDownload == 2)
                //{
                //    return PartialView("_SeferOzetiRaporuPDF", reportList);
                //}
                //var html = RenderViewAsString("_SeferOzetiRaporuPDF", reportList);
                //var file = HtmlToPdf(html);
                //byte[] fileBytes = System.IO.File.ReadAllBytes(file);
                //if (_showOrDownload == 1)
                //{
                //    return File(fileBytes, "application/pdf", file);
                //}
                //return File(file, "application/pdf");
                if (_fileTypeSOR == 2)
                {
                    var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                    ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                    ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateSOR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateSOR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailPDF(reportList, "SeferOzeti", "_SeferOzetiRaporuPDF2", _emailToSendReport);
                        return Json(true);
                    }
                    return PartialView("_SeferOzetiRaporuPDF2", reportList);
                }
                return View();
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }


        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportGecmisKonumRaporu(string _startDateGKR, string _endDateGKR, string[] _vehiclesGKR, int _fileTypeGKR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            // File Type: 1 ise Excel ,  2 ise PDF
            var reportList = ReportDB.GetInstance().GecmisKonumRaporu(_startDateGKR, _endDateGKR, _vehiclesGKR);
            if (reportList == null)
            {
                reportList = new Dictionary<GKRKeyClass, List<GecmisKonumRaporuObjectRepo>>();
            }
            if (_fileTypeGKR == 1)
            {
                List<GecmisKonumRaporuObject> returnList2 = new List<GecmisKonumRaporuObject>();
                if (reportList.Count != 0)
                {
                    foreach (var item in reportList)
                    {
                        foreach (var item2 in item.Value)
                        {
                            returnList2.Add(item2);
                        }
                    }
                }
                if (returnList2.Count == 0)
                {
                    var bosGecmisKonumRaporuObject = new GecmisKonumRaporuObject();
                    returnList2.Add(bosGecmisKonumRaporuObject);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                dt.Columns.Remove("Grup");
                var fileName = $"GecmisKonumRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("GecmisKonumRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;

                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "GecmisKonumRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }
            }
            if (_fileTypeGKR == 2)
            {
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateGKR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateGKR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Geçmiş Konum", "_GecmisKonumRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_GecmisKonumRaporuPDF", reportList);
            }
            //if (_showOrDownload == 2)
            //{
            //    return PartialView("_GecmisKonumRaporuPDF", reportList);
            //}
            //var html = RenderViewAsString("_GecmisKonumRaporuPDF", reportList);
            //var file = HtmlToPdf(html);
            //byte[] fileBytes = System.IO.File.ReadAllBytes(file);
            //if (_showOrDownload == 1)
            //{
            //    return File(fileBytes, "application/pdf", file);
            //}
            return View();
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportSonKonumRaporu(int _groupIdSKR, int _fileTypeSKR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            // File Type: 1 ise Excel ,  2 ise PDF
            var reportList = ReportDB.GetInstance().SonKonumRaporu(_groupIdSKR, userObj);
            if (reportList == null)
            {
                reportList = new Dictionary<string, List<SonKonumRaporuObjectRepo>>();
            }
            if (_fileTypeSKR == 1)
            {
                List<SonKonumRaporuObjectRepo> returnList2 = new List<SonKonumRaporuObjectRepo>();
                if (reportList.Count != 0)
                {
                    foreach (var item in reportList)
                    {
                        foreach (var item2 in item.Value)
                        {
                            returnList2.Add(item2);
                        }
                    }
                }
                if (returnList2.Count == 0)
                {
                    var bosSonKonumRaporuObject = new SonKonumRaporuObjectRepo()
                    {
                        Grup = "-",
                        Konum = "-",
                        MobilTanimi = "-",
                        Tarih = "-",
                        ToplamKm = "-"
                    };
                    returnList2.Add(bosSonKonumRaporuObject);
                }
                var fileName = $"SonKonumRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("SonKonumRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;

                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "SonKonumRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }
            }

            if (_fileTypeSKR == 2)
            {
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Son Konum", "_SonKonumRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_SonKonumRaporuPDF", reportList);
            }

            return View();
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportMesafeOzetRaporu(string _startDateMOR, string _endDateMOR, string[] _vehiclesMOR, int _fileTypeMOR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            // File Type: 1 ise Excel ,  2 ise PDF
            var reportList = ReportDB.GetInstance().MesafeOzetRaporu(_startDateMOR, _endDateMOR, _vehiclesMOR, userObj);
            if (reportList == null)
            {
                reportList = new Dictionary<MORKeyClass, List<MesafeOzetRaporuObjectRepo>>();
            }
            if (_fileTypeMOR == 1)
            {
                List<MesafeOzetRaporuObject> returnList2 = new List<MesafeOzetRaporuObject>();
                if (reportList.Count != 0)
                {
                    foreach (var item in reportList)
                    {
                        foreach (var item2 in item.Value)
                        {
                            returnList2.Add(item2);
                        }
                    }
                }
                if (returnList2.Count == 0)
                {
                    var bosMesafeOzetRaporuObject = new MesafeOzetRaporuObject();
                    returnList2.Add(bosMesafeOzetRaporuObject);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                dt.Columns.Remove("CreateDate");
                dt.Columns.Remove("DistanceBetweenTwoPackages");
                dt.Columns.Remove("Latitude");
                dt.Columns.Remove("Longtitude");
                dt.Columns["Gun"].ColumnName = "Gün";
                dt.Columns["Arac"].ColumnName = "Araç Plakası";
                dt.Columns["MesaiDisiKm"].ColumnName = "Mesai Dışı Km";
                dt.Columns["MesafeKm"].ColumnName = "Mesafe Km";
                dt.Columns["BolgeDisiMesafeKm"].ColumnName = "Bölge Dışı Km";

                var fileName = $"MesafeOzetRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("MesafeOzetRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;


                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "MesafeOzetRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }
            }
            if (_fileTypeMOR == 2)
            {
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateMOR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateMOR).ToString($"yyyy-MM-dd {dateTimeHour}"));

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Mesafe Özeti", "_MesafeOzetRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_MesafeOzetRaporuPDF", reportList);
            }
            //if (_showOrDownload == 2)
            //{
            //    return PartialView("_MesafeOzetRaporuPDF", reportList);
            //}
            //var html = RenderViewAsString("_MesafeOzetRaporuPDF", reportList);
            //var file = HtmlToPdf(html);
            //byte[] fileBytes = System.IO.File.ReadAllBytes(file);
            //if (_showOrDownload == 1)
            //{
            //    return File(fileBytes, "application/pdf", file);
            //}
            return View();
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportGiseGecisRaporu(string _startDateGGR, string _endDateGGR, string[] _vehiclesGGR, int _fileTypeGGR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            // File Type: 1 ise Excel ,  2 ise PDF
            var reportList = ReportDB.GetInstance().AracGiseGecisRaporu(_startDateGGR, _endDateGGR, _vehiclesGGR);
            if (reportList == null)
            {
                reportList = new Dictionary<GGRKeyClass, List<GiseGecisRaporuTemp>>();
            }
            if (_fileTypeGGR == 1)
            {
                var returnList2 = new List<GiseGecisRaporuTemp>();
                foreach (var item in reportList)
                {
                    foreach (var item2 in item.Value)
                    {
                        returnList2.Add(item2);
                    }
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                //dt.Columns.Add("Giriş Zamanı", typeof(string));
                //dt.Columns.Add("Çıkış Zamanı", typeof(string));

                //if (returnList2.Count > 0)
                //{
                //    for (int i = 0; i < returnList2.Count; i++)
                //    {
                //        var gZamani = dt.Rows[i]["GirisZamani"].ToString();
                //        dt.Rows[i]["Giriş Zamanı"] = gZamani;
                //        var cZamani = dt.Rows[i]["CikisZamani"].ToString();
                //        dt.Rows[i]["Çıkış Zamanı"] = cZamani;
                //    }
                //}

                if (returnList2.Count > 0)
                {
                    dt.Columns.Add("Giriş Zamanı", typeof(string));
                    for (int i = 0; i < returnList2.Count; i++)
                    {
                        var gZamani = dt.Rows[i]["Zaman"].ToString();
                        dt.Rows[i]["Giriş Zamanı"] = gZamani;
                    }
                    dt.Columns.Remove("Zaman");
                }

                if (returnList2.Count == 0)
                {
                    var emptyRepo = new GiseGecisRaporuTemp();
                    returnList2.Add(emptyRepo);
                    dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                    //dt.Columns.Add("Giriş Zamanı", typeof(string));
                    //dt.Columns.Add("Çıkış Zamanı", typeof(string));
                }
                dt.Columns.Remove("DirectionDegree");
                //dt.Columns["GecisSuresi"].ColumnName = "Geçiş Süresi";
                //dt.Columns["GirisGisesi"].ColumnName = "Giriş Gişesi";
                //dt.Columns["CikisGisesi"].ColumnName = "Çıkış Gişesi";
                //dt.Columns.Remove("GirisDirectionDegree");
                //dt.Columns.Remove("CikisDirectionDegree");
                //dt.Columns.Remove("GirisZamani");
                //dt.Columns.Remove("CikisZamani");

                var fileName = $"AracGiseGecisRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("AracGiseGecisRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;


                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "AracGiseGecisRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }
            }
            if (_fileTypeGGR == 2)
            {
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateGGR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateGGR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Araç Gişe Geçiş", "_GiseGecisRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_GiseGecisRaporuPDF", reportList);
            }
            return View();
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportBolgeVarisAyrilisRaporu(string _startDateBVAR, string _endDateBVAR, int _companyAreaBVAR, string[] _vehiclesBVAR, int _fileTypeBVAR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            var reportList = ReportDB.GetInstance().AracBolgeVarisAyrilisRaporu(_startDateBVAR, _endDateBVAR, _companyAreaBVAR, _vehiclesBVAR, userObj);
            if (reportList.Values.Count == 0)
                reportList = new Dictionary<BVARKeyClass, List<BolgeVarisAyrilisRaporuObject>>();
            if (_fileTypeBVAR == 1)
            {
                List<BolgeVarisAyrilisRaporuObject> returnList2 = new List<BolgeVarisAyrilisRaporuObject>();
                foreach (var item in reportList)
                {
                    foreach (var item2 in item.Value)
                    {
                        returnList2.Add(item2);
                    }
                }
                if (returnList2.Count == 0)
                {
                    var emptyObject = new BolgeVarisAyrilisRaporuObject();
                    returnList2.Add(emptyObject);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                dt.Columns["VarisZamani"].ColumnName = "Varış Zamanı";
                dt.Columns["AyrilisZamani"].ColumnName = "Ayrılış Zamanı";
                dt.Columns["BeklemeDk"].ColumnName = "Bekleme Dk";
                dt.Columns["MesafeKm"].ColumnName = "Mesafe Km";
                dt.Columns["Bolge"].ColumnName = "Bölge";

                var fileName = $"BolgeVarisAyrilisRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("BolgeVarisAyrilisRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;

                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "BolgeVarisAyrilisRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }

            }
            if (_fileTypeBVAR == 2)
            {
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateBVAR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateBVAR).ToString($"yyyy-MM-dd {dateTimeHour}"));

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Bölge Varış Ayrılış", "_BolgeVarisAyrilisRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_BolgeVarisAyrilisRaporuPDF", reportList);
            }
            return View();
        }
        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportDurakRaporu(string _startDateDR, string _endDateDR, string[] _vehiclesDR, int _fileTypeDR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            var reportList = ReportDB.GetInstance().AracDurakRaporu(_startDateDR, _endDateDR, _vehiclesDR, userObj);
            if (reportList.Values.Count == 0)
                reportList = new Dictionary<DRKeyClass, List<DurakRaporuObject>>();
            if (_fileTypeDR == 1)
            {
                List<DurakRaporuObject> returnList2 = new List<DurakRaporuObject>();
                foreach (var item in reportList)
                {
                    foreach (var item2 in item.Value)
                    {
                        returnList2.Add(item2);
                    }
                }
                if (returnList2.Count == 0)
                {
                    var emptyObject = new DurakRaporuObject();
                    returnList2.Add(emptyObject);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                dt.Columns["SeferSayisi"].ColumnName = "Sefer Sayısı";
                dt.Columns["Driver"].ColumnName = "Şoför";

                var fileName = $"DurakRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("DurakRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;

                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "DurakRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }

            }
            if (_fileTypeDR == 2)
            {
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateDR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateDR).ToString($"yyyy-MM-dd {dateTimeHour}"));

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Durak", "_DurakRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_DurakRaporuPDF", reportList);
            }
            return View();
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportRotaVarisAyrilisRaporu(string _startDateRVAR, string _endDateRVAR, int _companyRouteRVAR, string[] _vehiclesRVAR, int _fileTypeRVAR, int _downloadOrMail, string _emailToSendReport)
        {
            if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
            {
                return Json(false);
            }
            var reportList = ReportDB.GetInstance().AracRotaVarisAyrilisRaporu(_startDateRVAR, _endDateRVAR, _companyRouteRVAR, _vehiclesRVAR, userObj);
            if (reportList.Values.Count == 0)
                reportList = new Dictionary<RVARKeyClass, List<RotaVarisAyrilisRaporuObject>>();
            if (_fileTypeRVAR == 1)
            {
                List<RotaVarisAyrilisRaporuObject> returnList2 = new List<RotaVarisAyrilisRaporuObject>();
                foreach (var item in reportList)
                {
                    foreach (var item2 in item.Value)
                    {
                        returnList2.Add(item2);
                    }
                }
                if (returnList2.Count == 0)
                {
                    var emptyObject = new RotaVarisAyrilisRaporuObject();
                    returnList2.Add(emptyObject);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                dt.Columns["AracAdi"].ColumnName = "Araç Adı";
                dt.Columns["AyrilisZamani"].ColumnName = "Ayrılış Zamanı";

                var fileName = $"RotaVarisAyrilisRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("RotaVarisAyrilisRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;

                if (_downloadOrMail == 1)
                {
                    var url = "/Content/" + fileName;
                    return Json(url);
                    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailExcel(dt, "RotaVarisAyrilisRaporu", fileName, _emailToSendReport);
                    return Json(true);
                }

            }
            if (_fileTypeRVAR == 2)
            {
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateRVAR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateRVAR).ToString($"yyyy-MM-dd {dateTimeHour}"));

                if (_downloadOrMail == 2)
                {
                    SendReportWithEmailPDF(reportList, "Rota Varış Ayrılış", "_RotaVarisAyrilisRaporuPDF", _emailToSendReport);
                    return Json(true);
                }
                return PartialView("_RotaVarisAyrilisRaporuPDF", reportList);
            }
            return View();
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportKontakDurumRaporu(string _startDateKDR, string _endDateKDR, string[] _vehiclesKDR, int _fileTypeKDR, int _downloadOrMail, string _emailToSendReport)
        {
            try
            {
                if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
                {
                    return Json(false);
                }
                // File Type== 1: Excel , 2 :PDF
                //Download Or Mail == 1:download , 2 :mail
                var reportList = ReportDB.GetInstance().KontakDurumRaporu(_startDateKDR, _endDateKDR, _vehiclesKDR);
                if (reportList == null)
                {
                    reportList = new Dictionary<KDRKeyClass, List<KontakDurumRaporuToShow>>();
                }
                if (_fileTypeKDR == 1)
                {
                    List<KontakDurumRaporuToShow> returnList2 = new List<KontakDurumRaporuToShow>();
                    if (reportList.Count != 0)
                    {
                        foreach (var item in reportList)
                        {
                            foreach (var item2 in item.Value)
                            {
                                returnList2.Add(item2);
                            }
                        }
                    }
                    if (returnList2.Count == 0)
                    {
                        var bosSeferOzetiObjectRepo = new KontakDurumRaporuToShow();
                        returnList2.Add(bosSeferOzetiObjectRepo);
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                    dt.Columns["MobilTanimi"].ColumnName = "Mobil Tanımı";
                    dt.Columns["KontakDurumu"].ColumnName = "Kontak Durumu";
                    var fileName = $"KontakDurumRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("KontakDurumRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        sheet.Protection.IsProtected = false;
                        sheet.Protection.AllowSelectLockedCells = false;
                        package.SaveAs(new FileInfo(rootPath));
                    }

                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        var url = "/Content/" + fileName;
                        return Json(url);
                        //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }

                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailExcel(dt, "KontakDurumRaporu", fileName, _emailToSendReport);
                        return Json(true);
                    }
                }

                //if (_showOrDownload == 2)
                //{
                //    return PartialView("_SeferOzetiRaporuPDF", reportList);
                //}
                //var html = RenderViewAsString("_SeferOzetiRaporuPDF", reportList);
                //var file = HtmlToPdf(html);
                //byte[] fileBytes = System.IO.File.ReadAllBytes(file);
                //if (_showOrDownload == 1)
                //{
                //    return File(fileBytes, "application/pdf", file);
                //}
                //return File(file, "application/pdf");
                if (_fileTypeKDR == 2)
                {
                    var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                    ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                    ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateKDR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateKDR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailPDF(reportList, "KontakDurumu", "_KontakDurumRaporuPDF", _emailToSendReport);
                        return Json(true);
                    }
                    return PartialView("_KontakDurumRaporuPDF", reportList);
                }
                return View();
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportSeferOlayRaporu(string _startDateSOLR, string _endDateSOLR, string[] _vehiclesSOLR, int _fileTypeSOLR, int _downloadOrMail, string _emailToSendReport)
        {
            try
            {
                if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
                {
                    return Json(false);
                }
                var reportList = ReportDB.GetInstance().SeferOlayRaporu(_startDateSOLR, _endDateSOLR, _vehiclesSOLR);
                if (reportList == null)
                {
                    reportList = new Dictionary<SOLRKeyClass, List<SeferOlayıRaporuObjectRepo>>();
                }
                if (_fileTypeSOLR == 1)
                {
                    List<SeferOlayıRaporuObjectRepo> returnList2 = new List<SeferOlayıRaporuObjectRepo>();
                    if (reportList.Count != 0)
                    {
                        foreach (var item in reportList)
                        {
                            foreach (var item2 in item.Value)
                            {
                                returnList2.Add(item2);
                            }
                        }
                    }
                    if (returnList2.Count == 0)
                    {
                        var bosSeferOlayıObjectRepo = new SeferOlayıRaporuObjectRepo();
                        returnList2.Add(bosSeferOlayıObjectRepo);
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                    var fileName = $"SeferOlayıRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                    dt.Columns["Arac"].ColumnName = "Araç Plakası";
                    dt.Columns["OlayZamani"].ColumnName = "Olay Zamanı";
                    dt.Columns["Aciklama"].ColumnName = "Açıklama";
                    dt.Columns["Surucu"].ColumnName = "Sürücü";
                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("SeferOlayıRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        sheet.Protection.IsProtected = false;
                        sheet.Protection.AllowSelectLockedCells = false;
                        package.SaveAs(new FileInfo(rootPath));
                    }

                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        var url = "/Content/" + fileName;
                        return Json(url);
                        //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }

                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailExcel(dt, "SeferOlayıRaporu", fileName, _emailToSendReport);
                        return Json(true);
                    }
                }
                if (_fileTypeSOLR == 2)
                {
                    var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                    ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                    ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateSOLR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateSOLR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailPDF(reportList, "SeferOlayı", "_SeferOlayıRaporuPDF", _emailToSendReport);
                        return Json(true);
                    }
                    return PartialView("_SeferOlayıRaporuPDF", reportList);
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportHizAsimiRaporu(string _startDateHAR, string _endDateHAR, string[] _vehiclesHAR, int _fileTypeHAR, int _downloadOrMail, string _emailToSendReport)
        {
            try
            {
                if (!String.IsNullOrEmpty(_emailToSendReport) && !_emailToSendReport.Contains('@'))
                {
                    return Json(false);
                }
                var reportList = ReportDB.GetInstance().HizAsimiRaporu(_startDateHAR, _endDateHAR, _vehiclesHAR);
                if (reportList == null)
                {
                    reportList = new Dictionary<HARKeyClass, List<HizAsimiRaporuObjectRepo>>();
                }
                if (_fileTypeHAR == 1)
                {
                    List<HizAsimiRaporuObjectRepo> returnList2 = new List<HizAsimiRaporuObjectRepo>();
                    if (reportList.Count != 0)
                    {
                        foreach (var item in reportList)
                        {
                            foreach (var item2 in item.Value)
                            {
                                returnList2.Add(item2);
                            }
                        }
                    }
                    if (returnList2.Count == 0)
                    {
                        var bosHizAsimiObjectRepo = new HizAsimiRaporuObjectRepo();
                        returnList2.Add(bosHizAsimiObjectRepo);
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                    var fileName = $"HizAsimiRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                    dt.Columns["Arac"].ColumnName = "Araç Plakası";
                    dt.Columns["OlayZamani"].ColumnName = "Olay Zamanı";
                    dt.Columns["Aciklama"].ColumnName = "Açıklama";
                    dt.Columns["Surucu"].ColumnName = "Sürücü";
                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("HizAsimiRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        sheet.Protection.IsProtected = false;
                        sheet.Protection.AllowSelectLockedCells = false;
                        package.SaveAs(new FileInfo(rootPath));
                    }

                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        var url = "/Content/" + fileName;
                        return Json(url);
                        //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }

                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailExcel(dt, "HizAsimiRaporu", fileName, _emailToSendReport);
                        return Json(true);
                    }
                }
                if (_fileTypeHAR == 2)
                {
                    var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                    ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                    ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDateHAR).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDateHAR).ToString($"yyyy-MM-dd {dateTimeHour}"));
                    if (_downloadOrMail == 2)
                    {
                        SendReportWithEmailPDF(reportList, "HizAsimi", "_HizAsimiRaporuPDF", _emailToSendReport);
                        return Json(true);
                    }
                    return PartialView("_HizAsimiRaporuPDF", reportList);
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportSeferOlayRaporuEXCEL(int _vehicleIdO, int _groupIdO, int _eventTypeO, string _startDateO, string _endDateO)
        {
            try
            {
                var reportList = ReportDB.GetInstance().GetEventListToSearch(_vehicleIdO, _groupIdO, _eventTypeO, _startDateO, _endDateO);
                if (reportList == null)
                {
                    reportList = new List<SeferOlayıRaporuListObjectRepo>();
                }
                List<SeferOlayıRaporuListObjectRepo> returnList2 = new List<SeferOlayıRaporuListObjectRepo>();
                if (reportList.Count != 0)
                {
                    foreach (var item in reportList)
                    {
                        returnList2.Add(item);
                    }
                }
                if (returnList2.Count == 0)
                {
                    var bosSeferOlayıObjectRepo = new SeferOlayıRaporuListObjectRepo();
                    returnList2.Add(bosSeferOlayıObjectRepo);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                dt.Columns["Arac"].ColumnName = "Araç Plakası";
                dt.Columns["OlayZamani"].ColumnName = "Olay Zamanı";
                dt.Columns["OlayTipi"].ColumnName = "Olay Tipi";
                dt.Columns["OlayDetayı"].ColumnName = "Olay Detayı";
                var stream = new MemoryStream();
                var fileName = $"SeferOlayıRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("SeferOlayıRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;
                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                var url = "/Content/" + fileName;
                return Json(url);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetDevicesByCarPlateNumber(string term)
        {
            try
            {
                if (String.IsNullOrEmpty(term))
                    return Json(false);
                var returnList = DeviceDB.GetInstance().GetDevicesByCarPlateNumber(term, userObj);
                if (returnList == null || returnList.Count == 0)
                    return Json(false);
                returnList = returnList.Where(x => x.Plaka.Trim().ToLower().StartsWith(term.Trim().ToLower())).ToList();
                var carPlateNumbersAndLocations = returnList.Select(x => x.Plaka + "/ " + x.Konum).ToList();
                return Json(carPlateNumbersAndLocations);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetLatLongFromVehicleName(string _carPlateNumber)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var plaka = "";
                    if (!String.IsNullOrEmpty(_carPlateNumber))
                    {
                        plaka = _carPlateNumber.Split("/")[0];
                    }
                    var device = context.Device.FirstOrDefault(x => x.CarPlateNumber.Trim().ToLower().StartsWith(plaka.Trim().ToLower()));
                    if (device == null)
                    {
                        return Json(false);
                    }
                    if (device.LastDeviceDataId != null)
                    {
                        var deviceLatLong = context.DeviceData.LastOrDefault(x => x.Id == device.LastDeviceDataId);
                        DeviceInfoForMap deviceLastLocationLatLong = new DeviceInfoForMap()
                        {
                            lat = deviceLatLong.Latitude,
                            lng = deviceLatLong.Longtitude,
                            CarPlateNumber = device.CarPlateNumber,
                            IoStatus = deviceLatLong.IoStatus,
                            KmPerHour = deviceLatLong.KmPerHour,
                            TotalKmDaily = Convert.ToDecimal(GetTotalKMBetweenTwoDateOfADevice2(device.Id)),
                            LastLocationTime = GetStringLastLocationTimeOfDevice(device.Id)/*.Replace("T", " ").Replace("-", ".")*/,
                            LastDeviceId = device.Id,
                            Konum = DeviceDB.GetInstance().GetLocationFromLatLon2(deviceLatLong.LocationId),
                            DeviceId = deviceLatLong.DeviceId,
                            ActivityTime = Convert.ToInt32(device.ActivityTime) > (DateTime.Now - Convert.ToDateTime(deviceLatLong.CreateDate)).TotalMinutes ? "Aktif" : "Pasif"
                        };
                        return Json(deviceLastLocationLatLong);
                    }
                    return Json(false);
                }
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public JsonResult DisplayVehicleOnMap(int _deviceId)
        {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceById(_deviceId);
                var lastDeviceDataId = device.LastDeviceDataId == null ? 0 : device.LastDeviceDataId;
                if (lastDeviceDataId != 0)
                {
                    var lastDeviceData = DeviceDataDB.GetInstance().GetDeviceDataById((int)lastDeviceDataId);
                    DeviceInfoForMap deviceInfoForMap = new DeviceInfoForMap()
                    {
                        DeviceId = device.Id,
                        lat = lastDeviceData.Latitude,
                        lng = lastDeviceData.Longtitude,
                        CarPlateNumber = device.CarPlateNumber,
                        Altitude = lastDeviceData.Altitude,
                        Date = lastDeviceData.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        IoStatus = lastDeviceData.IoStatus.EndsWith("1") ? "Açık" : "Kapalı",
                        ActivityTime = device.ActivityTime > (DateTime.Now - Convert.ToDateTime(lastDeviceData.CreateDate)).TotalMinutes ? "Aktif" : "Pasif",
                        TotalKmDaily = lastDeviceData.TotalKm,
                        KmPerHour = lastDeviceData.KmPerHour,
                        Location = DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.LocationId),
                        DirectionDegree = Convert.ToInt16(lastDeviceData.DirectionDegree)
                    };
                    return Json(deviceInfoForMap);
                }
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult DisplayOnMapWithSelected(List<int> _selected)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    List<DeviceInfoForMap> deviceList = new List<DeviceInfoForMap>();
                    for (var i = 0; i < _selected.Count; i++)
                    {
                        var selectedDevice = context.Device.Where(x => x.Id == _selected[i]).ToList();
                        foreach (var item in selectedDevice)
                        {
                            var device = DeviceDB.GetInstance().GetDeviceById(item.Id);
                            var lastDeviceDataId = device.LastDeviceDataId == null ? 0 : device.LastDeviceDataId;
                            if (lastDeviceDataId != 0)
                            {
                                var lastDeviceData = DeviceDataDB.GetInstance().GetDeviceDataById((int)lastDeviceDataId);
                                DeviceInfoForMap deviceInfoForMap = new DeviceInfoForMap()
                                {
                                    DeviceId = device.Id,
                                    lat = lastDeviceData.Latitude,
                                    lng = lastDeviceData.Longtitude,
                                    CarPlateNumber = device.CarPlateNumber,
                                    Altitude = lastDeviceData.Altitude,
                                    Date = Convert.ToString(lastDeviceData.CreateDate),
                                    IoStatus = lastDeviceData.IoStatus.EndsWith("1") ? "Açık" : "Kapalı",
                                    ActivityTime = device.ActivityTime > (DateTime.Now - Convert.ToDateTime(lastDeviceData.CreateDate)).TotalMinutes ? "Aktif" : "Pasif",
                                    TotalKmDaily = lastDeviceData.TotalKm,
                                    KmPerHour = lastDeviceData.KmPerHour,
                                    Location = DeviceDB.GetInstance().GetLocationFromLatLon2(lastDeviceData.LocationId),
                                    DirectionDegree = Convert.ToInt16(lastDeviceData.DirectionDegree)
                                };
                                deviceList.Add(deviceInfoForMap);
                            }
                        }
                    }
                    return Json(deviceList);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public IActionResult GetAreaListOfCompany(string _areaNameTLA, int _areaTypeTLA)
        {
            try
            {
                _areaNameTLA = _areaNameTLA == null ? "" : _areaNameTLA;
                var areaList = AreaDB.GetInstance().GetAreaListOfCompany(_areaNameTLA, _areaTypeTLA, userObj);
                if (areaList != null)
                    return PartialView("_CompanyAreaList", areaList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult GetRouteListOfCompany(string _routeNameTLA)
        {
            try
            {
                _routeNameTLA = _routeNameTLA == null ? "" : _routeNameTLA;
                var routeList = RouteDB.GetInstance().GetRouteListOfCompany(_routeNameTLA, userObj);
                if (routeList != null)
                    return PartialView("_CompanyRouteList", routeList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult DeleteCompanyArea(int _id)
        {
            try
            {
                var area = AreaDB.GetInstance().DeleteArea(_id);
                if (area != null)
                {
                    AreaDeviceDB.GetInstance().DeleteAreaDevices(area.Id);
                    CompanyAreaDB.GetInstance().DeleteCompanyAreaWithAreaId(area.Id);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult DeleteCompanyRoute(int _id)
        {
            try
            {
                var route = RouteDB.GetInstance().DeleteRoute(_id);
                if (route != null)
                {
                    RouteDeviceDB.GetInstance().DeleteRouteDevices(route.Id);
                    CompanyRouteDB.GetInstance().DeleteCompanyRouteWithRouteId(route.Id);
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetCompanyAreaById(int _id)
        {
            try
            {
                var area = AreaDB.GetInstance().GetAreaById(_id);
                if (area != null)
                    return Json(area);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetCompanyRouteById(int _id)
        {
            try
            {
                var route = RouteDB.GetInstance().GetRouteById(_id);
                if (route != null)
                    return Json(route);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetDeviceById(int _id)
        {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceById(_id);
                if (device != null)
                    return Json(device);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetUsersAssignedReports()
        {
            try
            {
                var userIsAdmin = CompanyUserDB.GetInstance().GetCompanyUserByUserId(userObj.Id).IsCompanyAdmin;
                if (userIsAdmin)
                {
                    var userReportIsAssigned = new UserReportIsAssigned()
                    {
                        AracGiseRaporu = true,
                        BolgeGirisCikisRaporu = true,
                        GecmisKonumRaporu = true,
                        MesafeOzetRaporu = true,
                        SeferOlayRaporu = true,
                        SeferOzetiRaporu = true,
                        SonKonumRaporu = true,
                        KontakDurumRaporu = true,
                        RotaGirisCikisRaporu = true,
                        HizAsimiRaporu = true,
                        DurakRaporu = true,
                    };
                    return Json(userReportIsAssigned);
                }
                var usersAssignedReports = UserReportDB.GetInstance().GetUsersAssignedReports(userObj);
                return Json(usersAssignedReports);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        /* public int GetTotalKMBetweenTwoDateOfADevice(int _deviceId, string _startDate, string _endDate)
         {
             using (var context = new VeraEntities())
             {
                 using (var con = context.Database.GetDbConnection())
                 {
                     try
                     {
                         if (con.State != ConnectionState.Open)
                             con.Open();

                         using (var cmd = con.CreateCommand())
                         {
                             cmd.CommandText = $@"SELECT SUM(DistanceBetweenTwoPackages) / 1000 FROM DeviceData
                                                  WHERE CreateDate BETWEEN '{_startDate}' AND '{_endDate}'
                                                  AND DeviceId = {_deviceId}";
                             cmd.CommandTimeout = Int32.MaxValue;
                             context.Database.SetCommandTimeout(Int32.MaxValue);

                             var reader = cmd.ExecuteScalar().ToString();
                             if(reader==null || reader == "")
                             {
                                 var intResult2= 0;
                                 return intResult2;
                             }
                             var intResult = Convert.ToInt32(reader);
                             return intResult;
                         }
                     }
                     catch (Exception exc)
                     {
                         throw exc;
                     }
                     finally
                     {
                         if (con.State != ConnectionState.Closed)
                             con.Close();
                     }
                 }
             }
         }*/
        public IActionResult ExportVehicleInformation()
        {
            using (var context = new VeraEntities())
            {

                List<DeviceObjectForExcellFrontSide> deviceList = DeviceDB.GetInstance().GetDevicesInformationForExcel(userObj);
                foreach (var item in deviceList)
                {
                    var device = DeviceDB.GetInstance().GetDeviceById(item.LastDeviceId);
                    var lastDeviceData = context.DeviceData.LastOrDefault(x => x.Id == device.LastDeviceDataId);
                    item.Aktiflik = Convert.ToInt32((DateTime.Now.AddMinutes(3) - Convert.ToDateTime(item.LastDeviceDataCreateDate)).TotalMinutes) > device.ActivityTime ? "Kapali" : "Acik";
                    item.KontakDurumu = item.KontakDurumu.EndsWith("1") ? "Açık" : "Kapalı";
                    item.KonumZamani = item.KonumZamani.Replace("T", " ");
                }

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(deviceList));
                dt.Columns.Remove("LastDeviceId");
                dt.Columns.Remove("LastDeviceDataCreateDate");

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Araç Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"AraçBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportVehicles()
        {
            using (var context = new VeraEntities())
            {

                List<DeviceObjectForExcellVehicles> deviceList = DeviceDB.GetInstance().GetVehiclesForExcel(userObj);
                foreach (var item in deviceList)
                {
                    var device = DeviceDB.GetInstance().GetDeviceById(item.LastDeviceId);
                    var lastDeviceData = context.DeviceData.LastOrDefault(x => x.Id == device.LastDeviceDataId);
                    string totalHours = item.ActivityTime == null ? "" : (DateTime.Now - Convert.ToDateTime(item.ActivityTime)).TotalHours.ToString();
                    item.WorkingTime = item.WorkingTime == null ? "" : item.WorkingTime.Replace("T", " ").Substring(0, 19);
                    item.ActivityTime = item.ActivityTime == null ? "" : ReportDB.GetInstance().TotalHoursToHumanReadableString(totalHours.ToString());
                    item.MontageType = item.MontageType == null ? "" : item.MontageType.Equals("1") ? "Monte" : "Demonte";
                    item.ActivityStatus = item.ActivityStatus.EndsWith("1") ? "Aktif" : "Pasif";
                    item.Bloke = item.Bloke == "true" ? "Aktif" : "Pasif";
                    if (item.VehicleType == "0")
                        item.VehicleType = "";
                    if (item.VehicleType == "1")
                        item.VehicleType = "Otomobil";
                    if (item.VehicleType == "2")
                        item.VehicleType = "Motosiklet";
                    if (item.VehicleType == "4")
                        item.VehicleType = "Taksi";
                    if (item.VehicleType == "5")
                        item.VehicleType = "Minibüs";
                    item.SpeedType = item.VehicleType;
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(deviceList));
                dt.Columns.Remove("LastDeviceId");
                dt.Columns.Remove("LastDeviceDataCreateDate");
                dt.Columns["WorkingTime"].ColumnName = "Çalışma Süresi";
                dt.Columns["CarPlateNumber"].ColumnName = "Araç Adı";
                dt.Columns["CompanyName"].ColumnName = "Firma Adı";
                dt.Columns["DeviceGSMNo"].ColumnName = "Cihaz GSM No";
                dt.Columns["DeviceId"].ColumnName = "Cihaz No";
                dt.Columns["VehicleType"].ColumnName = "Araç Tipi";
                dt.Columns["IconType"].ColumnName = "İkon Tipi";
                dt.Columns["SpeedType"].ColumnName = "Kanuni Hız Tipi";
                dt.Columns["MontageType"].ColumnName = "Montaj Tipi";
                dt.Columns["ActivityStatus"].ColumnName = "Aktiflik";
                dt.Columns["Bloke"].ColumnName = "Motor Bloke Durumu";
                dt.Columns["ActivityTime"].ColumnName = "Aktiflik Zamanı";
                dt.Columns["LastKmValue"].ColumnName = "Son KM Değeri";
                dt.Columns["Mail"].ColumnName = "Mail";
                dt.Columns["PositionTime"].ColumnName = "Pozisyon Süresi";
                dt.Columns["PositionDistance"].ColumnName = "Pozisyon Mesafesi";
                dt.Columns["SpeedLimit"].ColumnName = "Hız Limiti";
                dt.Columns["WaitingTime"].ColumnName = "Rölanti Süresi";
                dt.Columns["MontageDate"].ColumnName = "Montaj Tarihi";
                dt.Columns["Description"].ColumnName = "Teknik Not";
                var stream = new MemoryStream();
                var fileName = $"AraçBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("AraçBilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;
                var url = "/Content/" + fileName;
                return Json(url);
                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportUsersInfo()
        {
            using (var context = new VeraEntities())
            {
                var userReturnList = new List<UsersInfo>();
                var userList = UserDB.GetInstance().GetUserListOfCompany(userObj);
                if (userList == null || userList.Count == 0)
                {
                    return Json(false);
                }
                foreach (var item in userList)
                {
                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == item.Id);
                    userReturnList.Add(new UsersInfo()
                    {
                        UserCode = item.UserCode,
                        Name = item.Name,
                        Surname = item.Surname,
                        UserType = companyUser.IsCompanyAdmin == true ? "Admin" : "User",
                        Geo = item.GeographicalAuthorityId == 2 ? "Var" : "Yok",
                        Mail = item.Mail
                    });
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(userReturnList));
                dt.Columns["UserCode"].ColumnName = "Kullanıcı Adı";
                dt.Columns["Name"].ColumnName = "Adı";
                dt.Columns["Surname"].ColumnName = "Soyadı";
                dt.Columns["UserType"].ColumnName = "Kullanıcı Tipi";
                dt.Columns["Geo"].ColumnName = "Coğrafi Yetki";
                dt.Columns["Mail"].ColumnName = "E-mail";
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Kullanıcı Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"KullanıcıBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportEmployeesInfo()
        {
            using (var context = new VeraEntities())
            {
                var empReturnList = new List<EmployeesInfo>();
                var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == Convert.ToInt32(userObj.Id));
                var empList = EmployeeDB.GetInstance().GetCompanysEmployeeList(companyUser.CompanyId);
                if (empList != null && empList.Count > 0)
                {
                    foreach (var item in empList)
                    {
                        empReturnList.Add(new EmployeesInfo()
                        {
                            Name = item.Name,
                            Surname = item.Surname,
                            EmployeeNo = item.EmployeeNo == null ? "" : item.EmployeeNo,
                            BirthDate = item.BirthDate.ToString(),
                            PhoneNumber = item.PhoneNumber,
                            HomePhoneNumber = item.HomePhoneNumber,
                            Occupation = item.Occupation == null ? "" : item.Occupation
                        });
                    }
                }
                if (empList == null || empList.Count == 0)
                {
                    empReturnList.Add(new EmployeesInfo());
                }

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(empReturnList));
                dt.Columns.Remove("Id");
                dt.Columns.Remove("DriverLicenceNumber");
                dt.Columns.Remove("DriverLicenceClass");
                dt.Columns.Remove("DriverLicenceDate");
                dt.Columns.Remove("Address");
                dt.Columns.Remove("CompanyId");
                dt.Columns["Name"].ColumnName = "Görevli Adı";
                dt.Columns["Surname"].ColumnName = "Görevli Soyadı";
                dt.Columns["EmployeeNo"].ColumnName = "Görevli No";
                dt.Columns["BirthDate"].ColumnName = "Doğum Tarihi";
                dt.Columns["PhoneNumber"].ColumnName = "Telefon";
                dt.Columns["HomePhoneNumber"].ColumnName = "Ev Telefon";
                dt.Columns["Occupation"].ColumnName = "Görev Tipi";
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Görevli Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"GörevliBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportGroups()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var groupReturnList = new List<GroupsInfo>();
                    var groupList = GroupDB.GetInstance().GetAllCompanyGroupsByUserId(userObj);
                    if (groupList == null || groupList.Count == 0)
                        return Json(false);
                    foreach (var item in groupList)
                    {
                        groupReturnList.Add(new GroupsInfo()
                        {
                            Name = item.Name
                        });
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(groupReturnList));
                    dt.Columns["Name"].ColumnName = "Grup Adı";
                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("Grup Bilgileri");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        package.Save();
                    }
                    stream.Position = 0;
                    var fileName = $"GrupBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public IActionResult ExportGroupVehicles()
        {
            using (var context = new VeraEntities())
            {
                var groupReturnList = new List<GroupVehiclesInfo>();
                var groupList = GroupDB.GetInstance().GetAllCompanyGroupsByUserId(userObj);
                var groupListGroupIds = groupList.Select(x => x.Id).ToList();
                var groupDevices = context.GroupDevice.Where(x => groupListGroupIds.Contains(x.GroupId)).ToList();
                foreach (var item in groupDevices)
                {
                    var carPlateNumber = context.Device.FirstOrDefault(x => x.Id == item.DeviceId).CarPlateNumber;
                    var groupName = context.Group.FirstOrDefault(x => x.Id == item.GroupId).Name;
                    groupReturnList.Add(new GroupVehiclesInfo()
                    {
                        CarPlateNumber = carPlateNumber,
                        GroupName = groupName
                    });

                }
                if (groupReturnList == null || groupReturnList.Count == 0)
                    return Json(false);
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(groupReturnList));
                dt.Columns["GroupName"].ColumnName = "Grup Adı";
                dt.Columns["CarPlateNumber"].ColumnName = "Araç Adı";
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Grup Araç Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"GrupAraçBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportAreas()
        {
            using (var context = new VeraEntities())
            {
                var areaReturnList = new List<AreasInfo>();
                var areaList = AreaDB.GetInstance().GetAreaList(userObj);
                if (areaList == null && areaList.Count == 0)
                    return Json(false);
                foreach (var item in areaList)
                {
                    areaReturnList.Add(new AreasInfo()
                    {
                        Name = item.Name,
                        Type = item.IsRestricted == true ? "Normal" : "Yasaklı",
                        Color = "--"
                    });
                }

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(areaReturnList));
                dt.Columns["Name"].ColumnName = "Bölge Adı";
                dt.Columns["Type"].ColumnName = "Bölge Tipi";
                dt.Columns["Color"].ColumnName = "Renk";
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Bölge Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"BölgeBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportRoutes()
        {
            using (var context = new VeraEntities())
            {
                var routeReturnList = new List<RoutesInfo>();
                var routeList = RouteDB.GetInstance().GetRouteList(userObj);
                if (routeList == null && routeList.Count == 0)
                    return Json(false);
                foreach (var item in routeList)
                {
                    routeReturnList.Add(new RoutesInfo()
                    {
                        Name = item.Name
                    });
                }

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(routeReturnList));
                dt.Columns["Name"].ColumnName = "Rota Adı";
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Rota Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"RotaBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportAreasForDevice(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                var areaReturnList = new List<AreasInfo>();
                var areaList = AreaDeviceDB.GetInstance().GetAreaVehiclesByDeviceId(_deviceId);
                if (areaList != null && areaList.Count <= 0)
                {
                    return Json(false);
                }
                foreach (var item in areaList)
                {
                    areaReturnList.Add(new AreasInfo()
                    {
                        Name = item.Name
                    });
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(areaReturnList));
                dt.Columns["Name"].ColumnName = "Bölge Adı";
                dt.Columns.Remove("Type");
                dt.Columns.Remove("Color");
                var stream = new MemoryStream();
                var fileName = $"BölgeBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Bölge Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;
                var url = "/Content/" + fileName;
                return Json(url);
                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportRoutesForDevice(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                var routeReturnList = new List<RoutesInfo>();
                var routeList = RouteDeviceDB.GetInstance().GetRouteVehiclesByDeviceId(_deviceId);
                if (routeList != null && routeList.Count <= 0)
                {
                    return Json(false);
                }
                foreach (var item in routeList)
                {
                    routeReturnList.Add(new RoutesInfo()
                    {
                        Name = item.Name
                    });
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(routeReturnList));
                dt.Columns["Name"].ColumnName = "Rota Adı";
                var stream = new MemoryStream();
                var fileName = $"RotaBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                var rootPath = Path.Combine(_env.WebRootPath, "Content", fileName);
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Rota Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                    package.SaveAs(new FileInfo(rootPath));
                }
                stream.Position = 0;
                var url = "/Content/" + fileName;
                return Json(url);
                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ExportAP(int _groupIdAP)
        {
            using (var context = new VeraEntities())
            {
                var deviceAPReturnList = new List<DeviceListObjectAPExport>();
                var deviceList = DeviceDB.GetInstance().GetDeviceListAP(userObj, _groupIdAP);
                foreach (var item in deviceList)
                {
                    deviceAPReturnList.Add(new DeviceListObjectAPExport()
                    {
                        CarPlateNumber = item.CarPlateNumber,
                        PositionTime = item.PositionTime,
                        PositionDistance = item.PositionDistance,
                        SpeedLimit = item.SpeedLimit,
                        RolantiTime = item.RolantiTime,
                        LocationStatus = item.PositionTime == null || item.PositionTime == "0" ? "Programlanmadı" : "Programlandı",
                        SpeedStatus = item.SpeedLimit == null || item.SpeedLimit == "0" ? "Programlanmadı" : "Programlandı",
                        RolantiStatus = item.RolantiTime == null || item.RolantiTime == "0" ? "Programlanmadı" : "Programlandı"
                    });
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(deviceAPReturnList));
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("Araç Programlama Bilgileri");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"AracProgramlama_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public JsonResult GetSeferOzetiList(string[] _vehicleIdTL, string _startDateTL, string _endDateTL)
        {
            try
            {
                var reportList = ReportDB.GetInstance().SeferOzetiRaporu(_startDateTL, _endDateTL, _vehicleIdTL);
                var returnList = new List<SeferOzetiRaporuObjectRepo>();
                foreach (var item in reportList)
                {
                    foreach (var item2 in item.Value)
                    {
                        returnList.Add(item2);
                    }
                }
                if (returnList != null)
                    return Json(returnList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult GetEventListToSearch(int _vehicleIdO, int _groupIdO, int _eventTypeO, string _startDateO, string _endDateO)
        {
            try
            {
                var reportList = ReportDB.GetInstance().GetEventListToSearch(_vehicleIdO, _groupIdO, _eventTypeO, _startDateO, _endDateO);
                var returnList = new List<SeferOlayıRaporuListObjectRepo>();
                foreach (var item in reportList)
                {
                    returnList.Add(item);
                }
                if (returnList != null)
                    return Json(returnList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public IActionResult ShowGecmisKonumRaporu(int _time, string[] _deviceId)
        {
            try
            {
                var _endDate = Convert.ToString(DateTime.Now);
                var _startDate = Convert.ToString(DateTime.Now.AddHours(-_time));
                var pastLocationList = ReportDB.GetInstance().GecmisKonumRaporu(_startDate, _endDate, _deviceId);
                var returnList = new List<GecmisKonumRaporuObjectRepo>();
                foreach (var item in pastLocationList)  // Dictionaryden listi ayırmak için.
                {
                    foreach (var item2 in item.Value)
                    {
                        returnList.Add(item2);
                    }
                }
                if (returnList == null)
                    returnList = new List<GecmisKonumRaporuObjectRepo>();
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDate).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDate).ToString($"yyyy-MM-dd {dateTimeHour}"));
                return PartialView("_GecmisKonumRaporuPDF", pastLocationList);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public IActionResult ShowSeferOlayRaporu(int _time, string[] _deviceId)
        {
            try
            {
                var _endDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                var _startDate = DateTime.Now.AddHours(-_time).ToString("dd.MM.yyyy HH:mm:ss");
                var pastLocationList = ReportDB.GetInstance().SeferOlayRaporu(_startDate, _endDate, _deviceId);

                if (pastLocationList == null)
                    pastLocationList = new Dictionary<SOLRKeyClass, List<SeferOlayıRaporuObjectRepo>>();
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDate).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDate).ToString($"yyyy-MM-dd {dateTimeHour}"));
                return PartialView("_SeferOlayıRaporuPDF", pastLocationList);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public IActionResult ShowSeferOzetiRaporu(int _time, string[] _deviceId)
        {
            try
            {
                var _endDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                var _startDate = DateTime.Now.AddHours(-_time).ToString("dd.MM.yyyy HH:mm:ss");
                var pastLocationList = ReportDB.GetInstance().SeferOzetiRaporu(_startDate, _endDate, _deviceId);

                if (pastLocationList == null)
                    pastLocationList = new Dictionary<SORKeyClass, List<SeferOzetiRaporuObjectRepo>>();
                var dateTimeHour = DateTime.Now.ToString("HH:mm:ss");
                ViewBag.Tarih = Convert.ToString((DateTime.Now + "/" + userObj.UserCode));
                ViewBag.Sure = Convert.ToString(Convert.ToDateTime(_startDate).ToString("yyyy-MM-dd HH:mm:ss") + "/" + Convert.ToDateTime(_endDate).ToString($"yyyy-MM-dd {dateTimeHour}"));
                return PartialView("_SeferOzetiRaporuPDF", pastLocationList);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult DisplayDevicePastLocationOnModal(int _lastDeviceDataId)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var deviceData = context.DeviceData.FirstOrDefault(x => x.Id == _lastDeviceDataId);
                    var device = context.Device.FirstOrDefault(x => x.Id == deviceData.DeviceId);
                    var deviceInfoForMap = new DeviceInfoForMap()
                    {
                        lat = deviceData.Latitude,
                        lng = deviceData.Longtitude,
                        Altitude = deviceData.Altitude,
                        CarPlateNumber = device.CarPlateNumber,
                        DirectionDegree = deviceData.DirectionDegree,
                        IoStatus = deviceData.IoStatus.EndsWith("1") ? "Açık" : "Kapalı",
                        KmPerHour = deviceData.KmPerHour
                    };
                    if (deviceInfoForMap != null)
                        return Json(deviceInfoForMap);
                    return Json(false);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult DrawDevicePastLocationOnModal(string _startDateTLPL, string _endDateTLPL, int _deviceIdTLPL)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var returnList = new List<DeviceInfoForMap>();
                    _startDateTLPL += " 00:00:00";
                    _endDateTLPL += " 23:59:59";
                    var deviceDatas = context.DeviceData.Where(x => x.DeviceId == _deviceIdTLPL && x.CreateDate > Convert.ToDateTime(_startDateTLPL) && x.CreateDate < Convert.ToDateTime(_endDateTLPL)).ToList();
                    foreach (var item in deviceDatas)
                    {
                        returnList.Add(new DeviceInfoForMap()
                        {
                            lat = item.Latitude,
                            lng = item.Longtitude,
                            DirectionDegree = item.DirectionDegree
                        });
                    }
                    if (returnList != null)
                        return Json(returnList.OrderBy(x => x.LastLocationTime));
                    return Json(false);

                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetSeferOzetiLatLongList(int _deviceId, string _startTime, string _endTime)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var deviceDatas = context.DeviceData.Where(x => x.DeviceId == _deviceId && x.CreateDate > Convert.ToDateTime(_startTime) && x.CreateDate < Convert.ToDateTime(_endTime)).ToList();
                    var returnList = new List<DeviceInfoForMap>();
                    foreach (var item in deviceDatas)
                    {
                        returnList.Add(new DeviceInfoForMap()
                        {
                            lat = item.Latitude,
                            lng = item.Longtitude,
                            DirectionDegree = item.DirectionDegree
                        });
                    }
                    if (returnList != null)
                        return Json(returnList);
                    return Json(false);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public JsonResult AssignVehicleToArea(int _areaId, List<int> _selectedDeviceIds)
        {
            try
            {
                var deleteAreas = AreaDeviceDB.GetInstance().DeleteAreaDevices(_areaId);
                foreach (var deviceId in _selectedDeviceIds)
                {
                    var areaDevice = new AreaDevice()
                    {
                        DeviceId = deviceId,
                        AreaId = _areaId,
                        Status = 1
                    };
                    var insertResult = AreaDeviceDB.GetInstance().AddNewAreaDevice(areaDevice);
                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult AssignVehicleToRoute(int _routeId, List<int> _selectedDeviceIds)
        {
            try
            {
                var deleteRoutes = RouteDeviceDB.GetInstance().DeleteRouteDevices(_routeId);
                foreach (var deviceId in _selectedDeviceIds)
                {
                    var routeDevice = new RouteDevice()
                    {
                        DeviceId = deviceId,
                        RouteId = _routeId,
                        Status = 1
                    };
                    var insertResult = RouteDeviceDB.GetInstance().AddNewRouteDevice(routeDevice);
                    if (insertResult == null)
                    {
                        return Json(false);
                    }
                }
                return Json(true);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetAreaVehicles(int _areaId)
        {
            try
            {
                var vehicles = AreaDeviceDB.GetInstance().GetAreaVehiclesByAreaId(_areaId);
                if (vehicles.Count > 0)
                    return Json(vehicles);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetRouteVehicles(int _routeId)
        {
            try
            {
                var vehicles = RouteDeviceDB.GetInstance().GetRouteVehiclesByRouteId(_routeId);
                if (vehicles.Count > 0)
                    return Json(vehicles);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetDevicesToAreaModal()
        {
            try
            {
                var devices = DeviceDB.GetInstance().GetDevicesToFilter(userObj).Where(x => x.LastDeviceDataId != null).ToList();
                if (devices != null && devices.Count > 0)
                    return Json(devices);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetDevicesToRouteModal()
        {
            try
            {
                var devices = DeviceDB.GetInstance().GetDevicesToFilter(userObj).Where(x => x.LastDeviceDataId != null).ToList();
                if (devices != null && devices.Count > 0)
                    return Json(devices);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult DeleteAreaVehicle(int _id)
        {
            try
            {
                var deleteResult = AreaDeviceDB.GetInstance().DeleteAreaDevice(_id);
                if (deleteResult)
                    return Json(_id);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult DeleteRouteVehicle(int _id)
        {
            try
            {
                var deleteResult = RouteDeviceDB.GetInstance().DeleteRouteDevice(_id);
                if (deleteResult)
                    return Json(_id);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetAreaLatLongs(int _areaId)
        {
            try
            {
                var area = AreaDB.GetInstance().GetAreaById(_areaId);
                var areaLatLongList = area.LatsLongs.Split(",");
                var deviceInfoListForMap = new List<DeviceInfoForMap>();
                for (int i = 0; i < areaLatLongList.Length; i += 2)
                {
                    deviceInfoListForMap.Add(new DeviceInfoForMap()
                    {
                        lat = Convert.ToDecimal(areaLatLongList[i].Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR")),
                        lng = Convert.ToDecimal(areaLatLongList[i + 1].Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR"))
                    });
                }
                if (deviceInfoListForMap != null)
                    return Json(deviceInfoListForMap);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetRouteLatLongs(int _routeId)
        {
            try
            {
                var route = RouteDB.GetInstance().GetRouteById(_routeId);
                var routeLatLongList = route.LatsLongs.Split(",");
                var deviceInfoListForMap = new List<DeviceInfoForMap>();
                for (int i = 0; i < routeLatLongList.Length; i += 2)
                {
                    deviceInfoListForMap.Add(new DeviceInfoForMap()
                    {
                        lat = Convert.ToDecimal(routeLatLongList[i].Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR")),
                        lng = Convert.ToDecimal(routeLatLongList[i + 1].Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR"))
                    });
                }
                if (deviceInfoListForMap != null)
                    return Json(deviceInfoListForMap);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetAreasWithVehicles()
        {
            try
            {
                var areaVehicles = AreaDeviceDB.GetInstance().GetAreasWithVehicles(userObj);
                if (areaVehicles != null)
                {
                    areaVehicles = areaVehicles.OrderBy(x => x.AreaName).ToList();
                    return Json(areaVehicles);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void SendReportWithEmailExcel(DataTable _dt, string _sheetName, string _fileName, string _emailToSendReport)
        {
            var rootPath = Path.Combine(_env.WebRootPath, "Content", _fileName);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add(_sheetName);
                sheet.Cells.LoadFromDataTable(_dt, true);
                sheet.Protection.IsProtected = false;
                sheet.Protection.AllowSelectLockedCells = false;
                package.SaveAs(new FileInfo(rootPath));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("veraaractakip@gmail.com", "veravera1234");
            smtp.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.Attachments.Add(new Attachment(rootPath));
            mail.IsBodyHtml = true;

            mail.Subject = _sheetName;
            mail.To.Add(_emailToSendReport);
            mail.From = new MailAddress("veraaractakip@gmail.com");
            smtp.Send(mail);
        }
        public void SendReportWithEmailPDF(Object _reportList, string _mailSubject, string _partialPdfName, string _emailToSendReport)
        {
            var html = RenderViewAsString(_partialPdfName, _reportList);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("veraaractakip@gmail.com");
            mail.To.Add(_emailToSendReport);
            mail.Subject = _mailSubject;
            mail.IsBodyHtml = true;
            var completeHtml = html.Split(new string[] { "<!--replacepoint1-->" }, StringSplitOptions.None)[0];
            completeHtml += html.Split(new string[] { "<!--replacepoint2-->" }, StringSplitOptions.None)[1];
            mail.Body = completeHtml;

            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("veraaractakip@gmail.com", "veravera1234");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
        public int GetTotalKMBetweenTwoDateOfADevice2(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                using (var con = context.Database.GetDbConnection())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        using (var cmd = con.CreateCommand())
                        {
                            //cmd.CommandText = $@"SELECT ISNULL(SUM(DistanceBetweenTwoPackages), 0) / 1000 FROM DeviceData
                            //                     WHERE CreateDate BETWEEN '{_startDate}' AND '{_endDate}'
                            //                     AND DeviceId = {_deviceId}";

                            cmd.CommandText = $@"SELECT dbo.UFN_GetTotalKmDaily({_deviceId})";
                            cmd.CommandTimeout = Int32.MaxValue;
                            context.Database.SetCommandTimeout(Int32.MaxValue);

                            var reader = cmd.ExecuteScalar().ToString();

                            var intResult = Convert.ToInt32(reader);

                            return intResult;
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }
            }
        }
        public JsonResult GetTotalKMBetweenTwoDateOfADevice(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                using (var con = context.Database.GetDbConnection())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        using (var cmd = con.CreateCommand())
                        {
                            //cmd.CommandText = $@"SELECT ISNULL(SUM(DistanceBetweenTwoPackages), 0) / 1000 FROM DeviceData
                            //                     WHERE CreateDate BETWEEN '{_startDate}' AND '{_endDate}'
                            //                     AND DeviceId = {_deviceId}";

                            cmd.CommandText = $@"SELECT dbo.UFN_GetTotalKmDaily({_deviceId})";
                            cmd.CommandTimeout = Int32.MaxValue;
                            context.Database.SetCommandTimeout(Int32.MaxValue);

                            var reader = cmd.ExecuteScalar().ToString();

                            var intResult = Convert.ToInt32(reader);

                            return Json(new
                            {
                                DeviceId = _deviceId,
                                TotalKM = intResult
                            });
                            //return intResult;
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }
            }
        }
        public JsonResult GetLastLocationTimeOfDevice(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                using (var con = context.Database.GetDbConnection())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        using (var cmd = con.CreateCommand())
                        {
                            //cmd.CommandText = $@"SELECT ISNULL(SUM(DistanceBetweenTwoPackages), 0) / 1000 FROM DeviceData
                            //                     WHERE CreateDate BETWEEN '{_startDate}' AND '{_endDate}'
                            //                     AND DeviceId = {_deviceId}";

                            cmd.CommandText = $@"SELECT dbo.UFN_GetLastLocationTimeOfDevice({_deviceId})";
                            cmd.CommandTimeout = Int32.MaxValue;
                            context.Database.SetCommandTimeout(Int32.MaxValue);

                            var reader = cmd.ExecuteScalar().ToString();

                            var Result = reader;

                            return Json(new
                            {
                                DeviceId = _deviceId,
                                LastLocationTime = Result != "" ? Convert.ToDateTime(Result).ToString("yyyy.MM.dd HH:mm:ss") : Result
                            });
                            //return intResult;
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }
            }
        }
        public string GetStringLastLocationTimeOfDevice(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                using (var con = context.Database.GetDbConnection())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        using (var cmd = con.CreateCommand())
                        {
                            //cmd.CommandText = $@"SELECT ISNULL(SUM(DistanceBetweenTwoPackages), 0) / 1000 FROM DeviceData
                            //                     WHERE CreateDate BETWEEN '{_startDate}' AND '{_endDate}'
                            //                     AND DeviceId = {_deviceId}";

                            cmd.CommandText = $@"SELECT dbo.UFN_GetLastLocationTimeOfDevice({_deviceId})";
                            cmd.CommandTimeout = Int32.MaxValue;
                            context.Database.SetCommandTimeout(Int32.MaxValue);

                            var reader = cmd.ExecuteScalar().ToString();

                            var Result = reader;

                            return Result != "" ? Convert.ToDateTime(Result).ToString("yyyy.MM.dd HH:mm:ss") : "";
                            //return intResult;
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }
            }
        }
        public JsonResult SaveCompanyEmployee(int _employeeId, int _empCompanyId, string _empNo, string _empName, string _empSurname, string _empBirthDate, string _empLicenceNo, string _empLicenceClass, string _empLicenceDate, string _empPhoneNumber,
            string _empHomePhoneNumber, string _empAddress, string _empOccupation)
        {
            try
            {
                var empDB = EmployeeDB.GetInstance();
                DateTime? birthDate = null;
                DateTime? licenceDate = null;
                if (_empBirthDate != null && _empBirthDate != "")
                    birthDate = Convert.ToDateTime(_empBirthDate);
                if (_empLicenceDate != null && _empLicenceDate != "")
                    licenceDate = Convert.ToDateTime(_empLicenceDate);
                if (_employeeId == 0)
                {
                    var employee = new Employee()
                    {
                        CompanyId = _empCompanyId,
                        EmployeeNo = _empNo,
                        Name = _empName,
                        Surname = _empSurname,
                        BirthDate = birthDate,
                        DriverLicenceNumber = _empLicenceNo,
                        DriverLicenceDate = licenceDate,
                        PhoneNumber = _empPhoneNumber,
                        HomePhoneNumber = _empHomePhoneNumber,
                        Address = _empAddress,
                        Occupation = _empOccupation == "0" ? "" : _empOccupation,
                        DriverLicenceClass = _empLicenceClass == "0" ? "" : _empLicenceClass,
                    };
                    var result = empDB.AddNewEmployee(employee);
                    if (result != null)
                        return Json(true);
                    return Json(false);
                }
                else
                {
                    var emp = empDB.GetEmployeeById(_employeeId);
                    emp.EmployeeNo = _empNo;
                    emp.Name = _empName;
                    emp.Surname = _empSurname;
                    emp.BirthDate = birthDate;
                    emp.DriverLicenceNumber = _empLicenceNo;
                    emp.DriverLicenceClass = _empLicenceClass == "0" ? "" : _empLicenceClass;
                    emp.DriverLicenceDate = licenceDate;
                    emp.PhoneNumber = _empPhoneNumber;
                    emp.HomePhoneNumber = _empHomePhoneNumber;
                    emp.Address = _empAddress;
                    emp.Occupation = _empOccupation == "0" ? "" : _empOccupation;
                    var result = empDB.UpdateEmployee(emp);
                    if (result != null)
                        return Json(true);
                    return Json(false);
                }
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult DeleteCompanyEmployee(int _employeeId)
        {
            try
            {
                var result = EmployeeDB.GetInstance().DeleteEmployee(_employeeId);
                if (result)
                    return Json(true);
                return Json(false);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult GetCompanyEmployee(int _employeeId)
        {
            try
            {
                var employee = EmployeeDB.GetInstance().GetEmployeeById(_employeeId);
                if (employee != null)
                    return Json(employee);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult GetCompanyEmployeeList(int _companyId)
        {
            try
            {
                var employeeListOfCompany = EmployeeDB.GetInstance().GetCompanysEmployeeList(_companyId);
                if (employeeListOfCompany != null)
                    return PartialView("CompanyEmployeeList", employeeListOfCompany);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public JsonResult BlockEngineOfDevice(int _deviceId, int _blockOrRemoveBlock)
        {
            try
            {
                var deviceDb = DeviceDB.GetInstance();
                var device = deviceDb.GetDeviceById(_deviceId);
                if (device.DeviceGsmNumber == null)
                    return Json(false);
                var result = deviceDb.SetEngineBlockStatus(_deviceId, _blockOrRemoveBlock);
                return result == true ? Json(true) : Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult SaveChannel(int _channelId, int _companyId, string _channelName)
        {
            try
            {
                var channelDB = ChannelDB.GetInstance();
                if (_channelId == 0)
                {
                    Channel channel = new Channel()
                    {
                        CompanyId = _companyId,
                        IsAvailableToRecord = true,
                        Name = _channelName
                    };
                    var result = channelDB.AddNewChannel(channel);
                    if (result != null)
                        return Json(true);
                    return Json(false);
                }
                else
                {
                    var channel = channelDB.GetChannelById(_channelId);
                    channel.Name = _channelName;
                    var result = channelDB.UpdateChannel(channel);
                    if (result != null)
                        return Json(true);
                    return Json(false);
                }
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public IActionResult SaveUsersToChannel(int _channelId, int[] _userId)
        {
            try
            {
                var channel = ChannelDB.GetInstance().GetChannelById(_channelId);
                var uChannelDB = UserChannelDB.GetInstance();
                var channelHasUser = uChannelDB.CheckIfChannelHasUser(_channelId);
                if (channelHasUser)
                    uChannelDB.DeleteAllUsersOfChannel(_channelId);
                if (_userId.Length == 0)
                    return Json(true);
                if (channel != null)
                {
                    foreach (var userId in _userId)
                    {
                        UserChannel uChannel = new UserChannel()
                        {
                            ChannelId = channel.Id,
                            UserId = userId,
                            ChannelPermission = false
                        };
                        uChannelDB.AddNewUserChannel(uChannel);
                    }
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public IActionResult DeleteChannel(int _channelId)
        {
            try
            {
                var channelDB = ChannelDB.GetInstance();
                var result = channelDB.DeleteChannel(_channelId);
                if (result == true)
                {
                    var channelHasUser = UserChannelDB.GetInstance().CheckIfChannelHasUser(_channelId);
                    if (channelHasUser == true)
                    {
                        var result2 = UserChannelDB.GetInstance().DeleteAllUsersOfChannel(_channelId);
                        return result2 == true ? Json(true) : Json(false);
                    }
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }
        }
        public IActionResult GetUserListOfChannel(int _channelId)
        {
            try
            {
                var userList = UserChannelDB.GetInstance().GetAllUsersOfChannel(_channelId);
                return userList != null ? Json(userList) : Json(false);
            }
            catch (Exception exc)
            {
                Json(false);
                throw exc;
            }

        }
        public IActionResult GetAllChannelsOfCompany(int _companyId)
        {
            try
            {
                var channelList = ChannelDB.GetInstance().GetAllChannelsOfCompany(_companyId);
                return channelList != null ? Json(channelList) : Json(false);
            }
            catch (Exception exc)
            {
                Json(false);
                throw exc;
            }
        }
        public IActionResult GetChannelToUpdate(int _channelId)
        {
            try
            {
                var channel = ChannelDB.GetInstance().GetChannelById(_channelId);
                return channel != null ? Json(channel) : Json(false);
            }
            catch (Exception exc)
            {
                Json(false);
                throw exc;
            }
        }
        public IActionResult GetUsersToChannelModal(int _channelId)
        {
            try
            {
                var userList = CompanyUserDB.GetInstance().GetUserListOfCompanyWhichHasSpesificChannel(userObj.Id, _channelId);
                return userList != null ? Json(userList) : Json(false);
            }
            catch (Exception exc)
            {
                Json(false);
                throw exc;
            }
        }
        public JsonResult GetHashedPassword(string _hashedPassword, string _password)
        {
            try
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    bool passwordIsMatched = false;
                    var hashedPassword = VeraHash.MD5Sifrele(_password);
                    if (_hashedPassword.Trim() == hashedPassword.Trim())
                        passwordIsMatched = true;
                    return passwordIsMatched == true ? Json(true) : Json(false);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public IActionResult SaveCompanyEmployeeMission(int _companyEmployeeMissionId, int _missionVehicleId, int _missionEmployeeId, DateTime _missionStartDate, DateTime _missionEndDate, string _missionExplanation)
        {
            try
            {
                var empDb = EmployeeMissionDB.GetInstance();
                if (_companyEmployeeMissionId == 0)
                {
                    var companyEmployeeMission = new EmployeeMission()
                    {
                        DeviceId = _missionVehicleId,
                        EmployeeId = _missionEmployeeId,
                        StartDate = _missionStartDate,
                        EndDate = _missionEndDate,
                        Explanation = _missionExplanation
                    };
                    var result = empDb.AddNewEmployeeMission(companyEmployeeMission);
                    return result != null ? Json(true) : Json(false);
                }
                else
                {
                    var ce = empDb.GetEmployeeMissionById(_companyEmployeeMissionId);
                    ce.DeviceId = _missionVehicleId;
                    ce.EmployeeId = _missionEmployeeId;
                    ce.StartDate = _missionStartDate;
                    ce.EndDate = _missionEndDate;
                    ce.Explanation = _missionExplanation;
                    var result = empDb.UpdateEmployeeMission(ce);
                    return result != null ? Json(true) : Json(false);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult DeleteCompanyEmployeeMission(int _id)
        {
            try
            {
                var result = EmployeeMissionDB.GetInstance().DeleteEmployeeMission(_id);
                return result == true ? Json(true) : Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult GetCompanyEmployeeMissionList()
        {
            try
            {
                var empMissionList = EmployeeMissionDB.GetInstance().GetEmployeeMissionList(userObj);
                if (empMissionList != null && empMissionList.Count > 0)
                    return Json(empMissionList);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult GetCompanyEmployeeMission(int _id)
        {
            try
            {
                var companyEmployeeMission = EmployeeMissionDB.GetInstance().GetEmployeeMissionById(_id);
                return companyEmployeeMission != null ? Json(companyEmployeeMission) : Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult ExportCompanyEmployeeMission()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    var returnList = EmployeeMissionDB.GetInstance().GetEmployeeMissionList(userObj);
                    if (returnList == null || returnList.Count == 0)
                    {
                        returnList = new List<EmployeeMissionRepo>();
                        returnList.Add(new EmployeeMissionRepo());
                    }
                    var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList));

                    dt.Columns["VehicleName"].ColumnName = "Araç Adı";
                    dt.Columns["Name"].ColumnName = "İsim";
                    dt.Columns["Surname"].ColumnName = "Soyisim";
                    dt.Columns.Add("Başlangıç Tarihi", typeof(string));
                    dt.Columns.Add("Bitiş Tarihi", typeof(string));
                    for (int i = 0; i < returnList.Count; i++)
                    {
                        var sDate = dt.Rows[i]["StartDate"].ToString();
                        dt.Rows[i]["Başlangıç Tarihi"] = sDate;
                        var bDate = dt.Rows[i]["EndDate"].ToString();
                        dt.Rows[i]["Bitiş Tarihi"] = bDate;
                    }
                    dt.Columns["Explanation"].ColumnName = "Açıklama";
                    dt.Columns["Occupation"].ColumnName = "Görev";
                    dt.Columns["EmployeeNo"].ColumnName = "Görevli No";
                    dt.Columns.Remove("Id");
                    dt.Columns.Remove("BirthDate");
                    dt.Columns.Remove("DriverLicenceNumber");
                    dt.Columns.Remove("DriverLicenceClass");
                    dt.Columns.Remove("DriverLicenceDate");
                    dt.Columns.Remove("PhoneNumber");
                    dt.Columns.Remove("HomePhoneNumber");
                    dt.Columns.Remove("Address");
                    dt.Columns.Remove("CompanyId");
                    dt.Columns.Remove("StartDate");
                    dt.Columns.Remove("EndDate");


                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("Atanmış Görevli Excel");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        package.Save();
                    }
                    stream.Position = 0;
                    var fileName = $"GorevliBilgileri_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public IActionResult GetCompanyEmployeeListToMissionModal()
        {
            try
            {
                var _companyId = 0;
                using (var context = new VeraEntities())
                {
                    _companyId = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id).CompanyId;
                }
                var employeeListOfCompany = EmployeeDB.GetInstance().GetCompanysEmployeeList(_companyId);
                if (employeeListOfCompany != null && employeeListOfCompany.Count > 0)
                    return Json(employeeListOfCompany);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public IActionResult GetDeviceListToEmployeeMissionModal()
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    //var deviceList = DeviceDB.GetInstance().GetAllDBDevices();
                    //var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    //var deviceListOfCompany = deviceList.Where(x => x.CompanyId == companyUser.CompanyId).ToList();
                    //if (deviceListOfCompany != null && deviceListOfCompany.Count > 0)
                    //    return Json(deviceListOfCompany);
                    //return Json(false);
                    var company = CompanyUserDB.GetInstance().GetCompanyByCompanyUserId(userObj);
                    var subCompaniesAndOwnCompanyCompanyIds = context.Company.Where(x => x.MainCompanyId == company.Id).Select(x => x.Id).ToList();
                    subCompaniesAndOwnCompanyCompanyIds.Add(company.Id);
                    var companyUser = context.CompanyUser.FirstOrDefault(x => x.UserId == userObj.Id);
                    var devices = DeviceDB.GetInstance().GetAllDBDevices().Where(x => x.LastDeviceDataId != null).ToList();
                    var assignedDevicesDeviceIds = context.UserDevice.Where(x => x.UserId == userObj.Id).Select(x => x.DeviceId).ToList();
                    devices = devices.Where(x => assignedDevicesDeviceIds.Contains(x.Id)).ToList();
                    if (devices != null && devices.Count > 0)
                        return Json(devices);
                    return Json(false);
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
    public class DeviceInfoForMap
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public string CarPlateNumber { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public decimal Altitude { get; set; }
        public string IoStatus { get; set; }
        public decimal TotalKmDaily { get; set; }
        public int KmPerHour { get; set; }
        public decimal DirectionDegree { get; set; }
        public int DeviceId { get; set; }
        public string LastLocationTime { get; set; }
        public int LastDeviceId { get; set; }
        public string Konum { get; set; }
        public string ActivityTime { get; set; }
    }
    public class UsersInfo
    {
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public string Geo { get; set; }
        public string Mail { get; set; }
    }
    public class GroupsInfo
    {
        public string Name { get; set; }
    }
    public class AreasInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
    }
    public class RoutesInfo
    {
        public string Name { get; set; }
    }
    public class DeviceListObjectAPExport
    {
        public string CarPlateNumber { get; set; }
        public string PositionTime { get; set; }
        public string PositionDistance { get; set; }
        public string SpeedLimit { get; set; }
        public string WaitingTime { get; set; }
        public string RolantiTime { get; set; }
        public string LocationStatus { get; set; }
        public string SpeedStatus { get; set; }
        public string RolantiStatus { get; set; }
    }
    public class GroupVehiclesInfo
    {
        public string GroupName { get; set; }
        public string CarPlateNumber { get; set; }
    }
}
public class EmployeesInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string BirthDate { get; set; }
    public string DriverLicenceNumber { get; set; }
    public string DriverLicenceClass { get; set; }
    public string DriverLicenceDate { get; set; }
    public string PhoneNumber { get; set; }
    public string HomePhoneNumber { get; set; }
    public string Address { get; set; }
    public string Occupation { get; set; }
    public string EmployeeNo { get; set; }
    public string CompanyId { get; set; }
}
public class LocationAndDevice
{
    public int LocationId { get; set; }
    public int DeviceId { get; set; }
    public LocationAndDevice(int LocationId, int DeviceId)
    {
        this.LocationId = LocationId;
        this.DeviceId = DeviceId;
    }
}