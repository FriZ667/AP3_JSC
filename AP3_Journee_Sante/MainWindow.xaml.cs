using System.Windows;

namespace AP3_Journee_Sante
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetAll()
        {
            ChangeAssoPage.Content = null;
            ChangeReponsePage.Content = null;
            ChangeJourneePage.Content = null;
            ChangeMembrePage.Content = null;

            ChangeAssoPage.Visibility = Visibility.Collapsed;
            ChangeReponsePage.Visibility = Visibility.Collapsed;
            ChangeJourneePage.Visibility = Visibility.Collapsed;
            ChangeMembrePage.Visibility = Visibility.Collapsed;

            welcome.Visibility = Visibility.Collapsed;
            intro.Visibility = Visibility.Collapsed;
            logo.Visibility = Visibility.Collapsed;
            video.Visibility = Visibility.Collapsed;
        }

        private void btnasso_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            ChangeAssoPage.Visibility = Visibility.Visible;
            ChangeAssoPage.Navigate(new PageAsso(this));
        }

        private void btnreponse_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            ChangeReponsePage.Visibility = Visibility.Visible;
            ChangeReponsePage.Navigate(new PageReponse(this));
        }

        private void btnjournee_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            ChangeJourneePage.Visibility = Visibility.Visible;
            ChangeJourneePage.Navigate(new PageJournee(this));
        }

        private void btnmembre_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            ChangeMembrePage.Visibility = Visibility.Visible;
            ChangeMembrePage.Navigate(new PageListeMembres());
        }
    }
}