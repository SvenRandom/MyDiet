
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
    public class AccountManager 
    {

        
        MobileServiceClient client;
        IMobileServiceTable<AccountInfo> accountTable;


		public AccountManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            
			this.accountTable = client.GetTable<AccountInfo>();
        }
              
		public async Task<AccountInfo> GetAccountInfosAsync(String email)
        {
            try
            {
				var items = await accountTable.LookupAsync(email);
					

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

		public async Task SaveTaskAsync(AccountInfo item, bool isNew)
        {
			
                if (isNew == true)
                {
					await accountTable.InsertAsync(item);
                }
                else
                {
					await accountTable.UpdateAsync(item);
                }
		    
          
        }

		public async Task DeleteTaskAsync(AccountInfo item)
        {
           
			await accountTable.DeleteAsync(item);


        }
       
    }
}

