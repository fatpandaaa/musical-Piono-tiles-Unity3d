using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadePlusGameManager : MonoBehaviour
{

	public enum ArcadeMode
	{
		Bomb,
		Lightning,
		Duel }
	;
	public GameManager gameManager;
	public ArcadeMode currentMode = ArcadeMode.Bomb;
	public GameObject tile;
	public bool isGameStarted = false;
	public bool isGameOver = false;
	public bool isLevelFailed = false;
	//public int totalTap = 25;
	public int tapCounter = 0;
	public int count = 0;
	public float timeCounter = 0f;
	public int speed = 8;
	public int BesttimeCounter;


	public Transform[] row1 = new Transform[4];
	public Transform[] row2 = new Transform[4];
	public Transform[] row3 = new Transform[4];
	public Transform[] row4 = new Transform[4];
	public Transform[] row5 = new Transform[4];
	public Transform[] row6 = new Transform[4];

	public AudioClip bombSound;
	public AudioClip wrongSound;
	private Text timerText;
	private Text scoreText;
	private Text HighScoreText;

	//private Slider timeSlider;
	private Animator gameOverUIAnim;
	private Animator lightning;
	void Start ()
	{
		gameManager = GameObject.FindObjectOfType<GameManager> ();
		lightning = GameObject.Find ("Lightning").GetComponent<Animator> ();
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		HighScoreText = GameObject.Find ("HighScore").GetComponent<Text> ();

		//timeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
		gameOverUIAnim = GameObject.Find ("GameOverUI").GetComponent<Animator> ();
		if (gameManager.subLevelIndex == 1) {
			currentMode = ArcadeMode.Bomb;
		} else if (gameManager.subLevelIndex == 2) {
			currentMode = ArcadeMode.Lightning;
		} else if (gameManager.subLevelIndex == 3) {
			currentMode = ArcadeMode.Duel;
		}
		if (gameManager.isSoundOn)
			GetComponent<AudioSource> ().volume = 1f;
		else
			GetComponent<AudioSource> ().volume = 0f;
		/*
        if (currentMode == ArcadeMode.Bomb)
        {
            //totalTap = 25;
            //timeCounter = 15f;
            speed = 8;
        }
        else if (currentMode == ArcadeMode.Lightning)
        {
            //totalTap = 50;
            //timeCounter = 30f;
            speed = 10;
        }
        else if (currentMode == ArcadeMode.Duel)
        {
            //totalTap = 50;
            //timeCounter = 50f;
            speed = 8;
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        */
		if (currentMode == ArcadeMode.Lightning) {
			InvokeRepeating ("Lightning", 4f, 4f);
		}
		//timeSlider.minValue = 0f;
		//timeSlider.maxValue = timeCounter;
		GenerateFullGrid ();

	}
	public void SetHighScore ()
	{
		BesttimeCounter = PlayerPrefs.GetInt ("HighScoreArcadePlus");
		
		if (tapCounter > BesttimeCounter) {
			print (tapCounter);
			PlayerPrefs.SetInt ("HighScoreArcadePlus", tapCounter);
			PlayerPrefs.Save ();
		}
		HighScoreText.text = "Best " + PlayerPrefs.GetInt ("HighScoreArcadePlus").ToString ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}

		if (!isGameOver && isGameStarted) {
			timeCounter += Time.deltaTime;
			//timeSlider.value = timeCounter;

			if (Application.loadedLevelName == "Rush") {
				float avg = tapCounter / timeCounter;
				timerText.text = avg.ToString ("0.000") + " / sec";
			} else {
				timerText.text = tapCounter.ToString ();
			}
		}
		InputHandler ();
		//Debug.Log(callCount);
		/*
    else if (!isGameOver && timeCounter <= 0f)
    {
        isGameOver = true;
        GameOver();
    }
         * */
	}
	public void LoadMenu ()
	{
		Debug.Log ("Menu Called");
		Application.LoadLevel (0);
	}
	void GenerateFullGrid ()
	{
		for (int i = 0; i < 4; i++) {
			GenerateRow (i, 0f);
		}
	}

	public void GenerateRow (int i, float y)
	{

		int rnd = Random.Range (0, 4);

		if (i == 0) {
			for (int j = 0; j < 4; j++) {
				GameObject tmp = Instantiate (tile, row1 [j].position, Quaternion.identity) as GameObject;
				tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
				tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
			}
		} else if (i == 1) {

			for (int j = 0; j < 4; j++) {
				GameObject tmp = Instantiate (tile, row2 [j].position, Quaternion.identity) as GameObject;
				tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
				tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
			}
		} else if (i == 2) {
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					GameObject tmp = Instantiate (tile, row3 [rnd].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Black;
					tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					tmp.GetComponent<SpriteRenderer> ().sprite = tmp.GetComponent<ArcadePlusPianoManager> ().blackStartSprite;
					tmp.tag = "Start";
				} else {
					GameObject tmp = Instantiate (tile, row3 [j].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
					tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
				}
			}

		} else if (i == 3) {
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					GameObject tmp = Instantiate (tile, row4 [rnd].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Black;
					tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
				} else {
					GameObject tmp = Instantiate (tile, row4 [j].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
					tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
				}
			}
		} else if (i == 4) {
			int rnd2 = Random.Range (0, 4);
			while (rnd2 == rnd)
				rnd2 = Random.Range (0, 4);

			for (int j = 0; j < 4; j++) {
				if (currentMode == ArcadeMode.Lightning) {
					if (j == rnd) {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [rnd].position.x, row6 [rnd].position.y - y), Quaternion.identity) as GameObject;
						tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Black;
						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					} else {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [j].position.x, row6 [j].position.y - y), Quaternion.identity) as GameObject;
						tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					}
				} else if (currentMode == ArcadeMode.Bomb) {
					if (j == rnd) {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [rnd].position.x, row6 [rnd].position.y - y), Quaternion.identity) as GameObject;

						int rnd3 = Random.Range (0, 5);
						if (rnd3 < 4) {
							tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Black;
						} else {
							tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Bomb;
						}

						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					} else {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [j].position.x, row6 [j].position.y - y), Quaternion.identity) as GameObject;
						tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					}
				} else if (currentMode == ArcadeMode.Duel && count == 0) {
					//Debug.Log(j + "---" + rnd + "----" + rnd2);
					if (j == rnd) {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [rnd].position.x, row6 [rnd].position.y - y), Quaternion.identity) as GameObject;
						tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Black;
						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					} else if (j == rnd2) {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [rnd2].position.x, row6 [rnd2].position.y - y), Quaternion.identity) as GameObject;
						tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.Black;
						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					} else {
						GameObject tmp = Instantiate (tile, new Vector2 (row6 [j].position.x, row6 [j].position.y - y), Quaternion.identity) as GameObject;
						tmp.GetComponent<ArcadePlusPianoManager> ().currentPianoState = ArcadePlusPianoManager.PianoState.White;
						tmp.GetComponent<ArcadePlusPianoManager> ().UpdateState ();
					}
				}
                

			}
			count++;
			if (count == 2)
				count = 0;
		}
		//Debug.Log(rnd);
	}


	public void CheckBlackTile ()
	{
		for (int i = 0; i < 4; i++) {
			if (row5 [i].GetComponent<ArcadePlusPianoManager> ().currentPianoState == ArcadePlusPianoManager.PianoState.Black) {
				//Debug.Log("Is Dead!!!!!!!!");
				break;
			}
		}
	}

	void InputHandler ()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Debug.Log ("Called");
			Vector3 touchWorldPoint = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);

			touchWorldPoint.z = 0f;

			RaycastHit2D hit = Physics2D.Raycast (touchWorldPoint, -Vector2.up);

			if (hit.collider.tag == "PianoButton" && isGameStarted) {
				ArcadePlusPianoManager tmpPiano = hit.collider.gameObject.GetComponent<ArcadePlusPianoManager> ();

				if (tmpPiano.currentPianoState == ArcadePlusPianoManager.PianoState.Black) {
                    gameManager.ShowEffect(hit.transform.position);
					gameManager.PlayRandomNote ();
					tapCounter++;
					tmpPiano.currentPianoState = ArcadePlusPianoManager.PianoState.White;
					tmpPiano.UpdateState ();
					//RowSwapping();
					//GenerateRow(3);
					//CheckBlackTile();

				} else if (tmpPiano.currentPianoState == ArcadePlusPianoManager.PianoState.White || tmpPiano.currentPianoState == ArcadePlusPianoManager.PianoState.Bomb) {
					Debug.Log (tmpPiano.gameObject.transform.position);
					if (tmpPiano.currentPianoState == ArcadePlusPianoManager.PianoState.White) {
						GetComponent<AudioSource> ().clip = wrongSound;
					} else if (tmpPiano.currentPianoState == ArcadePlusPianoManager.PianoState.Bomb) {
						GetComponent<AudioSource> ().clip = bombSound;
					}
					GetComponent<AudioSource> ().Play ();
					tmpPiano.gameObject.GetComponent<Animator> ().Play ("TileBlink");
					isGameOver = true;
					isLevelFailed = false;
					//Application.LoadLevel(Application.loadedLevel);
				}

				if (isGameOver) {
					isGameOver = true;
					GameOver ();
				}
			} else if (hit.collider.tag == "Start") {
                gameManager.ShowEffect(hit.transform.position);
				gameManager.PlayRandomNote ();
				ArcadePlusPianoManager tmpPiano = hit.collider.gameObject.GetComponent<ArcadePlusPianoManager> ();
				isGameStarted = true;
				tapCounter++;
				tmpPiano.currentPianoState = ArcadePlusPianoManager.PianoState.White;
				tmpPiano.UpdateState ();
			}
		}
	}

	public void GameOver ()
	{
		StartCoroutine ("DelayGameOver");
	}
	IEnumerator DelayGameOver ()
	{
		yield return new WaitForSeconds (0.5f);
		scoreText.text = tapCounter.ToString ();
		SetHighScore ();

		if (currentMode == ArcadeMode.Lightning) {
			CancelInvoke ("Lightning");
		}

		gameOverUIAnim.SetBool ("open", true);
		Admanager.GameState = 1;
	}
	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Lightning ()
	{
		lightning.Play ("Lightning");
	}
}
