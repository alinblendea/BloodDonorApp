using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BloodDonorApp.Views.Login
{
    /// <summary>
    /// Interaction logic for CodeValidationWindow.xaml
    /// </summary>
    public partial class CodeValidationWindow : Window
    {
        private string generated;

        public CodeValidationWindow(string mail)
        {
            InitializeComponent();
            InitializeMail(mail);
            generated = GenerateCode();
            SendMail(mail, generated);
        }

        private void InitializeMail(string mail)
        {
            txtMail.Text = mail;
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

        private void SendMail(string mail, string code)
        {
            var smtpClient = new SmtpClient("smtp.mail.yahoo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("alinblendea@yahoo.com", "pdbbuabfmgzxaoua"),
                EnableSsl = true,
            };

            string body = "[nu raspundeti acestui e-mail]\n\n\n" +
                "Buna ziua!\n\n" +
                "Codul de resetare a parolei contului dumneavoastra este: " + code + "\n\n" + 
                "Daca nu ati facut solicitarea schimbarii parolei, ignorati acest mail.";

            smtpClient.Send("alinblendea@yahoo.com", mail, "Schimbare parola Blood Donor App", body);
        }

        private bool CheckCode(string entered)
        {
            if (entered.Equals(generated))
                return true;
            return false;
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckCode(txtCode.Text))
            {
                CodeValidationWindow mainWindow = (Application.Current.MainWindow as CodeValidationWindow);
                Application.Current.MainWindow = new ChangePasswordWindow(txtMail.Text);
                Application.Current.MainWindow.Show();
                mainWindow.Close();
            }
            else
            {
                MessageBox.Show("Cod incorect");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CodeValidationWindow mainWindow = (Application.Current.MainWindow as CodeValidationWindow);
            Application.Current.MainWindow = new ForgotPasswordWindow();
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}
