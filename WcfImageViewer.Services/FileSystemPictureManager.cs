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

        public FileSystemPictureManager()
        {
            _storageDirectory = ConfigurationManager.AppSettings["workingDirectory"] ?? string.Empty;
        }

        public PictureUploadInfo[] GetAll()
        {
            var files =
                Directory.GetFiles(_storageDirectory, "*.*", SearchOption.AllDirectories)
                    .Where(f => KNOWN_EXTENSIONS.Any(e => f.EndsWith(e)));

            var result = new List<PictureUploadInfo>();
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("File not found", file);
                var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                result.Add(new PictureUploadInfo
                {
                    Name = fileInfo.Name,
                    Image = stream,
                    CreationDate = fileInfo.CreationTime
                });
            }

            return result.ToArray();
        }

        public Stream Get(string name)
        {
            var fullName = Path.Combine(_storageDirectory, name);
            return new FileStream(fullName, FileMode.Open, FileAccess.Read);
        }

        public void Upload(PictureUploadInfo picture)
        {
            var fullName = Path.Combine(_storageDirectory, picture.Name);
            FileStream targetStream = null;
            using (targetStream = new FileStream(fullName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                int bufferSize = 100000;
                byte[] buffer = new byte[bufferSize];
                int count = 0;
                while ((count = picture.Image.Read(buffer, 0, count)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                picture.Image.Close();
            }
        }


        public string GetMessage()
        {
            return "Hello world";
        }
    }
}
