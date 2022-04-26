using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Views;
using BloodDonorApp.Views.LoginMenu;

namespace BloodDonorApp.ViewModels
{
    class StatsOptionsWindowVM
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
                    StatsOptionsWindow mainWindow1 = (Application.Current.MainWindow as StatsOptionsWindow);
                    Application.Current.MainWindow = new BenefitsOfferedWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    StatsOptionsWindow mainWindow2 = (Application.Current.MainWindow as StatsOptionsWindow);
                    Application.Current.MainWindow = new BenefitsRemainingWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    StatsOptionsWindow mainWindow3 = (Application.Current.MainWindow as StatsOptionsWindow);
                    Application.Current.MainWindow = new VolumeDonatedWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow3.Close();
                    break;

                case "4":
                    StatsOptionsWindow mainWindow = (Application.Current.MainWindow as StatsOptionsWindow);
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}