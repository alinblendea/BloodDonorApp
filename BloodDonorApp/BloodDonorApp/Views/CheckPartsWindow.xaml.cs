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
    /// Interaction logic for CheckPartsWindow.xaml
    /// </summary>
    public partial class CheckPartsWindow : Window
    {
        public CheckPartsWindow(string mail)
        {
            InitializeComponent();
            txtMail.Text = mail;

            BloodDonorEntities context = new BloodDonorEntities();

            List<Cerere_Donare> cereri = context.Cerere_Donare.ToList();
            int nrPlasma = 0, nrTrombocite = 0, nrGlobule = 0;

            foreach (Cerere_Donare cerere in cereri)
            {
                if (cerere.plasma)
                    nrPlasma++;
                if (cerere.trombocite)
                    nrTrombocite++;
                if (cerere.globule_rosii)
                    nrGlobule++;
            }

            txtPlasma.Text = nrPlasma.ToString();
            txtPlatelets.Text = nrTrombocite.ToString();
            txtRedCells.Text = nrGlobule.ToString();
        }
    }
}
