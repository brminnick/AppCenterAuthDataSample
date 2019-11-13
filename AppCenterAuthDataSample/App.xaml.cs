using Microsoft.AppCenter;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Data;
using Xamarin.Forms;

namespace AppCenterAuthDataSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() => AppCenter.Start(AppCenterConstants.Key, typeof(Data), typeof(Auth));
    }
}
