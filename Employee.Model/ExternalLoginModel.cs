using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Model
{
    public class ExternalLoginModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }

    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
}
