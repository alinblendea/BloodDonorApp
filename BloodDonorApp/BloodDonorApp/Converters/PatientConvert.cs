using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BloodDonorApp.ViewModels;

namespace BloodDonorApp.Converters
{
    class PatientConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null)
            {
                return new PatientVM()
                {
                    PatientCnp = values[0].ToString(),
                    Name = values[1].ToString(),
                    HospitalName = values[2].ToString(),
                    Grupa = values[3].ToString(),
                    HospitalId = 0
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            PatientVM pers = value as PatientVM;
            object[] result = new object[5] { pers.PatientCnp, pers.Name, pers.HospitalName, pers.Grupa, pers.HospitalId };
            return result;
        }
    }
}
