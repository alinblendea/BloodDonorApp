using BloodDonorApp.Models;
using BloodDonorApp.ViewModels;
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
    /// Interaction logic for CodeConfirmWindow.xaml
    /// </summary>
    public partial class CodeConfirmWindow : Window
    {
        private string generated;
        private string mail;
        private string password;
        private string type;
        BloodDonorEntities context; 

        public CodeConfirmWindow(string code, string m, string p, string t)
        {
            InitializeComponent();
            context = new BloodDonorEntities();
            generated = code;
            mail = m;
            password = p;
            type = t;
        }

        private bool checkCode(string entered)
        {
            if (entered.Equals(generated))
                return true;
            return false;
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if(checkCode(txtCode.Text))
            {
                context.Conts.Add(new Cont() { id_cont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1, email = mail, parola = password, type = type });
                context.SaveChanges();
                MessageBox.Show("Cont creat cu succes! Puteti inchide aceasta fereastra.");
            }
            else
            {
                MessageBox.Show("Cod incorect.");
            }
        }
    }
}
