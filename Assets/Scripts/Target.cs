using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public enum Type {Unknown, Tile, UI, Player, Enemy};
	
	public Type type;

	public Target(){
		this.type = Type.Unknown;
	}

	public void setType(Type newType){
		this.type = newType;
	}
	public Type getType(){
		return this.type;
	}
}
