using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;
using Microsoft.Win32;
using System.Net.Mail;

namespace AP3_Journee_Sante
{
    public partial class PageCreeAsso : Page
    {
        private string cheminImageSelectionnee = "";
        private readonly AdoAssociation _ado;   // Instance ADO nécessaire

        public PageCreeAsso()
        {
            InitializeComponent();
            _ado = new AdoAssociation();
        }

        private void btncreeimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choisir l'image de l'association";
            openFileDialog.Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                cheminImageSelectionnee = openFileDialog.FileName;
                lblcreenomimage.Content = System.IO.Path.GetFileName(cheminImageSelectionnee);
                imgNouvelleAsso.Source = new BitmapImage(new Uri(cheminImageSelectionnee));
            }
        }

        private async void btncreerasso_Click(object sender, RoutedEventArgs e)
        {



            // IMAGE OBLIGATOIRE

            if (string.IsNullOrWhiteSpace(cheminImageSelectionnee))

            {

                MessageBox.Show("L'image est obligatoire pour créer une association.",

                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                return;

            }

            // VERIF LONGUEUR NOM ET MAIL
            if ((tbcreenomasso.Text.Length > 50) || (btncreemailasso.Text.Length > 50)) // si le nom de l'asso ou son mail est suppérieur à 50 caractères, on affiche le message
            {
                MessageBox.Show("Le nom ou le mail de l'association ne peuvent pas dépasser 50 caractères !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // VERIF ESPACE BLANCS OU RIEN DANS MAIL ET NOM
            if(string.IsNullOrWhiteSpace(tbcreenomasso.Text) || string.IsNullOrWhiteSpace(btncreemailasso.Text) || string.IsNullOrWhiteSpace(btncreetelasso.Text)) // si le mail/nom/tel n'a rien ou a des espaces on affiche le message
            {
              MessageBox.Show("Le nom, le mail ou le téléphone de l'association ne peuvent pas être vides !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
             }

            // VERIF LONGUEUR TEL
            if (btncreetelasso.Text.Length > 10) // si le tel est supérieur à 10 caractères
            {
                MessageBox.Show("Le téléphone de l'association ne peut pas dépasser 10 caractères !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (btncreetelasso.Text.Length < 10) // si le tel est supérieur à 10 caractères
            {
                MessageBox.Show("Le téléphone de l'association ne peut pas être inférieur à 10 caractères !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // VERIF NOM TOTALEMENT EN STRING
            if (tbcreenomasso.Text.Any(char.IsDigit)) //vérifie que le nom n'a pas un caractère qui est un chiffre
            {
                MessageBox.Show("Le nom ne doit pas contenir de chiffres.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            // Dans ta méthode btncreerasso_Click, remplace la vérification du mail par :

            // VERIF FORMAT DU MAIL
            try
            {
                var mailAddress = new MailAddress(btncreemailasso.Text);
                // Vérifie aussi que le domaine a une extension valide (au moins 2 caractères après le dernier point)
                string domain = mailAddress.Host;
                int lastDot = domain.LastIndexOf('.');
                if (lastDot < 0 || domain.Length - lastDot - 1 < 2)
                {
                    throw new FormatException();
                }
            }
            catch
            {
                MessageBox.Show("Le format du mail n'est pas valide (ex: contact@exemple.fr)", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // VERIF TEL TOTALEMENT CHIFFRE
            if (!btncreetelasso.Text.All(char.IsDigit)) // si tout le contenu n'est pas en chiffre (digit) alors on affiche le message
            {
                MessageBox.Show("Le téléphone doit contenir uniquement des chiffres.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // VERIF TEL TOTALEMENT CHIFFRE
            if (!btncreetelasso.Text.All(char.IsDigit))
            {
                MessageBox.Show("Le téléphone doit contenir uniquement des chiffres.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //VERIF DOUBLON — À METTRE ICI 🔥
            bool existe = await _ado.ExisteAsync(tbcreenomasso.Text, btncreemailasso.Text);

            if (existe)
            {
                MessageBox.Show("Une association avec ce nom ou ce mail existe déjà !", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            string tel = btncreetelasso.Text;


            Associations nouvelleAsso = new Associations(
                0,
                tbcreenomasso.Text,
                btncreemailasso.Text,
                tel,
                //0,
                cheminImageSelectionnee
            );

            //  Appel correct sur l’instance ADO
            bool success = await _ado.CreateAsync(nouvelleAsso);

            if (success)
            {
                MessageBox.Show("Association créée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Vider le formulaire
                tbcreenomasso.Clear();
                btncreemailasso.Clear();
                btncreetelasso.Clear();
                cheminImageSelectionnee = "";
                imgNouvelleAsso.Source = null;
                lblcreenomimage.Content = "";

            }
            else
            {
                MessageBox.Show("Erreur lors de la création de l'association !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void btnretourasso_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }



        private void tbcreenomasso_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
