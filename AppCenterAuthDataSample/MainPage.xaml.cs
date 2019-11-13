
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Auth;
using Microsoft.AppCenter.Data;
using Xamarin.Forms;

namespace AppCenterAuthDataSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage() => InitializeComponent();

        async void HandleSignInButtonClicked(object sender, EventArgs e)
        {
            OutputLabel.Text = "Signing In User";

            var userInfo = await Auth.SignInAsync();

            OutputLabel.Text = $"Signed In As Id: {userInfo.AccountId}";
        }

        async void HandleGetUserDataButtonClicked(object sender, EventArgs e)
        {
            OutputLabel.Text = "Getting User Data";

            var paginatedDocuments = await Data.ListAsync<MessageModel>(DefaultPartitions.UserDocuments);

            if (paginatedDocuments.CurrentPage.Items.Any())
            {
                OutputLabel.Text = "";

                foreach (var model in paginatedDocuments.CurrentPage.Items)
                    OutputLabel.Text += $"Message: {model.DeserializedValue.Message}\nNumber: {model.DeserializedValue.Number ?? -1}\nIsCached: {model.IsFromDeviceCache}\n\n";
            }
            else
            {
                OutputLabel.Text = "No User Data Found";
            }
        }

        async void HandleGetSharedDataButtonClicked(object sender, EventArgs e)
        {
            OutputLabel.Text = "Getting Shared Data";

            var paginatedDocuments = await Data.ListAsync<MessageModel>(DefaultPartitions.AppDocuments);

            if (paginatedDocuments.CurrentPage.Items.Any())
            {
                OutputLabel.Text = "";

                foreach (var model in paginatedDocuments.CurrentPage.Items)
                    OutputLabel.Text += $"Message: {model.DeserializedValue.Message}\nNumber: {model.DeserializedValue.Number ?? -1}\nIsCached: {model.IsFromDeviceCache}\n\n";
            }
            else
            {
                OutputLabel.Text += "No Shared Data Found";
            }
        }

        async void HandleAddDataButtonClicked(object sender, EventArgs e)
        {
            OutputLabel.Text = "Sending User Data";

            var paginatedDocuments = await Data.ListAsync<MessageModel>(DefaultPartitions.UserDocuments);

            var messageModel = new MessageModel
            {
                Message = "User Data Message",
                Number = paginatedDocuments.CurrentPage.Items.Count + 1
            };

            var response = await Data.CreateAsync(Guid.NewGuid().ToString(), messageModel, DefaultPartitions.UserDocuments);

            OutputLabel.Text = $"Message: {response.DeserializedValue.Message}\nNumber: {response.DeserializedValue.Number ?? -1}\nIsCached: {response.IsFromDeviceCache}\n\n";
        }
    }
}
