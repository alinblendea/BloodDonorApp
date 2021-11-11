using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BloodDonorApp.Commands;
using BloodDonorApp.Views;

namespace BloodDonorApp.ViewModels
{
    class MedicWindowVM
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
                    MedicWindow mainWindow1 = (Application.Current.MainWindow as MedicWindow);
                    Application.Current.MainWindow = new RequestWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;

                case "2":
                    MedicWindow mainWindow2 = (Application.Current.MainWindow as MedicWindow);
                    Application.Current.MainWindow = new CheckPartsWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;

                case "3":
                    MedicWindow mainWindow = (Application.Current.MainWindow as MedicWindow);
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow.Close();
                    break;
            }
        }
    }
}
