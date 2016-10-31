using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WcfImageViewer.Contracts.DataContracts;
using WcfImageViewer.Contracts.ServiceContracts;

namespace WcfImageViewer.Services.Tests
{
    class ClientProxy : ClientBase<IPictureManager>, IPictureManager
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
    }
}
