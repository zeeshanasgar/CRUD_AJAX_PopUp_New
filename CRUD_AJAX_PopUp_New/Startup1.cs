using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Services.Description;
using Hangfire.SqlServer;
using Hangfire;
using CRUD_AJAX_PopUp_New.Controllers;
using Employee.IBL;
using Employee.BL;

//[assembly: OwinStartup(typeof(CRUD_AJAX_PopUp_New.Startup1))]

namespace CRUD_AJAX_PopUp_New
{
    public class Startup1
    {
        [System.Obsolete]
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/LoginGoogle"),
                SlidingExpiration = true
            });

            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "93791973171-hagb5nqba882rrm413uff4utpb72sdrl.apps.googleusercontent.com",
                ClientSecret = "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",
                Scope = { "https://www.googleapis.com/auth/userinfo.profile" }
            };

            app.UseGoogleAuthentication(googleOptions);


            //not in use
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "93791973171-hagb5nqba882rrm413uff4utpb72sdrl.apps.googleusercontent.com",
            //    ClientSecret = "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",
            //});


            IEmployeeRepository employeeRepository = new EmployeeRepository();

            JobStorage.Current = new SqlServerStorage(@"Server=.\sqlexpress; Database=JqueryDataTableDB; Integrated Security=SSPI;");
            SchdulerController schdulerController = new SchdulerController(employeeRepository);
            RecurringJob.AddOrUpdate(() => schdulerController.RunSchedulerMethod(), Cron.Minutely);
            app.UseHangfireServer();


        }

    }
}
