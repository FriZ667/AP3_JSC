using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AP3_Journee_Sante.ADO
{
    public static class Connexion
    {
        public static SqlConnection GetConnexion()
        {
            string connectionString = "Server=192.168.21.1;Database=NOVALISM;User Id=novalism_admin;Password=N0val1sm@!2024*;TrustServerCertificate=True;Encrypt=False;";
            return new SqlConnection(connectionString);
        }
    }
}
