using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var proxy = new PictureManagerClient();
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
                    targetPicture = model.Pictures.First(i => i.DisplayName.GetHashCode().ToString() == id);
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
                var proxy = new PictureManagerClient();
                var uploadMessage = new FileUploadMessage
                {
                    Name = uploadFile.FileName,
                    Image = uploadFile.InputStream
                };
                proxy.Upload(uploadMessage);
                proxy.Close();
            }
            return RedirectToAction("Index");
        }

        private byte[] ReadFileStream(Stream source)
        {
            int bufferLen = 100000;
            byte[] buffer = new byte[bufferLen];
            MemoryStream targetStream = null;

            using (targetStream = new MemoryStream())
            {
                int count = 0;
                while ((count = source.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                source.Close();
            }

            return targetStream.ToArray();
        }
    }
}