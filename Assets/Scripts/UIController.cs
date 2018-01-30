using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//This class controls the player's UI interactions and movement. It is located on the Main Camera.

public class UIController : MonoBehaviour {

	bool isSelecting;
	GameObject npc; // currently selected target
	TileSpawn mapPointer; 
	//private Tile[,] map;
	HashSet<Point> highlighted; //if movement is selected, these are the tiles highlighted
	TurnSystem turn;
	public GameObject buttons;
	public Button move;
	public Button attack;
	public Button skip;
	bool isMoving; //is the player currently in the move action?
	bool isAttacking; //is player attacking
	bool isSkipping; // is player skipping

	void Start () {
		this.isMoving = false;
		this.isSelecting = false;
		mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		//this.map = mapPointer.getMap();
		Point.pointEqualityComparer comparer = new Point.pointEqualityComparer();
		highlighted = new HashSet<Point>(comparer);
		turn = GameObject.Find("MapCode").GetComponent<TurnSystem>();
		move.onClick.AddListener(MoveAction);
	}
	
	//If move button is pressed..
	void MoveAction(){
		//you can press move button again if you don't want to move anymore.
		if(isMoving == true){
			isMoving = false;
			UnHighlight();
			highlighted.Clear();
			this.move.GetComponent<Image>().color = Color.white;
		}
		else if(isAttacking == true || isSkipping == true){ //maybe if person clicked two buttons at once?
			isAttacking = false;
			isSkipping = false;
		}
		else if(isSelecting){
			CharacterAttributes charInfo = npc.GetComponent<CharacterAttributes>(); 
			Highlight(charInfo.getSpd(), charInfo.getLocation(), true);
			this.isMoving = true;
			this.move.GetComponent<Image>().color = new Color(1f,.5f,.5f,1);
		}
	}
	
	void PlayerHighlight(GameObject player){
		Renderer rend = player.GetComponent<Renderer>();
		rend.material.color = new Color(1.1f,1.1f,1.1f,1);
	}
	void PlayerUnhighlight(GameObject player){
		Renderer rend = player.GetComponent<Renderer>();
		rend.material.color = Color.white;
	}
	
	//Highlights area on the board that the player may move.
	void Highlight(int movement, Point position, bool start){
		//dont want to highlight the character's space
		if(start == true){
			Highlight(movement, new Point(position.x + 1, position.y), false);
			Highlight(movement, new Point(position.x - 1, position.y), false);
			Highlight(movement, new Point(position.x, position.y + 1), false);
			Highlight(movement, new Point(position.x, position.y - 1), false);
			return;
		}
		if(movement != 0 && mapPointer.getTile(position) != null && mapPointer.getTile(position).filled && !mapPointer.getTile(position).occupied)
		{
			highlighted.Add(position);
			GameObject temp = mapPointer.getTile(position).self;
			Renderer rend = temp.GetComponent<Renderer>();
			rend.material.color = Color.red;
			movement--;

			Highlight(movement, new Point(position.x + 1, position.y), false);
			Highlight(movement, new Point(position.x - 1, position.y), false);
			Highlight(movement, new Point(position.x, position.y + 1), false);
			Highlight(movement, new Point(position.x, position.y - 1), false);
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
							 this.isSelecting = true;
							 this.buttons.SetActive(true);
							 this.npc = target;
							 PlayerHighlight(this.npc);
						 }
						 //Used for attacking or moving
						 if(type.getType() == Target.Type.Tile){
							if(this.isMoving){
								Vector3 position = target.transform.position;
								Point position2d = new Point((int)position.x, (int)position.z);
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
									isSelecting = false;
									buttons.SetActive(false);
									this.isMoving = false;
									PlayerUnhighlight(this.npc);
									this.move.GetComponent<Image>().color = Color.white;
									this.npc = null;
								}
							}
							if(this.isAttacking){
								//implement
							}
						 }
						 if(type.getType() == Target.Type.UI){
							//do nothing
						 }
						 //deselecting
						 if(type.getType() == Target.Type.Unknown){
							PlayerUnhighlight(this.npc);
							this.npc = null;
							this.isSelecting = false;
							UnHighlight();
							this.highlighted.Clear();
							this.buttons.SetActive(false);
							//making sure none of the buttons have their previous states
							this.isMoving = false;
							this.move.GetComponent<Image>().color = Color.white;
						 }
					}
					//Deselect if click off map
					else{
						npc = null;
						//isSelecting = false;
						UnHighlight();
						highlighted.Clear();
						//buttons.SetActive(false);
					}
				}
				i++;
			}
		/*}*/
	}
}
