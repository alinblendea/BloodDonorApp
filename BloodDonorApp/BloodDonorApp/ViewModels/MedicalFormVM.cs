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
    class MedicalFormVM : BaseVM
    {
        MedicalFormActions pAct;

        public MedicalFormVM()
        {
            pAct = new MedicalFormActions(this);
        }

        #region Data Members

        private string cnp_donator;
        private string nume;
        private string domiciliu;
        private string resedinta;
        private string email;
        private string telefon;
        private int id_chestionar;
        private string message;

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

        public string Domiciliu
        {
            get
            {
                return domiciliu;
            }
            set
            {
                domiciliu = value;
                NotifyPropertyChanged("Domiciliu");
            }
        }

        public string Resedinta
        {
            get
            {
                return resedinta;
            }
            set
            {
                resedinta = value;
                NotifyPropertyChanged("Resedinta");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }

        public string PhoneNr
        {
            get
            {
                return telefon;
            }
            set
            {
                telefon = value;
                NotifyPropertyChanged("PhoneNr");
            }
        }

        public int FormId
        {
            get
            {
                return id_chestionar;
            }
            set
            {
                id_chestionar = value;
                NotifyPropertyChanged("FormId");
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

