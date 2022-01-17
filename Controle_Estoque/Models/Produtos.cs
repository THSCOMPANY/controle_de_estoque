using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Models
{
    //Embedding DLLs in a compiled executable
    //https://stackoverflow.com/questions/189549/embedding-dlls-in-a-compiled-executable
    class Produtos
    {
        //Declaracao de variaveis sempre primeiro, como fiz aqui
        System.String codigoProduto;
        System.String produto;
        System.String marca;
        System.String ano;
        System.String valor_Unitario;
        System.String valor_Final;
        System.String classificacao;
        System.String quantidade;

        //Instance class
        Drive drive = new Drive();
        Diretorio_Files diretorio = new Diretorio_Files();
        Arquivo arq = new Arquivo();
        Sistema sistema = new Sistema();

        public Produtos() { }

        public Produtos(string codigoProduto, string produto, string marca, string ano, string valor_Unitario, string valor_Final, string classificacao, string quantidade)
        {
            this.codigoProduto  = codigoProduto;
            this.produto        = produto;
            this.marca          = marca;
            this.ano            = ano;
            this.valor_Unitario = valor_Unitario;
            this.valor_Final    = valor_Final;
            this.classificacao  = classificacao;
            this.quantidade     = quantidade;

            diretorio.CreateDirectory(); //Create Directory Project Controle Estoque
            drive.verifyDriver();        //Verify Driver System
            arq.AppendTextFile(codigoProduto, produto, marca, ano, valor_Unitario, valor_Final, classificacao, quantidade); //Create File
            sistema.VerificarSistema();
        }

        //Maneira Exclusiva da linguagem
        public string CodigoProduto
        {
            get { return codigoProduto;  }
            set { codigoProduto = value; }
        }

        public string Produto
        {
            get { return produto; }
            set { produto = value; }
        }

        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        public string Valor_Unitario
        {
            get { return valor_Unitario; }
            set { valor_Unitario = value; }
        }

        public string Valor_Final
        {
            get { return valor_Final; }
            set { valor_Final = value; }
        }

        public string Classificacao
        {
            get { return classificacao; }
            set { classificacao = value; }
        }

        public string Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public String toString()
        {
            return codigoProduto + produto + marca + valor_Unitario + valor_Final + classificacao + quantidade;
        }
    }
}
