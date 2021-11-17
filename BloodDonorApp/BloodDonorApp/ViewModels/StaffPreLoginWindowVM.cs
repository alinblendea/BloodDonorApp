using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Views;
using BloodDonorApp.Views.Register;

namespace BloodDonorApp.ViewModels
{
    class StaffPreLoginWindowVM
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
                    StaffPreLoginWindow mainWindow1 = (Application.Current.MainWindow as StaffPreLoginWindow);
                    Application.Current.MainWindow = new StaffLoginWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    StaffPreLoginWindow mainWindow2 = (Application.Current.MainWindow as StaffPreLoginWindow);
                    Application.Current.MainWindow = new StaffRegisterWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    StaffPreLoginWindow mainWindow = (Application.Current.MainWindow as StaffPreLoginWindow);
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
