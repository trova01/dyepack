using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher Detector = new FileSystemWatcher(@"C:\Users\dam\Desktop\test");
            Detector.NotifyFilter = (
                NotifyFilters.LastAccess |
                NotifyFilters.FileName |
                NotifyFilters.LastWrite |
                NotifyFilters.Security |
                NotifyFilters.DirectoryName
                );
            Detector.Changed += activador;
            Detector.Deleted += activador;
            Detector.Renamed += activador;
            

            Detector.EnableRaisingEvents = true;
            Console.WriteLine("presione <Enter> para detener el monitor");
            Console.ReadLine(); 
        }

        private static void activador(object source, FileSystemEventArgs e)
        {
            //documenta el cambio realizado
            WatcherChangeTypes log = e.ChangeType;
            string linea = "[" + DateTime.Now + "] El documento " + e.Name + " sufrió una alteración de tipo " + log.ToString() + ".";
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\dam\Desktop\log.txt",true);
           
            file.WriteLine(linea);
            file.Close();

            //flag
            Console.WriteLine("[{0}] - El archivo {1} sufrió una alteración de tipo {2}.", DateTime.Now, e.FullPath, log.ToString());

           // ejecuta Dyepack
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = true;
            info.FileName = "hidden-tear.exe";
            info.WorkingDirectory = @"C:\Users\dam\Desktop\test";

            Process.Start(info);
        }


    }
}
