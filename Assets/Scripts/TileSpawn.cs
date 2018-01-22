using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generates the Map and initial player positions
public class TileSpawn : MonoBehaviour {
	
	Tile[,] map;
	int rectX, rectY;

	public Tile getTile(Point point){
		if(point.x >= map.GetLength(0) || point.x < 0 || point.y >= map.GetLength(1) || point.y < 0){
			return null;
		}
		return map[point.x, point.y];
	}
	
	public Tile[,] getMap(){
		return this.map;
	}
	
	public Point[] getPoints(int x1, int x2, int y1, int y2){
		Point rect1 = new Point(Random.Range(x1,x2), Random.Range(y1,y2));
		Point rect2 = new Point(Random.Range(x1,x2), Random.Range(y1,y2));
		// 1 should always be higher than 2
		if(rect1.x < rect2.x){
			return new Point[] {rect2, rect1};
		}
		return new Point[] {rect1, rect2};
	}
	
	public void connect(Point[] points1, int num1, Point[] points2, int num2, int option){
		int breaker = 0;
		int x = points1[num1].x;
		int y = points1[num1].y;
		if(points1[num1].x == points2[num2].x && points1[num1].y == points2[num2].y){
			if(!map[x,y].filled){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != points2[num2].x || y != points2[num2].y) && breaker != 15) {
				if(!map[x,y].filled){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				
				if (option == 0){
					if(points2[num2].x > x){x++;}
					if(points2[num2].y > y){y++;}
				}
				else if (option == 1){
					if(points1[num1].x > points2[num2].x){
						if(points2[num2].x > x){x--;}
					}
					else if(points2[num2].x > x){x++;}
					if(points2[num2].y > y){y++;}
				}
				else if (option == 2){
					if(points2[num2].x < x){x--;}
					if(points2[num2].y > y){y++;}
				}
				else if (option == 3){
					if(points2[num2].x < x){x--;}
					if(points1[num1].y > points2[num2].y){
						if(points2[num2].y < y){y--;}
					}
					else if(points2[num2].y > y){y++;}
				}
				else if (option == 4){
					if(points2[num2].x < x){x--;}
					if(points2[num2].y < y){y--;}
				}
				else if (option == 5){
					if(points1[num1].x > points2[num2].x){
						if(points2[num2].x < x){x--;}
					}
					else if(points2[num2].x < x){x++;}
					if(points2[num2].y < y){y--;}
				}
				else if (option == 6){
					if(points2[num2].x > x){x++;}
					if(points2[num2].y < y){y--;}
				}
				else if (option == 7){
					if(points2[num2].x > x){x++;}
					if(points2[num2].y > points1[num1].y){
						if(points2[num2].y > y){y++;}
					}
					else if(points2[num2].y < y){y--;}
				}
				
				breaker++;
			}
			if(!map[x,y].filled){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
	}

	void Start () {
		//Temporarily only for 5x5 map
		//Tile root = new Tile(new Vector3(0,0,0));
		//Vector3 position;
		this.map = new Tile[5,5];
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				map[i,j] = new Tile();
			}
		}
		
		// Using idea from http://www.roguebasin.com/index.php?title=Irregular_Shaped_Rooms to generate irregular shaped rooms
		//Getting left rectangle points
		
		Point[] leftRects = getPoints(1,4,0,2);
		Point[] upperRects = getPoints(3,5,1,4);
		Point[] rightRects = getPoints(1,4,3,5);
		Point[] lowerRects = getPoints(0,2,1,4);

		//Line Connecting Code
		
		connect(leftRects, 0, upperRects, 0, 0); //Connecting left to up
		connect(upperRects, 0, upperRects, 1, 1); //Connecting up1 to up2
		connect(upperRects, 1, rightRects, 0, 2); //Connecting upper to right	
		connect(rightRects, 0, rightRects, 1, 3); //Connecting right1 to right2
		connect(rightRects, 1, lowerRects, 0, 4); //Connecting right2 to lower
		connect(lowerRects, 0, lowerRects, 1, 5); //Connecting lower to lower
		connect(lowerRects, 1, leftRects, 1, 6); //Connecting lower to left
		connect(leftRects, 1, leftRects, 0, 7); //Connecting left to left
		
		//Filling in the center of map
		bool filled;
		for(int i = 0; i < 5; i++){
			filled = false;
			for(int j = 0; j < 5; j++){		
				if (map[i,j].filled) {
					filled = !filled;
				}
				else if (filled) {
					map[i,j] = new Tile(new Vector3(i,0,j));
				}
			}
		}
		
		//Making sure player spawn area is filled... 
		for(int i=1; i <= 3; i++){
			if(!map[i,1].filled){
				map[i,1] = new Tile(new Vector3(i,0,1));
			}
		}
	}
}
