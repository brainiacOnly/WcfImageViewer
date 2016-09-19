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
        [FaultContract(typeof(FileNotFoundException))]
        Stream Get(string name);

        [OperationContract]
        [FaultContract(typeof(ArgumentException))]
        void Upload(FileUploadMessage picture);
    }
}
