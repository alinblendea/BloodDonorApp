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
    /// Interaction logic for BenefitsRemainingWindow.xaml
    /// </summary>
    public partial class BenefitsRemainingWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public BenefitsRemainingWindow()
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

            foreach (Benefit benefit in benefits)
            {
                if (skippedFirstEntry)
                {
                    int remained = benefit.nr_ramase;
                    string title = benefit.denumire.Substring(0, benefit.denumire.IndexOf(" "));
                    SeriesCollection.Add(
                    new PieSeries
                    {
                        Title = title,
                        Values = new ChartValues<ObservableValue> { new ObservableValue(remained) },
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
            MessageBox.Show("Compania " + chartPoint.SeriesView.Title + " mai are disponibile un numar de " + chartPoint.Y + " recompense.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BenefitsRemainingWindow mainWindow = (Application.Current.MainWindow as BenefitsRemainingWindow);
            Application.Current.MainWindow = new StatsOptionsWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
