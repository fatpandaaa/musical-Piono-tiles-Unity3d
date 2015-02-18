using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using ChartboostSDK;

public class Admanager : MonoBehaviour
{
	public static int GameState;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	private static bool created = false;

	public static int adcounter;

		



	void FixedUpdate ()
	{
		if (PlayerPrefs.GetInt ("SKU_remove_Ads") == 1) {
			//////no add , user parchase the no ad section 
		} else {
			
			if (GameState == 1) {
				//MainMenu
				GameState = 0;

				adcounter++;
				if (adcounter == 3) {
					RequestInterstitial ();
					Chartboost.cacheInterstitial (CBLocation.Default);
					adcounter = 0;
				}
		
			}
			if (GameState == 2) {
				//gameOver
				Debug.Log ("GameOver ad calling");
				GameState = 0;
				RequestInterstitial ();
				Chartboost.cacheInterstitial (CBLocation.Default);
				
			}
			if (GameState == 3) {
				//gamepause
				GameState = 0;
				RequestInterstitial ();
				Chartboost.cacheInterstitial (CBLocation.Default);
				
			}
			if (GameState == 4) {
				//level Select
				GameState = 0;
				RequestInterstitial ();
				Chartboost.cacheInterstitial (CBLocation.Default);		
			}
		}	
	}


	private void RequestBanner ()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-9985775175828398/1393706267";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-9985775175828398/5823905860";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView (adUnitId, AdSize.SmartBanner, AdPosition.Top);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd (createAdRequest ());
	}
	
	private void RequestInterstitial ()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-9985775175828398/2870439460";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-9985775175828398/7300639060";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create an interstitial.
		interstitial = new InterstitialAd (adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		// Load an interstitial ad.
		interstitial.LoadAd (createAdRequest ());
	}
	
	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest ()
	{
		return new AdRequest.Builder ()
			.AddTestDevice (AdRequest.TestDeviceSimulator)
				.AddTestDevice ("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword ("game")
				.SetGender (Gender.Male)
				.SetBirthday (new DateTime (1985, 1, 1))
				.TagForChildDirectedTreatment (false)
				.AddExtra ("color_bg", "9B30FF")
				.Build ();
		
	}


	void OnEnable ()
	{
		// Listen to all impression-related events
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += didDismissInterstitial;
		Chartboost.didCloseInterstitial += didCloseInterstitial;
		Chartboost.didClickInterstitial += didClickInterstitial;
		Chartboost.didCacheInterstitial += didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial += shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial += didDisplayInterstitial;
		Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps += didDismissMoreApps;
		Chartboost.didCloseMoreApps += didCloseMoreApps;
		Chartboost.didClickMoreApps += didClickMoreApps;
		Chartboost.didCacheMoreApps += didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps += shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps += didDisplayMoreApps;
		Chartboost.didFailToRecordClick += didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo += didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;
		Chartboost.didCacheInPlay += didCacheInPlay;
		Chartboost.didFailToLoadInPlay += didFailToLoadInPlay;
		Chartboost.didPauseClickForConfirmation += didPauseClickForConfirmation;
		Chartboost.willDisplayVideo += willDisplayVideo;
#if UNITY_IPHONE
		Chartboost.didCompleteAppStoreSheetFlow += didCompleteAppStoreSheetFlow;
#endif
	}
	void OnDisable ()
	{
		// Remove event handlers
		Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= didDismissInterstitial;
		Chartboost.didCloseInterstitial -= didCloseInterstitial;
		Chartboost.didClickInterstitial -= didClickInterstitial;
		Chartboost.didCacheInterstitial -= didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial -= shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial -= didDisplayInterstitial;
		Chartboost.didFailToLoadMoreApps -= didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps -= didDismissMoreApps;
		Chartboost.didCloseMoreApps -= didCloseMoreApps;
		Chartboost.didClickMoreApps -= didClickMoreApps;
		Chartboost.didCacheMoreApps -= didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps -= shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps -= didDisplayMoreApps;
		Chartboost.didFailToRecordClick -= didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;
		Chartboost.didCacheInPlay -= didCacheInPlay;
		Chartboost.didFailToLoadInPlay -= didFailToLoadInPlay;
		Chartboost.didPauseClickForConfirmation -= didPauseClickForConfirmation;
		Chartboost.willDisplayVideo -= willDisplayVideo;
#if UNITY_IPHONE
		Chartboost.didCompleteAppStoreSheetFlow -= didCompleteAppStoreSheetFlow;
#endif
	}

	public static void cacheCBAtgameOver ()
	{
		cacheInterstitial (CBLocation.Default);
	}
	public static void showCBAtgameOver ()
	{
		showInterstitial (CBLocation.Default);
	}


	public static void cacheCBAtStart ()
	{
		cacheInterstitial (CBLocation.Default);
	}
	public static void showCBAtStart ()
	{
		showInterstitial (CBLocation.Default);
	}




	public void ShowMoreApp ()
	{
		Chartboost.showMoreApps (CBLocation.Default);
	}



	public static void cacheMoreApps (CBLocation location)
	{
		CBExternal.cacheMoreApps (location);
	}

	/// 
	/// Determine if a locally cached "more applications" exists for the given CBLocation.
	/// A return value of true here indicates that the corresponding
	/// showMoreApps:(CBLocation)location method will present without making
	/// additional Chartboost API server requests to fetch data to present.
	/// 
	///true if the "more applications" is cached, false if not.
	///The location for the Chartboost impression type.
	public static bool hasMoreApps (CBLocation location)
	{
		return CBExternal.hasMoreApps (location);
	}

	public static void showMoreApps (CBLocation location)
	{
		CBExternal.showMoreApps (location);
	}





	public static void cacheInterstitial (CBLocation location)
	{
		CBExternal.cacheInterstitial (location);
	}
	public static bool hasInterstitial (CBLocation location)
	{
		return CBExternal.hasInterstitial (location);
	}



	public static void showInterstitial (CBLocation location)
	{
		CBExternal.showInterstitial (location);
	}



	void didFailToLoadInterstitial (CBLocation location, CBImpressionError error)
	{
		Debug.Log (string.Format ("didFailToLoadInterstitial: {0} at location {1}", error, location));
	}

	void didDismissInterstitial (CBLocation location)
	{
		Debug.Log ("didDismissInterstitial: " + location);
	}

	void didCloseInterstitial (CBLocation location)
	{
		Debug.Log ("didCloseInterstitial: " + location);
	}

	void didClickInterstitial (CBLocation location)
	{
		Debug.Log ("didClickInterstitial: " + location);
	}

	void didCacheInterstitial (CBLocation location)
	{
		Debug.Log ("didCacheInterstitial: " + location);
		showInterstitial (location);
	}

	bool shouldDisplayInterstitial (CBLocation location)
	{
		Debug.Log ("shouldDisplayInterstitial: " + location);
		return true;
	}

	void didDisplayInterstitial (CBLocation location)
	{
		Debug.Log ("didDisplayInterstitial: " + location);
	}

	void didFailToLoadMoreApps (CBLocation location, CBImpressionError error)
	{
		Debug.Log (string.Format ("didFailToLoadMoreApps: {0} at location: {1}", error, location));
	}

	void didDismissMoreApps (CBLocation location)
	{
		Debug.Log (string.Format ("didDismissMoreApps at location: {0}", location));
	}

	void didCloseMoreApps (CBLocation location)
	{
		Debug.Log (string.Format ("didCloseMoreApps at location: {0}", location));
	}

	void didClickMoreApps (CBLocation location)
	{
		Debug.Log (string.Format ("didClickMoreApps at location: {0}", location));
	}

	void didCacheMoreApps (CBLocation location)
	{
		Debug.Log (string.Format ("didCacheMoreApps at location: {0}", location));
	}

	bool shouldDisplayMoreApps (CBLocation location)
	{
		Debug.Log (string.Format ("shouldDisplayMoreApps at location: {0}", location));
		return true;
	}

	void didDisplayMoreApps (CBLocation location)
	{
		Debug.Log ("didDisplayMoreApps: " + location);
	}

	void didFailToRecordClick (CBLocation location, CBImpressionError error)
	{
		Debug.Log (string.Format ("didFailToRecordClick: {0} at location: {1}", error, location));
	}

	void didFailToLoadRewardedVideo (CBLocation location, CBImpressionError error)
	{
		Debug.Log (string.Format ("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
	}

	void didDismissRewardedVideo (CBLocation location)
	{
		Debug.Log ("didDismissRewardedVideo: " + location);
	}

	void didCloseRewardedVideo (CBLocation location)
	{
		Debug.Log ("didCloseRewardedVideo: " + location);
	}

	void didClickRewardedVideo (CBLocation location)
	{
		Debug.Log ("didClickRewardedVideo: " + location);
	}

	void didCacheRewardedVideo (CBLocation location)
	{
		Debug.Log ("didCacheRewardedVideo: " + location);
		Chartboost.showRewardedVideo (CBLocation.Default);
	}

	bool shouldDisplayRewardedVideo (CBLocation location)
	{
		Debug.Log ("shouldDisplayRewardedVideo: " + location);
		return true;
	}

	void didCompleteRewardedVideo (CBLocation location, int reward)
	{
		Debug.Log (string.Format ("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
	}

	void didDisplayRewardedVideo (CBLocation location)
	{
		Debug.Log ("didDisplayRewardedVideo: " + location);
	}

	void didCacheInPlay (CBLocation location)
	{
		Debug.Log ("didCacheInPlay called: " + location);
	}

	void didFailToLoadInPlay (CBLocation location, CBImpressionError error)
	{
		Debug.Log (string.Format ("didFailToLoadInPlay: {0} at location: {1}", error, location));
	}

	void didPauseClickForConfirmation ()
	{
		Debug.Log ("didPauseClickForConfirmation called");
	}

	void willDisplayVideo (CBLocation location)
	{
		Debug.Log ("willDisplayVideo: " + location);
	}

#if UNITY_IPHONE
	void didCompleteAppStoreSheetFlow() {
		Debug.Log("didCompleteAppStoreSheetFlow");
	}
#endif





		

		





	public void ShowAdmobInterstitial ()
	{
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		} else {
			print ("Interstitial is not ready yet.");
		}
	}

    #region Banner callback handlers

	public void HandleAdLoaded (object sender, EventArgs args)
	{
		print ("HandleAdLoaded event received.");
	}

	public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		print ("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened (object sender, EventArgs args)
	{
		print ("HandleAdOpened event received");
	}

	void HandleAdClosing (object sender, EventArgs args)
	{
		print ("HandleAdClosing event received");
	}

	public void HandleAdClosed (object sender, EventArgs args)
	{
		print ("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication (object sender, EventArgs args)
	{
		print ("HandleAdLeftApplication event received");
	}

    #endregion

    #region Interstitial callback handlers

	public void HandleInterstitialLoaded (object sender, EventArgs args)
	{
		print ("HandleInterstitialLoaded event received.");
		ShowAdmobInterstitial ();
	}

	public void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args)
	{
		print ("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened (object sender, EventArgs args)
	{
		print ("HandleInterstitialOpened event received");
	}

	void HandleInterstitialClosing (object sender, EventArgs args)
	{
		print ("HandleInterstitialClosing event received");
	}

	public void HandleInterstitialClosed (object sender, EventArgs args)
	{
		print ("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication (object sender, EventArgs args)
	{
		print ("HandleInterstitialLeftApplication event received");
	}

    #endregion
}
