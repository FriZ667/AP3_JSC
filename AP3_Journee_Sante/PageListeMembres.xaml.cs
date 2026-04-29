using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;

namespace AP3_Journee_Sante
{
    public partial class PageListeMembres : Page
    {
        private readonly AdoMembres _adoMembres;
        private readonly AdoAssociation _adoAsso;

        public ObservableCollection<MembreAffiche> Liste { get; set; }

        public PageListeMembres()
        {
            InitializeComponent();

            _adoMembres = new AdoMembres();
            _adoAsso = new AdoAssociation();

            Liste = new ObservableCollection<MembreAffiche>();
            lvmembres.ItemsSource = Liste;

            this.Loaded += PageListeMembres_Loaded;
        }

        private async void PageListeMembres_Loaded(object sender, RoutedEventArgs e)
        {
            Liste.Clear();

            var membres = _adoMembres.GetAll();
            var associations = await _adoAsso.ReadAllAsync();

            foreach (var m in membres)
            {
                var asso = associations.Find(a => a.IdAsso == m.IdAsso);

                Liste.Add(new MembreAffiche
                {
                    Nom = m.Nom,
                    Prenom = m.Prenom,
                    Fonction = m.Fonction,
                    NomAssociation = asso?.NomAsso ?? "Aucune"
                });
            }
        }

        private void lvmembres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selection = lvmembres.SelectedItem != null;

            btnModifier.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
            btnSupprimer.Visibility = selection ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCreerMembre());
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            var item = (MembreAffiche)lvmembres.SelectedItem;
            if (item == null) return;

            var membre = _adoMembres.GetAll().Find(m =>
                m.Nom == item.Nom &&
                m.Prenom == item.Prenom &&
                m.Fonction == item.Fonction
            );

            if (membre == null)
            {
                MessageBox.Show("Impossible de retrouver le membre.");
                return;
            }

            NavigationService.Navigate(new PageModifierMembre(membre));
        }


        private async void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var membreSelectionne = (MembreAffiche)lvmembres.SelectedItem;

            if (membreSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un membre.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show(
                "Voulez-vous vraiment supprimer ce membre ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result != MessageBoxResult.Yes)
                return;

            // Retrouver le vrai membre (avec l’ID)
            var membre = _adoMembres.GetAll().Find(m =>
                m.Nom == membreSelectionne.Nom &&
                m.Prenom == membreSelectionne.Prenom &&
                m.Fonction == membreSelectionne.Fonction
            );

            if (membre == null)
            {
                MessageBox.Show("Impossible de retrouver le membre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Suppression BDD
            _adoMembres.Delete(membre.Id);

            // Suppression dans la liste affichée
            Liste.Remove(membreSelectionne);

            MessageBox.Show("Membre supprimé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }

    public class MembreAffiche
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Fonction { get; set; }
        public string NomAssociation { get; set; }
    }
}
