using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using WcfImageViewer.Contracts.DataContracts;
using WcfImageViewer.Contracts.ServiceContracts;

namespace WcfImageViewer.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class FileSystemPictureManager : IPictureManager
    {
        private string[] KNOWN_EXTENSIONS = { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
        private string _storageDirectory;

        public FileSystemPictureManager()
        {
            _storageDirectory = ConfigurationManager.AppSettings["workingDirectory"] ?? string.Empty;
            if (!Directory.Exists(_storageDirectory))
            {
                Directory.CreateDirectory(_storageDirectory);
            }
        }

        public PictureInfo[] GetAll()
        {
            var files =
                Directory.GetFiles(_storageDirectory, "*.*", SearchOption.AllDirectories)
                    .Where(f => KNOWN_EXTENSIONS.Any(e => f.ToLower().EndsWith(e)));

            var result = new List<PictureInfo>();
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("File not found", file);
                result.Add(new PictureInfo
                {
                    Name = fileInfo.Name,
                    CreationDate = fileInfo.CreationTime
                });
            }

            return result.ToArray();
        }

        public Stream Get(string name)
        {
            var fullName = Path.Combine(_storageDirectory, name);
            FileStream result = null;
            try
            {
                result = new FileStream(fullName, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException ex)
            {
                throw new FaultException<FileNotFoundException>(ex);
            }
            return result;
        }

        public void Upload(FileUploadMessage picture)
        {
            var fullName = Path.Combine(_storageDirectory, picture.Name);
            FileStream targetStream = null;

            try
            {
                using (targetStream = new FileStream(fullName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    int bufferSize = 100000;
                    byte[] buffer = new byte[bufferSize];
                    int count = 0;
                    while ((count = picture.Image.Read(buffer, 0, bufferSize)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                    }
                    targetStream.Close();
                    picture.Image.Close();
                }
            }
            catch (ArgumentException ex)
            {                
                throw new FaultException<ArgumentException>(ex);
            }
        }
    }
}
