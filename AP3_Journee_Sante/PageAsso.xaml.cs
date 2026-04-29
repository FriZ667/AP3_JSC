using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AP3_Journee_Sante
{
    /// <summary>
    /// Logique d'interaction pour PageAsso.xaml
    /// </summary>
    public partial class PageAsso : Page
    {
        private readonly AdoAssociation _ado;
        public ObservableCollection<Associations> ListeAssociations { get; set; }
        public ObservableCollection<Journee> ListeJournee { get; set; }

        MainWindow mainWindow;

        public PageAsso(MainWindow oneMainWindow)
        {
            this.mainWindow = oneMainWindow;
            InitializeComponent();

            _ado = new AdoAssociation();
            ListeAssociations = new ObservableCollection<Associations>();
            lvasso.ItemsSource = ListeAssociations;

            this.Loaded += PageAsso_Loaded;

            ListeJournee = new ObservableCollection<Journee>();
        }

        private async void PageAsso_Loaded(object sender, RoutedEventArgs e)
        {
            await ChargerAssoAsync();
        }

        private void btncreerasso_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.ChangeAssoPage.Navigate(new PageCreeAsso());
            ChangeAjouterPage.Visibility = Visibility.Visible;
        }

        private void btnmodifasso_Click(object sender, RoutedEventArgs e)
        {
            Associations assoSelectionnee = (Associations)lvasso.SelectedItem;

            if (assoSelectionnee != null)
            {
                this.mainWindow.ChangeAssoPage.Navigate(new PageModifier(assoSelectionnee));
                ChangeModifPage.Visibility = Visibility.Visible;
            }
        }


        private void btnlierasso_Click(object sender, RoutedEventArgs e)
        {
            Associations assoSelectionnee = (Associations)lvasso.SelectedItem;

            if (assoSelectionnee != null)
            {
                this.mainWindow.ChangeAssoPage.Navigate(new PageLier(assoSelectionnee));
                ChangeLierPage.Visibility = Visibility.Visible;
            }
        }

        private void btncom_Click(object sender, RoutedEventArgs e)
        {
            Associations assoSelectionnee = (Associations)lvasso.SelectedItem;

            if (assoSelectionnee != null)
            {
                this.mainWindow.ChangeAssoPage.Navigate(new PageCom());
                ChangeComPage.Visibility = Visibility.Visible;
            }
        }

        private void lvasso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selection = lvasso.SelectedItem != null;

            btnsuppasso.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
            btnmodifasso.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
            btnlierasso.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
            btncom.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
        }

        private async Task ChargerAssoAsync()
        {
            try
            {
                ListeAssociations.Clear();
                var associations = await _ado.ReadAllAsync();

                foreach (var a in associations)
                    ListeAssociations.Add(a);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des associations : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public PageAsso()
        {
            InitializeComponent();

            _ado = new AdoAssociation();
            ListeAssociations = new ObservableCollection<Associations>();
            lvasso.ItemsSource = ListeAssociations;

            this.Loaded += PageAsso_Loaded;
        }

        private async void btnsuppasso_Click(object sender, RoutedEventArgs e)
        {
            Associations assoSelectionnee = (Associations)lvasso.SelectedItem;

            if (assoSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une association.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show(
                "Voulez-vous vraiment supprimer cette association ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question
            );

            if (result != MessageBoxResult.Yes)
            {
                MessageBox.Show("Suppression annulée.", "Suppression annulée", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                bool deleted = await _ado.DeleteAsync(assoSelectionnee.IdAsso);

                if (deleted)
                {
                    ListeAssociations.Remove(assoSelectionnee);
                    MessageBox.Show("Association supprimée avec succès !", "Succès de suppression", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression de l'association !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btncreermembre_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageCreerMembre());
        }

        public void AjouterNouvelleAssociationDansUI(Associations nouvelle)
        {
            if (nouvelle != null)
                ListeAssociations.Add(nouvelle);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ChangeAjouterPage_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private void ChangeLierPage_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private void ChangeModifPage_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private void ChangeComPage_Navigated(object sender, NavigationEventArgs e)
        { 
        }
    }
}
