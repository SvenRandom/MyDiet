using System;
using System.Collections.Generic;
using MyDiet.Manager;
using MyDiet.Models;
using Xamarin.Forms;

namespace MyDiet.Views
{
    public partial class AddMedicinePage : ContentPage
    {
		Medicine currentMedicine;
		bool isNewItem=false;

		public AddMedicinePage(Medicine medicine) 
        {
            InitializeComponent();
			if(medicine==null){
				isNewItem = true;
				currentMedicine = new Medicine
				{
					Id = Guid.NewGuid().ToString(),
					UserId = App.account.Id             
                };


			}else
			{
				currentMedicine = medicine;
			}
			tableView.BindingContext = currentMedicine;

        }

		async void DoneClicked(object sender, EventArgs e)
		{
			MedicineManager medicineManager = MedicineManager.DefaultManager;
			await medicineManager.SaveTaskAsync(currentMedicine, isNewItem);
			App.contentChanged = true;
			await Navigation.PopAsync();
		}
    }
}
