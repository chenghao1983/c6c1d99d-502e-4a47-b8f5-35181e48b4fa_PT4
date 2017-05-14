using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
namespace ActiveLearning.Business.Interface
{
    public interface IChatManager : IDisposable
    {
        Chat GetChatByChatSid(int chatSid, out string message);
        IEnumerable<Chat> GetChatsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetChatSidsByCourseSid(int courseSid, out string message);
        IEnumerable<Chat> GetChatHistoryByCourseSid(int courseSid, out string message);
        IEnumerable<Chat> GetChatsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message);
        IEnumerable<int> GetChatSidsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message);
        IEnumerable<Chat> GetChatsByInstructorSidAndCourseSid(int instructorSid, int courseSid, out string message);
        IEnumerable<int> GetChatSidsByInstructorSidAndCourseSid(int instructorSid, int courseSid, out string message);
        Chat AddStudentChatToCourse(Chat chat, int studentSid, int courseSid, out string message);
        Chat AddInstructorChatToCourse(Chat chat, int instructorSid, int courseSid, out string message);
        bool UpdateChat(Chat chat, out string message);
        bool DeleteChat(Chat chat, out string message);
        bool DeleteChat(int chatSid, out string message);
        bool SendMessageToGroup(string groupName, string messge, out string message);
        bool GetMessageByGroup(string groupName, out string message);
    }
}
