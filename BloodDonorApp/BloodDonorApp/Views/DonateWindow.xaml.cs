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
    /// Interaction logic for DonateWindow.xaml
    /// </summary>
    public partial class DonateWindow : Window
    {
        public DonateWindow(string mail)
        {
            InitializeComponent();
            InitializeMail(mail);
            InitializeBloodTypes();
            InitializePatients();
        }

        public void InitializeMail(string mail)
        {
            txtMail.Text = mail;
        }

        public void InitializeBloodTypes()
        {
            txtGrupa.Items.Add("");
            txtGrupa.Items.Add("0-");
            txtGrupa.Items.Add("0+");
            txtGrupa.Items.Add("A-");
            txtGrupa.Items.Add("A+");
            txtGrupa.Items.Add("B-");
            txtGrupa.Items.Add("B+");
            txtGrupa.Items.Add("AB-");
            txtGrupa.Items.Add("AB+");
        }

        public void InitializePatients()
        {
            BloodDonorEntities context = new BloodDonorEntities();

            List<Pacient> patients = context.Pacients.ToList();
            txtPacient.Items.Clear();

            txtPacient.Items.Add("");

            foreach (Pacient pacient in patients)
            {
                txtPacient.Items.Add(pacient.nume);
            }
        }
    }
}
