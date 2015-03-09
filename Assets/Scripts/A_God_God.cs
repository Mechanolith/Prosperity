using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_God_God : MonoBehaviour {
	public float startTime, buildTime, disappointTime, warnTime, timer, remarkChance;

	private List<string> positiveLines = new List<string>();
	private List<string> disappointLines = new List<string>();
	private List<string> angerLines = new List<string>();
	private List<string> cultLines = new List<string>();
	private TextMesh godText;
	private float fadeTimer;
	private bool messageSent = false;

	void Start () {
		positiveLines.Add("Ah, you truly are the greatest of all my creations!");
		positiveLines.Add("Amazing work! Keep going!");
		positiveLines.Add("You surpass my every expectation!");
		positiveLines.Add("You are without doubt, among the greatest of your species!");

		disappointLines.Add("Perhaps you should consider building more...");
		disappointLines.Add("You're trying my patience here, mortal...");
		disappointLines.Add("I suggest you pick up the pace...");
		disappointLines.Add("You don't have an eternity to finish this, you know?");
		
		angerLines.Add ("Get back to work!");
		angerLines.Add ("Hurry up!");
		angerLines.Add ("Build! Before my patience runs out!");
		angerLines.Add ("Your lack of results infuriates me, mortal!");

		cultLines.Add("YES! BUILD HIGHER!");
		cultLines.Add("GIVE IT ALL TO THE TOWER!");
		cultLines.Add("MORE! MORE! MORE!");
		cultLines.Add("NEVER STOP! NEVER FALTER!");

		timer = startTime;

		godText = GameObject.Find("I_God_Text").GetComponent<TextMesh>();
	}

	void Update () {
		float tempNum;
		int rand;

		timer -= Time.deltaTime;
		fadeTimer -= Time.deltaTime;

		if(timer < disappointTime && timer > disappointTime - 1){
			if(!messageSent){
				rand = Mathf.RoundToInt(Random.Range(0,disappointLines.Count));
				godText.text = disappointLines[rand];
				StartTextFade();
				messageSent = true;
			}
		}
		else if(timer < warnTime && timer > warnTime - 1){
			if(!messageSent){
				rand = Mathf.RoundToInt(Random.Range(0,angerLines.Count));
				godText.text = angerLines[rand];
				StartTextFade();
				messageSent = true;
			}
		}
		else if(timer <= 0){
			if(!messageSent){
				godText.text = "I'M THROUGH WITH YOU, MORTAL!";
				StartTextFade();
				print ("YOU LOSE");
				messageSent = true;
			}
		}
		else{
			messageSent = false;
		}

		if(fadeTimer <= 0){
			tempNum = godText.color.a - Time.deltaTime;
			godText.color = new Color(godText.color.r, godText.color.g, godText.color.b, tempNum);
		}
	}

	void StartTextFade(){
		godText.color = Color.black;
		fadeTimer = 5;
	}

	public void GiveEncouragement(){
		float chance;
		int rand;

		chance = Random.Range(0,100);

		if(chance < remarkChance){
			rand = Mathf.RoundToInt(Random.Range(0,positiveLines.Count));
			godText.text = positiveLines[rand];
			StartTextFade();
		}
	}
}
