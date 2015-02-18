using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{

	public GameManager gameManager;

	private bool isPopUp = false;

	private Animator classicAnim;
	private Animator arcadeAnim;
	private Animator zenAnim;
	private Animator rushAnim;
	private Animator relayAnim;
	private Animator arcadePlusAnim;
	private Animator settingsAnim;
	void Start ()
	{
		Admanager.GameState = 1;
		if (!GameObject.FindObjectOfType<GameManager> ()) {
			gameManager = (Instantiate (gameManager, Vector2.zero, Quaternion.identity) as GameObject).GetComponent<GameManager> ();
		} else {
			gameManager = GameObject.FindObjectOfType<GameManager> ();
			if (gameManager.isSoundOn) {
				GetComponent<AudioSource> ().volume = 1f;
				GameObject.Find ("SoundText").GetComponent<Text> ().text = "Sound On";
			} else {
				GetComponent<AudioSource> ().volume = 0f;
				GameObject.Find ("SoundText").GetComponent<Text> ().text = "Sound Off";
			}
                
		}

		classicAnim = GameObject.Find ("ClassicSubLevel").GetComponent<Animator> ();
		arcadeAnim = GameObject.Find ("ArcadeSubLevel").GetComponent<Animator> ();
		zenAnim = GameObject.Find ("ZenSubLevel").GetComponent<Animator> ();
		rushAnim = GameObject.Find ("RushSubLevel").GetComponent<Animator> ();
		relayAnim = GameObject.Find ("RelaySubLevel").GetComponent<Animator> ();
		arcadePlusAnim = GameObject.Find ("ArcadePlusSubLevel").GetComponent<Animator> ();
		settingsAnim = GameObject.Find ("SettingsSub").GetComponent<Animator> ();

		classicAnim.SetBool ("open", false);
		arcadeAnim.SetBool ("open", false);
		zenAnim.SetBool ("open", false);
		rushAnim.SetBool ("open", false);
		relayAnim.SetBool ("open", false);
		arcadePlusAnim.SetBool ("open", false);
		settingsAnim.SetBool ("open", false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Back ();
                
		}
	}

	public void Back ()
	{
		if (isPopUp) {
			classicAnim.SetBool ("open", false);
			arcadeAnim.SetBool ("open", false);
			zenAnim.SetBool ("open", false);
			rushAnim.SetBool ("open", false);
			relayAnim.SetBool ("open", false);
			arcadePlusAnim.SetBool ("open", false);
			settingsAnim.SetBool ("open", false);

			isPopUp = false;
		} else {
			Application.Quit ();
		}
	}

	public void Shuffle ()
	{
		int rnd = Random.Range (0, 6);
		int rnd2 = Random.Range (0, 3);

		gameManager.subLevelIndex = rnd2 + 1;
		Application.LoadLevel (rnd + 1);
	}

	public void Classic ()
	{
		isPopUp = true;
		classicAnim.SetBool ("open", true);
	}

	public void Arcade ()
	{
		isPopUp = true;
		arcadeAnim.SetBool ("open", true);
	}

	public void Zen ()
	{
		isPopUp = true;
		zenAnim.SetBool ("open", true);
	}

	public void Rush ()
	{
		isPopUp = true;
		rushAnim.SetBool ("open", true);
	}

	public void Relay ()
	{
		isPopUp = true;
		relayAnim.SetBool ("open", true);
	}

	public void ArcadePlus ()
	{
		isPopUp = true;
		arcadePlusAnim.SetBool ("open", true);
	}

	public void Settings ()
	{
		isPopUp = true;
		settingsAnim.SetBool ("open", true);
	}

	public void Classic1 ()
	{
		gameManager.subLevelIndex = 1;
		Application.LoadLevel (1);
	}
	public void Classic2 ()
	{
		gameManager.subLevelIndex = 2;
		Application.LoadLevel (1);
	}
	public void Classic3 ()
	{
		gameManager.subLevelIndex = 3;
		Application.LoadLevel (1);
	}
	public void Arcade1 ()
	{
		gameManager.subLevelIndex = 1;
		Application.LoadLevel (2);
	}
	public void Arcade2 ()
	{
		gameManager.subLevelIndex = 2;
		Application.LoadLevel (2);
	}
	public void Arcade3 ()
	{
		gameManager.subLevelIndex = 3;
		Application.LoadLevel (2);
	}

	public void Zen1 ()
	{
		gameManager.subLevelIndex = 1;
		Application.LoadLevel (3);
	}
	public void Zen2 ()
	{
		gameManager.subLevelIndex = 2;
		Application.LoadLevel (3);
	}
	public void Zen3 ()
	{
		gameManager.subLevelIndex = 3;
		Application.LoadLevel (3);
	}

	public void Rush1 ()
	{
		gameManager.subLevelIndex = 1;
		Application.LoadLevel (4);
	}
	public void Rush2 ()
	{
		gameManager.subLevelIndex = 3;
		Application.LoadLevel (4);
	}
	public void Rush3 ()
	{
		gameManager.subLevelIndex = 2;
		Application.LoadLevel (4);
	}

	public void Relay1 ()
	{
		gameManager.subLevelIndex = 1;
		Application.LoadLevel (5);
	}
	public void Relay2 ()
	{
		gameManager.subLevelIndex = 2;
		Application.LoadLevel (5);
	}
	public void Relay3 ()
	{
		gameManager.subLevelIndex = 3;
		Application.LoadLevel (5);
	}

	public void ArcadePlus1 ()
	{
		gameManager.subLevelIndex = 1;
		Application.LoadLevel (6);
	}
	public void ArcadePlus2 ()
	{
		gameManager.subLevelIndex = 2;
		Application.LoadLevel (6);
	}
	public void ArcadePlus3 ()
	{
		gameManager.subLevelIndex = 3;
		Application.LoadLevel (6);
	}

	public void RemoveAds ()
	{ 
        
	}

	public void Restore ()
	{ 
        
	}

}
