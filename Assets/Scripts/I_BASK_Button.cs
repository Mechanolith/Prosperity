using UnityEngine;
using System.Collections;

public class I_BASK_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public A_BASK_God baskGod;
	public TextMesh thisText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		baskGod = GameObject.Find("A_Game_Logic").GetComponent<A_BASK_God>();
		thisText = gameObject.GetComponent<TextMesh>();
	}
	
	void Update () {
	}
	
	void OnMouseOver(){
		thisText.color = Color.red;
		if(resGod.isCult){
			thisText.text = "REVEL";
		}
	}
	
	void OnMouseExit(){
		thisText.color = Color.black;
	}

	void OnMouseDown(){
		OnMouseExit();
		baskGod.StartBask();
	}
}
