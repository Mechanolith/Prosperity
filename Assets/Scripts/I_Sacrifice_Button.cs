using UnityEngine;
using System.Collections;

public class I_Sacrifice_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public TextMesh thisText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		thisText = gameObject.GetComponent<TextMesh>();
	}

	void Update () {
	
	}

	void OnMouseOver(){
		thisText.color = Color.red;
	}
	
	void OnMouseExit(){
		thisText.color = Color.white;
	}
	
	void OnMouseDown(){
		if(!resGod.sacrificing){
			resGod.sacrificing = true;
			print("Knife out");
		}
		else{
			resGod.sacrificing = false;
			print("Kinfe away");
		}
	}
}
