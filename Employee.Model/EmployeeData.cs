using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Model
{
    public class EmployeeData
    {
        public List<EmployeeModel> Data { get; set; }
        public int totalCount { get; set; }
        public int pageNo { get; set; }
    }
}
