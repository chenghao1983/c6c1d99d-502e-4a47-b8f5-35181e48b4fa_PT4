using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Common;
using ActiveLearning.DB;
using System.Linq;
using ActiveLearning.Web.Filter;
using System.Web.SessionState;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveLearning.Web.Controllers
{
    [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Student_Code)]
    public class StudentController : BaseController
    {
        #region Index

        public ActionResult Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            return View();
        }
        #endregion

        #region course
        [OutputCache(Duration = Cache_Length)]
        public ActionResult CourseList()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {
                var courseList = courseManager.GetEnrolledCoursesByStudentSid(GetLoginUser().Students.FirstOrDefault().Sid, out message);
                if (courseList == null || courseList.Count() == 0)
                {
                    SetViewBagError(message);
                }
                return View(courseList);
            }
        }
        #endregion

        #region chat
        public ActionResult Chat(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            var claims = new List<Claim>();


            claims.Add(new Claim(ClaimTypes.GroupSid, courseSid.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, GetLoginUser().FullName));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
            {
                //ExpiresUtc = DateTime.UtcNow.(200),
                IsPersistent = true
            }, identity);

            using (var chatManager = new ChatManager())
            {
                var chatHistory = chatManager.GetChatHistoryByCourseSid(courseSid, out message);
                ViewBag.CourseSid = courseSid;
                ViewBag.StudentSid = GetLoginUser().Students.FirstOrDefault().Sid;
                SetBackURL("courselist");
                return View(chatHistory);
            }
        }
        #endregion

        #region quiz
        [OutputCache(Duration = Cache_Length)]
        public ActionResult Quiz(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                var allQuizs = quizManager.GetActiveQuizQuestionsByCourseSid(courseSid, out message);
                if (allQuizs == null || allQuizs.Count() == 0)
                {
                    SetViewBagError(message);
                }
                ViewBag.CourseSid = courseSid;
                SetBackURL("courselist");
                return View(allQuizs);
            }
        }
        #endregion

        #region content
        [OutputCache(Duration = Cache_Length)]
        public ActionResult Content(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            List<Content> items = new List<Content>();
            using (var contentManager = new ContentManager())
            {
                message = string.Empty;
                var contents = contentManager.GetContentsByCourseSid(courseSid, out message);
                if (contents != null)
                {
                    items = contents.ToList();
                }
                else
                {
                    SetViewBagError(message);
                }
            }
            ViewBag.CourseSid = courseSid;
            SetBackURL("courselist");
            return View(items);
        }

        public ActionResult Download(int courseSid, int contentSid, string originalFileName)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            string filepath;
            string fileType;
            using (var contentManager = new ContentManager())
            {
                var content = contentManager.GetContentByContentSid(contentSid, out message);
                if (content == null)
                {
                    return RedirectToError(message);
                }
                filepath = content.Path + content.FileName;
                fileType = content.Type;
            }
            var file = File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
            if (file == null)
            {
                return RedirectToError(ActiveLearning.Business.Common.Constants.ValueNotFound(ActiveLearning.Business.Common.Constants.File));
            }
            if (fileType.Equals(ActiveLearning.Business.Common.Constants.Content_Type_Video))
            {
                ViewBag.VideoPath = filepath;
                SetBackURL("Content?courseSid=" + courseSid);
                return View("Video");
            }
            else if (fileType.Equals(ActiveLearning.Business.Common.Constants.Content_Type_File))
            {
                return file;
            }
            return null;
        }
        #endregion
    }
}