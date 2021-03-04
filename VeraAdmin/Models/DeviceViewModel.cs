using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL.DB;
using VeraDAL.Entities;
using VeraDAL.Models;

namespace VeraAdmin.Models
{
    public class DeviceViewModel
    {
        public List<DeviceRepo> Devices { get; set; }
        public List<TerminalProtocol> TerminalProtocols { get; set; }
        public List<TerminalType> TerminalTypes { get; set; }
        public List<RefTelemetryType> RefTelemetryTypes { get; set; }
        public List<Gateway> Gateways { get; set; }
        public List<UsingStatus> UsingStatus { get; set; }
        public List<ServiceType> ServiceTypes { get; set; }
        public List<WocheckerFlag> WocheckerFlags { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<DeviceMontageType> DeviceMontageTypes { get; set; }
        public List<Company> Companies { get; set; }
        public List<Messaging> Messagings { get; set; }
		public List<CompanyType> CompanyTypes { get; set; } 
	}
}
