using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Employee.Model
{
    public class EmployeeModel
    {
        public string yes { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string city { get; set; }
        public int? salary { get; set; }
        //[Required]
        public string Image { get; set; }
        public HttpPostedFileBase file { get; set; }
        public int? SalaryId { get; set; }

        //[Required]
        public string Email { get; set; }
        public string no { get; set; }
            public int totalCount { get; set; }
            public int pageNo { get; set; }
        public virtual SalaryModel SalaryTable { get; set; }
    }
}
