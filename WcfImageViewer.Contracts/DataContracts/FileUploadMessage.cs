using System;
using System.IO;
using System.ServiceModel;

namespace WcfImageViewer.Contracts.DataContracts
{
    [MessageContract]
    public class FileUploadMessage : IDisposable
    {
        [MessageHeader]
        public string Name { get; set; }

        [MessageBodyMember]
        public Stream Image { get; set; }

        public void Dispose()
        {
            if(Image != null)
            {
                Image.Close();
                Image = null;
            }
        }
    }
}
