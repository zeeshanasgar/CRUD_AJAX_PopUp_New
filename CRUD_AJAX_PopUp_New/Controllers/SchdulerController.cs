using Employee.IBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRUD_AJAX_PopUp_New.Controllers
{
    public class SchdulerController : Controller
    {
        // GET: Schduler
        //public ActionResult Index()
        //{
        //    return View();
        //}

        readonly IEmployeeRepository userRepository;

        public SchdulerController(IEmployeeRepository employeeRepository)
        {
            userRepository = employeeRepository;
        }

        public ActionResult RunSchedulerMethod()
        {
            var data = userRepository.RunSchedulerMethod();
            //return RedirectToAction("Index", "HomeController");
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
    }
}