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
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow(string mail, string cnp)
        {
            InitializeComponent(); 
            InitializeMail(mail);
            InitializeInfo(cnp);
        }

        public void InitializeMail(string mail)
        {
            txtmail.Text = mail;
        }
    
        public void InitializeInfo(string cnp)
        {

        }
    }
}
