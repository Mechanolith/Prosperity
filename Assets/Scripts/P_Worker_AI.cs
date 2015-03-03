using UnityEngine;
using System.Collections;

public class P_Worker_AI : MonoBehaviour {
	public float moveSpeed;
	public Resource.ResType mustCollect;
	public bool dying = false;
	public bool carrying = false;
	private int resNo;
	private A_Resource_God resGod;
	private int rand;


	void Start () {
		resGod = GameObject.Find ("A_Game_Logic").GetComponent<A_Resource_God> ();

		for(int i = 0; i < resGod.resList.Count; i++){
			if(resGod.resList[i].name == mustCollect){
				resNo = i;
			}
		}

		rand = Random.Range (0, 100);
		if(rand < 50){
			moveSpeed = 1;
		}
		else{
			moveSpeed = -1;
		}
	}

	void Update () {
		if(!dying){
			rigidbody.velocity = Vector3.right * moveSpeed;
		}
		else{
			rigidbody.velocity = Vector3.zero;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "End"){
			moveSpeed *= -1;
			carrying = true;
		}
		else if (col.tag == "Tower" && carrying == true){
			resGod.GetResource(resNo);
			moveSpeed *= -1;
			carrying = false;
		}
	}
}
