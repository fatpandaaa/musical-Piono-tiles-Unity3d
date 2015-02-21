using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZenGameManager : MonoBehaviour
{

	public enum ZenMode
	{
		Second15,
		Second30,
		Pro }
	;

	public ZenMode currentMode = ZenMode.Second15;
	public GameManager gameManager;
	public bool isGameStarted = false;
	public bool isGameOver = false;
	public bool isLevelFailed = false;
	//public int totalTap = 25;
	public int tapCounter = 0;
	public float timeCounter = 15f;

	public GameObject[] row1 = new GameObject[4];
	public GameObject[] row2 = new GameObject[4];
	public GameObject[] row3 = new GameObject[4];
	public GameObject[] row4 = new GameObject[4];
	public GameObject[] row5 = new GameObject[4];

	private Text timerText;
	private Text scoreText;
	private Slider timeSlider;
	private Animator gameOverUIAnim;
	void Start ()
	{
		gameManager = GameObject.FindObjectOfType<GameManager> ();
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		timeSlider = GameObject.Find ("TimeSlider").GetComponent<Slider> ();
		gameOverUIAnim = GameObject.Find ("GameOverUI").GetComponent<Animator> ();
		if (gameManager.subLevelIndex == 1) {
			currentMode = ZenMode.Second15;
		} else if (gameManager.subLevelIndex == 2) {
			currentMode = ZenMode.Second30;
		} else if (gameManager.subLevelIndex == 3) {
			currentMode = ZenMode.Pro;
		}
		if (gameManager.isSoundOn)
			GetComponent<AudioSource> ().volume = 1f;
		else
			GetComponent<AudioSource> ().volume = 0f;
		if (currentMode == ZenMode.Second15) {
			//totalTap = 25;
			timeCounter = 15f;
		} else if (currentMode == ZenMode.Second30) {
			//totalTap = 50;
			timeCounter = 30f;
		} else if (currentMode == ZenMode.Pro) {
			//totalTap = 50;
			timeCounter = 50f;
		}
        gameManager.ResetNoteIndex();
		isGameStarted = false;
		timeSlider.minValue = 0f;
		timeSlider.maxValue = timeCounter;
		timeSlider.value = timeCounter;
		GenerateFullGrid ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}
		InputHandler ();
		if (!isGameOver && timeCounter > 0f && isGameStarted) {
			timeCounter -= Time.deltaTime;
			timeSlider.value = timeCounter;
			//Debug.Log("Entered");
            
			timerText.text = tapCounter.ToString ();
		} else if (!isGameOver && timeCounter <= 0f && isGameStarted) {
			isGameOver = true;
			GameOver ();
		}
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
				GameOver ();
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

				if (tmpPiano.currentPianoState == PianoButtonManager.PianoState.Black) {
                    gameManager.ShowEffect(hit.transform.position);
					gameManager.PlayRandomNote ();
					tmpPiano.currentPianoState = PianoButtonManager.PianoState.White;
					tmpPiano.UpdateState ();
					RowSwapping ();
					GenerateRow (3);
					CheckBlackTile ();
					tapCounter++;
					if (isGameOver || timeCounter <= 0f) {
						Debug.Log (isGameOver + "-------" + timeCounter);
						isGameOver = true;
						GameOver ();
					}
				} else if (tmpPiano.currentPianoState == PianoButtonManager.PianoState.White) {
					isGameOver = true;
					isLevelFailed = false;
					GetComponent<AudioSource> ().Play ();
					tmpPiano.gameObject.GetComponent<Animator> ().Play ("TileBlink");
					if (isGameOver || timeCounter <= 0f) {
						Debug.Log (isGameOver + "-------" + timeCounter);
						isGameOver = true;
						StartCoroutine ("DelayGameOver");
					}
					//Application.LoadLevel(Application.loadedLevel);
				} else {
					if (isGameOver || timeCounter <= 0f) {
						Debug.Log (isGameOver + "-------" + timeCounter);
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
		scoreText.text = tapCounter.ToString ();

		gameOverUIAnim.SetBool ("open", true);
		Admanager.GameState = 1;
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
