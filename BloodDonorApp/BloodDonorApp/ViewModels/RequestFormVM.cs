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
    class RequestFormVM : BaseVM
    {
        RequestFormActions pAct;

        public RequestFormVM()
        {
            pAct = new RequestFormActions(this);
        }

        #region Data Members

        private int id_cerere;
        private string grupa_sanguina;
        private string email;
        private string status;
        private bool plasma;
        private bool trombocite;
        private bool globule_rosii;
        private int id_medic;
        private string message;

        public string GrupaSanguina
        {
            get
            {
                return grupa_sanguina;
            }
            set
            {
                grupa_sanguina = value;
                NotifyPropertyChanged("GrupaSanguina");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
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

        public bool Plasma
        {
            get
            {
                return plasma;
            }
            set
            {
                plasma = value;
                NotifyPropertyChanged("Plasma");
            }
        }

        public bool Trombocite
        {
            get
            {
                return trombocite;
            }
            set
            {
                trombocite = value;
                NotifyPropertyChanged("Trombocite");
            }
        }

        public bool GlobuleRosii
        {
            get
            {
                return globule_rosii;
            }
            set
            {
                globule_rosii = value;
                NotifyPropertyChanged("GlobuleRosii");
            }
        }

        public int MedicId
        {
            get
            {
                return id_medic;
            }
            set
            {
                id_medic = value;
                NotifyPropertyChanged("MedicId");
            }
        }

        public int FormId
        {
            get
            {
                return id_cerere;
            }
            set
            {
                id_cerere = value;
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
