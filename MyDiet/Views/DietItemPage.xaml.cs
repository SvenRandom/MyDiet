using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using MyDiet.Helpers;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class DietItemPage : ContentPage
    {      

		bool isNewItem = false;
        public static DietManager dietManager { get; private set; }
        DietItem dietItemCurrent;

        public DietItemPage(DietItem dietItem)
        {
            InitializeComponent();
            if (dietItem == null)
            {
                isNewItem = true;
                dietItemCurrent = new DietItem
                { 
                    Id = Guid.NewGuid().ToString()

                };
                dietItemCurrent.Date = DateTime.Now;
                dietItemCurrent.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
				dietItemCurrent.UserId = Settings.AccountEmail;


            }

            else
            {
                dietItemCurrent = dietItem;

            }

            BindingContext = dietItemCurrent;
			dietManager = DietManager.DefaultManager;

        }


        async void OnSaveActivated(object sender, EventArgs e)
        {
			dietItemCurrent.SetTime();
			await dietManager.SaveTaskAsync(dietItemCurrent,isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
			var response =await DisplayAlert("Warning", "Are you sure to delete it?", "yes", "no");
			if(response){
				await dietManager.DeleteTaskAsync(dietItemCurrent);
                await Navigation.PopAsync();
			}

        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
			var response =await DisplayAlert("Warning", "Are you sure to cancel?", "yes", "no");
            if(response){
				await Navigation.PopAsync();
            }
           
        }

       


    }
}
