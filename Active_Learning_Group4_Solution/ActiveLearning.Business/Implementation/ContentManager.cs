using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Business.Common;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace ActiveLearning.Business.Implementation
{
    public class ContentManager : BaseManager, IContentManager
    {
        public Content GetContentByContentSid(int contentSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var content = unitOfWork.Contents.Find(c => c.Sid == contentSid && !c.DeleteDT.HasValue).FirstOrDefault();
                    if (content == null)
                    {
                        message = Constants.ValueNotFound(Constants.Content);
                        return null;
                    }
                    message = string.Empty;
                    return content;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Content);
                return null;
            }
        }
        public IEnumerable<Content> GetContentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var contents = unitOfWork.Contents.Find(c => c.CourseSid == courseSid && !c.DeleteDT.HasValue);
                    if (contents == null || contents.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Content);
                        return null;
                    }
                    message = string.Empty;
                    return contents.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Content);
                return null;
            }
        }

        public IEnumerable<int> GetContentSidsByCounrseSid(int courseSid, out string message)
        {
            var contents = GetContentsByCourseSid(courseSid, out message);
            if (contents == null || contents.Count() == 0)
            {
                return null;
            }
            return contents.Select(c => c.Sid).ToList();
        }
        public Content AddContent(Controller controller, HttpPostedFileBase file, int courseSid, out string message)
        {
            message = string.Empty;
            if (controller == null)
            {
                message = Constants.ValueIsEmpty(Constants.SourceController);
                return null;
            }
            if (file == null || file.ContentLength == 0 || String.IsNullOrEmpty(file.FileName))
            {
                message = Constants.ValueIsEmpty(Constants.File);
                return null;
            }
            if (courseSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.Course);
                return null;
            }

            var fileName = file.FileName;
            var fileExtension = Path.GetExtension(file.FileName);
            var fileSize = file.ContentLength;

            if (String.IsNullOrEmpty(fileExtension))
            {
                message = Constants.UnknownValue(Constants.FileExtension);
                return null;
            }

            var allowedFileExtension = Util.GetAllowedFileExtensionFromConfig();


            if (!allowedFileExtension.Contains(fileExtension))
            {
                message = Constants.OnlyValueAllowed(allowedFileExtension.Replace(".", ""));
                return null;
            }

            var allowedFileSize = Util.GetAllowedFileSizeFromConfig();

            if (fileSize > allowedFileSize * 1024 * 1024)
            {
                message = Constants.ValueNotAllowed(Constants.FileSize);
                return null;
            }

            string GUIDFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var uploadFolder = Util.GetUploadFolderFromConfig();

            try
            {
                var uploadPath = Path.Combine(controller.Server.MapPath(uploadFolder), GUIDFileName);
                file.SaveAs(uploadPath);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringSavingValue(Constants.File);
                return null;
            }

            try
            {
                Content content = new Content();
                content.CourseSid = courseSid;
                content.CreateDT = DateTime.Now;
                content.FileName = GUIDFileName;
                content.OriginalFileName = file.FileName;
                if (Util.GetVideoFormatsFromConfig().Contains(fileExtension))
                {
                    content.Type = Constants.Content_Type_Video;
                }
                else
                {
                    content.Type = Constants.Content_Type_File;
                }
                content.Path = uploadFolder;

                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    unitOfWork.Contents.Add(content);
                    unitOfWork.Complete();
                    return content;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue(Constants.File);
                return null;
            }
        }

        public bool DeleteContent(Controller controller, Content content, out string message)
        {
            message = string.Empty;
            if (content == null || content.Sid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.File);
                return false;
            }
            return DeleteContent(controller, content.Sid, out message);
        }
        public bool DeleteContent(Controller controller, int contentSid, out string message)
        {
            message = string.Empty;
            if (contentSid == 0)
            {
                message = Constants.ValueIsEmpty(Constants.File);
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var content = unitOfWork.Contents.Get(contentSid);
                    string path = controller.Server.MapPath(content.Path + content.FileName);
                    File.Delete(path);
                    content.DeleteDT = DateTime.Now;
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringDeletingValue(Constants.File);
                return false;
            }
        }
        public string GetContentPathByContentSid(int contentSid, out string message)
        {
            message = string.Empty;
            string path = string.Empty;
            try
            {
                var content = GetContentByContentSid(contentSid, out message);
                if (content == null)
                {
                    message = Constants.ValueNotFound(Constants.Content);
                    return null;
                }
                path = content.Path + content.FileName;
                return path;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.File);
                return null;
            }


            //message = string.Empty;
            //using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            //{
            //    var file = unitOfWork.Contents.GetAll().Where(c => c.CourseSid == courseID && c.DeleteDT == null && c.OriginalFileName == originalFilename).FirstOrDefault();

            //    return file.FileName;
            //}
        }
    }
}
