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
                ApproveWindow mainWindow1 = (Application.Current.MainWindow as ApproveWindow);

                if (String.IsNullOrEmpty(mainWindow1.txtDate.Text))
                {
                    MessageBox.Show("Introduceti data in care va fi programat donatorul.");
                }
                else
                {
                    DateTime date;
                    if(DateTime.TryParseExact(mainWindow1.txtDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                    {
                        context.ApproveForm(formVM.DonorCnp, true);

                        context.Donares.Add(new Donare() { id_donare = context.Donares.OrderByDescending(p => p.id_donare).FirstOrDefault().id_donare + 1, data = date, isDone = false, cnp_donator = formVM.DonorCnp, email = formVM.Mail });

                        context.SaveChanges();
                        formContext.FormsList = AllForms();
                    }
                    else
                    {
                        MessageBox.Show("Data nu a fost introdusa in formatul corect.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Nu a fost selectat niciun chestionar medical.");
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
            formContext.FormsList = AllForms();
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
