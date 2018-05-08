using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;

namespace MyDiet.Views
{
    public partial class MyDietPage : ContentPage
    {
		

		DietManager dietManager;

		int currentView;
		public MyDietPage()
        {
            InitializeComponent();
            dietManager = DietManager.DefaultManager;

			currentView = 0;

        }

		protected override void OnAppearing()
        {
            base.OnAppearing();
			//await RefreshItems(true, syncItems: true);
			if(currentView==0){
				TodayClicked();
             
            }
			if (currentView == 1)
				WeekClicked();

			if (currentView == 2)
				HistoryClicked();

            
			//await RefreshItems(true, syncItems: false);//show activity indicator and not sync

            



        }
       

        async void OnItemAdded(object sender, EventArgs e)
        {
            DietItem diet = null;
            await Navigation.PushAsync(new DietItemPage(diet)
              );
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var diet = e.SelectedItem as DietItem;

            await Navigation.PushAsync(new DietItemPage(diet));
        }

		public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }
        

		public async void OnSyncItems()
        {
            await RefreshItems(true, true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
				if (currentView == 0)
				{
					var temp = await dietManager.GetHistoryAsync(syncItems);
                    listView.ItemsSource = temp.Where(dietItem => dietItem.Date.Year == DateTime.Now.Year &&
                                                                   dietItem.Date.Month == DateTime.Now.Month &&
                                                                   dietItem.Date.Day == DateTime.Now.Day);
				}
					
				if (currentView == 1){
					var temp = await dietManager.GetHistoryAsync(syncItems);
                    listView.ItemsSource = temp.Where(dietItem => (DateTime.Now - dietItem.Date).Days <= 7)
						.OrderByDescending(DietItem => DietItem.Date);;
				}
            
				if (currentView == 2)
				{
					var temp = await dietManager.GetHistoryAsync(syncItems);
					listView.ItemsSource = temp.OrderByDescending(DietItem => DietItem.Date);
				}
            }
        }


		private class ActivityIndicatorScope : IDisposable
        {
            private bool showIndicator;
            private ActivityIndicator indicator;
            private Task indicatorDelay;

            public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
            {
                this.indicator = indicator;
                this.showIndicator = showIndicator;

                if (showIndicator)
                {
                    indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                this.indicator.IsVisible = isActive;
                this.indicator.IsRunning = isActive;
            }

            public void Dispose()
            {
                if (showIndicator)
                {
                    indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }

		void TodayClicked(object sender, System.EventArgs e)
        {
			TodayClicked();
        }
        
		async void TodayClicked()
        {
			var temp = await dietManager.GetHistoryAsync();
			listView.ItemsSource = temp.Where(dietItem => dietItem.Date.Year == DateTime.Now.Year &&
                                                           dietItem.Date.Month == DateTime.Now.Month  && 
                                                           dietItem.Date.Day == DateTime.Now.Day);
            today.BackgroundColor = Color.FromHex("#2196F3");
            today.TextColor = Color.White;
            week.BackgroundColor = Color.WhiteSmoke;
            week.TextColor = Color.FromHex("#2196F3");
            history.BackgroundColor = Color.WhiteSmoke;
            history.TextColor = Color.FromHex("#2196F3");
            currentView = 0;
        }
        
		void WeekClicked(object sender, System.EventArgs e)
        {
			WeekClicked();
        }
		async void WeekClicked()
        {
			var temp = await dietManager.GetHistoryAsync();         
			listView.ItemsSource = temp.Where(dietItem => (DateTime.Now - dietItem.Date).Days <= 7)
				.OrderByDescending(DietItem => DietItem.Date);
            today.BackgroundColor = Color.WhiteSmoke;
            today.TextColor = Color.FromHex("#2196F3");
            week.BackgroundColor = Color.FromHex("#2196F3");
            week.TextColor = Color.White;
            history.BackgroundColor = Color.WhiteSmoke;
            history.TextColor = Color.FromHex("#2196F3");
            currentView = 1;
        }



		void HistoryClicked(object sender, System.EventArgs e)
        {
			HistoryClicked();
        }
		async void HistoryClicked()
        {
            var temp = await dietManager.GetHistoryAsync();
			listView.ItemsSource = temp.OrderByDescending(DietItem=>DietItem.Date);

            today.BackgroundColor = Color.WhiteSmoke;
            today.TextColor = Color.FromHex("#2196F3");
            week.BackgroundColor = Color.WhiteSmoke;
            week.TextColor = Color.FromHex("#2196F3");
            history.BackgroundColor = Color.FromHex("#2196F3");
            history.TextColor = Color.White;
            currentView = 2;
        }

    }
}
