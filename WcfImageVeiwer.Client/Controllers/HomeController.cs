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
        public ActionResult Index()
        {
            var proxy = new PictureManagerClient();
            var pictures = proxy.GetAll();
            var model = new PageModel {Pictures = new List<PictureInfo>()};
            if (pictures.Any())
            {
                model.Pictures = pictures;
                var imageStream = proxy.Get(pictures.First().Name);
                model.ImageBase64String = Convert.ToBase64String(ReadFileStream(imageStream));
            }
            return View();
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