using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public bool isSoundOn = true;
	public int subLevelIndex = 1;
    public int[] noteOrder = new int[26];
	public AudioClip[] audio;
    public GameObject particleEffect;
	
    private AudioSource[] audioSource = new AudioSource[26];
    private int noteInd = 0;

	void Start ()
	{
		isSoundOn = true;
        noteInd = 0;
		//Debug.Log (audio.Length);
		for (int i = 0; i < audio.Length; i++) {
			audioSource [i] = this.gameObject.AddComponent ("AudioSource") as AudioSource;
            
			audioSource [i].clip = audio [i];
			audioSource [i].playOnAwake = false;
			audioSource [i].loop = false;
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PlayRandomNote ()
	{
		//int rnd = Random.Range (0, 26);

        audioSource[noteOrder[noteInd] - 1].Play();
        noteInd++;
        if (noteInd > audio.Length - 1)
            noteInd = 0;
	}

    public void ResetNoteIndex() {
        noteInd = 0;
    }

	public void SoundToggle ()
	{
		if (isSoundOn) {
			isSoundOn = false;
			for (int i = 0; i < audio.Length; i++) {
				audioSource [i].volume = 0f;
			}
			GameObject.Find ("MainMenuManager").GetComponent<AudioSource> ().volume = 0f;
			GameObject.Find ("SoundText").GetComponent<Text> ().text = "Sound Off";
		} else {
			isSoundOn = true;
			for (int i = 0; i < audio.Length; i++) {
				audioSource [i].volume = 1f;
			}
			GameObject.Find ("MainMenuManager").GetComponent<AudioSource> ().volume = 1f;
			GameObject.Find ("SoundText").GetComponent<Text> ().text = "Sound On";
		}
	}

    public void ShowEffect(Vector2 pos) {
        Instantiate(particleEffect, pos, Quaternion.identity);
    }
}
