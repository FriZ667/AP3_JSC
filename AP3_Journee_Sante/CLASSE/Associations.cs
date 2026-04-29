using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP3_Journee_Sante.CLASSE
{
    public class Associations
    {
        // Attributs privés de l'association

        public int IdAsso { get; set; }
        public string NomAsso { get; set; }          // Nom de l'association
        public string MailAsso { get; set; }         // Adresse mail de contact
        public string TelAsso { get; set; }             // Numéro de téléphone
        //public int MembresAsso { get; set; }      // Nombre des membres
        public string ImgChemin { get; set; }        // Chemin vers l'image de la'telier
        //public string ComAsso { get; set; }        // Chemin vers l'image de la'telier
        public string JourneeParticipe { get; set; } // Liste des journées auxquelles l'association participe
        // Constructeur 
        public Associations(int idAsso, string nomAsso, string mailAsso, string telAsso, string imgChemin) //string comAsso //, int membresAsso
        {
            this.IdAsso = idAsso;
            this.NomAsso = nomAsso.Trim();       // Supprime les espaces inutiles
            this.MailAsso = mailAsso?.Trim() ?? "";
            this.TelAsso = telAsso.Trim(); // si null en BDD, ReadAll() doit fournir 0
            //this.MembresAsso = membresAsso; // idem
            this.ImgChemin = string.IsNullOrWhiteSpace(imgChemin)
                             ? "pack://application:,,,/Images/default.png"
                             : imgChemin;
            //this.ComAsso = comAsso;
        }

        // Accesseurs pour le nom de l'association
        public void SetNomAsso(string nomAsso)   // Permet de modifier le nom
        {
            this.NomAsso = nomAsso;
        }

        public string GetNomAsso()               // Permet de lire le nom
        {
            return NomAsso;
        }

        // Accesseurs pour le mail de l'association
        public void SetMailAsso(string mailAsso)
        {
            this.MailAsso = mailAsso;
        }

        public string GetMailAsso()
        {
            return MailAsso;
        }

        // Accesseurs pour le téléphone
        public void SetTelAsso(string telAsso)
        {
            this.TelAsso = telAsso;
        }

        public string GetTelAsso()
        {
            return TelAsso;
        }

        // Accesseurs pour les membres
        public int MembresAsso { get; set; }


        // Accesseurs pour le chemin de l'image
        public void SetImgChemin(string imgChemin)
        {
            this.ImgChemin = imgChemin;
        }

        public string GetImgChemin()
        {
            return ImgChemin;
        }

        //Liste des membres de l'association
        public string ListeMembres { get; set; }

    }
}
