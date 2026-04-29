using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP3_Journee_Sante.CLASSE
{
    internal class Intervention
    {
        //Attributs de la classe Intervention

        public string StructureAsso { get; set; }
        public string MembresAsso { get; set; }
        public string Description { get; set; }
        public string Titre { get; set; }
        public string SalleBesoin { get; set; }
        public string SalleDisposition { get; set; }
        public string Commentaire { get; set; }
        public string Materiel { get; set; }
        public int Pause { get; set; }
        public int Repas { get; set; }
        public int Cout { get; set; }
        public int TypePause { get; set; }
        public int CoordonneesAsso { get; set; }
        public DateTime DateIntervention { get; set; }



        // Constructeur de la classe Intervention

        public Intervention(string structureAsso, string membresAsso, string description, string titre,
                             string salleBesoin, string salleDisposition, string commentaire, string materiel, int pause,
                            int repas, int cout, int typePause, int coordonneesAsso, DateTime dateIntervention)
        {

            this.StructureAsso = structureAsso;
            this.MembresAsso = membresAsso;
            this.Description = description;
            this.Titre = titre;
            this.SalleBesoin = salleBesoin;
            this.SalleDisposition = salleDisposition;
            this.Commentaire = commentaire;
            this.Materiel = materiel;
            this.Pause = pause;
            this.Repas = repas;
            this.Cout = cout;
            this.TypePause = typePause;
            this.CoordonneesAsso = coordonneesAsso;
            this.DateIntervention = dateIntervention;
        }

        // Accesseurs en lecture pour la structure de l'entreprise

        public string GetStructureAsso()
        {
            return StructureAsso; // en lecture
        }

        // Accesseurs en lecture pour les membres de l'association

        public string GetMembresAsso()
        {
            return MembresAsso; // en lecture
        }

        // Accesseurs en lecture pour la description de l'intervention

        public string GetDescription()
        {
            return Description; // en lecture
        }

        // Accesseurs en lecture pour le titre de l'intervention

        public string GetTitre()
        {
            return Titre; // en lecture
        }

        // Accesseurs en lecture pour les besoins de la salle

        public string GetSalleBesoin()
        {
            return SalleBesoin; // en lecture
        }

        // Accesseurs en lecture pour la disposition de la salle

        public string GetSalleDisposition()
        {
            return SalleDisposition; // en lecture
        }

        // Accesseurs en lecture pour le Commentaire

        public string GetCommentaire()
        {
            return Commentaire; // en lecture
        }

        // Accesseurs en lecture pour le matériel

        public string GetMateriel()
        {
            return Materiel; // en lecture
        }

        // Accesseurs en lecture pour la pause

        public int GetPause()
        {
            return Pause; // en lecture
        }

        // Accesseurs en lecture pour le type de pause

        public int GetTypePause()
        {
            return TypePause; // en lecture
        }

        // Accesseurs en lecture pour le repas

        public int GetRepas()
        {
            return Repas; // en lecture
        }

        // Accesseurs en lecture pour le coût


        public int GetCout()
        {
            return Cout; // en lecture
        }

        // Accesseurs en lecture  pour les coordonnées des associations


        public int GetCooordonneesAsso()
        {
            return CoordonneesAsso; // en lecture
        }

        // Accesseurs en lecture pour la date de l'intervention

        public DateTime GetDateIntervention()
        {
            return DateIntervention; // en lecture
        }

    }

    // Relations métier (à activer quand les classes seront prêtes)

    // Association organisatrice
    // private Association organisateur;

    // Liste des membres participants
    // private List<Membre> participants;

    // Journée santé concernée
    // private Journee journee;

}
