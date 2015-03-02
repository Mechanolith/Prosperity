using UnityEngine;
using System.Collections;

public class I_Upgrade_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public string upgName;
	public Upgrade upg;
	public TextMesh thisText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		thisText = gameObject.GetComponent<TextMesh>();
	}

	void Update () {
		if (upgName == "Worker"){
			upg = resGod.newWorker;
		}
		else if (upgName == "Level"){
			upg = resGod.newLevel;
		}
	}

	void OnMouseOver(){
		thisText.color = Color.red;
	}
	
	void OnMouseExit(){
		thisText.color = Color.white;
	}

	void OnMouseDown(){
		resGod.buyUpgrade(upg);
	}
}
