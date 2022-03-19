using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Views.Register;
using BloodDonorApp.Views.LoginMenu;
using BloodDonorApp.Views.Login;
using BloodDonorApp.Views;
using System.Diagnostics;
using BloodDonorApp.Models;

namespace BloodDonorApp.ViewModels
{
    class CheckPartsWindowVM
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
                    CheckPartsWindow mainWindow1 = (Application.Current.MainWindow as CheckPartsWindow);
                    Application.Current.MainWindow = new MedicWindow(mainWindow1.txtMail.Text);
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    Process.GetCurrentProcess().Kill();
                    break;
            }
        }
    }
}
