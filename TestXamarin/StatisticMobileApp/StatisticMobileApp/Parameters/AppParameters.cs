using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticMobileApp.Parameters
{
    public static class AppParameters
    {
        private static Dictionary<string, object> parameters = new Dictionary<string, object>();

        public static void ChangeParameter(string key, object value)
        {
            if (parameters.ContainsKey(key))
                parameters[key] = value;
            else
                parameters.Add(key, value);
        }

        public static object GetParameter(string key)
        {
            if (parameters.ContainsKey(key))
                return parameters[key];
            else
                return null;
        }
    }
}
