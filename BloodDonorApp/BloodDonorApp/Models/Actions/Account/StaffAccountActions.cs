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
    class StaffAccountActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private StaffAccountVM staffAccountContext;
        public StaffAccountActions(StaffAccountVM staffAccountContext)
        {
            this.staffAccountContext = staffAccountContext;
        }

        public void AddMethod(object obj)
        {
            StaffAccountVM staffAccountVM = obj as StaffAccountVM;
            if (staffAccountVM != null)
            {
                string email = staffAccountVM.Email;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);

                if (String.IsNullOrEmpty(staffAccountVM.Email)
                    || String.IsNullOrEmpty(staffAccountVM.Password)
                    || String.IsNullOrEmpty(staffAccountVM.ConfirmPassword)
                    || String.IsNullOrEmpty(staffAccountVM.Name))
                {
                    staffAccountContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else if (!match.Success)
                {
                    staffAccountContext.Message = "Mail-ul introdus este incorect.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else if (staffAccountVM.Password.Length < 6)
                {
                    staffAccountContext.Message = "Parola nu poate fi mai mica de 6 caractere.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else if (staffAccountVM.Password != staffAccountVM.ConfirmPassword)
                {
                    staffAccountContext.Message = "Parolele introduse nu coincid.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else
                {
                    staffAccountVM.Type = "Staff";
                    List<Cont> accounts = context.Conts.ToList();
                    bool alreadyExists = false;

                    foreach (Cont acc in accounts)
                    {
                        if (acc.email.Equals(staffAccountVM.Email))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        int idcont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1;
                        context.Personal_Recoltare.Add(new Personal_Recoltare() { id_personal = context.Personal_Recoltare.OrderByDescending(p => p.id_personal).FirstOrDefault().id_personal + 1, email = staffAccountVM.Email, nume = staffAccountVM.Name, id_cont = idcont });
                        context.Conts.Add(new Cont() { id_cont = idcont, email = staffAccountVM.Email, parola = staffAccountVM.Password, type = "Staff" });
                        context.SaveChanges();
                        staffAccountContext.Message = "Cont creat cu succes!";
                        MessageBox.Show(staffAccountContext.Message);
                    }
                    else
                    {
                        staffAccountContext.Message = "Exista deja cont asociat acestei adrese.";
                        MessageBox.Show(staffAccountContext.Message);
                    }
                }
            }
        }

        public void LoginMethod(object obj)
        {
            StaffAccountVM staffAccountVM = obj as StaffAccountVM;
            if (staffAccountVM != null)
            {
                if (String.IsNullOrEmpty(staffAccountVM.Email)
                    || String.IsNullOrEmpty(staffAccountVM.Password))
                {
                    staffAccountContext.Message = "Introduceti email-ul si parola.";
                    MessageBox.Show(staffAccountContext.Message);
                }
                else
                {
                    List<Cont> accounts = context.Conts.ToList();
                    bool foundMail = false;
                    foreach (Cont acc in accounts)
                    {
                        if (acc.email.Equals(staffAccountVM.Email))
                        {
                            foundMail = true;
                            if (acc.parola.Equals(staffAccountVM.Password))
                            {
                                if (acc.type.Equals("Staff"))
                                {
                                    StaffLoginWindow mainWindow = (Application.Current.MainWindow as StaffLoginWindow);
                                    Application.Current.MainWindow = new StaffWindow();
                                    Application.Current.MainWindow.Show();
                                    mainWindow.Close();
                                }
                                else
                                {
                                    staffAccountContext.Message = "Contul introdus nu este de personal de recoltare.";
                                    MessageBox.Show(staffAccountContext.Message);
                                    break;
                                }
                            }
                            else
                            {
                                staffAccountContext.Message = "Parola incorecta.";
                                MessageBox.Show(staffAccountContext.Message);
                                break;
                            }
                        }
                    }
                    if (!foundMail)
                    {
                        staffAccountContext.Message = "Adresa de mail invalida.";
                        MessageBox.Show(staffAccountContext.Message);
                    }
                }
            }
        }

        public void BackMethod(object obj)
        {
            StaffRegisterWindow mainWindow = (Application.Current.MainWindow as StaffRegisterWindow);
            Application.Current.MainWindow = new StaffPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public void LogoutMethod(object obj)
        {
            StaffLoginWindow mainWindow = (Application.Current.MainWindow as StaffLoginWindow);
            Application.Current.MainWindow = new StaffPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
