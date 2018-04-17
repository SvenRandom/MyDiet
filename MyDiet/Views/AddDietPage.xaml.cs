using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HelloWorld;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddDietPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        //private ObservableCollection<DietRecord> _dietRecords;

        public AddDietPage()
        {
            InitializeComponent();
            timePicker.Time= new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);

        }

        async public void OnDoneClicked(object sender, EventArgs e)
        {
            

            var addSucceed = IsAddValid();
            if (addSucceed)
            {

                _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

                await _connection.CreateTableAsync<DietRecord>();

                //var dietRecords = await _connection.Table<DietRecord>().ToListAsync();
                //_dietRecords = new ObservableCollection<DietRecord>(dietRecords);

                //dietRecordListView.ItemsSource = _dietRecords;


                var newDiet = new DietRecord
                {
                    Description = descriptionEditor.Text,
                    Calories = Convert.ToInt32(calEntry.Text),
                    Image = imageEntry.Text,
                    Date = datePicker.Date,
                    Time = timePicker.Time,
                    UserId=App.user.UserId
                };

                newDiet.SetTime();

                await _connection.InsertAsync(newDiet);

                //_dietRecords.Add(newDiet);


                await Navigation.PopToRootAsync();

            }
            else
                message.Text="please enter description and enter numbers in calories";


        }


        bool IsAddValid()
        {
            
            return (!string.IsNullOrEmpty(descriptionEditor.Text) &&
                    (int.TryParse(calEntry.Text, out int i) || string.IsNullOrEmpty(calEntry.Text) ));
        }
    }
}
