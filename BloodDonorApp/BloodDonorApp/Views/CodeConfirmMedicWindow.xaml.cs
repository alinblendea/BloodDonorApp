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
    /// Interaction logic for CodeConfirmMedicWindow.xaml
    /// </summary>
    public partial class CodeConfirmMedicWindow : Window
    {
        private string generated;
        private string mail;
        private string password;
        private string type;
        private string name;
        private int hospitalId;
        BloodDonorEntities context;

        public CodeConfirmMedicWindow(string code, string m, string p, string t, string n, int h)
        {
            InitializeComponent();
            context = new BloodDonorEntities();
            generated = code;
            mail = m;
            password = p;
            type = t;
            name = n;
            hospitalId = h;
        }

        private bool checkCode(string entered)
        {
            if (entered.Equals(generated))
                return true;
            return false;
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkCode(txtCode.Text))
            {
                int idcont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1;

                context.Conts.Add(new Cont() { id_cont = idcont, email = mail, parola = password, type = type });
                context.Medics.Add(new Medic() { id_medic = context.Medics.OrderByDescending(p => p.id_medic).FirstOrDefault().id_medic + 1, email = mail, nume = name, id_cont = idcont, id_spital = hospitalId });
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
