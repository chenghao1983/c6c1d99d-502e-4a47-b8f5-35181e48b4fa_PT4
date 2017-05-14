using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.Interface.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository Admins { get; }
        IChatRepository Chats { get; }
        IContentRepository Contents { get; }
        ICourseRepository Courses { get; }
        IInstructor_Course_MapRepository Instructor_Course_Maps { get; }
        IInstructorRepository Instructors { get; }
        IQuizAnswerRepository QuizAnswers { get; }
        IQuizOptionRepository QuizOptions { get; }
        IQuizQuestionRepository QuizQuestions { get; }
        IStudent_Course_MapRepository Student_Course_Maps { get; }
        IStudentRepository Students { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
