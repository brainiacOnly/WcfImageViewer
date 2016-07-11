using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WcfImageViewer.Services;

namespace WcfImageViewer.Services.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(FileSystemPictureManager));
            host.Open();

            Console.WriteLine("Service started. Press [Enter] to exit.");
            Console.ReadLine();

            host.Close();
        }
    }
}
