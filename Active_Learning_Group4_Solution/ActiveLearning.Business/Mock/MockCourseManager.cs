using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.CustomExcepetion;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Context;
using ActiveLearning.Business.Common;
using System.Transactions;
using ActiveLearning.Business.Implementation;

namespace ActiveLearning.Business.Mock
{
    public class MockCourseManager : ICourseManager, IManagerFactoryBase<ICourseManager>
    {
        public MockCourseManager()
        {


        }

        public ICourseManager Create()
        {
            return this;
        }

        public void Dispose()
        {

        }

        #region Course Mock Data

        public IQueryable MockCourses { get; set; }

        public Course MockCourse { get; set; }


        #endregion

        #region Course
        public bool CourseNameExists(string courseName, out string message)
        {
            throw new NotImplementedException();
        }
        public Course GetCourseByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Course> GetAllCourses(out string message)
        {
            message = string.Empty;
            try
            {
                var mock = MockCourses as IEnumerable<Course>;
                if (mock == null || mock.Count() == 0)
                {
                    message = Constants.ValueNotFound(Constants.Course);
                }
                return mock;
            }
            catch (Exception ex)
            {
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Course);
                return null;
            }
        }
        public Course AddCourse(Course course, out string message)
        {
            message = string.Empty;
            if (course == null)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return null;
            }
            try
            {
                if (MockCourse == null)
                {
                    throw new Exception();
                }

                return MockCourse;
            }
            catch (Exception ex)
            {
                message = Constants.OperationFailedDuringAddingValue(Constants.Course);
                return null;
            }
        }
        public bool UpdateCourse(Course course, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteCourse(Course course, out string message)
        {
            throw new NotImplementedException();
        }
        public bool DeleteCourse(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Student Enrolment
        public IEnumerable<Student> GetEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledStudentSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledStudentUserSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();

        }
        public IEnumerable<Student> GetNonEnrolledStudentsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledStudentSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledStudentUserSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();

        }
        public IEnumerable<Course> GetEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledCourseSidsByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Course> GetNonEnrolledCoursesByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledCourseSidsByStudentSid(int studentSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool EnrolStudentsToCourse(IEnumerable<Student> students, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool EnrolStudentsToCourse(IEnumerable<int> studentSids, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool RemoveStudentsFromCourse(IEnumerable<Student> students, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool RemoveStudentsFromCourse(IEnumerable<int> studentSids, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStudentsCourseEnrolment(IEnumerable<Student> students, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool UpdateStudentsCourseEnrolment(IEnumerable<int> studentSids, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Instructor enrolment
        public IEnumerable<Instructor> GetEnrolledInstructorsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledInstructorSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Instructor> GetNonEnrolledInstructorsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledInstructorSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Course> GetEnrolledCoursesByInstructorSid(int InstructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Course> GetNonEnrolledCoursesByInstructorSid(int InstructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<int> GetNonEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool EnrolInstructorsToCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool EnrolInstructorsToCourse(IEnumerable<int> InstructorSids, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool RemoveInstructorsFromCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool RemoveInstructorsFromCourse(IEnumerable<int> InstructorSids, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool UpdateInstructorsCourseEnrolment(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }
        public bool UpdateInstructorsCourseEnrolment(IEnumerable<int> InstructorSids, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStudentsCourseEnrolmentByHasEnrolledIndicator(IEnumerable<Student> students, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool UpdateInstructorsCourseEnrolmentByHasEnrolledIndicator(IEnumerable<Instructor> Instructors, int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllStudentsWithHasEnrolledIndicatorByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instructor> GetAllInstructorsWithHasEnrolledIndicatorByCourseSid(int courseSid, out string message)
        {
            throw new NotImplementedException();
        }



        #endregion

    }
}
