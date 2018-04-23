using System;
using System.Collections.Generic;
using HelloWorld;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class DietEditPage : ContentPage
    {
        private DietRecord diet;

        public DietEditPage(DietRecord dietRecord)
        {
            InitializeComponent();
            diet = dietRecord;
            BindingContext = diet;

            //timePicker.Time = diet.Time;
            //datePicker.Date = diet.Date;
            //descriptionEditor.Text = diet.Description;
            //calEntry.Text = Convert.ToString(diet.Calories);
            //imageEntry.Text = diet.Image;
        }

        async void OnComfirmClicked(object sender, System.EventArgs e)
        {
            SQLiteAsyncConnection _connection=DependencyService.Get<ISQLiteDb>().GetConnection();
            if(IsAddValid()){
                diet.SetTime();
                await _connection.UpdateAsync(diet);

                await Navigation.PopToRootAsync();
			}else{
				message.BackgroundColor = Color.Red;
				message.Text = "please enter description and enter numbers in calories";
			}
                
           
        }
        
        bool IsAddValid()
        {

            return (!string.IsNullOrEmpty(diet.Description) &&
                    (int.TryParse(calEntry.Text, out int i) || string.IsNullOrEmpty(calEntry.Text)));
        }
    }
}
