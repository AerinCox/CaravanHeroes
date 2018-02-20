using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public AudioClip swordAttack;
	public AudioClip bowAttack;
	AudioSource audioSource;
	
	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public void playSwordAttack(){
		audioSource.PlayOneShot(swordAttack, 0.2F);
	}
	public void playBowAttack(){
		audioSource.PlayOneShot(bowAttack, 0.1F);
	}
}
