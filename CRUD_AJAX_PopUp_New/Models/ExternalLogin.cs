using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_AJAX_PopUp_New.Models
{
    public class ExternalLogin
    {
        public string Email { get; set;}
        public string FullName { get; set;}
        public bool Gender { get; set;}

    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set;}
    }
}