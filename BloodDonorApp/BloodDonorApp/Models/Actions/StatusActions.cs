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
    class StatusActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private StatusWindowVM donationContext;
        public StatusActions(StatusWindowVM donationContext)
        {
            this.donationContext = donationContext;
        }

        public void CompleteMethod(object obj)
        {
            StatusWindowVM donVM = obj as StatusWindowVM;
            
            if (donVM != null)
            {
                context.ApproveDonation(donVM.DonorCnp, true);
                
                context.SaveChanges();
                ChangeStatusWindow mainWindow = (Application.Current.MainWindow as ChangeStatusWindow);
                Application.Current.MainWindow = new ChangeStatusWindow();
                Application.Current.MainWindow.Show();
                mainWindow.Close();
            }
            else
            {
                MessageBox.Show("Nu a fost selectata nicio donare.");
            }
        }

        public void BackMethod(object obj)
        {
            ChangeStatusWindow mainWindow = (Application.Current.MainWindow as ChangeStatusWindow);
            Application.Current.MainWindow = new StaffWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public ObservableCollection<StatusWindowVM> AllDonations()
        {
            List<Donare> dons = context.Donares.ToList();
            ObservableCollection<StatusWindowVM> result = new ObservableCollection<StatusWindowVM>();

            foreach (Donare don in dons)
            {
                if (don.isDone == false)
                {
                    result.Add(new StatusWindowVM()
                    {
                        DonorCnp = don.cnp_donator,
                        Data = don.data,
                        PatientName = don.nume_pacient,
                        Grupa = don.grupa_sanguina,
                        Completed = don.isDone
                    });
                }
            }
            result.OrderByDescending(d => d.Data);
            return result;
        }
    }
}
