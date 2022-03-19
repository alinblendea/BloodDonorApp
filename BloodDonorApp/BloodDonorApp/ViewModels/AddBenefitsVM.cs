using BloodDonorApp.Commands;
using BloodDonorApp.Helpers;
using BloodDonorApp.Models.Actions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BloodDonorApp.ViewModels
{
    class AddBenefitsVM : BaseVM
    {
        BenefitsActions pAct;

        public AddBenefitsVM()
        {
            pAct = new BenefitsActions(this);
        }

        #region Data Members

        private int id_beneficiu;
        private string denumire;
        private int nr_total;
        private int nr_ramase;
        private int cost_per_buc;

        public int BenefitId
        {
            get
            {
                return id_beneficiu;
            }
            set
            {
                id_beneficiu = value;
                NotifyPropertyChanged("BenefitId");
            }
        }

        public string Name
        {
            get
            {
                return denumire;
            }
            set
            {
                denumire = value;
                NotifyPropertyChanged("Name");
            }
        }

        public int TotalNo
        {
            get
            {
                return nr_total;
            }
            set
            {
                nr_total = value;
                NotifyPropertyChanged("TotalNo");
            }
        }

        public int RemainingNo
        {
            get
            {
                return nr_ramase;
            }
            set
            {
                nr_ramase = value;
                NotifyPropertyChanged("RemainingNo");
            }
        }

        public int Cost
        {
            get
            {
                return cost_per_buc;
            }
            set
            {
                cost_per_buc = value;
                NotifyPropertyChanged("Cost");
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

        private ICommand back2Command;
        public ICommand Back2Command
        {
            get
            {
                if (back2Command == null)
                {
                    back2Command = new RelayCommand(pAct.Back2Method);
                }
                return back2Command;
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

        #endregion
    }
}
