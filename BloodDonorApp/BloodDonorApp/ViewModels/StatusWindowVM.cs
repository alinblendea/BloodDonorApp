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
    class StatusWindowVM : BaseVM
    {
        StatusActions pAct;

        public StatusWindowVM()
        {
            pAct = new StatusActions(this);
        }

        #region Data Members

        private string cnp_donator;
        private System.DateTime data;
        private bool completed;
        private string nume_pacient;
        private string message;
        private ObservableCollection<StatusWindowVM> donationsList;

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

        public string PatientName
        {
            get
            {
                return nume_pacient;
            }
            set
            {
                nume_pacient = value;
                NotifyPropertyChanged("PatientName");
            }
        }

        public System.DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                NotifyPropertyChanged("Data");
            }
        }

        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
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

        public ObservableCollection<StatusWindowVM> DonationsList
        {
            get
            {
                if (Application.Current.MainWindow.Title.Equals("Blood Donor - Complete Donation"))
                {
                    ChangeStatusWindow thisWindow = (Application.Current.MainWindow as ChangeStatusWindow);
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

        private ICommand completeCommand;
        public ICommand CompleteCommand
        {
            get
            {
                if (completeCommand == null)
                {
                    completeCommand = new RelayCommand(pAct.CompleteMethod);
                }
                return completeCommand;
            }
        }

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

        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(pAct.RefreshMethod);
                }
                return refreshCommand;
            }
        }

        #endregion
    }
}
