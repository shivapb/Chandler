using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ChandlerPro.Utils
{
    public static class ReClasser
    {
     
        public static object ModifyJsonObject<T>(this T objectToTransform)
        {
            var type = objectToTransform.GetType();
            var returnClass = new ExpandoObject() as IDictionary<string, object>;
            foreach (var propertyInfo in type.GetProperties())
            {
                var value = propertyInfo.GetValue(objectToTransform);
                var valueIsNotAString = !(value is string && !string.IsNullOrEmpty(value.ToString()));
                if (valueIsNotAString && value != null)
                {
                    returnClass.Add(propertyInfo.Name, value);
                }
            }
            return returnClass;
        }

        public static string FormateQty(string qty)
        {
            string[] result = qty.Split('.');

            string str1, str2;
            str1 = result[0];

            if(result.Length>1)
            {
                return str1;
            }
            else
            {
                return qty;
            }
            
        }
    }
}
