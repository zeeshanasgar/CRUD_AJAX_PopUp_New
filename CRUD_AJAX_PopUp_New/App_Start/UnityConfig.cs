using Employee.BL;
using Employee.IBL;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CRUD_AJAX_PopUp_New
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IEmployeeRepository, EmployeeRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}