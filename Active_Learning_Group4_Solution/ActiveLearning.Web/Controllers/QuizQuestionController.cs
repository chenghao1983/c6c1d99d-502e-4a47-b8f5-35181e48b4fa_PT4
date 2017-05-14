using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;
using ActiveLearning.Repository.Context;
//using ActiveLearning.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using ActiveLearning.Business.ViewModel;
using System.Web.SessionState;
using ActiveLearning.Web.Controllers;

namespace ActiveLearning.Web.Controllers
{
    public class QuizQuestionController : ApiController
    {
        public QuizQuestionController()
        { }

        public async Task<QuizQuestion> Get(int courseSid)
        {
            if (courseSid == 0)
            {
                return null;
            }
            var session = SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);

            if (session == null || session[BaseController.UserSessionParam] == null)
            {
                return null;
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            var user = session[BaseController.UserSessionParam] as User;
            string message = string.Empty;
            using (var userManager = new UserManager())
            {
                if (!userManager.HasAccessToCourse(user, courseSid, out message))
                {
                    throw new UnauthorizedAccessException(message);
                }
            }

            using (var quizManager = new QuizManager())
            {
                int studentSid = user.Students.FirstOrDefault().Sid;
                int courseID = courseSid;

                QuizQuestion nextQuestion = await quizManager.NextQuestionAsync(studentSid, courseID);

                if (nextQuestion == null)
                {
                    return null;
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return nextQuestion;
            }
        }

        public async Task<HttpResponseMessage> Post(QuizAnswer answer)
        {
            if (ModelState.IsValid)
            {
                int CourseSid = answer.CourseSid;

                var session = SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);

                if (session == null || session[BaseController.UserSessionParam] == null)
                {
                    return null;
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                var user = session[BaseController.UserSessionParam] as User;
                string message = string.Empty;
                using (var userManager = new UserManager())
                {
                    if (!userManager.HasAccessToCourse(user, CourseSid, out message))
                    {
                        throw new UnauthorizedAccessException(message);
                    }
                }

                using (var quizManager = new QuizManager())
                {
                    int studentSid = user.Students.FirstOrDefault().Sid;
                    answer.StudentSid = studentSid;

                    var isCorrect = await quizManager.StoreAsync(answer);
 
                    await quizManager.NotifyUpdates(CourseSid);
                    return Request.CreateResponse(HttpStatusCode.Created, isCorrect);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            //this.db.Dispose();
            base.Dispose(disposing);
        }





    }
}
