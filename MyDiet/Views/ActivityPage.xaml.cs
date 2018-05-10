using System;
using System.Collections.Generic;
using Microcharts;  
using SkiaSharp; 
using Entry = Microcharts.Entry;  
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class ActivityPage : ContentPage
    {
        public ActivityPage()
        {
            InitializeComponent();
			 
            Chart4.Chart = new BarChart() { Entries = entries };

            //Chart5.Chart = new PointChart() { Entries = entries };

        }

		List<Entry> entries = new List<Entry>  
        {  
            new Entry(3490)  
            {  
                Color=SKColor.Parse("#FF1943"),  
                Label ="Monday",  
				ValueLabel = "3490"  
            },  
            new Entry(4483)  
            {  
				Color = SKColor.Parse("#2196F3"),  
                Label = "TuesDay",  
				ValueLabel = "4483"  
            },  
            new Entry(1236)  
            {  
				Color =  SKColor.Parse("#FF1943"),  
                Label = "Wednesday",  
				ValueLabel = "1236"  
            },  
			new Entry(7694)
            {
				Color =  SKColor.Parse("#2196F3"),
                Label = "Thursday",
				ValueLabel = "7694"
            },
			new Entry(13952)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Friday",
				ValueLabel = "13952"
            },
			new Entry(1100)
            {
				Color =  SKColor.Parse("#FF1943"),
                Label = "Saturday",
				ValueLabel = "1100"
            },
			new Entry(5700)
            {
				Color =  SKColor.Parse("#2196F3"),
                Label = "Sunday",
				ValueLabel = "5700"
            },
            };  
        

	
    }
}
