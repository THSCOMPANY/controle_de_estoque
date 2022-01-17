using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle_Estoque
{
    public partial class Relatorio_Estoque : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";
        private int contadorPaginas = 0; //Para contagem de páginas

        public Relatorio_Estoque()
        {
            InitializeComponent();
            loadListView();
            //setMyButtonIcon();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                PaperSize tamanho = new PaperSize("", 800, 800);
                tamanho.PaperName = PaperKind.Custom.ToString();
                Margins margem = new Margins();
                margem.Left = 0;
                margem.Top = 50;

                contadorPaginas = 0;
                printDocument1.Print();
            }

            Progresso_PDF_Relatorio progresso = new Progresso_PDF_Relatorio();
            progresso.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //https://stackoverflow.com/questions/24561611/multiple-page-print-document-in-c-sharp
            try
            {
                Graphics graphics = e.Graphics;
                SolidBrush brush = new SolidBrush(Color.Black);

                Font font = new Font("Courier New", 10);

                float pageWidth =  e.PageSettings.PrintableArea.Width;
                float pageHeight = e.PageSettings.PrintableArea.Height;

                float fontHeight = font.GetHeight();
                int startX = 10;
                //int startY = 30;
                int offsetY = 6;

                int i;
                i = 1;

                //e.Graphics.DrawString("Produto",    new System.Drawing.Font("Times Roman", 10), Brushes.Black, 750, startX + offsetY);
                //e.Graphics.DrawString("Quantidade", new System.Drawing.Font("Times Roman", 10), Brushes.Black, 30, startX + offsetY);

                //while( contadorPaginas <= listView1.Items.Count)
                while (contadorPaginas <= listView1.Items.Count - 1)
                {
                    // foreach (ListViewItem item in listView1.Items)
                    //{

                    //graphics.DrawString("Line:" + i, font, brush, startX, startY + offsetY);
                    //offsetY += (int)fontHeight;
                    //e.Graphics.DrawString(listView1.Items[contadorPaginas].Text, new System.Drawing.Font("Times Roman", 10), Brushes.Black, 750, startX + offsetY);

                    e.Graphics.DrawString(listView1.Items[contadorPaginas].SubItems[7].Text, new System.Drawing.Font("Times Roman", 9), Brushes.Black, 750, startX + offsetY);
                    e.Graphics.DrawString(listView1.Items[contadorPaginas].SubItems[1].Text, new System.Drawing.Font("Times Roman", 9), Brushes.Black, 30, startX + offsetY);
                    offsetY += (int)fontHeight;
                    i += 2; //Espaçamento

                    if (offsetY >= pageHeight)
                    {
                        e.HasMorePages = true;
                        offsetY = 0;
                        return;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }

                    contadorPaginas = contadorPaginas + 1;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

        public void loadListView() // Esse procedimento, é para carregar os dados do banco, tabela Produtos e exibir no ListView para o usuário
        {
            MySqlConnection Connection = new MySqlConnection(connectionString);

            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            MySqlCommand cmd;
            MySqlDataAdapter da;
            DataTable dt;
            DataSet ds;

            //Cabeçalho do ListView
            //listView1.Columns.Add("ID PRODUTO", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("COD. PRODUTO",   80, HorizontalAlignment.Center);
            listView1.Columns.Add("PRODUTO",        150, HorizontalAlignment.Center);
            listView1.Columns.Add("MARCA",          50, HorizontalAlignment.Center);
            listView1.Columns.Add("ANO",            50, HorizontalAlignment.Center);
            listView1.Columns.Add("VALOR UNITÁRIO", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("VALOR FINAL",    50, HorizontalAlignment.Center);
            listView1.Columns.Add("CLASSIFICACAO",  150, HorizontalAlignment.Center);
            listView1.Columns.Add("QUANTIDADE",     80, HorizontalAlignment.Center);
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
            //11 linhas + 1
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString()); //ID
                listView1.Items.Add(dt.Rows[i].ItemArray[1].ToString()); //Codigo produto
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString()); //Produto
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString()); //Marca
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString()); //Ano
                // Carrega transformando para R$
                listView1.Items[i].SubItems.Add(Convert.ToDouble(dt.Rows[i].ItemArray[5]).ToString("C")); //Valor Unitário
                listView1.Items[i].SubItems.Add(Convert.ToDouble(dt.Rows[i].ItemArray[6]).ToString("C")); //Valor Final
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());//.ToString("C")); //Classificação
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString()); //Quantidade
               // listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString()); //Quant
                //listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel ex = new Excel();
            ex.exportarExcel(listView1);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (TextWriter tw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                    {
                        foreach(ListViewItem item in listView1.Items)
                        {
                            await tw.WriteLineAsync(item.SubItems[0].Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text + "\t" + item.SubItems[3].Text + "\t" + item.SubItems[4].Text + "\t" + item.SubItems[5].Text + "\t" + item.SubItems[5].Text + "\t" + item.SubItems[6].Text + "\t" + item.SubItems[7].Text);
                        }

                        MessageBox.Show("Seus dados foram exportados com Sucesso", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Coloca o icone no botão
        private void setMyButtonIcon()
        {
            //Assign an image to the button
            button1.Image = Image.FromFile("C:\\Users\\PARTICULAR\\source\\repos\\Controle_Estoque\\Controle_Estoque\\Resources\\printer.ico");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.TextAlign = ContentAlignment.MiddleRight;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Graficos g = new Graficos();
            //g.Show();
            GraphBar g = new GraphBar();
            g.Show();
        }

        private void tomadaDecisaoBaseadaMovimentacao()
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // TesteGraficos teste = new TesteGraficos();
            //teste.Show();
            TesteBar b = new TesteBar();
            b.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Gerador_Codigo_Barras gerador = new Gerador_Codigo_Barras();
            gerador.ShowDialog();
            
        }
    }
}
