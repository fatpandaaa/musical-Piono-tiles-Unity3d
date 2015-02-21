using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClassicGameManager : MonoBehaviour
{
	public enum ClassicMode
	{
		Mode25,
		Mode50,
		Pro}
	;

	public ClassicMode currentMode = ClassicMode.Mode25;
	public GameManager gameManager;
	public bool isGameStarted = false;
	public bool isGameOver = false;
	public bool isLevelFailed = false;
	public int totalTap = 25;
	public int tapCounter = 0;
	public float timeCounter = 0f;
	public float BesttimeCounter;

	public GameObject[] row1 = new GameObject[4];
	public GameObject[] row2 = new GameObject[4];
	public GameObject[] row3 = new GameObject[4];
	public GameObject[] row4 = new GameObject[4];
	public GameObject[] row5 = new GameObject[4];

	private Text timerText;
	private Text scoreText;
	private Text HighScoreText;
	private Animator gameOverUIAnim;
	void Start ()
	{
		gameManager = GameObject.FindObjectOfType<GameManager> ();
        GameObject.Find("LevelFailed").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		HighScoreText = GameObject.Find ("HighScore").GetComponent<Text> ();
		gameOverUIAnim = GameObject.Find ("GameOverUI").GetComponent<Animator> ();

		if (gameManager.subLevelIndex == 1) {
			currentMode = ClassicMode.Mode25;
		} else if (gameManager.subLevelIndex == 2) {
			currentMode = ClassicMode.Mode50;
		} else if (gameManager.subLevelIndex == 3) {
			currentMode = ClassicMode.Pro;
		}
		if (gameManager.isSoundOn)
			GetComponent<AudioSource> ().volume = 1f;
		else
			GetComponent<AudioSource> ().volume = 0f;
		if (currentMode == ClassicMode.Mode25) {
			totalTap = 25;
		} else if (currentMode == ClassicMode.Mode50) {
			totalTap = 50;
		} else if (currentMode == ClassicMode.Pro) {
			totalTap = 50;
		}
        gameManager.ResetNoteIndex();
		GenerateFullGrid ();
		scoreText.text = timeCounter.ToString ("0.00") + " sec";
	}

	public void SetHighScore ()
	{
	

		BesttimeCounter = PlayerPrefs.GetFloat ("HighScoreClassic");

		if (timeCounter > BesttimeCounter) {
			print ("save a dhukse" + PlayerPrefs.GetFloat ("HighScoreClassic") + timeCounter);
			PlayerPrefs.SetFloat ("HighScoreClassic", timeCounter);
			PlayerPrefs.Save ();
		}
		HighScoreText.text = "Best " + PlayerPrefs.GetFloat ("HighScoreClassic").ToString ("0.00") + " sec";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}
		if (!isGameOver && tapCounter < totalTap && isGameStarted) {
			timeCounter += Time.deltaTime;
			timerText.text = "<b>" + timeCounter.ToString ("0.00") + "</b> sec";
            
		}
		InputHandler ();

	}
	public void LoadMenu ()
	{
		Application.LoadLevel (0);
	}
	void GenerateFullGrid ()
	{
		for (int i = 0; i < 4; i++) {
			GenerateRow (i);
		}
	}

	void GenerateRow (int i)
	{
		int rnd = Random.Range (0, 4);

		if (i == 0) {

		} else if (i == 1) {

			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					row2 [rnd].GetComponent<PianoButtonManager> ().currentPianoState = PianoButtonManager.PianoState.Black;
					row2 [rnd].GetComponent<PianoButtonManager> ().UpdateState ();
					row2 [rnd].GetComponent<SpriteRenderer> ().sprite = row2 [j].GetComponent<PianoButtonManager> ().blackStartSprite;
					row2 [rnd].tag = "Start";
				} else {
					row2 [j].GetComponent<PianoButtonManager> ().currentPianoState = PianoButtonManager.PianoState.White;
					row2 [j].GetComponent<PianoButtonManager> ().UpdateState ();
				}
                
			}
		} else if (i == 2) {
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					row3 [rnd].GetComponent<PianoButtonManager> ().currentPianoState = PianoButtonManager.PianoState.Black;
				} else {
					row3 [j].GetComponent<PianoButtonManager> ().currentPianoState = PianoButtonManager.PianoState.White;
				}
				row3 [j].GetComponent<PianoButtonManager> ().UpdateState ();
			}
		} else if (i == 3) {
			for (int j = 0; j < 4; j++) {
				if (j == rnd) {
					row4 [rnd].GetComponent<PianoButtonManager> ().currentPianoState = PianoButtonManager.PianoState.Black;
				} else {
					row4 [j].GetComponent<PianoButtonManager> ().currentPianoState = PianoButtonManager.PianoState.White;
				}
				row4 [j].GetComponent<PianoButtonManager> ().UpdateState ();
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

	void CheckBlackTile ()
	{
		for (int i = 0; i < 4; i++) {
			if (row5 [i].GetComponent<PianoButtonManager> ().currentPianoState == PianoButtonManager.PianoState.Black) {
				isGameOver = true;
				isLevelFailed = true;
				GetComponent<AudioSource> ().Play ();
				//GameOver();
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

			if (hit.collider.tag == "PianoButton" && isGameStarted && !isGameOver) {
				PianoButtonManager tmpPiano = hit.collider.gameObject.GetComponent<PianoButtonManager> ();

				if (tmpPiano.currentPianoState == PianoButtonManager.PianoState.Black && !isGameOver) {
                    gameManager.ShowEffect(hit.transform.position);
					gameManager.PlayRandomNote ();
					tmpPiano.currentPianoState = PianoButtonManager.PianoState.White;
					tmpPiano.UpdateState ();
					RowSwapping ();
					GenerateRow (3);
					CheckBlackTile ();
					tapCounter++;
					if (isGameOver || tapCounter == totalTap) {
						isGameOver = true;
						GameOver ();
					}
				} else if (tmpPiano.currentPianoState == PianoButtonManager.PianoState.White) {
					isGameOver = true;
					isLevelFailed = true;
					GetComponent<AudioSource> ().Play ();
					tmpPiano.gameObject.GetComponent<Animator> ().Play ("TileBlink");
					//Application.LoadLevel(Application.loadedLevel);
					if (isGameOver || tapCounter == totalTap) {
						isGameOver = true;
						StartCoroutine ("DelayGameOver");
					}
				} else {
					if (isGameOver || tapCounter == totalTap) {
						isGameOver = true;
						GameOver ();
					}
				}
                
			} else if (hit.collider.tag == "Start") {
                gameManager.ShowEffect(hit.transform.position);
				gameManager.PlayRandomNote ();
				PianoButtonManager tmpPiano = hit.collider.gameObject.GetComponent<PianoButtonManager> ();
				isGameStarted = true;
				tmpPiano.currentPianoState = PianoButtonManager.PianoState.White;
				tmpPiano.UpdateState ();
				RowSwapping ();
				GenerateRow (3);
				//CheckBlackTile();
				tapCounter++;
			}
		}
	}

	void GameOver ()
	{
		Admanager.GameState = 1;
		print ("ad asar kotha");
		if (isLevelFailed) {
			scoreText.text = null;
            HighScoreText.text = null;
            GameObject.Find("LevelFailed").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
		} else {
			scoreText.text = timeCounter.ToString ("0.00") + " sec";
			SetHighScore ();
		}
		gameOverUIAnim.SetBool ("open", true);

	}
	IEnumerator DelayGameOver ()
	{
		yield return new WaitForSeconds (0.5f);
		GameOver ();
	}
	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
