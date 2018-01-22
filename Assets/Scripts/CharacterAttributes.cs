using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

	int hp;
	int str;
	int def;
	int spd; //for each point, a character can move one space. For example, 1 spd = 1 tile
	Point location;
	GameObject self;

	public CharacterAttributes(){
		this.str = -1;
		this.def = -1;
		this.hp = -1;
		this.spd = -1;
		this.location = new Point(-1,-1);
		this.self = null;
	}

	public CharacterAttributes(int str, int def, int spd, int hp, GameObject g){
		this.str = str;
		this.def = def;
		this.spd = spd;
		this.hp = hp;
		this.self = g;
	}
	public CharacterAttributes(int str, int def, int spd, int hp, Point location, GameObject g){
		this.str = str;
		this.def = def;
		this.hp = hp;
		this.spd = spd;
		this.location = location;
		this.self = g;
	}
	public void setAttributes(int str, int def, int spd, int hp, Point location, GameObject g){
		this.str = str;
		this.def = def;
		this.hp = hp;
		this.spd = spd;
		this.location = location;
		this.self = g;
	}

	public void setSprite(Sprite sprite){
		self.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	public Sprite getSprite(){
		return self.GetComponent<SpriteRenderer> ().sprite;
	}

	public void setSpd(int spd){
		this.spd = spd;
	}
	public void setStr(int str){
		this.str = str;
	}
	public void setDef(int def){
		this.def = def;
	}
	public void setLocation(int x, int y){
		this.location = new Point(x,y);
	}
	public void setLocation(Point p){
		this.location = new Point(p.x, p.y); //eventually change so that it transforms the position of the entity too
	}
	public void printLocation(){
		//print("X: " + location.x + " Y: " + location.y);
	}
	public int getSpd(){
		return this.spd;
	}
	public Point getLocation(){
		return this.location;
	}
}
