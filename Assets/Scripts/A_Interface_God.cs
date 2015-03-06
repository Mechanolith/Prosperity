using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Interface_God : MonoBehaviour {
	public A_Resource_God resGod;
	public float textTimer;
	public TextMesh woodCount,stoneCount,ironCount,goldCount,boneCount, workerCount;
	public TextMesh woodAssigned, stoneAssigned, ironAssigned, goldAssigned, currentLevel, hireText;
	public GameObject userInterface, sacrificeButton, BaskButton;
	private List<string> resNames = new List<string>();
	private List<TextMesh> resTexts = new List<TextMesh>();

	void Start () {
		resGod = gameObject.GetComponent<A_Resource_God>();
		resTexts.Add(GameObject.Find("I_Wood_Count").GetComponent<TextMesh>());
		resTexts.Add(GameObject.Find("I_Stone_Count").GetComponent<TextMesh>());
		resTexts.Add(GameObject.Find("I_Iron_Count").GetComponent<TextMesh>());
		resTexts.Add(GameObject.Find("I_Gold_Count").GetComponent<TextMesh>());
		boneCount = GameObject.Find("I_Bones_Count").GetComponent<TextMesh>();
		workerCount = GameObject.Find("I_Worker_Count").GetComponent<TextMesh>();

		woodAssigned = GameObject.Find("I_Wood_Assigned").GetComponent<TextMesh>();
		stoneAssigned = GameObject.Find("I_Stone_Assigned").GetComponent<TextMesh>();
		ironAssigned = GameObject.Find("I_Iron_Assigned").GetComponent<TextMesh>();
		goldAssigned = GameObject.Find("I_Gold_Assigned").GetComponent<TextMesh>();

		currentLevel = GameObject.Find("I_CurrentLevel_Text").GetComponent<TextMesh>();

		hireText = GameObject.Find("I_Hire_Text").GetComponent<TextMesh>();

		userInterface = GameObject.Find("I_Interface");

		sacrificeButton = GameObject.Find ("I_Sacrifice_Button");
		sacrificeButton.SetActive (false);

		BaskButton = GameObject.Find ("I_BASK_Button");
		BaskButton.SetActive (false);

		resNames.Add("WOOD");
		resNames.Add("STONE");
		resNames.Add("IRON");
		resNames.Add("GOLD");
	}

	void Update () {
		for(int i = 0; i < resGod.resList.Count-1; i++){
			if(resGod.resList[i].current > 0 || resGod.resList[i].supply > 0){
				resTexts[i].text = resNames[i] + ": " + resGod.resList[i].current;
			}
			else if(resGod.resList[i].supply == 0){
				resTexts[i].text = resNames[i] + ": SPENT";
			}
		}
		if(resGod.isCult){
			boneCount.text = "BONES: " + resGod.resList[4].current;
		}
		else{
			boneCount.text = "";
		}
		workerCount.text = "WORKERS: " + resGod.freeWorkers +"/" + resGod.maxWorkers;

		woodAssigned.text = resGod.resList[0].workers + "";
		stoneAssigned.text = resGod.resList[1].workers + "";
		ironAssigned.text = resGod.resList[2].workers + "";
		goldAssigned.text = resGod.resList[3].workers + "";

		currentLevel.text = resGod.newLevel.iteration - 1 + " LEVELS";

		if(textTimer > 0){
			textTimer -= Time.deltaTime;
		}
		else{
			hireText.text = "";
		}
	}

	public void EnableInterface(){
		userInterface.SetActive(true);
	}

	public void DisableInterface(){
		userInterface.SetActive(false);
	}

	public void EnableSacrifice(){
		sacrificeButton.SetActive (true);
	}

	public void EnableBask(){
		BaskButton.SetActive (true);
	}

	public void OnHireWorker(){
		textTimer = 5f;
		if(!resGod.isCult){
			hireText.text = resGod.nameList[resGod.nameList.Count-1] + " has joined the crew!";
		}
		else{
			hireText.text = resGod.nameList[resGod.nameList.Count-1] + " has joined the cult!";
		}
	}
}
