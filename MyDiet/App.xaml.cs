using MyDiet.Models;
using MyDiet.Views;
using Xamarin.Forms;

namespace MyDiet
{
    public partial class App : Application
    {
        //public static bool UseMockDataStore = true; 
        
		public static AccountInfo account;
		public static string email;

		public static string barcode = null;
		public static bool contentChanged = false; 

        public App()
        {
            InitializeComponent();

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else
                //DependencyService.Register<CloudDataStore>();
            
			MainPage = new LoadingPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

        }

        protected override void OnResume()
        {
            // Handle when your app resumes
           
        }
        

    }
}
