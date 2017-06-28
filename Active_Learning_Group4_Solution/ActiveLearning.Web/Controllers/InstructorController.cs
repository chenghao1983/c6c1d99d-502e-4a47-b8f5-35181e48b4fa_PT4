using ActiveLearning.DB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Web.Filter;
using System.Threading.Tasks;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Controllers
{
    [CustomAuthorize(Roles = Business.Common.Constants.User_Role_Instructor_Code)]
    public class InstructorController : BaseController
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



        #region Course
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
                var courseList = courseManager.GetEnrolledCoursesByInstructorSid(GetLoginUser().Instructors.FirstOrDefault().Sid, out message);
                if (courseList == null || courseList.Count() == 0)
                {
                    SetViewBagError(message);
                }
                GetErrorAneMessage();
                return View(courseList);
            }
        }
        #endregion

        #region Chat
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
                ViewBag.InstructorSid = GetLoginUser().Instructors.FirstOrDefault().Sid;
                SetBackURL("courselist");
                return View(chatHistory);
            }
        }
        #endregion

        #region Content
        [HttpGet]
        public ActionResult ManageContent(int courseSid)
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
            ViewBag.CourseSid = courseSid;
            var items = new List<Content>();
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
                    return View(items);
                }
            }
            GetErrorAneMessage();
            SetBackURL("courselist");
            return View(items);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file, int courseSid)
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
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToError(Business.Common.Constants.ValueIsEmpty("UrlReferrer"));
            //}

            using (var contentManger = new ContentManager())
            {
                var content = contentManger.AddContent(this, file, courseSid, out message);
                if (content != null)
                {
                    SetTempDataMessage(Business.Common.Constants.ValueSuccessfuly("File has been uploaded"));
                }
                else
                {
                    SetTempDataError(message);
                }
            }
            //return new RedirectResult(Request.UrlReferrer.ToString());
            return RedirectToAction("ManageContent", new { courseSid = courseSid });
        }


        public ActionResult Delete(int courseSid, int contentSid)
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
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToError(Business.Common.Constants.ValueIsEmpty("UrlReferrer"));
            //}
            using (var contentManager = new ContentManager())
            {
                if (contentManager.DeleteContent(this, contentSid, out message))
                {
                    SetTempDataMessage(Business.Common.Constants.ValueSuccessfuly("File hase been deleted"));
                }
                else
                {
                    SetTempDataError(message);
                }
            }
            return RedirectToAction("ManageContent", new { courseSid = courseSid });
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
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToError(Business.Common.Constants.ValueIsEmpty("UrlReferrer"));
            //}
            string filepath = string.Empty;
            string fileType;
            FileResult file = null;
            using (var contentManager = new ContentManager())
            {
                var content = contentManager.GetContentByContentSid(contentSid, out message);
                if (content == null)
                {
                    SetTempDataError(message);
                    return RedirectToAction("ManageContent", new { courseSid = courseSid });
                    //return RedirectToError(message);
                }

                fileType = content.Type;
                if (Util.IsUploadLocationFileSystem())
                {
                    filepath = content.Path + content.FileName;
                    file = File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
                }
                else
                {
                    var storage = Util.OpenCloudStorageConnection();
                    var container = Util.OpenCloudStorageContainer(storage);
                    filepath = container.Uri + "/" + content.FileName;
                    try
                    {
                        using (var client = new System.Net.WebClient())
                        {
                            byte[] contents = client.DownloadData(filepath);
                            file = File(contents, System.Net.Mime.MediaTypeNames.Application.Octet, originalFileName);
                        }

                    }
                    catch (Exception ex)
                    {
                        Util.ExceptionLog(ex);
                        return RedirectToError(null);
                    }
                }
            }

            if (file == null)
            {
                SetTempDataError(ActiveLearning.Business.Common.Constants.ValueNotFound(ActiveLearning.Business.Common.Constants.File));
                return RedirectToAction("ManageContent", new { courseSid = courseSid });
                //return RedirectToError());
            }
            if (fileType.Equals(ActiveLearning.Business.Common.Constants.Content_Type_Video))
            {
                ViewBag.VideoPath = filepath;
                SetBackURL("ManageContent?courseSid=" + courseSid);
                return View("Video");
            }
            else if (fileType.Equals(ActiveLearning.Business.Common.Constants.Content_Type_File))
            {
                return file;
            }
            return RedirectToError(null);
        }
        #endregion

        #region Quiz

        // GET: ManageQuiz
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ManageQuiz(int courseSid)
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
                var listQuiz = quizManager.GetActiveQuizQuestionsByCourseSid(courseSid, out message);
                if (listQuiz == null)
                {
                    SetViewBagError(message);
                }
                TempData["cid"] = courseSid;
                TempData.Keep("cid");
                //TempData.Peek("cid");
                SetBackURL("courselist");
                GetErrorAneMessage();
                return View(listQuiz);
            }

        }
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateQuizQuestion()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            SetBackURL("ManageQuiz?coursesid=" + courseSid);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateQuizQuestion(QuizQuestion quizQuestion)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                if (quizManager.AddQuizQuestionToCourse(quizQuestion, courseSid, out message) == null)
                {
                    SetViewBagError(message);
                    SetBackURL("ManageQuiz?coursesid=" + courseSid);
                    return View();
                }
            }
            SetTempDataMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Question has been created"));
            return RedirectToAction("ManageQuiz", new { courseSid = courseSid });
        }


        public ActionResult DeleteQuizQuestion(int quizQuestionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                QuizQuestion quizQuesion = quizManager.GetQuizQuestionByQuizQuestionSid(quizQuestionSid, out message);
                if (quizQuesion == null)
                {
                    SetViewBagError(message);
                }
                SetBackURL("ManageQuiz?coursesid=" + courseSid);
                return View(quizQuesion);
            }
        }

        [HttpPost, ActionName("DeleteQuizQuestion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuiz(int quizQuestionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int courseSid = Convert.ToInt32(TempData.Peek("cid"));
            string message = string.Empty;
            using (var deleteQuiz = new QuizManager())
            {
                QuizQuestion quizQuestion = deleteQuiz.GetQuizQuestionByQuizQuestionSid(quizQuestionSid, out message);
                if (deleteQuiz.DeleteQuizQuestion(quizQuestion, out message))
                {
                    SetTempDataMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Question has been deleted"));
                }
                else
                {
                    SetTempDataError(message);
                    return View(quizQuestion);
                }
                SetBackURL("ManageQuiz?coursesid=" + courseSid);
                return RedirectToAction("ManageQuiz", new { courseSid = courseSid });
            };
        }

        public ActionResult EditQuizQuestion(int quizQuestionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var getQuizQuestion = new QuizManager())
            {
                QuizQuestion quizQuesion = getQuizQuestion.GetQuizQuestionByQuizQuestionSid(quizQuestionSid, out message);
                if (quizQuesion == null)
                {
                    SetBackURL("managequiz?coursesid=" + courseSid);
                    SetViewBagError(message);
                }
                TempData["QuizQuesion"] = quizQuesion;
                SetBackURL("managequiz?coursesid=" + courseSid);
                return View(quizQuesion);
            };
        }

        [HttpPost, ActionName("EditQuizQuestion")]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult updateQuizQus(QuizQuestion quizQuestion)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int courseSid = Convert.ToInt32(TempData.Peek("cid"));
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            var quizQusToUpdate = TempData.Peek("QuizQuesion") as QuizQuestion;
            quizQusToUpdate.Title = quizQuestion.Title;

            using (var updateQus = new QuizManager())
            {
                if (updateQus.UpdateQuizQuestion(quizQusToUpdate, out message))
                {
                    SetTempDataMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Question has been updated"));
                }
                else
                {
                    SetViewBagError(message);
                    SetBackURL("managequiz?coursesid=" + courseSid);
                    return View(quizQusToUpdate);
                }
                return RedirectToAction("ManageQuiz", new { courseSid = courseSid });
            }
        }

        public async Task<ActionResult> QuizStatistics(int courseSid)
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
                SetBackURL("courselist");
                return View(await quizManager.GenerateStatistics(courseSid));
            }
        }


        // GET: ManageOption
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ManageOption(int quizQuestionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            using (var quizManager = new QuizManager())
            {
                var listOption = quizManager.GetQuizOptionsByQuizQuestionSid(quizQuestionSid, out message);
                if (listOption == null)
                {
                    SetViewBagError(message);
                }
                TempData["quizQuestionSid"] = quizQuestionSid;
                TempData.Keep("quizQuestionSid");

                //TempData.Peek("cid");
                SetBackURL("managequiz?coursesid=" + courseSid);
                GetErrorAneMessage();
                return View(listOption);
            }

        }

        // GET: ManageOption/CreateQuizOption
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateQuizOption()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            int quizQuestionSid = Convert.ToInt32(TempData.Peek("quizQuestionSid"));
            SetBackURL("ManageOption?quizQuestionSid=" + quizQuestionSid);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateQuizOption(QuizOption quizOption)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));
            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            int quizQuestionSid = Convert.ToInt32(TempData.Peek("quizQuestionSid"));
            using (var quizManager = new QuizManager())
            {
                if (quizManager.AddQuizOptionToQuizQuestion(quizOption, quizQuestionSid, out message) == null)
                {
                    SetViewBagError(message);
                    SetBackURL("ManageOption?quizQuestionSid=" + quizQuestionSid);
                    return View();
                }
            }
            SetTempDataMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Option has been created"));
            return RedirectToAction("ManageOption", new { quizQuestionSid = quizQuestionSid });
        }


        public ActionResult DeleteQuizOption(int quizOptionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            int quizQuestionSid = Convert.ToInt32(TempData.Peek("quizQuestionSid"));
            using (var quizManager = new QuizManager())
            {
                QuizOption quizOption = quizManager.GetQuizOptionByQuizOptionSid(quizOptionSid, out message);
                if (quizOption == null)
                {
                    SetViewBagError(message);
                }
                SetBackURL("ManageOption?quizQuestionSid=" + quizQuestionSid);
                return View(quizOption);
            }
        }


        [HttpPost, ActionName("DeleteQuizOption")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuizOp(int quizOptionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            int quizQuestionSid = Convert.ToInt32(TempData.Peek("quizQuestionSid"));
            using (var deleteOption = new QuizManager())
            {
                QuizOption quizOption = deleteOption.GetQuizOptionByQuizOptionSid(quizOptionSid, out message);
                if (deleteOption.DeleteQuizOption(quizOption, out message))
                {
                    SetTempDataMessage(Business.Common.Constants.ValueIsSuccessful("Quiz option has been deleted"));
                }
                else
                {
                    SetViewBagError(message);
                    SetBackURL("ManageOption?quizQuestionSid=" + quizQuestionSid);
                    return View(quizOption);
                }
                return RedirectToAction("ManageOption", new { quizQuestionSid = quizQuestionSid });
            };
        }


        public ActionResult EditQuizOption(int quizOptionSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }
            int quizQuestionSid = Convert.ToInt32(TempData.Peek("quizQuestionSid"));
            using (var getQuizOption = new QuizManager())
            {
                QuizOption quizOption = getQuizOption.GetQuizOptionByQuizOptionSid(quizOptionSid, out message);
                if (quizOption == null)
                {
                    SetViewBagError(message);
                }
                TempData["QuizOption"] = quizOption;
                SetBackURL("ManageOption?quizQuestionSid=" + quizQuestionSid);
                return View(quizOption);
            };
        }


        [HttpPost, ActionName("EditQuizOption")]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult updateQuizOpt(QuizOption quizOption)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            int courseSid = Convert.ToInt32(TempData.Peek("cid"));

            string message = string.Empty;
            if (!HasAccessToCourse(courseSid, out message))
            {
                return RedirectToError(message);
            }

            int quizQuestionSid = Convert.ToInt32(TempData.Peek("quizQuestionSid"));

            var quizOptToUpdate = TempData.Peek("QuizOption") as QuizOption;
            quizOptToUpdate.Title = quizOption.Title;

            quizOptToUpdate.IsCorrect = quizOption.IsCorrect;

            using (var updateOpt = new QuizManager())
            {
                if (updateOpt.UpdateQuizOption(quizOptToUpdate, out message))
                {
                    SetTempDataMessage(Business.Common.Constants.ValueIsSuccessful("Quiz Option has been updated"));

                }
                else
                {
                    SetViewBagError(message);
                    SetBackURL("ManageOption?quizQuestionSid=" + quizQuestionSid);
                    return View(quizOptToUpdate);
                }
                return RedirectToAction("ManageOption", new { quizQuestionSid = quizQuestionSid });
            }
        }
        #endregion
    }
}