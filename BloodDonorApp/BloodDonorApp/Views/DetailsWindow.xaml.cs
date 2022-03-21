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
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow(string mail, string name, string cnp)
        {
            InitializeComponent(); 
            InitializeMail(mail);
            InitializeInfo(name, cnp);
        }

        public void InitializeMail(string mail)
        {
            txtmail.Text = mail;
        }
    
        public void InitializeInfo(string name, string cnp)
        {
            txtName.Text = name;

            int donationsNumber = 0;
            int contravaloare = 0;
            List<int?> benefitIds = new List<int?>();

            BloodDonorEntities context = new BloodDonorEntities();

            List<Donare> donations = context.Donares.ToList();
            foreach(Donare donation in donations)
            {
                if(donation.cnp_donator.Equals(cnp) && donation.isDone == true)
                {
                    donationsNumber++;
                    benefitIds.Add(donation.id_beneficiu);
                }
            }

            txtNo.Text = donationsNumber.ToString();
            txtBenefits.Content = "-";

            List<Benefit> benefits = context.Benefits.ToList();
            foreach(int? id in benefitIds)
            {
                txtBenefits.Content = txtBenefits.Content + benefits[(int)id].denumire + "\n";
                contravaloare += (int)benefits[(int)id].cost_per_buc;
            }

            txtPrice.Text = contravaloare.ToString();
        }
    }
}
