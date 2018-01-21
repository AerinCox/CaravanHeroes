using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public enum Type {UI, NPC};
	
	public Type type;
	
	public void setType(Type newType){
		this.type = newType;
	}
	public Type getType(){
		return this.type;
	}
}
