using System;
using System.Collections.Generic;

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

		void OnAdded(object sender, System.EventArgs e)
        {
            
        }

		void ReminderClicked(object sender, System.EventArgs e)
        {
			ReminderClicked();
        }

		void ReminderClicked()
        {
            
            reminder.BackgroundColor = Color.FromHex("#2196F3");
            reminder.TextColor = Color.White;
            medicine.BackgroundColor = Color.WhiteSmoke;
            medicine.TextColor = Color.FromHex("#2196F3");
            history.BackgroundColor = Color.WhiteSmoke;
            history.TextColor = Color.FromHex("#2196F3");
            currentView = 0;
        }

		void MedicineClicked(object sender, System.EventArgs e)
        {
			MedicineClicked();
        }

		void MedicineClicked()
        {
          
              
			reminder.BackgroundColor = Color.WhiteSmoke;
			reminder.TextColor = Color.FromHex("#2196F3");
			medicine.BackgroundColor = Color.FromHex("#2196F3");
			medicine.TextColor = Color.White;
            history.BackgroundColor = Color.WhiteSmoke;
            history.TextColor = Color.FromHex("#2196F3");
            currentView = 1;
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
