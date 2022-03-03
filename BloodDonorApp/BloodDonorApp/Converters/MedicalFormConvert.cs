using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BloodDonorApp.ViewModels;

namespace BloodDonorApp.Converters
{
    class MedicalFormConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null && values[5] != null && values[7] != null && values[8] != null && values[9] != null)
            {
                return new MedicalFormVM()
                {
                    DonorCnp = values[0].ToString(),
                    Name = values[1].ToString(),
                    Domiciliu = values[2].ToString(),
                    Resedinta = values[3].ToString(),
                    Email = values[4].ToString(),
                    PhoneNr = values[5].ToString(),
                    AlteBoli = values[6].ToString(),
                    Greutate = values[7].ToString(),
                    Puls = values[8].ToString(),
                    Tensiune = values[9].ToString(),
                    Interventii = (bool)values[10],
                    Sarcina = (bool)values[11],
                    Grasimi = (bool)values[12],
                    Tratament = (bool)values[13],
                    PatientName = values[14].ToString()
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            MedicalFormVM pers = value as MedicalFormVM;
            object[] result = new object[8] { pers.DonorCnp, pers.Name, pers.Domiciliu, pers.Resedinta, pers.Email, pers.PhoneNr, pers.FormId, pers.PatientName};
            return result;
        }
    }
}
