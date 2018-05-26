using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddReminderPage : ContentPage
    {


		Reminder currentReminder;
		bool isNewItem=false;
		MedicineManager medicineManager = MedicineManager.DefaultManager;
		List<string> vs = new List<string>();
		List<string> vsUnit = new List<string>();
		public AddReminderPage()
        {
            InitializeComponent();
            
        }
		public AddReminderPage(Reminder reminder)
        {
            InitializeComponent();
			if(reminder==null){
				isNewItem = true;
				currentReminder = new Reminder
				{
					Id = Guid.NewGuid().ToString(),
					UserId = App.email,
					Checked=false
				};

			}else
			{
				currentReminder = reminder;

			}


        }

		async protected override void OnAppearing()
		{
			
			//vs.Add("enalapril(Vasotec)");
			//vs.Add("Eplerenone (Inspra)");
			//vs.Add("Lomotil");
			var temp =await medicineManager.GetMedicinesAsync();

			foreach (var item in temp)
			{
				vs.Add(item.MedicineName);
				vsUnit.Add(item.Unit);
			}

			medicinePicker.ItemsSource = vs;

			BindingContext = currentReminder;
			base.OnAppearing();
		}

		async void DoneClicked(object sender, EventArgs e)
        {
			currentReminder.SetTimeToDisplay();
			currentReminder.SetUnChecked();
			ReminderManager reminderManeger = ReminderManager.DefaultManager;
			await reminderManeger.SaveTaskAsync(currentReminder, isNewItem);
			App.contentChanged = true;
			DateTime notiTime = new DateTime(
				DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 
				currentReminder.Time.Hours, currentReminder.Time.Minutes, 0
                    );
            
			if(DateTime.Compare(notiTime, DateTime.Now)<0){
				notiTime = notiTime.AddDays(1);
			}
			CrossLocalNotifications.Current.Show("Medicine Notification",
			                                     "It's time to take "+currentReminder.MedicineName, currentReminder.GetHashCode(),
			                                     notiTime);
			
			await Navigation.PopAsync();
        }

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
			var a = medicinePicker.SelectedItem.ToString();
			var index = vs.IndexOf(a);
			var b =vsUnit.ToArray()[index];
			currentReminder.Unit = b;
			unitLabel.Text = b;
        }
        

		async void DeleteClicked(object sender, System.EventArgs e)
        {
			var confirm = await DisplayAlert("Notice!", "Are you sure to delete this reminder?", "Yes", "Cancel");
			if(confirm){
				ReminderManager reminderManager = ReminderManager.DefaultManager;
                await reminderManager.DeleteTaskAsync(currentReminder);
                await Navigation.PopAsync();
			}

        }
    }
}
