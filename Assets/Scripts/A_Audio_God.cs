using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Audio_God : MonoBehaviour {
	public List<AudioSource> AudioProphets = new List<AudioSource>();
	public float lerpFactor;
	public float lerpTimeStep;
	public float testTimer;
	private float lerpTimer;
	private int state = 0;

	void Start () {
		for (int i = 0; i < 4; i++){
			AudioProphets.Add (GameObject.Find("A_Music_Prophet_"+i).GetComponent<AudioSource>());
		}

		testTimer = 7;
		lerpTimer = lerpTimeStep;
	}

	void Update () {
		switch(state){
			case 1:
				if(lerpTimer <=0 && AudioProphets[1].volume < 0.9f){
					AudioProphets[1].volume = Mathf.Lerp(AudioProphets[1].volume,1,lerpFactor);
					lerpTimer = lerpTimeStep;
//					print("Audio 1 Vol: " + AudioProphets[1].volume);
				}
				break;

			case 2:
				if(lerpTimer <=0 && AudioProphets[2].volume < 0.9f){
					AudioProphets[2].volume = Mathf.Lerp(AudioProphets[2].volume,1,lerpFactor);
					lerpTimer = lerpTimeStep;
//					print("Audio 2 Vol: " + AudioProphets[2].volume);
				}
				break;

			case 3:
				if(lerpTimer <=0 && AudioProphets[3].volume < 0.9f){
					AudioProphets[3].volume = Mathf.Lerp(AudioProphets[3].volume,1,lerpFactor);
					lerpTimer = lerpTimeStep;
//					print("Audio 3 Vol: " + AudioProphets[3].volume);
				}
				break;
			default:
				break;
		}

		testTimer -= Time.deltaTime;
		if(testTimer <= 0){
			AdvanceAudio();
			testTimer = 7;
		}

		lerpTimer -= Time.deltaTime;
	}

	public void AdvanceAudio(){
		state ++;
	}
}
