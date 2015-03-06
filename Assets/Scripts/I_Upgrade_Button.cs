using UnityEngine;
using System.Collections;

public class I_Upgrade_Button : MonoBehaviour {
	public A_Resource_God resGod;
	public string upgName, displayName;
	public Upgrade upg;
	public TextMesh thisText, levelUpgText, levelCostText, workerUpgText, workerCostText;

	void Start () {
		resGod = GameObject.Find("A_Game_Logic").GetComponent<A_Resource_God>();
		thisText = gameObject.GetComponent<TextMesh>();
		levelUpgText = GameObject.Find ("I_LevelUpgrade_Text").GetComponent<TextMesh> ();
		levelCostText = GameObject.Find ("I_LevelCost_Text").GetComponent<TextMesh> ();
		workerUpgText = GameObject.Find ("I_WorkerUpgrade_Text").GetComponent<TextMesh> ();
		workerCostText = GameObject.Find ("I_WorkerCost_Text").GetComponent<TextMesh> ();

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
			if(upg.name == Upgrade.UpgType.newLevel){
				levelUpgText.text = displayName;
				if(upg.costList[0] > 0){
					levelCostText.text = "W: " + upg.costList[0] + "\nS: " + upg.costList[1] + "\nI: " + upg.costList[2] + "\nImproves Productivity!";
				}
				else if(upg.costList[1] > 0 && resGod.isCult){
					levelCostText.text = "B: " + upg.costList[4] + "\nS: " + upg.costList[1] + "\nI: " + upg.costList[2] + "\nEnsures Productivity!";
				}
				else if(upg.costList[2] > 0 && resGod.isCult){
					levelCostText.text = "B: " + upg.costList[4] + "\nI: " + upg.costList[2] + "\nDemands Productivity!";
				}
				else if(resGod.isCult){
					levelCostText.text = "B: " + upg.costList[4] + "\nEnforces Productivity!";
				}
				else{
					levelCostText.text = "W: DEPLETED" + "\nS: " + upg.costList[1] + "\nI: " + upg.costList[2] + "\nImproves Productivity!";
				}
			}
			else{
				workerUpgText.text = displayName;
				workerCostText.text = "G: " + upg.costList[3];
			}
		}
	}
	
	void OnMouseExit(){
		thisText.color = Color.black;
		if(!resGod.sacrificing){
			levelUpgText.text = "";
			levelCostText.text = "";
			workerUpgText.text = "";
			workerCostText.text = "";
		}
	}

	void OnMouseDown(){
		resGod.buyUpgrade(upg);
	}
}
