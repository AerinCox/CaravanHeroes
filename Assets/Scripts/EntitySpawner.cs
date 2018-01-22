using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generates players, enemies, and items on map. Contains a list of all players and enemies on map.
public class EntitySpawner : MonoBehaviour {

	TileSpawn mapPointer; 
	List<CharacterAttributes> enemies;
	List<CharacterAttributes> players;

	void Start () {
		//Temporary code for spawning things into the map! Will be removed once we have a save system going.

		this.mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		this.enemies = new List<CharacterAttributes>();
		this.players = new List<CharacterAttributes>();	

		GameObject g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Swordman";
		CharacterAttributes p = g.GetComponent<CharacterAttributes> ();
		Point location = new Point (1, 1);
		p.setAttributes(1, 1, 1, 1, location, g);
		p.setSprite (Resources.Load("Sprites/swordman") as Sprite);
		g.transform.position = new Vector3 (.7f, 1.5f, .6f);
		g.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/swordman");
		g.GetComponent<Target> ().setType (Target.Type.Player);
		this.players.Add (p);

		g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Ghoul";
		p = g.GetComponent<CharacterAttributes> ();
		g.AddComponent<EnemyAI> ();
		location = new Point (3, 3);
		p.setAttributes(1, 1, 1, 1, location, g);
		p.setSprite (Resources.Load<Sprite> ("Sprites/ghoul"));
		g.transform.position = new Vector3 (2.5f, 1.5f, 2.5f);
		g.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/ghoul");
		g.GetComponent<Target> ().setType (Target.Type.Enemy);
		GameObject shadow = g.transform.Find ("shadow").gameObject;
		shadow.transform.localPosition = new Vector3 (0f,-5.5f,0f);
		shadow.transform.localScale = new Vector3 (1, 1, 1);
		this.enemies.Add (p);
	}
		
	public void addEnemy(CharacterAttributes enemy){
		enemies.Add(enemy);
	}

	public void addPlayer (CharacterAttributes player){
		players.Add(player);
	}

	public List<CharacterAttributes> getPlayers(){
		return this.players;
	}

	public List<CharacterAttributes> getEnemies(){
		return this.enemies;
	}
		

	public void Spawner(CharacterAttributes p1, CharacterAttributes p2, CharacterAttributes p3){
		//For now, player starting area is (1,1)(2,1)(3,1)

	}
}
