using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle_Estoque
{
    public partial class Nota_Entrada : Form
    {
        public static string id_Fornecedor = "";

        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public Nota_Entrada()
        {
            InitializeComponent();
        }

        private void Nota_Entrada_Load(object sender, EventArgs e)
        {
            loadGridView01(id_Fornecedor);
            loadGridView02(id_Fornecedor);
        }

        private void loadGridView01(string id)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM FORNECEDOR WHERE ID_FORNECEDOR='" + id + "'", connection);

                DataTable table = new DataTable();
                mySqlDataAdapter.Fill(table);

                dataGridView1.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Aconteceu alguma falha no processo!!!");
                Console.WriteLine(ex);
            }
        }

        private void loadGridView02(string id)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM ITENS_NOTA_ENTRADA WHERE ID_FORNECEDOR='" + id + "'", connection);

                DataTable table = new DataTable();
                mySqlDataAdapter.Fill(table);

                dataGridView2.DataSource = table;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Aconteceu alguma falha no processo!!!");
                Console.WriteLine(ex);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Dados do Fornecedor
            System.String id_fornecedor, fornecedor, data, valor_total_nota;

            //Produtos do Fornecedor
            System.String id_itens_entrada, codigo_produto, produto, marca, ano, valor_unitario, valor_final, classificacao, quantidade;


            Atualizar_Fornecedor_Nota_Entrada entrada = new Atualizar_Fornecedor_Nota_Entrada();

            //Recebendo o Cabeçalho
            id_fornecedor       = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            fornecedor          = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            data                = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            valor_total_nota    = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();

            //##########################################################################################################################
            //Recebendo o Corpo
            id_itens_entrada    = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();
            codigo_produto      = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
            produto             = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();
            marca               = this.dataGridView2.CurrentRow.Cells[3].Value.ToString();
            ano                 = this.dataGridView2.CurrentRow.Cells[4].Value.ToString();
            valor_unitario      = this.dataGridView2.CurrentRow.Cells[5].Value.ToString();
            valor_final         = this.dataGridView2.CurrentRow.Cells[6].Value.ToString();
            classificacao       = this.dataGridView2.CurrentRow.Cells[7].Value.ToString();
            quantidade          = this.dataGridView2.CurrentRow.Cells[8].Value.ToString();

            //Cabeçalho para envio
            entrada.textBox1.Text = id_Fornecedor;
            entrada.textBox2.Text = fornecedor;
            entrada.textBox3.Text = data;
            entrada.textBox4.Text = valor_total_nota;

            //Corpo para envio
            entrada.textBox6.Text = id_itens_entrada;
            entrada.textBox8.Text = codigo_produto;
            entrada.textBox7.Text = produto;
            entrada.textBox5.Text = marca;
            entrada.textBox9.Text = ano;
            entrada.textBox10.Text = valor_unitario;
            entrada.textBox11.Text = valor_final;
            entrada.textBox12.Text = classificacao;
            entrada.textBox14.Text = quantidade;
            entrada.textBox13.Text = id_fornecedor;

            entrada.ShowDialog();
        }
    }
}
