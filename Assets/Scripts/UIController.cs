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
	public Button moveButton;
	public Button attackButton;
	public Button skipButton;
	bool isMoving; //is the player currently in the move action?
	bool isAttacking; //is player attacking
	bool isSkipping; // is player skipping

	void Start () {
		this.isMoving = false;
		this.isSelecting = false;
		this.isAttacking = false;
		this.isSkipping = false;
		mapPointer = GameObject.Find("MapCode").GetComponent<TileSpawn>();
		//this.map = mapPointer.getMap();
		Point.pointEqualityComparer comparer = new Point.pointEqualityComparer();
		highlighted = new HashSet<Point>(comparer);
		turn = GameObject.Find("MapCode").GetComponent<TurnSystem>();
		moveButton.onClick.AddListener(MoveAction);
		skipButton.onClick.AddListener(SkipAction);
	}
	
	//If move button is pressed..
	void MoveAction(){
		//you can press move button again if you don't want to move anymore.
		if(isMoving == true){
			isMoving = false;
			UnHighlight();
			highlighted.Clear();
			this.moveButton.GetComponent<Image>().color = Color.white;
		}
		//Checking if person clicked two buttons at once (dont know if this can actually happen but..)
		else if(isAttacking == true){ 
			isAttacking = false;
		}
		//Shows the spaces the player can move, and enables moving.
		else if(isSelecting){
			CharacterAttributes charInfo = npc.GetComponent<CharacterAttributes>(); 
			Highlight(charInfo.getSpd(), charInfo.getLocation(), true);
			this.isMoving = true;
			this.moveButton.GetComponent<Image>().color = new Color(1f,.5f,.5f,1);
		}
	}
	
	void SkipAction(){
		if(isAttacking == true || isMoving == true){ 
			isAttacking = false;
			isMoving = false;
		}
		//Making sure nothing is highlighted
		UnHighlight();
		PlayerUnhighlight(this.npc);
		//Making buttons go away and setting flag for skipping turn to be true
		this.buttons.SetActive(false);
		this.turn.changeTurn();
	}
	
	//Highlights current target and the tile its on
	void PlayerHighlight(GameObject player){
		CharacterAttributes charInfo = player.GetComponent<CharacterAttributes>(); 
		Tile playerTile = mapPointer.getTile(charInfo.getLocation());
		Renderer rend = playerTile.self.GetComponent<Renderer>();
		rend.material.color = new Color(1.3f,1.3f,1.3f,1);
		rend = player.GetComponent<Renderer>();
		rend.material.color = new Color(1.1f,1.1f,1.1f,1);
	}
	
	//UnHighlights current target and the tile its on
	void PlayerUnhighlight(GameObject player){
		CharacterAttributes charInfo = player.GetComponent<CharacterAttributes>(); 
		Tile playerTile = mapPointer.getTile(charInfo.getLocation());
		Renderer rend = playerTile.self.GetComponent<Renderer>();
		rend.material.color = Color.white;
		rend = player.GetComponent<Renderer>();
		rend.material.color = Color.white;
	}
	
	//Highlights area on the board that the player may move.
	void Highlight(int movement, Point position, bool start){
		//If we're on the player's space, don't highlight it.
		if(start == true){
			Highlight(movement, new Point(position.x + 1, position.y), false);
			Highlight(movement, new Point(position.x - 1, position.y), false);
			Highlight(movement, new Point(position.x, position.y + 1), false);
			Highlight(movement, new Point(position.x, position.y - 1), false);
		}
		//Otherwise, recursively highlight area depending on player's speed.
		else if(movement != 0 && mapPointer.getTile(position) != null && mapPointer.getTile(position).filled && !mapPointer.getTile(position).occupied)
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
		this.highlighted.Clear();
	}
	
	void Update () {	
		if(turn.isPlayerTurn()){
			int i = 0;
			/* while (i < Input.touchCount) {
				if (Input.GetTouch(i).phase == TouchPhase.Began){ */
				if (Input.GetMouseButtonDown (0) && turn.isPlayerTurn()){
					RaycastHit rayTarget;
					Vector3 fwd = transform.TransformDirection(Vector3.forward);
					bool hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(/*Input.GetTouch(i).position*/Input.mousePosition), fwd, out rayTarget);
					 
					//Human interaction input
					if(hit && rayTarget.collider != null){
						 GameObject target = rayTarget.collider.gameObject;
						 Target type = target.GetComponent<Target>();
						 
						 //Selecting player 
						 if(type.getType() == Target.Type.Player){
							 this.isSelecting = true;
							 this.buttons.SetActive(true);
							 this.npc = target;
							 PlayerHighlight(this.npc);
						 }
						 
						 //Selecting tiles. Used for attacking or moving
						 if(type.getType() == Target.Type.Tile){
							if(this.isMoving){
								Vector3 position = target.transform.position;
								Point position2d = new Point((int)position.x, (int)position.z);
								if(highlighted.Contains(position2d) && !mapPointer.getTile(position2d).occupied){
									//Setting current tile to unoccupied, setting new position tile to occupied
									CharacterAttributes npcInfo = npc.GetComponent<CharacterAttributes>();
									Tile currentTile = mapPointer.getTile(npcInfo.getLocation());
									currentTile.occupied = false;
									currentTile.self.GetComponent<Renderer>().material.color = Color.white;
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
									this.moveButton.GetComponent<Image>().color = Color.white;
									this.npc = null;
								}
							}
							
							//Selecting the mob you want to attack (must press attack button first)
							if(this.isAttacking){
								//implement
							}
						 }
						 if(type.getType() == Target.Type.UI){
							 //No use for this right now lol
						 }
						 
						 //deselecting (clicked on void area outside tiles)
						 if(type.getType() == Target.Type.Unknown && npc != null){
							PlayerUnhighlight(this.npc);
							UnHighlight();
							this.npc = null;
							this.isSelecting = false;
							this.buttons.SetActive(false);
							//making sure none of the buttons have their previous states
							this.isMoving = false;
							this.moveButton.GetComponent<Image>().color = Color.white;
						 }
					}
					//this shouldn't happen.. void area has a collider to detect off map clicks
					else{
						print("colliders not catching full area");
					}
				}
				i++;
			}
		/*}*/
	}
}
