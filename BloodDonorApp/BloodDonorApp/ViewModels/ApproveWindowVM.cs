using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    class ApproveWindowVM : BaseVM
    {
        ApproveActions pAct;

        public ApproveWindowVM()
        {
            pAct = new ApproveActions(this);
        }

        #region Data Members

        private string cnp_donator;
        private string mail;
        private string greutate;
        private string puls;
        private string tensiune;
        private bool interventii;
        private bool sarcina;
        private bool grasimi;
        private bool tratament;
        private string alte_boli;
        private bool aprobat;
        private string nume_pacient;
        private string grupa_sanguina;
        private DateTime approved_date;
        private string message;
        private ObservableCollection<ApproveWindowVM> formsList;

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

        public string Grupa
        {
            get
            {
                return grupa_sanguina;
            }
            set
            {
                grupa_sanguina = value;
                NotifyPropertyChanged("Grupa");
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

        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
                NotifyPropertyChanged("Mail");
            }
        }

        public string Greutate
        {
            get
            {
                return greutate;
            }
            set
            {
                greutate = value;
                NotifyPropertyChanged("Greutate");
            }
        }

        public string Puls
        {
            get
            {
                return puls;
            }
            set
            {
                puls = value;
                NotifyPropertyChanged("Puls");
            }
        }

        public string Tensiune
        {
            get
            {
                return tensiune;
            }
            set
            {
                tensiune = value;
                NotifyPropertyChanged("Tensiune");
            }
        }

        public bool Interventii
        {
            get
            {
                return interventii;
            }
            set
            {
                interventii = value;
                NotifyPropertyChanged("Interventii");
            }
        }

        public bool Sarcina
        {
            get
            {
                return sarcina;
            }
            set
            {
                sarcina = value;
                NotifyPropertyChanged("Sarcina");
            }
        }

        public bool Grasimi
        {
            get
            {
                return grasimi;
            }
            set
            {
                grasimi = value;
                NotifyPropertyChanged("Grasimi");
            }
        }

        public bool Tratament
        {
            get
            {
                return tratament;
            }
            set
            {
                tratament = value;
                NotifyPropertyChanged("Tratament");
            }
        }

        public string AlteBoli
        {
            get
            {
                return alte_boli;
            }
            set
            {
                alte_boli = value;
                NotifyPropertyChanged("AlteBoli");
            }
        }

        public bool Aprobat
        {
            get
            {
                return aprobat;
            }
            set
            {
                aprobat = value;
                NotifyPropertyChanged("Aprobat");
            }
        }

        public DateTime DateApproved
        {
            get
            {
                return approved_date;
            }
            set
            {
                approved_date = value;
                NotifyPropertyChanged("DateApproved");
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

        public ObservableCollection<ApproveWindowVM> FormsList
        {
            get
            {
                if (Application.Current.MainWindow.Title.Equals("Blood Donor - Approve"))
                {
                    ApproveWindow thisWindow = (Application.Current.MainWindow as ApproveWindow);
                    formsList = pAct.AllForms();
                }
                else
                {
                    formsList = pAct.AllForms();
                }
                return formsList;
            }
            set
            {
                formsList = value;
                NotifyPropertyChanged("FormsList");
            }
        }

        #endregion

        #region Command Members

        private ICommand aprobareCommand;
        public ICommand AprobareCommand
        {
            get
            {
                if (aprobareCommand == null)
                {
                    aprobareCommand = new RelayCommand(pAct.AprobareMethod);
                }
                return aprobareCommand;
            }
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(pAct.UpdateMethod);
                }
                return updateCommand;
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
