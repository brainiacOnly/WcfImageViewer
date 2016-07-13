using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfImageVeiwer.Client.Proxies;
using WcfImageViewer.Contracts;

namespace WcfImageViewer.Services.Tests
{
    [TestClass]
    public class ProxyTests
    {
        private ServiceHost _host = null;
        private string _directory;

        [TestInitialize]
        public void Initialize()
        {
            _host = new ServiceHost(typeof(FileSystemPictureManager));
            _host.Open();
            _directory = ConfigurationManager.AppSettings["workingDirectory"];
            Directory.CreateDirectory(_directory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _host.Close();
            Directory.Delete(_directory, true);
        }

        [TestMethod]
        public void ShouldReturnHelloWorld()
        {
            var proxy = new PictureManagerClient();

            var result = proxy.GetMessage();
            proxy.Close();

            Assert.AreEqual(result, "Hello world");
        }

        private void SendImage(string fromPath)
        {
            var proxy = new PictureManagerClient();
            var fileInfo = new FileInfo(fromPath);
            using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                var request = new FileUploadMessage
                {
                    Image = stream,
                    Name = fileInfo.Name
                };
                proxy.Upload(request);
            }
            proxy.Close();
        }

        [TestMethod]
        public void ShouldSaveImage()
        {
            var imageInfo = new FileInfo(ConfigurationManager.AppSettings["image1"]);

            SendImage(imageInfo.FullName);

            Assert.IsTrue(File.Exists(Path.Combine(_directory, imageInfo.Name)));
        }

        [TestMethod]
        public void ShouldDonloadAllImages()
        {
            var proxy = new PictureManagerClient();
            var imageInfo1 = new FileInfo(ConfigurationManager.AppSettings["image1"]);
            var imageInfo2 = new FileInfo(ConfigurationManager.AppSettings["image2"]);
            SendImage(imageInfo1.FullName);
            SendImage(imageInfo2.FullName);
            
            var result = proxy.GetAll();
            proxy.Close();

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Any(i => i.Name == imageInfo1.Name));
            Assert.IsTrue(result.Any(i => i.Name == imageInfo2.Name));
        }

        [TestMethod]
        public void ShouldDownloadImage()
        {
            var proxy = new PictureManagerClient();
            var imageInfo = new FileInfo(ConfigurationManager.AppSettings["image1"]);
            SendImage(imageInfo.FullName);
            var bufferName = Path.Combine(imageInfo.Directory.FullName, "buffer." + imageInfo.Name);

            var result = proxy.Get(imageInfo.FullName);
            using (FileStream writerStream = new FileStream(bufferName, FileMode.Create, FileAccess.Write, FileShare.None))
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

            Assert.IsTrue(File.Exists(bufferName));
            File.Delete(bufferName);
        }
    }
}
