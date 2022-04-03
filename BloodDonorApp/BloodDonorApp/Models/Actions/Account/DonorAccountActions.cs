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
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

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

        /* 
         * AES ENCRYPTED
         * 
         */

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private string GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        private void SendCodeViaEmail(string code, string mail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("noresponse.blooddonorapp@gmail.com", "036998aA."),
                EnableSsl = true,
            };

            string body = "[nu raspundeti acestui e-mail]\n\n\n" +
                "Buna ziua!\n\n" +
                "Codul de confirmare a adresei de mail este: " + code;

            smtpClient.Send("noresponse.blooddonorapp@gmail.com", mail, "Activare cont donator", body);
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
                else if(donorAccountVM.Password.Length < 6)
                {
                    donorAccountContext.Message = "Parola nu poate fi mai mica de 6 caractere.";
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
                        string code = GenerateCode();

                        SendCodeViaEmail(code, donorAccountVM.Email);

                        CodeConfirmWindow confirmWindow = new CodeConfirmWindow(code, donorAccountVM.Email, Encrypt(donorAccountVM.Password), "Donor");
                        confirmWindow.Show();
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
                            if (Decrypt(acc.parola).Equals(donorAccountVM.Password))
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
