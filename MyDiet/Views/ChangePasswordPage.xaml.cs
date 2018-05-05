using System;
using System.Collections.Generic;
using MyDiet.Manager;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }



        async void OnDoneClicked(object sender, System.EventArgs e)
        {
			if(oldEntry.Text==App.account.Password){
				if(newEntry.Text==comfirmNewEntry.Text){
					var response = await DisplayAlert("Notice", "Are you sure to change?", "yes", "no");
					if (response)
					{

						activityIndicator.IsRunning = true;
						activityIndicator.IsVisible = true;

						App.account.Password = newEntry.Text;

						var accountManager = new AccountManager();
						await accountManager.SaveTaskAsync(App.account, false);
						await Navigation.PopAsync();
					}
				}else
				{
					await DisplayAlert("Warning", "Please ensure new password and comfirm password are the same", "Comfirm");
				}

			}else
			{
				await DisplayAlert("Warning", "Your old passaord is not correct!", "Comfirm");

			}
            
        }



    }
}
