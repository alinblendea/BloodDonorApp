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
    class BenefitsWindowVM
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
                    BenefitsWindow mainWindow1 = (Application.Current.MainWindow as BenefitsWindow);
                    Application.Current.MainWindow = new BenefitAddWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    BenefitsWindow mainWindow2 = (Application.Current.MainWindow as BenefitsWindow);
                    Application.Current.MainWindow = new BenefitIncreaseWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    BenefitsWindow mainWindow = (Application.Current.MainWindow as BenefitsWindow);
                    Application.Current.MainWindow = new StaffWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
