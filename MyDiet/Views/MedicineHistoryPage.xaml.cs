using System;
using System.Collections.Generic;
using MyDiet.Models;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class MedicineHistoryPage : ContentPage
    {
        public MedicineHistoryPage()
        {
            InitializeComponent();
        }

		public MedicineHistoryPage(MedicineHistory medicineHistory)
        {
            InitializeComponent();
			BindingContext = medicineHistory;
        }


    }
}
