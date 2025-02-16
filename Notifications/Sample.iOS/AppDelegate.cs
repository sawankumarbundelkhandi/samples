﻿using Foundation;
using Shiny;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


namespace Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			this.ShinyFinishedLaunching(new Startup());
			Forms.Init();
			FormsMaps.Init();
			this.LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
	}
}
