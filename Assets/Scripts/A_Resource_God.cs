using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Resource_God : MonoBehaviour {
	public A_Worker_God workGod;
	public List<Resource> resList = new List<Resource>();

	public int maxWorkers, freeWorkers;
	public Upgrade newWorker, newLevel;
	public float resTimer;
	public GameObject towerBase, towerBody, towerCap;
	public GameObject currentWorker, worker;
	public List<GameObject> workForce = new List<GameObject> ();
	private Vector3 towerPos;
	
	void Awake(){
		resList.Add(new Resource(Resource.ResType.Wood,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Stone,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Iron,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Gold,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Bones,0,0,0,0));

		newWorker = new Upgrade(Upgrade.UpgType.newWorker,0,0,0,100,0,1,1);
		newLevel = new Upgrade(Upgrade.UpgType.newLevel,10,10,10,0,0,1,1);

		towerPos = new Vector3 (0f, -0.18f, -3.25f);
	}

	void Start () {
		workGod = gameObject.GetComponent<A_Worker_God>();
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
			newWorker.iteration++;
		}

		if(upg.name == Upgrade.UpgType.newLevel){
			ConstructTower();
			for(int i = 0; i < newLevel.costList.Count; i++){
				newLevel.costList[i] = Mathf.RoundToInt(newLevel.costList[i] * 1.15f);
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
}