using UnityEngine;
using System.Collections;

public class ArcadePlusPianoManager : MonoBehaviour {

    public enum PianoState { White, Black , Bomb};

    public PianoState currentPianoState = PianoState.White;
    public Sprite whiteSprite;
    public Sprite blackSprite;
    public Sprite blackStartSprite;
    public Sprite bombSprite;

    private ArcadePlusGameManager gameManager;
    private bool isGenNewRow = false;
    private bool canCall = false;
    void Start()
    {
        gameManager = GameObject.Find("ArcadePlusGameManager").GetComponent<ArcadePlusGameManager>();
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
        if (!gameManager.isGameOver && gameManager.isGameStarted)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -100f), Time.deltaTime * gameManager.speed);
        }

        if (transform.position.y <= gameManager.row5[0].position.y && gameManager.isGameStarted)
        {
            if (currentPianoState == PianoState.Black)
            {
                gameManager.isGameOver = true;
                gameManager.GameOver();
            }
        }

        if (currentPianoState == PianoState.Black && transform.position.y <= 4.49f && transform.position.y > 3f 
            && !isGenNewRow && !gameManager.isGameOver && gameManager.isGameStarted && (gameManager.currentMode == ArcadePlusGameManager.ArcadeMode.Lightning
            || gameManager.currentMode == ArcadePlusGameManager.ArcadeMode.Duel))
        {
            //Debug.Log("problem ta ki??");
            isGenNewRow = true;
            gameManager.GenerateRow(4, 4.49f - transform.position.y);
            //Debug.Log(4.49f - transform.position.y);
        }
        else if ((currentPianoState == PianoState.Black || currentPianoState == PianoState.Bomb) && transform.position.y <= 4.49f && transform.position.y > 3f && !isGenNewRow && !gameManager.isGameOver && gameManager.isGameStarted)
        {
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
        else if (currentPianoState == PianoState.Bomb)
        {
            GetComponent<SpriteRenderer>().sprite = bombSprite;
        }
    }
}
