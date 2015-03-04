using UnityEngine;
using System.Collections;

public class I_Upgrade_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public string upgName, displayName;
	public Upgrade upg;
	public TextMesh thisText, upgText, costText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		thisText = gameObject.GetComponent<TextMesh>();
		upgText = GameObject.Find ("I_Upgrade_Text").GetComponent<TextMesh> ();
		costText = GameObject.Find ("I_Cost_Text").GetComponent<TextMesh> ();

		if (upgName == "Worker"){
			upg = resGod.newWorker;
			displayName = "Hire Worker";
		}
		else if (upgName == "Level"){
			upg = resGod.newLevel;
			displayName = "Expand Tower";
		}
	}

	void Update () {

	}

	void OnMouseOver(){
		thisText.color = Color.red;
		if(!resGod.sacrificing){
			upgText.text = displayName;
			if(upg.name == Upgrade.UpgType.newLevel){
				if(upg.costList[0] > 0){
					costText.text = "W: " + upg.costList[0] + "\nS: " + upg.costList[1] + "\nI: " + upg.costList[2];
				}
				else if(upg.costList[1] > 0 && resGod.isCult){
					costText.text = "B: " + upg.costList[4] + "\nS: " + upg.costList[1] + "\nI: " + upg.costList[2];
				}
				else if(upg.costList[2] > 0 && resGod.isCult){
					costText.text = "B: " + upg.costList[4] + "\nI: " + upg.costList[2];
				}
				else if(resGod.isCult){
					costText.text = "B: " + upg.costList[4];
				}
				else{
					costText.text = "W: INSUFFICIENT" + "\nS: " + upg.costList[1] + "\nI: " + upg.costList[2];
				}
			}
			else{
				costText.text = "G: " + upg.costList[3];
			}
		}
	}
	
	void OnMouseExit(){
		thisText.color = Color.white;
		if(!resGod.sacrificing){
			upgText.text = "";
			costText.text = "";
		}
	}

	void OnMouseDown(){
		resGod.buyUpgrade(upg);
	}
}
