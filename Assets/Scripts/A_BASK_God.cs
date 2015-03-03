using UnityEngine;
using System.Collections;

public class A_BASK_God : MonoBehaviour {
	public A_Resource_God resGod;
	public A_Interface_God  uiGod;
	public float baskLength, loiterLength;
	public AudioClip baskTrack, revelTrack;
	public float camSpeed;

	private bool basking = false;
	private float baskTimer;
	private Vector3 initCamPos;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		uiGod = GameObject.Find("A_Game_Logic").GetComponent<A_Interface_God>();
		initCamPos = Camera.main.transform.position;
	}

	void Update () {
		baskTimer -= Time.deltaTime;
		
		if(basking){
			if(baskTimer >= loiterLength){
				camSpeed = (resGod.newLevel.iteration - 1)* 2/baskLength;
				Camera.main.transform.position += new Vector3 (0,camSpeed,0) * Time.deltaTime;
			}
			
			if(baskTimer <= 0){
				uiGod.EnableInterface();
				basking = false;
				ResetCamera();
			}
		}
	}

	public void StartBask(){
		uiGod.DisableInterface();
		baskTimer = baskLength + loiterLength;
		basking = true;
	}

	void ResetCamera(){
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,initCamPos,1);
	}
}
