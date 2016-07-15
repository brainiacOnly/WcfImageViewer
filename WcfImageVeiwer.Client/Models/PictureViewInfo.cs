using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfImageVeiwer.Client.Models
{
    public class PictureViewInfo
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public bool IsActive { get; set; }
    }
}