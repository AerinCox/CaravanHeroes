using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This class controls the player's UI interactions and movement

public class UIController : MonoBehaviour {

	private bool isSelecting;
	private GameObject npc;
	private TileSpawn mapPointer; 
	//private Tile[,] map;
	private HashSet<Point> highlighted; //if movement is selected, these are the tiles highlighted
	private TurnSystem turn;

	void Start () {
		this.isSelecting = false;
		this.mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		//this.map = mapPointer.getMap();
		Point.pointEqualityComparer comparer = new Point.pointEqualityComparer();
		this.highlighted = new HashSet<Point>(comparer);
		this.turn = GameObject.Find("MapCode").GetComponent<TurnSystem>();
	}
	
	//Highlights area on the board that the player may move.
	void Highlight(int movement, Point position){
		if(this.mapPointer.getTile(position) == null){
			return;
		}
		if(movement == -1 || highlighted.Contains(position) || !this.mapPointer.getTile(position).filled){
			return;
		}
		else{
			highlighted.Add(position);
			GameObject temp = mapPointer.getTile(position).self;
			Renderer rend = temp.GetComponent<Renderer>();
			rend.material.color = Color.red;
			movement = movement - 1;
			Highlight(movement, new Point(position.x + 1, position.y));
			Highlight(movement, new Point(position.x - 1, position.y));
			Highlight(movement, new Point(position.x, position.y + 1));
			Highlight(movement, new Point(position.x, position.y - 1));
		}
	}
	
	// Unhighlights the board.
	void UnHighlight(){
		foreach(Point p in highlighted){
			Tile t = mapPointer.getTile(p);
			Renderer rend = t.self.GetComponent<Renderer>();
			rend.material.color = Color.white;
		}
	}
	
	void Update () {	
		if(turn.isPlayerTurn()){
			int i = 0;
			/* while (i < Input.touchCount) {
				if (Input.GetTouch(i).phase == TouchPhase.Began){ */ if (Input.GetMouseButtonDown (0)){
					RaycastHit rayTarget;
					Vector3 fwd = transform.TransformDirection(Vector3.forward);
				bool hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(/*Input.GetTouch(i).position*/Input.mousePosition), fwd, out rayTarget);
					 
					 //Selecting and deselecting
					if(hit){
						 if(rayTarget.collider != null){
							 GameObject target = rayTarget.collider.gameObject;
							 Renderer rend = target.GetComponent<Renderer>();
							 Target type = target.GetComponent<Target>();
							 //Selecting player target
							 if(type.getType() == Target.Type.Player){
								 this.isSelecting = true;
								 this.npc = target;
								 CharacterAttributes charInfo = npc.GetComponent<CharacterAttributes>(); 
								 Highlight(charInfo.getSpd(), charInfo.getLocation());
							 }
							 //Selecting tiles
							 if(type.getType() == Target.Type.UI){
								 //Character movement, for now
								 if(isSelecting){
										 Vector3 position = target.transform.position;
										 Point position2d = new Point((int)position.x, (int)position.z);
								print (position2d.printPoint ());
								print (mapPointer.getTile (position2d).occupied);
										 if(this.highlighted.Contains(position2d) && !mapPointer.getTile(position2d).occupied){
											 //Setting current tile to unoccupied, setting new position tile to occupied
											 CharacterAttributes npcInfo = this.npc.GetComponent<CharacterAttributes>();
											 mapPointer.getTile(npcInfo.getLocation()).occupied = false;
											 npcInfo.setLocation(position2d);
											 mapPointer.getTile(position2d).occupied = true;
											 //Moving character
											 position = position + new Vector3(-0.3f,1.5f,-0.4f);
											 this.npc.transform.position = position;
											 //Unhighlighting map
											 UnHighlight();
											 this.highlighted.Clear();
											 turn.changeTurn();
											}
										else{
											print("nope");
										}
								 }
							 }
						 }
					}
					//Deselect if click off map
					 else{
						 print("deselected");
						 this.npc = null;
						 isSelecting = false;
						 UnHighlight();
						this.highlighted.Clear();
					 }
				}
				i++;
			}
		/*}*/
	}
}
