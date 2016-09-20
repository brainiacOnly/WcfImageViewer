using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfImageViewer.Contracts.DataContracts;

namespace WcfImageViewer.Contracts.ServiceContracts
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
    }
}
