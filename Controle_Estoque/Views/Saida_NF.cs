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
    public partial class Saida_NF : Form
    {
        //String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;pwd=Michaeldias03092009;";
        System.String connectionString = "Server=127.0.0.1;Database=controle_estoque;Uid=root;";
        
        //Recebe os dados vindos do Pesquisar_Produtos
        public static string codigoProduto  = "";
        public static string produto        = "";
        public static string marca          = "";
        public static string ano            = "";
        public static string valorUnitario  = "";
        public static string valorFinal     = "";
        public static string classificacao  = "";
        public static string quant          = "";
        
        public Saida_NF()
        {
            InitializeComponent();
            textBox1.ReadOnly  = true;
            textBox4.ReadOnly  = true;
            textBox5.ReadOnly  = true;
            textBox6.ReadOnly  = true;
            textBox7.ReadOnly  = true;
            textBox8.ReadOnly  = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
            //textBox2.Select();
            button3.Select();
            listView1.FullRowSelect = true; //SELECIONA A LINHA
        }
       
        private void button1_Click(object sender, EventArgs e) { }

        private void Saida_NF_Load(object sender, EventArgs e) { }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Declaração das variaveis que irão compor os itens da nota de Saida
            System.String codigoProduto;
            System.String produto;
            System.String marca;
            System.String ano;
            System.String valorUnitario;
            System.String valorFinal;
            System.String classificacao;
            System.String quantEstoque;
            System.String quantNF;

            codigoProduto = textBox4.Text;
            produto       = textBox5.Text;
            marca         = textBox6.Text;
            ano           = textBox7.Text;
            valorUnitario = textBox8.Text;
            valorFinal    = textBox10.Text;
            classificacao = textBox11.Text;
            quantEstoque  = textBox12.Text;
            quantNF       = textBox13.Text;

            if(codigoProduto == "" && produto == "" && marca == "" && ano == "" && valorUnitario == "" && valorFinal == "" && classificacao == "" && quantEstoque == "" && quantNF == "")
            {
                System.Windows.Forms.MessageBox.Show("Por Favor Selecione um Item!!");

                goto Fim;
            }

            //Remove o "R$" para fazer o calculo do Lucro por Peça
            System.String valorUnit = ""; // = valorUnitario.Substring(3);
            System.String valorFin  = "";

            //Só vai entrar aqui, se conter o valor monetário
            if (valorFinal.Contains("R$"))
            {
                valorFin = valorFinal.Substring(3);
            }

            if (valorUnitario.Contains("R$"))
            {
                valorUnit = valorUnitario.Substring(3);
            }

            if(!valorFinal.Contains("R$"))
            {
                valorFin = valorFinal;
            }

            if(!valorUnitario.Contains("R$"))
            {
                valorUnit = valorUnitario;
            }

            //Calcula a diferença dos valores
            decimal lucroPeca = (Convert.ToDecimal(valorUnit) - Convert.ToDecimal(valorFin));

            decimal saidaLucro = 0;
            //verifica se o valor unitario e maior que o valor de venda, caso seja VERDADEIRO, seta para Negativo
            if (Convert.ToDecimal(valorUnit) > Convert.ToDecimal(valorFin))
            {
                //converte para negativo
                decimal perda = System.Math.Abs(lucroPeca) * (-1);

                saidaLucro = perda;
            }
            //verifica se o valor unitario e maior que o valor de venda, caso seja VERDADEIRO, seta para Positivo
            else if (Convert.ToDecimal(valorUnit) < Convert.ToDecimal(valorFin))
            {
                decimal lucroPositivo = Math.Abs(lucroPeca);

                saidaLucro = lucroPositivo;
            }
            //Calcula o Lucro dado, pela entrada
            saidaLucro *= Convert.ToDecimal(quantNF);

            if (quantNF == "")
            {
                System.Windows.Forms.MessageBox.Show("INFORME A QUANTIDADE DA NOTA");
                goto Fim;
            }
            else
            {
                //Calcula a diferença do estoque
                decimal subtracao = calcularResto(Convert.ToDecimal(quantEstoque),Convert.ToDecimal(quantNF));

                //Cria uma linha de dados
                String[] row = { codigoProduto, produto, marca, ano, valorUnitario, valorFinal, classificacao, quantEstoque, quantNF, subtracao.ToString(), saidaLucro.ToString("C") };

                //Cria um item no ListView
                ListViewItem item = new ListViewItem(row);

                //Adiciona a linha de dados no Listview
                listView1.Items.Add(item);

                //Limpa os campos de texto
                clear();
            }

        Fim:
            Console.WriteLine("CAMPOS OBRIGATÓRIOS!!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pesquisa_Produtos_Saida pesquisa = new Pesquisa_Produtos_Saida(this);
            pesquisa.Show();
        }

        public Boolean receberDados()
        {
            this.textBox4.Text  = codigoProduto.ToString();
            this.textBox5.Text  = produto.ToString();
            this.textBox6.Text  = marca.ToString();
            this.textBox7.Text  = ano.ToString();
            this.textBox8.Text  = valorUnitario.ToString();
            this.textBox10.Text = valorFinal.ToString();
            this.textBox11.Text = classificacao.ToString();
            this.textBox12.Text = quant.ToString();

            return true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Declaracao das variaveis que irão receber os dados do Cliente
            System.String Cliente = textBox2.Text;
            System.String DataNF  = dateTimePicker1.Text;
            System.String TotalNF = textBox3.Text;

            if (textBox2.Text == "" && textBox3.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("POR FAVOR INFORME OS CAMPOS OBRIGATORIOS [ CLIENTE E TOTAL DA NF ]!!!");
                goto Fim;
            }
            else if (textBox2.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("POR FAVOR INFORME OS CAMPOS OBRIGATORIOS [ CLIENTE ]!!!");
                goto Fim;
            }
            else if (textBox3.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("POR FAVOR INFORME OS CAMPOS OBRIGATORIOS [ TOTAL DA NF ]!!!");
                goto Fim;
            }

            else if (listView1.Items.Count == 0)
            {
                MessageBox.Show("POR FAVOR INSIRA DADOS NA LISTA");
                goto Fim;
            }

            //A partir daqui entra a lógica do cadastro
            //Variaveis que irão compor os itens da Nota Fiscal
            System.String codigo;
            System.String produto;
            System.String marca;
            System.String ano;
            System.String valorUnitario;
            System.String valorFinal;
            System.String classificacao;
            System.String diferencaSaldo;
            System.String Quant_NF;

            getID_Cliente(); //Geração de ID de Forma automática

            //Calcula o Lucro Total por Nota
            System.Decimal lucroTotal = calculaSomaTotalLucro();

            try
            {
                DialogResult resultado = MessageBox.Show("Salvar esta Nota ?", "Salvar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(resultado == DialogResult.Yes)
                {
                    //Abre conexão
                    MySqlConnection conexao = new MySqlConnection(connectionString);
                    conexao.Open();

                    MySqlCommand comando = conexao.CreateCommand();

                    comando.Parameters.AddWithValue("@ID_CLIENTE",   textBox1.Text);
                    comando.Parameters.AddWithValue("@CLIENTE",            Cliente);
                    comando.Parameters.AddWithValue("@DATA",                DataNF);
                    comando.Parameters.AddWithValue("@VALOR_TOTAL_NOTA",   TotalNF);
                    comando.Parameters.AddWithValue("@LUCRO",           lucroTotal);

                    comando.CommandText = "INSERT INTO CLIENTE(ID_CLIENTE, CLIENTE, DATA, VALOR_TOTAL_NOTA, LUCRO) VALUES(@ID_CLIENTE, @CLIENTE, @DATA, @VALOR_TOTAL_NOTA, @LUCRO)";

                    if (comando.ExecuteNonQuery() > 0)
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

                    //ESSE LOOP SÓ CAPTURA OS ITENS DA NOTA
                    //Percorrer a Lista inteira
                    foreach (ListViewItem item in listView1.Items)
                    {
                        codigo          = Convert.ToString(item.SubItems[0].Text);
                        produto         = Convert.ToString(item.SubItems[1].Text);
                        marca           = Convert.ToString(item.SubItems[2].Text);
                        ano             = Convert.ToString(item.SubItems[3].Text);
                        valorUnitario   = Convert.ToString(item.SubItems[4].Text);
                        valorFinal      = Convert.ToString(item.SubItems[5].Text);
                        classificacao   = Convert.ToString(item.SubItems[6].Text);
                        Quant_NF        = Convert.ToString(item.SubItems[8].Text);
                        diferencaSaldo  = Convert.ToString(item.SubItems[9].Text);

                        MySqlCommand comando = conexaoItens.CreateCommand();

                        comando.Parameters.AddWithValue("@CODIGOPRODUTO",         codigo);
                        comando.Parameters.AddWithValue("@PRODUTO",              produto);
                        comando.Parameters.AddWithValue("@MARCA",                  marca);
                        comando.Parameters.AddWithValue("@ANO",                      ano);
                        comando.Parameters.AddWithValue("@VALOR_UNITARIO", valorUnitario);
                        comando.Parameters.AddWithValue("@VALOR_FINAL",       valorFinal);
                        comando.Parameters.AddWithValue("@CLASSIFICACAO",  classificacao);
                        comando.Parameters.AddWithValue("@QUANTIDADE",          Quant_NF);
                        comando.Parameters.AddWithValue("@ID_CLIENTE",     textBox1.Text);

                        comando.CommandText = "INSERT INTO ITENS_NOTA_SAIDA(CODIGOPRODUTO, PRODUTO, MARCA, ANO, VALOR_UNITARIO, VALOR_FINAL, CLASSIFICACAO, QUANTIDADE, ID_CLIENTE) VALUES (@CODIGOPRODUTO, @PRODUTO, @MARCA, @ANO, @VALOR_UNITARIO, @VALOR_FINAL, @CLASSIFICACAO, @QUANTIDADE, @ID_CLIENTE)";

                        //Faz a verificação dos dados no Database para ser feito a alteração necessária
                        UpdateDatabaseProdutos p = new UpdateDatabaseProdutos();
                        p.UpdateSubtrairSaldoProdutos(codigo, diferencaSaldo);

                        if (comando.ExecuteNonQuery() > 0)

                            //MessageBox.Show("Sucesso!!");
                            Console.WriteLine("Sucesso!!");
                        else
                            //MessageBox.Show("Deu ruim!!");
                            Console.WriteLine("Deu ruim!!");
                        
                    }

                    conexaoItens.Close();
                }
                listView1.Items.Clear();
                textBox2.Text = "";
                textBox3.Text = "";
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        Fim:
            Console.WriteLine("DADOS OBRIGATORIOS");
        }

        private void clear()
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
        }

        private decimal calcularResto(decimal saldoAtual, decimal saldoSaida)
        {
            decimal resto = 0;

            resto = (saldoAtual - saldoSaida);

            return resto;
        }

        private void getID_Cliente()
        {
            MySqlConnection conexao = new MySqlConnection(connectionString);
            string proid;
            string querie = "SELECT ID_CLIENTE FROM CLIENTE ORDER BY ID_CLIENTE DESC";
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand(querie, conexao);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                proid = id.ToString("00000");
            }
            else if (Convert.IsDBNull(dr))
            {
                proid = ("00001");
            }
            else
            {
                proid = ("00001");
            }

            conexao.Close();

            textBox1.Text = proid.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) //Resolve o bug de negligencia do usuario em informar o dado a ser exonerado
            {
                MessageBox.Show("Por favor selecione um campo!!!");
            }

            else


            if (MessageBox.Show("Voce tem Certeza", "Excluir", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }

            //Chama o metodo Clear();
            clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Saida_NF_Shown(object sender, EventArgs e){ }

        private void Saida_NF_VisibleChanged(object sender, EventArgs e) { }

        private void Saida_NF_DragEnter(object sender, DragEventArgs e) { }

        private void Saida_NF_Deactivate(object sender, EventArgs e) { }

        private void Saida_NF_Activated(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref textBox3);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            conversaoMoedaBrasileira(ref textBox10);
        }

        System.String retiraMonetario01 = "";
        System.String retiraMonetario02 = "";

        private decimal calculaSomaTotalLucro()
        {
            //Preciso criar uma variavel valorAcumuladoPositivo, para receber os valores Positivos
            //outra para receber o valorAcumuladoNegativo, para receber os valores de perda
            //calcular a diferença de ambos, para se então, o Lucro Real

            decimal valorAcumuladoNegativo = 0;
            decimal valorAcumuladoPositivo = 0;

            decimal totalLucroReal = 0;

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                retiraMonetario01 = listView1.Items[i].SubItems[10].Text;

                //Verifica se tem valor monetário Negativo
                if (retiraMonetario01.StartsWith("-R$"))
                {
                    //System.Windows.Forms.MessageBox.Show("Tem R$");
                    retiraMonetario02 = retiraMonetario01.Substring(3);
                    valorAcumuladoNegativo += Convert.ToDecimal(retiraMonetario02);
                }
                else
                {
                    retiraMonetario02 = retiraMonetario01.Substring(3);
                    valorAcumuladoPositivo += Convert.ToDecimal(retiraMonetario02);
                }

                //retiraMonetario02 = retiraMonetario01.Substring(3);

                //valorAcumulado += Convert.ToDecimal(retiraMonetario02);

                //valorAcumulado += decimal.Parse(listView1.Items[i].SubItems[10].Text.ToString());
                //listView1.Items[i].SubItems[9].Text.Remove(1, 3);
                //valorAcumulado += decimal.Parse(listView1.Items[i].SubItems[9].Text.Remove(1,3).ToString());

                //Subtrai o valor Negativo do Positivo e retorna o valor Real de Lucro
                
            }

            totalLucroReal = (valorAcumuladoNegativo - valorAcumuladoPositivo);

            //decimal lucroPositivo = Math.Abs(lucroPeca); setar o valor de retorno para positivo, pois, já foi subtraido a diferença

            //if (totalLucro < 0)
            //{
            //converte para Negativo
            //  totalLucroReal = System.Math.Abs(totalLucro) * (-1);
            //}
            //else
            //{
            //converte para Positivo
            //  totalLucroReal = System.Math.Abs(totalLucro);
            //}

            return Math.Abs(totalLucroReal);
 
        }
    }
}
