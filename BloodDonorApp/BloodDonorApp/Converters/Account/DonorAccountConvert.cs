using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BloodDonorApp.ViewModels.Account;

namespace BloodDonorApp.Converters.Account
{
    class DonorAccountConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null)
            {
                return new DonorAccountVM()
                {
                    Email = values[0].ToString(),
                    Password = values[1].ToString(),
                    ConfirmPassword = values[2].ToString()
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            DonorAccountVM pers = value as DonorAccountVM;
            object[] result = new object[4] { pers.Email, pers.Password, pers.ConfirmPassword, pers.Type };
            return result;
        }
    }
}
