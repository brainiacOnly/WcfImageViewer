using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfImageViewer.Contracts;
using System.Configuration;
using System.IO;

namespace WcfImageViewer.Services
{
    public class FileSystemPictureManager : IPictureManager
    {
        private string[] KNOWN_EXTENSIONS = new[] { ".jpg", ".jpeg", ".bmp", ".gif" };
        private string _storageDirectory;
        private List<PictureUploadInfo> stub = new List<PictureUploadInfo>
        {
            new PictureUploadInfo{ Image = new MemoryStream(), CreationDate = DateTime.Now.AddDays(1), Name="Tomorrow picture"},
            new PictureUploadInfo{ Image = new MemoryStream(), CreationDate = DateTime.Now.AddDays(2), Name="The day after tomorrow picture"}
        };

        public FileSystemPictureManager()
        {
            _storageDirectory = ConfigurationManager.AppSettings["workingDirectory"] ?? string.Empty;
        }

        public PictureUploadInfo[] GetAll()
        {
            return stub.ToArray();
        }

        public Stream Get(string name)
        {
            return stub.FirstOrDefault(i => i.Name == name).Image;
        }

        public void Upload(PictureUploadInfo picture)
        {
            Console.WriteLine("Begin add. Storage length is {0}", stub.Count);
            stub.Add(picture);
            Console.WriteLine("Image({0},{1},{2})", picture.Image.Length, picture.CreationDate, picture.Name);
            Console.WriteLine("Finish add. Storage length is {0}", stub.Count);
        }


        public string GetMessage()
        {
            return "Hello world";
        }
    }
}
