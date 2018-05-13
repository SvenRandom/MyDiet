using System;
using System.Collections.Generic;
using MyDiet.Models;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class MedicinePage : ContentPage
    {
		int currentView = 0;
		public MedicinePage()
		{
			InitializeComponent();


		}
		protected override void OnAppearing()
        {
            base.OnAppearing();
           
            if (currentView == 0)
            {
				ReminderClicked();

            }
            if (currentView == 1)
				MedicineClicked();

            if (currentView == 2)
                HistoryClicked();


      

        }

		async void OnAdded(object sender, System.EventArgs e)
        {
			var response = await DisplayActionSheet("Which to add?", "Cancel", null, "New Reminder", "New Medicine");
			if (response == "New Reminder"){
				await Navigation.PushAsync(new AddReminderPage());
			}
               
			if (response == "New Medicine")
				await Navigation.PushAsync(new AddMedicinePage());
            
        }

		void ReminderClicked(object sender, System.EventArgs e)
        {
			ReminderClicked();

        }

		void ReminderClicked()
        {
         
			medicineListView.IsVisible = false;

			reminderListView.IsVisible = true;
            reminder.BackgroundColor = Color.FromHex("#2196F3");
            reminder.TextColor = Color.White;
            medicine.BackgroundColor = Color.WhiteSmoke;
            medicine.TextColor = Color.FromHex("#2196F3");
            history.BackgroundColor = Color.WhiteSmoke;
            history.TextColor = Color.FromHex("#2196F3");
            currentView = 0;

			List<Reminder> reminders = new List<Reminder>();
            Reminder reminder1 = new Reminder
            {
				MedicineName = "enalapril(Vasotec)",
                Time = new TimeSpan(9, 00, 00),
				Quantity = "2 Tablet"
            };
            reminder1.SetTimeToDisplay();

            Reminder reminder2 = new Reminder
            {
				MedicineName = "Eplerenone (Inspra)",
                Time = new TimeSpan(12, 00, 00),
                Quantity = "1 Tablet"
            };
            reminder1.SetTimeToDisplay();
            Reminder reminder3 = new Reminder
            {
				MedicineName = "enalapril(Vasotec)",
                Time = new TimeSpan(19, 00, 00),
				Quantity = "2 Tablet"
            };
            reminder1.SetTimeToDisplay();
            reminder2.SetTimeToDisplay();
            reminder3.SetTimeToDisplay();

            reminders.Add(reminder1);
            reminders.Add(reminder2);
            reminders.Add(reminder3);
			reminderListView.ItemsSource = reminders;

        }

		void MedicineClicked(object sender, System.EventArgs e)
        {
			MedicineClicked();

        }

		void MedicineClicked()
        {
			reminderListView.IsVisible = false;

			medicineListView.IsVisible = true;
			reminder.BackgroundColor = Color.WhiteSmoke;
			reminder.TextColor = Color.FromHex("#2196F3");
			medicine.BackgroundColor = Color.FromHex("#2196F3");
			medicine.TextColor = Color.White;
            history.BackgroundColor = Color.WhiteSmoke;
            history.TextColor = Color.FromHex("#2196F3");
            currentView = 1;

			List<Medicine> medicines = new List<Medicine>();
			Medicine medicine1 = new Medicine
            {
				MedicineName = "enalapril(Vasotec)",
				Description="This is a medicine for lowering blood pressure",
				Unit = "Tablet"
            };

			Medicine medicine2 = new Medicine
            {
				MedicineName = "Eplerenone (Inspra)",
				Description = "Treat heart disease",
				Unit = "Tablet"
            };

			Medicine medicine3 = new Medicine
            {
				MedicineName = "Lomotil",
				Description = "Treat diarrhea",
				Unit = "Tablet"
            };
           

			medicines.Add(medicine1);
			medicines.Add(medicine2);
			medicines.Add(medicine3);
			medicineListView.ItemsSource = medicines;


        }



        void HistoryClicked(object sender, System.EventArgs e)
        {
            HistoryClicked();
        }
         void HistoryClicked()
        {
          

			reminder.BackgroundColor = Color.WhiteSmoke;
			reminder.TextColor = Color.FromHex("#2196F3");
			medicine.BackgroundColor = Color.WhiteSmoke;
			medicine.TextColor = Color.FromHex("#2196F3");
            history.BackgroundColor = Color.FromHex("#2196F3");
            history.TextColor = Color.White;
            currentView = 2;
        }
    }
}
