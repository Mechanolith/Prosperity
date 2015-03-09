using UnityEngine;
using System.Collections;

public class A_BASK_God : MonoBehaviour {
	public A_Resource_God resGod;
	public A_Interface_God  uiGod;
	public float baskLength, loiterLength;
	public AudioClip baskTrack, revelTrack;
	public float camSpeed;

	private bool basking = false;
	private float baskTimer, tempXRot;
	private Vector3 initCamPos, tempRot;
	private Quaternion initCamRot;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		uiGod = GameObject.Find("A_Game_Logic").GetComponent<A_Interface_God>();
		initCamPos = Camera.main.transform.position;
		initCamRot = Camera.main.transform.rotation;
	}

	void Update () {
		baskTimer -= Time.deltaTime;
		
		if(basking){
			if(baskTimer >= loiterLength){
				camSpeed = (resGod.newLevel.iteration - 1)* 2.5f/baskLength;
				Camera.main.transform.position += new Vector3 (0,camSpeed,camSpeed/((5*resGod.newLevel.iteration)/((baskLength-baskTimer) + 1))) * Time.deltaTime;
				tempXRot = -camSpeed/(125/((baskLength-baskTimer) + 1));
				if(Camera.main.transform.rotation.eulerAngles.x < 75){
					tempXRot = Mathf.Clamp(tempXRot, 0, 75);
					Camera.main.transform.Rotate(new Vector3(tempXRot,0,0));
				}
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
		Camera.main.transform.rotation = initCamRot;
	}
}
