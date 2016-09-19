using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfImageViewer.Contracts.DataContracts;

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

        private void SendImage(string fromPath)
        {
            using (var proxy = new ClientProxy())
            {
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
            }
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
            PictureInfo[] result;
            var imageInfo1 = new FileInfo(ConfigurationManager.AppSettings["image1"]);
            var imageInfo2 = new FileInfo(ConfigurationManager.AppSettings["image2"]);

            using (var proxy = new ClientProxy())
            {
                SendImage(imageInfo1.FullName);
                SendImage(imageInfo2.FullName);
                result = proxy.GetAll();
            }

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Any(i => i.Name == imageInfo1.Name));
            Assert.IsTrue(result.Any(i => i.Name == imageInfo2.Name));
        }

        [TestMethod]
        public void ShouldDownloadImage()
        {
            var imageInfo = new FileInfo(ConfigurationManager.AppSettings["image1"]);
            var bufferName = Path.Combine(imageInfo.Directory.FullName, "buffer." + imageInfo.Name);
            Stream result;

            using (var proxy = new ClientProxy())
            {
                SendImage(imageInfo.FullName);
                result = proxy.Get(imageInfo.FullName);
            }
            
            using (FileStream writerStream = new FileStream(bufferName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                result.CopyTo(writerStream);
                writerStream.Close();
                result.Close();
            }

            Assert.IsTrue(File.Exists(bufferName));
            File.Delete(bufferName);
        }

        [TestMethod]
        public void ShouldThrowExceptionOnNotFoundFile()
        {
            using (var proxy = new ClientProxy())
            {
                try
                {
                    var result = proxy.Get("123.jpg");
                }
                catch (FaultException<FileNotFoundException> ex)
                {
                    Console.WriteLine(ex.Detail.Message);
                }
            }
        }

        [TestMethod]
        public void ShouldThrowExceptionOnWrongName()
        {
            var imageInfo = new FileInfo(ConfigurationManager.AppSettings["image1"]);
            using (var proxy = new ClientProxy())
            {
                try
                {
                    using (FileStream stream = new FileStream(imageInfo.FullName, FileMode.Open, FileAccess.Read))
                    {
                        var request = new FileUploadMessage
                        {
                            Image = stream,
                            Name = "?:////\\\\"
                        };
                        proxy.Upload(request);
                    }
                }
                catch (FaultException<ArgumentException> ex)
                {
                    Console.WriteLine(ex.Detail.Message);
                }
            }
        }

        [TestMethod]
        public void ShouldThrowExceptionOnUnknownExtension()
        {
            var imageInfo = new FileInfo(ConfigurationManager.AppSettings["image1"]);

            using (var proxy = new ClientProxy())
            {
                try
                {
                    using (FileStream stream = new FileStream(imageInfo.FullName, FileMode.Open, FileAccess.Read))
                    {
                        var request = new FileUploadMessage
                        {
                            Image = stream,
                            Name = "unknown-extinsion.tiff"
                        };
                        proxy.Upload(request);
                    }
                }
                catch (FaultException<ArgumentException> ex)
                {
                    Console.WriteLine(ex.Detail.Message);
                }
            }
        }
    }
}
