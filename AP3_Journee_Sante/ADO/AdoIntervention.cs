using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AP3_Journee_Sante.CLASSE;

namespace AP3_Journee_Sante.ADO
{
    internal class AdoIntervention
    {
        // CREATE
        public static bool Create(Intervention t)
        {
            SqlConnection cnx = Connexion.GetConnexion();

            try
            {
                cnx.Open();

                string query = @"INSERT INTO intervention 
                (structure, intervenants, description, titre, salle_besoin, disposition_salle,
                commentaire, materiel, pause, repas, cout, type_pause, coordonnees, date_intervention) 
                VALUES 
                (@structure, @intervenants, @description, @titre, @salle_besoin, 
                @disposition_salle, @commentaire, @materiel, @pause, @repas, @cout, 
                @type_pause, @coordonnees, @date_intervention)";

                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@structure", t.GetStructureAsso());
                cmd.Parameters.AddWithValue("@intervenants", t.GetMembresAsso());
                cmd.Parameters.AddWithValue("@description", t.GetDescription());
                cmd.Parameters.AddWithValue("@titre", t.GetTitre());
                cmd.Parameters.AddWithValue("@salle_besoin", t.GetSalleBesoin());
                cmd.Parameters.AddWithValue("@disposition_salle", t.GetSalleDisposition());
                cmd.Parameters.AddWithValue("@commentaire", t.GetCommentaire());
                cmd.Parameters.AddWithValue("@materiel", t.GetMateriel());
                cmd.Parameters.AddWithValue("@pause", t.GetPause());
                cmd.Parameters.AddWithValue("@repas", t.GetRepas());
                cmd.Parameters.AddWithValue("@cout", t.GetCout());
                cmd.Parameters.AddWithValue("@type_pause", t.GetTypePause());
                cmd.Parameters.AddWithValue("@coordonnees", t.GetCooordonneesAsso());
                cmd.Parameters.AddWithValue("@date_intervention", t.GetDateIntervention());

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                cnx.Close();
            }
        }

        // READ
        public static List<Intervention> ReadAll()
        {
            List<Intervention> liste = new List<Intervention>();
            SqlConnection cnx = Connexion.GetConnexion();

            try
            {
                cnx.Open();
                string query = "SELECT * FROM intervention";
                SqlCommand cmd = new SqlCommand(query, cnx);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Intervention i = new Intervention(
                        reader["structure"].ToString(),
                        reader["intervenants"].ToString(),
                        reader["description"].ToString(),
                        reader["titre"].ToString(),
                        reader["salle_besoin"].ToString(),
                        reader["disposition_salle"].ToString(),
                        reader["commentaire"].ToString(),
                        reader["materiel"].ToString(),
                        Convert.ToInt32(reader["pause"]),
                        Convert.ToInt32(reader["repas"]),
                        Convert.ToInt32(reader["cout"]),
                        Convert.ToInt32(reader["type_pause"]),
                        Convert.ToInt32(reader["coordonnees"]),
                        Convert.ToDateTime(reader["date_intervention"])
                    );
                    liste.Add(i);
                }
            }
            catch
            {
                return new List<Intervention>();
            }
            finally
            {
                cnx.Close();
            }

            return liste;
        }

        // UPDATE
        public static bool Update(Intervention t)
        {
            SqlConnection cnx = Connexion.GetConnexion();

            try
            {
                cnx.Open();

                string query = @"UPDATE intervention SET 
                                structure = @structure,
                                intervenants = @intervenants,
                                description = @description,
                                salle_besoin = @salle_besoin,
                                disposition_salle = @disposition_salle,
                                commentaire = @commentaire,
                                materiel = @materiel,
                                pause = @pause,
                                repas = @repas,
                                cout = @cout,
                                type_pause = @type_pause,
                                coordonnees = @coordonnees,
                                date_intervention = @date_intervention
                                WHERE titre = @titre";

                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@structure", t.GetStructureAsso());
                cmd.Parameters.AddWithValue("@intervenants", t.GetMembresAsso());
                cmd.Parameters.AddWithValue("@description", t.GetDescription());
                cmd.Parameters.AddWithValue("@salle_besoin", t.GetSalleBesoin());
                cmd.Parameters.AddWithValue("@disposition_salle", t.GetSalleDisposition());
                cmd.Parameters.AddWithValue("@commentaire", t.GetCommentaire());
                cmd.Parameters.AddWithValue("@materiel", t.GetMateriel());
                cmd.Parameters.AddWithValue("@pause", t.GetPause());
                cmd.Parameters.AddWithValue("@repas", t.GetRepas());
                cmd.Parameters.AddWithValue("@cout", t.GetCout());
                cmd.Parameters.AddWithValue("@type_pause", t.GetTypePause());
                cmd.Parameters.AddWithValue("@coordonnees", t.GetCooordonneesAsso());
                cmd.Parameters.AddWithValue("@date_intervention", t.GetDateIntervention());
                cmd.Parameters.AddWithValue("@titre", t.GetTitre());

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                cnx.Close();
            }
        }

        // DELETE
        public static bool Delete(string titre)
        {
            SqlConnection cnx = Connexion.GetConnexion();

            try
            {
                cnx.Open();
                string query = "DELETE FROM intervention WHERE titre = @titre";
                SqlCommand cmd = new SqlCommand(query, cnx);
                cmd.Parameters.AddWithValue("@titre", titre);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                cnx.Close();
            }
        }
    }
}
