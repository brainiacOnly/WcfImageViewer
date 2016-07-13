using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;

namespace WcfImageViewer.Contracts
{
    [MessageContract]
    public class PictureUploadInfo : IDisposable
    {
        [MessageHeader]
        public string Name { get; set; }

        [MessageHeader]
        public DateTime CreationDate { get; set; }

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
