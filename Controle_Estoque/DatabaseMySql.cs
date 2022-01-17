using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Controle_Estoque.Models
{

    //Add Icon in .exe'
    //https://www.badprog.com/c-visual-studio-adding-a-custom-ico-file-to-an-application
    //http://adamthetech.com/2011/06/embed-dll-files-within-an-exe-c-sharp-winforms/

    class DatabaseMySql
    {
        public static Cadastrar cadastrar = new Cadastrar();
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;"; //Minha maquina
        //Máquina do Éder
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;Port=3306;pwd=Michaeldias03092009;";

        //Create connection with Database
        public void connectionDatabase()
        {
            //Só para testar a conexão
            //https://stackoverflow.com/questions/41388626/how-to-connect-mysql-database-to-c-sharp-winform-application Connection to Database

            //Parametros do Banco
            String server = "127.0.0.1";
            String username = "root";
            String password = "";
            String port = "3306";
            String database = "controle_estoque";

            //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

            try
            {

                MySqlConnection connection = new MySqlConnection(connectionString);
                //sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database;
                //sqlConn.ConnectionString = "Server=" + server + ";" + "Username=" + username + ";" + "Database=" + database;
                connection.Open();
                //MessageBox.Show("Conexão Realizada Com Êxito!");
                //sqlCmd.Connection = sqlConn;
                //sqlConn.Close();
                //closeConnection();
                //System.Windows.Forms.MessageBox.Show("MySQL Version:" + connection.ServerVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open Connection!!" + ex);
            }

        }

        //Insert values in Database MySQL, passando o objeto TextBox via parametro
        public void insertDatas(ListView listView1, TextBox txtCodProduto, TextBox txtDescProduto, TextBox txtMarca, TextBox txtAno, TextBox txtValorUnitario, TextBox txtValorFinal, TextBox txtClassificacao, TextBox txtQuantidade)
        {
            //DatabaseMySqlWorkbench database = new DatabaseMySqlWorkbench();
            //database.EstabilishConnection();
            string codProduto, descProduto, marca, ano, valorUnitario, valorFinal, classificacao, quantidade;
            //recebe os dados, para passar para a classe, onde será montada a mensagem para "persistencia no Arquivo"
            codProduto = txtCodProduto.Text;
            descProduto = txtDescProduto.Text;
            marca = txtMarca.Text;
            ano = txtAno.Text;
            valorUnitario = txtValorUnitario.Text;
            valorFinal = txtValorFinal.Text;
            classificacao = txtClassificacao.Text;
            quantidade = txtQuantidade.Text;


            Produtos produtos = new Produtos(codProduto, descProduto, marca, ano, valorUnitario, valorFinal, classificacao, quantidade);
            //txtValorFinal.Text    = Convert.ToInt32(txtValorFinal.Text).ToString();
            //txtValorUnitario.Text = Convert.ToInt32(txtValorUnitario.Text).ToString();

            //System.String ValorUnitario = String.Format("{0:C8}", txtValorUnitario.Text);
            //System.String ValorFinal    = String.Format("{0:C}", txtValorFinal.Text);

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.Parameters.AddWithValue("@CODIGOPRODUTO", txtCodProduto.Text);
            command.Parameters.AddWithValue("@PRODUTO", txtDescProduto.Text);
            command.Parameters.AddWithValue("@MARCA", txtMarca.Text);
            command.Parameters.AddWithValue("@ANO", txtAno.Text);
            command.Parameters.AddWithValue("@VALOR_UNITARIO", txtValorUnitario.Text);
            command.Parameters.AddWithValue("@VALOR_FINAL", txtValorFinal.Text);
            command.Parameters.AddWithValue("@CLASSIFICACAO", txtClassificacao.Text);
            command.Parameters.AddWithValue("@QUANTIDADE", txtQuantidade.Text);

            command.CommandText = "INSERT INTO PRODUTOS (CODIGOPRODUTO, PRODUTO, MARCA, ANO, VALOR_UNITARIO, VALOR_FINAL, CLASSIFICACAO, QUANTIDADE) VALUES (@CODIGOPRODUTO, @PRODUTO, @MARCA, @ANO, @VALOR_UNITARIO, @VALOR_FINAL, @CLASSIFICACAO, @QUANTIDADE)";

            if (command.ExecuteNonQuery() > 0)

                MessageBox.Show("Cadastro Efetuado com Êxito!!");

            else
                MessageBox.Show("Registro não pode ser adicionado, aconteceu algum falha no Processo!");

            //sqlConn.Close();
            //closeConnection();

            //Limpa os campos
            txtCodProduto.Text = "";
            txtDescProduto.Text = "";
            txtMarca.Text = "";
            txtValorUnitario.Text = "";
            txtValorFinal.Text = "";
            txtClassificacao.Text = "";
            txtQuantidade.Text = "";
            txtAno.Text = "";

            connection.Close();

        }
    }
}
