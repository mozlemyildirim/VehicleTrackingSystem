using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL;
using VeraDAL.DB;
using VeraDAL.Entities;
using Microsoft.AspNetCore.Mvc;
using VeraAdmin.Models;
using VeraDAL;
using VeraDAL.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Spire.Xls;
using System.Data;
using System.Web;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace VeraAdmin.Controllers
{
    public class CompanyController : BaseController
    {
        public IActionResult Index()
        {

            //CompanyList = CompanyDB.Instance.GetCompanyList(),
            var returnModel = new CompanyViewModel()
            {
                CompanyTypeList = CompanyTypeDB.GetInstance().GetAllCompanyTypes(),
                CompanyList = CompanyDB.GetInstance().GetAllCompaniesForAdminPanel(userObj),
                Platforms = PlatformDB.GetInstance().GetAllPlatforms(),
                Sectors = SectorDB.GetInstance().GetAllSectors(),
                /*Users = UserDB.GetInstance().GetAllUsers(),*/
                Distributors = DistributorDB.GetInstance().GetAllDistributors(userObj),
                CompanyPartners = CompanyPartnerDB.GetInstance().GetAllCompanyPartners()
            };
            return View(returnModel);
        }
        public JsonResult SaveCompany(int _id, int _mainCompanyId, int _companyTypeId, int _companyPartnerId, int _platformId, int _distributorId, int _sectorId, int _companyReportId, string _companyCode, string _companyDescription, string _address1, string _phone, string _userName, string _userSurname, string _userPhone, string _userEmail, bool _technicalReport, string _technicalEmail1, string _technicalEmail2, string _technicalEmail3, bool _companyStatus, bool _alarmSms, bool _passwordControl, string _userCode, string _userPassword, bool _userStatus, bool _companyVehicleProgramming, string _companyGroupName, int _userId, string _accountingCode)
        {
            try
            {
                if (_id == 0)
                {

                    var user = new User()
                    {
                        Name = _userName,
                        Surname = _userSurname,
                        UserCode = _userCode,
                        Mail = _userEmail,
                        Password = _userPassword,
                        Telephone = _userPhone,
                        Status = 1,
                        UserTypeId = 1,
                        GeographicalAuthorityId = 1,
                        HomepageRefreshTime = 60000
                    };


                    UserDB.GetInstance().AddNewUser(user);
                    Company company = new Company()
                    {

                        MainCompanyId = _mainCompanyId,
                        CompanyTypeId = _companyTypeId,
                        CompanyPartnerId = _companyPartnerId,
                        PlatformId = _platformId,
                        DistributorId = _distributorId,
                        SectorId = _sectorId,
                        CompanyReportId = _companyReportId,
                        CompanyCode = _companyCode,
                        CompanyDescription = _companyDescription,
                        Address1 = _address1,
                        Phone = _phone,
                        TechnicalReport = _technicalReport,
                        TechnicalEmail1 = _technicalEmail1,
                        TechnicalEmail2 = _technicalEmail2,
                        TechnicalEmail3 = _technicalEmail3,
                        CompanyStatus = _companyStatus,
                        AlarmSms = _alarmSms,
                        PasswordControl = _passwordControl,
                        CompanyVehicleProgramming = _companyVehicleProgramming,
                        CompanyGroupName = _companyGroupName,
                        CreationDate = DateTime.Now,
                        Status = 1,
                        UserStatus = _userStatus,
                        AccountingCode = _accountingCode
                    };

                    var result = CompanyDB.GetInstance().AddNewCompany(company);

                    CompanyUser compUser = new CompanyUser()
                    {
                        IsCompanyAdmin = true,
                        UserId = user.Id,
                        Status = 1,
                        CompanyId = company.Id
                    };


                    CompanyUserDB.GetInstance().AddNewCompanyUser(compUser);

                    return Json(result != null);
                }
                else
                {
                    var user = UserDB.GetInstance().GetUserById(_userId);
                    user.Name = _userName;
                    user.Surname = _userSurname;
                    user.UserCode = _userCode;
                    user.Mail = _userEmail;
                    user.Telephone = _userPhone;
                    var isPasswordGoingToBeDifferent = 0;
                    if (user.Password.Trim().ToLower() == _userPassword.Trim().ToLower())
                        isPasswordGoingToBeDifferent = 0;
                    if (user.Password.Trim().ToLower() != _userPassword.Trim().ToLower())
                    {
                        user.Password = _userPassword;
                        isPasswordGoingToBeDifferent = 1;
                    }
                    UserDB.GetInstance().UpdateUser(user, isPasswordGoingToBeDifferent);
                    Company company = new Company()
                    {
                        Id = _id,
                        MainCompanyId = _mainCompanyId,
                        CompanyTypeId = _companyTypeId,
                        CompanyPartnerId = _companyPartnerId,
                        PlatformId = _platformId,
                        DistributorId = _distributorId,
                        SectorId = _sectorId,
                        CompanyReportId = _companyReportId,
                        CompanyCode = _companyCode,
                        CompanyDescription = _companyDescription,
                        Address1 = _address1,
                        Phone = _phone,
                        TechnicalReport = _technicalReport,
                        TechnicalEmail1 = _technicalEmail1,
                        TechnicalEmail2 = _technicalEmail2,
                        TechnicalEmail3 = _technicalEmail3,
                        CompanyStatus = _companyStatus,
                        AlarmSms = _alarmSms,
                        PasswordControl = _passwordControl,
                        CompanyVehicleProgramming = _companyVehicleProgramming,
                        CompanyGroupName = _companyGroupName,
                        CreationDate = DateTime.Now,
                        Status = 1,
                        UserStatus = _userStatus,
                        AccountingCode = _accountingCode
                    };

                    var result = CompanyDB.GetInstance().UpdateCompany(company);
                    return Json(result != null);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public ActionResult GetCompanyList(int _kullanimDurumu, int _firmaTipi, string _firmaKodu)
        {
            try
            {
                _firmaKodu = _firmaKodu == null ? "" : _firmaKodu;
                var companyList = CompanyDB.GetInstance().GetCompanyList(userObj, _kullanimDurumu, _firmaTipi, _firmaKodu);
                return PartialView("_CompanyList", companyList);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult GetCompanyByIdForDisplaying(int _id)
        {
            try
            {
                var company = CompanyDB.GetInstance().GetCompanyByIdForDisplaying(_id);
                return Json(company);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult DeleteCompany(int _id)
        {
            try
            {
                CompanyDB.GetInstance().DeleteCompany(_id);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
            return Json(false);

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
        public DateTime? GetExcelDateTime(string _str)
        {
            try
            {
                if (_str == null || _str == "")
                {
                    return null;
                }

                _str = string.Join(".", _str.Split('.').Select(x => x.Length == 1 ? ("0" + x) : x).ToList());

                var dt = DateTime.ParseExact(_str, "dd.MM.yyyy HH:mm", null);
                return dt;
            }
            catch (Exception exc)
            {
                return null;
            }
        }
        private IHostingEnvironment _env;
        public CompanyController(IHostingEnvironment env)
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
                        var companyCode = dr["Şirket Kodu"].ToString();
                        var companyDescription = dr["Şirket"].ToString().Trim();
                        var mobileCount = dr["Mobil Sayısı"].ToString().Trim();
                        var address1 = dr["Adres 1"].ToString().Trim();
                        var address2 = dr["Adres 2"].ToString().Trim();
                        var phone = dr["Telefon"].ToString().Trim();
                        var fax = dr["Fax"].ToString().Trim();
                        var personInCharge = dr["Yetkili Kişi"].ToString().Trim();
                        var companyPartner = dr["İş Ortağı"].ToString().Trim();
                        var distributorName = dr["Bayi Adı"].ToString().Trim();
                        var platform = dr["Platform"].ToString().Trim();
                        var sector = dr["Sektör"].ToString().Trim();
                        var loginUrl = dr["Login Url"].ToString().Trim();
                        var baseMap = dr["BaseMap"].ToString().Trim();
                        var technicalReport = dr["Teknik Rapor"].ToString().Trim();
                        var companyStatus = dr["Kullanım Durumu"].ToString().Trim();
                        var taxAdministration = dr["Vergi Dairesi"].ToString().Trim();
                        var taxNumber = dr["Vergi No"].ToString().Trim();
                        var entranceDate = dr["Giriş Zamanı"].ToString().Trim();
                        var exitDate = dr["Çıkış Zamanı"].ToString().Trim();
                        var alarmSms = dr["Alarm Sms"].ToString().Trim();
                        var passwordControl = dr["Şifre Kontrol"].ToString().Trim();
                        var infoSense = dr["InfoSense"].ToString().Trim();
                        var accountingCode = dr["Muhasebe Hes Kodu"].ToString().Trim();


                        //USER INFORMATİONSS NEEDED???????????


                        var company = new Company()
                        {
                            CompanyCode = companyCode,
                            CompanyDescription = companyDescription,
                            MobileCount = Convert.ToInt32(mobileCount),
                            Address1 = address1,
                            Address2 = address2,
                            Phone = phone,
                            Fax = fax,
                            //PersonInCharge = personInCharge, //buradaki yetkili kişi userdaki yetkili kişi ??
                            CompanyPartnerId = CompanyPartnerDB.GetInstance().GetCompanyPartnerByNameOrInsert(companyPartner).Id,
                            DistributorId = DistributorDB.GetInstance().GetDistributorIdByName(distributorName),
                            PlatformId = PlatformDB.GetInstance().GetPlatformByNameOrInsert(platform).Id,
                            SectorId = SectorDB.GetInstance().GetSectorByNameOrInsert(sector).Id,
                            LoginUrl = loginUrl,
                            BaseMap = baseMap,
                            TechnicalReport = technicalReport == "Aktif" ? true : false,
                            CompanyStatus = companyStatus == "Aktif" ? true : false,
                            TaxAdministration = taxAdministration,
                            TaxNumber = taxNumber,
                            EntranceDate = GetExcelDateTime(entranceDate),
                            ExitDate = GetExcelDateTime(exitDate),
                            AlarmSms = alarmSms == "Aktif" ? true : false,
                            PasswordControl = passwordControl == "Aktif" ? true : false,
                            InfoSense = infoSense == "Aktif" ? true : false,
                            AccountingCode = accountingCode
                        };

                        var result = CompanyDB.GetInstance().AddNewCompany(company);
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
        public IActionResult GetUserList(int _id)
        {
            try
            {
                var users = CompanyUserDB.GetInstance().GetUsersByCompanyId(_id);
                return PartialView("_UserList", users);
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
                var user = UserDB.GetInstance().GetUserById(_id);
                return Json(user);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public JsonResult SaveUser(int _compUserUserId, string _compUserUserCode, string _compUserName, string _compUserSurname, string _compUserTelephone, string _compUserMail, string _compUserpassword, int _compUserstatus, int _companyIdForSavingUser)
        {
            try
            {
                using (var context = new VeraEntities())
                {
                    if (_compUserUserId == 0)
                    {
                        User user = new User()
                        {
                            Name = _compUserName,
                            Surname = _compUserSurname,
                            Telephone = _compUserTelephone,
                            Mail = _compUserMail,
                            UserCode = _compUserUserCode,
                            UserTypeId = 1,
                            Password = _compUserpassword,
                            Status = 1
                        };
                        var result = UserDB.GetInstance().AddNewUser(user);
                        CompanyUser compUser = new CompanyUser()
                        {
                            CompanyId = _companyIdForSavingUser,
                            UserId = user.Id,
                            IsCompanyAdmin = true,
                            Status = 1
                        };

                        CompanyUserDB.GetInstance().AddNewCompanyUser(compUser);
                        return Json(result != null);
                    }
                    else
                    {
                        var userToUpdate = new User()
                        {
                            Id = _compUserUserId,
                            Name = _compUserName,
                            Surname = _compUserSurname,
                            Telephone = _compUserTelephone,
                            Mail = _compUserMail,
                            UserCode = _compUserUserCode,
                            UserTypeId = 1,
                            Password = _compUserpassword,
                            Status = 1
                        };
                        var userTUpdate = UserDB.GetInstance().GetUserById(_compUserUserId);
                        var isPasswordGoingToBeDifferent = 0;
                        if (userTUpdate.Password.Trim().ToLower() == _compUserpassword.Trim().ToLower())
                            isPasswordGoingToBeDifferent = 0;
                        if (userTUpdate.Password.Trim().ToLower() != _compUserpassword.Trim().ToLower())
                        {
                            userToUpdate.Password = _compUserpassword;
                            isPasswordGoingToBeDifferent = 1;
                        }
                        var result = UserDB.GetInstance().UpdateUser(userToUpdate, isPasswordGoingToBeDifferent);
                        return Json(result != null);
                    }
                }
            }
            catch (Exception exc)
            {
                return Json(false);
                throw exc;
            }

        }
        public JsonResult DeleteUser(int _id)
        {
            try
            {
                var result = UserDB.GetInstance().DeleteUser(_id);
                if (result == true)
                {
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public JsonResult ImportFile(string _excelData)
        {

            _excelData = HttpUtility.HtmlDecode(_excelData);
            var list2 = JsonConvert.DeserializeObject<List<CompanyExcelObject>>(_excelData);
            try
            {
                foreach (var item in list2)
                {
                    Company company = new Company()
                    {
                        CompanyCode = item.SirketKodu,
                        CompanyDescription = item.SirketKodu,
                        MobileCount = item.MobilSayisi,
                        Address1 = item.Adres1,
                        Address2 = item.Adres2,
                        Phone = item.Telefon,
                        Fax = item.Fax,
                        PersonInCharge = item.YetkiliKisi,
                        CompanyPartnerId = !string.IsNullOrEmpty(item.IsOrtagi) ? CompanyPartnerDB.GetInstance().GetCompanyPartnerByNameOrInsert(item.IsOrtagi).Id : 0,
                        DistributorId = DistributorDB.GetInstance().GetDistributorIdByName(item.BayiAdi),
                        PlatformId = !string.IsNullOrEmpty(item.Platform) ? PlatformDB.GetInstance().GetPlatformByNameOrInsert(item.Platform).Id : 0,
                        SectorId = !string.IsNullOrEmpty(item.Sektor) ? SectorDB.GetInstance().GetSectorByNameOrInsert(item.Sektor).Id : 0,
                        LoginUrl = item.LoginUrl,
                        BaseMap = item.BaseMap,
                        TechnicalReport = item.TeknikRapor == "Aktif" ? true : false,
                        TaxAdministration = item.VergiDairesi,
                        TaxNumber = item.VergiNo,
                        EntranceDate = GetExcelDateTime(item.GirisZamani),
                        ExitDate = GetExcelDateTime(item.CikisZamani),
                        AlarmSms = item.AlarmSms == "Aktif" ? true : false,
                        PasswordControl = item.SifreKontrol == "Aktif" ? true : false,
                        InfoSense = item.InfoSense == "Aktif" ? true : false,
                        AccountingCode = item.MuhasebeHesKodu,
                        Status = 1,
                        CompanyStatus = true,
                        CreationDate = DateTime.Now,
                        UserStatus = true
                    };
                    var companyAdmin = ConvertToEngStandarts(company.CompanyCode);
                    bool isUserExist = UserDB.GetInstance().CheckIfUserExist(companyAdmin);
                    if (isUserExist)
                    {
                        continue;
                    }
                    CompanyDB.GetInstance().AddNewCompany(company);
                    if (!isUserExist)
                    {

                        if (companyAdmin != "")
                        {
                            User user = new User()
                            {
                                UserTypeId = 1,
                                UserCode = companyAdmin,
                                Password = "11",
                                Status = 1
                            };
                            UserDB.GetInstance().AddNewUser(user);
                            CompanyUser companyUser = new CompanyUser()
                            {
                                CompanyId = company.Id,
                                IsCompanyAdmin = true,
                                Status = 1,
                                UserId = user.Id
                            };
                            CompanyUserDB.GetInstance().AddNewCompanyUser(companyUser);
                        }
                    }
                }
                return Json(true);

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public string ConvertToEngStandarts(string _word)
        {
            if (!string.IsNullOrEmpty(_word))
            {
                var wordToReturn = _word
                                   .Trim()
                                   .Replace(" ", "")
                                   .Replace("ı", "i")
                                   .Replace("ğ", "g")
                                   .Replace("ü", "u")
                                   .Replace("ş", "s")
                                   .Replace("ç", "c")
                                   .Replace("ö", "o")
                                   .Replace("I", "i")
                                   .Replace("i", "i")
                                   .Replace("Ğ", "g")
                                   .Replace("Ü", "u")
                                   .Replace("Ş", "s")
                                   .Replace("Ç", "c")
                                   .Replace("Ö", "o")
                                   .ToLower();
                return wordToReturn;
            }
            return "";
        }
        public IActionResult ExportFile()
        {
            var companyList = CompanyDB.GetInstance().GetAllDBCompaniesForExcel();
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Şirketler");
                sheet.Cells.LoadFromCollection(companyList, true);
                package.Save();
            }
            stream.Position = 0;
            var fileName = $"Şirketler_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }
    }
    public class CompanyExcelObject
    {
        public string SirketKodu { get; set; }
        public string Sirket { get; set; }
        public int MobilSayisi { get; set; }
        public string IsOrtagi { get; set; }
        public string BayiAdi { get; set; }
        public string BaseMap { get; set; }
        public string TeknikRapor { get; set; }
        public string KullanimDurumu { get; set; }
        public string GirisZamani { get; set; }
        public string AlarmSms { get; set; }
        public string SifreKontrol { get; set; }
        public string InfoSense { get; set; }
        public string MuhasebeHesKodu { get; set; }
        public string Platform { get; set; }
        public string Sektor { get; set; }
        public string LoginUrl { get; set; }
        public string Adres1 { get; set; }
        public string YetkiliKisi { get; set; }
        public string Telefon { get; set; }
        public string Adres2 { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNo { get; set; }
        public string Fax { get; set; }
        public string CikisZamani { get; set; }

    }
}