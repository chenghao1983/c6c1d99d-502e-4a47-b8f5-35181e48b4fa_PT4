using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ActiveLearning.Business.Interface
{
    public interface IContentManager
    {
        Content GetContentByContentSid(int contentSid, out string message);
        IEnumerable<Content> GetContentsByCourseSid(int courseSid, out string message);
        IEnumerable<int> GetContentSidsByCounrseSid(int courseSid, out string message);
        String GetContentPathByContentSid(int contentSid, out string message);
        Content AddContent(Controller controller, HttpPostedFileBase file, int courseSid, out string message);
        bool DeleteContent(Controller controller, Content conten, out string message);
        bool DeleteContent(Controller controller, int contentSid, out string message);
    }
}
