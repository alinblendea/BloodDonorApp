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
    /// Interaction logic for VolumeDonatedWindow.xaml
    /// </summary>
    public partial class VolumeDonatedWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public VolumeDonatedWindow()
        {
            InitializeComponent();
            InitializePie();
            DataContext = this;
        }

        private void InitializePie()
        {
            BloodDonorEntities database = new BloodDonorEntities();
            List<Donare> donations = database.Donares.ToList();
            SeriesCollection = new SeriesCollection();
            double total = 0;

            List<String> types = new List<String> { "0-", "0+", "A-", "A+", "B-", "B+", "AB-", "AB+" };
            List<double> volume = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (Donare donation in donations)
            {
                if (donation.isDone)
                {
                    volume[types.IndexOf(donation.grupa_sanguina)] += (double)donation.cantitate_ml / 1000;
                    total += (double)donation.cantitate_ml / 1000;
                }
            }

            foreach (String type in types)
            {
                if (volume[types.IndexOf(type)] != 0)
                {
                  SeriesCollection.Add(
                  new PieSeries
                  {
                      Title = type,
                      Values = new ChartValues<ObservableValue> { new ObservableValue(volume[types.IndexOf(type)]) },
                      DataLabels = true
                  });
                }
            }

            txtNumber.Text = total.ToString();
        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("Pentru grupa sanguina " + chartPoint.SeriesView.Title + " s-au donat pana acum " + chartPoint.Y + " litri de sange (" + String.Format("{0:0.00}", chartPoint.Participation * 100) + "% din totalul donat.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VolumeDonatedWindow mainWindow = (Application.Current.MainWindow as VolumeDonatedWindow);
            Application.Current.MainWindow = new StatsOptionsWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
