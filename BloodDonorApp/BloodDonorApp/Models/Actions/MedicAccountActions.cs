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
    class MedicAccountActions
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private MedicAccountVM medicAccountContext;
        public MedicAccountActions(MedicAccountVM medicAccountContext)
        {
            this.medicAccountContext = medicAccountContext;
        }

        public void AddMethod(object obj)
        {
            MedicAccountVM medicAccountVM = obj as MedicAccountVM;
            if (medicAccountVM != null)
            {
                if (String.IsNullOrEmpty(medicAccountVM.Email)
                    || String.IsNullOrEmpty(medicAccountVM.Password)
                    || String.IsNullOrEmpty(medicAccountVM.ConfirmPassword))
                {
                   medicAccountContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else if (medicAccountVM.Password != medicAccountVM.ConfirmPassword)
                {
                    medicAccountContext.Message = "Parolele introduse nu coincid.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else
                {
                    medicAccountVM.Type = "Medic";
                    context.Conts.Add(new Cont() { id_cont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1, email = medicAccountVM.Email, parola = medicAccountVM.Password, type = "Medic" });
                    context.SaveChanges();
                    medicAccountContext.Message = "Cont creat cu succes!";
                    MessageBox.Show(medicAccountContext.Message);
                }
            }
        }

        public void BackMethod(object obj)
        {
            MedicRegisterWindow mainWindow = (Application.Current.MainWindow as MedicRegisterWindow);
            Application.Current.MainWindow = new MedicPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
