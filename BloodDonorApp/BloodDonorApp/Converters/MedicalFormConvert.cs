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
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null && values[5] != null && values[6] != null)
            {
                return new MedicalFormVM()
                {
                    DonorCnp = values[0].ToString(),
                    Name = values[1].ToString(),
                    Domiciliu = values[2].ToString(),
                    Resedinta = values[3].ToString(),
                    Email = values[4].ToString(),
                    PhoneNr = values[5].ToString()
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
            object[] result = new object[7] { pers.DonorCnp, pers.Name, pers.Domiciliu, pers.Resedinta, pers.Email, pers.PhoneNr, pers.FormId};
            return result;
        }
    }
}
