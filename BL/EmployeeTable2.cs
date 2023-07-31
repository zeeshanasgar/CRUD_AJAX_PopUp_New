using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmployeeTable2 : IEmployee
    {
        public int ss()
        {
            return 10;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string city { get; set; }
        public Nullable<int> salary { get; set; }
        //[Required]
        public string Image { get; set; }
        public Nullable<int> SalaryId { get; set; }

        //[Required]
        //public HttpPostedFileBase file { get; set; }
        public string Email { get; set; }

        //public virtual SalaryTable SalaryTable { get; set; }
    }
}
