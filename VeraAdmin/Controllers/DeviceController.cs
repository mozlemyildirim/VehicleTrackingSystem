using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL.DB;
using VeraDAL.Entities;

using VeraAdmin.Models;
using System.IO;
using Spire.Xls;
using System.Data;
using Newtonsoft.Json;
using Spire.Xls;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Web;
using OfficeOpenXml;

namespace VeraAdmin.Controllers
{
    public class DeviceController : BaseController
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
                Companies = CompanyDB.GetInstance().GetAllCompaniesForAdminPanel(userObj).OrderBy(x=>x.CompanyDescription).ToList(),
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
                    var device = DeviceDB.GetInstance().GetDeviceById(_id);
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
                        TechnicalNote = _technicalNote,
                        LastDeviceDataId= device.LastDeviceDataId
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
            int _altFirmalıGoster, int _aktiflik, int _firmaTipi, int _terminalTipId, int _yazılımVersionId, int _cihazMontajTuru)
        {
            try
            {
                _cihazId = _cihazId == null ? "" : _cihazId;
                _cihazGsmNo = _cihazGsmNo == null ? "" : _cihazGsmNo;
                _plaka = _plaka == null ? "" : _plaka;
                var deviceList = DeviceDB.GetInstance().GetAllDevices(userObj, _cihazId, _cihazGsmNo, _plaka, _sirket, _kullanimDurumu,
                    _altFirmalıGoster, _aktiflik, _firmaTipi, _terminalTipId, _yazılımVersionId, _cihazMontajTuru);
                if (deviceList == null || deviceList.Count == 0)
                    return Json(false);
                return PartialView("_DeviceList", deviceList);
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
                return Json(device);
            }
            catch (Exception)
            {

                throw;
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
        public DateTime? GetExcelDateTime(string _str)
        {
            try
            {
                if (_str == null || _str == "")
                {
                    return null;
                }

                _str = string.Join(".", _str.Split('.').Select(x => x.Length == 1 ? ("0" + x) : x).ToList());

                var dt = DateTime.ParseExact(_str, "dd.MM.yyyy", null);
                return dt;
            }
            catch (Exception exc)
            {
                return null;
            }
        }
        private IHostingEnvironment _env;
        public DeviceController(IHostingEnvironment env)
        {
            _env = env;
        }
        public JsonResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return Json(false);
            }

            if (file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".xls"))
            {
                var fileName = Path.Combine(_env.WebRootPath, "Content", "TempUploads", Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName));

                try
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Create))
                    {
                        var task = file.CopyToAsync(fileStream);
                        task.Wait();
                    }

                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(fileName);
                    Worksheet sheet = workbook.Worksheets[0];
                    var dt = sheet.ExportDataTable();

                    foreach (DataRow dr in dt.Rows)
                    {
                        var company = dr["Şirket"].ToString().Trim();
                        var carPlateNumber = dr["Plaka"].ToString().Trim();
                        var customerPlateNumber = dr["Müşteri Plakası"].ToString().Trim();
                        var deviceId = dr["Cihaz ID (Comm_id)"].ToString().Trim();
                        var deviceGsmNumber = dr["Cihaz Gsm No"].ToString().Trim();
                        var imei = dr["IMEI No"].ToString().Trim();
                        var carPhoneNumber = dr["Araç Telefonu"].ToString().Trim();
                        var aveaPlaka = dr["Avea Plaka"].ToString().Trim();
                        var terminalProtocol = dr["Terminal Protokol"].ToString().Trim();
                        var gateway = dr["Gateway"].ToString().Trim();
                        var terminalType = dr["Terminal Tipi"].ToString().Trim();
                        var odometreStatus = dr["Odometre Durumu"].ToString().Trim();
                        var softwareVersion = dr["Yazılım Ver."].ToString().Trim();
                        var programmedLocationSendTime = dr["Programlanan Konum Süresi(dk)"].ToString().Trim();
                        var programmedLocationDistance = dr["Programlanan Konum Mesafe(mt)"].ToString().Trim();
                        var programmedSpeedLimit = dr["Programlanan Hız Limiti(km)"].ToString().Trim();
                        var ProgrammedWaitingTime = dr["Programlanan Bekleme(dk)"].ToString().Trim();
                        var ProgrammedHeartBeat = dr["Programlanan HeartBeat(sn)"].ToString().Trim();
                        var activityTime = dr["Aktiflik Süresi(dk)"].ToString().Trim();
                        var journey = dr["Sefer"].ToString().Trim();
                        var messaging = dr["Mesajlaşma"].ToString().Trim();
                        var telemetry = dr["Telemetri"].ToString().Trim();
                        var heatSensor = dr["Isı Sensörü"].ToString().Trim();
                        var doorSensor = dr["Kapı Sensörü"].ToString().Trim();
                        var constantMobile = dr["Sabit Mobil"].ToString().Trim();
                        var lastActivityTime = dr["Son Aktiflik Zamanı"].ToString().Trim();
                        var constantLatitude = dr["Sabit Lat. (DD.DDDDDD)"].ToString().Trim();
                        var wocheckerFlag = dr["WocheckerFlag"].ToString().Trim();
                        var mobileNote = dr["Mobil Not"].ToString().Trim();
                        var techinalNote = dr["Teknik Not"].ToString().Trim();
                        var montageDate = dr["Montaj Tarihi"].ToString().Trim();
                        var montagePerson = dr["Montajı Yapan"].ToString().Trim();
                        var deviceMontageType = dr["Cihaz Montaj Türü"].ToString().Trim();
                        var engineBlock = dr["Motor Blokajı"].ToString().Trim();
                        var engineBlockStatus = dr["Motor Blokaj Durumu"].ToString().Trim();
                        var vehicleBlock = dr["Araç Blokajı"].ToString().Trim();
                        var refTelemetryType = dr["Ref. Telemetri Reçetesi"].ToString().Trim();
                        var gSensor = dr["G Sensör"].ToString().Trim();
                        var driverScoring = dr["Sürücü Skorlama"].ToString().Trim();
                        var rfidEngineBlock = dr["RFID Motor Blokaj"].ToString().Trim();
                        var rfidEngineBlockStatus = dr["RFID Motor Blokaj Durumu"].ToString().Trim();

                        var device = new Device()
                        {
                            CompanyId = CompanyDB.GetInstance().GetCompanyByName(company).Id,
                            CarPlateNumber = carPlateNumber,
                            CustomerPlateNumber = customerPlateNumber,
                            DeviceId = deviceId,
                            DeviceGsmNumber = deviceGsmNumber,
                            IMEINumber = imei,
                            CarPhoneNumber = carPhoneNumber,
                            AveaPlaka = aveaPlaka,
                            TerminalProtocolId = TerminalProtocolDB.GetInstance().GetTerminalProtocolByNameOrInsert(terminalProtocol).Id,
                            GatewayId = GatewayDB.GetInstance().GetGatewayByNameOrInsert(gateway).Id,
                            TerminalTypeId = TerminalTypeDB.GetInstance().GetTerminalTypeByNameOrInsert(terminalType).Id,
                            SoftwareVersionId = SoftwareVersionDB.GetInstance().GetSoftwareVersionByNameOrInsert(softwareVersion, TerminalTypeDB.GetInstance().GetTerminalTypeByNameOrInsert(terminalType).Id).Id,
                            OdometreStatusId = OdometreStatusDB.GetInstance().GetOdometreStatusByNameOrInsert(odometreStatus).Id,
                            ProgrammedLocationSendTime = Convert.ToInt32(programmedLocationSendTime),
                            ProgrammedLocationDistance = Convert.ToInt32(programmedLocationDistance),
                            ProgrammedSpeedLimit = Convert.ToInt32(programmedSpeedLimit),
                            ProgrammedWaitingTime = Convert.ToInt32(ProgrammedWaitingTime),
                            ProgrammedHeartBeat = Convert.ToInt32(ProgrammedHeartBeat),
                            ActivityTime = Convert.ToInt32(activityTime),
                            Journey = journey == "Var" ? true : false,
                            MessagingId = MessagingDB.GetInstance().GetMessagingByNameOrInsert(messaging).Id,
                            Telemetry = telemetry == "Var" ? true : false,
                            HeatSensor = heatSensor == "Var" ? true : false,
                            DoorSensor = doorSensor == "Var" ? true : false,
                            ConstantMobile = constantMobile == "Aktif" ? true : false,
                            LastActivityTime = GetExcelDateTime(lastActivityTime),
                            ConstantLatitude = constantLatitude,
                            WocheckerFlagId = WocheckerFlagDB.GetInstance().GetWocheckerFlagByNameOrInsert(wocheckerFlag).Id,
                            MobileNote = mobileNote,
                            TechnicalNote = techinalNote,
                            MontageDate = GetExcelDateTime(montageDate),
                            MontagePerson = montagePerson,
                            DeviceMontageTypeId = DeviceMontageTypeDB.GetInstance().GetDeviceMontageTypeByNameOrInsert(deviceMontageType).Id,
                            EngineBlock = engineBlock == "Var" ? true : false,
                            EngineBlockStatus = engineBlockStatus == "Aktif" ? true : false,
                            VehicleBlock = vehicleBlock == "Var" ? true : false,
                            RefTelemetryRecipeId = RefTelemetryTypeDB.GetInstance().GetRefTelemetryTypeByNameOrInsert(refTelemetryType).Id,
                            GSensor = gSensor == "Var" ? true : false,
                            DriverScoring = driverScoring == "Var" ? true : false,
                            RFIDEngineBlock = rfidEngineBlock == "Var" ? true : false,
                            RFIDEngineBlockStatus = rfidEngineBlockStatus == "Var" ? true : false,
                            CreationDate = DateTime.Now,
                            Status = 1,

                        };

                        var result = DeviceDB.GetInstance().AddNewDevice(device);
                    }

                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }

                    return Json(true);
                }
                catch (Exception exc)
                {
                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }

                    return Json(exc.Message);
                }
            }
            else
            {
                return Json(false);
            }

        } 
        public JsonResult ImportFile(string _excelData)
        {

            _excelData = HttpUtility.HtmlDecode(_excelData);
            var list2 = JsonConvert.DeserializeObject<List<DeviceExcelObject>>(_excelData);
            try
            {
                foreach (var item in list2)
                {
                    Device device = new Device()
                    {

                        CompanyId = CompanyDB.GetInstance().GetCompanyByName(item.Sirket).Id,
                        CarPlateNumber = item.Plaka,
                        CustomerPlateNumber = item.MusteriPlakasi,
                        DeviceId = item.CihazId,
                        DeviceGsmNumber = item.CihazGsmNo,
                        IMEINumber = item.IMEINo,
                        VehiclePhone = item.AracTelefonu,
                        AveaPlaka = item.AveaPlaka,
                        TerminalProtocolId = TerminalProtocolDB.GetInstance().GetTerminalProtocolByNameOrInsert(item.TerminalProtokol).Id,
                        GatewayId = GatewayDB.GetInstance().GetGatewayByNameOrInsert(item.Gateway).Id,
                        TerminalTypeId = TerminalTypeDB.GetInstance().GetTerminalTypeByNameOrInsert(item.TerminalTipi).Id,
                        SoftwareVersionId = SoftwareVersionDB.GetInstance().GetSoftwareVersionByNameOrInsert(item.YazilimVers, TerminalTypeDB.GetInstance().GetTerminalTypeByNameOrInsert(item.TerminalTipi).Id).Id,
                        OdometreStatusId = OdometreStatusDB.GetInstance().GetOdometreStatusByNameOrInsert(item.OdometreDurumu).Id,
                        ProgrammedLocationSendTime = Convert.ToInt32(item.ProgramlananKonumSuresi.Trim()),
                        ProgrammedLocationDistance = Convert.ToInt32(item.ProgramlananKonumMesafe.Trim()),
                        ProgrammedSpeedLimit = Convert.ToInt32(item.ProgramlananHizLimiti.Trim()),
                        ProgrammedHeartBeat = Convert.ToInt32(item.ProgramlananHeartBeat.Trim()),
                        ProgrammedWaitingTime = Convert.ToInt32(item.ProgramlananBekleme.Trim()),
                        ActivityTime = Convert.ToInt32(item.AktiflikSuresi.Trim()),
                        Journey = item.Sefer.Trim() == "Var" ? true : false,
                        MessagingId = MessagingDB.GetInstance().GetMessagingByNameOrInsert(item.Mesajlasma).Id,
                        Telemetry = item.Telemetri.Trim() == "Var" ? true : false,
                        HeatSensor = item.IsiSensoru.Trim() == "Var" ? true : false,
                        DoorSensor = item.KapiSensoru.Trim() == "Var" ? true : false,
                        ConstantMobile = item.SabitMobil.Trim() == "Aktif" ? true : false,
                        ConstantLatitude = item.SabitLatitude,
                        //ConstantLongitude= ?????????????????????????????????????????????
                        WocheckerFlagId = WocheckerFlagDB.GetInstance().GetWocheckerFlagByNameOrInsert(item.WocheckerFlag).Id,
                        MobileNote = item.MobilNot,
                        TechnicalNote = item.TeknikNot, 
                        MontageDate=GetExcelDateTime(item.MontajTarihi),
                        MontagePerson = item.MontajiYapan,
                        DeviceMontageTypeId = DeviceMontageTypeDB.GetInstance().GetDeviceMontageTypeByNameOrInsert(item.CihazMontajTuru).Id,
                        EngineBlock = item.MotorBlokaji.Trim() == "Var" ? true : false,
                        EngineBlockStatus = item.MotorBlokajDurumu.Trim() == "Aktif" ? true : false,
                        GSensor = item.GSensor.Trim() == "Var" ? true : false,
                        DriverScoring = item.SurucuSkorlama.Trim() == "Var" ? true : false,
                        RFIDEngineBlock = item.RFIDMotorBlokaj.Trim() == "Var" ? true : false,
                        RFIDEngineBlockStatus = item.RFIDMotorBlokajDurumu.Trim() == "Aktif" ? true : false,
                        CreationDate=DateTime.Now,
                        Status=1
                        
                    };
                    if (device != null)
                    {
                        DeviceDB.GetInstance().AddNewDevice(device); 
                    }
                }
                return Json(true);

            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        public IActionResult ExportFile()
        {
            var companyList = DeviceDB.GetInstance().GetAllDBDevicesForExcel();
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Cihazlar");
                sheet.Cells.LoadFromCollection(companyList, true);
                package.Save();
            }
            stream.Position = 0;
            var fileName = $"Cihazlar_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        } 
    } 
}
public class DeviceExcelObject
{
    public int MobileID { get; set; }
    public string Sirket { get; set; }
    public string Plaka { get; set; }
    public string CihazId { get; set; }
    public string CihazGsmNo { get; set; }
    public string IMEINo { get; set; }
    public string TerminalProtokol { get; set; }
    public string Gateway { get; set; }
    public string TerminalTipi { get; set; }
    public string YazilimVers { get; set; }
    public string OdometreDurumu { get; set; }
    public string ProgramlananKonumSuresi { get; set; }
    public string ProgramlananKonumMesafe { get; set; }
    public string ProgramlananHizLimiti { get; set; }
    public string ProgramlananBekleme { get; set; }
    public string ProgramlananHeartBeat { get; set; }
    public string AktiflikSuresi { get; set; }
    public string Sefer { get; set; }
    public string Mesajlasma { get; set; }
    public string Telemetri { get; set; }
    public string IsiSensoru { get; set; }
    public string KapiSensoru { get; set; }
    public string SabitMobil { get; set; }
    public string SonAktiflikZamani { get; set; }
    public string WocheckerFlag { get; set; }
    public string TeknikNot { get; set; }
    public string MontajTarihi { get; set; }
    public string CihazMontajTuru { get; set; }
    public string MotorBlokaji { get; set; }
    public string MotorBlokajDurumu { get; set; }
    public string AracBlokaji { get; set; }
    public string GSensor { get; set; }
    public string SurucuSkorlama { get; set; }
    public string RFIDMotorBlokaj { get; set; }
    public string RFIDMotorBlokajDurumu { get; set; }
    public string AveaPlaka { get; set; }
    public string SabitLatitude { get; set; }
    public string MontajiYapan { get; set; }
    public string MusteriPlakasi { get; set; }
    public string MobilNot { get; set; }
    public string AracTelefonu { get; set; }
}