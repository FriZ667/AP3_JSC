using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using AP3_Journee_Sante.CLASSE;
using System.Windows;


namespace AP3_Journee_Sante.ADO
{
    public class AdoAssociation : ADO
    {
        // CREATE
        public async Task<bool> CreateAsync(Associations a)
        {
            try
            {
                Open();

                string query = @"
                INSERT INTO association 
                (nom_association, telephone_association, mail_association,  image_association) 
                VALUES 
                (@nom_association, @telephone_association, @mail_association, @image_association)";
                //a mettre plus tard ou pas @membres_association, membres_association,
                using SqlCommand cmd = new SqlCommand(query, Connection);

                cmd.Parameters.AddWithValue("@nom_association", a.GetNomAsso());
                cmd.Parameters.AddWithValue("@telephone_association", a.GetTelAsso());
                cmd.Parameters.AddWithValue("@mail_association", a.GetMailAsso());
                //cmd.Parameters.AddWithValue("@membres_association", a.GetMembresAsso());
                cmd.Parameters.AddWithValue("@image_association", a.GetImgChemin());

                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                Close();
            }
        }

        // READ ALL
        public async Task<List<Associations>> ReadAllAsync()
        {
            List<Associations> liste = new List<Associations>();

            try
            {
                Open();

                string query = @"SELECT a.*, " +
    "(SELECT COUNT(*) FROM membre m WHERE m.fk_association = a.id_association) AS nb_membres, " +
    "(SELECT STRING_AGG(m.nom + ' ' + m.prenom, ', ') FROM membre m WHERE m.fk_association = a.id_association) AS liste_membres, " +
    "(SELECT STRING_AGG(CONVERT(varchar(10), j.date_journee, 103), ', ') FROM journee j INNER JOIN association_journee aj ON j.id_journee = aj.id_journee WHERE aj.id_association = a.id_association) AS journees_participees " +
    "FROM association a";


                using SqlCommand cmd = new SqlCommand(query, Connection);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Associations a = new Associations(
                        reader["id_association"] != DBNull.Value ? (int)reader["id_association"] : 0,
                        reader["nom_association"]?.ToString() ?? "",
                        reader["mail_association"]?.ToString() ?? "",
                        reader["telephone_association"]?.ToString() ?? "",
                        reader["image_association"]?.ToString() ?? ""
                    );

                    a.MembresAsso = Convert.ToInt32(reader["nb_membres"]);
                    a.ListeMembres = reader["liste_membres"] == DBNull.Value ? "": reader["liste_membres"].ToString();
                    a.JourneeParticipe = reader["journees_participees"] == DBNull.Value ? "Aucune" : reader["journees_participees"].ToString();

                    liste.Add(a);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur ReadAllAsync : " + ex.Message);
                return new List<Associations>();
            }
            finally
            {
                Close();
            }

            return liste;
        }

        // UPDATE
        // UPDATE
        public async Task<bool> UpdateAsync(Associations a)
        {
            try
            {
                Open();

                string query = @"
        UPDATE association SET 
            nom_association = @nom_association,
            mail_association = @mail_association,
            telephone_association = @telephone_association,
            image_association = @image_association
        WHERE id_association = @id_association";

                using SqlCommand cmd = new SqlCommand(query, Connection);

                cmd.Parameters.AddWithValue("@id_association", a.IdAsso);
                cmd.Parameters.AddWithValue("@nom_association", a.GetNomAsso());
                cmd.Parameters.AddWithValue("@mail_association", a.GetMailAsso());
                cmd.Parameters.AddWithValue("@telephone_association", a.GetTelAsso());
                cmd.Parameters.AddWithValue("@image_association", a.GetImgChemin());

                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                Close();
            }
        }


        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Open();

                string query = "DELETE FROM association WHERE id_association = @id_association";

                using SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("@id_association", id);

                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur SQL DeleteAsync : " + ex.Message);
                return false;
            }

            finally
            {
                Close();
            }
        }


        //EVITE LES DOUBLONS
        public async Task<bool> ExisteAsync(string nom, string email)
        {
            string query = "SELECT COUNT(*) FROM Association WHERE nom_association = @nom OR mail_association = @mail";

            using (SqlConnection cnx = Connexion.GetConnexion())
            using (SqlCommand cmd = new SqlCommand(query, cnx))
            {
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@mail", email);

                await cnx.OpenAsync();
                int count = (int)await cmd.ExecuteScalarAsync();

                return count > 0;
            }
        }


        // READ BY JOURNEE (POUR AFFICHER LES ASSOCIATIONS DANS LA JOURNEE DE SANTE)
        public async Task<List<Associations>> ReadByJourneeAsync(int idJournee)
        {
            var liste = new List<Associations>();

            string query = @"
        SELECT a.*
        FROM association a 
        INNER JOIN association_journee aj 
            ON a.id_association = aj.id_association 
        WHERE aj.id_journee = @idJournee";

            using (var connection = Connexion.GetConnexion())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idJournee", idJournee);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    liste.Add(new Associations(
                        reader["id_association"] != DBNull.Value ? (int)reader["id_association"] : 0,
                        reader["nom_association"]?.ToString() ?? "",
                        reader["mail_association"]?.ToString() ?? "",
                        reader["telephone_association"]?.ToString() ?? "",
                        reader["image_association"]?.ToString() ?? ""
                    ));
                }
            }

            return liste;
        }


        // LIER UNE ASSOCIATION À UNE JOURNÉE DE SANTÉ
        public async Task<bool> LierAJourneeAsync(int idAssociation, int idJournee)
        {
            try
            {
                string query = @"
            INSERT INTO association_journee (id_association, id_journee)
            VALUES (@idAssociation, @idJournee)";

                using (var connection = Connexion.GetConnexion())
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idAssociation", idAssociation);
                    command.Parameters.AddWithValue("@idJournee", idJournee);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur LierAJourneeAsync : " + ex.Message);
                return false;
            }
        }
    }
}
