using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Views;
using BloodDonorApp.Views.Login;

namespace BloodDonorApp.ViewModels
{
    class DonorWindowVM
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
                    DonorWindow mainWindow1 = (Application.Current.MainWindow as DonorWindow);
                    Application.Current.MainWindow = new DonateWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    DonorWindow mainWindow2 = (Application.Current.MainWindow as DonorWindow);
                    Application.Current.MainWindow = new CheckDonationsWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    DonorWindow mainWindow = (Application.Current.MainWindow as DonorWindow);
                    Application.Current.MainWindow = new DonorLoginWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
