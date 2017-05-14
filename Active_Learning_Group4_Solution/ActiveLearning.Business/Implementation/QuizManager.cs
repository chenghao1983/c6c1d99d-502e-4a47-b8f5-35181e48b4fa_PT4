using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveLearning.Business.Common;
using System.Data.Entity;
using ActiveLearning.Business.ViewModel;
using ActiveLearning.Business.SignalRHub;
using Microsoft.AspNet.SignalR;
using System.Transactions;

namespace ActiveLearning.Business.Implementation
{
    public class QuizManager : BaseManager, IQuizManager
    {
        #region Normal
        public bool QuizQuestionTitleExists(string quizTitle, out string message)
        {
            if (string.IsNullOrEmpty(quizTitle))
            {
                message = Constants.ValueIsEmpty(Constants.QuizTitle);
                return true;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizQuestion = unitOfWork.QuizQuestions.Find(q => q.Title.Equals(quizTitle, StringComparison.CurrentCultureIgnoreCase) && !q.DeleteDT.HasValue).FirstOrDefault();
                    if (quizQuestion != null)
                    {
                        message = Constants.ValueAlreadyExists(quizTitle);
                        return true;
                    }
                }
                message = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizTitle);
                return true;
            }
        }
        public QuizQuestion GetQuizQuestionByQuizQuestionSid(int quizQuestionSid, out string message)
        {
            if (quizQuestionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizQuestion = unitOfWork.QuizQuestions.Find(q => q.Sid == quizQuestionSid && !q.DeleteDT.HasValue).FirstOrDefault();
                    if (quizQuestion == null)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.QuizQuestion);
                        return null;
                    }
                    else
                    {
                        message = string.Empty;
                        return quizQuestion;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizQuestion);
                return null;
            }
        }
        public IEnumerable<QuizQuestion> GetActiveQuizQuestionsByCourseSid(int courseSid, out string message)
        {
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizQuestions = unitOfWork.QuizQuestions.Find(q => q.CourseSid == courseSid && !q.DeleteDT.HasValue);
                    if (quizQuestions == null || quizQuestions.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.QuizQuestion);
                        return null;
                    }
                    message = string.Empty;
                    return quizQuestions.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizQuestion);
                return null;
            }
        }
        public IEnumerable<int> GetActiveQuizQuestionSidsByCourseSid(int courseSid, out string message)
        {
            var quizQuestions = GetActiveQuizQuestionsByCourseSid(courseSid, out message);
            if (quizQuestions == null || quizQuestions.Count() == 0)
            {
                return null;
            }
            return quizQuestions.Select(q => q.Sid).ToList();
        }

        public QuizQuestion AddQuizQuestionToCourse(QuizQuestion quizQuestion, int courseSid, out string message)
        {
            if (quizQuestion == null)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return null;
            }
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return null;
            }
            if(string.IsNullOrEmpty(quizQuestion.Title)|| string.IsNullOrEmpty(quizQuestion.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.QuizTitle);
                return null;
            }
            if (QuizQuestionTitleExists(quizQuestion.Title, out message))
            {
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        quizQuestion.CreateDT = DateTime.Now;
                        quizQuestion.CourseSid = courseSid;
                        unitOfWork.QuizQuestions.Add(quizQuestion);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return quizQuestion;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.QuizQuestion);
                return null;
            }
        }
        public bool UpdateQuizQuestion(QuizQuestion quizQuestion, out string message)
        {
            if (quizQuestion == null || quizQuestion.Sid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return false;
            }
            if (string.IsNullOrEmpty(quizQuestion.Title) || string.IsNullOrEmpty(quizQuestion.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.QuizTitle);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizQuestionToUpdate = unitOfWork.QuizQuestions.Get(quizQuestion.Sid);
                    Util.CopyNonNullProperty(quizQuestion, quizQuestionToUpdate);
                    quizQuestionToUpdate.UpdateDT = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
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
                message = Constants.OperationFailedDuringUpdatingValue(Constants.QuizQuestion);
                return false;
            }
        }
        public bool DeleteQuizQuestion(QuizQuestion quizQuestion, out string message)
        {
            if (quizQuestion == null)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return false;
            }
            return DeleteQuizQuestion(quizQuestion.Sid, out message);
        }
        public bool DeleteQuizQuestion(int quizQuestionSid, out string message)
        {
            if (quizQuestionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    unitOfWork.QuizQuestions.Get(quizQuestionSid).DeleteDT = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
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
                message = Constants.OperationFailedDuringDeletingValue(Constants.QuizQuestion);
                return false;
            }
        }
        public QuizOption GetQuizOptionByQuizOptionSid(int quizOptionSid, out string message)
        {
            if (quizOptionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizOption);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizOption = unitOfWork.QuizOptions.Find(o => o.Sid == quizOptionSid && !o.DeleteDT.HasValue).FirstOrDefault();
                    if (quizOption == null)
                    {
                        message = Constants.ValueNotFound(Constants.QuizOption);
                        return null;
                    }
                    message = string.Empty;
                    return quizOption;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizOption);
                return null;
            }
        }
        public IEnumerable<QuizOption> GetQuizOptionsByQuizQuestionSid(int quizQuestionSid, out string message)
        {
            if (quizQuestionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizOptions = unitOfWork.QuizOptions.Find(o => o.QuizQuestionSid == quizQuestionSid && !o.DeleteDT.HasValue);
                    if (quizOptions == null || quizOptions.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.QuizOption);
                        return null;
                    }
                    message = string.Empty;
                    return quizOptions.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizOption);
                return null;
            }
        }
        public IEnumerable<int> GetQuizOptionSidsByQuizQuestionSid(int quizQuestionSid, out string message)
        {
            var quizOptions = GetQuizOptionsByQuizQuestionSid(quizQuestionSid, out message);
            if (quizOptions == null)
            {
                return null;
            }
            return quizOptions.Select(o => o.Sid).ToList();
        }
        public QuizOption AddQuizOptionToQuizQuestion(QuizOption quizOption, int quizQuestionSid, out string message)
        {
            if (quizOption == null)
            {
                message = Constants.ValueIsEmpty(Constants.QuizOption);
                return null;
            }
            if (quizQuestionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return null;
            }
            if (string.IsNullOrEmpty(quizOption.Title) || string.IsNullOrEmpty(quizOption.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.OptionTitle);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        quizOption.CreateDT = DateTime.Now;
                        quizOption.QuizQuestionSid = quizQuestionSid;
                        unitOfWork.QuizOptions.Add(quizOption);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return quizOption;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.QuizOption);
                return null;
            }

        }
        public bool UpdateQuizOption(QuizOption quizOption, out string message)
        {
            if (quizOption == null || quizOption.Sid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizOption);
                return false;
            }
            if (string.IsNullOrEmpty(quizOption.Title) || string.IsNullOrEmpty(quizOption.Title.Trim()))
            {
                message = Constants.PleaseFillInAllRequiredFields();
                //message = Constants.PleaseEnterValue(Constants.OptionTitle);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizOptionToUpdate = unitOfWork.QuizOptions.GetAll().Where(a=> a.Sid == quizOption.Sid).FirstOrDefault();
                    Util.CopyNonNullProperty(quizOption, quizOptionToUpdate);
                    quizOptionToUpdate.UpdateDT = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
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
                message = Constants.OperationFailedDuringUpdatingValue(Constants.QuizOption);
                return false;
            }
        }
        public bool DeleteQuizOption(QuizOption quizOption, out string message)
        {
            if (quizOption == null)
            {
                message = Constants.ValueIsEmpty(Constants.QuizOption);
                return false;
            }
            return DeleteQuizOption(quizOption.Sid, out message);
        }
        public bool DeleteQuizOption(int quizOptionSid, out string message)
        {
            if (quizOptionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizOption);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizOptions = unitOfWork.QuizOptions.Find(o => o.Sid == quizOptionSid).ToList();

                    if (quizOptions == null)
                    {
                        message = Constants.ValueNotFound(Constants.QuizOption);
                        return false;
                    }
                    foreach (var option in quizOptions)
                    {
                        option.DeleteDT = DateTime.Now;
                    }
                    //unitOfWork.QuizOptions.Get(quizOptionSid).DeleteDT = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
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
                message = Constants.OperationFailedDuringDeletingValue(Constants.QuizOption);
                return false;
            }
        }

        public QuizAnswer GetQuizAnswerByQuizAnswerSid(int quizAnswerSid, out string message)
        {
            if (quizAnswerSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizAnswer);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizAnswer = unitOfWork.QuizAnswers.Find(a => a.Sid == quizAnswerSid && !a.DeleteDT.HasValue).FirstOrDefault();
                    if (quizAnswer == null)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.QuizAnswer);
                        return null;
                    }
                    else
                    {
                        message = string.Empty;
                        return quizAnswer;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizAnswer);
                return null;
            }
        }
        public IEnumerable<QuizAnswer> GetQuizAnswersByQuizQuestionSid(int quizQuestionSid, out string message)
        {
            if (quizQuestionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quizAnswers = unitOfWork.QuizAnswers.Find(a => a.QuizQuestionSid == quizQuestionSid && !a.DeleteDT.HasValue);
                    if (quizAnswers == null || quizAnswers.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.QuizAnswer);
                        return null;
                    }
                    message = string.Empty;
                    return quizAnswers.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.QuizAnswer);
                return null;
            }
        }
        public IEnumerable<int> GetQuizAnswerSidsByQuizQuestionSid(int quizQuestionSid, out string message)
        {
            var quizAnswers = GetQuizAnswersByQuizQuestionSid(quizQuestionSid, out message);
            if (quizAnswers == null || quizAnswers.Count() == 0)
            {
                return null;
            }
            return quizAnswers.Select(a => a.Sid).ToList();
        }
        public QuizAnswer AddQuizAnswerToQuizQuestionAndOption(QuizAnswer quizAnswer, int quizQuestionSid, int quizOptionSid, int studentSid, out string message)
        {
            if (quizAnswer == null)
            {
                message = Constants.ValueIsEmpty(Constants.QuizAnswer);
                return null;
            }
            if (quizQuestionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizQuestion);
                return null;
            }
            if (quizOptionSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizOption);
                return null;
            }
            if (studentSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        quizAnswer.CreateDT = DateTime.Now;
                        quizAnswer.QuizQuestionSid = quizQuestionSid;
                        quizAnswer.QuizOptionSid = quizOptionSid;
                        quizAnswer.StudentSid = studentSid;
                        unitOfWork.QuizAnswers.Add(quizAnswer);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return quizAnswer;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.QuizAnswer);
                return null;
            }
        }
        public bool UpdateQuizAnswer(QuizAnswer quizAnswer, out string message)
        {
            if (quizAnswer == null || quizAnswer.Sid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizAnswer);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var quitAnswerToUpdate = unitOfWork.QuizAnswers.Get(quizAnswer.Sid);
                    Util.CopyNonNullProperty(quizAnswer, quitAnswerToUpdate);
                    quitAnswerToUpdate.UpdateDT = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
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
                message = Constants.OperationFailedDuringUpdatingValue(Constants.QuizAnswer);
                return false;
            }
        }
        public bool DeleteQuizAnswer(QuizAnswer quizAnswer, out string message)
        {
            if (quizAnswer == null)
            {
                message = Constants.ValueIsEmpty(Constants.QuizAnswer);
                return false;
            }
            return DeleteQuizAnswer(quizAnswer.Sid, out message);
        }
        public bool DeleteQuizAnswer(int quizAnswerSid, out string message)
        {
            if (quizAnswerSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.QuizAnswer);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    unitOfWork.QuizAnswers.Get(quizAnswerSid).DeleteDT = DateTime.Now;

                    using (TransactionScope scope = new TransactionScope())
                    {
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
                message = Constants.OperationFailedDuringDeletingValue(Constants.QuizAnswer);
                return false;
            }
        }
        #endregion

        #region Async
        public async Task<QuizQuestion> NextQuestionAsync(int studentSid, int CourseSid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    //var lastAnswer = unitOfWork.QuizAnswers.Find(a => a.StudentSid == studentSid).LastOrDefault();

                    //var lastQuestionId = lastQuestion.GroupBy(a => a.QuizQuestionSid).Select(g => new { QuestionId = g.Key, Count = g.Count() }).
                    // OrderByDescending(q => new { q.Count, QuestionId = q.QuestionId }).Select(q => q.QuestionId).FirstOrDefault();

                    //var lastQuestionId = lastQuestion.FirstOrDefault().Sid;

                    var question = unitOfWork.QuizQuestions.Find(x => x.CourseSid == CourseSid);
                    var questionsCount = question.Count();

                    //var questionsCount = await db.QuizQuestions.Where(x => x.CourseSid == CourseSid).CountAsync();

                    if (questionsCount == 0)
                    {
                        return null;
                    }
                    //var nextQuestionId = (lastQuestionId % questionsCount) + 1;
                    List<int> questionSids = question.Select(q => q.Sid).ToList();

                    int ram = new Random().Next(questionsCount);
                    var nextQuestionId = questionSids.ElementAt(ram);

                    var nextQuestion = unitOfWork.QuizQuestions.Get(nextQuestionId);
                    nextQuestion.QuizOptions = await unitOfWork.QuizOptions.FindAsync(o => o.QuizQuestionSid == nextQuestion.Sid && !o.DeleteDT.HasValue) as ICollection<QuizOption>;

                    if (nextQuestion.QuizOptions != null)
                    {
                        foreach (var option in nextQuestion.QuizOptions)
                        {
                            option.CourseSid = CourseSid;
                        }
                    }

                    return nextQuestion;
                    //return await db.QuizQuestions.Include(e => e.QuizOptions).FirstOrDefaultAsync(c => c.Sid == nextQuestionId);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return null;
            }
            //var lastQuestionId = await db.QuizAnswers
            //    .Where(a => a.StudentSid == studentSid)
            //    .GroupBy(a => a.QuizQuestionSid)
            //    .Select(g => new { QuestionId = g.Key, Count = g.Count() })
            //    .OrderByDescending(q => new { q.Count, QuestionId = q.QuestionId })
            //    .Select(q => q.QuestionId)
            //    .FirstOrDefaultAsync();

            //try
            //{
            //    var questionsCount = await db.QuizQuestions.Where(x => x.CourseSid == CourseSid).CountAsync();

            //    if (questionsCount == 0)
            //    {
            //        return null;
            //    }
            //    var nextQuestionId = (lastQuestionId % questionsCount) + 1;

            //    //var p = db.QuizQuestions.Include(e => e.QuizOptions).FirstOrDefaultAsync(c => c.Sid == nextQuestionId);


            //    return await db.QuizQuestions.Include(e => e.QuizOptions).FirstOrDefaultAsync(c => c.Sid == nextQuestionId);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLog(ex);
            //    return null;

            //}
        }

        public async Task<bool> StoreAsync(QuizAnswer answer)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    answer.CreateDT = DateTime.Now;
                    unitOfWork.QuizAnswers.Add(answer);
                    await unitOfWork.CompleteAsync();

                    var selectedOption = await unitOfWork.QuizOptions.SingleOrDefaultAsync(o => o.Sid == answer.QuizOptionSid && o.QuizQuestionSid == answer.QuizQuestionSid && !o.DeleteDT.HasValue);
                    return selectedOption.IsCorrect;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return false;
            }

            //this.db.QuizAnswers.Add(answer);

            //await this.db.SaveChangesAsync();

            //var selectedOption = await this.db.QuizOptions.FirstOrDefaultAsync(o => o.Sid == answer.QuizOptionSid
            // && o.QuizQuestionSid == answer.QuizQuestionSid);

        }
        public async Task NotifyUpdates(int courseSid)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StatisticsHub>();
            if (hubContext != null)
            {
                var stats = await this.GenerateStatistics(courseSid);
                hubContext.Clients.All.updateStatistics(stats);
            }
        }
        public async Task<StatisticsViewModel> GenerateStatistics(int courseSid)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var correctAnswerCount = (await unitOfWork.QuizAnswers.FindAsync(a => a.QuizOption.IsCorrect && a.QuizOption.QuizQuestion.CourseSid == courseSid && !a.DeleteDT.HasValue)).Count();
                    var totalAnswerCount = (await unitOfWork.QuizAnswers.FindAsync(a => a.QuizOption.QuizQuestion.CourseSid == courseSid && !a.DeleteDT.HasValue)).Count();
                    var totalUserCount = (await unitOfWork.QuizAnswers.FindAsync(a => a.QuizOption.QuizQuestion.CourseSid == courseSid && !a.DeleteDT.HasValue)).Select(s => s.StudentSid).Count();
                    var incorrectAnswers = totalAnswerCount - correctAnswerCount;

                    return new StatisticsViewModel
                    {
                        CorrectAnswers = correctAnswerCount,
                        IncorrectAnswers = incorrectAnswers,
                        TotalAnswers = totalAnswerCount,
                        CorrectAnswersAverage = (totalUserCount > 0) ? correctAnswerCount / totalUserCount : 0,
                        IncorrectAnswersAverage = (totalUserCount > 0) ? incorrectAnswers / totalUserCount : 0,
                        TotalAnswersAverage = (totalUserCount > 0) ? totalAnswerCount / totalUserCount : 0,
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                return null;
            }


            //var correctAnswerCount = await db.QuizAnswers.CountAsync(a => a.QuizOption.IsCorrect);
            // var totalAnswerCount = await db.QuizAnswers.CountAsync();
            //var totalUserCount = (float)await db.QuizAnswers.GroupBy(a => a.StudentSid).CountAsync();

            //var incorrectAnswers = totalAnswerCount - correctAnswerCount;

            //return new StatisticsViewModel
            //{
            //    CorrectAnswers = correctAnswerCount,
            //    IncorrectAnswers = incorrectAnswers,
            //    TotalAnswers = totalAnswerCount,
            //    CorrectAnswersAverage = (totalUserCount > 0) ? correctAnswerCount / totalUserCount : 0,
            //    IncorrectAnswersAverage = (totalUserCount > 0) ? incorrectAnswers / totalUserCount : 0,
            //    TotalAnswersAverage = (totalUserCount > 0) ? totalAnswerCount / totalUserCount : 0,
            //};
        }
        #endregion
    }
}
