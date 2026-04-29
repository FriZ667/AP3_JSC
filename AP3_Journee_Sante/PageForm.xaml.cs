using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;
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
    /// Logique d'interaction pour PageForm.xaml
    /// </summary>
    public partial class PageForm : Page
    {
        private AdoAssociation _adoAssociation;
        public ObservableCollection<Associations> ListeAssociations { get; set; }
        public ObservableCollection<Journee> ListeJournee { get; set; }

        public PageForm()
        {
            InitializeComponent();

            _adoAssociation = new AdoAssociation();
            ListeAssociations = new ObservableCollection<Associations>();
            lvform.ItemsSource = ListeAssociations;
            this.Loaded += PageForm_Loaded;

            ListeJournee = new ObservableCollection<Journee>();
            cbform.ItemsSource = ListeJournee;
            ChargerJournees();
        }
        private async void PageForm_Loaded(object sender, RoutedEventArgs e)
        {
            await ChargerAssoAsync();
        }
        private async Task ChargerAssoAsync()
        {
            try
            {
                ListeAssociations.Clear();
                var associations = await _adoAssociation.ReadAllAsync();
                foreach (var a in associations)
                    ListeAssociations.Add(a);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des associations : " + ex.Message);
            }
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
        private async void cbform_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbform.SelectedItem is Journee journeeSelectionnee)
            {
                await ChargerAssoParJourneeAsync(journeeSelectionnee.Id);
            }
        }
        private async Task ChargerAssoParJourneeAsync(int idJournee)
        {
            try
            {
                ListeAssociations.Clear();

                var associations = await _adoAssociation.ReadByJourneeAsync(idJournee);

                foreach (var a in associations)
                    ListeAssociations.Add(a);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
    }
}
