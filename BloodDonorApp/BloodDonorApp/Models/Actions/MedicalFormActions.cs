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
                    MessageBox.Show(medicalFormContext.Message);
                }
                else
                {
                    bool alreadyExists = false;
                    List<Donator> donors = context.Donators.ToList();

                    foreach (Donator donor in donors)
                    {
                        if (donor.cnp_donator.Equals(medicalFormVM.DonorCnp))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        context.Donators.Add(new Donator() { cnp_donator = medicalFormVM.DonorCnp, nume = medicalFormVM.Name, domiciliu = medicalFormVM.Domiciliu, resedinta = medicalFormVM.Resedinta, email = medicalFormVM.Email, telefon = medicalFormVM.PhoneNr });
                    }
                    context.Chestionar_Medical.Add(new Chestionar_Medical() { id_chestionar = context.Chestionar_Medical.OrderByDescending(p => p.id_chestionar).FirstOrDefault().id_chestionar + 1, greutate = medicalFormVM.Greutate, puls = medicalFormVM.Puls, tensiune = medicalFormVM.Tensiune, interventii_chirurgicale_recente = medicalFormVM.Interventii, sarcina = medicalFormVM.Sarcina, alte_boli = medicalFormVM.AlteBoli, consum_grasimi = medicalFormVM.Grasimi, tratament = medicalFormVM.Tratament, aprobat = false, cnp_donator = medicalFormVM.DonorCnp });
                    context.SaveChanges();
                    MessageBox.Show("Chestionar trimis cu succes!");
                    medicalFormContext.Message = "";

                    DonateWindow mainWindow = (Application.Current.MainWindow as DonateWindow);
                    Application.Current.MainWindow = new ThankYouWindow(medicalFormVM.Name.ToString());
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                }
            }
        }

        public void BackMethod(object obj)
        {
            DonateWindow mainWindow = (Application.Current.MainWindow as DonateWindow);
            Application.Current.MainWindow = new DonorWindow(mainWindow.txtMail.Text.ToString());
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}

