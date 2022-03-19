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
    class PatientActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private PatientVM patientContext;
        public PatientActions(PatientVM patientContext)
        {
            this.patientContext = patientContext;
        }

        public bool correctCnp(string cnp)
        {
            if (cnp.Length != 13)
                return false;

            if (cnp[0] != '1' &&
                cnp[0] != '2' &&
                cnp[0] != '5' &&
                cnp[0] != '6')
                return false;

            foreach (char c in cnp)
                if (c < '0' || c > '9')
                    return false;

            return true;
        }

        public void AddMethod(object obj)
        {
            PatientVM patientVM = obj as PatientVM;
            if (patientVM != null)
            {
                PatientAddWindow mainWindow2 = (Application.Current.MainWindow as PatientAddWindow);
                if(String.IsNullOrEmpty(mainWindow2.txtHospital.Text))
                {
                    patientContext.Message = "Selectati spitalul din care face parte pacientul.";
                    MessageBox.Show(patientContext.Message);
                }
                else
                {
                    if (String.IsNullOrEmpty(patientVM.Name)
                        || String.IsNullOrEmpty(patientVM.PatientCnp)
                        || String.IsNullOrEmpty(patientVM.Grupa)
                        || String.IsNullOrEmpty(patientVM.HospitalName))
                    {
                        patientContext.Message = "Toate datele trebuie completate.";
                        MessageBox.Show(patientContext.Message);
                    }
                    else
                    {
                        if (correctCnp(patientVM.PatientCnp))
                        {
                            bool alreadyExists = false;
                            List<Pacient> patients = context.Pacients.ToList();

                            foreach (Pacient patient in patients)
                            {
                                if (patient.cnp_pacient.Equals(patientVM.PatientCnp))
                                {
                                    alreadyExists = true;
                                    break;
                                }
                            }
                            if (!alreadyExists)
                            {
                                int idSpital = 1;
                                List<Spital> spitale = context.Spitals.ToList();

                                foreach (Spital spital in spitale)
                                {
                                    if (spital.denumire == patientVM.HospitalName)
                                    {
                                        idSpital = spital.id_spital;
                                    }
                                }

                                context.Pacients.Add(new Pacient() { cnp_pacient = patientVM.PatientCnp, nume = patientVM.Name, grupa_sanguina = patientVM.Grupa, id_spital = idSpital, nume_spital = patientVM.HospitalName });

                                PatientAddWindow mainWindow = (Application.Current.MainWindow as PatientAddWindow);
                                List<Medic> medics = context.Medics.ToList();
                                int idMedic = 0;
                                foreach (Medic medic in medics)
                                {
                                    if (medic.email.Equals(mainWindow.txtMail.Text))
                                    {
                                        idMedic = medic.id_medic;
                                    }
                                }

                                bool tromb = (bool)mainWindow.checkTrombocite.IsChecked;
                                bool glob = (bool)mainWindow.checkGlobule.IsChecked;
                                bool plasm = (bool)mainWindow.checkPlasma.IsChecked;
                                context.Cerere_Donare.Add(new Cerere_Donare() { id_cerere = context.Cerere_Donare.OrderByDescending(p => p.id_cerere).FirstOrDefault().id_cerere + 1, status = "NOT DONE", grupa_sanguina = patientVM.Grupa, trombocite = tromb, globule_rosii = glob, plasma = plasm, id_medic = idMedic });
                                context.SaveChanges();
                                MessageBox.Show("Pacient inregistrat cu succes! O cerere pentru sange de grupa " + patientVM.Grupa + " a fost trimisa.");
                                patientContext.Message = "";
                            }
                            else
                            {
                                patientContext.Message = "Pacientul se afla deja in baza de date.";
                                MessageBox.Show(patientContext.Message);
                            }
                        }
                        else
                        {
                            patientContext.Message = "CNP incorect.";
                            MessageBox.Show(patientContext.Message);
                        }
                    }
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            PatientVM patientVM = obj as PatientVM;
            if (patientVM != null)
            {
                if (String.IsNullOrEmpty(patientVM.PatientCnp))
                {
                    patientContext.Message = "CNP-ul pacientului trebuie introdus pentru stergere.";
                    MessageBox.Show(patientContext.Message);
                }
                else
                {
                    bool alreadyExists = false;
                    List<Pacient> patients = context.Pacients.ToList();

                    foreach (Pacient patient in patients)
                    {
                        if (patient.cnp_pacient.Equals(patientVM.PatientCnp))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        patientContext.Message = "Pacientul cu CNP " + patientVM.PatientCnp + " nu poate fi sters pentru ca nu se afla in baza de date.";
                        MessageBox.Show(patientContext.Message);
                    }
                    else
                    {
                        context.DeletePatientByCNP(patientVM.PatientCnp);
                        context.SaveChanges();
                        patientContext.Message = "";
                        MessageBox.Show("Pacientul cu CNP " + patientVM.PatientCnp + " a fost sters din baza de date.");
                    }
                }
            }
        }

        public void BackMethod(object obj)
        {
            PatientAddWindow mainWindow = (Application.Current.MainWindow as PatientAddWindow);
            Application.Current.MainWindow = new MedicWindow(mainWindow.txtMail.Text.ToString());
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
