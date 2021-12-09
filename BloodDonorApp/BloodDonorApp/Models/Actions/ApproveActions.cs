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
    class ApproveActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private ApproveWindowVM formContext;
        public ApproveActions(ApproveWindowVM formContext)
        {
            this.formContext = formContext;
        }

        public void AprobareMethod(object obj)
        {
            ApproveWindowVM formVM = obj as ApproveWindowVM;

            if (formVM != null)
            {
                //formVM.Aprobat = true;

                context.SaveChanges();
                ApproveWindow mainWindow1 = (Application.Current.MainWindow as ApproveWindow);
                formContext.FormsList = AllForms();
            }
        }

        public void BackMethod(object obj)
        {
            ApproveWindow mainWindow = (Application.Current.MainWindow as ApproveWindow);
            Application.Current.MainWindow = new StaffWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public void RefreshMethod(object obj)
        {
            ApproveWindowVM formVM = obj as ApproveWindowVM;

            if (formVM != null)
            {
                formContext.FormsList = AllForms();
            }
        }

        public ObservableCollection<ApproveWindowVM> AllForms()
        {
            List<Chestionar_Medical> forms = context.Chestionar_Medical.ToList();
            ObservableCollection<ApproveWindowVM> result = new ObservableCollection<ApproveWindowVM>();

            foreach (Chestionar_Medical form in forms)
            {
                string mail = "";
                List<Donator> donors = context.Donators.ToList();

                foreach(Donator donor in donors)
                {
                    if (donor.cnp_donator.Equals(form.cnp_donator))
                        mail = donor.email;
                }

                result.Add(new ApproveWindowVM()
                {
                    DonorCnp = form.cnp_donator,
                    Mail = mail,
                    Greutate = form.greutate,
                    Puls = form.puls,
                    Tensiune = form.tensiune,
                    Interventii = form.interventii_chirurgicale_recente,
                    Sarcina = form.sarcina,
                    Grasimi = form.consum_grasimi,
                    Tratament = form.tratament,
                    AlteBoli = form.alte_boli,
                    Aprobat = form.aprobat
                }); 
            }
            return result;
        }
    }
}
