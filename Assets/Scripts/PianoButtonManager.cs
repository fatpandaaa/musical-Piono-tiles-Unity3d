using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PianoButtonManager : MonoBehaviour {

    public enum PianoState { White, Black };

    public PianoState currentPianoState = PianoState.White;
    public Sprite whiteSprite;
    public Sprite blackSprite;
    public Sprite blackStartSprite;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateState() {
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
