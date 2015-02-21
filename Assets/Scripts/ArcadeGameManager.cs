using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadeGameManager : MonoBehaviour
{

	// Use this for initialization
	public enum ArcadeMode
	{
		Normal,
		Fast,
		Reverse }
	;
	public GameManager gameManager;
	public ArcadeMode currentMode = ArcadeMode.Normal;
	public GameObject tile;
	public bool isGameStarted = false;
	public bool isGameOver = false;
	public bool isLevelFailed = false;
	//public int totalTap = 25;
	public int tapCounter = 0;
	public float timeCounter = 0f;
	public int speed = 5;

	public Transform[] row1 = new Transform[4];
	public Transform[] row2 = new Transform[4];
	public Transform[] row3 = new Transform[4];
	public Transform[] row4 = new Transform[4];
	public Transform[] row5 = new Transform[4];
	public Transform[] row6 = new Transform[4];

	private Text timerText;
	private Text scoreText;
	private Text HighScoreText;
	public int BesttimeCounter;


	//private Slider timeSlider;
	private Animator gameOverUIAnim;
	void Start ()
	{
		gameManager = GameObject.FindObjectOfType<GameManager> ();
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		HighScoreText = GameObject.Find ("HighScore").GetComponent<Text> ();

		//timeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
		gameOverUIAnim = GameObject.Find ("GameOverUI").GetComponent<Animator> ();

		if (gameManager.subLevelIndex == 1) {
			currentMode = ArcadeMode.Normal;
		} else if (gameManager.subLevelIndex == 2) {
			currentMode = ArcadeMode.Fast;
		} else if (gameManager.subLevelIndex == 3) {
			currentMode = ArcadeMode.Reverse;
		}
		if (gameManager.isSoundOn)
			GetComponent<AudioSource> ().volume = 1f;
		else
			GetComponent<AudioSource> ().volume = 0f;
		if (currentMode == ArcadeMode.Normal) {
			//totalTap = 25;
			//timeCounter = 15f;
			speed = 8;
		} else if (currentMode == ArcadeMode.Fast) {
			//totalTap = 50;
			//timeCounter = 30f;
			speed = 10;
		} else if (currentMode == ArcadeMode.Reverse) {
			//totalTap = 50;
			//timeCounter = 50f;
			speed = 8;
			Camera.main.transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));
		}
        gameManager.ResetNoteIndex();
		//timeSlider.minValue = 0f;
		//timeSlider.maxValue = timeCounter;
		GenerateFullGrid ();

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
				Debug.Log (tapCounter);
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
				tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.White;
				tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
			}
		} else if (i == 1) {

			for (int j = 0; j < 4; j++) {
				GameObject tmp = Instantiate (tile, row2 [j].position, Quaternion.identity) as GameObject;
				tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.White;
				tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
			}
		} else if (i == 2) {
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					GameObject tmp = Instantiate (tile, row3 [rnd].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.Black;
					tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
					tmp.GetComponent<SpriteRenderer> ().sprite = tmp.GetComponent<ArcadePianoButtonManager> ().blackStartSprite;
					tmp.tag = "Start";
					if (currentMode == ArcadeMode.Reverse) {
						tmp.transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 180f));
					}
				} else {
					GameObject tmp = Instantiate (tile, row3 [j].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.White;
					tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
				}
			}
            
		} else if (i == 3) {
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					GameObject tmp = Instantiate (tile, row4 [rnd].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.Black;
					tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
				} else {
					GameObject tmp = Instantiate (tile, row4 [j].position, Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.White;
					tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
				}
			}
		} else if (i == 4) {
			//Debug.Log("koybar dhukse");
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					GameObject tmp = Instantiate (tile, new Vector2 (row6 [rnd].position.x, row6 [rnd].position.y - y), Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.Black;
					tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
				} else {
					GameObject tmp = Instantiate (tile, new Vector2 (row6 [j].position.x, row6 [j].position.y - y), Quaternion.identity) as GameObject;
					tmp.GetComponent<ArcadePianoButtonManager> ().currentPianoState = ArcadePianoButtonManager.PianoState.White;
					tmp.GetComponent<ArcadePianoButtonManager> ().UpdateState ();
				}
                
			}
		}
		//Debug.Log(rnd);
	}

	void RowSwapping ()
	{
		for (int i = 0; i < 4; i++) {
			if (i == 0) {
				for (int j = 0; j < 4; j++) {
					row5 [j].GetComponent<PianoButtonManager> ().currentPianoState = row1 [j].GetComponent<PianoButtonManager> ().currentPianoState;
					row5 [j].GetComponent<PianoButtonManager> ().UpdateState ();
				}
			} else if (i == 1) {
				for (int j = 0; j < 4; j++) {
					row1 [j].GetComponent<PianoButtonManager> ().currentPianoState = row2 [j].GetComponent<PianoButtonManager> ().currentPianoState;
					row1 [j].GetComponent<PianoButtonManager> ().UpdateState ();
				}
			} else if (i == 2) {
				for (int j = 0; j < 4; j++) {
					row2 [j].GetComponent<PianoButtonManager> ().currentPianoState = row3 [j].GetComponent<PianoButtonManager> ().currentPianoState;
					row2 [j].GetComponent<PianoButtonManager> ().UpdateState ();
				}
			} else if (i == 3) {
				for (int j = 0; j < 4; j++) {
					row3 [j].GetComponent<PianoButtonManager> ().currentPianoState = row4 [j].GetComponent<PianoButtonManager> ().currentPianoState;
					row3 [j].GetComponent<PianoButtonManager> ().UpdateState ();
				}
			}
		}

		//Debug.Log(row5[0].GetComponent<PianoButtonManager>().currentPianoState);
	}

	public void CheckBlackTile ()
	{
		for (int i = 0; i < 4; i++) {
			if (row5 [i].GetComponent<ArcadePianoButtonManager> ().currentPianoState == ArcadePianoButtonManager.PianoState.Black) {
				//Debug.Log("Is Dead!!!!!!!!");
				break;
			}
		}
	}

	void InputHandler ()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			//Debug.Log("Called");
			Vector3 touchWorldPoint = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);

			touchWorldPoint.z = 0f;

			RaycastHit2D hit = Physics2D.Raycast (touchWorldPoint, -Vector2.up);

			if (hit.collider.tag == "PianoButton" && isGameStarted && !isGameOver) {
				ArcadePianoButtonManager tmpPiano = hit.collider.gameObject.GetComponent<ArcadePianoButtonManager> ();

				if (tmpPiano.currentPianoState == ArcadePianoButtonManager.PianoState.Black) {
					gameManager.PlayRandomNote ();
                    gameManager.ShowEffect(hit.transform.position);
                    tapCounter++;
					tmpPiano.currentPianoState = ArcadePianoButtonManager.PianoState.White;
					tmpPiano.UpdateState ();

					//RowSwapping();
					//GenerateRow(3);
					//CheckBlackTile();
                    
				} else if (tmpPiano.currentPianoState == ArcadePianoButtonManager.PianoState.White && isGameStarted && !isGameOver) {
					isGameOver = true;
					isLevelFailed = false;
					GetComponent<AudioSource> ().Play ();
					tmpPiano.gameObject.GetComponent<Animator> ().Play ("TileBlink");
                    
					//Application.LoadLevel(Application.loadedLevel);
				}

				if (isGameOver) {
					isGameOver = true;
					GameOver ();
				}
			} else if (hit.collider.tag == "Start") {
                gameManager.ShowEffect(hit.transform.position);
				gameManager.PlayRandomNote ();
				ArcadePianoButtonManager tmpPiano = hit.collider.gameObject.GetComponent<ArcadePianoButtonManager> ();
				isGameStarted = true;
				tapCounter++;
				tmpPiano.currentPianoState = ArcadePianoButtonManager.PianoState.White;
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
		if (Application.loadedLevelName == "Rush") {
			float avg = tapCounter / timeCounter;
			scoreText.text = avg.ToString ("0.000") + " / sec";

			float Bes = PlayerPrefs.GetFloat ("HighScorerush");
			if (avg > Bes) {
				print (tapCounter);
				PlayerPrefs.SetFloat ("HighScorerush", avg);
				PlayerPrefs.Save ();
			}
			HighScoreText.text = "Best " + PlayerPrefs.GetFloat ("HighScorerush").ToString ();


		} else {
			scoreText.text = tapCounter.ToString ();

			BesttimeCounter = PlayerPrefs.GetInt ("HighScoreArcad");
			
			if (tapCounter > BesttimeCounter) {
				print (tapCounter);
				PlayerPrefs.SetInt ("HighScoreArcad", tapCounter);
				PlayerPrefs.Save ();
			}
			HighScoreText.text = "Best " + PlayerPrefs.GetInt ("HighScoreArcad").ToString ();

		}

		gameOverUIAnim.SetBool ("open", true);
		Admanager.GameState = 1;
	}
	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
