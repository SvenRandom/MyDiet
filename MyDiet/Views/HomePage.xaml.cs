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
		ActivityDataManager activityDataManager;
        public HomePage()
        {
			
            InitializeComponent();
			//currentUser.Text = App.account.Email;
			date.Text = DateTime.Now.ToString("dd MMM yyyy dddd");
			dietManager = DietManager.DefaultManager;
			reminderManager = ReminderManager.DefaultManager;
			activityDataManager = new ActivityDataManager();
			//dietFrame.Tapped += async (sender, e) => {
    //            var tabbedPage = this.Parent as TabbedPage;
				//tabbedPage.SwitchToDiet();
            //};

        }


		protected override void OnAppearing()
		{
			base.OnAppearing();
			SetDiet();
			SetMedicine();
			SetActivity();
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

				mealLabel.Text = noOfMeals + " Meals";

			}
			else
			{
				mealLabel.Text = noOfMeals + " Meal";
			}


			calLabel.Text = totalCal + " Cal";
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
        
		async public void SetActivity()
        {
			var _data = await activityDataManager.GetActivityDataAsync();
			var current = _data.Where(data => data.date.Year == DateTime.Now.Year &&
                                        data.date.Month == DateTime.Now.Month &&
                                        data.date.Day == DateTime.Now.Day);
			var nums = current.Count();
            
            if (nums > 0)
            {
				var t = current.ElementAt(0);
                kmLabel.Text = t.walkedkm.ToString("0.00") + " km";
                //System.Diagnostics.Debug.WriteLine("km: " + t.walkedkm);
				stepLabel.Text = t.steps.ToString() + " Steps";
               

            }
           
   
        }

		void OnDietTapped(object sender, System.EventArgs e)
        {
			//tabbedPage.CurrentPage = tabbedPage.Children[1];
			var tabbedPage = this.Parent as TabbedPage;
			tabbedPage.CurrentPage=tabbedPage.Children[1];

        }
		void OnActivityTapped(object sender, System.EventArgs e)
        {
            //tabbedPage.CurrentPage = tabbedPage.Children[1];
            var tabbedPage = this.Parent as TabbedPage;
            tabbedPage.CurrentPage = tabbedPage.Children[3];

        }

		void OnMedicineTapped(object sender, System.EventArgs e)
        {
            //tabbedPage.CurrentPage = tabbedPage.Children[1];
            var tabbedPage = this.Parent as TabbedPage;
            tabbedPage.CurrentPage = tabbedPage.Children[2];

        }

	}
}
