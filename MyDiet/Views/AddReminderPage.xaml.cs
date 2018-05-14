using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddReminderPage : ContentPage
    {
		Reminder currentReminder;
		bool isNewItem=false;
		public AddReminderPage(Reminder reminder)
        {
            InitializeComponent();
			if(reminder==null){
				isNewItem = true;
				currentReminder = new Reminder
				{
					Id = Guid.NewGuid().ToString(),
					UserId = App.account.Id
				};

			}else
			{
				currentReminder = reminder;

			}

			tableView.BindingContext = currentReminder;
        }

		async void DoneClicked(object sender, EventArgs e)
        {
			currentReminder.SetTimeToDisplay();
			ReminderManager reminderManeger = ReminderManager.DefaultManager;
			await reminderManeger.SaveTaskAsync(currentReminder, isNewItem);
			await Navigation.PopAsync();
        }
    }
}
