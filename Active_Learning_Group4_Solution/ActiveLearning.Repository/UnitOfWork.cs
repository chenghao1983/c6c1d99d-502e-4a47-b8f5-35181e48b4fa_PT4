using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Interface.Core;
using ActiveLearning.Repository.Context;
using ActiveLearning.Repository.Repository.Core;
using ActiveLearning.Repository.Repository;
using ActiveLearning.DB;


namespace ActiveLearning.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ActiveLearningContext _context;

        public UnitOfWork(ActiveLearningContext context)
        {
            _context = context;

            Admins = new AdminRepository(_context);
            Chats = new ChatRepository(_context);
            Contents = new ContentRepository(_context);
            Courses = new CourseRepository(_context);
            Instructor_Course_Maps = new Instructor_Course_MapRepository(_context);
            Instructors = new InstructorRepository(_context);
            QuizAnswers = new QuizAnswerRepository(_context);
            QuizOptions = new QuizOptionRepository(_context);
            QuizQuestions = new QuizQuestionRepository(_context);
            Student_Course_Maps = new Student_Course_MapRepository(_context);
            Students = new StudentRepository(_context);
            Users = new UserRepository(_context);
        }
        public IAdminRepository Admins { get; private set; }
        public IChatRepository Chats { get; private set; }
        public IContentRepository Contents { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IInstructor_Course_MapRepository Instructor_Course_Maps { get; private set; }
        public IInstructorRepository Instructors { get; private set; }
        public IQuizAnswerRepository QuizAnswers { get; private set; }
        public IQuizOptionRepository QuizOptions { get; private set; }
        public IQuizQuestionRepository QuizQuestions { get; private set; }
        public IStudent_Course_MapRepository Student_Course_Maps { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IUserRepository Users { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public ActiveLearningContext ActiveLearningContext
        {
            get { return _context as ActiveLearningContext; }
        }
    }
}
