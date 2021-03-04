using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeraAdmin.Models;
using VeraDAL.DB;
using VeraDAL.Entities;

namespace VeraAdmin.Controllers
{
	public class DistributorController : BaseController
	{
		public IActionResult Index()
		{



			var model = new DistributorViewModel()
			{
				Distributors = DistributorDB.GetInstance().GetAllDistributors(userObj),
				//Companies = CompanyDB.GetInstance().GetCompanyList(userObj),
				CompanyPartners = CompanyPartnerDB.GetInstance().GetAllCompanyPartners()
			};
			return View(model);
		}
		public JsonResult SaveDistributor(int _id, string _name, int _companyPartnerId, string _entranceDate,
			string _exitDate, int _activity, string _phone1, string _phone2, string _fax, string _city, string _personInCharge,
			string _code, string _address, string _userCode, string _userPassword, string _userName, string _userSurname, string _userEmail, string _userPhone, int _userId)
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
						Telephone = _userPhone,
						Mail = _userEmail,
						Password = _userPassword,
						UserTypeId = 2,
						Status = 1
					};

					UserDB.GetInstance().AddNewUser(user);

					Distributor distributor = new Distributor()
					{
						Name = _name,
						UserId = user.Id,
						CompanyPartnerId = _companyPartnerId,
						EntranceDate = String.IsNullOrEmpty(_entranceDate) ? null : (DateTime?)DateTime.ParseExact(_entranceDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
						ExitDate = String.IsNullOrEmpty(_exitDate) ? null : (DateTime?)DateTime.ParseExact(_entranceDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
						Activity = _activity == 1,
						Phone1 = _phone1,
						Phone2 = _phone2,
						Fax = _fax,
						City = _city,
						PersonInCharge = _personInCharge,
						Code = _code,
						Address = _address,
						CreationDate = DateTime.Now,
						Status = 1
					};

					var result = DistributorDB.GetInstance().AddNewDistributor(distributor);
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

					Distributor distributor = new Distributor()
					{
						Id = _id,
						Name = _name,
						CompanyPartnerId = _companyPartnerId,
						EntranceDate = String.IsNullOrEmpty(_entranceDate) ? null : (DateTime?)DateTime.ParseExact(_entranceDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
						ExitDate = String.IsNullOrEmpty(_exitDate) ? null : (DateTime?)DateTime.ParseExact(_exitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
						Activity = _activity == 1,
						Phone1 = _phone1,
						Phone2 = _phone2,
						Fax = _fax,
						City = _city,
						PersonInCharge = _personInCharge,
						Code = _code,
						Address = _address,
						CreationDate = DateTime.Now,
						Status = 1,
						UserId=user.Id
					};
 					var result = DistributorDB.GetInstance().UpdateDistributor(distributor);
					return Json(result != null);
				}
			}
			catch (Exception)
			{

				throw;
			}

		}

		public IActionResult GetDistributorList(string _bayiAdi)
		{
			try
			{
				_bayiAdi = _bayiAdi == null ? "" : _bayiAdi;
				var distributorList = DistributorDB.GetInstance().GetAllDistributors(userObj, _bayiAdi);
				return PartialView("_DistributorList", distributorList);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public JsonResult GetDistributorByIdForDisplaying(int _id)
		{
			try
			{
				var distributor = DistributorDB.GetInstance().GetDistributorByIdForDisplaying(_id);
				return Json(distributor);
			}
			catch (Exception)
			{

				throw;
			}

		}
		public JsonResult DeleteDistributor(int _id)
		{
			try
			{
				DistributorDB.GetInstance().DeleteDistributor(_id);
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
	}
}