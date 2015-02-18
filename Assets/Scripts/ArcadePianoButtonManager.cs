using UnityEngine;
using System.Collections;

public class ArcadePianoButtonManager : MonoBehaviour {

    public enum PianoState { White, Black };

    public PianoState currentPianoState = PianoState.White;
    public Sprite whiteSprite;
    public Sprite blackSprite;
    public Sprite blackStartSprite;

    private ArcadeGameManager gameManager;
    private bool isGenNewRow = false;
    private bool canCall = false;
    void Start()
    {
        gameManager = GameObject.Find("ArcadeGameManager").GetComponent<ArcadeGameManager>();
        isGenNewRow = false;
        if (transform.position.y == 4.49f)
            canCall = true;
        else if (transform.position.y == 7.2f)
            canCall = true;
        else
            canCall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver && gameManager.isGameStarted) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -100f), Time.deltaTime * gameManager.speed);
        }

        if (transform.position.y <= gameManager.row5[0].position.y && gameManager.isGameStarted) {
            if (currentPianoState == PianoState.Black)
            {
                gameManager.isGameOver = true;
                gameManager.GameOver();
            }
        }

        if (currentPianoState == PianoState.Black && transform.position.y <= 4.49f && transform.position.y > 3f && !isGenNewRow && !gameManager.isGameOver && gameManager.isGameStarted) {
            //Debug.Log("problem ta ki??");
            isGenNewRow = true;
            gameManager.GenerateRow(4, 4.49f - transform.position.y);
            //Debug.Log(4.49f - transform.position.y);
        }
    }

    public void UpdateState()
    {
        if (currentPianoState == PianoState.Black)
        {
            GetComponent<SpriteRenderer>().sprite = blackSprite;
        }
        else if (currentPianoState == PianoState.White)
        {
            GetComponent<SpriteRenderer>().sprite = whiteSprite;
        }
    }
}
