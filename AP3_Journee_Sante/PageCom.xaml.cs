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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AP3_Journee_Sante
{
    /// <summary>
    /// Logique d'interaction pour PageCom.xaml
    /// </summary>
    public partial class PageCom : Page
    {
        public PageCom()
        {
            InitializeComponent();
        }

        private void btnsupcom_Click(object sender, RoutedEventArgs e)
        {
            btnconfouicom.Visibility = Visibility.Visible;
            btnconfnoncom.Visibility = Visibility.Visible;
            lblsupcom.Visibility = Visibility.Visible;
        }

        private void btnconfouicom_Click(object sender, RoutedEventArgs e)
        {
            btnconfouicom.Visibility = Visibility.Collapsed;
            btnconfnoncom.Visibility = Visibility.Collapsed;
            lblsupcom.Visibility = Visibility.Collapsed;
        }

        private void btnconfnoncom_Click(object sender, RoutedEventArgs e)
        {
            btnconfouicom.Visibility = Visibility.Collapsed;
            btnconfnoncom.Visibility = Visibility.Collapsed;
            lblsupcom.Visibility = Visibility.Collapsed;
        }
    }
}
