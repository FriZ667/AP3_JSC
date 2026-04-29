using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP3_Journee_Sante.CLASSE
{
    public class Membres
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Fonction { get; set; }
        public int IdAsso { get; set; }

        public Membres(string nom, string prenom, string fonction, int idAsso)
        {
            Nom = nom;
            Prenom = prenom;
            Fonction = fonction;
            IdAsso = idAsso;
        }


        public string RenseignerFonction()
        {
            return Fonction;
        }
    }
}
