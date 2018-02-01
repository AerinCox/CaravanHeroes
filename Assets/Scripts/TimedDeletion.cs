using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeletion : MonoBehaviour {

	float timeLeft;
	
	// Use this for initialization
	void Start () {
		timeLeft = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0){
			Destroy(gameObject);
		}
	}
}
