using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace AP3_JSC.ADO
{
    internal class ADO
    {
        protected static MySqlConnection connection;

        protected static void open()
        {
            string cs = @"server=localhost;userid=root;password=;database=AP3SERVER";
            try
            {
                connection = new MySqlConnection(cs);
                Console.WriteLine("Connexion ouverte");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected static void close()
        {
            if (connection != null)
            {
                connection.Close();
                Console.WriteLine("Connexion fermée");
            }
        }
    }
}
