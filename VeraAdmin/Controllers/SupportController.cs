using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VeraAdmin.Models;
using VeraDAL.DB;
using VeraDAL.Entities;

namespace VeraAdmin.Controllers
{
    public class SupportController : BaseController
    {
        public IActionResult Index()
        {
            var model = new DeviceViewModel()
            {
                //Devices = DeviceDB.GetInstance().GetAllDevices(userObj),
                TerminalProtocols = TerminalProtocolDB.GetInstance().GetAllTerminalProtocols(),
                Gateways = GatewayDB.GetInstance().GetAllGateways(),
                DeviceMontageTypes = DeviceMontageTypeDB.GetInstance().GetAllDeviceMontageTypes(),
                RefTelemetryTypes = RefTelemetryTypeDB.GetInstance().GetAllRefTelemetryTypes(),
                ServiceTypes = ServiceTypeDB.GetInstance().GetAllServiceTypes(),
                TerminalTypes = TerminalTypeDB.GetInstance().GetAllTerminalTypes(),
                UsingStatus = UsingStatusDB.GetInstance().GetAllUsingStatuses(),
                Vehicles = VehicleDB.GetInstance().GetAllVehicles(),
                WocheckerFlags = WocheckerFlagDB.GetInstance().GetAllWocheckerFlags(),
                Companies = CompanyDB.GetInstance().GetAllCompaniesForAdminPanel(userObj),
                Messagings = MessagingDB.GetInstance().GetAllMessagings(),
                CompanyTypes = CompanyTypeDB.GetInstance().GetAllCompanyTypes()
            };

            return View(model);
        }
        public JsonResult SaveDevice(int _id, int _companyId, int _deviceMontageTypeId, int _softwareVersionId, int _usingStatusId,
            int _messagingId, int _refTelemetryRecipeId, int _serviceTypeId, int _wocheckerFlagId, int _vehicleTypeId, string _deviceId, int _terminalProtocolId, int _terminalTypeId, int _gatewayId, string _carPlateNumber,
            string _deviceGsmNumber, bool _rfidEngineBlock, bool _rfidEngineBlockStatus, int _programmedLocationSendTime, int _programmedLocationDistance, int _programmedSpeedLimit, int _programmedWaitingTime, int _programmedHeartBeat, int _activityTime,
             string _cameraURL, bool _isSummerTime, int _timeDifference, string _IMEINumber, bool _journey, bool _telemetry, bool _doorSensor, bool _heatSensor, bool _engineBlock, bool _trafficData, bool _constantMobile, string _constantLatitude, string _constantLongitude, bool _GSensor, bool _driverScoring,
             string _accountingCode, string _montageDate, string _montagePerson, string _technicalNote)
        {
            try
            {
                if (_id == 0)
                {
                    Device deviceToAdd = new Device()
                    {
                        CompanyId = _companyId,
                        DeviceMontageTypeId = _deviceMontageTypeId,
                        SoftwareVersionId = _softwareVersionId,
                        UsingStatusId = _usingStatusId,
                        MessagingId = _messagingId,
                        RefTelemetryRecipeId = _refTelemetryRecipeId,
                        ServiceTypeId = _serviceTypeId,
                        WocheckerFlagId = _wocheckerFlagId,
                        VehicleTypeId = _vehicleTypeId,
                        DeviceId = _deviceId,
                        TerminalProtocolId = _terminalProtocolId,
                        TerminalTypeId = _terminalTypeId,
                        GatewayId = _gatewayId,
                        CarPlateNumber = _carPlateNumber,
                        DeviceGsmNumber = _deviceGsmNumber,
                        RFIDEngineBlock = _rfidEngineBlock,
                        RFIDEngineBlockStatus = _rfidEngineBlockStatus,
                        ProgrammedLocationSendTime = _programmedLocationSendTime,
                        ProgrammedLocationDistance = _programmedLocationDistance,
                        ProgrammedSpeedLimit = _programmedSpeedLimit,
                        ProgrammedWaitingTime = _programmedWaitingTime,
                        ProgrammedHeartBeat = _programmedHeartBeat,
                        ActivityTime = _activityTime,
                        CameraURL = _cameraURL,
                        IsSummerTime = _isSummerTime,
                        TimeDifference = _timeDifference,
                        IMEINumber = _IMEINumber,
                        Journey = _journey,
                        Telemetry = _telemetry,
                        DoorSensor = _doorSensor,
                        HeatSensor = _heatSensor,
                        EngineBlock = _engineBlock,
                        TrafficData = _trafficData,
                        ConstantMobile = _constantMobile,
                        ConstantLatitude = _constantLatitude,
                        ConstantLongitude = _constantLongitude,
                        GSensor = _GSensor,
                        DriverScoring = _driverScoring,
                        Status = 1,
                        CreationDate = DateTime.Now,
                        AccountingCode = _accountingCode
                    };
                    var result = DeviceDB.GetInstance().AddNewDevice(deviceToAdd);
                    return Json(result != null);
                }
                else
                {
                    Device deviceToUpdate = new Device()
                    {
                        Id = _id,
                        CompanyId = _companyId,
                        DeviceMontageTypeId = _deviceMontageTypeId,
                        SoftwareVersionId = _softwareVersionId,
                        UsingStatusId = _usingStatusId,
                        MessagingId = _messagingId,
                        RefTelemetryRecipeId = _refTelemetryRecipeId,
                        ServiceTypeId = _serviceTypeId,
                        WocheckerFlagId = _wocheckerFlagId,
                        VehicleTypeId = _vehicleTypeId,
                        DeviceId = _deviceId,
                        TerminalProtocolId = _terminalProtocolId,
                        TerminalTypeId = _terminalTypeId,
                        GatewayId = _gatewayId,
                        CarPlateNumber = _carPlateNumber,
                        DeviceGsmNumber = _deviceGsmNumber,
                        RFIDEngineBlock = _rfidEngineBlock,
                        RFIDEngineBlockStatus = _rfidEngineBlockStatus,
                        ProgrammedLocationDistance = _programmedLocationDistance,
                        ProgrammedLocationSendTime = _programmedLocationSendTime,
                        ProgrammedSpeedLimit = _programmedSpeedLimit,
                        ProgrammedWaitingTime = _programmedWaitingTime,
                        ProgrammedHeartBeat = _programmedHeartBeat,
                        ActivityTime = _activityTime,
                        CameraURL = _cameraURL,
                        IsSummerTime = _isSummerTime,
                        TimeDifference = _timeDifference,
                        IMEINumber = _IMEINumber,
                        Journey = _journey,
                        Telemetry = _telemetry,
                        DoorSensor = _doorSensor,
                        HeatSensor = _heatSensor,
                        EngineBlock = _engineBlock,
                        TrafficData = _trafficData,
                        ConstantMobile = _constantMobile,
                        ConstantLatitude = _constantLatitude,
                        ConstantLongitude = _constantLongitude,
                        GSensor = _GSensor,
                        DriverScoring = _driverScoring,
                        Status = 1,
                        CreationDate = DateTime.Now,
                        AccountingCode = _accountingCode,
                        MontageDate = String.IsNullOrEmpty(_montageDate) ? null : (DateTime?)DateTime.ParseExact(_montageDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        MontagePerson = _montagePerson,
                        TechnicalNote = _technicalNote
                    };
                    var result = DeviceDB.GetInstance().UpdateDevice(deviceToUpdate);
                    return Json(result != null);
                }
            }
            catch (Exception exc)
            {

                throw exc;

            }
        }
        public JsonResult GetSoftwareVersionsByTerminalTypeId(int _terminalTypeId)
        {
            try
            {
                var list = DeviceDB.GetInstance().GetSoftwareVersionsByTerminalTypeId(_terminalTypeId);
                return Json(list);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public IActionResult GetDeviceList(string _cihazId, string _cihazGsmNo, string _plaka, int _sirket, int _kullanimDurumu,
            int _altFirmalıGoster, int _aktiflik, int _firmaTipi, int _terminalTypeId, int _softwareVersionId,
              int _cihazMontajTuru)
        {
            try
            {
                _cihazId = _cihazId == null ? "" : _cihazId;
                _cihazGsmNo = _cihazGsmNo == null ? "" : _cihazGsmNo;
                _plaka = _plaka == null ? "" : _plaka;
                var deviceList = DeviceDB.GetInstance().GetAllDevices(userObj, _cihazId, _cihazGsmNo, _plaka, _sirket, _kullanimDurumu,
                    _altFirmalıGoster, _aktiflik, _firmaTipi, _terminalTypeId, _softwareVersionId, _cihazMontajTuru);
                if (deviceList == null || deviceList.Count == 0)
                    return Json(false);
                return PartialView("_SupportList", deviceList);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult GetDeviceById(int _id)
        {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceById(_id);
                return Json(device);
            }
            catch (Exception)
            {

                throw;
            } 
        }
        public JsonResult GetDeviceByIdToModal(int _id) {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceByIdToModal(_id);
                if (device != null)
                    return Json(device);
                return Json(false);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        
        }
        public JsonResult DeleteDevice(int _id)
        {
            try
            {
                DeviceDB.GetInstance().DeleteDevice(_id);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
            return Json(false);
        }
        public JsonResult SaveInfo(int _id, string _montageDate, string _montagePerson, string _technicalNote)
        {
            try
            {
                var deviceToUpdate = DeviceDB.GetInstance().GetDeviceById(_id);

                deviceToUpdate.MontagePerson = _montagePerson;
                deviceToUpdate.MontageDate = String.IsNullOrEmpty(_montageDate) ? null : (DateTime?)DateTime.ParseExact(_montageDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                deviceToUpdate.TechnicalNote = _technicalNote;
                DeviceDB.GetInstance().UpdateDevice(deviceToUpdate);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
            return Json(false);

        }
        public IActionResult SetKmOfDevice(int _deviceId, string _kmToSet)
        {
            try
            {
                var device = DeviceDB.GetInstance().GetDeviceById(_deviceId);
                if (device.DeviceGsmNumber == null || device.DeviceGsmNumber == "")
                    return Json(false);

                //set km of device 
                return Json(true);


            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public IActionResult SendMessageToDevice(string _messageToSendToDevice, string _deviceGsmNumber)
        {

            if (_messageToSendToDevice == null || _messageToSendToDevice == "")
                return Json(false);
            if (_deviceGsmNumber == null || _deviceGsmNumber == "")
                return Json(false);
            List<string> listGsms = new List<string>();
            var serviceUrl = "";
            HttpWebResponse response = null;
            try
            {
                string[] _selectedPhoneNumbers = { _deviceGsmNumber };
                string errorGSM = "";
                foreach (string s in _selectedPhoneNumbers)
                { /* Aynı zamanda geçerli bir gsm mi kontrol et */
                    string cGSM = controlgsmno(s, false);
                    if (!listGsms.Contains(cGSM) && cGSM != "")
                        listGsms.Add(cGSM);
                    else if (cGSM == "")
                        errorGSM += s + "\n\r";
                }
                if (errorGSM != "")
                {
                    return Json(false);
                }

                if (listGsms.Count > 1)
                {
                    for (int i = 0; i < listGsms.Count; i++)
                    {
                        if (i + 1 != listGsms.Count)
                            listGsms[i] += "|";
                    }
                }
                serviceUrl = $@"https://service.jetsms.com.tr/
                                SMS-Web/HttpSmsSend?
                                Password=testpasswd&Username=testuser 
                                &Msisdns={listGsms.ToArray()}
                                &Messages=deneme+mesajidir&TransmissionID=test&SendDate=2005-10-21+14%3A05%3A08";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (response.StatusCode.Equals("0"))
                    return Json(true);
                return Json(false);
            }
            catch (WebException ex)
            {
                return Json(false);
                throw ex;
            }
            finally
            {
            }
        }
        public JsonResult GetPastDeviceData(int _deviceId , string _startDateGD ,string _endDateGD,int _kayitSayisi)
        {
            try
            {
                var deviceDataList = DeviceDataDB.GetInstance().GetDeviceDataByDeviceId(_deviceId,_startDateGD,_endDateGD, _kayitSayisi);
                if (deviceDataList != null && deviceDataList.Count > 0)
                {
                    foreach (var item in deviceDataList)
                    {
                        item.CreateDate = Convert.ToDateTime(item.CreateDate.ToString().Replace("T", " "));
                    }
                    return Json(deviceDataList);
                }

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
    }
}