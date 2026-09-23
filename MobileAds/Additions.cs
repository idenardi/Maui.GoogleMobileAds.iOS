using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace Maui.MobileAds
{
	public static partial class Constants
	{
		static string? _GoogleMobileAdsVersionString;

		// extern const unsigned char[] GoogleMobileAdsVersionString __attribute__((swift_name("GoogleMobileAdsVersion")));
		public static string GoogleMobileAdsVersionString {
			get {
				if (_GoogleMobileAdsVersionString is null)
					_GoogleMobileAdsVersionString = Marshal.PtrToStringUTF8 (NativeSymbolReader.GetAddress ("GoogleMobileAdsVersionString"))!;
				return _GoogleMobileAdsVersionString;
			}
		}
	}

	public static partial class AdSize
	{
		// extern const GADAdSize GADAdSizeBanner __attribute__((swift_name("AdSizeBanner")));
		public static GADAdSize Banner => NativeSymbolReader.GetAdSize ("GADAdSizeBanner");

		// extern const GADAdSize GADAdSizeLargeBanner __attribute__((swift_name("AdSizeLargeBanner")));
		public static GADAdSize LargeBanner => NativeSymbolReader.GetAdSize ("GADAdSizeLargeBanner");

		// extern const GADAdSize GADAdSizeMediumRectangle __attribute__((swift_name("AdSizeMediumRectangle")));
		public static GADAdSize MediumRectangle => NativeSymbolReader.GetAdSize ("GADAdSizeMediumRectangle");

		// extern const GADAdSize GADAdSizeFullBanner __attribute__((swift_name("AdSizeFullBanner")));
		public static GADAdSize FullBanner => NativeSymbolReader.GetAdSize ("GADAdSizeFullBanner");

		// extern const GADAdSize GADAdSizeLeaderboard __attribute__((swift_name("AdSizeLeaderboard")));
		public static GADAdSize Leaderboard => NativeSymbolReader.GetAdSize ("GADAdSizeLeaderboard");

		// extern const GADAdSize GADAdSizeSkyscraper __attribute__((swift_name("AdSizeSkyscraper")));
		public static GADAdSize Skyscraper => NativeSymbolReader.GetAdSize ("GADAdSizeSkyscraper");

		// extern const GADAdSize GADAdSizeFluid __attribute__((swift_name("AdSizeFluid")));
		public static GADAdSize Fluid => NativeSymbolReader.GetAdSize ("GADAdSizeFluid");

		// extern const GADAdSize GADAdSizeInvalid __attribute__((swift_name("AdSizeInvalid")));
		public static GADAdSize Invalid => NativeSymbolReader.GetAdSize ("GADAdSizeInvalid");

		// extern const GADAdSize kGADAdSizeSmartBannerPortrait __attribute__((deprecated("Use GADLargePortraitAnchoredAdaptiveBannerAdSizeWithWidth instead.")));
		[Obsolete ("Use GADLargePortraitAnchoredAdaptiveBannerAdSizeWithWidth instead.")]
		public static GADAdSize SmartBannerPortrait => NativeSymbolReader.GetAdSize ("kGADAdSizeSmartBannerPortrait");

		// extern const GADAdSize kGADAdSizeSmartBannerLandscape __attribute__((deprecated("Use GADLargeLandscapeAnchoredAdaptiveBannerAdSizeWithWidth instead.")));
		[Obsolete ("Use GADLargeLandscapeAnchoredAdaptiveBannerAdSizeWithWidth instead.")]
		public static GADAdSize SmartBannerLandscape => NativeSymbolReader.GetAdSize ("kGADAdSizeSmartBannerLandscape");
	}

	static class NativeSymbolReader
	{
		// The [Field] attributes in NativeSymbols are what make the native linker keep and export these
		// symbols, so keep that type (and its attributes) when trimming.
		[DynamicDependency (DynamicallyAccessedMemberTypes.All, typeof (NativeSymbols))]
		public static IntPtr GetAddress (string symbol)
		{
			var ptr = Dlfcn.dlsym (Dlfcn.RTLD.Default, symbol);
			if (ptr == IntPtr.Zero)
				throw new EntryPointNotFoundException ($"Unable to find the symbol '{symbol}' in the Google Mobile Ads SDK.");
			return ptr;
		}

		public static GADAdSize GetAdSize (string symbol)
			=> Marshal.PtrToStructure<GADAdSize> (GetAddress (symbol));
	}
}
