using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using CRUD_AJAX_PopUp_New.Models;
//using GoogleAuthentication.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RestSharp;
using Employee.Model;
using Employee.IBL;

namespace CRUD_AJAX_PopUp_New.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account

        readonly IEmployeeRepository userRepository;
        public AccountController(IEmployeeRepository employeeRepository)
        {
            userRepository = employeeRepository;
        }

        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(MembershipModel model)
        //{
        //    //using (var context = new JqueryDataTableDBEntities())
        //    //{
        //    //    bool isValid = context.UserMaster.Any(x => (x.Name == model.username || x.Email == model.username) && x.Password == model.Password);
        //    //    if (isValid)
        //    //    {
        //    //        FormsAuthentication.SetAuthCookie(model.username, false);
        //    //        return RedirectToAction("Index", "Home");
        //    //        //return RedirectToAction("Index", "UserMasters");
        //    //    }
        //    //    else
        //    //    {
        //    //        ModelState.AddModelError("", "Invalid username or Password");
        //    //        return View();
        //    //    }
        //    //}
        //    userRepository.Login(model);


        //    return RedirectToAction("Index", "Home");
        //    //return View();
        //}

        [HttpPost]
        public ActionResult Login(MembershipModel model)
        {
            bool loginResult = userRepository.Login(model);

            if (loginResult)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                //return RedirectToAction("Login", "Account");
                return View();
            }
        }






        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserMasterModel userMasterModel)
        {
            userRepository.Signup(userMasterModel);
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }



        //[AllowAnonymous]
        public ActionResult GoogleLoginCallback(string code)
        {
            if (code != null)
            {
                var client = new RestClient("https://www.googleapis.com/oauth2/v4/token");
                var request = new RestRequest(Method.POST);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "https://localhost:44383/Account/Index");

                request.AddParameter("client_id", "93791973171-hagb5nqba882rrm413uff4utpb72sdrl.apps.googleusercontent.com");
                request.AddParameter("client_secret", "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");

                IRestResponse response = client.Execute(request);


                var content = response.Content;
                var res = (JObject)JsonConvert.DeserializeObject(content);
                var client2 = new RestClient("https://www.googleapis.com/oauth2/v1/userinfo");
                client2.AddDefaultHeader("Authorization", "Bearer " + res["access_token"]);

                request = new RestRequest(Method.GET);


                var response2 = client2.Execute(request);

                var content2 = response2.Content;

                var user = (JObject)JsonConvert.DeserializeObject(content2);
                return RedirectToAction("Index","Home");

            }
            else
            {
                ViewBag.ReturnData = "";
            }


            return RedirectToAction("Index", "Home");
        }




    }
}