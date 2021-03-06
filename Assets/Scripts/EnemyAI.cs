﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	//Very basic AI right now. Just moves towards nearest player, then attacks if close enough.
	
	private TileSpawn mapPointer; 
	private TurnSystem turn;
	private EntitySpawner entitiesTable;
	private CharacterAttributes self;
	
	void Start(){
		this.mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		this.turn = GameObject.Find("MapCode").GetComponent<TurnSystem>();
		this.entitiesTable = GameObject.Find("MapCode").GetComponent<EntitySpawner>(); 
		this.self = null;
	}
	
	void ResetNPCs(){
		foreach(CharacterAttributes character in entitiesTable.getPlayers()){
			if(character.isDead == true){
				turn.Action(character);
			}
			else{
				character.setSelectable(true);
			}
		}
	}
	
	void Update(){
		bool cont = false;
		if(!turn.isPlayerTurn()){
			foreach(CharacterAttributes enemy in entitiesTable.getEnemies()){
				this.self = enemy;
				if(!self.isDead){
					//Find closest player
					int myAtkRange = self.getAtkRange();
					int mySpd = self.getSpd();
					Point myLocation = self.getLocation();
					CharacterAttributes closest = null;
					int closestDistance = 1000; //...some arbitrary large distance
					foreach (CharacterAttributes character in entitiesTable.getPlayers()) {
						int curr = character.getLocation().getDifference (myLocation);
						if (curr < closestDistance && !character.isDead) {
							closestDistance = curr;
							closest = character;
						}
						//If am close enough to attack, I attack.
						if (myAtkRange >= closestDistance) {
							AttackSystem.Attack(this.self, closest);
							cont = true;
						}
					}
					if(cont == true){
						cont = false;
						continue;
					}
					//Moving if not close enough to attack.
					//Calculating movement
					int xMovement = 0;
					int yMovement = 0;
					Point targetLocation = closest.getLocation();
					//Moves with a preference to X axis
					if (targetLocation.x > myLocation.x && mySpd > 0) {
						for (int i = 0; i < targetLocation.x - myLocation.x && mySpd > 0; i++) {
							mySpd--;
							xMovement++;
						}
					}
					else if (targetLocation.x < myLocation.x && mySpd > 0) {
						for (int i = 0; i < myLocation.x - targetLocation.x && mySpd > 0; i++) {
							mySpd--;
							xMovement--;
						}
					}
					if (targetLocation.y > myLocation.y && mySpd > 0) {
						for (int i = 0; i < targetLocation.y - myLocation.y && mySpd > 0; i++) {
							mySpd--;
							yMovement++;
						}
					}
					else if (targetLocation.y < myLocation.y && mySpd > 0) {
						for (int i = 0; i < myLocation.y - targetLocation.y && mySpd > 0; i++) {
							mySpd--;
							yMovement--;
						}
					}
					Point newLocation = new Point (myLocation.x + xMovement, myLocation.y + yMovement);
					// does a "y axis" preferred movement instead if x preferred movement did not work
					if (mapPointer.getTile (newLocation).occupied == true || mapPointer.getTile (newLocation).filled == false) {
						mySpd = self.getSpd();
						xMovement = 0;
						yMovement = 0;
						if (targetLocation.y > myLocation.y && mySpd > 0) {
							for (int i = 0; i < targetLocation.y - myLocation.y && mySpd > 0; i++) {
								mySpd--;
								yMovement++;
							}
						}
						else if (targetLocation.y < myLocation.y && mySpd > 0) {
							for (int i = 0; i < myLocation.y - targetLocation.y && mySpd > 0; i++) {
								mySpd--;
								yMovement--;
							}
						}
						if (targetLocation.x > myLocation.x && mySpd > 0) {
							for (int i = 0; i < targetLocation.x - myLocation.x && mySpd > 0; i++) {
								mySpd--;
								xMovement++;
							}
						}
						else if (targetLocation.x < myLocation.x && mySpd > 0) {
							for (int i = 0; i < myLocation.x - targetLocation.x && mySpd > 0; i++) {
								mySpd--;
								xMovement--;
							}
						}
						newLocation = new Point (myLocation.x + xMovement, myLocation.y + yMovement);
					}
					newLocation = new Point (myLocation.x + xMovement, myLocation.y + yMovement);
					//Last resort if enemy could not go on tile of choice. 
					while (mapPointer.getTile (newLocation).occupied == true || mapPointer.getTile (newLocation).filled == false) {
						if (xMovement > 0) {
							xMovement--;
						} 
						else if (yMovement > 0) {
							yMovement--;
						} 
						else if (yMovement < 0) {
							yMovement++;
						} 
						else if (xMovement < 0) {
							xMovement++;
						}
						newLocation = new Point (myLocation.x + xMovement, myLocation.y + yMovement);

						//If enemy cannot find a place to move, it stays in place.
						if (xMovement == 0 && yMovement == 0) {
							//do nothing
							cont = true;
							break;
						}
					}
					if(cont == true){
						cont = false;
						continue;
					}
					//Note: there is one case not yet coded for. If character is parallel to enemy, and there is empty tile between them
					// and an empty tile in the direction that the player moves (upwards or downwards), the enemy stays in place.

					//Moving the enemy..
					mapPointer.getTile (newLocation).occupied = true;
					mapPointer.getTile (myLocation).occupied = false;
					self.setLocation (newLocation);
					if(self.getJob() == CharacterAttributes.Job.SkeleSwordman){
						self.getGameObject().transform.position = new Vector3 (newLocation.x - .52f, 1.71f, newLocation.y - 0.28f);
					}
					else if(self.getJob() == CharacterAttributes.Job.Ghoul){
						self.getGameObject().transform.position = new Vector3 (newLocation.x - 0.5f, 1.5f, newLocation.y - 0.5f);
					}
					else if(self.getJob() == CharacterAttributes.Job.Slime){
						self.getGameObject().transform.position = new Vector3 (newLocation.x - 0.41f, 1.34f, newLocation.y - 0.71f);
					}
				}
			}
			turn.changeTurn ();
			ResetNPCs();
		}
	}
}
