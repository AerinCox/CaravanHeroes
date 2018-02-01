using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

	int hp;
	int str; //for every 1 point of str, +1 damage
	int atkRange;
	int def; //for every 2 points of def, -1 damage.
	int spd; //for each point, a character can move one space. For example, 1 spd = 1 tile
	Point location;
	GameObject self;
	public bool isDead;

	public CharacterAttributes(){
		this.str = -1;
		this.def = -1;
		this.hp = -1;
		this.spd = -1;
		this.atkRange = -1;
		this.location = new Point(-1,-1);
		this.self = null;
		this.isDead = false;
	}

	public CharacterAttributes(int str, int def, int spd, int hp, int atkRange, GameObject g){
		this.str = str;
		this.def = def;
		this.spd = spd;
		this.hp = hp;
		this.self = g;
		this.isDead = false;
		this.atkRange = atkRange;
	}
	public CharacterAttributes(int str, int def, int spd, int hp, int atkRange, Point location, GameObject g){
		this.str = str;
		this.def = def;
		this.hp = hp;
		this.spd = spd;
		this.location = location;
		this.self = g;
		this.isDead = false;
		this.atkRange = atkRange;
	}
	public void setAttributes(int str, int def, int spd, int hp, int atkRange, Point location, GameObject g){
		this.str = str;
		this.def = def;
		this.hp = hp;
		this.spd = spd;
		this.location = location;
		this.self = g;
		this.atkRange = atkRange;
	}

	public void setSprite(Sprite sprite){
		self.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	public Sprite getSprite(){
		return self.GetComponent<SpriteRenderer> ().sprite;
	}

	public void setAtkRange(int atkRange){
		this.atkRange = atkRange;
	}
	public int getAtkRange(){
		return this.atkRange;
	}
	
	public void setSpd(int spd){
		this.spd = spd;
	}
	public void setStr(int str){
		this.str = str;
	}
	public int getStr(){
		return this.str;
	}
	public int getDef(){
		return this.def;
	}
	public void setDef(int def){
		this.def = def;
	}
	public GameObject getGameObject(){
		return this.self;
	}
	public void changeHp(int change){
		this.hp += change;
		if(this.hp <= 0){
			this.hp = 0;
			this.isDead = true;
			this.self.GetComponent<Renderer>().material.color = new Color(1f,1f,1f,.5f);
		}
	}
	public void setLocation(int x, int y){
		this.location = new Point(x,y);
	}
	public void setLocation(Point p){
		this.location = new Point(p.x, p.y); //eventually change so that it transforms the position of the entity too
	}
	public string locationToString(){
		return ("X: " + location.x + " Y: " + location.y);
	}
	public int getSpd(){
		return this.spd;
	}
	public Point getLocation(){
		return this.location;
	}
}
