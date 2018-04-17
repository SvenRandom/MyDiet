using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HelloWorld;
using MyDiet.Models;
using SQLite;
using Xamarin.Forms;

namespace MyDiet.Views
{
 
    public partial class DietPage : ContentPage
    {

        private SQLiteAsyncConnection _connection;
        private ObservableCollection<DietRecord> _dietRecords;

        public DietPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }


        protected override async void OnAppearing()
		{


            await _connection.CreateTableAsync<DietRecord>();

            var dietRecords0 = _connection.Table<DietRecord>().Where(v => v.UserId==App.user.UserId);
            var dietRecords =await dietRecords0.ToListAsync();

            //var dietRecords =await _connection.Table<DietRecord>().ToListAsync();
            _dietRecords = new ObservableCollection<DietRecord>(dietRecords);
            dietRecordListView.ItemsSource = _dietRecords;

            base.OnAppearing();
		}

        async void OnAddButtonClickedAsync(object sender, System.EventArgs e)
        {
            var diet1 = new DietRecord 
            { 
                Description = "description is this is lunch, I have beef noddle", 
                Calories=230, 
                Image="image1", 
                Date=DateTime.Now,
                Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0),
                UserId=App.user.UserId

            };
            //diet1.SetTime();

            await _connection.InsertAsync(diet1);

            _dietRecords.Add(diet1);


        }

        //void OnDeleteButtonClicked(object sender, System.EventArgs e)
        //{
        //    if(_dietRecords.Count!=0){
        //        var diet = _dietRecords[0];

        //        _connection.DeleteAsync(diet);

        //        _dietRecords.Remove(diet);
        //    }
           
        //}


        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDietPage());
        }


        void Delete_Clicked(object sender, System.EventArgs e)
        {
            var diet = (sender as MenuItem).CommandParameter as DietRecord;

            _connection.DeleteAsync(diet);

            _dietRecords.Remove(diet);

        }

        async void Edit_Clicked(object sender, System.EventArgs e)
        {
            var diet = (sender as MenuItem).CommandParameter as DietRecord;
            await Navigation.PushAsync(new DietEditPage(diet));
        }
	}
}

