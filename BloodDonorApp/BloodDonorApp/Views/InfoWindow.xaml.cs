using BloodDonorApp.Models;
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
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow(string mail)
        {
            InitializeComponent();
            InitializeMail(mail);
            InitializeNames(mail);
        }

        public void InitializeMail(string mail)
        {
            txtmail.Text = mail;
        }

        public void InitializeNames(string mail)
        {
            BloodDonorEntities context = new BloodDonorEntities();

            List<Donator> donors = context.Donators.ToList();
            foreach(Donator donor in donors)
            {
                if(donor.email.Equals(mail))
                {
                    txtName.Items.Add(donor.nume);
                }
            }
        }
    }
}
