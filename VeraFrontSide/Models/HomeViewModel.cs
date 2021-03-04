using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL.DB;
using VeraDAL.Entities;
using VeraDAL.Models;

namespace VeraFrontSide.Models
{
    public class HomeViewModel
    {
        public List<DeviceObjectForFrontSide> Devices { get; set; }
        public List<Group> Groups { get; set; }
        public List<Device> GroupDevicesToLeft { get; set; }
        public Company Company { get; set; }
        public List<Company> Companies { get; set; }
        public List<UserType> UserTypes { get; set; }
        public List<GeographicalAuthority> GeographicalAuthorities { get; set; }
        public UserRepo User { get; set; }
        public List<UserRepo> UserList { get; set; }
        public List<Device> DevicesToFilter { get; set; }
        public List<UsingStatus> UsingStatuses { get; set; }
        public List<Question> Question { get; set; }
        public List<UserDevice> UserDevices { get; set; }
        public List<UserDevice> UserDevicesToRight { get; set; }
        public List<Device> DeviceList { get; set; }
        public List<Company> CompanyOfUser { get; set; }
        public List<Area> Areas { get; set; }
        public List<Vehicle> Vehicle { get; set; }
        public List<UsingStatus> UsingStatus { get; set; }
        public Settings Settings { get; set; }
        public List<Route> Route { get; set; }

    }
}
