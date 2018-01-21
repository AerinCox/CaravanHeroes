using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{

	public bool filled; // does the tile exist? 
	public GameObject self; 
	public bool occupied; //is there something on me?
	
	public Tile(){
		this.self = null;
		this.filled = false;
		this.occupied = false;
	}
	
	public Tile(Vector3 position){
		this.self = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Renderer rend = self.GetComponent<Renderer>();
		rend.material = Resources.Load("Textures/BasicTile") as Material;
		this.self.AddComponent<Target>();
		self.GetComponent<Target>().setType(Target.Type.UI);
		self.transform.position = position;
		this.filled = true;
	}
}
