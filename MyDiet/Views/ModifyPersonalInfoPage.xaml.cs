using System;
using System.Collections.Generic;
using MyDiet.Manager;
using Xamarin.Forms;
using MyDiet.Helpers;

namespace MyDiet.Views
{
    public partial class ModifyPersonalInfoPage : ContentPage
    {
    
        public ModifyPersonalInfoPage()
        {
            InitializeComponent();
			GetAccount();


        }

		public async void GetAccount()
		{
			try
            {
                var accountManager = new AccountManager();
                var account = await accountManager.GetAccountInfosAsync(Settings.AccountEmail);
                App.account = account;
				nameCell.Text = App.account.Username;
                nameCell.Detail = App.account.Email;
                birthCell.Detail = App.account.DateOfBirth.ToString("dd MMM yyyy");
                sexCell.Detail = App.account.Gender;
                weightEntry.Text = App.account.Weight.ToString();
                heightEntry.Text = App.account.Height.ToString();
                cuisineEntry.Text = App.account.TypeOfCuisine;

            }
            catch
            {
                await DisplayAlert("Warning", "No Network available", "continue", "cancel");
               
				await Navigation.PopAsync();
               
            }
		}
      

		async void OnDoneClicked(object sender, System.EventArgs e)
        {
			if (AreDetailsValid())
			{
				
				var response = await DisplayAlert("Warning", "Are you sure to modify?", "yes", "no");
				if (response)
				{
					activityIndicator.IsRunning = true;
					activityIndicator.IsVisible = true;
					App.account.Weight = Convert.ToInt32(weightEntry.Text);

					App.account.Height = Convert.ToInt32(heightEntry.Text);
					App.account.TypeOfCuisine = cuisineEntry.Text;

					var accountManager = new AccountManager();
					await accountManager.SaveTaskAsync(App.account, false);
					await Navigation.PopAsync();
				}
			}else{
				await DisplayAlert("Warning", "please fill all with valid input","Sure");
			}
				
        }


		bool AreDetailsValid()
        {

            return (
                    int.TryParse(heightEntry.Text, out int i) &&
                    int.TryParse(weightEntry.Text, out i) &&
				    !string.IsNullOrWhiteSpace(cuisineEntry.Text));
        }
    }
}
