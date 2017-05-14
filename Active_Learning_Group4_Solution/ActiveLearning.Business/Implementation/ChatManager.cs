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

namespace ActiveLearning.Business.Implementation
{
    public class ChatManager : BaseManager, IChatManager
    {
        public Chat GetChatByChatSid(int chatSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chat = unitOfWork.Chats.Find(c => c.Sid == chatSid && !c.DeleteDT.HasValue).FirstOrDefault();

                    if (chat == null)
                    {
                        message = Constants.ValueNotFound(Constants.Chat);
                        return null;
                    }
                    message = string.Empty;
                    return chat;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Chat);
                return null;
            }
        }
        public IEnumerable<Chat> GetChatsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.CourseSid == courseSid && !c.DeleteDT.HasValue);

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.ValueNotFound(Constants.Chat);
                        return null;
                    }
                    message = string.Empty;
                    return chats.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Chat);
                return null;
            }
        }
        public IEnumerable<int> GetChatSidsByCourseSid(int courseSid, out string message)
        {
            var chats = GetChatsByCourseSid(courseSid, out message);
            if (chats == null || chats.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return chats.Select(c => c.Sid).ToList();
        }
        public IEnumerable<Chat> GetChatHistoryByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.CourseSid == courseSid && !c.DeleteDT.HasValue).OrderBy(c => c.Sid).Take(Util.GetChatHistoryCount());

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.ValueNotFound(Constants.Chat);
                        return null;
                    }
                    using (var userManager = new UserManager())
                    {
                        foreach (Chat chat in chats)
                        {
                            if (chat.StudentSid.HasValue)
                            {
                                chat.Student = userManager.GetStudentByStudentSid(chat.StudentSid.Value, out message);
                            }
                            if(chat.InstructorSid.HasValue)
                            {
                                chat.Instructor = userManager.GetInstructorByInstructorSid(chat.InstructorSid.Value, out message);
                            }
                        }
                    }

                    message = string.Empty;
                    return chats.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Chat);
                return null;
            }
        }
        public IEnumerable<Chat> GetChatsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.StudentSid == studentSid && c.CourseSid == courseSid && !c.DeleteDT.HasValue);

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.ValueNotFound(Constants.Chat);
                        return null;
                    }
                    message = string.Empty;
                    return chats.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Chat);
                return null;
            }
        }
        public IEnumerable<int> GetChatSidsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message)
        {
            var chats = GetChatsByStudentSidAndCourseSid(studentSid, courseSid, out message);
            if (chats == null || chats.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return chats.Select(c => c.Sid).ToList();
        }
        public IEnumerable<Chat> GetChatsByInstructorSidAndCourseSid(int instructorSid, int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.InstructorSid == instructorSid && c.CourseSid == courseSid && !c.DeleteDT.HasValue);

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.ValueNotFound(Constants.Chat);
                        return null;
                    }
                    message = string.Empty;
                    return chats.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Chat);
                return null;
            }
        }
        public IEnumerable<int> GetChatSidsByInstructorSidAndCourseSid(int instructorSid, int courseSid, out string message)
        {
            var chats = GetChatsByInstructorSidAndCourseSid(instructorSid, courseSid, out message);
            if (chats == null || chats.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return chats.Select(c => c.Sid).ToList();
        }
        public Chat AddStudentChatToCourse(Chat chat, int studentSid, int courseSid, out string message)
        {
            message = string.Empty;
            if (chat == null)
            {
                message = Constants.ValueIsEmpty(Constants.Chat);
                return null;
            }
            if (studentSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return null;
            }
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        chat.CreateDT = DateTime.Now;
                        chat.StudentSid = studentSid;
                        chat.CourseSid = courseSid;
                        unitOfWork.Chats.Add(chat);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return chat;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.Chat);
                return null;
            }
        }
        public Chat AddInstructorChatToCourse(Chat chat, int instructorSid, int courseSid, out string message)
        {
            message = string.Empty;
            if (chat == null)
            {
                message = Constants.ValueIsEmpty(Constants.Chat);
                return null;
            }
            if (instructorSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Student);
                return null;
            }
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        chat.CreateDT = DateTime.Now;
                        chat.InstructorSid = instructorSid;
                        chat.CourseSid = courseSid;
                        unitOfWork.Chats.Add(chat);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return chat;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.Chat);
                return null;
            }
        }
        public bool UpdateChat(Chat chat, out string message)
        {
            message = string.Empty;
            if (chat == null)
            {
                message = Constants.ValueIsEmpty(Constants.Chat);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        chat.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(chat, unitOfWork.Chats.Get(chat.Sid));
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
                message = Constants.OperationFailedDuringUpdatingValue(Constants.Chat);
                return false;
            }
        }
        public bool DeleteChat(Chat chat, out string message)
        {
            if (chat == null || chat.Sid == 0)
            {
                message = message = Constants.ValueIsEmpty(Constants.Chat);
                return false;
            }
            return DeleteChat(chat.Sid, out message);
        }
        public bool DeleteChat(int chatSid, out string message)
        {
            message = string.Empty;
            if (chatSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Chat);
                return false;
            }
            var chat = GetChatByChatSid(chatSid, out message);
            if (chat == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Chats.Get(chatSid).DeleteDT = DateTime.Now;
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
                message = Constants.OperationFailedDuringDeletingValue(Constants.Chat);
                return false;
            }
        }
        public bool SendMessageToGroup(string groupName, string messge, out string message)
        {
            throw new NotImplementedException();
        }
        public bool GetMessageByGroup(string groupName, out string message)
        {
            throw new NotImplementedException();
        }
    }
}
