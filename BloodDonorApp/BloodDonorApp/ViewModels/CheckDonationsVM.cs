using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Helpers;
using BloodDonorApp.Models.Actions;
using BloodDonorApp.Views;

namespace BloodDonorApp.ViewModels
{
    class CheckDonationsVM : BaseVM
    {
        CheckDonationsActions pAct;

        public CheckDonationsVM()
        {
            pAct = new CheckDonationsActions(this);
        }

        #region Data Members

        private string cnp_donator;
        private System.DateTime data;
        private bool isDone;
        private string message;
        private ObservableCollection<CheckDonationsVM> donationsList;

        public string DonorCnp
        {
            get
            {
                return cnp_donator;
            }
            set
            {
                cnp_donator = value;
                NotifyPropertyChanged("DonorCnp");
            }
        }

        public System.DateTime Date
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                NotifyPropertyChanged("Date");
            }
        }

        public bool Completed
        {
            get
            {
                return isDone;
            }
            set
            {
                isDone = value;
                NotifyPropertyChanged("Completed");
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public ObservableCollection<CheckDonationsVM> DonationsList
        {
            get
            {
                if (Application.Current.MainWindow.Title.Equals("Blood Donor - Check Donations"))
                {
                    CheckDonationsWindow thisWindow = (Application.Current.MainWindow as CheckDonationsWindow);
                    donationsList = pAct.AllDonations();
                }
                else
                {
                    donationsList = pAct.AllDonations();
                }
                return donationsList;
            }
            set
            {
                donationsList = value;
                NotifyPropertyChanged("DonationsList");
            }
        }

        #endregion

        #region Command Members

        private ICommand backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new RelayCommand(pAct.BackMethod);
                }
                return backCommand;
            }
        }

        private ICommand showCommand;
        public ICommand ShowCommand
        {
            get
            {
                if (showCommand == null)
                {
                    showCommand = new RelayCommand(pAct.ShowMethod);
                }
                return showCommand;
            }
        }

        #endregion
    }
}
