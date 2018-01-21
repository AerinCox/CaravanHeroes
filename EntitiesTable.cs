using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This keeps pointers to all NPCs/Enemies on map
public class EntitiesTable : MonoBehaviour {
	
	List<CharacterAttributes> enemies;
	List<CharacterAttributes> players;
	public GameObject p1; //temporary
	public GameObject p2; //temporary
	
	void Start(){
		this.enemies = new List<CharacterAttributes>();
		this.players = new List<CharacterAttributes>();
		
		//for now, just 2 fixed characters on the map
		enemies.Add(p1.GetComponent<CharacterAttributes>());
		players.Add(p2.GetComponent<CharacterAttributes>());
		
	}
	
	public void addEnemy(CharacterAttributes enemy){
		enemies.Add(enemy);
	}
	
	public void addPlayer (CharacterAttributes player){
		players.Add(player);
	}
	
}
