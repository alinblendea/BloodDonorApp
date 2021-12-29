using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BloodDonorApp.Helpers;
using BloodDonorApp.ViewModels.Account;
using BloodDonorApp.Views.Register;
using BloodDonorApp.Views.LoginMenu;
using BloodDonorApp.Views.Login;
using BloodDonorApp.Views;
using System.Text.RegularExpressions;

namespace BloodDonorApp.Models.Actions.Account
{
    class MedicAccountActions
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private MedicAccountVM medicAccountContext;
        public MedicAccountActions(MedicAccountVM medicAccountContext)
        {
            this.medicAccountContext = medicAccountContext;
        }

        public void AddMethod(object obj)
        {
            MedicAccountVM medicAccountVM = obj as MedicAccountVM;
            if (medicAccountVM != null)
            {
                string email = medicAccountVM.Email;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);

                if (String.IsNullOrEmpty(medicAccountVM.Email)
                    || String.IsNullOrEmpty(medicAccountVM.Password)
                    || String.IsNullOrEmpty(medicAccountVM.ConfirmPassword)
                    || String.IsNullOrEmpty(medicAccountVM.Name)
                    || String.IsNullOrEmpty(medicAccountVM.Hospital))
                {
                   medicAccountContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else if (!match.Success)
                {
                    medicAccountContext.Message = "Mail-ul introdus este incorect.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else if (medicAccountVM.Password.Length < 6)
                {
                    medicAccountContext.Message = "Parola nu poate fi mai mica de 6 caractere.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else if (medicAccountVM.Password != medicAccountVM.ConfirmPassword)
                {
                    medicAccountContext.Message = "Parolele introduse nu coincid.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else
                {
                    medicAccountVM.Type = "Medic";
                    List<Cont> accounts = context.Conts.ToList();
                    bool alreadyExists = false;

                    foreach (Cont acc in accounts)
                    {
                        if (acc.email.Equals(medicAccountVM.Email))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        int idcont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1;
                        int idspital = 0;
                        bool foundSpital = false;

                        List <Spital> spitale = context.Spitals.ToList();
                        foreach(Spital spital in spitale)
                        {
                            if(medicAccountVM.Hospital.Equals(spital.denumire))
                            {
                                foundSpital = true;
                                idspital = spital.id_spital;
                                break;
                            }
                        }

                        if(!foundSpital)
                        {
                            idspital = context.Spitals.OrderByDescending(p => p.id_spital).FirstOrDefault().id_spital + 1;
                            context.Spitals.Add(new Spital() { id_spital = idspital, denumire = medicAccountVM.Hospital, judet = "BV" });
                        }

                        context.Conts.Add(new Cont() { id_cont = idcont, email = medicAccountVM.Email, parola = medicAccountVM.Password, type = "Medic" });
                        context.Medics.Add(new Medic() { id_medic = context.Medics.OrderByDescending(p => p.id_medic).FirstOrDefault().id_medic + 1, email = medicAccountVM.Email, nume = medicAccountVM.Name, id_cont = idcont, id_spital = idspital });
                        context.SaveChanges();
                        medicAccountContext.Message = "Cont creat cu succes!";
                        MessageBox.Show(medicAccountContext.Message);
                    }
                    else
                    {
                        medicAccountContext.Message = "Exista deja cont asociat acestei adrese.";
                        MessageBox.Show(medicAccountContext.Message);
                    }
                }
            }
        }

        public void LoginMethod(object obj)
        {
            MedicAccountVM medicAccountVM = obj as MedicAccountVM;
            if (medicAccountVM != null)
            {
                if (String.IsNullOrEmpty(medicAccountVM.Email)
                    || String.IsNullOrEmpty(medicAccountVM.Password))
                {
                    medicAccountContext.Message = "Introduceti email-ul si parola.";
                    MessageBox.Show(medicAccountContext.Message);
                }
                else
                {
                    List<Cont> accounts = context.Conts.ToList();
                    bool foundMail = false;
                    foreach (Cont acc in accounts)
                    {
                        if (acc.email.Equals(medicAccountVM.Email))
                        {
                            foundMail = true;
                            if (acc.parola.Equals(medicAccountVM.Password))
                            {
                                if (acc.type.Equals("Medic"))
                                {
                                    MedicLoginWindow mainWindow = (Application.Current.MainWindow as MedicLoginWindow);
                                    Application.Current.MainWindow = new MedicWindow(acc.email);
                                    Application.Current.MainWindow.Show();
                                    mainWindow.Close();
                                }
                                else
                                {
                                    medicAccountContext.Message = "Contul introdus nu este de medic.";
                                    MessageBox.Show(medicAccountContext.Message);
                                    break;
                                }
                            }
                            else
                            {
                                medicAccountContext.Message = "Parola incorecta.";
                                MessageBox.Show(medicAccountContext.Message);
                                break;
                            }
                        }
                    }
                    if (!foundMail)
                    {
                        medicAccountContext.Message = "Adresa de mail invalida.";
                        MessageBox.Show(medicAccountContext.Message);
                    }
                }
            }
        }

        public void BackMethod(object obj)
        {
            MedicRegisterWindow mainWindow = (Application.Current.MainWindow as MedicRegisterWindow);
            Application.Current.MainWindow = new MedicPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public void LogoutMethod(object obj)
        {
            MedicLoginWindow mainWindow = (Application.Current.MainWindow as MedicLoginWindow);
            Application.Current.MainWindow = new MedicPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
