using System.IO;
using System.ServiceModel;
using WcfImageViewer.Contracts;

namespace WcfImageViewer.Services.Tests.Proxies
{
    public class PictureManagerClient : ClientBase<IPictureManager>, IPictureManager
    {
        public PictureUploadInfo[] GetAll()
        {
            return Channel.GetAll();
        }

        public Stream Get(string name)
        {
            return Channel.Get(name);
        }

        public void Upload(PictureUploadInfo picture)
        {
            Channel.Upload(picture);
        }

        public string GetMessage()
        {
            return Channel.GetMessage();
        }
    }
}
