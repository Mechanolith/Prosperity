using UnityEngine;
using System.Collections;

public class I_Resource_Collect_Text : MonoBehaviour {
	public float fadeTime, fadeScale;
	private float timer, tempNum;
	private TextMesh thisText;

	void Start () {
		timer = fadeTime;
		thisText = gameObject.GetComponent<TextMesh>();
		tempNum = thisText.color.a;
	}

	void Update () {
		timer -= Time.deltaTime;

		if(timer <= 0){
			tempNum -= Time.deltaTime * fadeScale;
			thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, tempNum);
		}
		
		if (timer <= -5){
			Destroy(gameObject);
		}
	}
}
