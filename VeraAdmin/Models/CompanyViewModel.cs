using VeraDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL.Models;

namespace VeraAdmin.Models
{
    public class CompanyViewModel
    {
        public List<Company> CompanyList { get; set; }
		public List<Platform> Platforms { get; set; }
		public List<CompanyType> CompanyTypeList { get; set; }
		public List<Sector> Sectors { get; set; }
		public List<User> Users { get; set; }
		public List<DistributorRepo> Distributors { get; set; }
		public List<CompanyPartner> CompanyPartners { get; set; }
		public List<UserRepo> UsersOfCompany { get; set; }


	}
}
