using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Resource_God : MonoBehaviour {
	public A_Worker_God workGod;
	public List<Resource> resList = new List<Resource>();

	public int maxWorkers, freeWorkers;
	//public Resource wood, stone, iron, gold, bones;
	public Upgrade newWorker, newLevel;
	public float resTimer;
	
	void Awake(){
		resList.Add(new Resource(Resource.ResType.Wood,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Stone,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Iron,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Gold,50000,10000,10,0));
		resList.Add(new Resource(Resource.ResType.Bones,0,0,0,0));

		newWorker = new Upgrade(Upgrade.UpgType.newWorker,0,0,0,100,0,1,1);
		newLevel = new Upgrade(Upgrade.UpgType.newLevel,10,10,10,0,0,1,1);
	}

	void Start () {
		workGod = gameObject.GetComponent<A_Worker_God>();
	}

	void Update () {
		resTimer -= Time.deltaTime;
		if(resTimer <= 0){
			for(int i = 0; i < resList.Count; i++){
				if(resList[i].supply > (resList[i].workers * resList[i].rate)){
					resList[i].current += resList[i].workers * resList[i].rate;
					resList[i].supply -= resList[i].workers * resList[i].rate;
				}
				else{
					resList[i].current += resList[i].supply;
					resList[i].supply = 0;
				}
			}
			resTimer = 15;
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
		}
	}

	public void RemoveWorker(Resource res){
		if(res.workers > 0){
			res.workers--;
			freeWorkers++;
		}
	}
}