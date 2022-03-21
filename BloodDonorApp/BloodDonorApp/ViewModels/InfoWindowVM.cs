using BloodDonorApp.Commands;
using BloodDonorApp.Models;
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
    class InfoWindowVM
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
                    InfoWindow mainWindow1 = (Application.Current.MainWindow as InfoWindow);

                    bool ok = false;
                    List<Donator> donors = context.Donators.ToList();
                    foreach (Donator donor in donors)
                    {
                        if (donor.nume.Equals(mainWindow1.txtName.Text))
                        {
                            if(donor.cnp_donator.Equals(mainWindow1.txtCnp.Text))
                            {
                                ok = true;
                            }
                        }
                    }
                    if (ok)
                    {
                        Application.Current.MainWindow = new DetailsWindow(mainWindow1.txtmail.Text, mainWindow1.txtName.Text, mainWindow1.txtCnp.Text);
                        Application.Current.MainWindow.Show();
                        mainWindow1.Close();
                    }
                    else
                    {
                        MessageBox.Show("CNP incorect. Nu se pot afisa informatiile.");
                    }
                    break;

                case "2":
                    InfoWindow mainWindow2 = (Application.Current.MainWindow as InfoWindow);
                    Application.Current.MainWindow = new DonorWindow(mainWindow2.txtmail.Text);
                    Application.Current.MainWindow.Show();
                    mainWindow2.Close();
                    break;
            }
        }
    }
}
