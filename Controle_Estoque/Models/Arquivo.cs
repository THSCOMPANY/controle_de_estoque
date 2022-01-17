using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace Controle_Estoque
{
    sealed class Arquivo //palavra reservada "sealed" impede que outra classe Herde da mesma
    {
        //File.AppendAllLines Method

        //Appends lines to a file, and then closes the file

        //AppendsAllLines(String, IEnumerable<String>)
        //Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates
        //a file, writes the specified lines to the file, and then closes the file.
        //public static void AppendAllLines(string path, System.Collections.Generic.IEnumerable<string> contents)

        // The following example writes selected lines from a sample data file to a file, and then appends more lines.
        // The directory named temp on drive C must exist for the example to complete successfuly.
        
        static string dataPath = @"c:\temp\timestamp.txt";

        public void AppendAllLines()
        {
            CreateSampleFile();

            var JulyWeekends = from line in File.ReadLines(dataPath)
                               where (line.StartsWith("Saturday") ||
                               line.StartsWith("Sunday")) &
                               line.Contains("July")
                               select line;

            File.WriteAllLines(@"C:\temp\selectedDays.txt", JulyWeekends);

            var MarchMondays = from line in File.ReadLines(dataPath)
                               where line.StartsWith("Monday") &&
                               line.Contains("March")
                               select line;
            File.AppendAllLines(@"C:\tem\selectedDays.txt", MarchMondays);
        }

        static void CreateSampleFile()
        {
            DateTime TimeStamp = new DateTime(1700, 1, 1);

            using (StreamWriter sw = new StreamWriter(dataPath))
            {
                for(int i = 0; i < 500; i++)
                {
                    DateTime TS1 = TimeStamp.AddYears(i);
                    DateTime TS2 = TS1.AddMonths(i);
                    DateTime TS3 = TS2.AddDays(i);
                    sw.WriteLine(TS3.ToLongDateString());
                }
            }
            // The method creates the file if it doesn't exist, but it doesn't create new directories. Therefore, the
            // value of the "path" parameter must contain existing directories.
        }
        //**************************************************************************************************

        // File.AppendText(String) Method
        // Creates a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified
        // file does not exist

        // The following example appends text to a file. The method creates a new file if the file doesn't exist.
        // However, the directory named "temp" on drive C must exist for the example to complete successfuly.
        public void AppendTextFile(string codigoProduto, string produto, string marca, string ano, string valor_Unitario, string valor_Final, string classificacao, string quantidade)
        {
            
            string path = @"C:\Controle_Estoque\CadastroEstoque.txt";
            
            // This text is added only once to the file
            if (!File.Exists(path))
            {
                // Create a file to write to
                using (StreamWriter sw = File.CreateText(path))
                { 
                    //Só vai cair aqui, caso não exista diretório, caso seja true, ele cria o diretorio e o arquivo
                    sw.WriteLine(" BACKUP DO BANCO DE DADOS DO CONTROLE DE ESTOQUE ");
                }
            }

            // This text is always added, making the file longer over time if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                //sw.WriteLine("Cod. Produto       |       Produto        |       Marca       |    Ano   |      Valor Unitário     |      Valor Final      |      Classificação      |     Quantidade ");
                sw.WriteLine("Codigo do Produto: " + codigoProduto + " | Produto: " + produto + " | Marca: " + marca + " | Ano: " + ano + " | Valor Unitario: R$:" + valor_Unitario + " | Valor Final: R$:" + valor_Final + " | Classificação: "  + classificacao + " | Quantidade: "  + quantidade + " |");
                //sw.WriteLine(produto);
            }

            //Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public void verificarSistema(string drive, string OSVersion, string processors, string nomeMaquina, string rastreamento, string fullDirectory, string nomeUsuario)
        {
            string path = @"C:\Controle_Estoque\Sistema.txt";

            // This text is added only once to the file
            if (!File.Exists(path))
            {
                // Create a file to write to
                using (StreamWriter sw = File.CreateText(path))
                {
                    //Só vai cair aqui, caso não exista diretório, caso seja true, ele cria o diretorio e o arquivo
                    sw.WriteLine(" INFORMACOES DO SISTEMA ");

                    // This text is always added, making the file longer over time if it is not deleted.
                    sw.WriteLine("Drive:: " + drive + " | OS:: " + OSVersion + " | Processadores:: " + processors + " | Máquina:: " + nomeMaquina + " | Diretorio Full::" + fullDirectory + " | Nome Usuário:: " + nomeUsuario);
                }
            }
        }
        //**************************************************************************************
        //File.Copy Method

        //Copies an existing file to a new file

        // Example
        // The following example copies files to the C:\archives\2008 folder. It user the two overloads of the Copy method as follows:

        // It first uses the File.Copy(String, String) method overload to copy text (.text) files. The code demonstrates that this 
        // does not allow overwriting files that were  already copied.

        // It then uses the File.Copy(String, String, Boolean) method overload to copy pictures (.jpg files). The code demonstrates
        // that this overload does allow overwriting files that were already copied.
        public void Copy()
        {
            string sourceDir = @"c:\current";
            string backupDir = @"c\archives\2008";

            try
            {
                string[] picList = Directory.GetFiles(sourceDir, "*.jpg");
                string[] txtList = Directory.GetFiles(sourceDir, "*.txt");

                //Copy picture files
                foreach (string f in picList)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    // Use the Path. Combine method to safely append the file name to the path.
                    // Will overwrite if the destination file already exists.
                    File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                }

                // Copy text files
                foreach(string f in txtList)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    try
                    {
                        // Will not overwrite if the destination file already exists.
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
                    }

                    // Catch exception if the file was already copied
                    catch(IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                }

                // Delete source files that were copied
                foreach(string f in txtList)
                {
                    File.Delete(f);
                }
                foreach(string f in picList)
                {
                    File.Delete(f);
                }
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
            // Remarks

            // This method is equivalent to the Copy(String, String, Boolean) method overload with the overwrite parameter
            // set to "false". The "sourceFileName and destFileName" parameters can specify relative or absolute path information
            // Relative path information is interpreted as relative to the current working directory. To obtain the current working
            // directory, see the Directory.GetCurrentDirectory method. This method does not support wildcard characters in the parameters.
        }

        public static void Create()
        {
            //Creates or overwrites a file in the specified path.
            string path = @"c:\temp\MyTest.txt";

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");

                    // Add some information to the file
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Remarks
            // The FileStream object created by this method has a default FileShare value of None; no other process or code can access the created file until the original file handle is closed.
            // This method is equivalent to the Create(String, Int32) method overload using the default buffer size of 4,096 bytes.
            // The path parameter is permitted to specify relative or absolute path information. Relative path information is interpreted as relative to the current working directory. To obtain the current working directory, see GetCurrentDirectory.
            // If the specified file does not exist, it is created; if it does exist and it is not read-only, the contents are overwritten.
            // By default, full read/write access to new files is granted to all users. The file is opened with read/write access and must be closed before it can be opened by another application.
            //  For a list of common I/O tasks, see Common I/O Tasks.
        }

        public static void FileDecrypt()
        {
            // The following code example uses the Encrypt method and the Decrypt method to encrypt and then
            // decrypt a file. The file must exist for the example to work.
            try
            {
                string fileName = "test.xml";

                Console.WriteLine("Encrypt" + fileName);

                // Encrypt the file.
                AddEncryption(fileName);

                Console.WriteLine("Decrypt " + fileName);

                // Decrypt the file.
                RemoveEncryption(fileName);

                Console.WriteLine("Done");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        // Encrypt a file
        public static void AddEncryption(string fileName)
        {
            File.Encrypt(fileName);
        }

        // Decrypt a file.
        public static void RemoveEncryption(string FileName)
        {
            File.Decrypt(FileName);
        }

        // Remarks
        // The Decrypt method allows you to decrypt a file that was encrypted using the Encrypt method.
        // The Decrypt method can decrypt only files that were encrypted using the current user account.
        // The Decrypt method requires exclusive access to the file being decrypted, and will raise an
        // exception if another process is using the file. If the file is not encrypted, Decrypt will return a
        // nonzero value, which indicates success.
        // Both the Encrypt method and the Decrypt method use the cryptographic service provider (CSP)
        // installed on the computer and the file encryption keys of the process calling the method.
        // The current file system must be formatted as NTFS and the current operating system must be
        //Windows NT or later.
        //*********************************************************************************************************

        public static void FileGetAttributes()
        {
            // Gets the FileAttributes of the file on the path

            // The following example demonstrates the "GetAttributes" and "SetAttributes" methods by applying
            // the "Archive" and "Hidden" attributes to a file.
            string path = @"c:\temp\MyTest.txt";

            // Create the file if it does not exist.
            if(!File.Exists(path))
            {
                File.Create(path);
            }

            FileAttributes attributes = File.GetAttributes(path);

            if((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                // Show the file
                attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                File.SetAttributes(path, attributes);
                Console.WriteLine("The {0} file is no longer hidden.", path);
            }
            else
            {
                // Hide the file
                File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
                Console.WriteLine("The {0} file is now hidden.", path);
            }
        }
        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
            //The path parameter is permitted to specify relative or absolute path information. Relative path
            //information is interpreted as relative to the current working directory. To obtain the current working
            //directory, see GetCurrentDirectory.
            //For a list of common I/O tasks, see Common I/O Tasks.
        }
        //************************************************************************************************************
        public static void FileOpenRead()
        {
            // Opens an existing file for reading

            string path = @"C:\Controle_Estoque\CadastroEstoque.txt";

            if(!File.Exists(path))
            {
                // Create the file
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("This is some in the file");

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            // Open the stream and read it back.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while(fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
                //This method is equivalent to the FileStream(String, FileMode, FileAccess, FileShare) constructor
                //overload with a FileMode value of Open, a FileAccess value of Read and a FileShare value of Read.
                //The path parameter is permitted to specify relative or absolute path information. Relative path
                //information is interpreted as relative to the current working directory. To obtain the current working
                //directory, see GetCurrentDirectory.
                //For a list of common I/O tasks, see Common I/O Tasks.
            }
        }
  
        public static void FileOpenText()
        {
            //Opens an existing UTF-8 encoded text file for reading.
            string path = @"C:\temp\MyTest.txt";

            if(!File.Exists(path))
            {
                // Create the file
                using (FileStream fs = File.Create(path))
                {
                    byte[] info =
                        new UTF8Encoding(true).GetBytes("This is some text in the file");

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            //This method is equivalent to the StreamReader(String) constructor overload.
            //The path parameter is permitted to specify relative or absolute path information. Relative path
            //information is interpreted as relative to the current working directory. To obtain the current working
            //directory, see GetCurrentDirectory.
        }

        //*********************************************************************************************************
        public static void FileOpenWrite()
        {
            // Opens an existing file or creates a new file for writing.

            string path = @"c:\temp\MyTest.txt";

            // Open the stream and write to it.
            using (FileStream fs = File.OpenWrite(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("This is to test the OpenWrite method.");

                // Add some information to the file
                fs.Write(info, 0, info.Length);
            }

            // Open the stream and read it back.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while(fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
            //This method is equivalent to the FileStream(String, FileMode, FileAccess, FileShare) constructor
            //overload with file mode set to OpenOrCreate, the access set to Write, and the share mode set to None.
            //The OpenWrite method opens a file if one already exists for the file path, or creates a new file if one
            //does not exist. For an existing file, it does not append the new text to the existing text. Instead, it
            //overwrites the existing characters with the new characters. If you overwrite a longer string (such as
            //"This is a test of the OpenWrite method") with a shorter string (such as "Second run"), the file will
            //contain a mix of the strings ("Second runtest of the OpenWrite method").
            //The path parameter may specify relative or absolute path information. Relative path information is
            //interpreted as relative to the current working directory. To obtain the current working directory, use
            //the GetCurrentDirectory method.
            //The returned FileStream does not support reading. To open a file for both reading and writing, use Open.
        }

        public static void FileReadAllLines()
        {
            // Opens a text file, reads all lines of the file into a string array, and then closes the file.

            // The following code example demonstrates the use of the "ReadAllLines" method to display the contents
            // of a file. In this example a file is created, if it doesn't already exist, and text is added to it.
            string path = @"c:\temp\MyTest.txt";

            // This text is added only once the file.
            if(!File.Exists(path))
            {
                // Creates a file to write to.
                string[] createText = { "Hello", "And", "Welcome" };
                File.WriteAllLines(path, createText);
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            string appendText = "This is extra text" + Environment.NewLine;
            File.AppendAllText(path, appendText);

            // Open the file to read from.
            string[] readText = File.ReadAllLines(path);
            foreach(string s in readText)
            {
                Console.WriteLine(s);
            }
            //This method opens a file, reads each line of the file, then adds each line as an element of a string
            //array. It then closes the file. A line is defined as a sequence of characters followed by a carriage
            //return ('\r'), a line feed ('\n'), or a carriage return immediately followed by a line feed. The
            //resulting string does not contain the terminating carriage return and/or line feed.
            //This method attempts to automatically detect the encoding of a file based on the presence of
            //byte order marks. Encoding formats UTF-8 and UTF-32 (both big-endian and little-endian) can
            //be detected.
        }
    }
}
