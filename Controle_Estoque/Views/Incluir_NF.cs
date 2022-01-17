using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Controle_Estoque
{
    public partial class Incluir_NF : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";
        //Máquina Éder
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;Port=3306;pwd=Michaeldias03092009;";
        //Recebe os dados vindos do Pesquisar_Produtos
        public static string codigoProduto = "";
        public static string Produto = "";
        public static string Marca = "";
        public static string Ano = "";
        public static string valor_Unitario_Anterior = "";
        public static string valor_Final = "";
        public static string classificacao = "";
        public static string quant = "";

        public Incluir_NF()
        {
            InitializeComponent();
            textBox2.ReadOnly   = true;
            textBox3.ReadOnly   = true;
            textBox4.ReadOnly   = true;
            textBox5.ReadOnly   = true;
            textBox6.ReadOnly   = true;
            textBox7.Enabled    = true;
            textBox8.ReadOnly   = true;
            textBox9.ReadOnly   = true;
            textBox10.ReadOnly  = true;
            textBox11.Enabled   = true;
            textBox13.ReadOnly  = true;
            button1.Select();
            listView2.FullRowSelect = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pesquisar_Produtos_Entrada produtos = new Pesquisar_Produtos_Entrada(this);
            produtos.Show();
        }

        private void Clear()
        {
            //textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Olha o tipo Boolean ai, ele verifica a veracidade do recebimento de dados
        public Boolean receberDados()
        {   
            textBox2.Text = codigoProduto.ToString();
            textBox3.Text = Produto.ToString();
            textBox4.Text = Marca.ToString();
            textBox5.Text = Ano.ToString();
            textBox6.Text = valor_Unitario_Anterior.ToString();
            textBox7.Text = "";
            textBox8.Text = valor_Final.ToString();
            textBox9.Text = classificacao.ToString();
            textBox10.Text = quant.ToString();

            return true;
        }

        private void Incluir_Shown(object sender, EventArgs e) { }

        private void Incluir_Load(object sender, EventArgs e)  { }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Acquire SelectedItems reference
            //var selectedItems = listView2.SelectedItems;

            //if(selectedItems.Count > 0)
            ///{
                //Display text of first item selected
               // this.Text = selectedItems[0].Text;
            //}
           // else
            //{
                //Display default string.
               // this.Text = "Empty";
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Declaro varias variaveis do tipo STRING, iguaal no JAVA. voce está aprendendo
            //Coloco o System.String por capricho, mas é a mesma coisa de String variavel 

            System.String Fornecedor;
            System.String DataNF;
            System.String codProduto;
            System.String Produto;
            System.String Marca;
            System.String Ano;
            System.String valorUnitarioAnterior;
            System.String valorUnitarioAtual;
            System.String valorFinal;
            System.String classificacao;
            System.String quantEstoque;
            System.String quantNF;

            //Variaveis recebem os dados vindos do outro form
            Fornecedor              = textBox1.Text;        //ok
            DataNF                  = dateTimePicker1.Text; //ok
            codProduto              = textBox2.Text;        //ok
            Produto                 = textBox3.Text;        //ok
            Marca                   = textBox4.Text;        //ok
            Ano                     = textBox5.Text;        //ok
            valorUnitarioAnterior   = textBox6.Text;        //ok
            valorUnitarioAtual      = textBox7.Text;        //ok
            valorFinal              = textBox8.Text;        //ok
            classificacao           = textBox9.Text;        //ok
            quantEstoque            = textBox10.Text;       //ok
            quantNF                 = textBox12.Text;       //ok

            if(valorUnitarioAtual == "" && quantNF == "")
            {
                System.Windows.Forms.MessageBox.Show("INFORME VALOR UNITARIO E QUANTIDADE DA NOTA");
                goto Fim;//vou te ensinar esse comando GOTO. Arcaico, só os programadores mais sinistros sabem usar esse comando
            }else if(valorUnitarioAtual == "")
            {
                System.Windows.Forms.MessageBox.Show("INFORME VALOR UNITARIO");
                goto Fim;
            }
            else if(quantNF == "")
            {
                System.Windows.Forms.MessageBox.Show("INFORME A QUANTIDADE DA NOTA");
                goto Fim;
            }

            else
            {
                //Que lindo essa função
                decimal media = mediaValorUnitario(Convert.ToDecimal(valorUnitarioAnterior), Convert.ToDecimal(valorUnitarioAtual));
                decimal soma = somaTotal(Convert.ToDecimal(quantEstoque), Convert.ToDecimal(quantNF));

                //Row
                String[] row = { codProduto, Produto, Marca, Ano, Convert.ToDouble(valorUnitarioAnterior).ToString("C"), Convert.ToDouble(valorUnitarioAtual).ToString("C"), media.ToString(), valorFinal, classificacao, quantEstoque, quantNF, soma.ToString() };

                ListViewItem item = new ListViewItem(row);

                listView2.Items.Add(item);

                clear();

            }

        Fim:
            Console.WriteLine("OBRIGATORIO");
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            listView2.LabelEdit = true;

            ListViewHitTestInfo hit = listView2.HitTest(e.Location);
            //MessageBox.Show(listView2.SelectedItems[0].SubItems[0].Text);

            if(listView2.SelectedItems.Count > 0)
            {
                if(hit.SubItem.Text == null)
                {
                    MessageBox.Show("Insira seu dado aqui!");
                }

                MessageBox.Show("Item:: " + hit.SubItem.Text);
            }
            else
            {
                MessageBox.Show("Por Favor Selecione um Item");
            }
            
        }

        private decimal mediaValorUnitario(decimal valorAnterior, decimal valorAtual)
        {
            decimal media = 0;

            media = (valorAnterior + valorAtual) / 2;

            return media;
        }

        private void button6_Click(object sender, EventArgs e) { }

        private void button8_Click(object sender, EventArgs e)
        {
            if(listView2.SelectedItems.Count ==  0) //Resolve o bug de negligencia do usuario em informar o dado a ser exonerado
            {
                MessageBox.Show("Por favor selecione um campo!!!");
            }

            else


            if(MessageBox.Show("Voce tem Certeza", "Excluir", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                listView2.Items.Remove(listView2.SelectedItems[0]);
            }

            //Chama o metodo Clear();
            clear();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            listView2.LabelEdit = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            totalSomaProdutosEstoque_NFChegada();
        }

        public void totalSomaProdutosEstoque_NFChegada() //Soma a quantidade inserida linha por linha
        {
            decimal valorTotalQuantEstoque = 0;
            decimal valorTotalNF = 0;
            decimal total = 0;

            for(int i = 0; i < listView2.Items.Count; i++)
            {
                for(int j = 0; j < listView2.Items.Count; j++)
                {
                    valorTotalQuantEstoque = decimal.Parse(listView2.Items[i].SubItems[9].Text);
                    valorTotalNF = decimal.Parse(listView2.Items[j].SubItems[10].Text);
                }
            }

            total = (valorTotalQuantEstoque + valorTotalNF);

            textBox11.Text = total.ToString();
        }

        public void somaTotalLinha()
        {
            decimal valorTotalUnitario = 0;

            for(int i = 0; i < listView2.Items.Count; i++)
            {
                valorTotalUnitario += decimal.Parse(listView2.Items[i].SubItems[9].Text);
            }

            MessageBox.Show(valorTotalUnitario.ToString());
            textBox11.Text = valorTotalUnitario.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void clear()
        {
            //textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox12.Text = "";
        }

        private void mediaMonetaria()
        {
            decimal valorUnitarioAnterior = 0;
            decimal valorUnitarioAtual = 0;
            decimal total = 0;

            for (int i = 0; i < listView2.Items.Count; i++)
            {
                for (int j = 0; j < listView2.Items.Count; j++)
                {
                    valorUnitarioAnterior = decimal.Parse(listView2.Items[i].SubItems[6].Text);
                    valorUnitarioAtual    = decimal.Parse(listView2.Items[j].SubItems[7].Text);
                }
            }

            total = (valorUnitarioAnterior + valorUnitarioAtual) / 2;

            textBox11.Text = total.ToString();
        }

        private decimal somaTotal(decimal valor01, decimal valor02)
        {
            decimal somaTotal = 0;

            somaTotal = (valor01 + valor02);

            return somaTotal;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mediaMonetaria();
        }

        private void textBox10_TextChanged(object sender, EventArgs e) { }

        private void button7_Click(object sender, EventArgs e)
        {
            //Informaçoes do Fornecedor
            System.String Fornecedor    = textBox1.Text;
            System.String DataCadastro  = dateTimePicker1.Text;
            System.String TotalNF       = textBox11.Text;

            if (Fornecedor == "" && TotalNF == "")
            {
                MessageBox.Show("POR FAVOR INFORME O  FORNECEDOR E VALOR TOTAL DA NOTA");
                goto Finalizar;
            }

            else if (Fornecedor == "")
            {
                MessageBox.Show("POR FAVOR INFORME O FORNECEDOR");
                goto Finalizar;
            }

            else if (TotalNF == "")
            {
                MessageBox.Show("POR FAVOR INFORME O VALOR TOTAL DA NOTA");
                goto Finalizar;
            }
            
            else if(listView2.Items.Count == 0)
            {
                MessageBox.Show("POR FAVOR INSIRA DADOS NA LISTA");
                goto Finalizar;
            }
                

            //Variaveis que irão compor os itens da Nota Fiscal
            System.String codigo;
            System.String produto;
            System.String marca;    
            System.String ano;
            System.String mediaValores;
            System.String valorFinal;
            System.String classificacao;
            System.String totalItem;

            System.String Quant_NF;

            getID_FORNECEDOR();

            try//tente
            {
                DialogResult resultado = MessageBox.Show("Salvar esta Nota ?", "Salvar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    MySqlConnection conexao = new MySqlConnection(connectionString);
                    conexao.Open();

                    MySqlCommand commando = conexao.CreateCommand();
                    commando.Parameters.AddWithValue("@ID_FORNECEDOR", textBox13.Text);
                    commando.Parameters.AddWithValue("@FORNECEDOR",        Fornecedor);
                    commando.Parameters.AddWithValue("@DATA",            DataCadastro);
                    commando.Parameters.AddWithValue("@VALOR_TOTAL_NOTA",     TotalNF);

                    commando.CommandText = "INSERT INTO FORNECEDOR(ID_FORNECEDOR, FORNECEDOR, DATA, VALOR_TOTAL_NOTA) VALUES(@ID_FORNECEDOR, @FORNECEDOR, @DATA, @VALOR_TOTAL_NOTA)";

                    if (commando.ExecuteNonQuery() > 0)
                        //MessageBox.Show("Sucesso!!");
                        Console.WriteLine("Sucesso!!");
                    else
                        //MessageBox.Show("Deu ruim!!");
                        Console.WriteLine("Deu ruim!!");
                    conexao.Close();

                }
                { 
                    MySqlConnection conexaoItens = new MySqlConnection(connectionString);
                    conexaoItens.Open();

                    //Percorrer a Lista inteira
                    foreach (ListViewItem item in listView2.Items)
                    {
                        codigo          = Convert.ToString(item.SubItems[0].Text);
                        produto         = Convert.ToString(item.SubItems[1].Text);
                        marca           = Convert.ToString(item.SubItems[2].Text);
                        ano             = Convert.ToString(item.SubItems[3].Text);
                        mediaValores    = Convert.ToString(item.SubItems[6].Text);
                        valorFinal      = Convert.ToString(item.SubItems[7].Text);
                        classificacao   = Convert.ToString(item.SubItems[8].Text);
                        Quant_NF        = Convert.ToString(item.SubItems[10].Text);
                        totalItem       = Convert.ToString(item.SubItems[11].Text);

                        MySqlCommand comando = conexaoItens.CreateCommand();

                        //comando.Parameters.AddWithValue("@ID_ITENS", textBox13.Text);
                        comando.Parameters.AddWithValue("@CODIGOPRODUTO",        codigo);
                        comando.Parameters.AddWithValue("@PRODUTO",             produto);
                        comando.Parameters.AddWithValue("@MARCA",                 marca);
                        comando.Parameters.AddWithValue("@ANO",                     ano);
                        comando.Parameters.AddWithValue("@VALOR_UNITARIO", mediaValores);
                        comando.Parameters.AddWithValue("@VALOR_FINAL",      valorFinal);
                        comando.Parameters.AddWithValue("@CLASSIFICACAO", classificacao);
                        comando.Parameters.AddWithValue("@QUANTIDADE",         Quant_NF);
                        comando.Parameters.AddWithValue("@ID_FORNECEDOR",textBox13.Text);  

                        comando.CommandText = "INSERT INTO ITENS_NOTA_ENTRADA(CODIGOPRODUTO, PRODUTO, MARCA, ANO, VALOR_UNITARIO, VALOR_FINAL, CLASSIFICACAO, QUANTIDADE, ID_FORNECEDOR) VALUES (@CODIGOPRODUTO, @PRODUTO, @MARCA, @ANO, @VALOR_UNITARIO, @VALOR_FINAL, @CLASSIFICACAO, @QUANTIDADE, @ID_FORNECEDOR)";

                        //Faz a verificação dos dados no Database para ser feito a alteração necessária
                        UpdateDatabaseProdutos p = new UpdateDatabaseProdutos();
                        p.UpdateSomaSaldoProdutos(codigo, mediaValores, totalItem);

                        if (comando.ExecuteNonQuery() > 0)

                            //MessageBox.Show("Sucesso!!");
                            Console.WriteLine("Sucesso!!");
                        else
                            //MessageBox.Show("Deu ruim!!");
                            Console.WriteLine("Deu ruim!!");
                    }
                    conexaoItens.Close();
                }

                listView2.Items.Clear();
                textBox1.Text = "";
                textBox11.Text = "";

            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        Finalizar:
            Console.WriteLine("OBRIGATORIO OS DADOS DO FORNECEDOR!!");
            //MessageBox.Show("OBRIGATORIO OS DADOS DO FORNECEDOR!!");
        }

        private void getID_FORNECEDOR()
        {
            MySqlConnection conexao = new MySqlConnection(connectionString);
            string proid;
            string querie = "SELECT ID_FORNECEDOR FROM FORNECEDOR ORDER BY ID_FORNECEDOR DESC";
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand(querie, conexao);
            MySqlDataReader dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if(Convert.IsDBNull(dr))
            {
                proid = ("00001");
            }
            else
            {
                proid = ("00001");
            }

            conexao.Close();

            textBox13.Text = proid.ToString();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create), Encoding.UTF8))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Codigo,Produto,Marca,Ano,Valor Unit. Anterior,Valor Unit. Atual,Media dos Valores,Valor Final, Classificação, Quant/Estoque, Quant/Nota, Soma");

                        foreach (ListViewItem item in listView2.Items)
                        {
                            sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text, item.SubItems[9].Text, item.SubItems[10].Text));
                        }
                        await sw.WriteLineAsync(sb.ToString());
                        MessageBox.Show("Seus dados foram exportados com sucesso!!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref textBox7);
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
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref textBox11);
        }
    }
}
