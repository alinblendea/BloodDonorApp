using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BloodDonorApp.ViewModels;

namespace BloodDonorApp.Converters
{
    class RequestFormConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null)
            {
                return new RequestFormVM()
                {
                    GrupaSanguina = values[0].ToString(),
                    Plasma = (bool)values[1],
                    Trombocite = (bool)values[2],
                    GlobuleRosii = (bool)values[3],
                    Email = values[4].ToString()
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            RequestFormVM pers = value as RequestFormVM;
            object[] result = new object[7] { pers.FormId, pers.Status, pers.GrupaSanguina, pers.Trombocite, pers.GlobuleRosii, pers.Plasma, pers.MedicId };
            return result;
        }
    }
}
