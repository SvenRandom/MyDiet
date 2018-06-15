
using System;
using Microsoft.WindowsAzure.MobileServices;
using MyDiet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;


namespace MyDiet.Manager
{
	public class PackageFoodDatabaseManager 
    {

        
        MobileServiceClient client;
		IMobileServiceTable<PackageFoodDatabase> MedicineDatabase;


		public PackageFoodDatabaseManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            
			this.MedicineDatabase = client.GetTable<PackageFoodDatabase>();
        }
              
		public async Task<PackageFoodDatabase> GetFoodAsync(String id)
        {
            try
            {
				var items = await MedicineDatabase.LookupAsync(id);

				return items;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

		public async Task SaveTaskAsync(PackageFoodDatabase item, bool isNew)
        {
			
                if (isNew == true)
                {
				await MedicineDatabase.InsertAsync(item);
                }
                else
                {
				await MedicineDatabase.UpdateAsync(item);
                }
		    
          
        }

		public async Task DeleteTaskAsync(PackageFoodDatabase item)
        {
           
			await MedicineDatabase.DeleteAsync(item);


        }
       
    }
}

