using BloodDonorApp.Helpers;
using BloodDonorApp.ViewModels;
using BloodDonorApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BloodDonorApp.Models.Actions
{
    class BenefitsActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private AddBenefitsVM benefitContext;
        public BenefitsActions(AddBenefitsVM benefitContext)
        {
            this.benefitContext = benefitContext;
        }

        public void AddMethod(object obj)
        {
            BenefitAddWindow mainWindow = (Application.Current.MainWindow as BenefitAddWindow);
            if(!String.IsNullOrEmpty(mainWindow.txtCompany.Text) &&
               !String.IsNullOrEmpty(mainWindow.txtName.Text) &&
               !String.IsNullOrEmpty(mainWindow.txtNr.Text) &&
               !String.IsNullOrEmpty(mainWindow.txtCost.Text))
            {
                string company = mainWindow.txtCompany.Text;
                string name = mainWindow.txtName.Text;
                int nr = 0, cost = 0;

                try
                {
                    nr = Int32.Parse(mainWindow.txtNr.Text);
                    cost = Int32.Parse(mainWindow.txtCost.Text);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Datele au fost introduse gresit!");
                    return;
                }

                bool exists = false;
                List<Benefit> benefits = context.Benefits.ToList();
                foreach (Benefit benefit in benefits)
                {
                    if (benefit.denumire.Equals(company + " " + name) && benefit.cost_per_buc == cost)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    context.Benefits.Add(new Benefit() { id_beneficiu = context.Benefits.OrderByDescending(p => p.id_beneficiu).FirstOrDefault().id_beneficiu + 1, denumire = company + " " + name, nr_total = nr, nr_ramase = nr, cost_per_buc = cost });
                    context.SaveChanges();
                    MessageBox.Show("Beneficiul a fost adaugat cu succes!");
                }
                else
                {
                    MessageBox.Show("Beneficiul introdus exista deja in baza de date.");
                }
            }
            else
            {
                MessageBox.Show("Toate datele trebuie completate!");
            }
        }

        public void BackMethod(object obj)
        {
            BenefitAddWindow mainWindow = (Application.Current.MainWindow as BenefitAddWindow);
            Application.Current.MainWindow = new BenefitsWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
