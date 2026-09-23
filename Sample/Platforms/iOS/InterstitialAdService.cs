using Foundation;
using Maui.MobileAds;

namespace Sample.Platforms.iOS;

public class InterstitialAdService
{
    // Google's test interstitial ad unit
    const string AdUnitId = "ca-app-pub-3940256099942544/4411468910";

    // Both references must be kept: the SDK doesn't retain the loaded ad after the load completion
    // handler, and fullScreenContentDelegate is a weak property.
    GADInterstitialAd? _interstitialAd;
    readonly InterstitialDelegate _delegate;

    public InterstitialAdService()
    {
        _delegate = new InterstitialDelegate(this);
    }

    public bool IsLoaded => _interstitialAd is not null;

    public void Load()
    {
        GADInterstitialAd.LoadWithAdUnitID(AdUnitId, new GADRequest(), (ad, error) =>
        {
            if (error is not null || ad is null)
            {
                Console.WriteLine($"Interstitial failed to load: {error?.LocalizedDescription}");
                return;
            }

            Console.WriteLine("Interstitial loaded");
            ad.FullScreenContentDelegate = _delegate;
            _interstitialAd = ad;
        });
    }

    public void Show()
    {
        var viewController = Platform.GetCurrentUIViewController();
        if (_interstitialAd is null || viewController is null)
        {
            Console.WriteLine("Interstitial not ready");
            return;
        }

        if (!_interstitialAd.CanPresentFromRootViewController(viewController, out var error))
        {
            Console.WriteLine($"Interstitial can't be presented: {error?.LocalizedDescription}");
            return;
        }

        _interstitialAd.PresentFromRootViewController(viewController);
    }

    class InterstitialDelegate(InterstitialAdService service) : GADFullScreenContentDelegate
    {
        public override void AdDidRecordImpression(IGADFullScreenPresentingAd ad)
            => Console.WriteLine($"Interstitial AdDidRecordImpression ({ad.GetType().Name})");

        public override void AdDidRecordClick(IGADFullScreenPresentingAd ad)
            => Console.WriteLine("Interstitial AdDidRecordClick");

        public override void AdWillPresentFullScreenContent(IGADFullScreenPresentingAd ad)
            => Console.WriteLine($"Interstitial AdWillPresentFullScreenContent ({ad.GetType().Name}, same instance: {ReferenceEquals(ad, service._interstitialAd)})");

        public override void AdDidFailToPresentFullScreenContent(IGADFullScreenPresentingAd ad, NSError error)
        {
            Console.WriteLine($"Interstitial AdDidFailToPresentFullScreenContent: {error.LocalizedDescription}");
            service._interstitialAd = null;
            service.Load();
        }

        public override void AdWillDismissFullScreenContent(IGADFullScreenPresentingAd ad)
            => Console.WriteLine("Interstitial AdWillDismissFullScreenContent");

        public override void AdDidDismissFullScreenContent(IGADFullScreenPresentingAd ad)
        {
            Console.WriteLine("Interstitial AdDidDismissFullScreenContent");
            // An interstitial can only be shown once, so load the next one.
            service._interstitialAd = null;
            service.Load();
        }
    }
}
