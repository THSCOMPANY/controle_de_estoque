using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle_Estoque
{
    class DatabaseMySqlWorkbench
    {
        //Atributos da conexão com o MySQL Workbench
        private static MySqlConnection connection;
        private static MySqlCommand    cmd = null;
        private static MySqlDataAdapter sda;

        public void EstabilishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server =   "127.0.0.1";
                builder.UserID =   "root";
                builder.Password = "admin";
                builder.Database = "controle_estoque";
                builder.SslMode = MySqlSslMode.None;
                connection = new MySqlConnection(builder.ToString());
                MessageBox.Show("Database connection successfull!!");
            }
            catch(MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void connectingInserValues()
        {

        }
    }
}
