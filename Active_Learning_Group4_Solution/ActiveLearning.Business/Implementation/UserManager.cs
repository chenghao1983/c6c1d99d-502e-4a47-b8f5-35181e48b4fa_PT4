using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ActiveLearning.Repository.Context;
using System.Transactions;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Business.Implementation
{
    public class UserManager : BaseManager, IUserManager, IManagerFactoryBase<IUserManager>
    {
        public IUserManager Create()
        {
            return new UserManager();
        }

        #region user
        public bool UserNameExists(string userName, out string message)
        {
            if (string.IsNullOrEmpty(userName))
            {
                message = Constants.ValueIsEmpty(Constants.UserName);
                return true;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var user = unitOfWork.Users.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && !u.DeleteDT.HasValue).FirstOrDefault();
                    if (user != null)
                    {
                        message = Constants.ValueAlreadyExists(userName);
                        return true;
                    }
                }
                message = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.UserName);
                return true;
            }
        }
        public User GenerateHashedUser(User user, out string message)
        {
            if (user == null)
            {
                message = Constants.ValueIsEmpty(Constants.User);
                return null;
            }
            if (string.IsNullOrEmpty(user.Username))
            {
                message = Constants.ValueIsEmpty(Constants.UserName);
                return null;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                message = Constants.ValueIsEmpty(Constants.Password);
                return null;
            }
            if (!Util.IsPasswordComplex(user.Password))
            {
                message = Constants.PasswordTooSimple;
                return null;
            }
            try
            {
                string salt = Util.GenerateSalt();
                user.PasswordSalt = salt;
                user.Password = Util.CreateHash(user.Password, salt);
                message = string.Empty;
                return user;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.User);
                return null;
            }
        }
        /*
      *parameters: 
      *String userName
      *String password
      * 
      *Return user object, with respective admin, instructor and student list
      * 
      * check user.role
      * ActiveLearning.Business.Common.Constants
      * 
      * A = Admin
      * S = Student
      * I = Instructor
      */
        public User IsAuthenticated(string userName, string pass, out string messge)
        {
            // TODO, hash password and compare with DB 
            User authenticatedUser = null;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(userName.Trim()) || string.IsNullOrEmpty(pass.Trim()))
            {
                messge = Constants.PleaseEnterValue(Constants.UserName + " and " + Constants.Password); ;
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var user = unitOfWork.Users.Find(u => u.Username.Equals(userName, StringComparison.CurrentCultureIgnoreCase) && u.IsActive && !u.DeleteDT.HasValue).FirstOrDefault();

                    if (user != null)
                    {
                        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.PasswordSalt))
                        {
                            messge = Constants.ValueCorrupted(Constants.User);
                            return null;
                        }

                        if (!Util.ValidatePassword(pass, user.Password, user.PasswordSalt))
                        {
                            messge = Constants.Invalid_Username_Or_Password;
                            return authenticatedUser;
                        }
                        //InfoLog(Constants.User + " : " + user.Username + " " + Constants.Authenticated);
                        switch (user.Role)
                        {
                            case Constants.User_Role_Student_Code:
                                var student = unitOfWork.Students.SingleOrDefault(s => s.UserSid == user.Sid);
                                if (student != null)
                                {
                                    authenticatedUser = user;
                                    authenticatedUser.Students.Add(student);
                                }
                                messge = string.Empty;
                                break;
                            case Constants.User_Role_Instructor_Code:
                                var instructor = unitOfWork.Instructors.SingleOrDefault(i => i.UserSid == user.Sid);
                                if (instructor != null)
                                {
                                    authenticatedUser = user;
                                    authenticatedUser.Instructors.Add(instructor);
                                }
                                messge = string.Empty;
                                break;

                            case Constants.User_Role_Admin_Code:
                                var admin = unitOfWork.Admins.SingleOrDefault(a => a.UserSid == user.Sid);
                                if (admin != null)
                                {
                                    authenticatedUser = user;
                                    authenticatedUser.Admins.Add(admin);
                                }
                                messge = string.Empty;
                                break;

                            default:
                                messge = Constants.Invalid_Username_Or_Password;
                                return authenticatedUser;
                        }

                    }
                    else
                    {
                        messge = Constants.Invalid_Username_Or_Password;
                        return authenticatedUser;
                    }
                }
                return authenticatedUser;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                messge = Constants.OperationFailedDuringAuthentingUserValue(userName);
                return authenticatedUser;
            }
        }
        public User IsAuthenticated(User user, out string message)
        {
            if (user == null)
            {
                message = Constants.ValueIsEmpty(Constants.User);
                return null;
            }
            return IsAuthenticated(user.Username, user.Password, out message);
        }
        public bool HasAccessToCourse(User user, int courseSid, out string message)
        {
            if (user == null || user.Sid == 0 || string.IsNullOrEmpty(user.Role))
            {
                message = Constants.ValueIsEmpty(Constants.User);
                return false;
            }
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    switch (user.Role)
                    {
                        case Constants.User_Role_Student_Code:
                            if (user.Students == null || user.Students.Count() == 0)
                            {
                                message = Constants.ValueIsEmpty(Constants.Student);
                                return false;
                            }
                            var student = user.Students.FirstOrDefault();
                            var student_Course_Map = unitOfWork.Student_Course_Maps.Find(m => m.StudentSid == student.Sid && m.CourseSid == courseSid);
                            if (student_Course_Map == null || student_Course_Map.Count() == 0)
                            {
                                message = Constants.NOAccess(Constants.Course);
                                return false;
                            }
                            message = string.Empty;
                            return true;
                            break;
                        case Constants.User_Role_Instructor_Code:
                            if (user.Instructors == null || user.Instructors.Count() == 0)
                            {
                                message = Constants.ValueIsEmpty(Constants.Instructor);
                                return false;
                            }
                            var instructor = user.Instructors.FirstOrDefault();
                            var instructor_course_map = unitOfWork.Instructor_Course_Maps.Find(m => m.InstructorSid == instructor.Sid && m.CourseSid == courseSid);
                            if (instructor_course_map == null || instructor_course_map.Count() == 0)
                            {
                                message = Constants.NOAccess(Constants.Course);
                                return false;
                            }
                            message = string.Empty;
                            return true;
                            break;
                        default:
                            message = Constants.UnknownValue(Constants.Role);
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.User);
                return false;
            }

        }
        public bool ChangePassword(User user, string oldPass, string newPass, string newPassConfirm, out string message)
        {
            if (user == null || user.Sid == 0 || string.IsNullOrEmpty(user.Username))
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            if (string.IsNullOrEmpty(oldPass))
            {
                message = Constants.PleaseEnterValue("Old Password");
                return false;
            }
            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(newPass.Trim()))
            {
                message = Constants.PleaseEnterValue("New Password");
                return false;
            }
            if (string.IsNullOrEmpty(newPassConfirm) || string.IsNullOrEmpty(newPassConfirm.Trim()))
            {
                message = Constants.PleaseEnterValue("Confirmed New Password");
                return false;
            }
            if (!newPass.Trim().Equals(newPassConfirm, StringComparison.CurrentCultureIgnoreCase))
            {
                message = Constants.PleaseConfirmValue("new password");
                return false;
            }
            if (!Util.IsPasswordComplex(newPass))
            {
                message = Constants.PasswordTooSimple;
                return false;
            }
            var existingUser = IsAuthenticated(user.Username, oldPass, out message);
            if (existingUser == null)
            {
                message = Constants.Invalid_Password;
                return false;
            }
            string newPassHash = Util.CreateHash(newPass, user.PasswordSalt);

            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                try
                {
                    unitOfWork.Users.Get(user.Sid).Password = newPassHash;
                    unitOfWork.Users.Get(user.Sid).UpdateDT = DateTime.Now;
                    unitOfWork.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = Constants.OperationFailedDuringSavingValue(Constants.Password);
                    return false;
                }
            }
        }
        #endregion

        #region Student
        public Student GetStudentByStudentSid(int studentSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var student = unitOfWork.Students.Get(studentSid);

                    if (student == null)
                    {
                        message = Constants.ValueNotFound(Constants.Student);
                        return null;
                    }

                    student.User = unitOfWork.Users.Find(u => u.Sid == student.UserSid && !u.DeleteDT.HasValue).FirstOrDefault();

                    if (student.User == null)
                    {
                        message = Constants.ValueNotFound(Constants.Student);
                        return null;
                    }
                    message = string.Empty;
                    return student;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Student);
                return null;
            }
        }
        public Student GetActiveStudentByStudentSid(int studentSid, out string message)
        {
            var student = GetStudentByStudentSid(studentSid, out message);
            if (student == null || student.User == null)
            {
                return null;
            }
            if (!student.User.IsActive)
            {
                return null;
            }
            return student;
        }
        public IEnumerable<Student> GetAllStudent(out string message)
        {
            message = string.Empty;
            List<Student> list = new List<Student>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var students = unitOfWork.Students.GetAll();
                    if (students == null || students.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Student);
                        return null;
                    }

                    foreach (var student in students)
                    {
                        student.User = unitOfWork.Users.Find(u => u.Sid == student.UserSid && !u.DeleteDT.HasValue).FirstOrDefault();
                        if (student.User != null)
                        {
                            list.Add(student);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Student);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Student);
                return null;
            }
        }
        public IEnumerable<Student> GetAllActiveStudent(out string message)
        {
            var allStudents = GetAllStudent(out message);
            if (allStudents == null || allStudents.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return allStudents.Where(s => s.User.IsActive).ToList();
        }
        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Student AddStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.ValueIsEmpty(Constants.Student);
                return null;
            }
            if (string.IsNullOrEmpty(student.User.Username) || string.IsNullOrEmpty(student.User.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.UserName);
                return null;
            }
            if (UserNameExists(student.User.Username, out message))
            {
                return null;
            }
            if (string.IsNullOrEmpty(student.User.Password) || string.IsNullOrEmpty(student.User.Password.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.Password);
                return null;
            }
            if (string.IsNullOrEmpty(student.User.FullName) || string.IsNullOrEmpty(student.User.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.FullName);
                return null;
            }
            if (string.IsNullOrEmpty(student.BatchNo) || string.IsNullOrEmpty(student.BatchNo.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.BatchNo);
                return null;
            }
            var hasedUser = GenerateHashedUser(student.User, out message);
            if (hasedUser == null)
            {
                return null;
            }
            student.User = hasedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        student.User.CreateDT = DateTime.Now;
                        student.User.Role = Constants.User_Role_Student_Code;
                        unitOfWork.Students.Add(student);
                        unitOfWork.Users.Add(student.User);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return student;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.Student);
                return null;
            }
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateStudent(Student student, out string message)
        {
            message = string.Empty;
            if (student == null || student.User == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                // message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            if (string.IsNullOrEmpty(student.User.Username) || string.IsNullOrEmpty(student.User.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.UserName);
                return false;
            }
            if (string.IsNullOrEmpty(student.User.FullName) || string.IsNullOrEmpty(student.User.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.FullName);
                return false;
            }
            if (string.IsNullOrEmpty(student.BatchNo) || string.IsNullOrEmpty(student.BatchNo.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.BatchNo);
                return false;
            }
            try
            {

                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var studentToUpdate = unitOfWork.Students.Get(student.Sid);
                    Util.CopyNonNullProperty(student, studentToUpdate);
                    var userToUpdate = unitOfWork.Users.Get(student.User.Sid);
                    Util.CopyNonNullProperty(student.User, userToUpdate);
                    userToUpdate.UpdateDT = DateTime.Now;
                    if (userToUpdate.Password != null) userToUpdate.Password = userToUpdate.Password.Trim();
                    if (!Util.IsPasswordComplex(userToUpdate.Password))
                    {
                        message = Constants.PasswordTooSimple;
                        return false;
                    }
                    if (!string.IsNullOrEmpty(userToUpdate.Password))
                    {
                        userToUpdate.Password = Util.CreateHash(userToUpdate.Password, userToUpdate.PasswordSalt);
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Student);
                return false;
            }
        }
        public bool DeleteStudent(Student student, out string message)
        {
            if (student == null || student.Sid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            return DeleteStudent(student.Sid, out message);
        }
        public bool DeleteStudent(int studentSid, out string message)
        {
            message = string.Empty;
            if (studentSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            var student = GetStudentByStudentSid(studentSid, out message);
            if (student == null || student.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(student.UserSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Student);
                return false;
            }
        }
        public bool ActivateStudent(Student student, out string message)
        {
            if (student == null)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            return ActivateStudent(student.Sid, out message);
        }
        public bool ActivateStudent(int studentSid, out string message)
        {
            message = string.Empty;
            if (studentSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            var student = GetStudentByStudentSid(studentSid, out message);
            if (student == null || student.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(student.UserSid).IsActive = true;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Student);
                return false;
            }
        }
        public bool DeactivateStudent(Student student, out string message)
        {
            if (student == null)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            return DeactivateStudent(student.Sid, out message);
        }
        public bool DeactivateStudent(int studentSid, out string message)
        {
            message = string.Empty;
            if (studentSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return false;
            }
            var student = GetStudentByStudentSid(studentSid, out message);
            if (student == null || student.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(student.UserSid).IsActive = false;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Student);
                return false;
            }
        }

        #endregion

        #region Instructor
        public Instructor GetInstructorByInstructorSid(int instructorSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructor = unitOfWork.Instructors.Get(instructorSid);

                    if (instructor == null)
                    {
                        message = Constants.ValueNotFound(Constants.Instructor);
                        return null;
                    }

                    instructor.User = unitOfWork.Users.Find(i => i.Sid == instructor.UserSid && !i.DeleteDT.HasValue).FirstOrDefault();

                    if (instructor.User == null)
                    {
                        message = Constants.ValueNotFound(Constants.Instructor);
                        return null;
                    }
                    message = string.Empty;
                    return instructor;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Instructor);
                return null;
            }
        }
        public Instructor GetActiveInstructorByInstructorSid(int instructorSid, out string message)
        {
            var instructor = GetInstructorByInstructorSid(instructorSid, out message);
            if (instructor == null || instructor.User == null)
            {
                return null;
            }
            if (!instructor.User.IsActive)
            {
                return null;
            }
            return instructor;
        }

        public IEnumerable<Instructor> GetAllInstructor(out string message)
        {
            message = string.Empty;
            List<Instructor> list = new List<Instructor>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var instructors = unitOfWork.Instructors.GetAll();
                    if (instructors == null || instructors.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Instructor);
                        return null;
                    }

                    foreach (var instructor in instructors)
                    {
                        instructor.User = unitOfWork.Users.Find(u => u.Sid == instructor.UserSid && !u.DeleteDT.HasValue).FirstOrDefault();
                        if (instructor.User != null)
                        {
                            list.Add(instructor);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Instructor);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Instructor);
                return null;
            }
        }
        public IEnumerable<Instructor> GetAllActiveInstructor(out string message)
        {
            var allInstructors = GetAllInstructor(out message);
            if (allInstructors == null || allInstructors.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return allInstructors.Where(s => s.User.IsActive).ToList();
        }
        /*
         *Will check username
         *If username exists, will return null and username already exists message 
        */
        public Instructor AddInstructor(Instructor instructor, out string message)
        {
            message = string.Empty;
            if (instructor == null || instructor.User == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.ValueIsEmpty(Constants.Instructor);
                return null;
            }
            if (string.IsNullOrEmpty(instructor.User.Username) || string.IsNullOrEmpty(instructor.User.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.UserName);
                return null;
            }
            if (UserNameExists(instructor.User.Username, out message))
            {
                return null;
            }
            if (string.IsNullOrEmpty(instructor.User.Password) || string.IsNullOrEmpty(instructor.User.Password.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.Password);
                return null;
            }
            if (string.IsNullOrEmpty(instructor.User.FullName) || string.IsNullOrEmpty(instructor.User.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.FullName);
                return null;
            }
            if (string.IsNullOrEmpty(instructor.Qualification) || string.IsNullOrEmpty(instructor.Qualification.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.Qualification);
                return null;
            }
            var hasedUser = GenerateHashedUser(instructor.User, out message);
            if (hasedUser == null)
            {
                return null;
            }

            instructor.User = hasedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        instructor.User.CreateDT = DateTime.Now;
                        instructor.User.Role = Constants.User_Role_Instructor_Code;
                        unitOfWork.Instructors.Add(instructor);
                        unitOfWork.Users.Add(instructor.User);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return instructor;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.Instructor);
                return null;
            }
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateInstructor(Instructor instructor, out string message)
        {
            message = string.Empty;
            if (instructor == null || instructor.User == null)
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            if (string.IsNullOrEmpty(instructor.User.Username) || string.IsNullOrEmpty(instructor.User.Username.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.UserName);
                return false;
            }
            if (string.IsNullOrEmpty(instructor.User.FullName) || string.IsNullOrEmpty(instructor.User.FullName.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.FullName);
                return false;
            }
            if (string.IsNullOrEmpty(instructor.Qualification) || string.IsNullOrEmpty(instructor.Qualification.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.Qualification);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        instructor.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(instructor, unitOfWork.Instructors.Get(instructor.Sid));
                        var userToUpdate = unitOfWork.Users.Get(instructor.User.Sid);
                        Util.CopyNonNullProperty(instructor.User, userToUpdate);
                        if (userToUpdate.Password != null) userToUpdate.Password = userToUpdate.Password.Trim();
                        if (!Util.IsPasswordComplex(userToUpdate.Password))
                        {
                            message = Constants.PasswordTooSimple;
                            return false;
                        }
                        if (!string.IsNullOrEmpty(userToUpdate.Password))
                        {
                            userToUpdate.Password = Util.CreateHash(userToUpdate.Password, userToUpdate.PasswordSalt);
                        }
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Instructor);
                return false;
            }
        }
        public bool DeleteInstructor(Instructor instructor, out string message)
        {
            if (instructor == null)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            return DeleteInstructor(instructor.Sid, out message);
        }
        public bool DeleteInstructor(int instructorSid, out string message)
        {
            message = string.Empty;
            if (instructorSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            var instructor = GetInstructorByInstructorSid(instructorSid, out message);
            if (instructor == null || instructor.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(instructor.UserSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Instructor);
                return false;
            }
        }
        public bool ActivateInstructor(Instructor instructor, out string message)
        {
            if (instructor == null)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            return ActivateInstructor(instructor.Sid, out message);
        }

        public bool ActivateInstructor(int instructorSid, out string message)
        {
            message = string.Empty;
            if (instructorSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            var instructor = GetInstructorByInstructorSid(instructorSid, out message);
            if (instructor == null || instructor.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(instructor.UserSid).IsActive = true;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Instructor);
                return false;
            }
        }

        public bool DeactivateInstructor(Instructor instructor, out string message)
        {
            if (instructor == null)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            return DeactivateInstructor(instructor.Sid, out message);
        }

        public bool DeactivateInstructor(int instructorSid, out string message)
        {
            message = string.Empty;
            if (instructorSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Instructor);
                return false;
            }
            var instructor = GetInstructorByInstructorSid(instructorSid, out message);
            if (instructor == null || instructor.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(instructor.UserSid).IsActive = false;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Instructor);
                return false;
            }
        }
        #endregion

        #region Admin
        public Admin GetAdminByAdminSid(int adminSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var admin = unitOfWork.Admins.Get(adminSid);

                    if (admin == null)
                    {
                        message = Constants.ValueNotFound(Constants.Admin);
                        return null;
                    }

                    admin.User = unitOfWork.Users.Find(u => u.Sid == admin.UserSid && !u.DeleteDT.HasValue).FirstOrDefault();

                    if (admin.User == null)
                    {
                        message = Constants.ValueNotFound(Constants.Admin);
                        return null;
                    }
                    message = string.Empty;
                    return admin;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Admin);
                return null;
            }
        }
        public Admin GetActiveAdminByAdminSid(int adminSid, out string message)
        {
            var admin = GetAdminByAdminSid(adminSid, out message);
            if (admin == null || admin.User == null)
            {
                return null;
            }
            if (!admin.User.IsActive)
            {
                return null;
            }
            return admin;
        }
        public IEnumerable<Admin> GetAllActiveAdmin(out string message)
        {
            message = string.Empty;
            List<Admin> list = new List<Admin>();

            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var admins = unitOfWork.Admins.GetAll();
                    if (admins == null || admins.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Admin);
                        return null;
                    }

                    foreach (var admin in admins)
                    {
                        admin.User = unitOfWork.Users.Find(u => u.Sid == admin.UserSid && !u.DeleteDT.HasValue).FirstOrDefault();
                        if (admin.User != null)
                        {
                            list.Add(admin);
                        }
                    }
                    if (list.Count == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Admin);
                        return null;
                    }
                    message = string.Empty;
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Admin);
                return null;
            }
        }
        /*
        *Will check username
        *If username exists, will return null and username already exists message 
       */
        public Admin AddAdmin(Admin admin, out string message)
        {
            message = string.Empty;
            if (admin == null || admin.User == null)
            {
                message = Constants.ValueIsEmpty(Constants.Admin);
                return null;
            }
            if (string.IsNullOrEmpty(admin.User.Username.Trim()))
            {
                message = Constants.ValueIsEmpty(Constants.UserName);
                return null;
            }
            if (string.IsNullOrEmpty(admin.User.Password.Trim()))
            {
                message = Constants.ValueIsEmpty(Constants.Password);
                return null;
            }
            if (UserNameExists(admin.User.Username, out message))
            {
                return null;
            }
            var hasedUser = GenerateHashedUser(admin.User, out message);
            if (hasedUser == null)
            {
                return null;
            }
            admin.User = hasedUser;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        admin.User.CreateDT = DateTime.Now;
                        admin.User.Role = Constants.User_Role_Admin_Code;
                        unitOfWork.Admins.Add(admin);
                        unitOfWork.Users.Add(admin.User);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return admin;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.Admin);
                return null;
            }
        }
        /*
        *Will NOT check whether username exists
        *Username is not allowed to be changed
        */
        public bool UpdateAdmin(Admin admin, out string message)
        {
            message = string.Empty;
            if (admin == null || admin.User == null)
            {
                message = Constants.ValueIsEmpty(Constants.Admin);
                return false;
            }
            if (string.IsNullOrEmpty(admin.User.Username.Trim()))
            {
                message = Constants.ValueIsEmpty(Constants.UserName);
                return false;
            }
            if (string.IsNullOrEmpty(admin.User.Password.Trim()))
            {
                message = Constants.ValueIsEmpty(Constants.Password);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        admin.User.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(admin, unitOfWork.Admins.Get(admin.Sid));
                        var userToUpdate = unitOfWork.Users.Get(admin.User.Sid);
                        Util.CopyNonNullProperty(admin.User, userToUpdate);
                        if (userToUpdate.Password != null) userToUpdate.Password = userToUpdate.Password.Trim();
                        if (!Util.IsPasswordComplex(userToUpdate.Password))
                        {
                            message = Constants.PasswordTooSimple;
                            return false;
                        }
                        if (!string.IsNullOrEmpty(userToUpdate.Password))
                        {
                            userToUpdate.Password = Util.CreateHash(userToUpdate.Password, userToUpdate.PasswordSalt);
                        }
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Admin);
                return false;
            }
        }
        public bool DeleteAdmin(Admin admin, out string message)
        {
            if (admin == null)
            {
                message = Constants.ValueIsEmpty(Constants.Admin);
                return false;
            }
            return DeleteAdmin(admin.Sid, out message);
        }
        public bool DeleteAdmin(int adminSid, out string message)
        {
            message = string.Empty;
            if (adminSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Admin);
                return false;
            }
            var admin = GetAdminByAdminSid(adminSid, out message);
            if (admin == null || admin.User == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Users.Get(admin.UserSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.Admin);
                return false;
            }

        }
        #endregion

    }
}
