using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using VeraDAL.DB;
using VeraDAL.Entities;

namespace VeraFrontSide.Controllers
{
    public class EmailController:Controller
    {
        public IActionResult Index() {
            return View();
        }
        protected IHostingEnvironment _env; 
        public EmailController(IHostingEnvironment env, ICompositeViewEngine viewEngine) {
            this.viewEngine = viewEngine;
            this._env = env;
        }
        protected ICompositeViewEngine viewEngine;

        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportHizAsimiRaporu(string _startDateHAR, string _endDateHAR, string[] _vehiclesHAR, int _fileTypeHAR, int _downloadOrMail, string _emailToSendReport, int _userId)
        {
            try
            {
                User userObj = GetUser(_userId);
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
        public IActionResult ExportSeferOzetiRaporu(string _startDateSOR, string _endDateSOR, string[] _vehiclesSOR, int _fileTypeSOR, int _downloadOrMail, string _emailToSendReport, int _userId)
        {
            try
            {
                User userObj = GetUser(_userId);

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
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("SeferOzetiRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        package.Save();
                    }
                    var fileName = $"SeferOzetiRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        private static User GetUser(int _userId)
        {
            var userObj = new User();
            var userDb = UserDB.GetInstance();
            userObj = userDb.GetUserById(_userId);
            return userObj;
        }
         
        [RequestFormLimits(ValueCountLimit = 10000)]
        public IActionResult ExportGecmisKonumRaporu(string _startDateGKR, string _endDateGKR, string[] _vehiclesGKR, int _fileTypeGKR, int _downloadOrMail, string _emailToSendReport,int _userId)
        {
            User userObj = GetUser(_userId);
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
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("GecmisKonumRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"GecmisKonumRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                if (_downloadOrMail == 1)
                {
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        public IActionResult ExportSonKonumRaporu(int _groupIdSKR, int _fileTypeSKR, int _downloadOrMail, string _emailToSendReport,int _userId)
        {
            User userObj = GetUser(_userId);
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

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("SonKonumRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"SonKonumRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                if (_downloadOrMail == 1)
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        public IActionResult ExportMesafeOzetRaporu(string _startDateMOR, string _endDateMOR, string[] _vehiclesMOR, int _fileTypeMOR, int _downloadOrMail, string _emailToSendReport,int _userId)
        {
            User userObj = GetUser(_userId);
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
                dt.Columns["Gun"].ColumnName = "Gün";
                dt.Columns["Arac"].ColumnName = "Araç Plakası";
                dt.Columns["MesaiDisiKm"].ColumnName = "Mesai Dışı Km";
                dt.Columns["MesafeKm"].ColumnName = "Mesafe Km";
                dt.Columns["BolgeDisiMesafeKm"].ColumnName = "Bölge Dışı Km";

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("MesafeOzetRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"MesafeOzetRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                if (_downloadOrMail == 1)
                {
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        public IActionResult ExportGiseGecisRaporu(string _startDateGGR, string _endDateGGR, string[] _vehiclesGGR, int _fileTypeGGR, int _downloadOrMail, string _emailToSendReport,int _userId)
        {
            User userObj = GetUser(_userId);
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
                List<GiseGecisRaporuTemp> returnList2 = new List<GiseGecisRaporuTemp>();
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
                if (returnList2.Count == 0)
                {
                    var emptyRepo = new GiseGecisRaporuTemp();
                    returnList2.Add(emptyRepo);
                    dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(returnList2));
                    //dt.Columns.Add("Giriş Zamanı", typeof(string));
                    //dt.Columns.Add("Çıkış Zamanı", typeof(string));
                }

                //dt.Columns["GecisSuresi"].ColumnName = "Geçiş Süresi";
                //dt.Columns["GirisGisesi"].ColumnName = "Giriş Gişesi";
                //dt.Columns["CikisGisesi"].ColumnName = "Çıkış Gişesi";
                //dt.Columns.Remove("GirisDirectionDegree");
                //dt.Columns.Remove("CikisDirectionDegree");
                //dt.Columns.Remove("GirisZamani");
                //dt.Columns.Remove("CikisZamani");





                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var sheet = package.Workbook.Worksheets.Add("AracGiseGecisRaporu");
                    sheet.Cells.LoadFromDataTable(dt, true);
                    package.Save();
                }
                stream.Position = 0;
                var fileName = $"AracGiseGecisRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                if (_downloadOrMail == 1)
                {
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        public IActionResult ExportBolgeVarisAyrilisRaporu(string _startDateBVAR, string _endDateBVAR, int _companyAreaBVAR, string[] _vehiclesBVAR, int _fileTypeBVAR, int _downloadOrMail, string _emailToSendReport, int _userId)
        {
            User userObj = GetUser(_userId);
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
        public IActionResult ExportRotaVarisAyrilisRaporu(string _startDateRVAR, string _endDateRVAR, int _companyRouteRVAR, string[] _vehiclesRVAR, int _fileTypeRVAR, int _downloadOrMail, string _emailToSendReport, int _userId)
        {
            User userObj = GetUser(_userId);
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

                dt.Columns["VarisZamani"].ColumnName = "Varış Zamanı";
                dt.Columns["AyrilisZamani"].ColumnName = "Ayrılış Zamanı";
                dt.Columns["BeklemeDk"].ColumnName = "Bekleme Dk";
                dt.Columns["MesafeKm"].ColumnName = "Mesafe Km";

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
        public IActionResult ExportKontakDurumRaporu(string _startDateKDR, string _endDateKDR, string[] _vehiclesKDR, int _fileTypeKDR, int _downloadOrMail, string _emailToSendReport,int _userId)
        {
            User userObj = GetUser(_userId);
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

                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("KontakDurumRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        package.Save();
                    }
                    var fileName = $"KontakDurumRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        public IActionResult ExportDurakRaporu(string _startDateDR, string _endDateDR, string[] _vehiclesDR, int _fileTypeDR, int _downloadOrMail, string _emailToSendReport, int _userId)
        {
            User userObj = GetUser(_userId);
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
        public IActionResult ExportSeferOlayRaporu(string _startDateSOLR, string _endDateSOLR, string[] _vehiclesSOLR, int _fileTypeSOLR, int _downloadOrMail, string _emailToSendReport,int _userId)
        {
            User userObj = GetUser(_userId);
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

                    dt.Columns["Arac"].ColumnName = "Araç Plakası";
                    dt.Columns["OlayZamani"].ColumnName = "Olay Zamanı";
                    dt.Columns["Aciklama"].ColumnName = "Açıklama";
                    dt.Columns["Surucu"].ColumnName = "Sürücü";
                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var sheet = package.Workbook.Worksheets.Add("SeferOlayıRaporu");
                        sheet.Cells.LoadFromDataTable(dt, true);
                        package.Save();
                    }
                    var fileName = $"SeferOlayıRaporu_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                    stream.Position = 0;
                    if (_downloadOrMail == 1)
                    {
                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
        protected string RenderViewAsString(string viewName, object model)
        {
            viewName = viewName ?? ControllerContext.ActionDescriptor.ActionName;
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                IView view = viewEngine.FindView(ControllerContext, viewName, true).View;
                ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, sw, new HtmlHelperOptions());

                view.RenderAsync(viewContext).Wait();

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
