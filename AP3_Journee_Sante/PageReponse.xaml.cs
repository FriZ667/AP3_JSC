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
    /// Logique d'interaction pour PageReponse.xaml
    /// </summary>
    public partial class PageReponse : Page
    {
        MainWindow mainWindow;
        public PageReponse(MainWindow oneMainWindow)
        {
            this.mainWindow = oneMainWindow;
            InitializeComponent();

            var faussesDonnees = new ObservableCollection<Intervention>();

            // Création d'objets Journee simplifiés
            faussesDonnees.Add(new Intervention(
                "SangBesac", // Nom
                "Monsieur Pernelle, Mathéo Ruiz",
                "On va présenter du sang humain",
                "Don du sang",
                "Vidéo Projecteur",
                "Des tables",
                "On fait un truc",
                "Un tableau",
                1,
                2,
                80,
                8,
                9,
                new DateTime(2025, 12, 01)

            ));

            faussesDonnees.Add(new Intervention(
                "Don du sang", // Nom
                "Don du sang",
                "Don du sang",
                "Don du sang",
                "Don du sang",
                "Don du sang",
                "Don du sang",
                "Don du sang",
                6,
                3,
                9,
                8,
                9,
                new DateTime(2025, 12, 01)
            ));

            lvreponse.ItemsSource = faussesDonnees;
        }

        private void btnasso_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void btnedition_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.ChangeReponsePage.Navigate(new PageAjouterRep());
            ChangeAjouterRepPage.Visibility = Visibility.Visible;
        }
        private void ChangeAjouterRepPage_Navigated(object sender, NavigationEventArgs e)
        {
        }
    }
}
