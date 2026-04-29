using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;


namespace AP3_Journee_Sante
{
    public partial class PageModifier : Page
    {
        private string cheminImageSelectionnee = "";
        private Associations assoSelectionnee;

        public PageModifier(Associations asso)
        {
            InitializeComponent();
            assoSelectionnee = asso;

            tbmodifnomasso.Text = assoSelectionnee.NomAsso;
            tbmodifmailasso.Text = assoSelectionnee.MailAsso;
            tbmodiftelasso.Text = assoSelectionnee.TelAsso.ToString();
            lblmodifnomimage2.Content = System.IO.Path.GetFileName(assoSelectionnee.ImgChemin);

            imgActuelle.Source = new BitmapImage(new Uri(assoSelectionnee.ImgChemin));

        }

        // Bouton Modifier
        private async void btnmodifasso_Click(object sender, RoutedEventArgs e)
        {
            // Vérifications
            if (tbmodifnomasso.Text.Length > 50)
            {
                MessageBox.Show("Le nom de l'association ne peut pas dépasser 50 caractères !");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbmodifnomasso.Text))
            {
                MessageBox.Show("Le nom de l'association ne peut pas être vide !");
                return;
            }

            string tel = tbmodiftelasso.Text;


            // Gérer l'image : si l'utilisateur n'a rien changé, garder l'ancienne
            string img = string.IsNullOrWhiteSpace(cheminImageSelectionnee)
                         ? assoSelectionnee.ImgChemin
                         : cheminImageSelectionnee;

            Associations AssoModifiee = new Associations(
                assoSelectionnee.IdAsso,
                tbmodifnomasso.Text,
                tbmodifmailasso.Text,
                tel,
                img
            );

            try
            {
                // Mise à jour en BDD
                AdoAssociation ado = new AdoAssociation();
                bool success = await ado.UpdateAsync(AssoModifiee);

                if (success)
                {
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification de l'association !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        // Bouton pour choisir une image
        private void btnmodifimageasso_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choisir l'image de l'association",
                Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                cheminImageSelectionnee = openFileDialog.FileName;
                lblmodifnomimage.Content = System.IO.Path.GetFileName(cheminImageSelectionnee);
                imgNouvelle.Source = new BitmapImage(new Uri(cheminImageSelectionnee));

            }
        }

        private void tbmodiftelasso_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }


    }
}
