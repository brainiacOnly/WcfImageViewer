using System;
using System.Runtime.Serialization;

namespace WcfImageViewer.Contracts.DataContracts
{
    [DataContract]
    public class PictureInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }
    }
}
