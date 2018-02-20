using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {

	bool playerTurn; //If true, it is player's turn. If false, it is enemy turn.
	int playerCount; //amt of player's npcs on map.. aka amt of actions the player may make
	int actionCounter; //amt of actions that has been played. Must be <= playerCount
	
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
	public void Action(CharacterAttributes character){
		this.actionCounter++;
		if(actionCounter == playerCount){
			changeTurn();
			actionCounter = 0;
		}
		character.setSelectable(false);
	}
	public void setPlayerCount(int playerCount){
		this.playerCount = playerCount;
	}
}
