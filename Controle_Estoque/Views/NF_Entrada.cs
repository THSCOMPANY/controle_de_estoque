using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Controle_Estoque
{
    public partial class NF_Entrada : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        static System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";

        public NF_Entrada()
        {
            InitializeComponent();
            carregaNF();
        }

        private void Entrada_Saida_Load(object sender, EventArgs e)
        {
            //carregaNF();//Carrega o Form com as NF's cadastradas
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Incluir_NF incluir = new Incluir_NF();
            incluir.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

        public void carregaNF() //não pode ser método static
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
                listView2.Columns.Add("N°", 90, HorizontalAlignment.Center);
                listView2.Columns.Add("FORNECEDOR", 350, HorizontalAlignment.Center);
                listView2.Columns.Add("DATA", 200, HorizontalAlignment.Center);
                listView2.Columns.Add("VALOR TOTAL", 130, HorizontalAlignment.Center);
                //listView2.Columns.Add("ITENS", 80, HorizontalAlignment.Center);
                //listView2.Columns.Add("VALOR TOTAL", 120, HorizontalAlignment.Center);
                // Set the view to show details
                listView2.View = View.Details;
                // Allow the user to edit item text
                //listView2.LabelEdit = true;
                // Allow the user to rearrange columns.
                listView2.AllowColumnReorder = true;
                // Display check boxes.
                //listView2.CheckBoxes = true;
                // Select the item and subitems when selection is made.
                listView2.FullRowSelect = true;
                // Display grid lines.
                listView2.GridLines = true;
                // Sort the items in the list in ascending order.
                //listView1.Sorting = SortOrder.Ascending;
                // Sort the items in the list in descending order.
                //listView1.Sorting = SortOrder.Descending;
                // No Sort datas
                listView2.Sorting = SortOrder.Descending;

                listView2.FullRowSelect = true;

                cmd = new MySqlCommand("SELECT * FROM FORNECEDOR", Connection);
                da = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "tableProducts");
                Connection.Close();

                dt = ds.Tables["tableProducts"];
                int i;
                //11 linhas + 1sse
                listView2.BeginUpdate();
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    listView2.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    listView2.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString()); //Cod
                    listView2.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString()); //Cod
                    listView2.Items[i].SubItems.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3]).ToString("C2"));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            listView2.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            string id;

            id = listView2.SelectedItems[0].SubItems[0].Text;
            Nota_Entrada entrada = new Nota_Entrada();
            Nota_Entrada.id_Fornecedor = id;
            entrada.Show();
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text == "")
                {
                    //Preenche os campos ao apagar na textbox
                    preencherListaNotaEntrada();
                }
                else
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();
                    MySqlCommand cmd;
                    MySqlDataReader dr;
                    cmd = new MySqlCommand("SELECT * FROM FORNECEDOR WHERE FORNECEDOR LIKE '%" + textBox1.Text + "%'", mySqlConnection);

                    dr = cmd.ExecuteReader();
                    listView2.Items.Clear();

                    while(dr.Read())
                    {
                        //Carrega os dados na lista
                        ListViewItem list = new ListViewItem(dr.GetString(0));
                        list.SubItems.Add(dr.GetString(1));
                        list.SubItems.Add(dr.GetString(2));
                        list.SubItems.Add(dr.GetString(3));
                        listView2.Items.Add(list);
                    }
                    mySqlConnection.Close();
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Procedimento para preencher os campos
        public void preencherListaNotaEntrada()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand mySqlCommand;
                MySqlDataReader dataReader;
                connection.Open();

                mySqlCommand = new MySqlCommand("SELECT * FROM FORNECEDOR", connection);
                dataReader = mySqlCommand.ExecuteReader();

                listView2.Items.Clear();
                while(dataReader.Read())
                {
                    ListViewItem list = new ListViewItem(dataReader.GetString(0));
                    list.SubItems.Add(dataReader.GetString(1));
                    list.SubItems.Add(dataReader.GetString(2));
                    list.SubItems.Add(dataReader.GetString(3));
                    listView2.Items.Add(list);
                }
                connection.Close();
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
