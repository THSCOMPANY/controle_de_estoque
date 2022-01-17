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
    public partial class Pesquisa : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public Pesquisa()
        {
            InitializeComponent();
            loadListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Boolean localizar = false;

            //https://stackoverflow.com/questions/20341113/search-listview-items-using-textbox

            System.String mensagem = "INFORME O CODIGO DO PRODUTO";
            System.String cabeçalho = "AVISO";
            
            listView1.SelectedItems.Clear();

            
                if (textBox1.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show(mensagem, cabeçalho, System.Windows.Forms.MessageBoxButtons.OK);
                }

                else

                try
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Text.ToLower().Contains(textBox1.Text.ToLower()))
                        {
                            item.Selected = true;
                            item.BackColor = Color.YellowGreen;
                            localizar = true;
                            Console.WriteLine("True" + localizar);
                        }
                        else
                        {
                            listView1.Items.Remove(item);
                        }
                    }
                if (listView1.SelectedItems.Count == 1)
                {
                    listView1.Focus();
                }

            }catch(Exception ex)
            {
                localizar = false;
                Console.WriteLine("False" + localizar + ex);
                
            }
        }

        public void loadListView()
        {
            try
            {
                MySqlConnection Connection = new MySqlConnection(connectionString);
                Connection.Open();
                MySqlCommand cmd;
                MySqlDataAdapter da;
                DataTable dt;
                DataSet ds;

                //Cabeçalho do ListView
                //listView1.Columns.Add("ID PRODUTO", 90, HorizontalAlignment.Center);
                listView1.Columns.Add("COD. PRODUTO", 150, HorizontalAlignment.Center);
                listView1.Columns.Add("PRODUTO", 200, HorizontalAlignment.Center);
                listView1.Columns.Add("MARCA", 120, HorizontalAlignment.Center);
                listView1.Columns.Add("ANO", 80, HorizontalAlignment.Center);
                listView1.Columns.Add("VALOR UNITÁRIO", 120, HorizontalAlignment.Center);
                listView1.Columns.Add("VALOR FINAL", 110, HorizontalAlignment.Center);
                listView1.Columns.Add("CLASSIFICACAO", 150, HorizontalAlignment.Center);
                listView1.Columns.Add("QUANTIDADE", 115, HorizontalAlignment.Center);
                // Set the view to show details
                listView1.View = View.Details;
                // Allow the user to edit item text
                listView1.LabelEdit = true;
                // Allow the user to rearrange columns.
                listView1.AllowColumnReorder = true;
                // Display check boxes.
                //listView1.CheckBoxes = true;
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

                cmd = new MySqlCommand("SELECT * FROM PRODUTOS", Connection);
                da = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "tableProducts");
                Connection.Close();

                dt = ds.Tables["tableProducts"];
                int i;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    listView1.Items.Add(dt.Rows[i].ItemArray[1].ToString()); //Cod
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString()); //Prod
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString()); //Marca
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString()); //Ano
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString()); //Valor Unit
                                                                                         // Carrega transformando para R$
                    listView1.Items[i].SubItems.Add(Convert.ToString(dt.Rows[i].ItemArray[6])); //Valor Final
                    listView1.Items[i].SubItems.Add(Convert.ToString(dt.Rows[i].ItemArray[7]));//Classe
                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString()); //Quant
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Cannot open Connection!!" + ex);
                //Console.WriteLine("Cannot open Connection!!", ex);
                Console.WriteLine(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        public void mensagemAlerta()
        {
            MessageBox.Show("Não existe!!");
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }
    }
}
