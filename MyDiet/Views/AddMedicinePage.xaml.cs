using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

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
				choice.IsVisible = true;
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
			BindingContext = currentMedicine;
            
        }

		async void DoneClicked(object sender, EventArgs e)
		{
			
			//System.Diagnostics.Debug.WriteLine("currentMedicine for done: " + currentMedicine.Description);
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
					if(t.MedicineId==currentMedicine.Id)
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
			if(currentMedicine.IsTaking){
				await DisplayAlert("Notice:", "Please end this cycle of treament first!", "Ok");
				return;
			}
			var result = await DisplayAlert("Notices", "Your new reminders starts from "+currentMedicine.StartTime.ToString("dd/MMM/yyyy dddd")
                               +" with "+currentMedicine.TimesPerDay+" times per day. Are you sure to activate?", "Sure", "Cancel");
			if(result){
				
    			currentMedicine.IsTaking = true;
                if (currentMedicine.TimesPerDay >= 1)
                {
                    Reminder reminder = new Reminder
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = App.email,
						MedicineId = currentMedicine.Id,
                        MedicineName = currentMedicine.MedicineName,
                        Unit = currentMedicine.Unit,
                        Checked = false
                    };
                    ReminderManager reminderManager = ReminderManager.DefaultManager;
                    reminder.Time = timePicker1.Time;
                    reminder.SetTimeToDisplay();
                    reminder.SetUnChecked();
                    await reminderManager.SaveTaskAsync(reminder, true);
					SetNotification(reminder);
                    if (currentMedicine.TimesPerDay >= 2)
                    {
                        reminder = new Reminder
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = App.email,
							MedicineId = currentMedicine.Id,
							MedicineName = currentMedicine.MedicineName,
                            Unit = currentMedicine.Unit,
                            Checked = false
                        };
                        reminder.Time = timePicker2.Time;
                        reminder.SetTimeToDisplay();
                        reminder.SetUnChecked();
                        await reminderManager.SaveTaskAsync(reminder, true);
						SetNotification(reminder);
                        if (currentMedicine.TimesPerDay >= 3)
                        {
                            reminder = new Reminder
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = App.email,
								MedicineId = currentMedicine.Id,
                                MedicineName = currentMedicine.MedicineName,
                                Unit = currentMedicine.Unit,
                                Checked = false
                            };
                            reminder.Time = timePicker3.Time;
                            reminder.SetTimeToDisplay();
                            reminder.SetUnChecked();
                            await reminderManager.SaveTaskAsync(reminder, true);
							SetNotification(reminder);
                            if (currentMedicine.TimesPerDay >= 4)
                            {
                                reminder = new Reminder
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    UserId = App.email,
									MedicineId = currentMedicine.Id,
                                    MedicineName = currentMedicine.MedicineName,
                                    Unit = currentMedicine.Unit,
                                    Checked = false
                                };
                                reminder.Time = timePicker4.Time;
                                reminder.SetTimeToDisplay();
                                reminder.SetUnChecked();
                                await reminderManager.SaveTaskAsync(reminder, true);
								SetNotification(reminder);
                                if (currentMedicine.TimesPerDay >= 5)
                                {
                                    reminder = new Reminder
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        UserId = App.email,
										MedicineId = currentMedicine.Id,
                                        MedicineName = currentMedicine.MedicineName,
                                        Unit = currentMedicine.Unit,
                                        Checked = false
                                    };
                                    reminder.Time = timePicker5.Time;
                                    reminder.SetTimeToDisplay();
                                    reminder.SetUnChecked();
                                    await reminderManager.SaveTaskAsync(reminder, true);
									SetNotification(reminder);
                                    if (currentMedicine.TimesPerDay >= 6)
                                    {
                                        reminder = new Reminder
                                        {
                                            Id = Guid.NewGuid().ToString(),
                                            UserId = App.email,
											MedicineId = currentMedicine.Id,
                                            MedicineName = currentMedicine.MedicineName,
                                            Unit = currentMedicine.Unit,
                                            Checked = false
                                        };
                                        reminder.Time = timePicker6.Time;
                                        reminder.SetTimeToDisplay();
                                        reminder.SetUnChecked();
                                        await reminderManager.SaveTaskAsync(reminder, true);
										SetNotification(reminder);
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
                        
    			MedicineManager medicineManager = MedicineManager.DefaultManager;
                await medicineManager.SaveTaskAsync(currentMedicine, isNewItem);
				App.contentChanged = true;
			}
            
		}

		async void EndClicked(object sender, EventArgs e)
        {
			if(!currentMedicine.IsTaking){
				await DisplayAlert("Notice:", "You are not taking this medicine!", "OK");
				return;
			}
			ReminderManager reminderManager = ReminderManager.DefaultManager;
			var temp = await reminderManager.GetReminderAsync();
			foreach(var t in temp)
			{
				if(t.MedicineId==currentMedicine.Id){
					await reminderManager.DeleteTaskAsync(t);
                    DeleteNotification(t);
				}

			}
			currentMedicine.IsTaking = false;
			MedicineManager medicineManager = MedicineManager.DefaultManager;
            await medicineManager.SaveTaskAsync(currentMedicine, isNewItem);
			status.Text = "You are not taking this medical now";
			App.contentChanged = true;

			MedicineHistory medicineHistory = new MedicineHistory
			{
				Id = Guid.NewGuid().ToString(),
				UserId = App.email,
				MedicineName = currentMedicine.MedicineName,
				Directions = currentMedicine.TimesPerDay + " times a day, " + currentMedicine.Unit + " each time",
				TimeToDisplay = "From " + currentMedicine.StartTime.ToString("dd/MMM/yyyy dddd")
				                                         + " to " + DateTime.Now.ToString("dd/MMM/yyyy dddd")
			};
			MedicineHistoryManager medicineHistoryManager = MedicineHistoryManager.DefaultManager;
			await medicineHistoryManager.SaveTaskAsync(medicineHistory, true);
			await DisplayAlert("Notice:", "You have ended this medicine!", "OK");

        }



        //********************** scan barcode **********************************

        async void scanClicked(object sender, EventArgs e)
        {

            //MobileBarcodeScanner.Initialize(Application);
            await DisplayAlert("notice", "start scan carcode", "yes");
			//App.barcode = "9316626605835";
#if __ANDROID__
    // Initialize the scanner first so it can track the current context
            MobileBarcodeScanner.Initialize (Application);
#endif
            // Initialize the scanner first so it can track the current context
            //if (Device.RuntimePlatform == Device.Android)
            //        {
            //ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            //}
            var scanPage = new ZXingScannerPage();
            //string results="";
            scanPage.Title = "Please Scan barcode";
            scanPage.OnScanResult += (result) => {
                // stop scanning
                scanPage.IsScanning = false;

                // show scan result
                Device.BeginInvokeOnMainThread(() => {
                    App.barcode = result.Text;
                    //DisplayAlert("Scanned Barcode", result.Text, "OK");
                    //DisplayAlert("Scanned Barcode from APP", App.barcode, "OK");
                    Navigation.PopAsync();

                });
            };

            // go to scan page
            await Navigation.PushAsync(scanPage);

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (App.barcode == null)
                    return true;
                else
                {
                    var result = App.barcode;

                    SearchBarcodeAsync();
                    return false;
                }
            });
        }

		async public void SearchBarcodeAsync()
		{
			await DisplayAlert("Scanned Barcode", App.barcode, "OK");
			try{
				MedicineDatabaseManager medicineDatabaseManager = new MedicineDatabaseManager();
				var medical =await medicineDatabaseManager.GetMedicineAsync(App.barcode);
				if(medical!=null)
				{
					currentMedicine.MedicineName = medical.MedicineName;
					currentMedicine.Description = medical.Description;
					currentMedicine.Directions = medical.Directions;
					currentMedicine.Duration = medical.Duration;
					currentMedicine.TimesPerDay = medical.TimesPerDay;
					currentMedicine.Unit = medical.Unit;

					nameEntry.Text=medical.MedicineName;
					descriptionEntry.Text = medical.Description;
					directionsEntry.Text = medical.Directions;
					durationEntry.Text = medical.Duration;
					timesPerDayEntry.Text = medical.TimesPerDay.ToString();
					unitEntry.Text = medical.Unit;

					SetReminders();
				}else
				{
					await DisplayAlert("Sorry!", "This medicine does not exist in our database", "OK");
				}
				
			}catch
			{
				await DisplayAlert("notice", "NetWork Error", "Sure");
			}

			App.barcode = null;
		}



		async void DeleteClicked(object sender, System.EventArgs e)
        {
			MedicineManager medicineManager = MedicineManager.DefaultManager;
			await medicineManager.DeleteTaskAsync(currentMedicine);
			App.contentChanged = true;
			await Navigation.PopAsync();
        }

        
		public void SetNotification(Reminder reminder)
		{
			DateTime notiTime = new DateTime(
                DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
				reminder.Time.Hours, reminder.Time.Minutes, 0
                    );

            if (DateTime.Compare(notiTime, DateTime.Now) < 0)
            {
                notiTime = notiTime.AddDays(1);
            }
            CrossLocalNotifications.Current.Show("Medicine Notification",
			                                     "It's time to take " + reminder.MedicineName, reminder.GetHashCode(),
                                                 notiTime);
		}

		public void DeleteNotification(Reminder reminder)
		{
			CrossLocalNotifications.Current.Cancel(reminder.GetHashCode());
		}

    }
}
