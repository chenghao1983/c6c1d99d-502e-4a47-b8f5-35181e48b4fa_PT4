using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using ActiveLearning.Business.ViewModel;

namespace ActiveLearning.Business.Interface
{
    public interface IQuizManager
    {
        bool QuizQuestionTitleExists(string quizTitle, out string message);
        QuizQuestion GetQuizQuestionByQuizQuestionSid(int quizQuestionSid, out string message);
        IEnumerable<QuizQuestion> GetActiveQuizQuestionsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetActiveQuizQuestionSidsByCourseSid(int courseSid, out string message);
        //IEnumerable<QuizQuestion> GetDeletedQuizQuestionsByCourseSid(int courseSid, out string message);
        //IEnumerable<int> GetDeletedQuizQuestionSidsByCourseSid(int courseSid, out string message);
        QuizQuestion AddQuizQuestionToCourse(QuizQuestion quizQuestion, int courseSid, out string message);
        bool UpdateQuizQuestion(QuizQuestion quizQuestion, out string message);
        bool DeleteQuizQuestion(QuizQuestion quizQuestion, out string message);
        bool DeleteQuizQuestion(int quizQuestionSid, out string message);
        QuizOption GetQuizOptionByQuizOptionSid(int quizOptionSid, out string message);
        IEnumerable<QuizOption> GetQuizOptionsByQuizQuestionSid(int quizQuestionSid, out string message);
        IEnumerable<int> GetQuizOptionSidsByQuizQuestionSid(int quizQuestionSid, out string message);
        QuizOption AddQuizOptionToQuizQuestion(QuizOption quizOption, int quizQuestionSid, out string message);
        bool UpdateQuizOption(QuizOption quizOption, out string message);
        bool DeleteQuizOption(QuizOption quizOption, out string message);
        bool DeleteQuizOption(int quizOptionSid, out string message);
        QuizAnswer GetQuizAnswerByQuizAnswerSid(int quizAnswerSid, out string message);
        IEnumerable<QuizAnswer> GetQuizAnswersByQuizQuestionSid(int quizQuestionSid, out string message);
        IEnumerable<int> GetQuizAnswerSidsByQuizQuestionSid(int quizQuestionSid, out string message);
        QuizAnswer AddQuizAnswerToQuizQuestionAndOption(QuizAnswer quizAnswer, int quizQuestionSid, int quizOptionSid, int studentSid, out string message);
        bool UpdateQuizAnswer(QuizAnswer quizAnswer, out string message);
        bool DeleteQuizAnswer(QuizAnswer quizAnswer, out string message);
        bool DeleteQuizAnswer(int quizAnswerSid, out string message);
        //IEnumerable<QuizQuestion> GetActiveQuizQuestionQuizOptionQuizAnswerByStudentSid(int studentSid, out string message);
        Task<QuizQuestion> NextQuestionAsync(int userId, int CourseSid);
        Task<bool> StoreAsync(QuizAnswer answer);
        Task NotifyUpdates(int courseSid);
        Task<StatisticsViewModel> GenerateStatistics(int courseSid);
    }
}
