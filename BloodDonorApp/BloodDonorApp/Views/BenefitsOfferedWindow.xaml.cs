using BloodDonorApp.Models;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    /// Interaction logic for BenefitsOfferedWindow.xaml
    /// </summary>
    public partial class BenefitsOfferedWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public BenefitsOfferedWindow()
        {
            InitializeComponent();
            InitializePie();
            DataContext = this;
        }


        private void InitializePie()
        {
            BloodDonorEntities database = new BloodDonorEntities();
            List<Benefit> benefits = database.Benefits.ToList();
            SeriesCollection = new SeriesCollection();
            bool skippedFirstEntry = false;

            foreach(Benefit benefit in benefits)
            {
                if (skippedFirstEntry)
                {
                    double totalCost = (double)benefit.cost_per_buc * benefit.nr_total;
                    string title = benefit.denumire.Substring(0, benefit.denumire.IndexOf(" "));
                    SeriesCollection.Add(
                    new PieSeries
                    {
                        Title = title,
                        Values = new ChartValues<ObservableValue> { new ObservableValue(totalCost) },
                        DataLabels = true
                    });
                }
                else
                {
                    skippedFirstEntry = true;
                }
            }
        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("Compania " + chartPoint.SeriesView.Title + " ofera produse in valoare de " + chartPoint.Y + " RON (" + String.Format("{0:0.00}", chartPoint.Participation * 100) + "% din totalul oferit");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BenefitsOfferedWindow mainWindow = (Application.Current.MainWindow as BenefitsOfferedWindow);
            Application.Current.MainWindow = new StatsOptionsWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
