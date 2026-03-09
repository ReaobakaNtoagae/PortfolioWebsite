using PROG7312PRACTICE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows;


namespace PROG7312PRACTICE
{
    public static class CategoryExtension
    {
        public static string ToLocalizedString(this Category category)
        {
            string key = $"Category_{category}";
            if (Application.Current.Resources.Contains(key))
            {
                return Application.Current.Resources[key].ToString();
            }
            return category.ToString(); 
        }
    }
}
