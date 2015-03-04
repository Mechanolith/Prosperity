using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Resource_God : MonoBehaviour {
	public List<Resource> resList = new List<Resource>();
	
	public Upgrade newWorker, newLevel;
	public GameObject towerBase, towerBody, towerCap;
	private Vector3 towerPos;

	public int maxWorkers, freeWorkers;
	public GameObject currentWorker, worker;
	public List<GameObject> workForce = new List<GameObject> ();
	public bool sacrificing = false;
	public bool isCult = false;

	public List<string> nameList = new List<string>();
	public List<string> firstNames = new List<string>();
	public List<string> lastNames = new List<string>();
	
	void Awake(){
		resList.Add(new Resource(Resource.ResType.Wood,498,0,10,0));
		resList.Add(new Resource(Resource.ResType.Stone,662,0,10,0));
		resList.Add(new Resource(Resource.ResType.Iron,766,0,10,0));
		resList.Add(new Resource(Resource.ResType.Gold,1522,10,10,0));
		resList.Add(new Resource(Resource.ResType.Bones,100000000,0,190,0));

		newWorker = new Upgrade(Upgrade.UpgType.newWorker,0,0,0,10,0,1,1);
		newLevel = new Upgrade(Upgrade.UpgType.newLevel,20,10,5,0,0,1,1);

		towerPos = new Vector3 (0f, -0.18f, -3.25f);
	}

	void Start () {
	}

	void Update () {

	}

	public void GetResource(int resNo){
		if(resList[resNo].supply > (resList[resNo].rate)){
			resList[resNo].current += resList[resNo].rate;
			resList[resNo].supply -= resList[resNo].rate;
		}
		else{
			resList[resNo].current += resList[resNo].supply;
			resList[resNo].supply = 0;
		}

		if(resList[resNo].supply == 0){
			gameObject.GetComponent<A_Interface_God>().EnableSacrifice();
		}
	}

	public void buyUpgrade(Upgrade upg){
		if(CheckAfford(upg)){
			PayForUpgrade(upg);
			UpgradeEffects(upg);
		}
	}

	bool CheckAfford(Upgrade upg){
		for(int i = 0; i < resList.Count; i++){
			if(resList[i].current < upg.costList[i]){
				return false;
			}
		}
		return true;
	}

	void PayForUpgrade(Upgrade upg){
		for(int j = 0; j < resList.Count; j++){
			resList[j].current -= upg.costList[j];
		}
	}

	void UpgradeEffects(Upgrade upg){
		if(upg.name == Upgrade.UpgType.newWorker){
			maxWorkers++;
			freeWorkers++;
			newWorker.costList[3] = Mathf.RoundToInt(newWorker.costList[3] * 1.15f);
			nameList.Add(ChooseName());
			newWorker.iteration++;
		}

		if(upg.name == Upgrade.UpgType.newLevel){
			ConstructTower();
			for(int i = 0; i < newLevel.costList.Count; i++){
				newLevel.costList[i] = Mathf.RoundToInt(newLevel.costList[i] * 1.15f);
				if(resList[i].current == 0 && resList[i].supply == 0){
					ReplaceResource(i);
				}
			}
			newLevel.iteration++;
		}
	}

	public void AddWorker(Resource res){
		if(freeWorkers > 0){
			res.workers++;
			freeWorkers--;
			currentWorker = Instantiate(worker,new Vector3(0f,-0.8f,-5f), Quaternion.identity) as GameObject;
			currentWorker.GetComponent<P_Worker_AI>().mustCollect = res.name;
			currentWorker.GetComponent<P_Worker_AI>().workerName = nameList[0];
			nameList.RemoveAt(0);
			workForce.Add(currentWorker);
		}
	}

	public void RemoveWorker(Resource res){
		P_Worker_AI workerScript;
		bool removed = false;
		GameObject tempWorker = null;
		int tempNum = 0;

		if(res.workers > 0){
			res.workers--;
			freeWorkers++;
			for(int k = 0; k < workForce.Count; k++){
				workerScript = workForce[k].GetComponent<P_Worker_AI>();
				if(workerScript.mustCollect == res.name && !workerScript.carrying){
					nameList.Add(workForce[k].GetComponent<P_Worker_AI>().workerName);
					Destroy(workForce[k]);
					workForce.RemoveAt(k);
					removed = true;
					break;
				}
			}

			if(!removed){
				for(int a = 0; a < workForce.Count; a++){
					workerScript = workForce[a].GetComponent<P_Worker_AI>();
					if(workerScript.mustCollect == res.name){
						tempWorker = workForce[a];
						tempNum = a;
					}
				}
				nameList.Add(tempWorker.GetComponent<P_Worker_AI>().workerName);
				Destroy(tempWorker);
				workForce.RemoveAt(tempNum);
			}
		}
	}

	void ConstructTower(){
		if(newLevel.iteration == 1){
			Instantiate(towerBase,towerPos,Quaternion.identity);
		}
		else if (newLevel.iteration > 1){
			Instantiate(towerBody,towerPos,Quaternion.identity);
		}

		towerPos += new Vector3 (0,2,0);
	}

	string ChooseName(){
		int firstRand, lastRand;
		string endName;

		firstRand = Mathf.RoundToInt(Random.Range(0,firstNames.Count));
		lastRand = Mathf.RoundToInt(Random.Range(0,lastNames.Count));

		endName = firstNames[firstRand] + " " + lastNames[lastRand];

		return endName;
	}

	void ReplaceResource(int resNo){
		newLevel.costList[4] += newLevel.costList[resNo];
		newLevel.costList [resNo] = 0;
	}
}