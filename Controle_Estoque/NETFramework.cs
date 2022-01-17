using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque
{
    class NETFramework
    {
        //Definindo Strings Textuais

        //Quando voce prefixa uma string literal com o símbolo "@", cria o que se chama de string textual. Utilizando
        //strings textuais, voce desativa o processamento dos caracteres de escape de uma literal e imprime a string
        //como ela é. Iso pode ser mais útil ao trabalhar com strings representando caminhos de diretório e rede.
        //Portanto, em vez de usar os caracteres de escape \\, é possível apenas escrever o seguinte:

        //A string a seguir é impressa textualmente
        //portanto, todos os caracteres de escape são exibidos
        public static void Exemplo()
        {
            Console.WriteLine(@"C:\MyApp\bin\Debug");

            //Note também que as strings textuais podem ser usadas para preservar espaços em branco para as strings
            //que seguem em diversas linhas:

            //O espaço em branco é preservado com string textuais
            //string myLongString = @"This is a very very very long string";

            //Usando string textuais, voce tambem pode inserir diretamente aspas duplas em uma string literal dobrando
            //o indentificador
            Console.WriteLine(@"Cerebrus said"" Darr! pretyyy """);
        }

        public static void System_Text_StringBuilder()
        {
            /*
                Como o tipo "string" pode ser ineficiente quando utilizado de maneira displicente, as bibliotecas de classe
                de base .NET oferecem o namespace System.Text. Dentro desse namespace (relativamente pequeno) está uma classe
                chamada StringBuilder define métodos que permitem que voce substitua ou formate segmentos, por exemplo. Quando
                voce quiser utilizar esse tipo em seus arquivos de código C#, o primeiro passo será importar o seguinte namespace
                para seu arquivo de código
                //StringBuilder está aqui
                using System.Text;

                O que é único com relação á classe StringBuilder é que, quando voce chama os membros desse tipo, modifica diretamente
                os dados de caractere internos do objeto (e o deixa mais eficiente), sem obter uma cópia dos dados em formato modificado
                Ao criar uma instância de construtores. Caso voce seja novo com relação aos construtores, compreenda apenas que eles 
                permitem criar um objeto com um estado inicial ao aplicar a palavra-chave "new". Considere a seguinte utilização
                de StringBuilder

             */
            Console.WriteLine("==> Using the StringBuilder:");
            StringBuilder sb = new StringBuilder("**** Fantastic Games ****");
            sb.Append("\n");
            sb.AppendLine("Half Life");
            sb.AppendLine("Beyond Good and Evil");
            sb.AppendLine("Deus Ex 2");
            sb.AppendLine("System Shock");
            Console.WriteLine(sb.ToString());

            sb.Replace("2", "Invisible War");
            Console.WriteLine(sb.ToString());
            Console.WriteLine("sb as {0} chars.", sb.Length);
            Console.WriteLine();

            /*
                Aqui nós construímos um StringBuilder definido para o valor inicial "**** Fantastic Games ****"
                Como voce pode ver, estamos anexando ao buffer interno e podemos substituir ou remover caracteres
                á vontade. Por padrão, um StringBuilder só é capaz de manter inicialmente uma string de 16 caracteres
                ou menos (mas expandirá automaticamente se necessário). Entretanto, esse valor inicial padrão pode ser
                modificado através de um argumento do construtor adicional.
             */
            // Crie um StringBuilder com um tamanho inicial de 256.
            StringBuilder sb02 = new StringBuilder("***** Fantastic Games *****", 256);
            //Se voce anexar mais caracteres do que o limite especificado, o objeto StringBuilder copiará esses dados
            //em uma nova instância e aumentará o buffer com o limite especificado
        }

        //Modificadores de parametros em C#
        //(Nenhum)  Se um parametro não estiver marcado por um modificador de parâmetro, sopõe-se que ele seja transmitido
        //          pelo valor significando que o método chamado recebe uma cópia do dados originais
        
        //out       Os parâmetros de saída dever ser atribuídos pelo método que está sendo chamado e, portanto, são transmitidos
        //          pela referência. Se o método chamado não conseguir atribuir os parâmetros de saída, você receberá um erro do
        //          compilador.

        //Modificador out
        /*  A seguir, veremos a utilização dos parâmetros de saída. Os métodos que foram definidos para aceitar os parâmetros de
            saída (através da palavra-chave out) são obrigados a atribui-los a um valor apropriado antes de sair do escopo do método
            (se não fizer isso, receberá erros do compilador). Para ilustrar, segue uma versão alternativa do método Add(), que
            retorna a soma de dois inteiros utilizando o modificador C# out (note que o valor de retorno físico desse método agora
            é void)*/

        //Os parâmetros de saída devem ser atribuidos pelo método chamado
        static void Add(int x, int y, out int ans)
        {
            ans = x + y;
        }
    }
}
