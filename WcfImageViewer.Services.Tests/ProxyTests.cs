using System;
using System.IO;
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
        public void StubTests()
        {
            var proxy = new PictureManagerClient();

            /*var allImages = proxy.GetAll();
            var singleImage = proxy.Get("Tomorrow picture");
            proxy.Upload(new PictureUploadInfo { Name = "Added", CreationDate = new DateTime(1994, 08, 19), Image = new MemoryStream()});
            proxy.Close();

            Assert.AreEqual(2, allImages.Length);
            Assert.IsNotNull(singleImage);
            Assert.IsInstanceOfType(singleImage, typeof(MemoryStream));*/

            var result = proxy.GetMessage();
            Assert.AreEqual(result, "Hello world");
        }
    }
}
