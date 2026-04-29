using AP3_Journee_Sante.CLASSE;
using AP3_Journee_Sante.ADO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour PageLier.xaml
    /// </summary>
    public partial class PageLier : Page
    {

        public ObservableCollection<Journee> ListeJournee;
        private Associations assoSelectionnee;
        private AdoAssociation _adoAssociation = new AdoAssociation();
        public PageLier(Associations asso) : this()
        {
            this.assoSelectionnee = asso;
            lblasso.Content = asso.NomAsso;
        }
        public PageLier()
        {

            InitializeComponent();

            ListeJournee = new ObservableCollection<Journee>();

            cbjournee.ItemsSource = ListeJournee;

            ChargerJournees();
        }

        private void cbjournee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ChargerJournees()
        {
            ListeJournee.Clear();

            AdoJournee ado = new AdoJournee();
            var journees = ado.GetAll();

            foreach (var j in journees)
            {
                ListeJournee.Add(j);
            }
        }

        private async void btnlier_Click(object sender, RoutedEventArgs e)
        {
            if (cbjournee.SelectedItem is not Journee journee)
            {
                MessageBox.Show("Sélectionnez d'abord une journée.");
                return;
            }

            bool ok = await _adoAssociation.LierAJourneeAsync(assoSelectionnee.IdAsso, journee.Id);

            if (ok)
            {
                MessageBox.Show($"{assoSelectionnee.NomAsso} liée à la journée du {journee.Date.ToShortDateString()}.");
            }
            else
            {
                MessageBox.Show($"{assoSelectionnee.NomAsso} est déjà liée à cette journée.");
            }
        }
    }
}
