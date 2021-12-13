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
    class DonorAccountActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private DonorAccountVM donorAccountContext;
        public DonorAccountActions(DonorAccountVM donorAccountContext)
        {
            this.donorAccountContext = donorAccountContext;
        }

        public void AddMethod(object obj)
        {
            DonorAccountVM donorAccountVM = obj as DonorAccountVM;
            if (donorAccountVM != null)
            {
                string email = donorAccountVM.Email;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);

                if (String.IsNullOrEmpty(donorAccountVM.Email)
                    || String.IsNullOrEmpty(donorAccountVM.Password)
                    || String.IsNullOrEmpty(donorAccountVM.ConfirmPassword))
                {
                    donorAccountContext.Message = "Toate datele trebuie completate.";
                    MessageBox.Show(donorAccountContext.Message);
                }
                else if(!match.Success)
                {
                    donorAccountContext.Message = "Mail-ul introdus este incorect.";
                    MessageBox.Show(donorAccountContext.Message);
                }
                else if(donorAccountVM.Password != donorAccountVM.ConfirmPassword)
                {
                    donorAccountContext.Message = "Parolele introduse nu coincid.";
                    MessageBox.Show(donorAccountContext.Message);
                }
                else 
                { 
                    donorAccountVM.Type = "Donor";
                    List<Cont> accounts = context.Conts.ToList();
                    bool alreadyExists = false;

                    foreach (Cont acc in accounts)
                    {
                        if (acc.email.Equals(donorAccountVM.Email))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        context.Conts.Add(new Cont() { id_cont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1, email = donorAccountVM.Email, parola = donorAccountVM.Password, type = "Donor" });
                        context.SaveChanges();
                        donorAccountContext.Message = "Cont creat cu succes!";
                        MessageBox.Show(donorAccountContext.Message);
                    }
                    else
                    {
                        donorAccountContext.Message = "Exista deja cont asociat acestei adrese.";
                        MessageBox.Show(donorAccountContext.Message);
                    }
                }
            }
        }

        public void LoginMethod(object obj)
        {
            DonorAccountVM donorAccountVM = obj as DonorAccountVM;
            if (donorAccountVM != null)
            {
                if (String.IsNullOrEmpty(donorAccountVM.Email)
                    || String.IsNullOrEmpty(donorAccountVM.Password))
                {
                    donorAccountContext.Message = "Introduceti email-ul si parola.";
                    MessageBox.Show(donorAccountContext.Message);
                }
                else
                {
                    List<Cont> accounts = context.Conts.ToList();
                    bool foundMail = false;
                    foreach (Cont acc in accounts)
                    {
                        if (acc.email.Equals(donorAccountVM.Email))
                        {
                            foundMail = true;
                            if (acc.parola.Equals(donorAccountVM.Password))
                            {
                                if(acc.type.Equals("Donor"))
                                {
                                    DonorLoginWindow mainWindow = (Application.Current.MainWindow as DonorLoginWindow);
                                    Application.Current.MainWindow = new DonorWindow(donorAccountVM.Email.ToString());
                                    Application.Current.MainWindow.Show();
                                    mainWindow.Close();
                                }
                                else
                                {
                                    donorAccountContext.Message = "Contul introdus nu este de donator.";
                                    MessageBox.Show(donorAccountContext.Message);
                                    break;
                                }
                            }
                            else
                            {
                                donorAccountContext.Message = "Parola incorecta.";
                                MessageBox.Show(donorAccountContext.Message);
                                break;
                            }
                        }
                    }
                    if(!foundMail)
                    {
                        donorAccountContext.Message = "Adresa de mail invalida.";
                        MessageBox.Show(donorAccountContext.Message);
                    }
                }
            }
        }

        public void BackMethod(object obj)
        {
            DonorRegisterWindow mainWindow = (Application.Current.MainWindow as DonorRegisterWindow);
            Application.Current.MainWindow = new DonorPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public void LogoutMethod(object obj)
        {
            DonorLoginWindow mainWindow = (Application.Current.MainWindow as DonorLoginWindow);
            Application.Current.MainWindow = new DonorPreLoginWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
