using Microsoft.Maui;
using Microsoft.Maui.Handlers;

#if IOS
using NativeBannerAdView = Maui.MobileAds.GADBannerView;
using Foundation;
using UIKit;
using Maui.MobileAds;
#else
using NativeBannerAdView = object;
#endif

namespace Sample;

public interface IGoogleAdsBannerView : IView
{

}

public partial class GoogleAdsBannerHandler : ViewHandler<IGoogleAdsBannerView, NativeBannerAdView>
{

    public GoogleAdsBannerHandler() : base(ViewHandler.ViewMapper)
    {
		
    }
    
    protected override NativeBannerAdView CreatePlatformView()
    {
#if IOS
        var viewWidth = UIKit.UIApplication.SharedApplication.KeyWindow.Frame.Width;
        
        // Here the current interface orientation is used. Use
        // GADLandscapeAnchoredAdaptiveBannerAdSizeWithWidth or
        // GADPortraitAnchoredAdaptiveBannerAdSizeWithWidth if you prefer to load an ad of a
        // particular orientation,
        var adaptiveSize = AdSize.GADCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(viewWidth);
        var bannerView = new GADBannerView(adaptiveSize);
        bannerView.Delegate = new BannerDg();
        return bannerView;
#else
		return null;
#endif
    }

    protected override void ConnectHandler(NativeBannerAdView platformView)
    {
        base.ConnectHandler(platformView);
        
        #if IOS
        platformView.AdUnitID = "ca-app-pub-3940256099942544/2934735716";
        platformView.RootViewController = this.ViewController;

        platformView.LoadRequest(new GADRequest());
#endif
    }
}

public partial class GoogleAdsBannerView : View, IGoogleAdsBannerView
{
}

#if IOS
public class BannerDg : GADBannerViewDelegate
{
   public override void BannerViewDidFailToReceiveAd(GADBannerView bannerView, NSError error)
	{
		Console.WriteLine("DidFailToReceiveAd " + error.Description);
	}

	public override void BannerViewDidReceiveAd(GADBannerView bannerView)
	{
		Console.WriteLine("BannerViewDidReceiveAd");
	}
}

#endif