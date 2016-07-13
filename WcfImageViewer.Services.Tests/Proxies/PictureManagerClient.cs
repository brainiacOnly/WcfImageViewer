using System.IO;
using System.ServiceModel;
using WcfImageViewer.Contracts;

namespace WcfImageViewer.Services.Tests.Proxies
{
    public class PictureManagerClient : ClientBase<IPictureManager>, IPictureManager
    {
        public PictureInfo[] GetAll()
        {
            return Channel.GetAll();
        }

        public Stream Get(string name)
        {
            return Channel.Get(name);
        }

        public void Upload(FileUploadMessage picture)
        {
            Channel.Upload(picture);
        }

        public string GetMessage()
        {
            return Channel.GetMessage();
        }
    }
}
