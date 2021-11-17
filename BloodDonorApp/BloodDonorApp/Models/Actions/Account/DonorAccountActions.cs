using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BloodDonorApp.Helpers;
using BloodDonorApp.ViewModels.Account;
using BloodDonorApp.Views.Register;
using BloodDonorApp.Views;

namespace BloodDonorApp.Models.Actions.Account
{
    class DonorAccountActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private DonorAccountVM donorAccountContext;
        public DonorAccountActions(DonorAccountVM donorAccountContext)
        {
            this.donorAccountContext = donorAccountContext;
        }

        public void AddMethod(object obj)
        {
            DonorAccountVM donorAccountVM = obj as DonorAccountVM;
            if (donorAccountVM != null)
            {
                if (String.IsNullOrEmpty(donorAccountVM.Email)
                    || String.IsNullOrEmpty(donorAccountVM.Password)
                    || String.IsNullOrEmpty(donorAccountVM.ConfirmPassword))
                {
                    donorAccountContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(donorAccountContext.Message);
                }
                else if(donorAccountVM.Password != donorAccountVM.ConfirmPassword)
                {
                    donorAccountContext.Message = "Parolele introduse nu coincid.";
                    MessageBox.Show(donorAccountContext.Message);
                }
                else 
                { 
                    donorAccountVM.Type = "Donor";
                    context.Conts.Add(new Cont() { id_cont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1, email = donorAccountVM.Email, parola = donorAccountVM.Password, type = "Donor"});
                    context.SaveChanges();
                    donorAccountContext.Message = "Cont creat cu succes!";
                    MessageBox.Show(donorAccountContext.Message);
                }
            }
        }

        public void BackMethod(object obj)
        {
            DonorRegisterWindow mainWindow = (Application.Current.MainWindow as DonorRegisterWindow);
            Application.Current.MainWindow = new DonorPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
