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
    public partial class NF_Saida : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        static System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public NF_Saida()
        {
            InitializeComponent();
            carregarNF();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Saida_NF saida = new Saida_NF();
            saida.Show();
        }

        private void carregarNF()
        {
            try
            {
                MySqlConnection Connection = new MySqlConnection(connectionString);
                //mySqlCon.ConnectionString = "server=" + server + ";" + "Username=" + username + ";" + "database=" + database;
                Connection.Open();

                MySqlCommand cmd;
                MySqlDataAdapter da;
                DataTable dt;
                DataSet ds;

                //Cabeçalho do ListView
                listView1.Columns.Add("N°",         90, HorizontalAlignment.Center);
                listView1.Columns.Add("CLIENTE",    350, HorizontalAlignment.Center);
                listView1.Columns.Add("DATA",       200, HorizontalAlignment.Center);
                listView1.Columns.Add("VALOR TOTAL", 130, HorizontalAlignment.Center);
                listView1.Columns.Add("LUCRO", 130, HorizontalAlignment.Center);
                //listView2.Columns.Add("ITENS", 80, HorizontalAlignment.Center);
                //listView2.Columns.Add("VALOR TOTAL", 120, HorizontalAlignment.Center);
                // Set the view to show details
                listView1.View = View.Details;
                // Allow the user to edit item text
                //listView2.LabelEdit = true;
                // Allow the user to rearrange columns.
                listView1.AllowColumnReorder = true;
                // Display check boxes.
                //listView2.CheckBoxes = true;
                // Select the item and subitems when selection is made.
                listView1.FullRowSelect = true;
                // Display grid lines.
                listView1.GridLines = true;
                // Sort the items in the list in ascending order.
                //listView1.Sorting = SortOrder.Ascending;
                // Sort the items in the list in descending order.
                //listView1.Sorting = SortOrder.Descending;
                // No Sort datas
                listView1.Sorting = SortOrder.None;

                listView1.FullRowSelect = true;

                cmd = new MySqlCommand("SELECT * FROM CLIENTE", Connection);
                da = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "tableProducts");
                Connection.Close();

                dt = ds.Tables["tableProducts"];
                int i;
                //11 linhas + 1
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString()); //Cod
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString()); //Cod
                    listView1.Items[i].SubItems.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3]).ToString("C2"));
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Aconteceu uma falha");
                Console.WriteLine(ex);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string id;

            id = listView1.SelectedItems[0].SubItems[0].Text;
            Nota_Saida saida = new Nota_Saida();
            Nota_Saida.id_cliente = id;
            saida.Show();
            //s.loadDataGridView01(id);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void listView1_Click(object sender, EventArgs e)
        {
            //System.Windows.MessageBox.Show("Voce Deseja alterar alguma informação nesta nota ?");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text == "")
                {
                    //System.Windows.Forms.MessageBox.Show("Por favor informe Dados");
                    preencherNotaSaida();
                }
                else
                {
                    MySqlConnection conexao = new MySqlConnection(connectionString);
                    conexao.Open();
                    MySqlCommand cmd;
                    MySqlDataReader dr;
                    cmd = new MySqlCommand("SELECT * FROM CLIENTE WHERE CLIENTE LIKE '%" + textBox1.Text + "%'", conexao);

                    dr = cmd.ExecuteReader();
                    listView1.Items.Clear();

                    while(dr.Read())
                    {
                        ListViewItem list = new ListViewItem(dr.GetString(0));
                        list.SubItems.Add(dr.GetString(1));
                        list.SubItems.Add(dr.GetString(2));
                        list.SubItems.Add(dr.GetString(3));
                        //list.SubItems.Add(dr.GetString(4)); //Como tem campo NULL preciso resolver
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

        public void preencherNotaSaida()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(connectionString);
                MySqlCommand mySqlCommand;
                MySqlDataReader dr;
                conexao.Open();

                mySqlCommand = new MySqlCommand("SELECT * FROM CLIENTE", conexao);
                dr = mySqlCommand.ExecuteReader();

                listView1.Items.Clear();
                while(dr.Read())
                {
                    ListViewItem lv = new ListViewItem(dr.GetString(0));
                    lv.SubItems.Add(dr.GetString(1));
                    lv.SubItems.Add(dr.GetString(2));
                    lv.SubItems.Add(dr.GetString(3));
                    listView1.Items.Add(lv);
                }
                conexao.Close();
            }
            catch(MySqlException ex)
            {
                String erro = ex.ToString();
                System.Windows.Forms.MessageBox.Show(erro);
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
