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
    /// Interaction logic for BenefitIncreaseWindow.xaml
    /// </summary>
    public partial class BenefitIncreaseWindow : Window
    {
        public BenefitIncreaseWindow()
        {
            InitializeComponent();
            InitializeBenefits();
        }

        public void InitializeBenefits()
        {
            BloodDonorEntities context = new BloodDonorEntities();
            List<Benefit> benefits = context.Benefits.ToList();
            foreach (Benefit benefit in benefits)
            {
                if (benefit.nr_ramase != 0)
                    txtName.Items.Add(benefit.denumire);
            }
        }
    }
}
