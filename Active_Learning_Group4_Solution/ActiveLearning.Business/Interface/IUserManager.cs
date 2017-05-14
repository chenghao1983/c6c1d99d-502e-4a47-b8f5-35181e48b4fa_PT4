using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Interface
{
    public interface IUserManager : IDisposable
    {
        #region User
        User GenerateHashedUser(User user, out string message);
        bool UserNameExists(string userName, out string message);
        User IsAuthenticated(string userName, string password, out string message);
        User IsAuthenticated(User user, out string message);
        bool HasAccessToCourse(User user, int courseSid, out string message);
        bool ChangePassword(User user, string oldPass, string newPass, string newPassConfirm, out string message);
        #endregion

        #region Student
        Student GetStudentByStudentSid(int studentSid, out string message);
        Student GetActiveStudentByStudentSid(int studentSid, out string message);
        IEnumerable<Student> GetAllStudent(out string message);
        IEnumerable<Student> GetAllActiveStudent(out string message);
        Student AddStudent(Student student, out string message);
        bool UpdateStudent(Student student, out string message);
        bool DeleteStudent(Student student, out string message);
        bool DeleteStudent(int studentSid, out string message);
        bool ActivateStudent(Student student, out string message);
        bool ActivateStudent(int studentSid, out string message);
        bool DeactivateStudent(Student student, out string message);
        bool DeactivateStudent(int studentSid, out string message);
        #endregion

        #region Instructor
        Instructor GetInstructorByInstructorSid(int instructorSid, out string message);
        Instructor GetActiveInstructorByInstructorSid(int instructorSid, out string message);
        IEnumerable<Instructor> GetAllInstructor(out string message);
        IEnumerable<Instructor> GetAllActiveInstructor(out string message);
        Instructor AddInstructor(Instructor instructor, out string message);
        bool UpdateInstructor(Instructor instructor, out string message);
        bool DeleteInstructor(Instructor instructor, out string message);
        bool DeleteInstructor(int instructorSid, out string message);
        bool ActivateInstructor(Instructor instructor, out string message);
        bool ActivateInstructor(int instructorSid, out string message);
        bool DeactivateInstructor(Instructor instructor, out string message);
        bool DeactivateInstructor(int instructorSid, out string message);
        #endregion

        #region Admin
        Admin GetAdminByAdminSid(int adminSid, out string message);
        Admin GetActiveAdminByAdminSid(int adminSid, out string message);
        IEnumerable<Admin> GetAllActiveAdmin(out string message);
        Admin AddAdmin(Admin admin, out string message);
        bool UpdateAdmin(Admin admin, out string message);
        bool DeleteAdmin(Admin admin, out string message);
        bool DeleteAdmin(int adminSid, out string message);
        #endregion
    }
}
