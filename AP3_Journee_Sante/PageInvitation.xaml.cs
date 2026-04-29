using AP3_Journee_Sante.ADO;
using AP3_Journee_Sante.CLASSE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Logique d'interaction pour PageInvitation.xaml
    /// </summary>
    
    
    public partial class PageInvitation : Page
    {
        private AdoAssociation _ado;
        public ObservableCollection<Associations> ListeAssociations { get; set; }
        public ObservableCollection<Journee> ListeJournee { get; set; }
        public PageInvitation()
        {

            InitializeComponent();

            ListeJournee = new ObservableCollection<Journee>();
            cbinvite.ItemsSource = ListeJournee;
            ChargerJournees();

            _ado = new AdoAssociation();
            ListeAssociations = new ObservableCollection<Associations>();
            lvinvite.ItemsSource = ListeAssociations;
            this.Loaded += PageInvitation_Loaded;
        }
        private async void PageInvitation_Loaded(object sender, RoutedEventArgs e)
        {
            await ChargerAssoAsync();
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
                MessageBox.Show("Erreur lors du chargement des associations : " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbinvite.SelectedItem == null || lvinvite.SelectedItem == null)
            {
                MessageBox.Show("Un élément obligatoire n'a pas été selectionné");
                return;
            }

            Journee journeeSelectionne = (Journee)cbinvite.SelectedItem;
            Associations assoSelectionnee = (Associations)lvinvite.SelectedItem;

            if (string.IsNullOrWhiteSpace(assoSelectionnee.MailAsso))
            {
                MessageBox.Show("Cette association n'a pas d'email.");
                return;
            }

            string dateFormatee = journeeSelectionne.Date.ToString("dd/MM/yyyy");
            string sujet = $"Invitation à la Journée Santé et Citoyenneté du {dateFormatee}";

            string corps = $@"À l'attention de {assoSelectionnee.NomAsso},
 
Ceci est un message de test pour l'envoi d'une invitation à une journée se passant à : {journeeSelectionne.Lieu}.
Voici un formulaire d'inscription pour la journée du {journeeSelectionne.Date} : http://192.168.20.1/formulaireAP3/index.php
 
Merci de votre réponse.
L'équipe de la Journée Santé et Citoyenneté";

            EnvoiMail(assoSelectionnee.MailAsso, sujet, corps);
        }

        private void EnvoiMail(string mail, string sujet, string corps)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("novalismjsc@gmail.com", "bozg wrup bozk qhok"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("novalismjsc@gmail.com", "Novalism"),
                    Subject = sujet,
                    Body = corps,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(mail);

                smtpClient.Send(mailMessage);

                MessageBox.Show($"L'invitation à été envoyée.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur", "Erreur d'envoi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
    }
}