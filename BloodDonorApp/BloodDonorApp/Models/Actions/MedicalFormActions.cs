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
    class MedicalFormActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private MedicalFormVM medicalFormContext;
        public MedicalFormActions(MedicalFormVM medicalFormContext)
        {
            this.medicalFormContext = medicalFormContext;
        }

        public void AddMethod(object obj)
        {
            MedicalFormVM medicalFormVM = obj as MedicalFormVM;
            if (medicalFormVM != null)
            {
                if (String.IsNullOrEmpty(medicalFormVM.Name) 
                    || String.IsNullOrEmpty(medicalFormVM.DonorCnp) 
                    || String.IsNullOrEmpty(medicalFormVM.Domiciliu)
                    || String.IsNullOrEmpty(medicalFormVM.Resedinta)
                    || String.IsNullOrEmpty(medicalFormVM.Email)
                    || String.IsNullOrEmpty(medicalFormVM.PhoneNr))
                {
                    medicalFormContext.Message = "Toate datele trebuie completate.";
                }
                else
                {
                    context.Donators.Add(new Donator() { cnp_donator = medicalFormVM.DonorCnp, nume = medicalFormVM.Name, domiciliu = medicalFormVM.Domiciliu, resedinta = medicalFormVM.Resedinta, email = medicalFormVM.Email, telefon = medicalFormVM.PhoneNr, id_chestionar = medicalFormVM.FormId });
                    context.Chestionar_Medical.Add(new Chestionar_Medical() { id_chestionar = medicalFormVM.FormId, greutate = 0, puls = 0, tensiune = "0/0", interventii_chirurgicale_recente = false, sarcina = false, alte_boli = false, consum_grasimi = false, tratament = false });
                    context.SaveChanges();
                    medicalFormContext.Message = "";
                }
            }
        }

        public void BackMethod(object obj)
        {
            DonateWindow mainWindow = (Application.Current.MainWindow as DonateWindow);
            Application.Current.MainWindow = new DonorWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}

