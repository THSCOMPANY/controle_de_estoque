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
    public partial class Pesquisa_Produtos_Saida : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        Saida_NF saidaNota;// = new Saida_NF(); //referencia para outro form

        public Pesquisa_Produtos_Saida(Saida_NF saida)
        {
            InitializeComponent();
            saidaNota = saida;//Passa a referencia do FORM
            fillList();
            listView1.FullRowSelect = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fillList()
        {
            try
            { 
                MySqlConnection conexao = new MySqlConnection(connectionString);
                MySqlCommand cmd;
                MySqlDataReader dr;
                conexao.Open();
                //System.Windows.Forms.MessageBox.Show("Conexão foi um Sucesso!!");
                cmd = new MySqlCommand("SELECT * FROM PRODUTOS", conexao);
                dr = cmd.ExecuteReader();

                listView1.Items.Clear();
                while (dr.Read())
                {
                    ListViewItem lv = new ListViewItem(dr.GetString(1));
                    lv.SubItems.Add(dr.GetString(2));
                    lv.SubItems.Add(dr.GetString(3));
                    lv.SubItems.Add(dr.GetString(4));
                    lv.SubItems.Add(Convert.ToDouble(dr.GetString(5)).ToString("C"));
                    lv.SubItems.Add(Convert.ToDouble(dr.GetString(6)).ToString("C"));
                    lv.SubItems.Add(dr.GetString(7));
                    lv.SubItems.Add(dr.GetString(8));
                    //lv.SubItems.Add(dr.GetString(8));
                    listView1.Items.Add(lv);
                }
                conexao.Close();
            }
            catch (MySqlException ex)
            {
                String errorConnection = ex.ToString();
                System.Windows.Forms.MessageBox.Show(errorConnection);
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    fillList();
                    //System.Windows.Forms.MessageBox.Show("Por favor informe uma informação!!!");
                }
                else
                {
                    MySqlConnection conexao = new MySqlConnection(connectionString);
                    conexao.Open();
                    MySqlCommand cmd;
                    MySqlDataReader dr;
                    cmd = new MySqlCommand("SELECT * FROM PRODUTOS WHERE PRODUTO LIKE '%" + textBox1.Text + "%'", conexao);
                    //cmd = new MySqlCommand("SELECT * FROM PRODUTOS WHERE PRODUTO='" + textBox1.Text + "'", conexao);

                    dr = cmd.ExecuteReader();
                    listView1.Items.Clear();

                    while (dr.Read())
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

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
            Saida_NF.codigoProduto  = c1.ToString();
            Saida_NF.produto        = c2.ToString();
            Saida_NF.marca          = c3.ToString();
            Saida_NF.ano            = c4.ToString();
            Saida_NF.valorUnitario  = c5.ToString();
            Saida_NF.valorFinal     = c6.ToString();
            Saida_NF.classificacao  = c7.ToString();
            Saida_NF.quant          = c8.ToString();
            saidaNota.receberDados();
            this.Close();
        }

        private void Pesquisa_Produtos_Saida_FormClosed(object sender, FormClosedEventArgs e) { }

        private void Pesquisa_Produtos_Saida_Load(object sender, EventArgs e)
        {

        }
    }
}
