using System;
using System.Collections.Generic;
using Microcharts;  
using SkiaSharp; 
using Entry = Microcharts.Entry;  
using Xamarin.Forms;
using MyDiet.Manager;
using System.Linq;
using MyDiet.Models;

namespace MyDiet.Views
{
    public partial class ActivityPage : ContentPage
    {
		ActivityDataManager activityDataManager = new ActivityDataManager();
		List<ActivityData> allData=null;
		int start = 0; 
        public ActivityPage()
        {
			InitializeComponent();

        }

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			allData = await activityDataManager.GetActivityDataAsync();
			var current = allData.Where(data=>data.date.Year == DateTime.Now.Year &&
			                            data.date.Month == DateTime.Now.Month &&
			                            data.date.Day == DateTime.Now.Day);
			var number =current.Count();

            if(number>=1)
			{
				var t = current.ElementAt(0);
				kmLabel.Text = t.walkedkm.ToString("0.00");
				System.Diagnostics.Debug.WriteLine("km: " + t.walkedkm);
				stepsLabel.Text = t.steps.ToString();
				floorLabel.Text = t.climbedFloor.ToString();
			}
			if(allData.Count>1)
			{
				setChart();
			}
			else{
				Chart4.Chart = new BarChart() { Entries = entries };
			}
		}

        public void setChart()
		{
			
			entries.Clear();
			for (int i = 0; i < 7;i++)
			{
				var temp = allData.ElementAt(i + start);
				Entry entry = new Entry(temp.steps);
				entry.Label = temp.date.DayOfWeek.ToString();
				entry.ValueLabel = temp.steps.ToString();
				if(temp.steps<4000)
				{
					entry.Color = SKColor.Parse("#FF1943");
				}else if(temp.steps<10000)
				{
					entry.Color = SKColor.Parse("#2196F3");
				}else
				{
					entry.Color = SKColor.Parse("#00CED1");
				}

				entries.Add(entry);

				if(i==0){
					day6.Text = temp.date.ToString("M.d");
				}
				if (i == 1)
                {
					day5.Text = temp.date.ToString("M.d");
                }
				if (i == 2)
                {
					day4.Text = temp.date.ToString("M.d");
                }
				if (i == 3)
                {
					day3.Text = temp.date.ToString("M.d");
                }
				if (i == 4)
                {
						day2.Text = temp.date.ToString("M.d");
                }
				if (i == 5)
                {
					day1.Text = temp.date.ToString("M.d");
                }
				if (i == 6)
                {
					day0.Text = temp.date.ToString("M.d");
                }
					
			}

			entries.Reverse();

			Chart4.Chart = new BarChart() { Entries = entries };
		}


		List<Entry> entries = new List<Entry>  
        {  
			new Entry(0)
            {
				Color =  SKColor.Parse("#FF1943"),
                Label = "Thursday",
                ValueLabel = "0"
            },
            new Entry(0)
            {
				Color =  SKColor.Parse("#FF1943"),
                Label = "Friday",
                ValueLabel = "0"
            },
            new Entry(0)
            {
                Color =  SKColor.Parse("#FF1943"),
                Label = "Saturday",
                ValueLabel = "0"
            },
			new Entry(0)
            {
				Color =  SKColor.Parse("#FF1943"),
                Label = "Sunday",
                ValueLabel = "0"
            },
            new Entry(0)  
            {  
                Color=SKColor.Parse("#FF1943"),  
                Label ="Monday",  
				ValueLabel = "0"  
            },  
            new Entry(0)  
            {  
				Color = SKColor.Parse("#FF1943"),  
                Label = "TuesDay",  
				ValueLabel = "0"  
            },  
            new Entry(0)  
            {  
				Color =  SKColor.Parse("#FF1943"),  
                Label = "Wednesday",  
				ValueLabel = "0"  
            },  

            };  
        
		void OnLeftTapped(object sender, System.EventArgs e)
        {
			if(allData.Count>1)
			{
				//System.Diagnostics.Debug.WriteLine("start: " + start);
                if (start >= 30)
                {
                    DisplayAlert("Warning", "No more data", "OK");
                }
                else
                {
                    start += 7;
                    setChart();
                }
			}
			else
				DisplayAlert("Warning", "No more data", "OK");

        }

		void OnRightTapped(object sender, System.EventArgs e)
        {
			if(allData.Count > 1)
			{
				if (start >= 6)
                {
                    start -= 7;
                    setChart();
                }
			}
			else
				DisplayAlert("Warning", "No more data", "OK");
            
        }
	
    }
}
