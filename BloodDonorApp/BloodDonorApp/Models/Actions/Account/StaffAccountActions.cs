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
    class StaffAccountActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private StaffAccountVM staffAccountContext;
        public StaffAccountActions(StaffAccountVM staffAccountContext)
        {
            this.staffAccountContext = staffAccountContext;
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

            smtpClient.Send("noresponse.blooddonorapp@gmail.com", mail, "Activare cont personal recoltare", body);
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
                        string code = GenerateCode();

                        SendCodeViaEmail(code, staffAccountVM.Email);

                        CodeConfirmStaffWindow confirmWindow = new CodeConfirmStaffWindow(code, staffAccountVM.Email, Encrypt(staffAccountVM.Password), "Staff", staffAccountVM.Name);
                        confirmWindow.Show();
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
                            if (Decrypt(acc.parola).Equals(staffAccountVM.Password))
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
