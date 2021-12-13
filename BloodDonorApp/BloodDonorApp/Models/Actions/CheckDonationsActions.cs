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
    class CheckDonationsActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private CheckDonationsVM donationContext;
        public CheckDonationsActions(CheckDonationsVM donationContext)
        {
            this.donationContext = donationContext;
        }

        public void BackMethod(object obj)
        {
            CheckDonationsWindow mainWindow = (Application.Current.MainWindow as CheckDonationsWindow);
            Application.Current.MainWindow = new DonorWindow(mainWindow.txtmail.Text);
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public void ShowMethod(object obj)
        {
            donationContext.DonationsList = AllDonations();   
        }

        public ObservableCollection<CheckDonationsVM> AllDonations()
        {
            CheckDonationsWindow mainWindow = (Application.Current.MainWindow as CheckDonationsWindow);

            List<Donare> donations = context.Donares.ToList();
            ObservableCollection<CheckDonationsVM> result = new ObservableCollection<CheckDonationsVM>();

            string mail = "";
            if(mainWindow != null)
                mail = mainWindow.txtmail.Text;

            foreach (Donare donation in donations)
            {
                if (donation.email.Equals(mail))
                {
                    result.Add(new CheckDonationsVM()
                    {
                        DonorCnp = donation.cnp_donator,
                        Date = donation.data,
                        Completed = donation.isDone
                    });
                }
            }
            return result;
        }
    }
}
