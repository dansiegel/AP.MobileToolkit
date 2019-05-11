﻿using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace AP.MobileToolkit.Extensions
{
    public static class INavigationParametersExtensions
    {
        public static IDictionary<string, string> ToErrorParameters(this INavigationParameters navigationParameters, string uri, params (string key, object value)[] extraParameters)
        {
            var dict = new Dictionary<string, string>();

            if(navigationParameters != null)
            {
                dict.Add("navigationParameters", JsonConvert.SerializeObject(navigationParameters));
            }

            dict.Add("uri", uri);

            if(extraParameters.Any())
            {
                foreach(var (key, value) in extraParameters)
                {
                    if(value is string str)
                    {
                        dict.Add(key, str);
                    }
                    else
                    {
                        dict.Add(key, JsonConvert.SerializeObject(value));
                    }
                }
            }

            return dict;
        }

        public static IDictionary<string, string> ToErrorParameters(this INavigationParameters parameters, string uri, Exception ex, params (string key, object value)[] extraParameters)
        {
            var dict = parameters.ToErrorParameters(uri, extraParameters);

            dict.Add("Exception", ex.GetType().Name);
            dict.Add("StackTrace", ex.ToString());
            return dict;
        }
    }
}