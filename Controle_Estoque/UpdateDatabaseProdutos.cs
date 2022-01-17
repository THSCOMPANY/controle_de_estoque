using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Controle_Estoque
{
    class UpdateDatabaseProdutos
    {
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        //System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";
        //Esse procedimento, atualiza o saldo da tabela Produtos, importante frisar, que ele apenas incrementa os dados
        public void UpdateSomaSaldoProdutos(String codigoProduto, String valorUnitarioNovo, String quantidadeNova)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(connectionString);
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("UPDATE PRODUTOS SET VALOR_UNITARIO='" + valorUnitarioNovo + "',QUANTIDADE='" + quantidadeNova + "'WHERE CODIGOPRODUTO='" + codigoProduto + "'", conexao);
                comando.ExecuteNonQuery();
                conexao.Close();

            }catch(MySqlException ex)
            {
                Console.WriteLine("Aconteceu uma falha ai:", ex);
            }

        }
        //Esse procedimento é antagônico a seu anterior, pois esse procedimento apenas subtrai e atualizar os dados do banco
        public void UpdateSubtrairSaldoProdutos(String codigoProduto, String quantidadeNova)
        {
                try
                {
                    MySqlConnection conexao = new MySqlConnection(connectionString);
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand("UPDATE PRODUTOS SET QUANTIDADE='" + quantidadeNova + "'WHERE CODIGOPRODUTO='" + codigoProduto + "'", conexao);
                    comando.ExecuteNonQuery();
                    conexao.Close();
                }
                
                catch(MySqlException ex)
                {
                    Console.WriteLine("Aconteceu uma falha ai:", ex);
                }
        }

        public void updateAtualizar_Soma_Fornecedor_Entrada(ref string codigoProduto, ref string valorUnitario, ref string valorFinal, ref string quantidade)
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand commando = new MySqlCommand("SELECT QUANTIDADE FROM PRODUTOS WHERE CODIGOPRODUTO='" + codigoProduto + "'", connection);
                MySqlDataReader dataReader =  commando.ExecuteReader();

                while(dataReader.Read())
                {
                    Console.WriteLine("{0}", dataReader.GetString(0));
                    //System.Windows.Forms.MessageBox.Show(dataReader.GetString(0));

                    //dataReader.GetString(0);
                }


                System.Int16 saldoEstoque = dataReader.GetInt16(0);

                System.Int16 saldoAtualizado;

                saldoAtualizado = Convert.ToInt16(saldoEstoque + Convert.ToInt16(quantidade));

                connection.Close();


                MySqlCommand command = new MySqlCommand("UPDATE PRODUTOS SET VALOR_UNITARIO='" + valorUnitario + "', VALOR_FINAL='" + valorFinal + "', QUANTIDADE='" + saldoAtualizado + "' WHERE CODIGOPRODUTO='" + codigoProduto + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();

                //fecha conexão
                connection.Close();
            }
            catch(MySqlException ex)
            {
               System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        public void updateAtualizar_Subtrair_Cliente_Saida(ref string codigoProduto, ref string valorFinal, ref string quantidade)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT QUANTIDADE FROM PRODUTOS WHERE CODIGOPRODUTO='" + codigoProduto + "'", connection);
                MySqlDataReader dataReader = command.ExecuteReader();


                while(dataReader.Read())
                {
                    Console.WriteLine("{0}", dataReader.GetString(0));
                    //System.Windows.Forms.MessageBox.Show(dataReader.GetString(0));
                }

                System.Int16 saldoEstoque = dataReader.GetInt16(0);

                System.Int16 saldoAtualizado;

                saldoAtualizado = Convert.ToInt16(saldoEstoque - Convert.ToInt16(quantidade));

                connection.Close();

                MySqlCommand comando = new MySqlCommand("UPDATE PRODUTOS SET VALOR_FINAL='" + valorFinal  + "', QUANTIDADE='" + saldoAtualizado + "' WHERE CODIGOPRODUTO='" + codigoProduto + "'", connection);
                connection.Open();
                comando.ExecuteNonQuery();

                //fecha conexão
                connection.Close();

            }
            catch(MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
    }
}
