using UnityEngine;
using System.Collections;

public class A_Interface_God : MonoBehaviour {
	public A_Resource_God resGod;
	public TextMesh woodCount,stoneCount,ironCount,goldCount,boneCount, workerCount;
	public TextMesh woodAssigned, stoneAssigned, ironAssigned, goldAssigned;
	public GameObject userInterface, sacrificeButton, BaskButton;

	void Start () {
		resGod = gameObject.GetComponent<A_Resource_God>();
		woodCount = GameObject.Find("I_Wood_Count").GetComponent<TextMesh>();
		stoneCount = GameObject.Find("I_Stone_Count").GetComponent<TextMesh>();
		ironCount = GameObject.Find("I_Iron_Count").GetComponent<TextMesh>();
		goldCount = GameObject.Find("I_Gold_Count").GetComponent<TextMesh>();
		boneCount = GameObject.Find("I_Bones_Count").GetComponent<TextMesh>();
		workerCount = GameObject.Find("I_Worker_Count").GetComponent<TextMesh>();

		woodAssigned = GameObject.Find("I_Wood_Assigned").GetComponent<TextMesh>();
		stoneAssigned = GameObject.Find("I_Stone_Assigned").GetComponent<TextMesh>();
		ironAssigned = GameObject.Find("I_Iron_Assigned").GetComponent<TextMesh>();
		goldAssigned = GameObject.Find("I_Gold_Assigned").GetComponent<TextMesh>();

		userInterface = GameObject.Find("I_Interface");

		sacrificeButton = GameObject.Find ("I_Sacrifice_Button");
		sacrificeButton.SetActive (false);

		BaskButton = GameObject.Find ("I_BASK_Button");
		BaskButton.SetActive (false);
	}

	void Update () {
		woodCount.text = "WOOD: " + resGod.resList[0].current;
		stoneCount.text = "STONE: " + resGod.resList[1].current;
		ironCount.text = "IRON: " + resGod.resList[2].current;
		goldCount.text = "GOLD: " + resGod.resList[3].current;
		if(resGod.isCult){
			boneCount.text = "BONES: " + resGod.resList[4].current;
		}
		else{
			boneCount.text = "";
		}
		workerCount.text = "WORKERS: " + resGod.freeWorkers +"/" + resGod.maxWorkers;

		woodAssigned.text = resGod.resList[0].workers + "W";
		stoneAssigned.text = resGod.resList[1].workers + "S";
		ironAssigned.text = resGod.resList[2].workers + "I";
		goldAssigned.text = resGod.resList[3].workers + "G";
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
}
