using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

	int str;
	int def;
	public int spd; //for each point, a character can move one space. For example, 1 spd = 1 tile
	Point location;
	public int x; //temporary for filling in positions.. will be generated eventually
	public int y;
	
	
	void Start(){
		this.str = 0;
		this.def = 0;
		this.location = new Point(this.x,this.y);
	}
	public CharacterAttributes(){
		this.str = 0;
		this.def = 0;
		this.location = new Point(this.x,this.y);
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
		this.location = new Point(p.x, p.y);
	}
	public void printLocation(){
		print("X: " + location.x + " Y: " + location.y);
	}
	public int getSpd(){
		return this.spd;
	}
	public Point getLocation(){
		return this.location;
	}
}
