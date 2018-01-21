using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	//Very basic AI right now. Just moves towards nearest player, then attacks if close enough.
	
	private TileSpawn mapPointer; 
	private TurnSystem turn;
	
	void Start(){
		this.mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		this.turn = GameObject.Find("MapCode").GetComponent<TurnSystem>();
	}
	void Update(){
		if(!turn.isPlayerTurn()){
			//Move toward character and attack... not implemented
		}
		
	}
	
	
}
