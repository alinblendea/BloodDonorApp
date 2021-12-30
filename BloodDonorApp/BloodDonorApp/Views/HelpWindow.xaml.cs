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

namespace BloodDonorApp.Views
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            txtHelp.Text = "There are 3 types of users: \n" +
                           "\n     -Donors, which can sign up for a donation and view its donations;" +
                           "\n     -Medics, which can request a donation and view now manu requests have \n been made " +
                           "for each blood part;" +
                           "\n     -Staff members, who can approve donors based on their medical form and \n who can " +
                           "set the donations' status as DONE whenever a donation has \n been completed;" +
                           "\n\n\n Each type of user needs to register with an email and \n password. If your account doesn't work on " +
                           "login, you may have chosen another \n type of user to login as.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow mainWindow = (Application.Current.MainWindow as HelpWindow);
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
