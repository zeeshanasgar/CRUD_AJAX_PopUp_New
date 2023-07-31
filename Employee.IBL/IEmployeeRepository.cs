using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.IBL
{
    public interface IEmployeeRepository
    {
        //List<EmployeeModel> GetEmployeeRecord(DataTableParamModelModel param);
        List<SalaryModel> Index();
        EmployeeData GetEmployeeRecord(DataTableParamModelModel param);
        void SendData(EmployeeModel employeess, string useremail);

        void Delete(int id);
        EmployeeModel Edit(int id);
        void Update(EmployeeModel employee);

        string Export(EmployeeModel employee);
        //void DownloadData(string fileName);

        string ExportCheck(int[] selectedFruits);

        //void Login(MembershipModel model);
        bool Login(MembershipModel model);

        void Signup(UserMasterModel userMasterModel);

        List<EmployeeModel> RunSchedulerMethod();
    }
}
