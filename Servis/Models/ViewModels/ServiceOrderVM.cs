using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servis.Models.ViewModels
{
    public class ServiceOrderVM
    {
        public IEnumerable<AppUser> CustomerList { get; set; }
        public IEnumerable<Status> StatusList { get; set; }
        public ServiceOrder ServiceOrder { get; set; }

        public class Status
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }        
    }
}
