using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Collections;

namespace Controle_Estoque
{
    public partial class Cadastrar : Form 
    {
        Models.DatabaseMySql  database = new Models.DatabaseMySql();

        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";
        //Máquina Éder
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;Port=3306;pwd=Michaeldias03092009;";

        //declaracao das variaveis sempre primeiro
        int quant;
        private int contadorPaginas = 0; //Para contagem de páginas, esse cara aqui é para impressão do relatório no PDF, ele vai usar essa variavel,
        //para contar a quantidade necessárias de impressão em folhas A4
        
        public Cadastrar()
        {
            InitializeComponent();
            //database.connectionDatabase(); //somente para testar a conexão
            loadListView();

            //Cor de fundo do ListView
            //listView1.BackColor = Color.LightBlue;
            quant = listView1.Items.Count;
            label11.Text = Convert.ToInt32(quant).ToString();
            calcularSomaValorUnitario();
            calcularSomaValorFinal();
            calcularSomaTotalQuant();

            /*Task tarefa = new Task(new Action(() =>
            {
                RefreshLines();

            }));

            tarefa.Start();*/

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

        }

        //Inutilizável no momento
        public void RefreshLines()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.RefreshLines));
            }

            for(int i = 1; i <= 10000; i++)
            {
                ListViewItem LVI = new ListViewItem("Track" + i);
                //LVI.SubItems.Add("Updated");
                //listView1.Items.Add(LVI);
                listView1.TopItem = LVI;
                listView1.EnsureVisible(listView1.Items.Count);
                Application.DoEvents();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codigoProduto, produto, marca, ano, valor_Unitario, valor_Final, classificacao, quantidade;

            codigoProduto   =   txtCodProduto.Text;
            produto         =   txtDescProduto.Text;
            marca           =   txtMarca.Text;
            ano             =   txtAno.Text;
            valor_Unitario  =   txtValorUnitario.Text;
            valor_Final     =   txtValorFinal.Text;
            classificacao   =   txtClassificacao.Text;
            quantidade      =   txtQuantidade.Text;

            Models.ArrayListProdutos arrayList = new Models.ArrayListProdutos();
            arrayList.addProdutos(codigoProduto, produto, marca, ano, valor_Unitario, valor_Final, classificacao, quantidade);

            if (txtCodProduto.Text == "")
            {
                MessageBox.Show("Por favor informe o codigo do Produto!!!");
            }
            else
            {
                database.insertDatas(listView1, txtCodProduto, txtDescProduto, txtMarca, txtAno, txtValorUnitario, txtValorFinal, txtClassificacao, txtQuantidade);
            }

            //Cadastrar_Load(sender, e);
            listView1.Items.Clear();
            loadListView();
        }

        public void ThreadAtualizarListView()
        {
            //CRIAR UMA THREAD QUE VAI FICAR INFINITAMENTE LENDO OS DADOS DO BANCO E CARREGANDO PARA O LISTVIEW

        }

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
            /*listView1.Columns.Add("ID PRODUTO",       60, HorizontalAlignment.Center);
            listView1.Columns.Add("COD. PRODUTO",     80, HorizontalAlignment.Center);
            listView1.Columns.Add("PRODUTO",         280, HorizontalAlignment.Center);
            listView1.Columns.Add("MARCA",           120, HorizontalAlignment.Center);
            listView1.Columns.Add("ANO",              80, HorizontalAlignment.Center);
            listView1.Columns.Add("VALOR UNITÁRIO",  120, HorizontalAlignment.Center);
            listView1.Columns.Add("VALOR FINAL",     110, HorizontalAlignment.Center);
            listView1.Columns.Add("CLASSIFICACAO",   150, HorizontalAlignment.Center);
            listView1.Columns.Add("QUANTIDADE",       90, HorizontalAlignment.Center);
            listView1.Columns.Add("LUCRO",            80, HorizontalAlignment.Center);
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

            //listView1.FullRowSelect = true;*/

            cmd = new MySqlCommand("SELECT * FROM PRODUTOS", Connection);
            da = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "tableProducts");
            Connection.Close();

            dt = ds.Tables["tableProducts"];
            int i;
            //11 linhas + 1
            for ( i = 0; i <= dt.Rows.Count - 1; i++ )
            {
                listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString()); //ID
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString()); //Cod
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString()); //Prod
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString()); //Marca
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString()); //Ano
                // Carrega transformando para R$
                string totalValorUnitario =    (Convert.ToDouble(dt.Rows[i].ItemArray[5]).ToString()); //Valor Uni
                //Depois de popular a Listview, devo converter para real
                //listView1.Items[i].SubItems.Add(Convert.ToDouble(dt.Rows[i].ItemArray[5]).ToString("C")); //Valor Uni
                listView1.Items[i].SubItems.Add(Convert.ToDouble(dt.Rows[i].ItemArray[5]).ToString()); //Valor Unitario
                //Depois de popular a Listview, devo converter para real
                //listView1.Items[i].SubItems.Add(Convert.ToDouble(dt.Rows[i].ItemArray[6]).ToString("C"));//.ToString("C")); //Valor Final

                listView1.Items[i].SubItems.Add(Convert.ToDouble(dt.Rows[i].ItemArray[6]).ToString());//.ToString("C")); //Valor Final

                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString()); //Class
                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString()); //Quant
                //listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());

                //string totalValorUnitario = listView1.Items[i].SubItems[5].Text.ToString();
                string totalQuant = listView1.Items[i].SubItems[8].Text.ToString();

                double totalValorUnit  = Convert.ToDouble((totalValorUnitario)); // * Convert.ToDouble(totalQuant)));
                double totalQuantidade = Convert.ToDouble((totalQuant));
                
                //resolver aqui estava entrando com R$
                double valorUnit_x_totalQuantidade = (totalValorUnit * totalQuantidade);

                listView1.Items[i].SubItems.Add(Convert.ToDouble(valorUnit_x_totalQuantidade).ToString());
                

                listView1.Items[i].BackColor = Color.LightSeaGreen;
                //listView1.Items[i].BackColor = Color.Green;

                //if (i % 2 == 0)
                if (Convert.ToDouble(listView1.Items[i].SubItems[8].Text) < Convert.ToDouble(0) || Convert.ToDouble(listView1.Items[i].SubItems[8].Text) <= Convert.ToDouble(10))
                {
                    listView1.Items[i].BackColor = Color.Red;

                }else if(Convert.ToDouble(listView1.Items[i].SubItems[8].Text) >= Convert.ToDouble(11) && Convert.ToDouble(listView1.Items[i].SubItems[8].Text) <= Convert.ToDouble(30))
                {
                    listView1.Items[i].BackColor = Color.Yellow;
                }
            }

            calculateSumEstoque();

            //Create one ImageList objects
            //ImageList imageListSmall = new ImageList();

            //Initialize the ImageList objects with bitmaps.
            //imageListSmall.Images.Add(Bitmap.FromFile("C:\\Users\\PARTICULAR\\source\\repos\\Controle_Estoque\\produto.bmp"));

            //Assign the ImageList objects to the ListView.
            //listView1.SmallImageList = imageListSmall;

            calcularLucro();
        }

        private void calculateSumEstoque()
        {
            decimal valorAcumulado = 0;

            for(int i = 0; i < listView1.Items.Count; i++)
            {
                valorAcumulado += decimal.Parse(listView1.Items[i].SubItems[9].Text.ToString());
                //listView1.Items[i].SubItems[9].Text.Remove(1, 3);
                //valorAcumulado += decimal.Parse(listView1.Items[i].SubItems[9].Text.Remove(1,3).ToString());
            }
            
            textBox1.Text = Convert.ToDouble(valorAcumulado).ToString("C");

            //for (int j = 0; j < listView1.Items.Count; j++)
            //{
              //  Convert.ToDouble(listView1.Items[j].SubItems[9].Text).ToString("C");
                //Convert.ToDouble(listView1.Items[j].SubItems[9].Text).ToString("C");
           // }   
        }

        private void calcularLucro()
        {
            decimal lucro            = 0;
            decimal valorUnitario    = 0;
            decimal valorFinal       = 0;

            //Primeiro Loop para calcular o Lucro
            for(int i = 0; i < listView1.Items.Count; i++)
            {
                valorUnitario = decimal.Parse(listView1.Items[i].SubItems[5].Text.ToString());
                valorFinal    = decimal.Parse(listView1.Items[i].SubItems[6].Text.ToString());

                lucro = (valorUnitario - valorFinal);

                //Agora preciso popular cada linha do Lucro da ListView
                //listView1.Items.Add(lucro.ToString());
                //listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString()); //Quant

                if (valorUnitario > valorFinal)
                {
                    //converte para negativo
                    decimal perda = System.Math.Abs(lucro) * (-1);

                    listView1.Items[i].SubItems[9].Text = perda.ToString("C"); //Recebe em cada linha, o lucro de cada produto

                    //Converte para Reais
                    listView1.Items[i].SubItems[5].Text = (valorUnitario.ToString("C"));
                    listView1.Items[i].SubItems[6].Text = (valorFinal.ToString("C"));

                }
                else
                {
                    //string text = lucro < 0 ? "-" + (-lucro).ToString("C") : lucro.ToString("C");
                    //listView1.Items[i].SubItems[9].Text = lucro.ToString(); //Recebe em cada linha, o lucro de cada produto
                    //string text = lucro < 0 ? "-" + (-lucro).ToString() : lucro.ToString();
                    var lucroPositivo = Math.Abs(lucro);

                    listView1.Items[i].SubItems[9].Text = lucroPositivo.ToString("C"); //Recebe em cada linha, o lucro de cada produto

                    //Tento converter para real cada linha em tempo de execuçção

                    //listView1.Items[i].SubItems[5].Text = (text.ToString());
                    //Converte para Reais
                    listView1.Items[i].SubItems[5].Text = (valorUnitario.ToString("C"));
                    listView1.Items[i].SubItems[6].Text = (valorFinal.ToString("C"));

                }
                
            }

            //Segundo Loop para converter tudo para real
            /*for(int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].SubItems[5].ToString("C");
                Convert.ToDecimal(listView1.Items[i].SubItems[6]).ToString("C");
            }*/
        }

        private void saldoTotalEstoque02()
        {
            double totalValorUnitario = 0.0;
            double totalQuant = 0.0;
            double totalEstoque = 0.0;

            foreach (ListViewItem item in listView1.Items)
            {
                totalValorUnitario = Double.Parse(item.SubItems[5].Text, NumberStyles.Currency);
                totalQuant = Double.Parse(item.SubItems[8].Text, NumberStyles.Currency);

                totalEstoque = (totalValorUnitario * totalQuant);

                string[] linha = {"","","","","","","","", totalEstoque.ToString("C2")};

                var listViewItem = new ListViewItem(linha);

                listView1.Items.Add(listViewItem);

                //listView1.Items.Add(item.SubItems[9].Text = totalEstoque.ToString("C2"));
            }
        }

        private void saldoTotalEstoque()
        {
            double totalValorUnitario   = 0.0;
            double totalQuant           = 0.0;
            double totalEstoque = 0.0;  

            foreach (ListViewItem item in listView1.Items)
            {
                totalValorUnitario  = Double.Parse(item.SubItems[5].Text, NumberStyles.Currency);
                totalQuant          = Double.Parse(item.SubItems[8].Text, NumberStyles.Currency);

                totalEstoque = (totalValorUnitario * totalQuant);

                //this.listView1.Columns[9].Text = totalEstoque.ToString();
                //listView1.Items.Add(item.SubItems[9].Text = totalEstoque.ToString("C2"));
            }

            totalEstoque = (totalValorUnitario * totalQuant);

            //Convert.ToDouble(textBox1.Text).ToString("C");
            
            textBox1.Text = totalEstoque.ToString("C");
            //Convert.ToDouble(textBox1.Text).ToString("C2");
        }

        private void Cadastrar_Load(object sender, EventArgs e)
        {
            //loadListView();
            //this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e) { }

        private void button4_Click(object sender, EventArgs e)
        {

            MySqlConnection Connection = new MySqlConnection(connectionString);

            MySqlCommand cmd;
            
            if (MessageBox.Show("Vc tem certeza ??", "Excluir", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Connection.Open();
                cmd = new MySqlCommand("DELETE FROM PRODUTOS WHERE ID_PRODUTO='" + txtIDProduto.Text + "'", Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Dados Excluidos com Sucesso!!!");
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]); // Atualiza a exclusão
                Connection.Close();
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.Enabled = false;

            txtIDProduto.Text       = listView1.SelectedItems[0].SubItems[0].Text;
            txtCodProduto.Text      = listView1.SelectedItems[0].SubItems[1].Text;
            txtDescProduto.Text     = listView1.SelectedItems[0].SubItems[2].Text;
            txtMarca.Text           = listView1.SelectedItems[0].SubItems[3].Text;
            txtAno.Text             = listView1.SelectedItems[0].SubItems[4].Text;
            txtValorUnitario.Text   = Convert.ToString(listView1.SelectedItems[0].SubItems[5].Text);
            txtValorFinal.Text      = Convert.ToString(listView1.SelectedItems[0].SubItems[6].Text);
            txtClassificacao.Text   = listView1.SelectedItems[0].SubItems[7].Text;
            txtQuantidade.Text      = listView1.SelectedItems[0].SubItems[8].Text;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void txtAno_TextChanged(object sender, EventArgs e) { }

        //Como esse sistema é grande, e fiz na correria, nem tive tempo de comentar ele todo ainda.
        private void button6_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;

            txtIDProduto.Text     = "";
            txtCodProduto.Text    = "";
            txtDescProduto.Text   = "";
            txtMarca.Text         = "";
            txtAno.Text           = "";
            txtValorUnitario.Text = "";
            txtValorFinal.Text    = "";
            txtClassificacao.Text = "";
            txtQuantidade.Text    = "";
        }

        //Como vc pode ver, tem muito codigo sem comentar, como fiz o sistema rápido, cliente tinha pressa, nem deu tempo
        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection Connection = new MySqlConnection(connectionString);

            MySqlCommand cmd;

            string codigoProduto   = txtCodProduto.Text;
            string produto         = txtDescProduto.Text;
            string marca           = txtMarca.Text;
            string ano             = txtAno.Text;
            //RETIRAR DA STRING RS:
            string valor_unitario  = txtValorUnitario.Text;
            string valor_final     = txtValorFinal.Text;

            string classificacao   = txtClassificacao.Text;
            string quantidade      = txtQuantidade.Text;

            if (txtValorUnitario.Text.Contains("R$") || txtValorFinal.Text.Contains("R$"))
            {
                MessageBox.Show("Por Favor Retire o 'R$' ");
                MessageBox.Show("Selecione a o Texto inteiro e Digite o Valor Desejado!!");
            }

            else

            if (codigoProduto == "" || produto == "")
            {
                MessageBox.Show("Por Favor Insira os valores dos campos!!");
            }
            else
            {                
                try
                {
                    Connection.Open();
                    cmd = new MySqlCommand("UPDATE PRODUTOS SET CODIGOPRODUTO='" + txtCodProduto.Text + "',PRODUTO='" + txtDescProduto.Text + "',MARCA='" + txtMarca.Text + "',ANO='" + txtAno.Text + "',VALOR_UNITARIO='" + txtValorUnitario.Text + "',VALOR_FINAL='" + txtValorFinal.Text + "',CLASSIFICACAO='" + txtClassificacao.Text + "',QUANTIDADE='" + txtQuantidade.Text + "' WHERE ID_PRODUTO='" + txtIDProduto.Text + "'", Connection);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Dados alterados com Sucesso");
                    Connection.Close();
                    
                    //Update List
                    listView1.SelectedItems[0].SubItems[1].Text = txtCodProduto.Text;
                    listView1.SelectedItems[0].SubItems[2].Text = txtDescProduto.Text;
                    listView1.SelectedItems[0].SubItems[3].Text = txtMarca.Text;
                    listView1.SelectedItems[0].SubItems[4].Text = txtAno.Text;
                    listView1.SelectedItems[0].SubItems[5].Text = txtValorUnitario.Text;
                    listView1.SelectedItems[0].SubItems[6].Text = txtValorFinal.Text;
                    listView1.SelectedItems[0].SubItems[7].Text = txtClassificacao.Text;
                    listView1.SelectedItems[0].SubItems[8].Text = txtQuantidade.Text;

                    //Clear textbox's
                    txtIDProduto.Text       = "";
                    txtCodProduto.Text      = "";
                    txtDescProduto.Text     = "";
                    txtMarca.Text           = "";
                    txtAno.Text             = "";
                    txtValorUnitario.Text   = "";
                    txtValorFinal.Text      = "";
                    txtClassificacao.Text   = "";
                    txtQuantidade.Text      = "";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aconteceu uma falha no Processo!!");
                    Console.WriteLine(ex);
                }

            }
            
        }

        private void txtValorUnitario_Click(object sender, EventArgs e) { }

        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref txtValorUnitario);
        }

        private void txtValorUnitario_Leave(object sender, EventArgs e) { }

        private void button7_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/17993657/unable-to-connect-to-any-of-the-specified-mysql-hosts-c-sharp-mysql/17994855

            //MySqlConnection Connection = new MySqlConnection(connectionString);
            // MySqlCommand Command;

            //Connection.Open();
            //Atualiza a lista, retornando-a ordenando a ListView
            //Command = new MySqlCommand("SELECT * FROM PRODUTOS", Connection);
            //Command.ExecuteNonQuery();
            
            listView1.Refresh();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Call another form
            Visualizar v = new Visualizar();
            //v.textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            v.textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            v.textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            v.textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            v.textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
            v.textBox6.Text = listView1.SelectedItems[0].SubItems[5].Text;
            v.textBox7.Text = listView1.SelectedItems[0].SubItems[6].Text;
            v.textBox8.Text = listView1.SelectedItems[0].SubItems[7].Text;
            v.textBox9.Text = listView1.SelectedItems[0].SubItems[8].Text;

            v.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pesquisa pesquisa = new Pesquisa();
            pesquisa.Show();
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtDescProduto_TextChanged(object sender, EventArgs e) { }

        private void txtDescProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtValorFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtClassificacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        //Esse cara é interessante, esse codigo aqui, é somente para transformar no REAL BRASILEIRO, olha o tamanho. kkkkkk
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
        //Ele chama o procedimento, que transforma para real, voce vai aprender sobre metodos e funcões em Java futuramente
        private void txtValorFinal_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref txtValorFinal);
        }

        private void calcularSomaValorUnitario()
        {
            double total = 0;

            foreach(ListViewItem item in listView1.Items)
            {
                total += Double.Parse(item.SubItems[5].Text, NumberStyles.Currency);
            }

            //label13.Text = String.Format("{0:C2}", total);
        }

        private void calcularSomaValorFinal()
        {
            double total = 0;

            foreach(ListViewItem item in listView1.Items)
            {
                total += Double.Parse(item.SubItems[6].Text, NumberStyles.Currency);
            }

            //label15.Text = String.Format("{0:C2}", total);
        }
        //Como o nome dos procedimentos dizem, ele realizam calculos
        private void calcularSomaTotalQuant()
        {
            double total = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                total += Double.Parse(item.SubItems[8].Text, NumberStyles.Currency);
            }

            label17.Text = total.ToString();
        }

        //Procedimento que carrega a listview e transfere os dados para a planilha de Excel!!!
        private async void button8_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if(sfd.ShowDialog() ==  DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("ID PRODUTO,COD. PRODUTO,PRODUTO,MARCA,ANO,VALOR UNITÁRIO,VALOR FINAL,CLASSIFICACAO,QUANTIDADE");
                        
                        foreach(ListViewItem item in listView1.Items)
                        {
                            sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text));
                        }
                        await sw.WriteLineAsync(sb.ToString());
                        MessageBox.Show("Seus dados foram exportados com sucesso!!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //Aqui começa a ficar sinistro, tem que usar classe bitmap, printDialog e muitas outras para trabalhar em consonância para fazer a impressão
        //do relatório para o cliente
        Font printFont;
        Bitmap bitmap;

        private void button9_Click(object sender, EventArgs e)
        {
            print();
            //PrintListView(bitmap);
            //printDialog1.Document = printDocument1;
            //printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            //printDocument1.DocumentName = "Print Document";
            //printDialog1.Document = printDocument1;
            //printDialog1.AllowSelection = true;
            //printDialog1.AllowSomePages = true;
            //printDocument1.DefaultPageSettings.Landscape = false;
            //if (printDialog1.ShowDialog() == DialogResult.OK)
            //{
              //  printDocument1.Print();
            //}
        }

        private void PrintListView(Bitmap data)
        {
            PrintDialog   pDlg  = new PrintDialog();
            PrintDocument pDoc  = new PrintDocument();
            pDoc.DocumentName   = "Print Document";
            pDlg.Document       = pDoc;
            //pDlg.AllowSelection = true;
            //pDlg.AllowSomePages = false;

            if (pDlg.ShowDialog() == DialogResult.OK)
            {
                printFont = new Font("Arial", 9);
                pDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                pDoc.DefaultPageSettings.Landscape = true;
                pDoc.Print();
            }
            else
            {
                MessageBox.Show("Cancelado");
            }
        }
        //Esse cara tira uma foto da lista
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            bitmap = new Bitmap(this.listView1.Width, this.listView1.Height);
            listView1.DrawToBitmap(bitmap, this.listView1.ClientRectangle);
            e.Graphics.DrawImage(bitmap, new Point(20, 20));
        }

        /*private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int i = 0;
            int y = e.MarginBounds.Top;
            // i = 1;
            //e.Graphics.DrawString("Lista Completa", new System.Drawing.Font("Times Roman", 10), Brushes.Black, 50, i);
            //i = 1;

            do
            {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        //e.Graphics.DrawString(item.Text, new System.Drawing.Font("Times Roman", 10), Brushes.Black, 10, i);
                        //e.Graphics.DrawString(item.SubItems[1].Text, new System.Drawing.Font("Times Roman", 10), Brushes.Black, 30, i);
                        //e.Graphics.DrawString(item.Text, new System.Drawing.Font("Times Roman", 10), Brushes.Black, 10, i);
                        e.Graphics.DrawString(item.SubItems[8].Text, new System.Drawing.Font("Times Roman", 10), Brushes.Black, 750, i);
                        e.Graphics.DrawString(item.SubItems[2].Text, new System.Drawing.Font("Times Roman", 10), Brushes.Black, 30, i);

                        i += 25; //Espaçamento
                    }
                    e.HasMorePages = true;
                    y = e.MarginBounds.Top;
                    continue;
                
            } while (listView1.Items.Count <= 377);
            e.HasMorePages = false;
        }*/

        //Esse procedimento que realiza a tarefa de impressão do relátorio
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //https://stackoverflow.com/questions/24561611/multiple-page-print-document-in-c-sharp
            try
            {
                Graphics graphics = e.Graphics;
                SolidBrush brush = new SolidBrush(Color.Black);

                Font font = new Font("Courier New", 10);

                float pageWidth = e.PageSettings.PrintableArea.Width;
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
                        
                        e.Graphics.DrawString(listView1.Items[contadorPaginas].SubItems[8].Text, new System.Drawing.Font("Times Roman", 9), Brushes.Black, 750, startX + offsetY);
                        e.Graphics.DrawString(listView1.Items[contadorPaginas].SubItems[2].Text, new System.Drawing.Font("Times Roman", 9), Brushes.Black, 30, startX + offsetY);
                        offsetY += (int)fontHeight;
                        i += 2; //Espaçamento

                        //variavel offsetY lado Y da pagina
                        if (offsetY >= pageHeight)//olha o IF
                        {
                            e.HasMorePages = true;//se tiver mais dados para impressão, libera folhas A4
                            offsetY = 0;
                            return;
                        }
                        else
                        {
                            e.HasMorePages = false; //se já tiver atendido a necessidade dos dados, não precisa mais de folhas A4
                        }

                        contadorPaginas = contadorPaginas + 1; //Conta a quantidade de paginas
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Esse procedimento é chamado para imprimir a Lista
        public void print()
        {
            printDialog1.Document = printDocument1;

            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                PaperSize tamanho = new PaperSize("", 800, 800);
                tamanho.PaperName = PaperKind.Custom.ToString();
                Margins margem = new Margins();
                margem.Left = 0;
                margem.Top = 50;

                contadorPaginas = 0;
                printDocument1.Print();
            }

            
        }
        //Esse procedimento é usado para tirar uma foto da lista
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin  = e.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height /
               printFont.GetHeight(e.Graphics);

            // Print each line of the file.
            while (listView1.Items != null)
            {
               yPos = topMargin + (count *
                   printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        //Coloca o icone no botão
        private void setMyButtonIcon()
        {
            //Assign an image to the button
            button7.Image = Image.FromFile("C:\\Users\\PARTICULAR\\source\\repos\\Controle_Estoque\\Controle_Estoque\\Resources\\atualizacao-do-sistema.png");
            button7.ImageAlign = ContentAlignment.MiddleLeft;
            button7.TextAlign = ContentAlignment.MiddleRight;
        }

        private void txtIDProduto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
