using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WcfImageViewer.Contracts
{
    [ServiceContract]
    public interface IPictureManager
    {
        [OperationContract]
        PictureUploadInfo[] GetAll();

        [OperationContract]
        Stream Get(string name);

        [OperationContract]
        void Upload(PictureUploadInfo picture);

        [OperationContract]
        string GetMessage();
    }
}
