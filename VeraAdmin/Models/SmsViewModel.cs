using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeraDAL.Entities;

namespace VeraAdmin.Models
{
    public class SmsViewModel
    {
        public List<User> UserList { get; set; }
        public List<Device> DeviceList { get; set; }
    }
}
