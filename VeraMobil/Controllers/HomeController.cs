using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SelectPdf;
using VeraDAL.DB;
using VeraDAL.Entities;
using VeraMobil.Models;
using GeoCoordinatePortable;

namespace VeraMobil.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env) : base(httpContextAccessor, env)
        {
        }

        public IActionResult Index()
        {
            var cookie = GetCookie("VeraMobilLoggedInUserId");
            if (cookie == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public JsonResult GetDevicesToShow()
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            var endDate = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);

            using (var context = new VeraEntities())
            {
                var list = new List<DeviceInfoForMap>();
                var deviceIds = context.DeviceData.Select(x => x.DeviceId).Distinct().ToList();

                foreach (var item in deviceIds)
                {
                    var device = context.Device.FirstOrDefault(x => x.Id == item);
                    var lastDeviceData = context.DeviceData.FirstOrDefault(x => x.Id == device.LastDeviceDataId);

                    list.Add(new DeviceInfoForMap()
                    { 
                        lat = lastDeviceData.Latitude,
                        lng = lastDeviceData.Longtitude,
                        CarPlateNumber = device.CarPlateNumber,
                        Altitude = lastDeviceData.Altitude,
                        Date = Convert.ToString(lastDeviceData.CreateDate),
                        IoStatus = lastDeviceData.IoStatus.EndsWith("0") ? "Kapalı" : "Açık",
                        TotaLkm = GetTotalKMBetweenTwoDateOfADevice(item, startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss")),
                        KmPerHour = lastDeviceData.KmPerHour,
                        Location = DeviceDB.GetInstance().GetLocationFromLatLon2(Convert.ToString(lastDeviceData.Latitude), Convert.ToString(lastDeviceData.Longtitude)),
                        DirectionDegree = Convert.ToInt16(lastDeviceData.DirectionDegree),
                        DeviceId = item
                    });
                    //list.Add(lastDeviceData.Latitude + "--" + lastDeviceData.Longtitude);
                }
                return Json(list);
            }
        }

        public int GetTotalKMBetweenTwoDateOfADevice(int _deviceId, string _startDate, string _endDate)
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

        public JsonResult GetSingleDeviceDetail(int _deviceId)
        {
            using (var context = new VeraEntities())
            {
                var list = new List<DeviceInfoForMap>();
                var deviceIds = new int[] { _deviceId };

                foreach (var item in deviceIds)
                {
                    var device = context.Device.FirstOrDefault(x => x.Id == item);
                    var lastDeviceData = context.DeviceData.FirstOrDefault(x => x.Id == device.LastDeviceDataId);

                    list.Add(new DeviceInfoForMap()
                    {
                        lat = lastDeviceData.Latitude,
                        lng = lastDeviceData.Longtitude,
                        CarPlateNumber = device.CarPlateNumber,
                        Altitude = lastDeviceData.Altitude,
                        Date = Convert.ToString(lastDeviceData.CreateDate),
                        IoStatus = lastDeviceData.IoStatus.EndsWith("0") ? "Kapalı" : "Açık",
                        TotaLkm = lastDeviceData.TotalKm,
                        KmPerHour = lastDeviceData.KmPerHour,
                        Location = DeviceDB.GetInstance().GetLocationFromLatLon2(Convert.ToString(lastDeviceData.Latitude), Convert.ToString(lastDeviceData.Longtitude)),
                        DirectionDegree = Convert.ToInt16(lastDeviceData.DirectionDegree),
                        DeviceId = item
                    });
                    //list.Add(lastDeviceData.Latitude + "--" + lastDeviceData.Longtitude);
                }
                return Json(list);
            }
        }

        public JsonResult GetGecmisKonum(int _deviceId, string _startDate = "", string _endDate = "")
        {
            _startDate += " 00:00:00";
            _endDate += " 23:59:59";

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
                            string query = $@"SELECT *, (SELECT Location FROM LatLonLocation WHERE Latitude = DD.Latitude AND Longtitude = DD.Longtitude) AS LocationString FROM DeviceData AS DD
                                              WHERE CreateDate >= '{_startDate}'
                                              AND CreateDate <= '{_endDate}'
                                              AND DeviceId = {_deviceId}
                                              ORDER BY Id
                                              FOR JSON AUTO";

                            cmd.CommandText = query;

                            var reader = cmd.ExecuteReader();

                            StringBuilder sb = new StringBuilder();

                            while (reader.Read())
                                sb.Append(reader.GetString(0));

                            var json = sb.ToString();
                            var dt = JsonConvert.DeserializeObject<DataTable>(json);
                            return Json(dt);
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

        public IActionResult SeferOzetiRaporu(int _deviceId, string _startDate, string _endDate)
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

                            _startDate += " 00:00:00";
                            _endDate += " 23:59:59";

                            cmd.CommandText = $"EXEC USP_SeferOzetiRaporu '{_startDate}','{_endDate}',{_deviceId}";
                            cmd.CommandTimeout = Int32.MaxValue;
                            context.Database.SetCommandTimeout(Int32.MaxValue);

                            var reader = cmd.ExecuteReader();

                            StringBuilder sb = new StringBuilder();

                            while (reader.Read())
                                sb.Append(reader.GetString(0));

                            var json = sb.ToString();
                            var list1 = JsonConvert.DeserializeObject<List<SeferOzetiRaporuObject>>(json);

                            list1 = list1 == null ? new List<SeferOzetiRaporuObject>() : list1;

                            list1 = list1.Where(x => x.EndKontakAcilmaTarihi != null).ToList();

                            var returnList = new List<SeferOzetiRaporuObjectRepo>();

                            var toplamSaat = 0d;

                            foreach (var item in list1)
                            {
                                var startDate = Convert.ToDateTime(item.StartKontakAcilmaTarihi);
                                var endDate = Convert.ToDateTime(item.EndKontakAcilmaTarihi);

                                toplamSaat += (endDate - startDate).TotalHours;

                                returnList.Add(new SeferOzetiRaporuObjectRepo()
                                {
                                    Sure = ReportDB.GetInstance().TotalHoursToHumanReadableString((endDate - startDate).TotalHours.ToString()),
                                    VarisZamani = item.EndKontakAcilmaTarihi,
                                    KalkisZamani = item.StartKontakAcilmaTarihi,
                                    OrtHiz = item.AverageKm,
                                    MaxHiz = item.MaxSpeed,
                                    Mesafe = (Convert.ToInt32(item.EndTotalKm) - Convert.ToInt32(item.StartTotalKm)).ToString(),
                                    KalkisPozisyon = DeviceDB.GetInstance().GetLocationFromLatLon2(item.StartLat, item.StartLong),
                                    VarisPozisyon = DeviceDB.GetInstance().GetLocationFromLatLon2(item.EndLat, item.EndLong),
                                    ToplamKm = item.EndTotalKm,
                                    StartTotalKm = item.StartTotalKm,
                                    EndTotalKm = item.EndTotalKm
                                });
                            }

                            ViewBag.ToplamGecenSureString = ReportDB.GetInstance().TotalHoursToHumanReadableString(toplamSaat.ToString()); 
                            ViewBag.ReportStartDate = DateTime.ParseExact(_startDate, "yyyy-MM-dd HH:mm:ss", null).ToString("dd.MM.yyyy HH:mm");
                            ViewBag.ReportEndDate = DateTime.ParseExact(_endDate, "yyyy-MM-dd HH:mm:ss", null).ToString("dd.MM.yyyy HH:mm");
                            ViewBag.MinSure = ReportDB.GetInstance().TotalHoursToHumanReadableString(returnList.Select(x => (Convert.ToDateTime(x.VarisZamani) - Convert.ToDateTime(x.KalkisZamani)).TotalHours).ToList().OrderBy(x => x).FirstOrDefault().ToString());
                            ViewBag.MaxSure = ReportDB.GetInstance().TotalHoursToHumanReadableString(returnList.Select(x => (Convert.ToDateTime(x.VarisZamani) - Convert.ToDateTime(x.KalkisZamani)).TotalHours).ToList().OrderByDescending(x => x).FirstOrDefault().ToString());
                            ViewBag.PlateNumber = DeviceDB.GetInstance().GetDeviceById(_deviceId).CarPlateNumber;

                            reader.Close();

                            return PartialView("_SeferOzetiRaporuResult", returnList);
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

        public IActionResult GecmisKonumRaporu(int _deviceId, string _startDate, string _endDate)
        {
            try
            {
                _startDate += " 00:00:00";
                _endDate += " 23:59:59";

                var result = ReportDB.GetInstance().GecmisKonumRaporu(_startDate, _endDate, new string[] { _deviceId.ToString() }).FirstOrDefault().Value;

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(result));
                dt.Columns.Remove("Grup");
                dt.Columns.Remove("Arac");
                dt.Columns.Remove("Enlem");
                dt.Columns.Remove("Boylam");

                foreach (DataRow dr in dt.Rows)
                {
                    dr["Tarih"] = Convert.ToDateTime(dr["Tarih"].ToString()).ToString("dd.MM.yyyy HH:mm");
                }

                dt = dt.DefaultView.ToTable(true);

                result = JsonConvert.DeserializeObject<List<GecmisKonumRaporuObjectRepo>>(JsonConvert.SerializeObject(dt));

                ViewBag.ReportStartDate = DateTime.ParseExact(_startDate, "yyyy-MM-dd HH:mm:ss", null).ToString("dd.MM.yyyy HH:mm");
                ViewBag.ReportEndDate = DateTime.ParseExact(_endDate, "yyyy-MM-dd HH:mm:ss", null).ToString("dd.MM.yyyy HH:mm");
                ViewBag.PlateNumber = DeviceDB.GetInstance().GetDeviceById(_deviceId).CarPlateNumber;

                return PartialView("_GecmisKonumRaporuResult", result);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        //

        //[ValidateInput(false)]
        public JsonResult HtmlToPdf(string _html)
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;

            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";

            PdfDocument doc = converter.ConvertHtmlString(_html, "application/pdf");
            doc.Save(Path.Combine(_env.WebRootPath, "Content", fileName));
            doc.Close();

            return Json(fileName);
        }

        public IActionResult GiseGecisRaporu(int _deviceId, string _startDate, string _endDate)
        {
            try
            {
                var returnList = new List<AracGiseRaporuResultRepo>();
                var tempList = new List<GiseGecisRaporuTemp>();

                _startDate += " 00:00:00";
                _endDate += " 23:59:59";

                var startDate = DateTime.ParseExact(_startDate, "yyyy-MM-dd HH:mm:ss", null);
                var endDate = DateTime.ParseExact(_endDate, "yyyy-MM-dd HH:mm:ss", null);

                var list = new List<TollBooth>();

                //list.Add(new TollBooth()
                //{
                //    Name = "Fatih Sultan Mehmet Köprüsü Gişesi Avrupa",
                //    LatLons = "41.091671, 29.037541,41.091407, 29.043650,41.090284, 29.043510,41.090463, 29.037225"
                //});

                //list.Add(new TollBooth()
                //{
                //    Name = "Fatih Sultan Mehmet Köprüsü Gişesi Anadolu",
                //    LatLons = "41.092351, 29.070094,41.091108, 29.070067,41.091210, 29.074501,41.092534, 29.074379"
                //});

                using (var context = new VeraEntities())
                {
                    list = context.TollBooth.ToList();

                    var deviceDatas = context.DeviceData.Where(x => x.DeviceId == _deviceId &&
                                                                    x.CreateDate >= startDate &&
                                                                    x.CreateDate <= endDate
                                                               ).OrderBy(x => x.CreateDate).ToList();


                    foreach (var deviceData in deviceDatas)
                    {
                        foreach (var item in list)
                        {
                            var latLonList = new List<CustomLatLon>();
                            var points = item.LatLons.Split(',');

                            for (var i = 0; i < points.Length; i += 2)
                            {
                                var lat = Convert.ToDouble(points[i].Trim().Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR"));
                                var lon = Convert.ToDouble(points[i + 1].Trim().Replace(".", ","), CultureInfo.GetCultureInfo("tr-TR"));
                                latLonList.Add(new CustomLatLon(lat, lon));
                            }

                            var result = Contains(new CustomLatLon((double)deviceData.Latitude, (double)deviceData.Longtitude), latLonList);

                            if (result)
                            {
                                tempList.Add(new GiseGecisRaporuTemp()
                                {
                                    DirectionDegree = deviceData.DirectionDegree,
                                    Gise = item.Name,
                                    Zaman = deviceData.CreateDate
                                });
                            }
                        }
                    }
                }

                tempList = tempList.OrderBy(x => x.Zaman).ToList();

                var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(tempList.OrderBy(x => x.Zaman).ToList()));

                dt.Columns.Add("WillBeDeleted");

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if ((i + 1) >= dt.Rows.Count)
                    {
                        break;
                    }
                    var currentGiseAdi = dt.Rows[i]["Gise"].ToString();
                    var nextGiseAdi = dt.Rows[i + 1]["Gise"].ToString();
                    var currentDegree = Convert.ToInt32(dt.Rows[i]["DirectionDegree"].ToString());
                    var nextDegree = Convert.ToInt32(dt.Rows[i + 1]["DirectionDegree"].ToString());

                    if (currentGiseAdi == nextGiseAdi)
                    {
                        if (currentDegree - 75 < nextDegree && currentDegree + 75 > nextDegree)
                        {
                            dt.Rows[i + 1]["WillBeDeleted"] = "true";
                        }
                    }
                }

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["WillBeDeleted"].ToString() == "true")
                    {
                        dt.Rows.RemoveAt(i);
                        i = -1;
                    }
                }

                var ss = JsonConvert.SerializeObject(dt);

                dt.Columns.Remove("WillBeDeleted");

                for (var i = 0; i < dt.Rows.Count; i += 2)
                {
                    returnList.Add(new AracGiseRaporuResultRepo()
                    {
                        GirisDirectionDegree = Convert.ToDecimal(dt.Rows[i]["DirectionDegree"].ToString()),
                        GirisGisesi = dt.Rows[i]["Gise"].ToString(),
                        GirisZamani = Convert.ToDateTime(dt.Rows[i]["Zaman"].ToString()),
                        CikisDirectionDegree = Convert.ToDecimal(dt.Rows[i + 1]["DirectionDegree"].ToString()),
                        CikisGisesi = dt.Rows[i + 1]["Gise"].ToString(),
                        CikisZamani = Convert.ToDateTime(dt.Rows[i + 1]["Zaman"].ToString())
                    });
                }

                foreach (var item in returnList)
                {
                    item.GecisSuresi = ReportDB.GetInstance().TotalHoursToHumanReadableString((item.CikisZamani - item.GirisZamani).TotalHours.ToString());
                }

                ViewBag.ReportStartDate = DateTime.ParseExact(_startDate, "yyyy-MM-dd HH:mm:ss", null).ToString("dd.MM.yyyy HH:mm");
                ViewBag.ReportEndDate = DateTime.ParseExact(_endDate, "yyyy-MM-dd HH:mm:ss", null).ToString("dd.MM.yyyy HH:mm");
                ViewBag.PlateNumber = DeviceDB.GetInstance().GetDeviceById(_deviceId).CarPlateNumber;

                return PartialView("_GiseGecisRaporuResult", returnList);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public bool Contains(CustomLatLon location, List<CustomLatLon> _vertices)
        {
            //if (!Bounds.Contains(location))
            //    return false;

            var lastPoint = _vertices[_vertices.Count - 1];
            var isInside = false;
            var x = location.Longitude;

            foreach (var point in _vertices)
            {
                var x1 = lastPoint.Longitude;
                var x2 = point.Longitude;
                var dx = x2 - x1;

                if (Math.Abs(dx) > 180.0)
                {
                    // we have, most likely, just jumped the dateline (could do further validation to this effect if needed).  normalise the numbers.
                    if (x > 0)
                    {
                        while (x1 < 0)
                            x1 += 360;
                        while (x2 < 0)
                            x2 += 360;
                    }
                    else
                    {
                        while (x1 > 0)
                            x1 -= 360;
                        while (x2 > 0)
                            x2 -= 360;
                    }
                    dx = x2 - x1;
                }

                if ((x1 <= x && x2 > x) || (x1 >= x && x2 < x))
                {
                    var grad = (point.Latitude - lastPoint.Latitude) / dx;
                    var intersectAtLat = lastPoint.Latitude + ((x - x1) * grad);

                    if (intersectAtLat > location.Latitude)
                        isInside = !isInside;
                }
                lastPoint = point;
            }

            return isInside;
        }
    }

    public class CustomLatLon
    {
        public CustomLatLon(double _lat, double _lon)
        {
            this.Latitude = _lat;
            this.Longitude = _lon;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }


    public class GiseGecisRaporuTemp
    {
        public string Gise { get; set; }
        public DateTime Zaman { get; set; }
        public decimal DirectionDegree { get; set; }
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
        public decimal TotaLkm { get; set; }
        public int KmPerHour { get; set; }
        public decimal DirectionDegree { get; set; }
        public int DeviceId { get; set; }
    }
}
