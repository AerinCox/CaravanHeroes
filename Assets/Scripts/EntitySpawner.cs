using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generates players, enemies, and items on map. Contains a list of all players and enemies on map.
public class EntitySpawner : MonoBehaviour {

	TileSpawn mapPointer; 
	List<CharacterAttributes> enemies;
	List<CharacterAttributes> players;
	TurnSystem turnSystem;

	void Start () {
		//Temporary code for spawning things into the map! Will be removed once we have a save system going.
		//Major issue right now: All the sprites are not the same size so you have to individually size them.. i need to not be lazy and just fix this issue with our assets..
		this.mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		turnSystem = GameObject.Find("MapCode").GetComponent<TurnSystem>();
		this.enemies = new List<CharacterAttributes>();
		this.players = new List<CharacterAttributes>();	

		
		//swordman
		GameObject g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Swordman";
		CharacterAttributes p = g.GetComponent<CharacterAttributes> ();
		Point location = new Point (1, 1);
		p.setAttributes(2, 4, 4, 4, 1, location, g, CharacterAttributes.Job.Swordman);
		p.setSprite (Resources.Load("Sprites/swordmanIdle") as Sprite);
		g.transform.position = new Vector3 (.7f, 1.5f, .6f);
		g.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/swordmanIdle");
		g.GetComponent<Target> ().setType (Target.Type.Player);
		g.AddComponent<Animator>();
		g.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/swordman") as RuntimeAnimatorController;
		this.players.Add (p);
		
		//Archer -.1, -.44
		g = null;
		g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Swordman";
		p = g.GetComponent<CharacterAttributes> ();
		location = new Point (2, 1);
		p.setAttributes(1, 1, 4, 3, 3, location, g, CharacterAttributes.Job.Archer);
		p.setSprite (Resources.Load("Sprites/archerIdle") as Sprite);
		g.transform.position = new Vector3 (1.90f, 1.5f, 0.66f);
		g.transform.localScale = new Vector3(0.56f, 0.5f, 0.5f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/archerIdle");
		g.GetComponent<Target> ().setType (Target.Type.Player);
		GameObject shadow = g.transform.Find ("shadow").gameObject;
		shadow.transform.localPosition = new Vector3 (-.35f,-1.35f,.96f);
		shadow.transform.localScale = new Vector3 (.5f, .5f, .5f);
		this.players.Add (p);
		
		//Cleric -.09, -.37
		g = null;
		g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Cleric";
		p = g.GetComponent<CharacterAttributes> ();
		location = new Point (3, 1);
		p.setAttributes(1, 1, 2, 2, 1, location, g, CharacterAttributes.Job.Cleric);
		p.setSprite (Resources.Load("Sprites/clericIdle") as Sprite);
		g.transform.position = new Vector3 (2.91f, 1.5f, 0.63f);
		g.transform.localScale = new Vector3(0.33f, 0.32f, 0.32f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/clericIdle");
		g.GetComponent<Target> ().setType (Target.Type.Player);
		shadow = g.transform.Find ("shadow").gameObject;
		shadow.transform.localPosition = new Vector3 (-.48f,-2.44f,1.22f);
		shadow.transform.localScale = new Vector3 (.6f, .6f, .6f);
		this.players.Add (p);
		
		//Skele Warrior
		g = null;
		g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Skele";
		Destroy(g.GetComponent<BoxCollider>());
		p = g.GetComponent<CharacterAttributes> ();
		location = new Point (4, 4);
		p.setAttributes(2, 2, 1, 3, 1, location, g, CharacterAttributes.Job.SkeleSwordman);
		p.setSprite (Resources.Load<Sprite> ("Sprites/skeleWarriorIdle"));
		g.transform.position = new Vector3 (3.48f, 1.71f, 3.72f);
		g.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/skeleWarriorIdle");
		g.GetComponent<Target> ().setType (Target.Type.Enemy);
		shadow = g.transform.Find ("shadow").gameObject;
		shadow.transform.localPosition = new Vector3 (.95f,-5.37f,.26f);
		shadow.transform.localScale = new Vector3 (1, 1, 1);
		this.enemies.Add (p);
		
		//Slime
		g = null;
		g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Slime";
		Destroy(g.GetComponent<BoxCollider>());
		p = g.GetComponent<CharacterAttributes> ();
		location = new Point (2, 4);
		p.setAttributes(3, 1, 1, 2, 1, location, g, CharacterAttributes.Job.Slime);
		p.setSprite (Resources.Load<Sprite> ("Sprites/slimeIdle"));
		g.transform.position = new Vector3 (1.59f, 1.34f, 3.29f);
		g.transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/slimeIdle");
		g.GetComponent<Target> ().setType (Target.Type.Enemy);
		shadow = g.transform.Find ("shadow").gameObject;
		Destroy(shadow);
		this.enemies.Add (p);
	
		//Ghoul
		g = null;
		g = Instantiate(GameObject.Find("CharacterBase")) as GameObject;
		g.SetActive (true);
		g.name = "Ghoul";
		Destroy(g.GetComponent<BoxCollider>());
		p = g.GetComponent<CharacterAttributes> ();
		location = new Point (3, 4);
		p.setAttributes(1, 1, 3, 1, 1, location, g, CharacterAttributes.Job.Ghoul);
		p.setSprite (Resources.Load<Sprite> ("Sprites/ghoul"));
		g.transform.position = new Vector3 (2.5f, 1.5f, 3.5f);
		g.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		g.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/ghoulIdle");
		g.GetComponent<Target> ().setType (Target.Type.Enemy);
		shadow = g.transform.Find ("shadow").gameObject;
		shadow.transform.localPosition = new Vector3 (0f,-5.5f,0f);
		shadow.transform.localScale = new Vector3 (1, 1, 1);
		this.enemies.Add (p);
		
		this.turnSystem.setPlayerCount(this.players.Count);
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
