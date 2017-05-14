using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            LogUserOut();
            return Redirect("~/Home/login");
        }

   
        #region login
        // GET: /Home/Login
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            LogUserOut();
            GetErrorAneMessage();
            return View();
        }

        //POST: /Home/Login
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login(User user)
        {
            ActionResult result = View();
            using (var userManager = new UserManager())
            {
                string message = string.Empty;
                var authenticatedUser = userManager.IsAuthenticated(user.Username, user.Password, out message);
                if (authenticatedUser != null)
                {
                    LogUserIn(authenticatedUser);
                    switch (GetLoginUserRole())
                    {
                        case Constants.User_Role_Admin_Code:
                            result = Redirect("~/Admin");
                            break;
                        case Constants.User_Role_Instructor_Code:
                            result = Redirect("~/Instructor");
                            break;
                        case Constants.User_Role_Student_Code:
                            result = Redirect("~/Student");
                            break;
                    }
                }
                SetViewBagError(message);
                
                return result;
            }
        }
        #endregion


        // POST: /Home/LogOff
        public ActionResult LogOff()
        {
            LogUserOut();
            return RedirectToLogin();
        }
    }
}