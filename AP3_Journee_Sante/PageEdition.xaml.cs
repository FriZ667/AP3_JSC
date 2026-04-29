using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AP3_Journee_Sante
{
    /// <summary>
    /// Logique d'interaction pour PageEdition.xaml
    /// </summary>
    public partial class PageEdition : Page
    {
        public PageEdition()
        {
            InitializeComponent();
        }

        private void btncreerjournee_Click(object sender, RoutedEventArgs e)
        {
            string Lieu = tblieujournee.Text.Trim();


            if (string.IsNullOrWhiteSpace(Lieu))
            {
                MessageBox.Show("Veuillez entrer un nom de lieu avant de l’ajouter !");
                return;
            }

            if (dpDateJournee.SelectedDate == null)
            {
                MessageBox.Show("Veuillez choisir une date !");
                return;
            }
            DateTime dateChoisie = dpDateJournee.SelectedDate.Value;
            try
            {
                Journee journee = new Journee(dateChoisie, Lieu);

                // 3️⃣ Ajout en BDD
                AdoJournee ado = new AdoJournee();
                ado.Create(journee);

                tblieujournee.Text = "";

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
        

        private void btncreerlieu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cblieujournee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
