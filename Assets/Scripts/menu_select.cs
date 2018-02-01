using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_select : MonoBehaviour {
	int selection, moving, time;
	int speed = 3; // Rotation Speed
	float angle;

	bool tap, isDragging;
	Vector2 startTouch, swipeDelta;
		
	void Start () {
		selection = moving = time = 0;

		// Position Menu Items
		for (int i = 0; i < transform.childCount; i++) {
			float theta = (2 * Mathf.PI / transform.childCount) * i;
			transform.GetChild(i).position = new Vector3(Mathf.Sin(theta) * 3, 0, Mathf.Cos(theta) * -3);
		}

		angle = 360 / transform.childCount;
	}

	void Update () {
		if (selection < 0){selection = transform.childCount - 1;}
		else if (selection > transform.childCount - 1){selection = 0;}

		// Swiping Touch
		if (Input.touches.Length > 0) {
			if (Input.touches [0].phase == TouchPhase.Began) {
				isDragging = tap = true;
				startTouch = Input.touches [0].position;
			} else if (Input.touches [0].phase == TouchPhase.Ended || Input.touches [0].phase == TouchPhase.Canceled) {
				startTouch = swipeDelta = Vector2.zero;
				isDragging = false;
			}
		}

		// Swiping Get Distance
		swipeDelta = Vector2.zero;
		if (isDragging && Input.touches.Length > 0)
			swipeDelta = (Input.touches [0].position)- startTouch;

		//Input
		if (moving == 0) {
			if (swipeDelta.magnitude > 100) {
				if (Mathf.Abs (swipeDelta.x) > Mathf.Abs (swipeDelta.y)) {
					if (swipeDelta.x > 0) {moving = 1;}
					else {moving = -1;}
					selection += moving;
				}
			} else if (tap && !isDragging) {
				SceneManager.LoadScene (transform.GetChild(selection).name.Replace("Menu_", ""));
			}
		} else {
			transform.Rotate(Vector3.down * angle/(angle/speed) * moving);
			foreach (Transform child in transform) {
				child.Rotate(Vector3.up * angle/(angle/speed) * moving);
			}
			time++;
			if (time >= angle/speed) {
				tap = isDragging = false;
				moving = time = 0;
			}
		}
	}
}