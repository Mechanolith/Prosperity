using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Resource
{
	public enum ResType{
		Wood,
		Iron,
		Stone,
		Gold,
		Bones
	}
	
	public ResType name;
	public int supply;
	public int current;
	public int rate;
	public int workers;
	public bool replaced;
	
	public Resource(ResType nam, int sup, int cur, int rat, int work, bool rep){
		name = nam;
		supply = sup;
		current = cur;
		rate = rat;
		workers = work;
		replaced = rep;
	}
}

[System.Serializable]
public class Upgrade{
	
	public enum UpgType{
		newWorker,
		newLevel
	}
	
	public UpgType name;
	public List<int> costList = new List<int>();
	public int effectMagnitude;
	public int iteration;
	
	public Upgrade(UpgType nam, int wc, int sc, int ic, int gc, int bc, int em, int it){
		name = nam;
		costList.Add(wc);
		costList.Add(sc);
		costList.Add(ic);
		costList.Add(gc);
		costList.Add(bc);
		effectMagnitude = em;
		iteration = it;
	}
}

