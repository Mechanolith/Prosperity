using UnityEngine;
using System.Collections;

public class I_Sacrifice_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public TextMesh thisText, titleText, infoText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		thisText = gameObject.GetComponent<TextMesh>();
		titleText = GameObject.Find("I_Upgrade_Text").GetComponent<TextMesh>();
		infoText = GameObject.Find("I_Cost_Text").GetComponent<TextMesh>();
	}

	void Update () {
	
	}

	void OnMouseOver(){
		thisText.color = Color.red;
		titleText.text = "Sacrifice Worker";
	}
	
	void OnMouseExit(){
		thisText.color = Color.white;
		titleText.text = "";
	}
	
	void OnMouseDown(){
		if(!resGod.sacrificing){
			resGod.sacrificing = true;
			infoText.text = "Choose a worker to sacrifice";
		}
		else{
			resGod.sacrificing = false;
			infoText.text = "";
		}
	}
}
