using System;
using System.Collections.Generic;
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
        byte[] Get(string name);

        [OperationContract]
        void Upload(PictureUploadInfo picture);
    }
}
