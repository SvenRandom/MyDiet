using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
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
				dietItemCurrent.UserId = App.account.Id;


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
			//var dietItem = (DietItem)BindingContext;
			//dietItem.SetTime();
			//dietItem.UserId = App.user.UserId.ToString();
			dietItemCurrent.SetTime();
			await dietManager.SaveTaskAsync(dietItemCurrent,isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var dietItem = (DietItem)BindingContext;
			await dietManager.DeleteTaskAsync(dietItem);
            await Navigation.PopAsync();
        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}
