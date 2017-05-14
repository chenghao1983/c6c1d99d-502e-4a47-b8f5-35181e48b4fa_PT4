using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Common;
using ActiveLearning.Web.Controllers;

namespace ActiveLearning.Web.Filter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        Entities context = new Entities(); // my entity  
        private string allowedroles;
        public User user { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            user = httpContext.Session[BaseController.UserSessionParam] as User;

            if (user == null)
            {
                return false;
            }
            else
            {
                if (!allowedroles.Contains(user.Role))
                {
                    return false;
                }
                return true;
            }
        }

        public new string Roles
        {
            get
            {
                return allowedroles;
            }
            set
            {
                allowedroles = value;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }


}