using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {

	bool playerTurn; //If true, it is player's turn. If false, it is enemy turn.
	
	void Start(){
		this.playerTurn = true;
	}
	
	public void setTurn(bool b){
		this.playerTurn = b;
	}
	
	public void changeTurn(){
		playerTurn = !playerTurn;
	}
	
	public bool isPlayerTurn(){
		return this.playerTurn;
	}
}
