using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public bool isSoundOn = true;
	public int subLevelIndex = 1;
    public int[] noteOrder1 = new int[26];
    public int[] noteOrder2 = new int[26];
    public int[] noteOrder3 = new int[26];
    public int[] noteOrder4 = new int[26];
    public int[] noteOrder5 = new int[26];
    public int[] noteOrder6 = new int[26];
    public int[] noteOrder7 = new int[26];

	public AudioClip[] audio;
    public GameObject particleEffect;
	
    private AudioSource[] audioSource = new AudioSource[26];
    private AudioSource[] reserveAudio = new AudioSource[10];
    private int noteInd = 0;
    private int noteType = 0;

	void Start ()
	{
        //reserveAudio = GetComponents<AudioSource>();
		isSoundOn = true;

        ResetNoteIndex();
        for (int i = 0; i < reserveAudio.Length; i++ )
        {
            reserveAudio[i] = this.gameObject.AddComponent("AudioSource") as AudioSource;
            reserveAudio[i].playOnAwake = false;
            reserveAudio[i].loop = false;
        }
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
        if (noteType <= 6)
        {
            if (noteType == 0)
            {
                if (!audioSource[noteOrder1[noteInd] - 1].isPlaying)
                    audioSource[noteOrder1[noteInd] - 1].Play();
                else {
                    for (int i = 0; i < reserveAudio.Length; i++) {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1) {
                            reserveAudio[i].clip = audioSource[noteOrder1[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                    
                }

                noteInd++;
                if (noteInd >= noteOrder1.Length)
                    SetNoteIndex();
            }
            else if (noteType == 1)
            {
                if (!audioSource[noteOrder2[noteInd] - 1].isPlaying)
                    audioSource[noteOrder2[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder2[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }

                noteInd++;
                if (noteInd >= noteOrder2.Length)
                    SetNoteIndex();
            }
            else if (noteType == 2)
            {
                if (!audioSource[noteOrder3[noteInd] - 1].isPlaying)
                    audioSource[noteOrder3[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder3[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                    
                }

                noteInd++;
                if (noteInd >= noteOrder3.Length)
                    SetNoteIndex();
            }
            else if (noteType == 3)
            {
                if (!audioSource[noteOrder4[noteInd] - 1].isPlaying)
                    audioSource[noteOrder4[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder4[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                    
                }

                noteInd++;
                if (noteInd >= noteOrder4.Length)
                    SetNoteIndex();
            }
            else if (noteType == 4)
            {
                if (!audioSource[noteOrder5[noteInd] - 1].isPlaying)
                    audioSource[noteOrder5[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder4[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }

                }

                noteInd++;
                if (noteInd >= noteOrder5.Length)
                    SetNoteIndex();
            }
            else if (noteType == 5)
            {
                if (!audioSource[noteOrder6[noteInd] - 1].isPlaying)
                    audioSource[noteOrder6[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder6[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }

                }

                noteInd++;
                if (noteInd >= noteOrder6.Length)
                    SetNoteIndex();
            }
            else if (noteType == 6)
            {
                if (!audioSource[noteOrder7[noteInd] - 1].isPlaying)
                    audioSource[noteOrder7[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder7[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }

                }

                noteInd++;
                if (noteInd >= noteOrder7.Length)
                    SetNoteIndex();
            }
        }
        else
        {
            if (noteType == 7)
            {
                if (!audioSource[noteOrder1[noteInd] - 1].isPlaying)
                    audioSource[noteOrder1[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder1[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                    
                }
            }
            else if (noteType == 8)
            {
                if (!audioSource[noteOrder2[noteInd] - 1].isPlaying)
                    audioSource[noteOrder2[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder2[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }
            }
            else if (noteType == 9)
            {
                if (!audioSource[noteOrder3[noteInd] - 1].isPlaying)
                    audioSource[noteOrder3[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder3[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }
            }
            else if (noteType == 10)
            {
                if (!audioSource[noteOrder4[noteInd] - 1].isPlaying)
                    audioSource[noteOrder4[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder4[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }
            }
            else if (noteType == 11)
            {
                if (!audioSource[noteOrder5[noteInd] - 1].isPlaying)
                    audioSource[noteOrder5[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder5[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }
            }
            else if (noteType == 12)
            {
                if (!audioSource[noteOrder6[noteInd] - 1].isPlaying)
                    audioSource[noteOrder6[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder6[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }
            }
            else if (noteType == 13)
            {
                if (!audioSource[noteOrder7[noteInd] - 1].isPlaying)
                    audioSource[noteOrder7[noteInd] - 1].Play();
                else
                {
                    for (int i = 0; i < reserveAudio.Length; i++)
                    {
                        if (!reserveAudio[i].isPlaying || i == reserveAudio.Length - 1)
                        {
                            reserveAudio[i].clip = audioSource[noteOrder7[noteInd] - 1].clip;
                            reserveAudio[i].Play();
                            break;
                        }
                    }
                }
            }

            noteInd--;
            if (noteInd < 0)
                SetNoteIndex();
        }
	}

    void SetNoteIndex() {
        if (noteType <= 6)
        {
            noteInd = 0;
        }
        else
        {
            if (noteType == 7)
                noteInd = noteOrder1.Length - 1;
            else if (noteType == 8)
                noteInd = noteOrder2.Length - 1;
            else if (noteType == 9)
                noteInd = noteOrder3.Length - 1;
            else if (noteType == 10)
                noteInd = noteOrder4.Length - 1;
            else if (noteType == 11)
                noteInd = noteOrder5.Length - 1;
            else if (noteType == 12)
                noteInd = noteOrder6.Length - 1;
            else if (noteType == 13)
                noteInd = noteOrder7.Length - 1;
        }
    }

    void RandomNoteGeneration() {
        if (noteInd >= 0 || noteInd <= 3)
        {
            int min = Mathf.Clamp(noteInd - 3, 0, noteInd);
            
            int rnd = Random.Range(min, noteInd+3);

            int count = 0;
            while(rnd == noteInd || audioSource[rnd].isPlaying){
                count++;
                if (count > 50)
                {
                    rnd = Random.Range(23, 26);
                }
                else {
                    rnd = Random.Range(min, noteInd + 3);
                }
                
                //Debug.Log("Loooop");
            }
            noteInd = rnd;
        }
        else if (noteInd <= 25 || noteInd >= 22)
        {
            int max = Mathf.Clamp(noteInd + 3, noteInd, 25);
            int rnd = Random.Range(noteInd-3, max);

            int count = 0;
            while (rnd == noteInd || audioSource[rnd].isPlaying)
            {
                count++;
                if (count > 50)
                {
                    rnd = Random.Range(0, 3);
                }
                else
                {
                    rnd = Random.Range(noteInd - 3, max);
                }
                
                //Debug.Log("Loooop");
            }
            noteInd = rnd;
        }

        else {
            int rnd = Random.Range(noteInd-3, noteInd+3);
            int count = 0;

            while (rnd == noteInd || audioSource[rnd].isPlaying)
            {
                count++;
                if (count > 50)
                {
                    rnd = Random.Range(4, 24);
                }
                else
                {
                    rnd = Random.Range(noteInd - 3, noteInd + 3);
                }
                
                Debug.Log("Loooop");
            }

            noteInd = rnd;
        }
    }

    public void ResetNoteIndex() {
        noteType = Random.Range(0, 14);
        SetNoteIndex();
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
