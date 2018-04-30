using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MyDiet.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
			Xamarin.Calabash.Start();
#endif

			ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            //to init Azure mobile client
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            //SQLitePCL.CurrentPlatform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
