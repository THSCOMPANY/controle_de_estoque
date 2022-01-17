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

namespace Controle_Estoque
{
    public partial class Pesquisar_Produtos_Entrada : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        Incluir_NF incluir_Nota;// = new Incluir();

        public Pesquisar_Produtos_Entrada(Incluir_NF inclui)
        {
            InitializeComponent();
            incluir_Nota = inclui;
            FillList();
            listView1.FullRowSelect = true;
        }

        public void FillList() //Preenche a lista com os dados dos produtos
        {
            try
            {
                MySqlCommand   cmd;
                MySqlDataReader dr;
                MySqlConnection conexao = new MySqlConnection(connectionString);
                conexao.Open();
                //System.Windows.Forms.MessageBox.Show("Conexão foi um Sucesso!!");
                cmd = new MySqlCommand("SELECT * FROM PRODUTOS", conexao);
                dr = cmd.ExecuteReader();

                listView1.Items.Clear();
                while(dr.Read())
                {
                    ListViewItem lv = new ListViewItem(dr.GetString(1));
                    lv.SubItems.Add(dr.GetString(2));
                    lv.SubItems.Add(dr.GetString(3));
                    lv.SubItems.Add(dr.GetString(4));
                    lv.SubItems.Add(dr.GetString(5));
                    lv.SubItems.Add(Convert.ToDouble(dr.GetString(6)).ToString("C"));
                    lv.SubItems.Add(dr.GetString(7));
                    lv.SubItems.Add(dr.GetString(8));
                    //lv.SubItems.Add(dr.GetString(8));
                    listView1.Items.Add(lv);
                }
                conexao.Close();
            }
            catch(MySqlException ex)
            {
                String errorConnection = ex.ToString();
                System.Windows.Forms.MessageBox.Show(errorConnection);
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

                if (textBox1.Text == "")
                {
                    FillList();
                    //System.Windows.Forms.MessageBox.Show("Por favor informe uma informação!!!");
                }
                else
                {
                    MySqlConnection conexao = new MySqlConnection(connectionString);
                    conexao.Open();
                    MySqlCommand   cmd;
                    MySqlDataReader dr;
                    cmd = new MySqlCommand("SELECT * FROM PRODUTOS WHERE PRODUTO LIKE '%"+ textBox1.Text + "%'", conexao);
                    //cmd = new MySqlCommand("SELECT * FROM PRODUTOS WHERE PRODUTO='" + textBox1.Text + "'", conexao);

                    dr = cmd.ExecuteReader();
                    listView1.Items.Clear();

                    while(dr.Read())
                    {
                        ListViewItem list = new ListViewItem(dr.GetString(1));
                        list.SubItems.Add(dr.GetString(2));
                        list.SubItems.Add(dr.GetString(3));
                        list.SubItems.Add(dr.GetString(4));
                        list.SubItems.Add(dr.GetString(5));
                        list.SubItems.Add(dr.GetString(6));
                        list.SubItems.Add(dr.GetString(7));
                        list.SubItems.Add(dr.GetString(8));
                        //list.SubItems.Add(dr.GetString(8));
                        listView1.Items.Add(list);
                    }
                    
                    conexao.Close();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        
        private void pesquisaProdutos() //Sem uso no momento
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(connectionString);
                MySqlCommand cmd;
                MySqlDataReader dr;
                conexao.Open();

                cmd = new MySqlCommand("SELECT CODIGOPRODUTO, PRODUTO, MARCA, ANO, VALOR_UNITARIO, VALOR_FINAL, CLASSIFICACAO, QUANTIDADE FROM PRODUTOS WHERE CODIGOPRODUTO='" + textBox1.Text + "'", conexao);
                dr = cmd.ExecuteReader();

                dr.Read();
                if(dr.HasRows)
                {
                    textBox1.Text = dr[0].ToString();
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void button2_Click(object sender, EventArgs e) { }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var c1 = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            var c2 = listView1.Items[listView1.FocusedItem.Index].SubItems[1].Text;
            var c3 = listView1.Items[listView1.FocusedItem.Index].SubItems[2].Text;
            var c4 = listView1.Items[listView1.FocusedItem.Index].SubItems[3].Text;
            var c5 = listView1.Items[listView1.FocusedItem.Index].SubItems[4].Text;
            var c6 = listView1.Items[listView1.FocusedItem.Index].SubItems[5].Text;
            var c7 = listView1.Items[listView1.FocusedItem.Index].SubItems[6].Text;
            var c8 = listView1.Items[listView1.FocusedItem.Index].SubItems[7].Text;
            Incluir_NF.codigoProduto            = c1.ToString();
            Incluir_NF.Produto                  = c2.ToString();
            Incluir_NF.Marca                    = c3.ToString();
            Incluir_NF.Ano                      = c4.ToString();
            Incluir_NF.valor_Unitario_Anterior  = c5.ToString();
            Incluir_NF.valor_Final              = c6.ToString();
            Incluir_NF.classificacao            = c7.ToString();
            Incluir_NF.quant                    = c8.ToString();
            incluir_Nota.receberDados();
            this.Close(); 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Cadastrar cadastro = new Cadastrar();
            cadastro.Show();
        }
    }
}
