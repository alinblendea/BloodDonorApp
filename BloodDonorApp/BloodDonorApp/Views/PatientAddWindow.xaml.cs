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
    /// Interaction logic for PatientAddWindow.xaml
    /// </summary>
    public partial class PatientAddWindow : Window
    {
        public PatientAddWindow(string mail)
        {
            InitializeComponent();
            txtMail.Text = mail;
            txtGrupa.Items.Add("");
            txtGrupa.Items.Add("0-");
            txtGrupa.Items.Add("0+");
            txtGrupa.Items.Add("A-");
            txtGrupa.Items.Add("A+");
            txtGrupa.Items.Add("B-");
            txtGrupa.Items.Add("B+");
            txtGrupa.Items.Add("AB-");
            txtGrupa.Items.Add("AB+");

            BloodDonorEntities context = new BloodDonorEntities();
            List<Spital> hospitals = context.Spitals.ToList();
            txtHospital.Items.Clear();

            foreach (Spital spital in hospitals)
            {
                txtHospital.Items.Add(spital.denumire);
            }
        }
    }
}
