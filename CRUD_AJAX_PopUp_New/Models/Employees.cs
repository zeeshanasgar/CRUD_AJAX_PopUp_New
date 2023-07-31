using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_AJAX_PopUp_New.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string City { get; set; }
        public int? Salary { get; set; }
    }
}