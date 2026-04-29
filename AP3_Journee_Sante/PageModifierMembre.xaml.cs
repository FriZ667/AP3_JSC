using System.Windows;
using System.Windows.Controls;
using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;

namespace AP3_Journee_Sante
{
    public partial class PageModifierMembre : Page
    {
        private readonly AdoMembres _adoMembres;
        private readonly AdoAssociation _adoAsso;
        private readonly Membres _membreOriginal;

        public PageModifierMembre(Membres membre)
        {
            InitializeComponent();

            _adoMembres = new AdoMembres();
            _adoAsso = new AdoAssociation();
            _membreOriginal = membre;

            ChargerAssociations();
            ChargerInfos();
        }


        private async void ChargerAssociations()
        {
            var liste = await _adoAsso.ReadAllAsync();
            cbAsso.ItemsSource = liste;
            cbAsso.DisplayMemberPath = "NomAsso";
            cbAsso.SelectedValuePath = "IdAsso";

            ChargerInfos();
        }

        private void ChargerInfos()
        {
            tbmodifNom.Text = _membreOriginal.Nom;
            tbmodifPrenom.Text = _membreOriginal.Prenom;
            tbmodifFunction.Text = _membreOriginal.Fonction;
            cbAsso.SelectedValue = _membreOriginal.IdAsso;
        }

        private async void btnmodifmembre_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbmodifNom.Text) ||
                string.IsNullOrWhiteSpace(tbmodifPrenom.Text) ||
                string.IsNullOrWhiteSpace(tbmodifFunction.Text))
            {
                MessageBox.Show("Nom, prénom et fonction sont obligatoires");
                return;
            }

            Membres modif = new Membres(
                tbmodifNom.Text,
                tbmodifPrenom.Text,
                tbmodifFunction.Text,
                (int)cbAsso.SelectedValue
            );

            modif.Id = _membreOriginal.Id; // garder l’ID

            bool ok = await _adoMembres.UpdateAsync(modif);

            if (ok)
            {
                MessageBox.Show("Membre modifié avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                // On revient simplement à la page de liste dans le même Frame
                NavigationService.Navigate(new PageListeMembres());
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnretour_Click(object sender, RoutedEventArgs e)
        {
            // On revient simplement à la page de liste dans le même Frame
            NavigationService.Navigate(new PageListeMembres());
        }



    }
}
