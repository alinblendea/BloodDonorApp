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
    class HospitalVM : BaseVM
    {
        HospitalActions pAct;

        public HospitalVM()
        {
            pAct = new HospitalActions(this);
        }

        private int id_spital;
        private string denumire;
        private string judet;
        private string message;

        #region Data Members

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

        public string HospitalName
        {
            get
            {
                return denumire;
            }
            set
            {
                denumire = value;
                NotifyPropertyChanged("HospitalName");
            }
        }
        public string County
        {
            get
            {
                return judet;
            }
            set
            {
                judet = value;
                NotifyPropertyChanged("County");
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
