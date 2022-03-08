using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BloodDonorApp.ViewModels;

namespace BloodDonorApp.Converters
{
    class ApproveConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            ApproveWindowVM pers = value as ApproveWindowVM;
            object[] result = new object[5] { pers.DonorCnp, pers.Greutate, pers.Puls, pers.Tensiune, pers.Grupa };
            return result;
        }
    }
}
