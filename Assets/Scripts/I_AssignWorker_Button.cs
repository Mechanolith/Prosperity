using UnityEngine;
using System.Collections;

public class I_AssignWorker_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public bool addWorker;
	public int resNum;
	public Resource.ResType resName;
	public TextMesh thisText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();

		for(int i = 0; i < resGod.resList.Count; i++){
			if(resGod.resList[i].name == resName){
				resNum = i;
			}
		}

		thisText = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		thisText.color = Color.red;
	}

	void OnMouseExit(){
		thisText.color = Color.black;
	}

	void OnMouseDown(){
		if(addWorker){
			resGod.AddWorker(resGod.resList[resNum]);
		}
		else{
			resGod.RemoveWorker(resGod.resList[resNum]);
		}
	}
}
