using AP3_Journee_Sante.CLASSE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP3_Journee_Sante.ADO
{
    public class AdoMembres : ADO
    {
        public void Create(Membres membre)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO membre (nom, prenom, fonction, fk_association) VALUES (@nom, @prenom, @fonction, @id_asso)",
                    Connection);

                cmd.Parameters.AddWithValue("@nom", membre.Nom);
                cmd.Parameters.AddWithValue("@prenom", membre.Prenom);
                cmd.Parameters.AddWithValue("@fonction", membre.Fonction);
                cmd.Parameters.AddWithValue("@id_asso", membre.IdAsso);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Membre créée");
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

        public List<Membres> GetByAssociation(int idAsso)
        {
            List<Membres> membres = new List<Membres>();
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM membre WHERE id_asso=@id_asso",
                    Connection);

                cmd.Parameters.AddWithValue("@id_asso", idAsso);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Membres membre = new Membres(
                            reader.GetString(reader.GetOrdinal("nom")),
                            reader.GetString(reader.GetOrdinal("prenom")),
                            reader.GetString(reader.GetOrdinal("fonction")),
                            idAsso 
                        );

                        membres.Add(membre);
                    }
                }
            }
            finally
            {
                Close();
            }

            return membres;
        }

        public List<Membres> GetAll()
        {
            List<Membres> membres = new List<Membres>();
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM membre", Connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Membres membre = new Membres(
                            reader.GetString(reader.GetOrdinal("nom")),
                            reader.GetString(reader.GetOrdinal("prenom")),
                            reader.GetString(reader.GetOrdinal("fonction")),
                            reader.GetInt32(reader.GetOrdinal("fk_association"))
                        );
                        membre.Id = reader.GetInt32(reader.GetOrdinal("id_membre")); // 🔥 ICI
                        membres.Add(membre);
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

            return membres;
        }

        public void Update(Membres membre)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE membre SET nom=@nom, prenom=@prenom, fonction=@fonction, id_asso=@id_asso WHERE id_membre=@id_membre",
                    Connection);

                cmd.Parameters.AddWithValue("@nom", membre.Nom);
                cmd.Parameters.AddWithValue("@prenom", membre.Prenom);
                cmd.Parameters.AddWithValue("@fonction", membre.Fonction);
                cmd.Parameters.AddWithValue("@id_asso", membre.IdAsso);
                cmd.Parameters.AddWithValue("@id_membre", membre.Id);

                cmd.ExecuteNonQuery();
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
                    "DELETE FROM membre WHERE id_membre=@id_membre",
                    Connection);

                cmd.Parameters.AddWithValue("@id_membre", id);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Membre supprimée");
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

        public async Task<bool> UpdateAsync(Membres m)
        {
            using (SqlConnection cnx = Connexion.GetConnexion())
            {
                await cnx.OpenAsync();

                string query = @"UPDATE membre SET 
                            nom = @nom,
                            prenom = @prenom,
                            fonction = @fonction,
                            fk_association = @id_asso
                         WHERE id_membre = @id";

                using (SqlCommand cmd = new SqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@nom", m.Nom);
                    cmd.Parameters.AddWithValue("@prenom", m.Prenom);
                    cmd.Parameters.AddWithValue("@fonction", m.Fonction);
                    cmd.Parameters.AddWithValue("@id_asso", m.IdAsso);
                    cmd.Parameters.AddWithValue("@id", m.Id);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

    }
}
