using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Controle_Estoque.Models
{
    class ArrayListProdutos
    {
        ArrayList arrayList = new ArrayList();

        List<Produtos> produtos = new List<Produtos>();

        public void addProdutos(string codigoProduto, string produto, string marca, string ano, string valor_Unitario, string valor_Final, string classificacao, string quantidade)
        {

            Produtos p = new Produtos(codigoProduto, produto, marca, ano, valor_Unitario, valor_Final, classificacao, quantidade);

            produtos.Add(p);

            arrayList.Add(p);

            //mostra os dados
            mostrarDados();
        }

        internal void mostrarDados()
        {
            foreach(object dados in arrayList)
            {
                Console.WriteLine(dados.ToString());
            }
        }
        
    }
}
