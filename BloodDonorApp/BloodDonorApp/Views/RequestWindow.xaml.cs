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
    /// Interaction logic for RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        public RequestWindow(string mail)
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
        }
    }
}
