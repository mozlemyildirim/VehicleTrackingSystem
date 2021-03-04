using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL.Entities;
using VeraDAL.Models;

namespace VeraAdmin.Models
{
	public class DistributorViewModel
	{
		public List<DistributorRepo> Distributors { get; set; }
		public List<CompanyRepo> Companies { get; set; }
		public List<CompanyPartner> CompanyPartners { get; set; }
	}
}
