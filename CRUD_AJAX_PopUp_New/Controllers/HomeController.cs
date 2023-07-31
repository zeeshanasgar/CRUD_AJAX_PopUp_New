using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.EnterpriseServices.CompensatingResourceManager;
using CRUD_AJAX_PopUp_New.Models;
using System.Data;
using System.Drawing.Printing;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Employee.BL;
using Employee.IBL;
using Employee.Model;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CRUD_AJAX_PopUp_New.Controllers
{
    public class HomeController : Controller
    {
        readonly IEmployeeRepository userRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            userRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            var data =  userRepository.Index();
            ViewBag.Salary = new SelectList(data, "Salary", "Salary");
            return View();
        }
        public ActionResult GetEmployeeRecord(DataTableParamModelModel param)
        {
            var employeeData = userRepository.GetEmployeeRecord(param);

            var jsonData = new
            {
                aaData = employeeData.Data,
                sEcho = param.sEcho,
                iTotalDisplayRecords = employeeData.totalCount,
                iTotalRecords = employeeData.totalCount
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel employeess, string useremail)
        {
            userRepository.SendData(employeess, useremail);
            return Json(new { IsInvalid = true, Message = "Data Successfully Added" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            userRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult Edit(int id)
        {
            EmployeeModel data = new EmployeeModel();
            data =  userRepository.Edit(id);
            
            return Json(data, JsonRequestBehavior.AllowGet);    // heyy return   working
        }

        [HttpPost]
        public JsonResult Update(/*Employee employee*/ EmployeeModel employee)
        {
            userRepository.Update(employee);
            
            return Json("Record Updated", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Export(EmployeeModel employee)
        {
            var downloadLink = userRepository.Export(employee);
            return Json(downloadLink, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadData(string fileName)
        {
            string folderPath = Server.MapPath("~/image/");
            string filePath = Path.Combine(folderPath, fileName);

            // Provide the file for download
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/csv", fileName);
        }

        public ActionResult ExportCheck(int[] selectedFruits)
        {
            var downloadLink = userRepository.ExportCheck(selectedFruits);
            return Json(downloadLink, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadDataCheck(string fileName)
        {
            string folderPath = Server.MapPath("~/image/");
            string filePath = Path.Combine(folderPath, fileName);

            // Provide the file for download
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/csv", fileName);
        }


        //public ActionResult RunSchedulerMethod()
        //{
        //    userRepository.RunSchedulerMethod();
        //    return RedirectToAction("Index");
        //}



    }
}