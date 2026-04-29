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
using System.Windows.Shapes;
using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;

namespace AP3_Journee_Sante
{
    /// <summary>
    /// Logique d'interaction pour PageCreerMembre.xaml
    /// </summary>
    public partial class PageCreerMembre : Page
    {
        private AdoAssociation _adoAsso = new AdoAssociation();
        private AdoMembres _adoMembre = new AdoMembres();
        private int selectedAssoId;

        public PageCreerMembre()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbasso.ItemsSource = await _adoAsso.ReadAllAsync();
        }

        private void cbasso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbasso.SelectedItem is Associations a)
                selectedAssoId = a.IdAsso;
        }

        private void btncreermembre_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btncreemembrenom.Text) ||
                string.IsNullOrWhiteSpace(btncreemembreprenom.Text) ||
                string.IsNullOrWhiteSpace(btncreefonctionmembre.Text))
            {
                MessageBox.Show("Nom, prénom et fonction sont obligatoires");
                return;
            }

            Membres m = new Membres(
                btncreemembrenom.Text,
                btncreemembreprenom.Text,
                btncreefonctionmembre.Text,
                selectedAssoId
            );

            _adoMembre.Create(m);

            ConfirmCreeMembre.Visibility = Visibility.Visible;
        }

        private void btnretourmembre_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageListeMembres());
        }

    }
}