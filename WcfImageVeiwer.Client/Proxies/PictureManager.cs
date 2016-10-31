using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfImageViewer.Contracts;
using WcfImageViewer.Contracts.DataContracts;
using WcfImageViewer.Contracts.ServiceContracts;

namespace WcfImageVeiwer.Client.Proxies
{
    public class PictureManager : ClientBase<IPictureManager>, IPictureManager
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

        public Task<PictureInfo[]> GetAllAsync()
        {
            return Channel.GetAllAsync();
        }

        public Task<Stream> GetAsync(string name)
        {
            return Channel.GetAsync(name);
        }

        public Task UploadAsync(FileUploadMessage picture)
        {
            return Channel.UploadAsync(picture);
        }
    }
}