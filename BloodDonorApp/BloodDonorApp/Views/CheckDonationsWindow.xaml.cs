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
    /// Interaction logic for CheckDonationsWindow.xaml
    /// </summary>
    public partial class CheckDonationsWindow : Window
    {
        public CheckDonationsWindow(string mail)
        {
            InitializeComponent();
            InitializeMail(mail);
        }

        public void InitializeMail(string mail)
        {
            txtmail.Text = mail;
        }
    }
}
