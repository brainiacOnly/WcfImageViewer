using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfImageViewer.Contracts.DataContracts;

namespace WcfImageVeiwer.Client.Models
{
    public class PageModel
    {
        public IEnumerable<PictureViewInfo> Pictures { get; set; }

        public string ImageBase64String { get; set; }
    }
}