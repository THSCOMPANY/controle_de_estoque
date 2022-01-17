using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque
{
    class Sistema
    {
        public void VerificarSistema()
        {
            //Classe "Environment"
            //Propriedades          Função

            //ExitCode              Obtém ou define o código de saída do aplicativo
            //MachineName           Obtém o nome da máquina atual
            //NewLine               Ontém um simbolo de nova linha para o ambiente atual
            //StackTrace            Obtém as informações atuais do rastreamento da pilha do aplicativo.
            //SystemDirectory       Retorna o caminho completo para o diretório do sistema.
            //UserName              Retorna o nome do usuário que inicio este aplicativo.

            Arquivo arquivo = new Arquivo();
            //Imprima os drivers nesta máquina
            //e os outros detalhes interessantes.
            foreach (string drive in Environment.GetLogicalDrives())
            {
                string OSVersion     =  Environment.OSVersion.ToString();
                string processors    =  Environment.ProcessorCount.ToString();
                string nomeMaquina   =  Environment.MachineName.ToString();
                string rastreamento  =  Environment.StackTrace.ToString();
                string fullDirectory =  Environment.SystemDirectory.ToString();
                string nomeUsuario   =  Environment.UserName.ToString();

                arquivo.verificarSistema(drive, OSVersion, processors, nomeMaquina, rastreamento, fullDirectory, nomeUsuario);
            }
        }
    }
}
