using MyDiet.Models;
using MyDiet.Views;
using Xamarin.Forms;


namespace MyDiet
{
    public partial class App : Application
    {
        //public static bool UseMockDataStore = true;
        //public static string BackendUrl = "https://localhost:5000";
        public static bool IsUserLoggedIn { get; set; }
        public static User user; 

        public App()
        {
            InitializeComponent();

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else
                //DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (!IsUserLoggedIn)
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            }

            else
                MainPage = new NavigationPage(new MainPage());
            
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new RegisterPage();
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
