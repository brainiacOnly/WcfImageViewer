using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using WcfImageVeiwer.Client.Models;
using WcfImageVeiwer.Client.Proxies;
using WcfImageViewer.Contracts.DataContracts;

namespace WcfImageVeiwer.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id = null)
        {
            var proxy = new PictureManager();
            var pictures = proxy.GetAll();
            var model = new PageModel();

            if (pictures.Any())
            {
                model.Pictures = pictures.Select(i => new PictureViewInfo
                {
                    DisplayName = i.Name,
                    Id = i.Name.GetHashCode().ToString()
                }).ToArray();

                PictureViewInfo targetPicture = null;
                if (id != null)
                {
                    targetPicture = model.Pictures.FirstOrDefault(i => i.DisplayName.GetHashCode().ToString() == id);
                    if (targetPicture == null)
                        throw new AggregateException("The image was not found");
                }
                else
                {
                    targetPicture = model.Pictures.First();
                }

                targetPicture.IsActive = true;
                var imageStream = proxy.Get(targetPicture.DisplayName);
                model.UrlName = Convert.ToBase64String(ReadFileStream(imageStream));
            }
            proxy.Close();
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null)
            {
                try
                {
                    var proxy = new PictureManager();
                    var uploadMessage = new FileUploadMessage
                    {
                        Name = uploadFile.FileName,
                        Image = uploadFile.InputStream
                    };
                    proxy.Upload(uploadMessage);
                    proxy.Close();
                }
                catch (FaultException<ArgumentException> ex)
                {
                    ViewData["Message"] = ex.Detail.Message;
                    ViewData["StackTrace"] = ex.Detail.StackTrace;
                    return View("Error");
                }
            }
            return RedirectToAction("Index");
        }

        private byte[] ReadFileStream(Stream source)
        {
            MemoryStream targetStream = new MemoryStream();
            source.CopyTo(targetStream);
            targetStream.Close();
            source.Close();

            return targetStream.ToArray();
        }
        
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var result = new ViewResult()
            {
                ViewName = "Error"
            };
            result.ViewData["Message"] = ex.Message;
            result.ViewData["StackTrace"] = ex.StackTrace;

            filterContext.Result = result;
        }
    }
}