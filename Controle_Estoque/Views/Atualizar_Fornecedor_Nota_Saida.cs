using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Controle_Estoque
{
    public partial class Atualizar_Fornecedor_Nota_Saida : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        //Máquina Éder
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;Port=3306;pwd=Michaeldias03092009;";

        public Atualizar_Fornecedor_Nota_Saida()
        {
            InitializeComponent();
            label1.Visible              = false;
            txtIdCliente.ReadOnly       = true;
            txtIdCliente.Visible        = false;
            txtIdItensEntrada.Visible   = false;
            label9.Visible              = false;
            label15.Visible             = false;
            textBox13.Visible           = false;


            txtIdCliente.TextAlign = HorizontalAlignment.Center;
            txtCliente.TextAlign = HorizontalAlignment.Center;
            txtCliente.ReadOnly = true;
            txtData.ReadOnly = true;
            txtValorTotal.ReadOnly = true;
            txtCodigoProduto.ReadOnly = true;
            txtProduto.ReadOnly = true;
            txtMarca.ReadOnly = true;
            txtAno.ReadOnly = true;
            txtClassificacao.ReadOnly = true;
            txtValorUnitario.ReadOnly = true;

            txtProduto.TextAlign = HorizontalAlignment.Center;
            txtData.TextAlign = HorizontalAlignment.Center;
            txtValorTotal.TextAlign = HorizontalAlignment.Center;

            txtIdItensEntrada.ReadOnly = true;
            txtIdItensEntrada.TextAlign = HorizontalAlignment.Center;
            txtCodigoProduto.TextAlign = HorizontalAlignment.Center;
            txtProduto.TextAlign = HorizontalAlignment.Center;
            txtMarca.TextAlign = HorizontalAlignment.Center;
            txtAno.TextAlign = HorizontalAlignment.Center;
            txtValorUnitario.TextAlign = HorizontalAlignment.Center;
            txtValorFinal.TextAlign = HorizontalAlignment.Center;
            txtClassificacao.TextAlign = HorizontalAlignment.Center;
            txtQuantidade.TextAlign = HorizontalAlignment.Center;
            textBox13.ReadOnly = true;
            textBox13.TextAlign = HorizontalAlignment.Center;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cabeçalho da nota do Cliente
            System.String id_cliente, cliente, data, valorTotalNota;

            id_cliente = txtIdCliente.Text;
            cliente = txtCliente.Text;
            data = txtData.Text;
            valorTotalNota = txtValorTotal.Text;
            //####################################################################################################
            //Corpo da nota
            System.String id_itens_saida, codProduto, produto, marca, ano, valorUnitario, valorFinal, classificacao, quantidade;

            id_itens_saida = txtIdItensEntrada.Text;
            codProduto = txtCodigoProduto.Text;
            produto = txtProduto.Text;
            marca = txtMarca.Text;
            ano = txtAno.Text;
            valorUnitario = txtValorUnitario.Text;
            valorFinal = txtValorFinal.Text;
            classificacao = txtClassificacao.Text;
            quantidade = txtQuantidade.Text;
            //#####################################################################################################

            //Operação de Database
            MySqlConnection mySqlConnection;
            MySqlCommand mySqlCommand;

            try
            {
                //passando a string de conexão com o banco
                mySqlConnection = new MySqlConnection(connectionString);

                //abrindo a conexão
                mySqlConnection.Open();

                //criando o 1° comando SQL
                mySqlCommand = new MySqlCommand("UPDATE CLIENTE SET CLIENTE ='" + cliente + "',DATA='" + data + "',VALOR_TOTAL_NOTA='" + valorTotalNota + "'WHERE ID_CLIENTE='" + id_cliente + "'", mySqlConnection);

                //Executa a 1° query
                mySqlCommand.ExecuteNonQuery();

                //criando o 2° comando SQL
                mySqlCommand = new MySqlCommand("UPDATE ITENS_NOTA_SAIDA SET CODIGOPRODUTO='" + codProduto + "',PRODUTO='" + produto + "',MARCA='" + marca + "',ANO='"
                   + ano + "',VALOR_UNITARIO='" + valorUnitario + "',VALOR_FINAL='" + valorFinal + "',CLASSIFICACAO='" + classificacao + "',QUANTIDADE='" + quantidade + "' WHERE ID_ITENS_SAIDA='" + id_itens_saida + "'", mySqlConnection);

                //Executa a 2° query
                mySqlCommand.ExecuteNonQuery();

                //Atualizando o saldo
                UpdateDatabaseProdutos atualizar = new UpdateDatabaseProdutos();
                atualizar.updateAtualizar_Subtrair_Cliente_Saida(ref codProduto, ref valorFinal, ref quantidade);

                System.Windows.Forms.MessageBox.Show("Dados alterados com Sucesso!!!");

                //Fecha a conexão
                mySqlConnection.Close();

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static void conversaoMoedaBrasileira(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');

                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtValorFinal_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref txtValorFinal);
        }
    }
}
