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
    class RequestFormActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private RequestFormVM requestFormContext;
        public RequestFormActions(RequestFormVM requestFormContext)
        {
            this.requestFormContext = requestFormContext;
        }

        public void AddMethod(object obj)
        {
            RequestFormVM requestFormVM = obj as RequestFormVM;
            if (requestFormVM != null)
            {
                if (String.IsNullOrEmpty(requestFormVM.GrupaSanguina))
                {
                    requestFormContext.Message = "Completati grupa sanguina!";
                    MessageBox.Show(requestFormContext.Message);
                }
                else
                {
                    int idmedic = 0;
                    List<Medic> medici = context.Medics.ToList();
                    foreach(Medic medic in medici)
                    {
                        if (medic.email.Equals(requestFormVM.Email))
                        {
                            idmedic = medic.id_medic;
                            break;
                        }
                    }
                    context.Cerere_Donare.Add(new Cerere_Donare() { id_cerere = context.Cerere_Donare.OrderByDescending(p => p.id_cerere).FirstOrDefault().id_cerere + 1, status = "NOT DONE", grupa_sanguina = requestFormVM.GrupaSanguina, trombocite = requestFormVM.Trombocite, globule_rosii = requestFormVM.GlobuleRosii, plasma = requestFormVM.Plasma, id_medic = idmedic });
                    context.SaveChanges();
                    MessageBox.Show("Cerere inregistrata cu succes!");
                    requestFormContext.Message = "";
                }
            }
        }

        public void BackMethod(object obj)
        {
            RequestWindow mainWindow = (Application.Current.MainWindow as RequestWindow);
            Application.Current.MainWindow = new MedicWindow(mainWindow.txtMail.Text.ToString());
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
