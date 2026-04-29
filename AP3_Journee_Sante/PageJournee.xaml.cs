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
    /// Logique d'interaction pour PageJournee.xaml
    /// </summary>
    public partial class PageJournee : Page
    {
        MainWindow mainWindow;

        public ObservableCollection<Journee> ListeJournee;
        public PageJournee(MainWindow oneMainWindow)
        {
            this.mainWindow = oneMainWindow;
            InitializeComponent();

            ListeJournee = new ObservableCollection<Journee>();

            lvjournee.ItemsSource = ListeJournee;

            ChargerJournees();
        }
        private void btninvitation_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.ChangeJourneePage.Navigate(new PageInvitation());

            ChangeInvitationPage.Visibility = Visibility.Visible;
        }
        private void btnform_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.ChangeJourneePage.Navigate(new PageForm());

            ChangeFormPage.Visibility = Visibility.Visible;
        }
        private void btnedition_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.ChangeJourneePage.Navigate(new PageEdition());

            ChangeEditionPage.Visibility = Visibility.Visible;
        }
        private void BtnVoirAvis_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.ChangeJourneePage.Navigate(new PageAvis());

            ChangeAvisPage.Visibility = Visibility.Visible;
        }
        private void btnModifEdition_Click(object sender, RoutedEventArgs e)
        {

            Journee journeeSelectionnee = (Journee)lvjournee.SelectedItem;

            if (journeeSelectionnee != null)
            {
                    // Passer la journée sélectionnée au constructeur
                    this.mainWindow.ChangeJourneePage.Navigate(new PageModifEdition(journeeSelectionnee));

                    ChangeModifEditionPage.Visibility = Visibility.Visible;
                
            }

        }
        private void ChangeInvitationPage_Navigated(object sender, NavigationEventArgs e) { }

        private void ChangeFormPage_Navigated(object sender, NavigationEventArgs e) { }

        private void ChangeEditionPage_Navigated(object sender, NavigationEventArgs e) { }

        private void ChangeAvisPage_Navigated(object sender, NavigationEventArgs e) { }

        private void ChangeModifEditionPage_Navigated(object sender, NavigationEventArgs e) { }

        private void lvjournee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selection = lvjournee.SelectedItem != null;

            btnSupEdition.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
            btnModifEdition.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
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

        private void ChangeModifPage_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void btnSupEdition_Click(object sender, RoutedEventArgs e)
        {
            btnConfSupOui.Visibility = Visibility.Visible;
            btnConfSupNon.Visibility = Visibility.Visible;
            lblsupjournee.Visibility = Visibility.Visible;
        }

        private void btnConfSupOui_Click(object sender, RoutedEventArgs e)
        {
            Journee journeeSelectionnee = (Journee)lvjournee.SelectedItem;

            if (journeeSelectionnee == null)
                return;

            try
            {
                AdoJournee adoJournee = new AdoJournee();

                adoJournee.Delete(journeeSelectionnee.Id);

                // On considère que la suppression a fonctionné si aucune exception n'a été levée
                ListeJournee.Remove(journeeSelectionnee);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
            }
            finally
            {
                btnConfSupOui.Visibility = Visibility.Collapsed;
                btnConfSupNon.Visibility = Visibility.Collapsed;
                lblsupjournee.Visibility = Visibility.Collapsed;
            }
        }
    }
}
