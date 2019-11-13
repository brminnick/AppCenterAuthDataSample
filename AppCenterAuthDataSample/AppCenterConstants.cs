
using System;
using Xamarin.Forms;

namespace AppCenterAuthDataSample
{
    public static class AppCenterConstants
    {
        public static string Key => GetKey();

        private static string GetKey() => Device.RuntimePlatform switch
        {
            Device.iOS => "de9fffc8-8b64-4f0c-8128-3053b212f26e",
            Device.Android => "8435e792-a539-4cb5-807d-292ab462a6fa",
            _ => throw new NotSupportedException()
        };
    }
}
