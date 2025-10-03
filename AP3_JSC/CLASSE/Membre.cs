using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP3_JSC.CLASSE
{
    internal class Membre
    {
        public String nom { get; set; }
        public String prenom { get; set; }
        public String fonction { get; set; }

        public Membre(string nom, string prenom, string fonction)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.fonction = fonction;
        }

        public void renseignerFonction()
        {

        }
    }
}
