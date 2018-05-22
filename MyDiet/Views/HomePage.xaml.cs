using System;
using MyDiet.Manager;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using MyDiet.Models;

namespace MyDiet.Views
{
    public partial class HomePage : ContentPage
    {
		DietManager dietManager;
        ReminderManager reminderManager;
        public HomePage()
        {
			
            InitializeComponent();
			//currentUser.Text = App.account.Email;
			date.Text = DateTime.Now.ToString("dd MMM yyyy dddd");
			dietManager = DietManager.DefaultManager;
			reminderManager = ReminderManager.DefaultManager;
        }


		protected override void OnAppearing()
		{
			base.OnAppearing();
			SetDiet();
			SetMedicine();
		}

		async public void SetDiet()
		{
			var _diets =await dietManager.GetHistoryAsync();
			var diets = _diets.Where(dietItem => dietItem.Date.Year == DateTime.Now.Year &&
												 dietItem.Date.Month == DateTime.Now.Month &&
									             dietItem.Date.Day == DateTime.Now.Day);
			var nums = diets.Count();
			int noOfMeals = 0;
			int totalCal = 0;
			if(nums>0){
				foreach(DietItem diet in diets )
				{
					totalCal += diet.Calories;
					noOfMeals++;
				}

				mealLabel.Text = noOfMeals + " meals";

			}
			else
			{
				mealLabel.Text = noOfMeals + " meal";
			}


			calLabel.Text = totalCal + " Total Cal";
		}

		async public void SetMedicine()
		{
			var medicine = await reminderManager.GetReminderAsync();
			var nums = medicine.Count();
			int taken = 0;
			int toTake = 0;
			if(nums>0){

				foreach(Reminder reminder in medicine)
				{
					if(reminder.Checked){
						taken++;
					}
					else
					{
						toTake++;
					}
				}
				takenLabel.Text = taken + " Taken";
				toTakeLabel.Text = toTake + " To Take";

			}
			else
			{
				takenLabel.Text = " No Reminder";
				toTakeLabel.Text = " ";
			}

		}
	}
}
