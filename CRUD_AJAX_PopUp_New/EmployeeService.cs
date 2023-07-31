//using CRUD_AJAX_PopUp_New.Controllers;
//using CRUD_AJAX_PopUp_New.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using System.IO;
//using System.Data.Entity.Migrations;
//using System.Linq.Dynamic;

//namespace CRUD_AJAX_PopUp_New
//{
//    public class EmployeeService : IEmployee
//    {
//        private JqueryDataTableDBEntities db;

//        //private List<EmployeeTable> users = new List<EmployeeTable>();
//        private IEnumerable<EmployeeTable> users = new List<EmployeeTable>();
        
//        private EmployeeTable userss = new EmployeeTable();
//        //private List<EmployeeTable> ss;

//        public EmployeeService()
//        {
//            db = new JqueryDataTableDBEntities();
//        }

//        //public IEnumerable<EmployeeTable> GetAll()
//        //{
//        //    var list = db.EmployeeTable.ToList();
//        //    return list;
//        //}

//        //impplement for datatable
//        //public void GetEmployeeRecord(DataTableParamModel param)
//        //{
//        //    //List<EmployeeTable> list = new List<EmployeeTable>();

//        //}


//        public void SendData(EmployeeTable employeess, string useremail)
//        {
//            log4net.Config.BasicConfigurator.Configure();         // by me
//            log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

//            try
//            {
//                string filename = Path.GetFileName(employeess.file.FileName);
//                string _filename = DateTime.Now.ToString("yymmssfff") + filename;
//                //string extension = Path.GetExtension(employeess.file.FileName);
//                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/"), _filename);
//                employeess.Image = "image/" + _filename;
//                employeess.file.SaveAs(path);
//            }
//            catch (Exception)
//            {
//                log.Error("Adding Image Exception");
//            }

//            try
//            {
//                // for email data working
//                var emp = new EmployeeTable()
//                {
//                    Name = employeess.Name,
//                    Gender = employeess.Gender,
//                    Designation = employeess.Designation,
//                    city = employeess.city,
//                    salary = employeess.salary,
//                    Image = employeess.Image,
//                    SalaryId = employeess.SalaryId,    // new table
//                    Email = employeess.Email,    //new table
//                                                 //file = employeess.file
//                };
//                MailMessage mm = new MailMessage("zeeshanasgar07@gmail.com", employeess.Email);
//                mm.Subject = "This is your Data";
//                // Construct the email body with the object data
//                string body = $"Name: {employeess.Name} \n";
//                body += $"Gender: {employeess.Gender} \n";
//                body += $"Designation: {employeess.Designation}  \n";
//                body += $"City: {employeess.city}  \n";
//                body += $"Salary: {employeess.salary}  \n";
//                //body += $"Image: {employeess.Image}   \n";
//                //body += $"SalaryId: {employeess.SalaryId}   \n";
//                body += $"Email: {employeess.Email}   \n";

//                // for sending image
//                string fileName = Path.GetFileName(employeess.file.FileName);
//                mm.Attachments.Add(new Attachment(employeess.file.InputStream, fileName));


//                mm.Body = body;
//                mm.IsBodyHtml = false;

//                SmtpClient smtp = new SmtpClient();
//                smtp.Host = "smtp.gmail.com";
//                smtp.Port = 587;
//                smtp.EnableSsl = true;

//                NetworkCredential nc = new NetworkCredential("zeeshanasgar07@gmail.com", "desydnsxnecdafeu");
//                smtp.UseDefaultCredentials = true;
//                smtp.Credentials = nc;
//                smtp.Send(mm);

//                // for email data
//            }
//            catch (Exception)
//            {
//                log.Error("Email Exception Occurs while uploading image");
//            }

//            var employee = new EmployeeTable()
//            {
//                //Id = employeess.Id,
//                Name = employeess.Name,
//                Gender = employeess.Gender,
//                Designation = employeess.Designation,
//                city = employeess.city,
//                salary = employeess.salary,
//                Image = employeess.Image,
//                SalaryId = employeess.SalaryId,    // new table
//                Email = employeess.Email,    //new table
//                //file = employeess.file
//            };

//            db.EmployeeTable.Add(employee);

//            db.SaveChanges();
//        }


//        public void Delete(int id)
//        {
//            log4net.Config.BasicConfigurator.Configure();         // by me
//            log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
//            try
//            {
//                var data = db.EmployeeTable.Where(e => e.Id == id).SingleOrDefault();
//                db.EmployeeTable.Remove(data);



//                db.SaveChanges();
//            }
//            catch (Exception)
//            {
//                log.Error("Delete Exception");
//            }

//        }


//        public EmployeeTable Edit(int id)
//        {
//            db = new JqueryDataTableDBEntities();
//            var data = db.EmployeeTable.Where(x => x.Id == id).SingleOrDefault();
//            db.SaveChanges();
//            return data;
//        }

//        public void Update(EmployeeTable employee)
//        {
//            //update here working

//            string filename = Path.GetFileName(employee.file.FileName);
//            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
//            //string extension = Path.GetExtension(employeess.file.FileName);
//            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/image/"), _filename);
//            employee.Image = "image/" + _filename;
//            employee.file.SaveAs(path);

//            // for email data working
//            var emp = new EmployeeTable()
//            {
//                Name = employee.Name,
//                Gender = employee.Gender,
//                Designation = employee.Designation,
//                city = employee.city,
//                salary = employee.salary,
//                Image = employee.Image,
//                SalaryId = employee.SalaryId,    // new table
//                Email = employee.Email,    //new table
//                //file = employeess.file
//            };
//            MailMessage mm = new MailMessage("zeeshanasgar07@gmail.com", employee.Email);
//            mm.Subject = "This is your Updated Data";
//            // Construct the email body with the object data
//            string body = $"Name: {employee.Name} \n";
//            body += $"Gender: {employee.Gender} \n";
//            body += $"Designation: {employee.Designation}  \n";
//            body += $"City: {employee.city}  \n";
//            body += $"Salary: {employee.salary}  \n";
//            //body += $"Image: {employeess.Image}   \n";
//            //body += $"SalaryId: {employeess.SalaryId}   \n";
//            body += $"Email: {employee.Email}   \n";

//            // for sending image
//            string fileName = Path.GetFileName(employee.file.FileName);
//            mm.Attachments.Add(new Attachment(employee.file.InputStream, fileName));


//            mm.Body = body;
//            mm.IsBodyHtml = false;

//            SmtpClient smtp = new SmtpClient();
//            smtp.Host = "smtp.gmail.com";
//            smtp.Port = 587;
//            smtp.EnableSsl = true;

//            NetworkCredential nc = new NetworkCredential("zeeshanasgar07@gmail.com", "desydnsxnecdafeu");
//            smtp.UseDefaultCredentials = true;
//            smtp.Credentials = nc;
//            smtp.Send(mm);

//            db.EmployeeTable.AddOrUpdate(employee);

//            db.SaveChanges();
//        }


//        public void Export(EmployeeTable employee)
//        {

//        }


//        public void ExportCheck(int[] selectedFruits)
//        {

//        }


















//    }
//}