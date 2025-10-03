using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP3_JSC.CLASSE;
using MySqlConnector;

namespace AP3_JSC.ADO
{
    internal class ADOMembre : ADO
    {
        public static void create(Membre membre)
        {
            try
            {
                open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO membre(nom, prenom, fonction) VALUES (@nom, @prenom, @fonction)";
                cmd.Parameters.AddWithValue("@nom", membre.nom);
                cmd.Parameters.AddWithValue("@prenom", membre.prenom);
                cmd.Parameters.AddWithValue("@fonction", membre.fonction);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("intervention créer");
                close();
            }
            catch (MySqlException ec)
            {
                Console.WriteLine(ec.Message);
            }
        }

        public static Membre getOne(int id)
        {
            Membre m = null;
            try
            {
                open();
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM membre where id=@id_membre";
                cmd.Parameters.AddWithValue("@id_membre", id);
                cmd.Prepare();
                m = new Membre(reader.GetString("nom"));
                close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return m;
        }
    }
}
