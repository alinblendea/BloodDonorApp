using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Views.Register;
using BloodDonorApp.Views;

namespace BloodDonorApp.ViewModels
{
    class DonorPreLoginWindowVM
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
                    DonorPreLoginWindow mainWindow1 = (Application.Current.MainWindow as DonorPreLoginWindow);
                    Application.Current.MainWindow = new DonorLoginWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    DonorPreLoginWindow mainWindow2 = (Application.Current.MainWindow as DonorPreLoginWindow);
                    Application.Current.MainWindow = new DonorRegisterWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    DonorPreLoginWindow mainWindow = (Application.Current.MainWindow as DonorPreLoginWindow);
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
