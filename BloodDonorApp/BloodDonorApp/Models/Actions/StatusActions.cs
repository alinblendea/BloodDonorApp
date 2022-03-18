using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BloodDonorApp.Helpers;
using BloodDonorApp.ViewModels;
using BloodDonorApp.Views;

namespace BloodDonorApp.Models.Actions
{
    class StatusActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private StatusWindowVM donationContext;
        public StatusActions(StatusWindowVM donationContext)
        {
            this.donationContext = donationContext;
        }

        public void CompleteMethod(object obj)
        {
            StatusWindowVM donVM = obj as StatusWindowVM;
            
            if (donVM != null)
            {

                ChangeStatusWindow mainWindow = (Application.Current.MainWindow as ChangeStatusWindow);
                if (!String.IsNullOrEmpty(mainWindow.txtQuantity.Text) && mainWindow.comboBenefit.SelectedItem != null)
                {
                    try
                    {
                        int idBen = 0;
                        List<Benefit> benefits = context.Benefits.ToList();
                        foreach (Benefit benefit in benefits)
                        {
                            if (benefit.denumire.Equals(mainWindow.comboBenefit.SelectedItem))
                            {
                                idBen = benefit.id_beneficiu;
                                if(idBen != 0)
                                    benefit.nr_ramase--;
                                break;
                            }
                        }

                        context.ApproveDonation(donVM.DonorCnp, true, Int32.Parse(mainWindow.txtQuantity.Text), idBen);
                        context.SaveChanges();
                        Application.Current.MainWindow = new ChangeStatusWindow();
                        Application.Current.MainWindow.Show();
                        mainWindow.Close();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("Valoare cantitate invalida.");
                    }
                }
                else
                {
                    MessageBox.Show("Cantitatea donata si beneficiul primit trebuie introduse inainte de completarea donarii.");
                }
            }
            else
            {
                MessageBox.Show("Nu a fost selectata nicio donare.");
            }
        }

        public void BackMethod(object obj)
        {
            ChangeStatusWindow mainWindow = (Application.Current.MainWindow as ChangeStatusWindow);
            Application.Current.MainWindow = new StaffWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public ObservableCollection<StatusWindowVM> AllDonations()
        {
            List<Donare> dons = context.Donares.ToList();
            ObservableCollection<StatusWindowVM> result = new ObservableCollection<StatusWindowVM>();

            foreach (Donare don in dons)
            {
                if (don.isDone == false)
                {
                    result.Add(new StatusWindowVM()
                    {
                        DonorCnp = don.cnp_donator,
                        Data = don.data,
                        PatientName = don.nume_pacient,
                        Grupa = don.grupa_sanguina,
                        Completed = don.isDone
                    });
                }
            }
            result.OrderByDescending(d => d.Data);
            return result;
        }
    }
}
