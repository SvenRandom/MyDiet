
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
    public class MedicineDatabaseManager 
    {

        
        MobileServiceClient client;
		IMobileServiceTable<MedicineDatabase> MedicineDatabase;


		public MedicineDatabaseManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            
			this.MedicineDatabase = client.GetTable<MedicineDatabase>();
        }
              
		public async Task<MedicineDatabase> GetMedicineAsync(String id)
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

		public async Task SaveTaskAsync(MedicineDatabase item, bool isNew)
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

		public async Task DeleteTaskAsync(MedicineDatabase item)
        {
           
			await MedicineDatabase.DeleteAsync(item);


        }
       
    }
}

