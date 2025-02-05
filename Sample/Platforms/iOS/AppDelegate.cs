using Foundation;
using Sample.Platforms.iOS;
using UIKit;

namespace Sample;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var privacyAndConsentService = new PrivacyAndConsentService();
        privacyAndConsentService.RequestConsentInfoUpdate(true);

        return base.FinishedLaunching(application, launchOptions);
    }
}