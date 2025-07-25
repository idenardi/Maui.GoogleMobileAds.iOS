param (
	[String]$MobileAdsVersion = '12.7.0',
	[String]$UserMessagingPlatformVersion = '3.0.0',
	[String]$BuildPath = '.build/',
	[Bool]$BuildNuGet = $true,
	[Bool]$GenerateBindings = $false,
	[String]$BuildNumber = '0',
	[Bool]$IsRelease = $false
)

Function DownloadGoogleMobileAdsSdks(
	[String]$MobileAdsVersion = '12.7.0',
	[String]$UserMessagingPlatformVersion = '3.0.0',
	[String]$DownloadPath = '.build/'
){
	# Get the download URL's for the Google Mobile Ads SDK tar.gz
	$GoogleMobileAdsSdkDownloadUrl = ((Invoke-WebRequest -Uri $GoogleMobileAdsPodspecUrl).Content | ConvertFrom-Json).source.http
	$GoogleUserMessagingPlatformDownloadUrl = ((Invoke-WebRequest -Uri $GoogleUserMessagingPlatformPodspecUrl).Content | ConvertFrom-Json).source.http

	# Make sure .build/ exists
	New-Item -ItemType Directory -Force -Path $DownloadPath -ErrorAction SilentlyContinue

	$AdsTgzPath = (Join-Path $DownloadPath "GoogleMobileAds.tar.gz")
	$UserMessagingPlatformTgzPath = (Join-Path $DownloadPath "GoogleUserMessagingPlatform.tar.gz")

	# Download the SDK tar.gz files
	Invoke-WebRequest $GoogleMobileAdsSdkDownloadUrl -OutFile $AdsTgzPath
	Invoke-WebRequest $GoogleUserMessagingPlatformDownloadUrl -OutFile $UserMessagingPlatformTgzPath

	$AdsSdkPath = (Join-Path $DownloadPath "GoogleMobileAds")
	$UserMessagingPlatformSdkPath = (Join-Path $DownloadPath "GoogleUserMessagingPlatform")
	
	# Make directories to extract
	New-Item -ItemType Directory -Force -Path $AdsSdkPath -ErrorAction SilentlyContinue
	New-Item -ItemType Directory -Force -Path $UserMessagingPlatformSdkPath -ErrorAction SilentlyContinue

	# Extract the tar.gz files
	& tar -xzf $AdsTgzPath -C $AdsSdkPath
	& tar -xzf $UserMessagingPlatformTgzPath -C $UserMessagingPlatformSdkPath
}

$GoogleUserMessagingPlatformPodspecUrl = "https://github.com/CocoaPods/Specs/raw/master/Specs/7/e/f/GoogleUserMessagingPlatform/$UserMessagingPlatformVersion/GoogleUserMessagingPlatform.podspec.json"
$GoogleMobileAdsPodspecUrl = "https://github.com/CocoaPods/Specs/raw/master/Specs/5/9/a/Google-Mobile-Ads-SDK/$MobileAdsVersion/Google-Mobile-Ads-SDK.podspec.json"

# Clear out old
Remove-Item -Recurse -Force -Path $BuildPath -ErrorAction SilentlyContinue

DownloadGoogleMobileAdsSdks -MobileAdsVersion $MobileAdsVersion -UserMessagingPlatformVersion $UserMessagingPlatformVersion -DownloadPath $BuildPath

if ($BuildNuGet -eq $true) {

	if ($IsRelease -eq $true) {
		$NuGetMobileAdsVersion = "$MobileAdsVersion.$BuildNumber"
		$NuGetUserMessagingPlatformVersion = "$UserMessagingPlatformVersion.$BuildNumber"
	} else {
		$NuGetMobileAdsVersion = "$MobileAdsVersion-ci.$BuildNumber"
		$NuGetUserMessagingPlatformVersion = "$UserMessagingPlatformVersion-ci.$BuildNumber"
	}

	$NuGetOutputPath = (Join-Path $BuildPath "NuGet")
	New-Item -ItemType Directory -Force -Path $NuGetOutputPath -ErrorAction SilentlyContinue

	dotnet build -t:Pack -c:Release -p:PackageVersion=$NuGetMobileAdsVersion -p:PackageOutputPath=$NuGetOutputPath ./MobileAds/MobileAds.csproj
	dotnet build -t:Pack -c:Release -p:PackageVersion=$NuGetUserMessagingPlatformVersion -p:PackageOutputPath=$NuGetOutputPath ./UserMessagingPlatform/UserMessagingPlatform.csproj
}

if ($GenerateBindings -eq $true) {
	$BindingOutputPath = (Join-Path $BuildPath "Bindings")
	New-Item -ItemType Directory -Force -Path $BindingOutputPath -ErrorAction SilentlyContinue

	& sharpie bind --sdk=iphoneos18.0 --output (Join-Path $BindingOutputPath "UserMessagingPlatform/") --namespace=UserMessagingPlatform --framework (Join-Path $BuildPath "GoogleUserMessagingPlatform/Frameworks/Release/UserMessagingPlatform.xcframework/ios-arm64/UserMessagingPlatform.framework")
	& sharpie bind --sdk=iphoneos18.0 --output (Join-Path $BindingOutputPath "MobileAds/") --namespace=GoogleMobileAds --framework (Join-Path $BuildPath "GoogleMobileAds/Frameworks/GoogleMobileAdsFramework/GoogleMobileAds.xcframework/ios-arm64/GoogleMobileAds.framework")
}
