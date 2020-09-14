using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public class ThemeHelper
    {
        public const string DynamicMaterialTheme = nameof(DynamicMaterialTheme);
        public const string DynamicPrimaryTextColor = nameof(DynamicPrimaryTextColor);
        public const string DynamicSecondaryTextColor = nameof(DynamicSecondaryTextColor);
        public const string DynamicBackgroundColor = nameof(DynamicBackgroundColor);
       

        public static void SetDynamicResource(string targetResourceName, string sourceResourceName)
        {
            if (!Application.Current.Resources.TryGetValue(sourceResourceName, out var value))
            {
                throw new InvalidOperationException($"key {sourceResourceName} not found in the resource dictionary");
            }

            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetDynamicResource<T>(string targetResourceName, T value)
        {
            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetDarkMode()
        {
            SetDynamicResource(DynamicPrimaryTextColor, "PrimaryLight");
            SetDynamicResource(DynamicBackgroundColor, "DarkSurface");
        }

        public static void SetLightMode()
        {
            SetDynamicResource(DynamicPrimaryTextColor, "PrimaryTextColor");
            SetDynamicResource(DynamicBackgroundColor, "PrimaryLight");
        }
    }
}
