using System;
using System.Collections.Generic;
using System.Linq;
using Renci;//FOR THE SSH
using System.Net;//FOR THE ADDRESS TRANSLATION
using System.Windows.Forms;
using System.Reflection; //FOR THE Assembly
using BarcodeLib; //

namespace Controle_Estoque
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            //Aqui é só objetos da classe System e Windows
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve02);
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve; //Carrega uma DLL por vez
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve; //Carrega várias DLL's ao mesmo tempo

            Application.Run(new Menu());
        }
        
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var MySql_Data_dll = "Controle_Estoque.DLL.MySql.Data.dll";
            var BarcodeStandard_dll = "Controle_Estoque.DLL.BarcodeStandard.dll";

            //using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Controle_Estoque.DLL.MySql.Data.dll"))
            using (var stream01 = Assembly.GetExecutingAssembly().GetManifestResourceStream(MySql_Data_dll))
            using (var stream02 = Assembly.GetExecutingAssembly().GetManifestResourceStream(BarcodeStandard_dll))
            {
                var assemblyData01 = new Byte[stream01.Length];
                stream01.Read(assemblyData01, 0, assemblyData01.Length);

                var assemblyData02 = new Byte[stream02.Length];
                stream02.Read(assemblyData02, 0, assemblyData02.Length);

                return Assembly.Load(assemblyData01, assemblyData02);
            }
        }

        
        /*static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            String Name = "Controle_Estoque";

            String DLL = new AssemblyName(args.Name).Name + ".dll";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(DLL))
            {
                Byte[] assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }

        }*/

        //Faz o upload da DLL do MySQL para conexão com o Database, Carrega apenas 1 DLL por vez
        static Assembly CurrentDomain_AssemblyResolve02(object sender, ResolveEventArgs args)
        {
            //Perdão, aqui uso variaveis, var tipo var usa tudo 
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Controle_Estoque.MySql.Data.dll"))
            {
                //Tipo byte, porém é um vetor de bytes, tu vai aprender sobre bytes mais pra frente
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }

        }

        //Faz o upload da DLL do Barcode Usado para criação do código de barras
        public static Assembly CurrentDomain_AssemblyResolve03(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Controle_Estoque.DLL.BarcodeStandard.dll"))
            {
                //Carrega a .dll no projeto
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }
}
