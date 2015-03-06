using UnityEngine;
using System.Collections;

public class P_Worker_AI : MonoBehaviour {
	public float moveSpeed;
	public Resource.ResType mustCollect;
	public bool dying = false;
	public bool carrying = false;
	public string workerName;
	public int workerNumber;

	private int resNo;
	private A_Resource_God resGod;
	private int rand;
	private TextMesh nameText;
	private TextMesh infoText;

	void Start () {
		resGod = GameObject.Find ("A_Game_Logic").GetComponent<A_Resource_God> ();

		for(int i = 0; i < resGod.resList.Count; i++){
			if(resGod.resList[i].name == mustCollect){
				resNo = i;
			}
		}

		rand = Random.Range (0, 100);
		if(rand < 50){
			moveSpeed *= -1;
		}

		nameText = gameObject.transform.GetChild(0).GetComponent<TextMesh>();
		infoText = GameObject.Find ("I_WorkerCost_Text").GetComponent<TextMesh> ();
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

	void OnMouseEnter(){
		if(resGod.sacrificing){
			//Highlight
		}

		nameText.text = workerName;
	}

	void OnMouseExit(){
		if(resGod.sacrificing){
			//Remove Highlight
        }

		nameText.text = "";
	}

	void OnMouseDown(){
		if(resGod.sacrificing){
			resGod.sacrificing = false;
			dying = true;
			resGod.isCult = true;
			resGod.GetResource(4);
			infoText.text = "";
			print ("Worker Says R = " + resNo + " W = " + workerNumber);
            resGod.SacrificeWorker(resNo, workerNumber);
			Destroy(gameObject);
        }
	}
}
