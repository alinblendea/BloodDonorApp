using BloodDonorApp.Commands;
using BloodDonorApp.Models;
using BloodDonorApp.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BloodDonorApp.ViewModels.Account
{
    class ForgotPasswordWindowVM
    {
        private ICommand openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (openWindowCommand == null)
                {
                    openWindowCommand = new RelayCommand(OpenWindow);
                }
                return openWindowCommand;
            }
        }

        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    BloodDonorEntities context = new BloodDonorEntities();
                    ForgotPasswordWindow mainWindow1 = (Application.Current.MainWindow as ForgotPasswordWindow);
                    bool found = false;

                    List<Cont> accounts = context.Conts.ToList();
                    foreach(Cont cont in accounts)
                    {
                        if (cont.email.Equals(mainWindow1.txtMail.Text))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        
                        Application.Current.MainWindow = new CodeValidationWindow(mainWindow1.txtMail.Text);
                        Application.Current.MainWindow.Show();
                        mainWindow1.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nu exista cont asociat acestei adrese de mail.");
                    }

                    break;

                case "2":
                    ForgotPasswordWindow mainWindow = (Application.Current.MainWindow as ForgotPasswordWindow);
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
