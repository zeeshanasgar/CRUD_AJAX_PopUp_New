using Employee.Data;
using Employee.IBL;
using Employee.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Migrations;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Web.ModelBinding;
using System.Web.Security;
using System.Net.Http;
using Squirrel;
using System.Web.UI;

namespace Employee.BL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private JqueryDataTableDBEntities db;
        public EmployeeRepository()
        {
            db = new JqueryDataTableDBEntities();
        }

        public void SendData(EmployeeModel employeess, string useremail)
        {
            log4net.Config.BasicConfigurator.Configure();         // by me
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmployeeRepository));

            try
            {
                string filename = Path.GetFileName(employeess.file.FileName);
                string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                //string extension = Path.GetExtension(employeess.file.FileName);
                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/image/"), _filename);
                employeess.Image = "/image/" + _filename;
                employeess.file.SaveAs(path);
            }
            catch (Exception)
            {
                log.Error("Adding Image Exception");
            }

            try
            {
                // for email data working
                var emp = new EmployeeTable()
                {
                    Name = employeess.Name,
                    Gender = employeess.Gender,
                    Designation = employeess.Designation,
                    city = employeess.city,
                    salary = employeess.salary,
                    Image = employeess.Image,
                    SalaryId = employeess.SalaryId,    // new table
                    Email = employeess.Email,    //new table
                                                 //file = employeess.file
                };
                MailMessage mm = new MailMessage("zeeshanasgar07@gmail.com", employeess.Email);
                mm.Subject = "This is your Data";
                // Construct the email body with the object data
                string body = $"Name: {employeess.Name} \n";
                body += $"Gender: {employeess.Gender} \n";
                body += $"Designation: {employeess.Designation}  \n";
                body += $"City: {employeess.city}  \n";
                body += $"Salary: {employeess.salary}  \n";
                //body += $"Image: {employeess.Image}   \n";
                //body += $"SalaryId: {employeess.SalaryId}   \n";
                body += $"Email: {employeess.Email}   \n";

                // for sending image
                string fileName = Path.GetFileName(employeess.file.FileName);
                mm.Attachments.Add(new Attachment(employeess.file.InputStream, fileName));


                mm.Body = body;
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("zeeshanasgar07@gmail.com", "desydnsxnecdafeu");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);

                // for email data
            }
            catch (Exception)
            {
                log.Error("Email Exception Occurs while uploading image");
            }

            var employee = new EmployeeTable()
            {
                //Id = employeess.Id,
                Name = employeess.Name,
                Gender = employeess.Gender,
                Designation = employeess.Designation,
                city = employeess.city,
                salary = employeess.salary,
                Image = employeess.Image,
                SalaryId = employeess.SalaryId,    // new table
                Email = employeess.Email,    //new table
                //file = employeess.file
            };

            db.EmployeeTable.Add(employee);

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            log4net.Config.BasicConfigurator.Configure();         // by me
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmployeeRepository));
            try
            {
                var data = db.EmployeeTable.Where(e => e.Id == id).SingleOrDefault();
                db.EmployeeTable.Remove(data);

                db.SaveChanges();
            }
            catch (Exception)
            {
                log.Error("Delete Exception");
            }
        }

        public EmployeeModel Edit(int id)
        {
            //EmployeeModel emp = new EmployeeModel();
            db = new JqueryDataTableDBEntities();
            var data = db.EmployeeTable.Where(x => x.Id == id).SingleOrDefault();
            EmployeeModel emp = new EmployeeModel()
            {
                Id = data.Id,
                Name = data.Name,
                Gender = data.Gender,
                Designation = data.Designation,
                salary = data.salary,
                city = data.city,
                Image = data.Image,
                Email = data.Email,
            };
            //db.SaveChanges();
            return emp;
        }

        public void Update(EmployeeModel employee)
        {
            EmployeeTable employeeTable = new EmployeeTable();
            string filename = Path.GetFileName(employee.file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            //string extension = Path.GetExtension(employeess.file.FileName);
            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/image/"), _filename);
            employee.Image = "/image/" + _filename;
            employee.file.SaveAs(path);

            // for email data working
            var emp = new EmployeeTable()
            {
                Id = employee.Id,
                Name = employee.Name,
                Gender = employee.Gender,
                Designation = employee.Designation,
                city = employee.city,
                salary = employee.salary,
                Image = employee.Image,
                SalaryId = employee.SalaryId,    // new table
                Email = employee.Email,    //new table
                //file = employeess.file
            };
            MailMessage mm = new MailMessage("zeeshanasgar07@gmail.com", employee.Email);
            mm.Subject = "This is your Updated Data";
            // Construct the email body with the object data
            string body = $"Name: {employee.Name} \n";
            body += $"Id: {employee.Id} \n";
            body += $"Gender: {employee.Gender} \n";
            body += $"Designation: {employee.Designation}  \n";
            body += $"City: {employee.city}  \n";
            body += $"Salary: {employee.salary}  \n";
            //body += $"Image: {employeess.Image}   \n";
            //body += $"SalaryId: {employeess.SalaryId}   \n";
            body += $"Email: {employee.Email}   \n";

            // for sending image
            string fileName = Path.GetFileName(employee.file.FileName);
            mm.Attachments.Add(new Attachment(employee.file.InputStream, fileName));


            mm.Body = body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("zeeshanasgar07@gmail.com", "desydnsxnecdafeu");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);

            db.EmployeeTable.AddOrUpdate(emp);

            db.SaveChanges();
        }


        //private readonly IFileDownloader fileDownloader;
        //private readonly HttpContextBase httpContext;

        //public EmployeeRepository(IFileDownloader fileDownloader, HttpContextBase httpContext)
        //{
        //    this.fileDownloader = fileDownloader;
        //    this.httpContext = httpContext;
        //}

        public string Export(EmployeeModel employee)
        {
            List<EmployeeTable> list = new List<EmployeeTable>();
            using (JqueryDataTableDBEntities dc = new JqueryDataTableDBEntities())
            {
                list = dc.EmployeeTable.ToList();
            }
            if (list.Count > 0)
            {
                string header = @"""Id"", ""Name"", ""Gender"",""Designation"",""city"",""salary"",""Image""";

                string folderpath = System.Web.HttpContext.Current.Server.MapPath("~/image/");
                string fileName = "EmployeeData2.csv";

                string filePath = Path.Combine(folderpath, fileName);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(header);

                foreach (var i in list)
                {
                    sb.AppendLine(string.Join(",",
                        string.Format(@"""{0}""", i.Id),
                        string.Format(@"""{0}""", i.Name),
                        string.Format(@"""{0}""", i.Gender),
                        string.Format(@"""{0}""", i.Designation),
                        string.Format(@"""{0}""", i.city),
                        string.Format(@"""{0}""", i.salary),
                        string.Format(@"""{0}""", i.Image),
                        string.Format(@"""{0}""", i.Email)
                        ));
                }
                System.IO.File.WriteAllText(filePath, sb.ToString());

                //string downloadLink = downloadData(fileName);
                string downloadLink = "/Home/DownloadData?fileName=" + HttpUtility.UrlEncode(fileName);
                return downloadLink;
            }
            return null;
        }


        //public byte[] downloadData(string fileName)
        //{
        //    string folderPath = System.Web.HttpContext.Current.Server.MapPath("~/image/");
        //    string filePath = Path.Combine(folderPath, fileName);

        //    // Provide the file for download
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    //return File(fileBytes, "text/csv", fileName);
        //    return fileBytes;
        //}


        public string ExportCheck(int[] selectedFruits)
        {
            List<EmployeeTable> list = new List<EmployeeTable>();
            using (JqueryDataTableDBEntities dc = new JqueryDataTableDBEntities())
            {
                list = dc.EmployeeTable.ToList();
            }
            string header = @"""Id"", ""Name"", ""Gender"",""Designation"",""city"",""salary"",""Image""";

            string folderpath = System.Web.HttpContext.Current.Server.MapPath("~/image/");
            string fileName = "SelectedFile.csv";

            string filePath = Path.Combine(folderpath, fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            if (selectedFruits != null && selectedFruits.Length > 0)
            {
                // Iterate over the selected fruits
                foreach (int selectedData in selectedFruits)
                {
                    // Filter the store data based on the selected fruit
                    var filteredData = list.Where(data => data.Id == selectedData);

                    // Append the filtered data to csvData
                    foreach (var i in filteredData)
                    {
                        // Format the data as a CSV row and append to csvData
                        sb.AppendLine(string.Join(",",
                        string.Format(@"""{0}""", i.Id),
                        string.Format(@"""{0}""", i.Name),
                        string.Format(@"""{0}""", i.Gender),
                        string.Format(@"""{0}""", i.Designation),
                        string.Format(@"""{0}""", i.city),
                        string.Format(@"""{0}""", i.salary),
                        string.Format(@"""{0}""", i.Image),
                        string.Format(@"""{0}""", i.Email)

                        ));
                    }
                }
            }
            System.IO.File.WriteAllText(filePath, sb.ToString());

            string downloadLink = "/Home/DownloadDataCheck?fileName=" + HttpUtility.UrlEncode(fileName);
            return downloadLink;
        }

        //public void Login(MembershipModel model)
        //{
        //    using (var context = new JqueryDataTableDBEntities())
        //    {
        //        bool isValid = context.UserMaster.Any(x => (x.Name == model.username || x.Email == model.username) && x.Password == model.Password);
        //        if (isValid)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.username, false);
        //            //return RedirectToAction("Index", "Home");
        //            //return RedirectToAction("Index", "UserMasters");
        //            var httpContext = System.Web.HttpContext.Current;
        //            httpContext.Response.Redirect("~/Home/Index"); // Replace "Home/Index" with the desired controller/action URL

        //            // Optionally, you can end the request to prevent further execution
        //            httpContext.Response.End();
        //        }
        //        //else
        //        //{
        //        //    ModelState.AddModelError("", "Invalid username or Password");
        //        //    return View();
        //        //}
        //    }
        //}

        
        public bool Login(MembershipModel model)
        {
            using (var context = new JqueryDataTableDBEntities())
            {
                bool isValid = context.UserMaster.Any(x => (x.Name == model.username || x.Email == model.username) && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);
                    return true; // Login success
                }
                else
                {
                    return false; // Login failure
                }
            }
        }














        public void Signup(UserMasterModel userMasterModel)
        {
            UserMaster userMaster = new UserMaster();
            //using (var context = new JqueryDataTableDBEntities())
            //{
            //    context.UserMaster.Add(userMaster);
            //    context.SaveChanges();
            //}
            var userMaster1 = new UserMaster()
            {
                //Id = employeess.Id,
                Id = userMasterModel.Id,
                Name = userMasterModel.Name,
                Email = userMasterModel.Email,
                Password = userMasterModel.Password,
                Gender = userMasterModel.Gender,
                Address = userMasterModel.Address,
                RoleId = userMasterModel.RoleId,    // new table
            };

            db.UserMaster.Add(userMaster1);
            db.SaveChanges();

        }



        public List<SalaryModel> Index()
        {
            var salaryList = db.SalaryTable.Select(entity => new SalaryModel
            {
                //SalaryId = entity.SalaryId,
                Salary = entity.Salary,
            }).ToList();

            //var salaryList = db.SalaryTable.ToList();
            return salaryList;
            //return salaryList;
        }



        public EmployeeData GetEmployeeRecord(DataTableParamModelModel param)
        {
            //employeeList = new List<EmployeeModel>();
            //List<EmployeeTable> list = new List<EmployeeTable>();

            //EmployeeModel result = new EmployeeModel();
            EmployeeData result = new EmployeeData();

            int pageNo = 1;
            if (param.iDisplayStart >= param.iDisplayLength)
            {
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
            }
            result.pageNo = pageNo;

            int totalCount = 0;

            if (param.sSearch != null)
            {
                totalCount = db.EmployeeTable
                    .Count(x => x.Name.Contains(param.sSearch) || x.Gender.Contains(param.sSearch) || x.Designation.Contains(param.sSearch) || x.city.Contains(param.sSearch) || x.salary.ToString().Contains(param.sSearch));

                result.Data = db.EmployeeTable
                    .Where(x => x.Name.Contains(param.sSearch) || x.Gender.Contains(param.sSearch) || x.Designation.Contains(param.sSearch) || x.city.Contains(param.sSearch) || x.salary.ToString().Contains(param.sSearch))
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * param.iDisplayLength)
                    .Take(param.iDisplayLength)
                    .Select(e => new EmployeeModel
                    {
                        //yes=e.yes,
                        Designation = e.Designation,
                        Gender = e.Gender,
                        Id = e.Id,
                        Image = e.Image,
                        Name = e.Name,
                        city = e.city,
                        salary = e.salary,
                        SalaryId = e.SalaryId,
                        Email = e.Email,
                        //no=e.no,
                    })
                    .ToList();
                //return data;
                //employeeList = data;
            }
            else
            {
                totalCount = db.EmployeeTable.Count();
                result.totalCount = totalCount;
                result.Data = db.EmployeeTable
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * param.iDisplayLength)
                    .Take(param.iDisplayLength)
                    .Select(e => new EmployeeModel
                    {
                        //yes=e.yes,
                        Designation = e.Designation,
                        Gender = e.Gender,
                        Id = e.Id,
                        Image = e.Image,
                        Name = e.Name,
                        city = e.city,
                        salary = e.salary,
                        SalaryId = e.SalaryId,
                        Email = e.Email,
                        //no=e.no,
                    })
                    .ToList();
                //return data;
                //employeeList = data;
            }
            return result;
        }




        //public List<EmployeeModel> RunSchedulerMethod()
        //{

        //    using (JqueryDataTableDBEntities dc = new JqueryDataTableDBEntities())
        //    {
        //        // Retrieve data from the database and project it to a list of EmployeeModel objects
        //        List<EmployeeModel> employees = dc.EmployeeTable
        //            .Select(e => new EmployeeModel
        //            {
        //                Designation = e.Designation,
        //                Gender = e.Gender,
        //                Id = e.Id,
        //                Image = e.Image,
        //                Name = e.Name,
        //                city = e.city,
        //                salary = e.salary,
        //                SalaryId = e.SalaryId,
        //                Email = e.Email,
        //            })
        //            .ToList();

        //        MailMessage mm = new MailMessage("zeeshanasgar07@gmail.com", "zeeshanasgar07@gmail.com");

        //        List<EmployeeModel> list = new List<EmployeeModel>();


        //        mm.Body = "Here is the Employee data:\n\n";

        //        foreach (var employee in employees)
        //        {
        //            mm.Body += $"{employee.Name} ({employee.Designation}): {employee.salary} , {employee.city} , {employee.Gender}\n";
        //        }

        //        mm.Subject = "This is your Autogenerated mail Data";
        //        // Construct the email body with the object data
        //        //string body = list.ToString();


        //        //mm.Body = body;
        //        mm.IsBodyHtml = false;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;

        //        NetworkCredential nc = new NetworkCredential("zeeshanasgar07@gmail.com", "desydnsxnecdafeu");
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = nc;
        //        smtp.Send(mm);

        //        //db.EmployeeTable.AddOrUpdate();

        //        //db.SaveChanges();

        //        return list;
        //        //throw new NotImplementedException();
        //    }
        //}




        public List<EmployeeModel> RunSchedulerMethod()
        {
            try
            {
                using (JqueryDataTableDBEntities dc = new JqueryDataTableDBEntities())
                {
                    // Retrieve data from the database and project it to a list of EmployeeModel objects
                    List<EmployeeModel> employees = dc.EmployeeTable
                        .Select(e => new EmployeeModel
                        {
                            Designation = e.Designation,
                            Gender = e.Gender,
                            Id = e.Id,
                            Image = e.Image,
                            Name = e.Name,
                            city = e.city,
                            salary = e.salary,
                            SalaryId = e.SalaryId,
                            Email = e.Email,
                        })
                        .ToList();

                    MailMessage mm = new MailMessage("zeeshanasgar07@gmail.com", "zeeshanasgar07@gmail.com");

                    List<EmployeeModel> list = new List<EmployeeModel>();


                    mm.Body = "Here is the Employee data:\n\n";

                    foreach (var employee in employees)
                    {
                        mm.Body += $"{employee.Name} ({employee.Designation}): {employee.salary} , {employee.city} , {employee.Gender}\n";
                    }

                    mm.Subject = "This is your Autogenerated mail Data";
                    // Construct the email body with the object data
                    //string body = list.ToString();


                    //mm.Body = body;
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("zeeshanasgar07@gmail.com", "desydnsxnecdafeu");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);

                    //db.EmployeeTable.AddOrUpdate();

                    //db.SaveChanges();

                    return list;
                    //throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.BasicConfigurator.Configure();
                log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmployeeRepository));
                log.Error("Send Error: " + ex.Message);
            }
            return null;
        }


    }
}
