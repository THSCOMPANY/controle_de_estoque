using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms; // To use MessageBox
using System.IO;

//public static System.IO.DirectoryInfo CreateDirectory(string path);

namespace Controle_Estoque
{
    //Essa classe Diretorio, ele é totalmente automata, o algoritmo trabalha de forma automatica, não precisa deu mexer nele, deixei ele parametrizado,
    //melhor, preparado para possiveis falhas, e ele corrigir de forma automatica, automata ou melhor "INDEPENDENTE DE MIM".
    class Diretorio_Files
    {
        Arquivo arq = new Arquivo();

        //specify the directory you want to manipulate and create
        static string pathDirectory    = @"C:\Controle_Estoque"; //Path Global
        static string pathSubDirectory = @"C:\Controle_Estoque\Cadastro";

        //Create all the directories in a specified path
        public void CreateDirectory()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=net-5.0

            try
            {
                //determine whether the directory exists
                if (Directory.Exists(pathDirectory))
                {
                    Console.WriteLine("The Path exists already. ");
                    return;
                }

                //Try to create the directory
                DirectoryInfo directory = Directory.CreateDirectory(pathDirectory); //Create Directory
                
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(pathDirectory));
                MessageBox.Show("Diretório criado com Êxito!");
                getCreationTime(); //Obtem a hora de criação do Diretório

                //deleteDirectory(path);
                //Delete the directory
                //directory.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }
            finally { }
            
            //arq.AppendTextFile();
        }

        //Caso eu queira excluir o diretório
        public void deleteDirectory(string pathDirectory)
        {
            //capture the directory for delete
            DirectoryInfo directory = Directory.CreateDirectory(pathDirectory);

            //delete directory
            directory.Delete();
            Console.WriteLine("The directory was deleted successfully.");
            MessageBox.Show("Directory deleted with Success!");
        }

        private static void verifyDirectory()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.exists?view=net-5.0
            if (File.Exists(pathDirectory))
            {
                //This path is a file
                ProcessFile(pathDirectory);
            }
            else if(Directory.Exists(pathDirectory))
            {
                //This path is a directory
                ProcessDirectory(pathDirectory);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", pathDirectory);
            }
        }
        //Process all files in the directory passed in, recurse on any directories
        //that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory)
        {
            //Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            //Recurse into subdirectories of this directory
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", pathDirectory);
        }

        //Get Hour creation Directory
        public void getCreationTime()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getcreationtime?view=net-5.0

            try
            {
                //Get the creation time of a well-know directory
                DateTime dt = Directory.GetCreationTime(Environment.CurrentDirectory);

                //Give feedback to the user
                if(DateTime.Now.Subtract(dt).TotalDays > 365)
                {
                    Console.WriteLine("This directory is over a year old.");
                }
                else if(DateTime.Now.Subtract(dt).TotalDays > 30)
                {
                    Console.WriteLine("This directory is over a month old.");
                }
                else if(DateTime.Now.Subtract(dt).TotalDays <= 1)
                {
                    Console.WriteLine("This directory was created on {0}", dt);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public static void CreatesubDirectory()
        {
            //Create a reference to a directory
            DirectoryInfo di = new DirectoryInfo("TempDir");

            //Create the directory only if it does not already exist.
            if (di.Exists == false)
                di.Create();

            //Create a subdirectory in the directory just created.
            DirectoryInfo dis = di.CreateSubdirectory(pathSubDirectory);

            //Process that directory as required
            //....

            //Delete the subdirectory
            dis.Delete(true);

            //Delete the directory
            di.Delete(true);
        }

        //DirectoryNotFoundException Class
        //The exception that is thrown when part of a file or directory cannot be found.
        public class DirectoryNotFoundException : System.IO.IOException
        {
            // The following example shows how to force and recover from a DirectoryNotFoundException
            public static void TestePath()
            {
                try
                {
                    //Specify a directory name that does not exist for this demo
                    string dir = @"c:\78fe9lk";

                    // If this directory does not exist, a DirectoryNotFoundException is thrown 
                    // When attempting to set the current directory
                    Directory.SetCurrentDirectory(dir);
                }
                catch (DirectoryNotFoundException dirMessage)
                {
                    //Let the user know that the directory did not exist.
                    Console.WriteLine("Directory not found: " + dirMessage);
                }
            }
        }

    }
}
