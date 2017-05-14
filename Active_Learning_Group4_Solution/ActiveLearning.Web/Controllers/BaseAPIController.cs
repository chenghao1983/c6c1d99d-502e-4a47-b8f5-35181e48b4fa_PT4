using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.DB;
using System.Reflection;
using ActiveLearning.Web.Filter;
using log4net;
using ActiveLearning.Business.Implementation;
using System.Web.Mvc;


namespace ActiveLearning.Web.Controllers
{
    public class BaseAPIController : ApiController
    {
        public static string UserSessionParam = "LoginUser";
        protected const string LF = "\r\n";
        private const string SEPARATOR = "---------------------------------------------------------------------------------------------------------------";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    }
}
