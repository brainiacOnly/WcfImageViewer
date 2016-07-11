using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfImageViewer.Services.Tests.PictureClient;

namespace WcfImageViewer.Services.Tests
{
    [TestClass]
    public class ProxyTests
    {
        [TestMethod]
        public void StubTests()
        {
            var proxy = new PictureManagerClient();
            
            proxy.Open();
            var allImages = proxy.GetAll();
            var singleImage = proxy.Get("Tomorrow picture");
            proxy.Upload(new Picture { Name = "Added", CreationDate = new DateTime(1994, 08, 19), Image = new byte[21] });
            proxy.Close();

            Assert.AreEqual(2, allImages.Length);
            Assert.IsNotNull(singleImage);
            Assert.AreEqual(10, singleImage.Length);
        }
    }
}
