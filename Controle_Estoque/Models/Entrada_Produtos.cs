using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Models
{
    sealed class Entrada_Produtos : Produtos //Classe Entrada produtos Herda todos os metodos publicos da classe Produto
    {//Derivo a classe Entrada_Produtos classe Mãe, mas não permito que outra classe Derive dela 

        public void Teste() //os itens
        {
            Entrada_Produtos entrada = new Entrada_Produtos();
          
        }

        private void teste01()
        {

        }

        protected void teste02()
        {

        }

        internal void teste03()
        {

        }

        protected internal void teste04()
        {

        }
    }
    
}
