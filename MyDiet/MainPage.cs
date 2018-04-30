using System;
using MyDiet.Views;
using Xamarin.Forms;

namespace MyDiet
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page myDietPage, homePage, dietPage, medicinePage, activityPage, profilePage = null;
            

			myDietPage = new MyDietPage()
            {
                Title = "My Diet"
            };

                        homePage = new HomePage()
                        {
                            Title = "Home"
                        };

                        dietPage = new DietPage()
                        {
                            Title = "Diet"
                        };
			         

                        medicinePage = new MedicinePage()
                        {
                            Title = "Medicine"
                        };

                        activityPage = new ActivityPage()
                        {
                            Title = "Activity"
                        };

                        profilePage = new ProfilePage()
                        {
                            Title = "Profile"
                        };

			if (Device.RuntimePlatform == Device.iOS)
			{
				profilePage.Icon = "tab_about.png";

				homePage.Icon = "tab_feed.png";
			}


            Children.Add(homePage);
            //Children.Add(dietPage);
			Children.Add(myDietPage);
            //Children.Add(medicinePage);
            Children.Add(activityPage);
            Children.Add(profilePage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
