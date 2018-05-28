using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddMedicinePage : ContentPage
    {


		Medicine currentMedicine;

		TimePicker timePicker1 = null;
		TimePicker timePicker2 = null;
		TimePicker timePicker3 = null;
		TimePicker timePicker4 = null;
		TimePicker timePicker5 = null;
		TimePicker timePicker6 = null;


		bool isNewItem=false;
		public AddMedicinePage()
		{
			InitializeComponent();
		}

		public AddMedicinePage(Medicine medicine) 
        {
            InitializeComponent();
			if(medicine==null){
				isNewItem = true;
				currentMedicine = new Medicine
				{
					Id = Guid.NewGuid().ToString(),
					UserId = App.email,
					StartTime = DateTime.Now,
					IsTaking=false
                };
				status.Text = "This is a new medicine for you";

			}else
			{
				currentMedicine = medicine;
				if(currentMedicine.IsTaking){
					var startday = new DateTime(currentMedicine.StartTime.Year, currentMedicine.StartTime.Month, currentMedicine.StartTime.Day);
					var diff = DateTime.Now - startday;

					var difff = diff.Days + 1;
					status.Text = "You are recovering! Today is the "+difff+" of "+currentMedicine.Duration+ " days treament";
						
				}else
				{
					status.Text = "You are not taking this medical now";
				}

				if (currentMedicine.IsTaking)
					GetReminders();
				else
					SetReminders();

			}
			tableView.BindingContext = currentMedicine;
            
        }

		async void DoneClicked(object sender, EventArgs e)
		{
			

			MedicineManager medicineManager = MedicineManager.DefaultManager;
			await medicineManager.SaveTaskAsync(currentMedicine, isNewItem);
			App.contentChanged = true;
			await Navigation.PopAsync();
		}

              
		void FrequancyCompleted(object sender, System.EventArgs e)
        {
			if (currentMedicine.TimesPerDay > 6 || currentMedicine.TimesPerDay < 0){
				DisplayAlert("Notice!", "Please input correct number of reminders", "OK");
				currentMedicine.TimesPerDay = 0;
			}
				
			else
	    		SetReminders();

        }


		async public void GetReminders()
		{
			if(currentMedicine.TimesPerDay>0){
				reminders.Children.Clear();
				ReminderManager reminderManager = ReminderManager.DefaultManager;
                var temp = await reminderManager.GetReminderAsync();
                List<TimeSpan> timeSpans = new List<TimeSpan>();

                foreach (var t in temp)
                {
                    timeSpans.Add(t.Time);
                }
				var array = timeSpans.ToArray();
                
				switch (array.Length)
                {
					case 1:
                        timePicker1 = new TimePicker();
						timePicker1.Time = array[0];
                        reminders.Children.Add(timePicker1);
                        break;
                    case 2:
                        timePicker1 = new TimePicker();
						timePicker1.Time = array[0];
                        timePicker2 = new TimePicker();
						timePicker2.Time = array[1];
                        reminders.Children.Add(timePicker1);
                        reminders.Children.Add(timePicker2);
                        break;
                    case 3:
                        timePicker1 = new TimePicker();
						timePicker1.Time = array[0];
                        timePicker2 = new TimePicker();
						timePicker2.Time = array[1];
                        timePicker3 = new TimePicker();
						timePicker3.Time = array[2];
                        reminders.Children.Add(timePicker1);
                        reminders.Children.Add(timePicker2);
                        reminders.Children.Add(timePicker3);
                        break;
                    case 4:
                        timePicker1 = new TimePicker();
						timePicker1.Time = array[0];
                        timePicker2 = new TimePicker();
						timePicker2.Time = array[1];
                        timePicker3 = new TimePicker();
						timePicker3.Time = array[2];
                        timePicker4 = new TimePicker();
						timePicker4.Time = array[3];
                        reminders.Children.Add(timePicker1);
                        reminders.Children.Add(timePicker2);
                        reminders.Children.Add(timePicker3);
                        reminders.Children.Add(timePicker4);
                        break;
					case 5:
                        timePicker1 = new TimePicker();
						timePicker1.Time = array[0];
                        timePicker2 = new TimePicker();
						timePicker2.Time = array[1];
                        timePicker3 = new TimePicker();
						timePicker3.Time = array[2];
                        timePicker4 = new TimePicker();
						timePicker4.Time = array[3];
                        timePicker5 = new TimePicker();
						timePicker5.Time = array[4];
                        reminders.Children.Add(timePicker1);
                        reminders.Children.Add(timePicker2);
                        reminders.Children.Add(timePicker3);
                        reminders.Children.Add(timePicker4);
                        reminders.Children.Add(timePicker5);
                        break;
                    case 6:
                        timePicker1 = new TimePicker();
						timePicker1.Time = array[0];
                        timePicker2 = new TimePicker();
						timePicker2.Time = array[1];
                        timePicker3 = new TimePicker();
						timePicker3.Time = array[2];
                        timePicker4 = new TimePicker();
						timePicker4.Time = array[3];
                        timePicker5 = new TimePicker();
						timePicker5.Time = array[4];
                        timePicker6 = new TimePicker();
						timePicker6.Time = array[5];
                        reminders.Children.Add(timePicker1);
                        reminders.Children.Add(timePicker2);
                        reminders.Children.Add(timePicker3);
                        reminders.Children.Add(timePicker4);
                        reminders.Children.Add(timePicker5);
                        reminders.Children.Add(timePicker6);
                        break;
 
                    default:
                        break;
                }
			}


		}
		public void SetReminders()
		{
			//StackLayout stackLayout = new StackLayout();
			//stackLayout.Orientation = StackOrientation.Horizontal;
			reminders.Children.Clear();
			switch(currentMedicine.TimesPerDay)
			{
                case 1:
                    timePicker1 = new TimePicker();
					timePicker1.Time = new TimeSpan(12,0,0);
					reminders.Children.Add(timePicker1);
                    break;
                case 2:
					timePicker1 = new TimePicker();
                    timePicker1.Time = new TimeSpan(9, 0, 0);
					timePicker2 = new TimePicker();
                    timePicker2.Time = new TimeSpan(19, 0, 0);
					reminders.Children.Add(timePicker1);
					reminders.Children.Add(timePicker2);
                    break;
                case 3:
					timePicker1 = new TimePicker();
                    timePicker1.Time = new TimeSpan(9, 0, 0);
                    timePicker2 = new TimePicker();
                    timePicker2.Time = new TimeSpan(12, 0, 0);
					timePicker3 = new TimePicker();
					timePicker3.Time = new TimeSpan(20, 0, 0);
                    reminders.Children.Add(timePicker1);
					reminders.Children.Add(timePicker2);
					reminders.Children.Add(timePicker3);
                    break;
                case 4:
					timePicker1 = new TimePicker();
                    timePicker1.Time = new TimeSpan(8, 0, 0);
                    timePicker2 = new TimePicker();
                    timePicker2.Time = new TimeSpan(12, 0, 0);
                    timePicker3 = new TimePicker();
                    timePicker3.Time = new TimeSpan(16, 0, 0);
					timePicker4 = new TimePicker();
					timePicker4.Time = new TimeSpan(20, 0, 0);
                    reminders.Children.Add(timePicker1);
                    reminders.Children.Add(timePicker2);
                    reminders.Children.Add(timePicker3);
					reminders.Children.Add(timePicker4);
                    break;
                case 5:
					timePicker1 = new TimePicker();
                    timePicker1.Time = new TimeSpan(6, 0, 0);
                    timePicker2 = new TimePicker();
                    timePicker2.Time = new TimeSpan(10, 0, 0);
                    timePicker3 = new TimePicker();
                    timePicker3.Time = new TimeSpan(14, 0, 0);
                    timePicker4 = new TimePicker();
                    timePicker4.Time = new TimeSpan(18, 0, 0);
					timePicker5 = new TimePicker();
					timePicker5.Time = new TimeSpan(22, 0, 0);
                    reminders.Children.Add(timePicker1);
                    reminders.Children.Add(timePicker2);
                    reminders.Children.Add(timePicker3);
					reminders.Children.Add(timePicker4);
					reminders.Children.Add(timePicker5);
                    break;
                case 6:
					timePicker1 = new TimePicker();
                    timePicker1.Time = new TimeSpan(6, 0, 0);
                    timePicker2 = new TimePicker();
                    timePicker2.Time = new TimeSpan(9, 0, 0);
                    timePicker3 = new TimePicker();
                    timePicker3.Time = new TimeSpan(12, 0, 0);
                    timePicker4 = new TimePicker();
                    timePicker4.Time = new TimeSpan(15, 0, 0);
                    timePicker5 = new TimePicker();
                    timePicker5.Time = new TimeSpan(18, 0, 0);
					timePicker6 = new TimePicker();
					timePicker6.Time = new TimeSpan(21, 0, 0);
                    reminders.Children.Add(timePicker1);
                    reminders.Children.Add(timePicker2);
                    reminders.Children.Add(timePicker3);
                    reminders.Children.Add(timePicker4);
                    reminders.Children.Add(timePicker5);
					reminders.Children.Add(timePicker6);
                    break;
              
                default:
                    break;
			}
		}


		async void StartClicked(object sender, EventArgs e)
		{
			currentMedicine.IsTaking = true;
            if (currentMedicine.TimesPerDay >= 1)
            {
                Reminder reminder = new Reminder
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = App.email,
                    MedicineId = currentMedicine.MedicineName,
                    Unit = currentMedicine.Unit,
                    Checked = false
                };
                ReminderManager reminderManager = ReminderManager.DefaultManager;
                reminder.Time = timePicker1.Time;
                reminder.SetTimeToDisplay();
                reminder.SetUnChecked();
                await reminderManager.SaveTaskAsync(reminder, true);
                if (currentMedicine.TimesPerDay >= 2)
                {
                    reminder = new Reminder
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = App.email,
                        MedicineId = currentMedicine.MedicineName,
                        Unit = currentMedicine.Unit,
                        Checked = false
                    };
                    reminder.Time = timePicker2.Time;
                    reminder.SetTimeToDisplay();
                    reminder.SetUnChecked();
                    await reminderManager.SaveTaskAsync(reminder, true);
                    if (currentMedicine.TimesPerDay >= 3)
                    {
                        reminder = new Reminder
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = App.email,
                            MedicineId = currentMedicine.MedicineName,
                            Unit = currentMedicine.Unit,
                            Checked = false
                        };
                        reminder.Time = timePicker3.Time;
                        reminder.SetTimeToDisplay();
                        reminder.SetUnChecked();
                        await reminderManager.SaveTaskAsync(reminder, true);
                        if (currentMedicine.TimesPerDay >= 4)
                        {
                            reminder = new Reminder
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = App.email,
                                MedicineId = currentMedicine.MedicineName,
                                Unit = currentMedicine.Unit,
                                Checked = false
                            };
                            reminder.Time = timePicker4.Time;
                            reminder.SetTimeToDisplay();
                            reminder.SetUnChecked();
                            await reminderManager.SaveTaskAsync(reminder, true);
                            if (currentMedicine.TimesPerDay >= 5)
                            {
                                reminder = new Reminder
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    UserId = App.email,
                                    MedicineId = currentMedicine.MedicineName,
                                    Unit = currentMedicine.Unit,
                                    Checked = false
                                };
                                reminder.Time = timePicker5.Time;
                                reminder.SetTimeToDisplay();
                                reminder.SetUnChecked();
                                await reminderManager.SaveTaskAsync(reminder, true);
                                if (currentMedicine.TimesPerDay >= 6)
                                {
                                    reminder = new Reminder
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        UserId = App.email,
                                        MedicineId = currentMedicine.MedicineName,
                                        Unit = currentMedicine.Unit,
                                        Checked = false
                                    };
                                    reminder.Time = timePicker6.Time;
                                    reminder.SetTimeToDisplay();
                                    reminder.SetUnChecked();
                                    await reminderManager.SaveTaskAsync(reminder, true);
                                }
                            }
                        }
                    }
                }
            }

			var startday = new DateTime(currentMedicine.StartTime.Year, currentMedicine.StartTime.Month, currentMedicine.StartTime.Day);
                    var diff = DateTime.Now - startday;

                    var difff = diff.Days + 1;
                    status.Text = "You are recovering! Today is the "+difff+" of "+currentMedicine.Duration+ " days treament";
                    

		}

		async void EndClicked(object sender, EventArgs e)
        {
			ReminderManager reminderManager = ReminderManager.DefaultManager;
			var temp = await reminderManager.GetReminderAsync();
			foreach(var t in temp)
			{
				await reminderManager.DeleteTaskAsync(t);
			}
			currentMedicine.IsTaking = false;
			status.Text = "You are not taking this medical now";
        }

    }
}
