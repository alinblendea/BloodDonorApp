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
    class BloodBagWindowVM
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
                    BloodBagWindow mainWindow1 = (Application.Current.MainWindow as BloodBagWindow);
                    Application.Current.MainWindow = new BloodBagAddWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    BloodBagWindow mainWindow2 = (Application.Current.MainWindow as BloodBagWindow);
                    Application.Current.MainWindow = new SendBloodBagWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    BloodBagWindow mainWindow = (Application.Current.MainWindow as BloodBagWindow);
                    Application.Current.MainWindow = new StaffWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
