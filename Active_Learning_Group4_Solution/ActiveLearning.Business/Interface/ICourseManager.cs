using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Interface
{
    public interface ICourseManager : IDisposable
    {
        #region Course
        bool CourseNameExists(string courseName, out string message);
        Course GetCourseByCourseSid(int courseSid, out string message);
        IEnumerable<Course> GetAllCourses(out string message);
        Course AddCourse(Course course, out string message);
        bool UpdateCourse(Course course, out string message);
        bool DeleteCourse(Course course, out string message);
        bool DeleteCourse(int courseSid, out string message);
        #endregion

        #region Student Enrolment
        IEnumerable<Student> GetAllStudentsWithHasEnrolledIndicatorByCourseSid(int courseSid, out string message);
        IEnumerable<Student> GetEnrolledStudentsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetEnrolledStudentSidsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetEnrolledStudentUserSidsByCourseSid(int courseSid, out string message);
        IEnumerable<Student> GetNonEnrolledStudentsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetNonEnrolledStudentSidsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetNonEnrolledStudentUserSidsByCourseSid(int courseSid, out string message);
        IEnumerable<Course> GetEnrolledCoursesByStudentSid(int studentSid, out string message);
        IEnumerable<int> GetEnrolledCourseSidsByStudentSid(int studentSid, out string message);
        IEnumerable<Course> GetNonEnrolledCoursesByStudentSid(int studentSid, out string message);
        IEnumerable<int> GetNonEnrolledCourseSidsByStudentSid(int studentSid, out string message);
        bool EnrolStudentsToCourse(IEnumerable<Student> students, int courseSid, out string message);
        bool EnrolStudentsToCourse(IEnumerable<int> studentSids, int courseSid, out string message);
        bool RemoveStudentsFromCourse(IEnumerable<Student> students, int courseSid, out string message);
        bool RemoveStudentsFromCourse(IEnumerable<int> studentSids, int courseSid, out string message);
        bool UpdateStudentsCourseEnrolment(IEnumerable<Student> students, int courseSid, out string message);
        bool UpdateStudentsCourseEnrolment(IEnumerable<int> studentSids, int courseSid, out string message);
        bool UpdateStudentsCourseEnrolmentByHasEnrolledIndicator(IEnumerable<Student> students, int courseSid, out string message);
        #endregion

        #region Instructor Enrolment
        IEnumerable<Instructor> GetAllInstructorsWithHasEnrolledIndicatorByCourseSid(int courseSid, out string message);
        IEnumerable<Instructor> GetEnrolledInstructorsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetEnrolledInstructorSidsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message);
        IEnumerable<Instructor> GetNonEnrolledInstructorsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetNonEnrolledInstructorSidsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetNonEnrolledInstructorUserSidsByCourseSid(int courseSid, out string message);
        IEnumerable<Course> GetEnrolledCoursesByInstructorSid(int InstructorSid, out string message);
        IEnumerable<int> GetEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message);
        IEnumerable<Course> GetNonEnrolledCoursesByInstructorSid(int InstructorSid, out string message);
        IEnumerable<int> GetNonEnrolledCourseSidsByInstructorSid(int InstructorSid, out string message);
        bool EnrolInstructorsToCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message);
        bool EnrolInstructorsToCourse(IEnumerable<int> InstructorSids, int courseSid, out string message);
        bool RemoveInstructorsFromCourse(IEnumerable<Instructor> Instructors, int courseSid, out string message);
        bool RemoveInstructorsFromCourse(IEnumerable<int> InstructorSids, int courseSid, out string message);
        bool UpdateInstructorsCourseEnrolment(IEnumerable<Instructor> Instructors, int courseSid, out string message);
        bool UpdateInstructorsCourseEnrolment(IEnumerable<int> InstructorSids, int courseSid, out string message);
        bool UpdateInstructorsCourseEnrolmentByHasEnrolledIndicator(IEnumerable<Instructor> Instructors, int courseSid, out string message);
        #endregion
    }
}
