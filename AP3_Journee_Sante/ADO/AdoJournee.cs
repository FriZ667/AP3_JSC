using AP3_Journee_Sante.CLASSE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AP3_Journee_Sante.ADO
{
    public class AdoJournee : ADO
    {
        public void Create(Journee journee)
        {
            try
            {
                Open(); // ouvre la connexion

                // Vérifie si la connexion est ouverte
                if (Connection == null || Connection.State != System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("Impossible de se connecter à la base de données.");
                    return;
                }

                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO journee (date_journee, lieu_journee) VALUES (@date_journee, @lieu_journee)",
                    Connection))
                {
                    cmd.Parameters.AddWithValue("@date_journee", journee.Date);
                    cmd.Parameters.AddWithValue("@lieu_journee", journee.Lieu);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Journée créée avec succès !");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erreur SQL : " + ex.Message);
            }
            finally
            {
                Close(); // ferme la connexion
            }
        }


        public Journee GetOne(int id)
        {
            Journee journee = null;
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM journee WHERE id_journee=@id_journee",
                    Connection);
                cmd.Parameters.AddWithValue("@id_journee", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        journee = new Journee(
                            
                            reader.GetDateTime(reader.GetOrdinal("date_journee")),
                            reader.GetString(reader.GetOrdinal("lieu_journee"))
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erreur SQL : " + ex.Message);
            }
            finally
            {
                Close();
            }

            return journee;
        }

        public List<Journee> GetAll()
        {
            List<Journee> journees = new List<Journee>();
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM journee", Connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Journee journee = new Journee(
                            reader.GetInt32(reader.GetOrdinal("id_journee")),
                            reader.GetDateTime(reader.GetOrdinal("date_journee")),
                            reader.GetString(reader.GetOrdinal("lieu_journee"))
                        );
                        journees.Add(journee);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erreur SQL : " + ex.Message);
            }
            finally
            {
                Close();
            }

            return journees;
        }

        public void Update(Journee journee)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE journee SET date_journee=@date_journee, lieu_journee=@lieu_journee WHERE id_journee=@id_journee",
                    Connection);

                cmd.Parameters.AddWithValue("@date_journee", journee.Date);
                cmd.Parameters.AddWithValue("@lieu_journee", journee.Lieu);
                cmd.Parameters.AddWithValue("@id_journee", journee.Id);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Journée mise à jour");
            }
            catch (SqlException ec)
            {
                Console.WriteLine("Erreur SQL : " + ec.Message);
            }
            finally
            {
                Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM journee WHERE id_journee=@id_journee",
                    Connection);

                cmd.Parameters.AddWithValue("@id_journee", id);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Journée supprimée");
            }
            catch (SqlException ec)
            {
                Console.WriteLine("Erreur SQL : " + ec.Message);
            }
            finally
            {
                Close();
            }
        }


    }
}
