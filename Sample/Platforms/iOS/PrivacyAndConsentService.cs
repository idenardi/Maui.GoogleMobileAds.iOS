using Maui.MobileAds;
using Foundation;
using System.Diagnostics;
using Maui.UserMessagingPlatform;

namespace Sample.Platforms.iOS;

public class PrivacyAndConsentService
{
    private readonly UMPConsentInformation _consentInformation;
    private static readonly Lock LockObject = new();
    private bool _isMobileAdsSDKInitialized;

    public PrivacyAndConsentService()
    {
        _consentInformation = UMPConsentInformation.SharedInstance;
    }

    public void RequestConsentInfoUpdate(bool? isDebugGeographyEEA = null)
    {
        UMPRequestParameters consentParameters = new();
        if (isDebugGeographyEEA ?? false)
        {
            consentParameters.DebugSettings = new UMPDebugSettings()
            {
                Geography = UMPDebugGeography.Eea
            };
        }

        _consentInformation.RequestConsentInfoUpdateWithParameters(consentParameters, OnConsentInformationUpdateCompletionHandler);

        if (_consentInformation.CanRequestAds)
        {
            InitializeMobileAdsSDK();
        }
    }

    public void ShowPrivacyOptionsForm()
    {
        if (IsPrivacyOptionsRequired())
        {
            var viewController = Platform.GetCurrentUIViewController();

            if (viewController is not null)
            {
                UMPConsentForm.PresentPrivacyOptionsFormFromViewController(viewController, ConsentFormPresentCompletionHandler);
            }
        }
    }

    public bool IsPrivacyOptionsRequired() =>
        _consentInformation.PrivacyOptionsRequirementStatus == UMPPrivacyOptionsRequirementStatus.Required;

    private void OnConsentInformationUpdateCompletionHandler(NSError? error)
    {
        if (error is not null)
        {
            Debug.WriteLine($"{error.Code} - {error.Description}");
        }

        var viewController = Platform.GetCurrentUIViewController();
        if (viewController is not null)
        {
            UMPConsentForm.LoadAndPresentIfRequiredFromViewController(viewController, ConsentFormPresentCompletionHandler);
        }
    }

    private void ConsentFormPresentCompletionHandler(NSError? error)
    {
        if (error is not null)
        {
            Debug.WriteLine($"{error.Code} - {error.Description}");
        }

        if (_consentInformation.CanRequestAds)
        {
            InitializeMobileAdsSDK();
        }
    }

    private void InitializeMobileAdsSDK()
    {
        lock (LockObject)
        {
            if (!_isMobileAdsSDKInitialized)
            {
                GADMobileAds.SharedInstance.StartWithCompletionHandler(CompletionHandler);
                _isMobileAdsSDKInitialized = true;
            }
        }
    }

    void CompletionHandler(GADInitializationStatus status) { }

    public bool CanRequestAds() =>
        _consentInformation.CanRequestAds;

    public void Reset() =>
        _consentInformation.Reset();
}
