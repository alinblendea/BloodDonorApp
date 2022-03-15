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
    class MedicalFormActions : BaseVM
    {
        BloodDonorEntities context = new BloodDonorEntities();

        private MedicalFormVM medicalFormContext;
        public MedicalFormActions(MedicalFormVM medicalFormContext)
        {
            this.medicalFormContext = medicalFormContext;
        }

        public bool nothingEmpty(MedicalFormVM medicalFormVM)
        {
            if (String.IsNullOrEmpty(medicalFormVM.Name)
                    || String.IsNullOrEmpty(medicalFormVM.DonorCnp)
                    || String.IsNullOrEmpty(medicalFormVM.Domiciliu)
                    || String.IsNullOrEmpty(medicalFormVM.Resedinta)
                    || String.IsNullOrEmpty(medicalFormVM.Email)
                    || String.IsNullOrEmpty(medicalFormVM.PhoneNr))
                return false;
            return true;
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

        public bool correctAge(string cnp)
        {
            String dateOfBirthString = null;

            if (cnp[0] == '1' || cnp[0] == '2')
            {
                dateOfBirthString = "19" +
                    cnp[1] + cnp[2] + "/" +
                    cnp[3] + cnp[4] + "/" +
                    cnp[5] + cnp[6];
            }
            else
            {
                dateOfBirthString = "20" +
                    cnp[1] + cnp[2] + "/" +
                    cnp[3] + cnp[4] + "/" +
                    cnp[5] + cnp[6];
            }
            DateTime dob;
            try
            {
                dob = Convert.ToDateTime(dateOfBirthString);
            }
            catch (System.FormatException)
            {
                return false;
            }

            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age = age - 1;

            if (age > 60 || age < 18)
                return false;

            return true;
        }

        public bool correctWeight(string weight)
        {
            if (Int32.Parse(weight) < 60)
                return false;

            return true;
        }

        public bool donorExists(string cnp)
        {
            List<Donator> donors = context.Donators.ToList();

            foreach (Donator donor in donors)
                if (donor.cnp_donator.Equals(cnp))
                    return true;

            return false;
        }

        public bool canDonate(string cnp)
        {
            List<DateTime> datesDonated = new List<DateTime>();
            List<Donare> donations = context.Donares.ToList();
            foreach (Donare donation in donations)
            {
                if (donation.cnp_donator.Equals(cnp))
                {
                    if (donation.isDone == false)
                        return false;
                    else
                    {
                        datesDonated.Add(donation.data);
                    }
                }
            }

            datesDonated.OrderByDescending(d => d);

            if (datesDonated[datesDonated.Count - 1].AddMonths(6) > DateTime.Now)
                return false;

            return true;
        }

        public void addDonor(MedicalFormVM medicalFormVM, int option)
        {
            string message = null;
            switch (option)
            {
                case 1:
                    message = "Chestionar trimis cu succes!";
                    break;

                case 2:
                    message = "Pacientul introdus nu se afla in baza de date. A fost creata o cerere de programare la donare fara pacient.";
                    break;
            }

            context.Donators.Add(new Donator() { cnp_donator = medicalFormVM.DonorCnp, nume = medicalFormVM.Name, domiciliu = medicalFormVM.Domiciliu, resedinta = medicalFormVM.Resedinta, email = medicalFormVM.Email, telefon = medicalFormVM.PhoneNr, grupa_sanguina = medicalFormVM.Grupa });
            context.Chestionar_Medical.Add(new Chestionar_Medical() { id_chestionar = context.Chestionar_Medical.OrderByDescending(p => p.id_chestionar).FirstOrDefault().id_chestionar + 1, greutate = medicalFormVM.Greutate, puls = medicalFormVM.Puls, tensiune = medicalFormVM.Tensiune, interventii_chirurgicale_recente = medicalFormVM.Interventii, sarcina = medicalFormVM.Sarcina, alte_boli = medicalFormVM.AlteBoli, consum_grasimi = medicalFormVM.Grasimi, tratament = medicalFormVM.Tratament, aprobat = false, cnp_donator = medicalFormVM.DonorCnp, nume_pacient = medicalFormVM.PatientName, grupa_sanguina = medicalFormVM.Grupa });
            context.SaveChanges();
            MessageBox.Show(message);
            medicalFormContext.Message = "";

            DonateWindow mainWindow = (Application.Current.MainWindow as DonateWindow);
            Application.Current.MainWindow = new ThankYouWindow(medicalFormVM.Name.ToString());
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }

        public void AddMethod(object obj)
        {
            MedicalFormVM medicalFormVM = obj as MedicalFormVM;
            if (medicalFormVM != null)
            {
                if (!nothingEmpty(medicalFormVM))
                {
                    medicalFormContext.Message = "Toate datele obligatorii trebuie completate.";
                    MessageBox.Show(medicalFormContext.Message);
                }
                else
                {
                    if (correctCnp(medicalFormVM.DonorCnp))
                    {
                        if (correctAge(medicalFormVM.DonorCnp))
                        {
                            if (correctWeight(medicalFormVM.Greutate))
                            {
                                
                                if (!donorExists(medicalFormVM.DonorCnp))
                                {

                                    bool exists = false;
                                    String grupaPacient = "";
                                    List<Pacient> patients = context.Pacients.ToList();
                                    foreach (Pacient patient in patients)
                                    {
                                        if (patient.nume.Equals(medicalFormVM.PatientName))
                                        {
                                            exists = true;
                                            grupaPacient = patient.grupa_sanguina;
                                            break;
                                        }
                                    }

                                    if (exists)
                                    {
                                        if (grupaPacient.Equals(medicalFormVM.Grupa))
                                        {
                                            addDonor(medicalFormVM, 1);
                                        }
                                        else
                                        {
                                            medicalFormContext.Message = "Pacientul nu are aceeasi grupa de sange cu dumneavoastra.";
                                            MessageBox.Show(medicalFormContext.Message);
                                        }
                                    }
                                    else
                                    {
                                        medicalFormVM.PatientName = "";
                                        addDonor(medicalFormVM, 2);
                                    }
                                }
                                else
                                {
                                    if(canDonate(medicalFormVM.DonorCnp))
                                    {
                                        bool exists = false;
                                        String grupaPacient = "";
                                        List<Pacient> patients = context.Pacients.ToList();
                                        foreach (Pacient patient in patients)
                                        {
                                            if (patient.nume.Equals(medicalFormVM.PatientName))
                                            {
                                                exists = true;
                                                grupaPacient = patient.grupa_sanguina;
                                                break;
                                            }
                                        }

                                        if (exists)
                                        {
                                            if (grupaPacient.Equals(medicalFormVM.Grupa))
                                            {
                                                addDonor(medicalFormVM, 1);
                                            }
                                            else
                                            {
                                                medicalFormContext.Message = "Pacientul nu are aceeasi grupa de sange cu dumneavoastra.";
                                                MessageBox.Show(medicalFormContext.Message);
                                            }
                                        }
                                        else
                                        {
                                            medicalFormVM.PatientName = "";
                                            addDonor(medicalFormVM, 2);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Asteptati sa primiti aprobarea ultimei cereri de donare sau asteptati sa treaca 6 luni de la ultima dumneavoastra donare. Va multumim pentru implicare!");
                                        medicalFormContext.Message = "";
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nu aveti greutatea necesara donarii de sange.");
                                medicalFormContext.Message = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nu aveti varsta necesara donarii de sange.");
                            medicalFormContext.Message = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("CNP Incorect!");
                        medicalFormContext.Message = "";
                    }
                }
            }
        }

        public void BackMethod(object obj)
        {
            DonateWindow mainWindow = (Application.Current.MainWindow as DonateWindow);
            Application.Current.MainWindow = new DonorWindow(mainWindow.txtMail.Text.ToString());
            Application.Current.MainWindow.Show();
            mainWindow.Close();
        }
    }
}

