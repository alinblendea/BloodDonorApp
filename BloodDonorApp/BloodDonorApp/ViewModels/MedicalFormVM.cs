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
        private string alte_boli;
        private string greutate;
        private string puls;
        private string tensiune;
        private bool tratament;
        private bool interventii;
        private bool sarcina;
        private bool grasimi;
        private int id_chestionar;
        private string nume_pacient;
        private string grupa_sanguina;
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

