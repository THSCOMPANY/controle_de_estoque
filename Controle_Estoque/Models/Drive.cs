using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque
{
    //DriveInfo Class
    class Drive
    {
        //Provides access to information on a drive
        //The following code example demonstrates the use of the DriveInfo class to display information 
        //about all of the drives on the current system.
        //Fornece acesso ao drive de informacoes
        //O exemplo demonstra o uso da classe DriveInfo para mostrar as informações
        //sobre os drives do sistema atual
        //Basicamente é isso que esse procedimento "verifyDriver()" faz
        public void verifyDriver()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("Drive type: {0}", d.DriveType);

                if (d.IsReady == true)
                {
                    Console.WriteLine("Volume label:  {0}", d.VolumeLabel);
                    Console.WriteLine("File system:   {0}", d.DriveFormat);
                    Console.WriteLine(
                        "Available space to current user: {0, 15} bytes", d.AvailableFreeSpace);

                    Console.WriteLine(
                        "Total available space:       {0, 15} bytes", d.TotalFreeSpace);

                    Console.WriteLine(
                        "Total size of drive:      {0, 15} bytes", d.TotalSize);
                }
            }
        }

        //Esse cara já realiza um evento para outra demanda, nem uso ele no projeto, mas já deixei pronto
        public static void ErroEventHandler()
        {
            //ErrorEventHandler Delegate

            //Represents the method that will handle the "Error" event of a "FileSystemWatcher" object
            //public delegate void ErrorEventHandler(object sender, ErrorEventArgs e)

            //The following code example shows how to create a "FileSystemWatcher" object to monitor file
            //changes (creates, delete, renames, changes) occuring on a disk drive. The example also shows
            //how to properly receive error notifications.
            FileSystemWatcher fsw = new FileSystemWatcher("C:\\");

            //Watch for changes in LastAccess and LastWrite times, and the renaming or files or directories.
            fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Register a handler that gets called when a file is created, or deleted.
            fsw.Changed += new FileSystemEventHandler(OnChanged);

            fsw.Created += new FileSystemEventHandler(OnChanged);

            fsw.Deleted += new FileSystemEventHandler(OnChanged);

            // Register a handler that gets called when a file is renamed.
            fsw.Renamed += new RenamedEventHandler(OnRenamed);

            // Register a handler that gets called if the
            // FileSystemWatcher needs to report an error
            fsw.Error += new ErrorEventHandler(OnError);

            // Begin watching
            fsw.EnableRaisingEvents = true;

            Console.WriteLine("Press \'Enter\' to quit the sample.");
            Console.ReadLine();
        }

        // This method is called when a file is created, changed, or deleted.
        //Este metodo é chamado quando o arquivo é criado, mudando ou excluido
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Show that a file has been created, changed, or deleted.
            WatcherChangeTypes wct = e.ChangeType;
            Console.WriteLine("File {0} {1}", e.FullPath, wct.ToString());
        }

        // This method is called a file is renamed.
        //Este método é chamado quando o arquivo é renomeado
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Show that a file has been renamed.
            WatcherChangeTypes wct = e.ChangeType;
            Console.WriteLine("File {0} {2} to {1}", e.OldFullPath, e.FullPath, wct.ToString());
        }

        // This method is called when the FileSystemWatcher detects an error.
        //Este metodo é chamado quando o arquivo FileSystemWatcher detecta um erro
        private static void OnError(object source, ErrorEventArgs e)
        {
            // SHow that an error has been detected
            //por ai vai.

            //Se lembra que te falei que era importante comentar os codigo ?
            //
            Console.WriteLine("The FileSystemWatcher has detected an error");
            //Give more information if the error is due to an internal buffer overflow.
            if(e.GetException().GetType() == typeof(InternalBufferOverflowException))
            {
                //This can happen if Windows is reporting many file system events quicly
                //and internal buffer of the FileSystemWatcher is not large enough to handle this
                //rate of events. The InternalBufferOverflowException errors informs the  application
                //that some of the file system events are being lost.
                Console.WriteLine("The file system watcher experienced an internal buffer overflow: " + e.GetException().Message);
            }
        }
    }
}
