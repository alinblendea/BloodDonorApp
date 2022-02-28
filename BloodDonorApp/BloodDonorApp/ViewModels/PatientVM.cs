using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Helpers;
using BloodDonorApp.Models.Actions;

namespace BloodDonorApp.ViewModels
{
    class PatientVM : BaseVM
    {
        PatientActions pAct;

        public PatientVM()
        {
            pAct = new PatientActions(this);
        }

        private string cnp_pacient;
        private string nume;
        private string grupa_sanguina;
        private string nume_spital;
        private int id_spital;
        private string message;

        #region Data Members

        public string PatientCnp
        {
            get
            {
                return cnp_pacient;
            }
            set
            {
                cnp_pacient = value;
                NotifyPropertyChanged("PatientCnp");
            }
        }

        public string Name
        {
            get
            {
                return nume;
            }
            set
            {
                nume = value;
                NotifyPropertyChanged("Name");
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

        public string HospitalName
        {
            get
            {
                return nume_spital;
            }
            set
            {
                nume_spital = value;
                NotifyPropertyChanged("HospitalName");
            }
        }

        public int HospitalId
        {
            get
            {
                return id_spital;
            }
            set
            {
                id_spital = value;
                NotifyPropertyChanged("HospitalId");
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

        #endregion

        #region Command Members

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

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(pAct.AddMethod);
                }
                return addCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(pAct.DeleteMethod);
                }
                return deleteCommand;
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

        #endregion
    }
}
