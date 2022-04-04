using BloodDonorApp.Commands;
using BloodDonorApp.Models;
using BloodDonorApp.Views.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BloodDonorApp.ViewModels.Account
{
    class ChangePasswordWindowVM
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

        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    BloodDonorEntities context = new BloodDonorEntities();
                    ChangePasswordWindow mainWindow = (Application.Current.MainWindow as ChangePasswordWindow);

                    if (String.IsNullOrEmpty(mainWindow.txtPassword.Text)
                    || String.IsNullOrEmpty(mainWindow.txtConfirm.Text))
                    {
                        MessageBox.Show("Toate datele trebuie completate.");
                    }
                    else if (mainWindow.txtPassword.Text.Length < 6)
                    {
                        MessageBox.Show("Parola nu poate fi mai mica de 6 caractere.");
                    }
                    else if (mainWindow.txtPassword.Text != mainWindow.txtConfirm.Text)
                    {
                        MessageBox.Show("Parolele introduse nu coincid.");
                    }
                    else
                    {
                        List<Cont> accounts = context.Conts.ToList();

                        foreach (Cont acc in accounts)
                        {
                            if (acc.email.Equals(mainWindow.txtMail.Text))
                            {
                                acc.parola = Encrypt(mainWindow.txtPassword.Text);
                                break;
                            }
                        }
                        context.SaveChanges();

                        MessageBox.Show("Parola a fost schimbata cu succes.");

                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                        mainWindow.Close();
                    }
                    break;

                case "2":
                    ChangePasswordWindow mainWindow1 = (Application.Current.MainWindow as ChangePasswordWindow);
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    mainWindow1.Close();
                    break;
            }
        }
    }
}
