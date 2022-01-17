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
    public partial class Nota_Saida : Form
    {
        public static string id_cliente = "";

        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public Nota_Saida()
        {
            InitializeComponent();
        }

        private void Nota_Saida_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = loadNotaFornecedor();
            //dataGridView2.DataSource = loadItensFornecedor();
            loadDataGridView01(id_cliente);
            loadDataGridView02(id_cliente);
        }

        public void loadDataGridView01(string id)
        {
            try
            {
                //MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(); 
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                //string sqlQuerie = "SELECT * FROM CLIENTE WHERE ID_CLIENTE='" + id + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM CLIENTE WHERE ID_CLIENTE='" + id_cliente + "'", connection);

                //mySqlDataAdapter.SelectCommand = new MySqlCommand(sqlQuerie, connection);

                DataTable table = new DataTable();
                mySqlDataAdapter.Fill(table);

                //BindingSource bSource = new BindingSource();
                //bSource.DataSource = table;

                dataGridView1.DataSource = table;

            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

        //SELECT * FROM ITENS_NOTA_SAIDA WHERE ID_CLIENTE = 1

        public void loadDataGridView02(string id_cliente)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM ITENS_NOTA_SAIDA WHERE ID_CLIENTE='" + id_cliente + "'", connection);

                DataTable table = new DataTable();
                mySqlDataAdapter.Fill(table);

                dataGridView2.DataSource = table;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }


        public DataTable loadNotaSaida(string id)
        {
            DataTable notaTable = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM FORNECEDOR WHERE ID_CLIENTE='" + id + "'", con))
                {
                    con.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    notaTable.Load(reader);
                }
            }

            return notaTable;
        }

        private DataTable loadNotaFornecedor()
        {
            DataTable notaTable = new DataTable();


            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM FORNECEDOR", con))
                {
                    con.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    notaTable.Load(reader);
                }
            }

            return notaTable;
              /*  try
                {
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    MySqlCommand cmd;
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    string querie = "SELECT * FROM FORNECEDOR";
                    //cmd = new MySqlCommand("SELECT * FROM FORNECEDOR WHERE ID_FORNECEDOR='" + item. + "'", connection);
                    //cmd = new MySqlCommand("SELECT * FROM FORNECEDOR'", connection);
                    adapter.SelectCommand = new MySqlCommand(querie, connection);
                    DataSet data = new DataSet();
                    //DataTable table = new DataTable();
                    adapter.Fill(data);

                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = data;

                    dataGridView1.DataSource = bSource;
                    //dataGridView1.DataSource = data.Tables[0];
                }
                catch (Exception ex)
                {

                }*/
        }

        private DataTable loadItensFornecedor()
        {
            DataTable notaItens = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM ITENS_NOTA_SAIDA", con))
                {
                    con.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    notaItens.Load(reader);
                }
            }

            return notaItens;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cabeçalho do Cliente
            System.String id_cliente, cliente, data, valorTotal;

            //Itens da nota do Cliente
            System.String id_itens_saida, codProduto, produto, marca, ano, valorUnitario, valorFinal, classificacao, quantidade;

            Atualizar_Fornecedor_Nota_Saida saida = new Atualizar_Fornecedor_Nota_Saida();
            //Recebendo o cabeçalho
            id_cliente      = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cliente         = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            data            = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            valorTotal      = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //###########################################################################
            //Recebendo o corpo
            id_itens_saida  = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();
            codProduto      = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
            produto         = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();
            marca           = this.dataGridView2.CurrentRow.Cells[3].Value.ToString();
            ano             = this.dataGridView2.CurrentRow.Cells[4].Value.ToString();
            valorUnitario   = this.dataGridView2.CurrentRow.Cells[5].Value.ToString();
            valorFinal      = this.dataGridView2.CurrentRow.Cells[6].Value.ToString();
            classificacao   = this.dataGridView2.CurrentRow.Cells[7].Value.ToString();
            quantidade      = this.dataGridView2.CurrentRow.Cells[8].Value.ToString();
            //###########################################################################

            //Cabeçalho para envio
            saida.txtIdCliente.Text         = id_cliente;
            saida.txtCliente.Text           = cliente;
            saida.txtData.Text              = data;
            saida.txtValorTotal.Text        = valorTotal;

            //Corpo para envio
            saida.txtIdItensEntrada.Text    = id_itens_saida;
            saida.txtCodigoProduto.Text     = codProduto;
            saida.txtProduto.Text           = produto;
            saida.txtMarca.Text             = marca;
            saida.txtAno.Text               = ano;
            saida.txtValorUnitario.Text     = valorUnitario;
            saida.txtValorFinal.Text        = valorFinal;
            saida.txtClassificacao.Text     = classificacao;
            saida.txtQuantidade.Text        = quantidade;
            saida.textBox13.Text            = id_itens_saida;

            saida.ShowDialog();
        }
    }
}
