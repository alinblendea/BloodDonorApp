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
    class HospitalActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private HospitalVM hospitalContext;
        public HospitalActions(HospitalVM hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }

        public void RefreshMethod(object obj)
        {

            HospitalAddWindow mainWindow1 = (Application.Current.MainWindow as HospitalAddWindow);
            List<Spital> hospitals = context.Spitals.ToList();
            mainWindow1.comboHospital.Items.Clear();

            foreach (Spital spital in hospitals)
            {                    
                mainWindow1.comboHospital.Items.Add(spital.denumire);
            }
        }

        public void AddMethod(object obj)
        {
            HospitalVM hospitalVM = obj as HospitalVM;
            if (hospitalVM != null)
            {
                if (String.IsNullOrEmpty(hospitalVM.County)
                    || String.IsNullOrEmpty(hospitalVM.HospitalName))
                {
                    hospitalContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(hospitalContext.Message);
                }
                else
                {
                    bool alreadyExists = false;
                    List<Spital> hospitals = context.Spitals.ToList();

                    foreach (Spital spital in hospitals)
                    {
                        if (spital.denumire.Equals(hospitalVM.HospitalName))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        context.Spitals.Add(new Spital() { id_spital = context.Spitals.OrderByDescending(p => p.id_spital).FirstOrDefault().id_spital + 1, denumire = hospitalVM.HospitalName, judet = hospitalVM.County });
                        context.SaveChanges();
                        MessageBox.Show("Spital adaugat cu succes!");
                        hospitalContext.Message = "";
                    }
                    else
                    {
                        hospitalContext.Message = "Spitalul se afla deja in baza de date.";
                        MessageBox.Show(hospitalContext.Message);
                    }
                }
            }
        }

        public void DeleteMethod(object obj)
        {

        }

        public void BackMethod(object obj)
        {
            HospitalAddWindow mainWindow = (Application.Current.MainWindow as HospitalAddWindow);
            Application.Current.MainWindow = new StaffWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
