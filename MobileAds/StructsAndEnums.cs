using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace Maui.MobileAds
{
	[Native]
	public enum GADAdChoicesPosition : long
	{
		TopRightCorner,
		TopLeftCorner,
		BottomRightCorner,
		BottomLeftCorner
	}

	[Native]
	public enum GADAdFormat : long
	{
		Banner = 0,
		Interstitial = 1,
		Rewarded = 2,
		Native = 3,
		RewardedInterstitial = 4,
		AppOpen = 6
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GADAdSize
	{
		public CGSize size;

		public nuint flags;
	}

	public static class AdSize
	{
		// extern GADAdSize GADPortraitInlineAdaptiveBannerAdSizeWithWidth (CGFloat width) __attribute__((swift_name("portraitInlineAdaptiveBanner(width:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADPortraitInlineAdaptiveBannerAdSizeWithWidth (nfloat width);

		// extern GADAdSize GADLandscapeInlineAdaptiveBannerAdSizeWithWidth (CGFloat width) __attribute__((swift_name("landscapeInlineAdaptiveBanner(width:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADLandscapeInlineAdaptiveBannerAdSizeWithWidth (nfloat width);

		// extern GADAdSize GADCurrentOrientationInlineAdaptiveBannerAdSizeWithWidth (CGFloat width) __attribute__((swift_name("currentOrientationInlineAdaptiveBanner(width:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADCurrentOrientationInlineAdaptiveBannerAdSizeWithWidth (nfloat width);

		// extern GADAdSize GADInlineAdaptiveBannerAdSizeWithWidthAndMaxHeight (CGFloat width, CGFloat maxHeight) __attribute__((swift_name("inlineAdaptiveBanner(width:maxHeight:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADInlineAdaptiveBannerAdSizeWithWidthAndMaxHeight (nfloat width, nfloat maxHeight);

		// extern GADAdSize GADPortraitAnchoredAdaptiveBannerAdSizeWithWidth (CGFloat width) __attribute__((swift_name("portraitAnchoredAdaptiveBanner(width:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADPortraitAnchoredAdaptiveBannerAdSizeWithWidth (nfloat width);

		// extern GADAdSize GADLandscapeAnchoredAdaptiveBannerAdSizeWithWidth (CGFloat width) __attribute__((swift_name("landscapeAnchoredAdaptiveBanner(width:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADLandscapeAnchoredAdaptiveBannerAdSizeWithWidth (nfloat width);

		// extern GADAdSize GADCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth (CGFloat width) __attribute__((swift_name("currentOrientationAnchoredAdaptiveBanner(width:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth (nfloat width);

		// extern GADAdSize GADAdSizeFromCGSize (CGSize size) __attribute__((swift_name("adSizeFor(cgSize:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADAdSizeFromCGSize (CGSize size);

		// extern GADAdSize GADAdSizeFullWidthPortraitWithHeight (CGFloat height) __attribute__((swift_name("fullWidthPortrait(height:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADAdSizeFullWidthPortraitWithHeight (nfloat height);

		// extern GADAdSize GADAdSizeFullWidthLandscapeWithHeight (CGFloat height) __attribute__((swift_name("fullWidthLandscape(height:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADAdSizeFullWidthLandscapeWithHeight (nfloat height);

		// extern BOOL GADAdSizeEqualToSize (GADAdSize size1, GADAdSize size2) __attribute__((swift_name("isAdSizeEqualToSize(size1:size2:)")));
		[DllImport ("__Internal")]
		public static extern bool GADAdSizeEqualToSize (GADAdSize size1, GADAdSize size2);

		// extern BOOL IsGADAdSizeValid (GADAdSize size) __attribute__((swift_name("isAdSizeValid(size:)")));
		[DllImport ("__Internal")]
		public static extern bool IsGADAdSizeValid (GADAdSize size);

		// extern BOOL GADAdSizeIsFluid (GADAdSize size) __attribute__((swift_name("isAdSizeFluid(size:)")));
		[DllImport ("__Internal")]
		public static extern bool GADAdSizeIsFluid (GADAdSize size);

		// extern CGSize CGSizeFromGADAdSize (GADAdSize size) __attribute__((swift_name("cgSize(for:)")));
		[DllImport ("__Internal")]
		public static extern CGSize CGSizeFromGADAdSize (GADAdSize size);

		// extern NSString * _Nonnull NSStringFromGADAdSize (GADAdSize size) __attribute__((swift_name("string(for:)")));
		[DllImport ("__Internal")]
		public static extern NSString NSStringFromGADAdSize (GADAdSize size);

		// extern NSValue * _Nonnull NSValueFromGADAdSize (GADAdSize size) __attribute__((swift_name("nsValue(for:)")));
		[DllImport ("__Internal")]
		public static extern NSValue NSValueFromGADAdSize (GADAdSize size);

		// extern GADAdSize GADAdSizeFromNSValue (NSValue * _Nonnull value) __attribute__((swift_name("adSizeFor(nsValue:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADAdSizeFromNSValue (NSValue value);

		// extern NSString * _Nonnull GADGetStringFromVersionNumber (GADVersionNumber version) __attribute__((swift_name("string(for:)")));
		[DllImport ("__Internal")]
		public static extern NSString GADGetStringFromVersionNumber (GADVersionNumber version);

		// extern GADAdSize GADClosestValidSizeForAdSizes (GADAdSize original, NSArray<NSValue *> * _Nonnull possibleAdSizes) __attribute__((swift_name("closestValidSizeForAdSizes(original:possibleAdSizes:)")));
		[DllImport ("__Internal")]
		public static extern GADAdSize GADClosestValidSizeForAdSizes (GADAdSize original, NSValue[] possibleAdSizes);
	}

	[Native]
	public enum GADAdValuePrecision : long
	{
		Unknown = 0,
		Estimated = 1,
		PublisherProvided = 2,
		Precise = 3
	}

	[Native]
	public enum GADAdapterInitializationState : long
	{
		NotReady = 0,
		Ready = 1
	}

	[Native]
	public enum GADMediaAspectRatio : long
	{
		Unknown = 0,
		Any = 1,
		Landscape = 2,
		Portrait = 3,
		Square = 4
	}

	[Native]
	public enum GADPublisherPrivacyPersonalizationState : long
	{
		Default = 0,
		Enabled = 1,
		Disabled = 2
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GADVersionNumber
	{
		public nint majorVersion;

		public nint minorVersion;

		public nint patchVersion;
	}

	[Native]
	public enum GADErrorCode : long
	{
		InvalidRequest = 0,
		NoFill = 1,
		NetworkError = 2,
		ServerError = 3,
		OSVersionTooLow = 4,
		Timeout = 5,
		MediationDataError = 7,
		MediationAdapterError = 8,
		MediationInvalidAdSize = 10,
		InternalError = 11,
		InvalidArgument = 12,
		AdAlreadyUsed = 19,
		ApplicationIdentifierMissing = 20,
		ReceivedInvalidAdString = 21
	}

	[Native]
	public enum GADPresentationErrorCode : long
	{
		CodeAdNotReady = 15,
		CodeAdTooLarge = 16,
		CodeInternal = 17,
		CodeAdAlreadyUsed = 18,
		NotMainThread = 21,
		Mediation = 22
	}

	[Native]
	public enum GADMBannerAnimationType : long
	{
		None = 0,
		FlipFromLeft = 1,
		FlipFromRight = 2,
		CurlUp = 3,
		CurlDown = 4,
		SlideFromLeft = 5,
		SlideFromRight = 6,
		FadeIn = 7,
		Random = 8
	}
}
