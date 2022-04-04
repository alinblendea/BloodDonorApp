using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Helpers;
using BloodDonorApp.Models.Actions;
using BloodDonorApp.Models.Actions.Account;

namespace BloodDonorApp.ViewModels.Account
{
    class StaffAccountVM : BaseVM
    {
        StaffAccountActions pAct;

        public StaffAccountVM()
        {
            pAct = new StaffAccountActions(this);
        }

        #region Data Members

        private int id_cont;
        private string email;
        private string parola;
        private string confirmedPassword;
        private string name;
        private string type;
        private string message;

        public int AccId
        {
            get
            {
                return id_cont;
            }
            set
            {
                id_cont = value;
                NotifyPropertyChanged("AccId");
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

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Password
        {
            get
            {
                return parola;
            }
            set
            {
                parola = value;
                NotifyPropertyChanged("Password");
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return confirmedPassword;
            }
            set
            {
                confirmedPassword = value;
                NotifyPropertyChanged("ConfirmPassword");
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
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

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(pAct.LoginMethod);
                }
                return loginCommand;
            }
        }

        private ICommand forgotCommand;
        public ICommand ForgotCommand
        {
            get
            {
                if (forgotCommand == null)
                {
                    forgotCommand = new RelayCommand(pAct.ForgotMethod);
                }
                return forgotCommand;
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

        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new RelayCommand(pAct.LogoutMethod);
                }
                return logoutCommand;
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(ExitMethod);
                }
                return exitCommand;
            }
        }

        public void ExitMethod(object obj)
        {
            Process.GetCurrentProcess().Kill();
        }

        #endregion
    }
}
