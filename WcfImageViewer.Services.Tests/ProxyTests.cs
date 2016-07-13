using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfImageViewer.Contracts;
using WcfImageViewer.Services.Tests.Proxies;
using WcfImageViewer.Services.Tests.Proxies;

namespace WcfImageViewer.Services.Tests
{
    [TestClass]
    public class ProxyTests
    {
        [TestMethod]
        public void ShouldReturnHelloWorld()
        {
            var proxy = new PictureManagerClient();

            var result = proxy.GetMessage();
            proxy.Close();

            Assert.AreEqual(result, "Hello world");
        }

        [TestMethod]
        public void ShouldSaveImage()
        {
            var proxy = new PictureManagerClient();
            var fileInfo = new FileInfo(@"..\..\Files\wcf.png");
            using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                var request = new PictureUploadInfo
                {
                    Image = stream,
                    Name = fileInfo.Name
                };
                proxy.Upload(request);
            }
            proxy.Close();

            Assert.IsTrue(File.Exists(@"C:\Projects\Images\wcf.png"));
        }

        [TestMethod]
        public void ShouldDonloadAllImages()
        {
            var proxy = new PictureManagerClient();

            var result = proxy.GetAll();
            proxy.Close();

            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(429804, result.First(i => i.Name == "1.jpg").Image.Length);
        }

        [TestMethod]
        public void ShouldDownloadImage()
        {
            var proxy = new PictureManagerClient();
            var name = "1.jpg";

            var result = proxy.Get(name);
            using (FileStream writerStream = new FileStream(name, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int bufferSize = 100000;
                byte[] buffer = new byte[bufferSize];
                int count = 0;
                while ((count = result.Read(buffer, 0, bufferSize)) > 0)
                {
                    writerStream.Write(buffer, 0, count);
                }
                writerStream.Close();
                result.Close();
            }

            Assert.IsTrue(File.Exists(name));
            File.Delete(name);
        }
    }
}
