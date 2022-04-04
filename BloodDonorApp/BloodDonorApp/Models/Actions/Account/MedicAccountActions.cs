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
using System.Net.NetworkInformation;

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

        private bool CheckConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
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

            smtpClient.Send("noresponse.blooddonorapp@gmail.com", mail, "Activare cont medic", body);
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
                        if (CheckConnection())
                        {
                            int idcont = context.Conts.OrderByDescending(p => p.id_cont).FirstOrDefault().id_cont + 1;
                            int idspital = 0;
                            bool foundSpital = false;

                            List<Spital> spitale = context.Spitals.ToList();
                            foreach (Spital spital in spitale)
                            {
                                if (medicAccountVM.Hospital.Equals(spital.denumire))
                                {
                                    foundSpital = true;
                                    idspital = spital.id_spital;
                                    break;
                                }
                            }

                            if (!foundSpital)
                            {
                                idspital = context.Spitals.OrderByDescending(p => p.id_spital).FirstOrDefault().id_spital + 1;
                                context.Spitals.Add(new Spital() { id_spital = idspital, denumire = medicAccountVM.Hospital, judet = "BV" });
                            }

                            string code = GenerateCode();

                            SendCodeViaEmail(code, medicAccountVM.Email);

                            CodeConfirmMedicWindow confirmWindow = new CodeConfirmMedicWindow(code, medicAccountVM.Email, Encrypt(medicAccountVM.Password), "Medic", medicAccountVM.Name, idspital);
                            confirmWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Nu exista conexiune la internet.");
                        }
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
                            if (Decrypt(acc.parola).Equals(medicAccountVM.Password))
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

        public void ForgotMethod(object obj)
        {
            MedicLoginWindow mainWindow = (Application.Current.MainWindow as MedicLoginWindow);
            Application.Current.MainWindow = new ForgotPasswordWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
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
