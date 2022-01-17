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
    public partial class Atualizar_Fornecedor_Nota_Entrada : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        //Máquina Éder
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;Port=3306;pwd=Michaeldias03092009;";

        public Atualizar_Fornecedor_Nota_Entrada()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Enabled = false;
            textBox1.Visible = false;
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.ReadOnly = true;
            textBox3.TextAlign = HorizontalAlignment.Center;
            textBox3.ReadOnly = true;
            textBox4.TextAlign = HorizontalAlignment.Center;
            textBox4.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox6.TextAlign = HorizontalAlignment.Center;
            textBox6.Visible = false;
            textBox8.TextAlign = HorizontalAlignment.Center;
            textBox8.ReadOnly = true;
            textBox7.TextAlign = HorizontalAlignment.Center;
            textBox7.ReadOnly = true;
            textBox5.TextAlign = HorizontalAlignment.Center;
            textBox5.ReadOnly = true;
            textBox9.TextAlign = HorizontalAlignment.Center;
            textBox9.ReadOnly = true;
            textBox10.TextAlign = HorizontalAlignment.Center;
            textBox11.TextAlign = HorizontalAlignment.Center;
            textBox12.TextAlign = HorizontalAlignment.Center;
            textBox12.ReadOnly = true;
            textBox13.ReadOnly = true;
            textBox13.TextAlign = HorizontalAlignment.Center;
            textBox13.Visible = false;
            textBox14.TextAlign = HorizontalAlignment.Center;

            label15.Visible = false;
            label1.Visible = false;
            label9.Visible = false;
            /*Task tarefa = new Task(new Action(() =>
            {
                RefreshLines();

            }));

            tarefa.Start();*/
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //Cabeçalho da Nota
            System.String fornecedor, data, valor_total;

            fornecedor = textBox2.Text;
            data = textBox3.Text;
            valor_total = textBox4.Text;
            //#################################################################################
            //Itens da Nota
            System.String codigoProduto, produto, marca, ano, valor_unitario, valor_final, classificacao, quantidade;

            codigoProduto = textBox8.Text;
            produto = textBox7.Text;
            marca = textBox5.Text;
            ano = textBox9.Text;
            valor_unitario = textBox10.Text;
            valor_final = textBox11.Text;
            classificacao = textBox12.Text;
            quantidade = textBox14.Text;

            if (valor_final.Contains("R$"))
            {
                System.Windows.Forms.MessageBox.Show("POR FAVOR RETIRE O 'R$' ");
            }
            else
            {

                //Cria as instancias da Classe Mysql
                MySqlConnection mySqlConnection;
                MySqlCommand mySqlCommand;

                try
                {
                    //Passa a os parametros de conexão com o banco
                    mySqlConnection = new MySqlConnection(connectionString);
                    //Abre a conexão
                    mySqlConnection.Open();

                    //Cria a 1° query
                    mySqlCommand = new MySqlCommand("UPDATE FORNECEDOR SET FORNECEDOR ='" + fornecedor + "',DATA='" + data + "',VALOR_TOTAL_NOTA='" + valor_total + "' WHERE ID_FORNECEDOR ='" + textBox1.Text + "'", mySqlConnection);
                    //UPDATE PRODUTOS SET CODIGOPRODUTO='" + txtCodProduto.Text + "',PRODUTO='" + txtDescProduto.Text + "',MARCA='" + txtMarca.Text + "',ANO='" + txtAno.Text + "',VALOR_UNITARIO='" + txtValorUnitario.Text + "',VALOR_FINAL='" + txtValorFinal.Text + "',CLASSIFICACAO='" + txtClassificacao.Text + "',QUANTIDADE='" + txtQuantidade.Text + "' WHERE ID_PRODUTO='" + txtIDProduto.Text + "'", Connection

                    //Executa a 1° query
                    mySqlCommand.ExecuteNonQuery();

                    //Cria a 2° query
                    mySqlCommand = new MySqlCommand("UPDATE ITENS_NOTA_ENTRADA SET CODIGOPRODUTO='" + codigoProduto + "',PRODUTO='" + produto + "',MARCA='" + marca +
                        "',ANO='" + ano + "',VALOR_UNITARIO='" + valor_unitario + "',VALOR_FINAL='" + valor_final + "',CLASSIFICACAO='" + classificacao + "',QUANTIDADE='" + quantidade + "' WHERE ID_ITENS_ENTRADA='" + textBox6.Text + "'", mySqlConnection);

                    //Executa a 2° query
                    mySqlCommand.ExecuteNonQuery();

                    //Atualizando o saldo 
                    UpdateDatabaseProdutos updateDatabaseProdutos = new UpdateDatabaseProdutos();
                    updateDatabaseProdutos.updateAtualizar_Soma_Fornecedor_Entrada(ref codigoProduto, ref valor_unitario, ref valor_final, ref quantidade);

                    MessageBox.Show("Dados alterados com Sucesso");
                    //Fecha a conexão
                    mySqlConnection.Close();

                }

                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }

            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
        }

        //Esse procedimento está inutilizável no momemento
        public void RefreshLines()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.RefreshLines));
            }

            for(int i = 1; i <= 10000; i++)
            {
                ListViewItem LVI = new ListViewItem("Track" + i);
                LVI.SubItems.Add("Atualizando");
                
            }

        }
        
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref textBox10);
        }
        
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref textBox11);
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
                System.Windows.Forms.MessageBox.Show("Erro na conversão Monetária!!!");
            }
        }
    }
}
