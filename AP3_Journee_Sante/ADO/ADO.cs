using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AP3_Journee_Sante.ADO
{
    public abstract class ADO
    {
        //Chaîne de connexion à SQL Server
        private string connectionString =

        //"Data Source=ARTHUR;Initial Catalog=AP3_JSC;Integrated Security=True;TrustServerCertificate=True;";
        "Data Source = 127.0.0.1; Initial Catalog = NOVALISM; TrustServerCertificate=True;User ID = novalism_app; Password=*4202!@ms1lav0N"; 

        // Si tu utilises un compte SQL Server au lieu de Windows :
        // "Data Source=localhost;Initial Catalog=MaBase;User ID=MonUtilisateur;Password=MonMotDePasse;"

        //Objet connexion
        public SqlConnection Connection { get; private set; }

        //Méthode pour ouvrir la connexion
        public void Open()
        {
            try
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ouverture de la connexion : " + ex.Message);
                MessageBox.Show("Erreur connexion SQL : " + ex.Message);
            }
        }

        //Méthode pour fermer la connexion
        public void Close()
        {
            try
            {
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la fermeture de la connexion : " + ex.Message);
            }
        }

        //Test de connexion rapide
        public bool TestConnection()
        {
            try
            {
                Open();
                Console.WriteLine("Connexion réussie !");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Échec de la connexion : " + ex.Message);
                return false;
            }
            finally
            {
                Close();
            }
        }

        //// Exemple : exécuter une requête SELECT et renvoyer un DataTable
        //public DataTable ExecuteQuery(string query)
        //{
        //    DataTable table = new DataTable();
        //    try
        //    {
        //        Open();
        //        using (SqlCommand cmd = new SqlCommand(query, Connection))
        //        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //        {
        //            adapter.Fill(table);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erreur lors de l'exécution de la requête : " + ex.Message);
        //    }
        //    finally
        //    {
        //        Close();
        //    }

        //    return table;
        //}

        //// Exemple : exécuter une commande (INSERT, UPDATE, DELETE)
        //public int ExecuteNonQuery(string query)
        //{
        //    int result = 0;
        //    try
        //    {
        //        Open();
        //        using (SqlCommand cmd = new SqlCommand(query, Connection))
        //        {
        //            result = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erreur lors de l'exécution de la commande : " + ex.Message);
        //    }
        //    finally
        //    {
        //        Close();
        //    }

        //    return result;
        //}
    }
}
