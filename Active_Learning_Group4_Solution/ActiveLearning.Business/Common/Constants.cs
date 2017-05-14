using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Common
{
    public class Constants
    {
        #region User Role
        public const string User_Role_Student_Code = "S";
        public const string User_Role_Instructor_Code = "I";
        public const string User_Role_Admin_Code = "A";
        //public const string User_Role_Student_Name = "Student";
        //public const string User_Role_Instructor_Name = "Instructor";
        //public const string User_Role_Admin_Name = "Admin";
        #endregion

        #region Content Type
        public const string Content_Type_Video = "V";
        public const string Content_Type_File = "F";
        #endregion

        #region Message
        public const string Role = "Role";
        public const string User = "User";
        public const string Student = "Student";
        public const string Student_List = "Student List";
        public const string EnrolledStudent = "Enrolled Student";
        public const string NonEnrolledStudent = "Non Enrolled Student";
        public const string Instructor = "Instructor";
        public const string Instructor_List = "Instructor List";
        public const string EnrolledInstructor = "Enrolled Instructor";
        public const string NonEnrolledInstructor = "Non Enrolled Instructor";
        public const string Student_Course_Enrolment = "Student Course Enrolment ";
        public const string Instructor_Course_Enrolment = "Instructor Course Enrolment ";

        public const string AllRequiredFields = "All Required Fields";
        public const string Admin = "Admin";
        public const string UserName = "User Name";
        public const string Password = "Password";
        public const string FullName = "Full Name";
        public const string Qualification = "Qualification";
        public const string BatchNo = "Batch No";
        public const string Course = "Course";
        public const string CourseName = "Course Name";
        public const string EnrolledCourse = "Enrolled Course";
        public const string NonEnrolledCourse = "Non Enrolled Course";
        public const string Chat = "Chat";
        public const string Quiz = "Quiz";
        public const string QuizQuestion = "Quiz Question";
        public const string QuizOption = "Quiz Option";
        public const string QuizAnswer = "Quiz Answer";
        public const string QuizTitle = "Quiz Title";
        public const string OptionTitle = "Option Title";

        public const string SourceController = "Source Controller";
        public const string Content = "Content";
        public const string File = "File";
        public const string FileSize = "File Size";
        public const string FilePath = "File Path";
        public const string FileExtension = "File Extension";
        public const string Upload = "Upload";

        public const string Invalid_Username_Or_Password = "Invalid Username or Password";
        public const string Invalid_Password = "Invalid Password";
        public const string Authenticating_User = "Authenticating User ";
        public const string Authenticated = "Authenticated ";
        public const string PasswordTooSimple = "Password must be a combination of at least 2 digits, 2 upper case letters, 2 lower case letters and 2 symbols";

        public static string ValueNotAllowed(string value)
        {
            return value + " is not allowed";
        }
        public static string OnlyValueAllowed(string value)
        {
            return "Only " + value + " allowed";
        }
        public static string PleaseFillInAllRequiredFields()
        {
            return "Please fill in all required fields";
        }
        public static string PleaseEnterValue(string value)
        {
            return "Please enter " + value;
        }
        public static string PleaseConfirmValue(string value)
        {
            return "Please confirm " + value;
        }
        public static string UnknownValue(string value)
        {
            return "Unknown " + value;
        }
        public static string ValueIsEmpty(string value)
        {
            return value + " is empty";
        }
        public static string ValueNotFound(string value)
        {
            return value + " not found";
        }
        public static string ThereIsNoValueFound(string value)
        {
            return "There is no " + value + " found.";
        }
        public static string ValueAlreadyExists(string value)
        {
            return value + " already exists";
        }
        public static string OperationFailedDuringRetrievingValue(string value)
        {
            return "Operation failed during retrieving " + value + ". Please contact system admin";
        }
        public static string OperationFailedDuringSavingValue(string value)
        {
            return "Operation failed during saving " + value + ". Please contact system admin";
        }
        public static string OperationFailedDuringUpdatingValue(string value)
        {
            return "Operation failed during updating " + value + ". Please contact system admin";
        }
        public static string OperationFailedDuringAddingValue(string value)
        {
            return "Operation failed during adding " + value + ". Please contact system admin";
        }
        public static string OperationFailedDuringDeletingValue(string value)
        {
            return "Operation failed during deleting " + value + ". Please contact system admin";
        }
        public static string OperationFailedDuringAuthentingUserValue(string value)
        {
            return "Operation failed during authenticating user " + value + ". Please contact system admin";
        }
        public static string ValueCorrupted(string value)
        {
            return value + " data is corrupted. Please contact system admin";
        }
        public static string NOAccess(string value)
        {
            return "Sorry you don't have access to this " + value;
        }
        public static string ValueIsSuccessful(string value)
        {
            return value + " is successful";
        }
        public static string ValueSuccessfuly(string value)
        {
            return value + " successfully";
        }
        #endregion

    }
}
