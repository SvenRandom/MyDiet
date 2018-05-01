using MyDiet.Models;
using MyDiet.Views;
using Xamarin.Forms;


namespace MyDiet
{
    public partial class App : Application
    {
        //public static bool UseMockDataStore = true;
        //public static string CurrentEmail;
        
		public static AccountInfo account;
		public static User user;
		//private const string CurrentAccount = "CurrentUSer";
		//private const string logstate = "IsLoggedIn";

        public App()
        {
            InitializeComponent();

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else
                //DependencyService.Register<CloudDataStore>();

            //if (Device.RuntimePlatform == Device.iOS)
            
                //if (!IsUserLoggedIn)
                //{
                //    MainPage = new NavigationPage(new LoginPage());
                //}
                //else
                //{
                //    MainPage = new NavigationPage(new MainPage());
                //}
            

           
            
            //MainPage = new NavigationPage(new MyAzurePage());
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

		//public User CurrentUser
		//{
		//	get{
		//		if (Application.Current.Properties.ContainsKey(CurrentAccount))
		//			return (User)Application.Current.Properties[CurrentAccount];
		//		else
		//			return null;
		//	}
		//	set{
		//		Application.Current.Properties[CurrentAccount] = value;

		//	}
		//}

		//public bool IsLoggedIn
    //    {
    //        get
    //        {
				//if (Application.Current.Properties.ContainsKey(logstate))
				//	return (bool)Application.Current.Properties[logstate];
    //            else
				//	return false;
    //        }
    //        set
    //        {
				//Application.Current.Properties[logstate] = value;

        //    }
        //}

    }
}
