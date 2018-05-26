#define OFFLINE_SYNC_ENABLED
using System;
using Microsoft.WindowsAzure.MobileServices;
using MyDiet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace MyDiet.Manager
{
	public partial class MedicineHistoryManager
    {
		static MedicineHistoryManager defaultInstance = new MedicineHistoryManager();
        MobileServiceClient client;
        
#if OFFLINE_SYNC_ENABLED
		IMobileServiceSyncTable<MedicineHistory> medicineTable;
#else
		IMobileServiceTable<MedicineHistory> medicineTable;
#endif

        const string offlineDbPath = @"localstore.db";

		private MedicineHistoryManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
			store.DefineTable<MedicineHistory>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

			this.medicineTable = client.GetSyncTable<MedicineHistory>();
#else
			this.dietTable = client.GetTable<Medicine>();
#endif
        }

		public static MedicineHistoryManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsOfflineEnabled
        {
			get { return medicineTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<MedicineHistory>; }
        }
  

		public async Task<ObservableCollection<MedicineHistory>> GetMedicinesAsync(bool syncItems = false)
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<MedicineHistory> items = await medicineTable
					.Where(medicine => medicine.UserId == App.email)
                    .ToEnumerableAsync();

				return new ObservableCollection<MedicineHistory>(items);
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

		public async Task SaveTaskAsync(MedicineHistory item, bool isNew)
        {
			try{
				if (isNew == true)
                {
					await medicineTable.InsertAsync(item);

                }
                else
                {
					await medicineTable.UpdateAsync(item);

                }
			}
			catch (Exception ex)
            {
				Debug.WriteLine(@"Exception: {0}", ex.Message);
            }
           

        }

		public async Task DeleteTaskAsync(MedicineHistory item)
        {
           
			await medicineTable.DeleteAsync(item);


        }




#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

				await this.medicineTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allDietItems",
					this.medicineTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
					await ResolveConflictAsync(error);
					Debug.WriteLine("confilct here.");
      //              if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
      //              {
      //                  //Update failed, reverting to server's copy.
      //                 await error.CancelAndUpdateItemAsync(error.Result);
						//Debug.WriteLine("Update failed, reverting to server's copy.");
      //              }
      //              else
      //              {
						//// Discard local change.
						//await error.CancelAndDiscardItemAsync();
						//Debug.WriteLine("Discard local change.");
                    //}

                    //Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }

		public async Task ResolveConflictAsync(MobileServiceTableOperationError error)
        {
			var serverItem = error.Result.ToObject<MedicineHistory>();
			var localItem = error.Item.ToObject<MedicineHistory>();

            // Note that you need to implement the public override Equals(TodoItem item)
            // method in the Model for this to work         
            // Client Always Wins
            //localItem.Version = serverItem.Version;
            await error.UpdateOperationAsync(JObject.FromObject(localItem));
			Debug.WriteLine("update here");

            // Server Always Wins
            // await error.CancelAndDiscardItemAsync();
        }

#endif
  

       
    }
}

