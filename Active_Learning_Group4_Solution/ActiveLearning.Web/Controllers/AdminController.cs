using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveLearning.Business.Implementation;
using ActiveLearning.DB;
using System.Security.Principal;
using System.Security.Claims;
using ActiveLearning.Web.Filter;
using ActiveLearning.Business.Common;
using ActiveLearning.Business.Interface;
using System.Text;

namespace ActiveLearning.Web.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin)]
    public class AdminController : BaseController
    {
        private IManagerFactoryBase<ICourseManager> _CourseManagerfactory { get; set; }
        private IManagerFactoryBase<IUserManager> _UserManagerfactory { get; set; }

        public AdminController()
        {
            _CourseManagerfactory = new CourseManager();
            _UserManagerfactory = new UserManager();
        }

        public AdminController(IManagerFactoryBase<ICourseManager> _courseFactory, IManagerFactoryBase<IUserManager> _userFactory)
        {
            _CourseManagerfactory = _courseFactory;
            _UserManagerfactory = _userFactory;
            User user = new DB.User();
            user.Sid = 1;
            user.CreateDT = DateTime.Parse("2015-01-01");
            user.Role = "A";
            user.Username = "Bruce";
            user.FullName = "Bruce Lee";
            user.Admins.Add(new Admin { Sid=1, UserSid = 1});
            LogUserIn(user);
        }

        public ActionResult Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            return View();
        }

        #region Course

        public ActionResult ManageCourse()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var courseManager = _CourseManagerfactory.Create())
            {
                var listCourse = courseManager.GetAllCourses(out message);
                if (listCourse == null || listCourse.Count() == 0)
                {
                    SetViewBagError(message);
                    return View(listCourse);
                }
                GetErrorAneMessage();
                return View(listCourse);
            }
        }

        public ActionResult CreateCourse()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            SetBackURL("ManageCourse");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateCourse(Course course)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var courseManager = _CourseManagerfactory.Create())
            {
                var newcourse = courseManager.AddCourse(course, out message);
                if (newcourse == null)
                {
                    SetViewBagError(message);
                    SetBackURL("ManageCourse");
                    return View();
                }
            }
            SetTempDataMessage(Constants.ValueSuccessfuly("Course has been created"));
            SetBackURL("ManageCourse");
            return RedirectToAction("ManageCourse");
        }

        public ActionResult DeleteCourse(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var deleteCourse = new CourseManager())
            {
                Course course = deleteCourse.GetCourseByCourseSid(courseSid, out message);
                if (course == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("ManageCourse");
                return View(course);
            };
        }

        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCou(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var courseManager = new CourseManager())
            {
                if (courseManager.DeleteCourse(courseSid, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Course has been deleted"));
                    return RedirectToAction("ManageCourse");
                }
                else
                {
                    var course = courseManager.GetCourseByCourseSid(courseSid, out message);
                    SetViewBagError(message);
                    SetBackURL("ManageCourse");
                    return View(course);
                }
            };
        }

        public ActionResult EditCourse(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var getCourse = new CourseManager())
            {
                Course course = getCourse.GetCourseByCourseSid(courseSid, out message);
                if (course == null)
                {
                    SetViewBagError(message);
                }

                TempData["EditCourse"] = course;
                SetBackURL("ManageCourse");
                return View(course);
            };
        }

        [HttpPost, ActionName("EditCourse")]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult updateCou(Course course)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            var courseToUpdate = TempData.Peek("EditCourse") as Course;
            courseToUpdate.CourseName = course.CourseName;

            using (var updateCourse = new CourseManager())
            {
                if (updateCourse.UpdateCourse(courseToUpdate, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Course has been updated"));
                    TempData.Remove("EditCourse");
                    return RedirectToAction("ManageCourse");
                }
                SetViewBagError(message);
                SetBackURL("ManageCourse");
                return View(courseToUpdate);
            }
        }
        #endregion

        #region Instructor

        public ActionResult ManageInstructor()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var userManager = new UserManager())
            {
                var listInstructor = userManager.GetAllInstructor(out message);
                GetErrorAneMessage();
                return View(listInstructor);
            }
        }

        public ActionResult EditInstructor(int instructorSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var getInstructor = new UserManager())
            {
                var instructor = getInstructor.GetInstructorByInstructorSid(instructorSid, out message);
                if (instructor == null)
                {
                    SetViewBagError(message);
                }
                else
                {
                    instructor.User.Password = string.Empty;
                }
                TempData["EditInstructor"] = instructor;
                SetBackURL("ManageInstructor");
                return View(instructor);
            };
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("EditInstructor")]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult updateIns(Instructor instructor)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            if (GetLoginUser() == null)
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            var instructorToUpdate = TempData.Peek("EditInstructor") as Instructor;
            instructorToUpdate.User.Username = instructor.User.Username;
            instructorToUpdate.User.Password = instructor.User.Password;
            instructorToUpdate.User.FullName = instructor.User.FullName;
            instructorToUpdate.Qualification = instructor.Qualification;

            using (var updateInstructor = new UserManager())
            {
                if (updateInstructor.UpdateInstructor(instructorToUpdate, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Instructor has been updated"));
                    TempData.Remove("EditInstructor");
                    return RedirectToAction("ManageInstructor");
                }
                SetViewBagError(message);
                SetBackURL("ManageInstructor");
                return View(instructorToUpdate);
            }
        }

        public ActionResult CreateInstructor()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            SetBackURL("ManageInstructor");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateInstructor(Instructor instructor)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var userManager = new UserManager())
            {
                var newInstructor = userManager.AddInstructor(instructor, out message);
                if (newInstructor == null)
                {
                    SetViewBagError(message);
                    SetBackURL("ManageInstructor");
                    return View();
                }
                else
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Instructor has been created"));
                    SetBackURL("ManageInstructor");
                    return RedirectToAction("ManageInstructor");
                }
            }
        }

        public ActionResult DeleteInstructor(int instructorSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var deleteInstructor = new UserManager())
            {
                Instructor instructor = deleteInstructor.GetInstructorByInstructorSid(instructorSid, out message);
                if (instructor == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("ManageInstructor");
                return View(instructor);
            };
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteInstructor")]
        public ActionResult DeleteIns(int instructorSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var deleteInstructor = new UserManager())
            {
                Instructor instructor = deleteInstructor.GetInstructorByInstructorSid(instructorSid, out message);
                if (deleteInstructor.DeleteInstructor(instructor, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Instructor has been deleted"));
                    return RedirectToAction("ManageInstructor");
                }
                else
                {
                    SetViewBagError(message);
                    SetBackURL("ManageInstructor");
                    return View(instructor);
                }
            };
        }

        public ActionResult InstructorDetails(int instructorSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var getInstructor = new UserManager())
            {
                var instructor = getInstructor.GetInstructorByInstructorSid(instructorSid, out message);
                if (instructor == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("ManageInstructor");
                return View(instructor);
            };
        }

        public ActionResult ActivateInstructor(int instructorSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var activateInstructor = new UserManager())
            {
                var instructor = activateInstructor.GetInstructorByInstructorSid(instructorSid, out message);
                if (activateInstructor.ActivateInstructor(instructorSid, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Instructor has been activated"));
                }
                else
                {
                    SetTempDataError(message);
                }
                return RedirectToAction("ManageInstructor");
            };
        }

        public ActionResult DeactivateInstructor(int instructorSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var deactivateInstructor = new UserManager())
            {
                if (deactivateInstructor.DeactivateInstructor(instructorSid, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Instructor has been deactivated"));
                }
                else
                {
                    SetTempDataError(message);
                }
                return RedirectToAction("ManageInstructor");
            };
        }
        #endregion

        #region Student
        public ActionResult ManageStudent()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var userManager = _UserManagerfactory.Create())
            {
                var listStudent = userManager.GetAllStudent(out message);
                GetErrorAneMessage();
                if (listStudent == null)
                {
                    SetViewBagError(message);
                }
                return View(listStudent);
            }
        }

        public ActionResult StudentDetails(int studentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var getStudent = new UserManager())
            {
                Student student = getStudent.GetStudentByStudentSid(studentSid, out message);
                if (student == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("ManageStudent");
                return View(student);
            };
        }

        public ActionResult CreateStudent()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            SetBackURL("ManageStudent");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CreateStudent(Student student)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var userManager = _UserManagerfactory.Create())
            {
                var newStudent = userManager.AddStudent(student, out message);
                if (newStudent == null)
                {
                    SetViewBagError(message);
                    SetBackURL("ManageStudent");
                    return View();
                }
                else
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Student has been created"));
                    return RedirectToAction("ManageStudent");
                }
            }
        }

        public ActionResult EditStudent(int studentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var getStudent = new UserManager())
            {
                var student = getStudent.GetStudentByStudentSid(studentSid, out message);
                if (student == null)
                {
                    SetViewBagError(message);
                }
                else
                {
                    student.User.Password = string.Empty;
                }
                TempData["EditStudent"] = student;
                SetBackURL("ManageStudent");
                return View(student);
            };
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("EditStudent")]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult updateStu(Student student)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            var studentToUpdate = TempData.Peek("EditStudent") as Student;
            studentToUpdate.User.Username = student.User.Username;
            studentToUpdate.User.Password = student.User.Password;
            studentToUpdate.User.FullName = student.User.FullName;

            studentToUpdate.BatchNo = student.BatchNo;
            using (var userManager = new UserManager())
            {
                if (userManager.UpdateStudent(studentToUpdate, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Student has been updated"));
                    TempData.Remove("EditStudent");
                    return RedirectToAction("ManageStudent");
                }
                else
                {
                    SetViewBagError(message);
                    SetBackURL("ManageStudent");
                    return View(studentToUpdate);
                }
            }
        }

        public ActionResult DeleteStudent(int studentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var deleteStudent = new UserManager())
            {
                Student student = deleteStudent.GetStudentByStudentSid(studentSid, out message);
                if (student == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("ManageStudent");
                return View(student);
            };

        }


        // POST: ManageStudent/DeleteStudent/6
        [HttpPost, ActionName("DeleteStudent")]
        public ActionResult Delete(int studentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var deleteStudent = new UserManager())
            {
                Student student = deleteStudent.GetStudentByStudentSid(studentSid, out message);
                if (deleteStudent.DeleteStudent(student, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Student has been deleted"));
                    return RedirectToAction("ManageStudent");
                }
                else
                {
                    SetViewBagError(message);
                    return View(student);
                }
            };
        }

        public ActionResult ActivateStudent(int studentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var userManager = new UserManager())
            {
                if (userManager.ActivateStudent(studentSid, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Student has been activated"));
                }
                else
                {
                    SetTempDataError(message);
                }
                return RedirectToAction("ManageStudent");
            };
        }

        public ActionResult DeactivateStudent(int studentSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var userManager = new UserManager())
            {
                if (userManager.DeactivateStudent(studentSid, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Student has been deactivated"));
                }
                else
                {
                    SetTempDataError(message);
                }
                return RedirectToAction("ManageStudent");
            };
        }
        #endregion

        #region Enrolment

        public ActionResult ManageStudentEnrolment(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var enrol = new CourseManager())
            {
                var listStudent = enrol.GetAllStudentsWithHasEnrolledIndicatorByCourseSid(courseSid, out message);
                if (listStudent == null)
                {
                    SetViewBagError(message);
                    return View(new List<Student>());
                }
                else
                {
                    TempData["CourseId"] = courseSid;
                    TempData["EntrolStudent"] = listStudent.ToList();

                    GetErrorAneMessage();
                    SetBackURL("ManageCourse");
                    return View(listStudent.ToList());
                }
            }
        }

        // POST: ManageCourse/UpdateStudentEnrolment
        //[HttpPost, ActionName("ManageEnrolment")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult UpdateStudentEnrolment(IList<Student> student)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            var studentEnrol = TempData.Peek("EntrolStudent") as List<Student>;
            int courseId = Convert.ToInt32(TempData["CourseId"]);
            for (int i = 0; i < studentEnrol.Count(); i++)
            {
                studentEnrol[i].HasEnrolled = student[i].HasEnrolled;
            }

            using (var enrolStudent = new CourseManager())
            {
                if (enrolStudent.UpdateStudentsCourseEnrolmentByHasEnrolledIndicator(studentEnrol, courseId, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly(Constants.Student_Course_Enrolment + "has been updated"));
                }
                else
                {
                    SetTempDataError(message);
                }
                TempData.Remove("EntrolStudent");
                return RedirectToAction("ManageStudentEnrolment", new { courseSid = courseId });
            }
        }

        public ActionResult ManageInstructorEnrolment(int courseSid)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var enrol = new CourseManager())
            {
                var listInstructor = enrol.GetAllInstructorsWithHasEnrolledIndicatorByCourseSid(courseSid, out message);
                if (listInstructor == null)
                {
                    SetViewBagError(message);
                    return View(new List<Instructor>());
                }
                else
                {
                    GetErrorAneMessage();
                    TempData["CourseId"] = courseSid;
                    TempData["EntrolInstructor"] = listInstructor.ToList();
                    SetBackURL("ManageCourse");
                    return View(listInstructor.ToList());
                }

            }
        }

        //[HttpPost, ActionName("ManageInstructorEnrolment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult UpdateInstructorEnrolment(IList<Instructor> instructor)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            var instructorEnrol = TempData.Peek("EntrolInstructor") as List<Instructor>;
            int courseId = Convert.ToInt32(TempData["CourseId"]);
            for (int i = 0; i < instructorEnrol.Count(); i++)
            {
                instructorEnrol[i].HasEnrolled = instructor[i].HasEnrolled;
            }

            using (var enrolInstructor = new CourseManager())
            {
                if (enrolInstructor.UpdateInstructorsCourseEnrolmentByHasEnrolledIndicator(instructorEnrol, courseId, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly(Constants.Instructor_Course_Enrolment + "has been updated"));
                }
                else
                {
                    SetTempDataError(message);
                }
                TempData.Remove("EntrolInstructor");
                return RedirectToAction("ManageInstructorEnrolment", new { courseSid = courseId });
            }
        }
        #endregion
    }
}