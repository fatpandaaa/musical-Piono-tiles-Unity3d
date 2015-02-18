using UnityEngine;
using OnePF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InPurchaseManager : MonoBehaviour
{
	const string SKU = "sku";

	const string SKU_remove_Ads = "com.kaj.blacktile.removeads";



	string _label = "";
	bool _isInitialized = false;
	Inventory _inventory = null;


	public enum ModeNum
	{
		KidNumber = 1,
		midNumber = 11,
		Starternumber = 21,
		SmartNumber = 31,
		SSmartNumber = 41
	}
	public  ModeNum Modeselected;

	private void OnEnable ()
	{
		// Listen to all events for illustration purposes
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}
	private void OnDisable ()
	{
		// Remove all event handlers
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}


	private void Start ()
	{

		OpenIAB.mapSku (SKU_remove_Ads, OpenIAB_Android.STORE_GOOGLE, "com.kaj.blacktile.removeads");


		// IOS		
		OpenIAB.mapSku (SKU_remove_Ads, OpenIAB_iOS.STORE, "com.kaj.blacktile.removeads");		
				


		// Map skus for different stores       
		OpenIAB.mapSku (SKU, OpenIAB_Android.STORE_GOOGLE, "sku");
		OpenIAB.mapSku (SKU, OpenIAB_Android.STORE_AMAZON, "sku");
		OpenIAB.mapSku (SKU, OpenIAB_iOS.STORE, "sku");
		OpenIAB.mapSku (SKU, OpenIAB_WP8.STORE, "ammo");


		InitializeOpenInAppBilling ();
		ButtonDisable ();
	}
	
	private void InitializeOpenInAppBilling ()
	{
		var publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgdg5KIUp4e7TAWt7j+PQY8rfhmryL3iyOr7/ybeWXQPZHMjdZ4Fxw8IsGr6oD2qlOTy8u9ZaTB1HIf66iVbG8SP4c54Y78cxI4MOrsZlWEsPt0TvNzsV6uLmFLHGw5yosqIs1kXsYpTRbONWFTxg/b9wzSsZRpCNPFCwraUGUEY/WgQemPQPBUauJ+mnJxtygYcqlLwyet42SI3OjeylHkEu12y/pYhzaQafiu6e7ydzZPvam3MDYsnxn2SkXhLiTJYwWKPIBM4xVdc9RVd3did42lR2cKsRTq1U2PgdNdU1oxhJAOAxBaK8/s+gDGx6qZN9lqcsvDhcEfBEhkOluQIDAQAB";
		
		var options = new Options ();
		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, publicKey} };
		options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
		
		// Transmit options and start the service
		OpenIAB.init (options);

	}


	void ButtonDisable ()
	{
		GameObject.Find ("RemoveAd").GetComponent<Button> ().interactable = false;
		GameObject.Find ("RestorePurchase").GetComponent<Button> ().interactable = false;
			
	}
	void ButtonEnable ()
	{
		if (PlayerPrefs.GetInt ("SKU_remove_Ads") == 1) {
			GameObject.Find ("RemoveAd").GetComponent<Button> ().interactable = false;
			GameObject.Find ("RestorePurchase").GetComponent<Button> ().interactable = false;
		} else {
			GameObject.Find ("RemoveAd").GetComponent<Button> ().interactable = true;
			GameObject.Find ("RestorePurchase").GetComponent<Button> ().interactable = true;
		}

/*

				#if UNITY_EDITOR
				GameObject.Find ("Restore").GetComponent<Button> ().interactable = true;
				#elif UNITY_IPHONE
		GameObject.Find ("Restore").GetComponent<Button> ().interactable = true;
				#else
		GameObject.Find ("Restore").GetComponent<Button> ().interactable = false;
				#endif
            */

	}


		

	public void MakePurchaseRemoveAds ()
	{
		Debug.Log ("Purchase is called");
		if (_isInitialized) {
			Debug.Log ("is called");
			MakeInventoryQuery ();
			OpenIAB.purchaseProduct (SKU_remove_Ads);
		} else {
			Debug.Log ("is not called");
			InitializeOpenInAppBilling ();
		}
	}

	public  void MakePurchaseRestore ()
	{
		Debug.Log ("restore is called");
		if (_isInitialized) {
			MakeInventoryQuery ();
			OpenIAB.restoreTransactions ();
		} else {
			InitializeOpenInAppBilling ();
		}
	}

	public void BackToMainMenu ()
	{
		OpenIAB.unbindService ();
		Application.LoadLevel ("MainMenu");
	}




	void MakeInventoryQuery ()
	{
		OpenIAB.queryInventory (new string[] {
						
						SKU_remove_Ads
						
				});
	}


	private void billingSupportedEvent ()
	{
		_isInitialized = true;
		ButtonEnable ();
		Debug.Log ("billingSupportedEvent " + _isInitialized);
		//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "Billing Supported";
	}
	private void billingNotSupportedEvent (string error)
	{
		Debug.Log ("billingNotSupportedEvent: " + error);
		//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "billingNotSupportedEvent: " + error;
	}
	private void queryInventorySucceededEvent (Inventory inventory)
	{
		Debug.Log ("queryInventorySucceededEvent: " + inventory);
		//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "queryInventorySucceededEvent: " + inventory;
		if (inventory != null) {
			_label = inventory.ToString ();
			_inventory = inventory;
		}
	}
	private void queryInventoryFailedEvent (string error)
	{
		Debug.Log ("queryInventoryFailedEvent: " + error);
		//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "queryInventoryFailedEvent: " + error;
		_label = error;
	}
	private void purchaseSucceededEvent (Purchase purchase)
	{
				
		if (purchase.Sku == SKU_remove_Ads) {
						
			//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "Remove All Ads";
			PlayerPrefs.SetInt ("SKU_remove_Ads", 1);
		}

			
		Debug.Log ("purchaseSucceededEvent: " + purchase);
		_label = "PURCHASED:" + purchase.ToString ();
	}
	private void purchaseFailedEvent (int errorCode, string errorMessage)
	{
		Debug.Log ("purchaseFailedEvent: " + errorMessage);
		_label = "Purchase Failed: " + errorMessage;
		///GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "Purchase Failed: " + errorMessage;
		StartCoroutine ("ClearPurchaseLog");
	}
	private void consumePurchaseSucceededEvent (Purchase purchase)
	{
		Debug.Log ("consumePurchaseSucceededEvent: " + purchase);
		//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = "Consume Purchase Failed";
		_label = "CONSUMED: " + purchase.ToString ();
	}
	private void consumePurchaseFailedEvent (string error)
	{
		Debug.Log ("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}

	IEnumerator ClearPurchaseLog ()
	{
		yield return new WaitForSeconds (8f);
		//GameObject.Find ("purchaseLog").GetComponent<Text> ().text = null;
	}
}