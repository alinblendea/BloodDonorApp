using BloodDonorApp.Commands;
using BloodDonorApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BloodDonorApp.ViewModels
{
    class DetailsWindowVM
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
                    DetailsWindow mainWindow2 = (Application.Current.MainWindow as DetailsWindow);
                    Application.Current.MainWindow = new InfoWindow(mainWindow2.txtmail.Text);
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;
            }
        }
    }
}
