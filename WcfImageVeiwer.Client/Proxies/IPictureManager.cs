using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfImageViewer.Contracts.DataContracts;

namespace WcfImageVeiwer.Client.Proxies
{
    [ServiceContract]
    public interface IPictureManager
    {
        [OperationContract]
        PictureInfo[] GetAll();

        [OperationContract]
        Stream Get(string name);

        [OperationContract]
        void Upload(FileUploadMessage picture); 

        [OperationContract]
        Task<PictureInfo[]> GetAllAsync();

        [OperationContract]
        Task<Stream> GetAsync(string name);

        [OperationContract]
        Task UploadAsync(FileUploadMessage picture);
    }
}
