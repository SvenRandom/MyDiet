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
                Label ="January",  
				ValueLabel = "3490"  
            },  
            new Entry(4483)  
            {  
                Color = SKColor.Parse("#00BFFF"),  
                Label = "March",  
				ValueLabel = "4483"  
            },  
            new Entry(1236)  
            {  
				Color =  SKColor.Parse("#2196F3"),  
                Label = "Octobar",  
				ValueLabel = "1236"  
            },  
			new Entry(7694)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
				ValueLabel = "7694"
            },
			new Entry(13952)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
				ValueLabel = "13952"
            },
			new Entry(1100)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
				ValueLabel = "1100"
            },
			new Entry(5700)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
				ValueLabel = "5700"
            },
            };  
        

	
    }
}
