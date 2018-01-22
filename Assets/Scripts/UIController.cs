using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This class controls the player's UI interactions and movement

public class UIController : MonoBehaviour {

	bool isSelecting;
	GameObject npc;
	TileSpawn mapPointer; 
	//private Tile[,] map;
	HashSet<Point> highlighted; //if movement is selected, these are the tiles highlighted
	TurnSystem turn;

	void Start () {
		isSelecting = false;
		mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		//this.map = mapPointer.getMap();
		Point.pointEqualityComparer comparer = new Point.pointEqualityComparer();
		highlighted = new HashSet<Point>(comparer);
		turn = GameObject.Find("MapCode").GetComponent<TurnSystem>();
	}
	
	//Highlights area on the board that the player may move.
	void Highlight(int movement, Point position){
		if(movement != -1 && mapPointer.getTile(position) != null && mapPointer.getTile(position).filled)
		{
			highlighted.Add(position);
			GameObject temp = mapPointer.getTile(position).self;
			Renderer rend = temp.GetComponent<Renderer>();
			rend.material.color = Color.red;
			movement--;
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
				if (Input.GetTouch(i).phase == TouchPhase.Began){ */
				if (Input.GetMouseButtonDown (0)){
					RaycastHit rayTarget;
					Vector3 fwd = transform.TransformDirection(Vector3.forward);
					bool hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(/*Input.GetTouch(i).position*/Input.mousePosition), fwd, out rayTarget);
					 
					 //Selecting and deselecting
					if(hit && rayTarget.collider != null){
						 GameObject target = rayTarget.collider.gameObject;
						 Renderer rend = target.GetComponent<Renderer>();
						 Target type = target.GetComponent<Target>();
						 //Selecting player target
						 if(type.getType() == Target.Type.Player){
							 isSelecting = true;
							 npc = target;
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
								if(highlighted.Contains(position2d) && !mapPointer.getTile(position2d).occupied){
									//Setting current tile to unoccupied, setting new position tile to occupied
									CharacterAttributes npcInfo = npc.GetComponent<CharacterAttributes>();
									mapPointer.getTile(npcInfo.getLocation()).occupied = false;
									npcInfo.setLocation(position2d);
									mapPointer.getTile(position2d).occupied = true;
									//Moving character
									position += new Vector3(-0.3f,1.5f,-0.4f);
									npc.transform.position = position;
									//Unhighlighting map
									UnHighlight();
									highlighted.Clear();
									turn.changeTurn();
								}
								else{
									print("nope");
								}
							 }
						 }
					}
					//Deselect if click off map
					else{
						print("deselected");
						npc = null;
						isSelecting = false;
						UnHighlight();
						highlighted.Clear();
					}
				}
				i++;
			}
		/*}*/
	}
}
