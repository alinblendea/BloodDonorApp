using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BloodDonorApp.Helpers;
using BloodDonorApp.ViewModels;
using BloodDonorApp.Views;

namespace BloodDonorApp.Models.Actions
{
    class StaffAccountActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private StaffAccountVM staffAccountContext;
        public StaffAccountActions(StaffAccountVM staffAccountContext)
        {
            this.staffAccountContext = staffAccountContext;
        }

        public void AddMethod(object obj)
        {
            StaffAccountVM staffAccountVM = obj as StaffAccountVM;
            if (staffAccountVM != null)
            {
                if (String.IsNullOrEmpty(staffAccountVM.Email)
                    || String.IsNullOrEmpty(staffAccountVM.Password)
                    || String.IsNullOrEmpty(staffAccountVM.ConfirmPassword))
                {
                    staffAccountContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else if (staffAccountVM.Password != staffAccountVM.ConfirmPassword)
                {
                    staffAccountContext.Message = "Parolele introduse nu coincid.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else
                {
                    staffAccountVM.Type = "Staff";
                    context.Conts.Add(new Cont() { id_cont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1, email = staffAccountVM.Email, parola = staffAccountVM.Password, type = "Staff" });
                    context.SaveChanges();
                    staffAccountContext.Message = "Cont creat cu succes!";
                    MessageBox.Show(staffAccountContext.Message);
                }
            }
        }

        public void BackMethod(object obj)
        {
            StaffRegisterWindow mainWindow = (Application.Current.MainWindow as StaffRegisterWindow);
            Application.Current.MainWindow = new StaffPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
