using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generates the Map and initial player positions
public class TileSpawn : MonoBehaviour {
	
	Tile[,] map;

	public Tile getTile(Point point){
		if(point.x >= map.GetLength(0) || point.x < 0 || point.y >= map.GetLength(1) || point.y < 0){
			return null;
		}
		return map[point.x, point.y];
	}
	
	public Tile[,] getMap(){
		return this.map;
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
		Point tempPoint;
		int rectX = Random.Range(1,4);
		int rectY = Random.Range(0,2);
		Point leftRect1 = new Point(rectX, rectY);
		rectX = Random.Range(1,4);
		rectY = Random.Range(0,2);
		Point leftRect2 = new Point(rectX, rectY);
		// 1 should always be higher than 2
		if(leftRect1.x < leftRect2.x){
			tempPoint = leftRect2;
			leftRect2 = leftRect1;
			leftRect1 = tempPoint;
		}
		//Getting Upper rectangle points
		rectX = Random.Range(3,5);
		rectY = Random.Range(1,4);
		Point upperRect1 = new Point(rectX, rectY);
		rectX = Random.Range(3,5);
		rectY = Random.Range(1,4);
		Point upperRect2 = new Point(rectX, rectY);
		if(upperRect1.y > upperRect2.y){
			tempPoint = upperRect2;
			upperRect2 = upperRect1;
			upperRect1 = tempPoint;
		}
		//Getting Right rectangle points
		rectX = Random.Range(1,4);
		rectY = Random.Range(3,5);
		Point rightRect1 = new Point(rectX, rectY);
		rectX = Random.Range(1,4);
		rectY = Random.Range(3,5);
		Point rightRect2 = new Point(rectX, rectY);
		if(rightRect1.x < rightRect2.x){
			tempPoint = rightRect2;
			rightRect2 = rightRect1;
			rightRect1 = tempPoint;
		}
		//Getting Lower rectangle points
		rectX = Random.Range(0,2);
		rectY = Random.Range(1,4);
		Point lowerRect1 = new Point(rectX, rectY);
		rectX = Random.Range(0,2);
		rectY = Random.Range(1,4);
		Point lowerRect2 = new Point(rectX, rectY);
		if(lowerRect1.y < lowerRect2.y){
			tempPoint = lowerRect2;
			lowerRect2 = lowerRect1;
			lowerRect1 = tempPoint;
		}

		//Line Connecting Code
		//Connecting left to up
		int breaker = 0;
		int x = leftRect1.x;
		int y = leftRect1.y;
		if(leftRect1.x == upperRect1.x && leftRect1.y == upperRect1.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != upperRect1.x || y != upperRect1.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(upperRect1.x > x){
					x++;
				}
				if(upperRect1.y > y){
					y++;
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting up1 to up2
		breaker = 0;
		x = upperRect1.x;
		y = upperRect1.y;
		if(upperRect1.x == upperRect2.x && upperRect1.y == upperRect2.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != upperRect2.x || y != upperRect2.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(upperRect1.x > upperRect2.x){
					if(upperRect2.x > x){
						x--;
					}
				}
				else{
					if(upperRect2.x > x){
						x++;
					}
				}
				if(upperRect2.y > y){
					y++;
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting upper to right
		breaker = 0;
		x = upperRect2.x;
		y = upperRect2.y;
		if(upperRect2.x == rightRect1.x && upperRect2.y == rightRect1.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != rightRect1.x || y != rightRect1.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(rightRect1.x < x){
					x--;
				}
				if(rightRect1.y > y){
					y++;
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting right1 to right2
		breaker = 0;
		x = rightRect1.x;
		y = rightRect1.y;
		if(rightRect1.x == rightRect2.x && rightRect2.y == rightRect1.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != rightRect2.x || y != rightRect2.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(rightRect2.x < x){
					x--;
				}
				if(rightRect1.y > rightRect2.y){
					if(rightRect2.y < y){
						y--;
					}
				}
				else{
					if(rightRect2.y > y){
						y++;
					}
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting right2 to lower
		breaker = 0;
		x = rightRect2.x;
		y = rightRect2.y;
		if(rightRect2.x == lowerRect1.x && rightRect2.y == lowerRect1.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != lowerRect1.x || y != lowerRect1.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(lowerRect1.x < x){
					x--;
				}
				if(lowerRect1.y < y){
					y--;
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting lower to lower
		breaker = 0;
		x = lowerRect1.x;
		y = lowerRect1.y;
		if(lowerRect1.x == lowerRect2.x && lowerRect1.y == lowerRect2.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != lowerRect2.x || y != lowerRect2.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(lowerRect1.x > lowerRect2.x){
					if(lowerRect2.x < x){
						x--;
					}
				}
				else{
					if(lowerRect2.x < x){
						x++;
					}
				}
				if(lowerRect2.y < y){
					y--;
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting lower to left
		breaker = 0;
		x = lowerRect2.x;
		y = lowerRect2.y;
		if(lowerRect2.x == leftRect2.x && lowerRect2.y == leftRect2.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != leftRect2.x || y != leftRect2.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(leftRect2.x > x){
					x++;
				}
				if(leftRect2.y < y){
					y--;
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		//Connecting left to left
		breaker = 0;
		x = leftRect2.x;
		y = leftRect2.y;
		if(leftRect2.x == leftRect1.x && leftRect1.y == leftRect2.y){
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		else{
			while((x != leftRect1.x || y != leftRect1.y) && breaker != 15) {
				if(map[x,y].filled == false){
					map[x,y] = new Tile(new Vector3(x,0,y));
				}
				if(leftRect1.x > x){
					x++;
				}
				if(leftRect1.y > leftRect2.y){
					if(leftRect1.y > y){
						y++;
					}
				}
				else{
					if(leftRect1.y < y){
						y--;
					}
				}
				breaker++;
			}
			if(map[x,y].filled == false){
				map[x,y] = new Tile(new Vector3(x,0,y));
			}
		}
		
		//Filling in the center of map
		bool filled = false;
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				if(map[i,j].filled == true && filled == true){
					filled = false;
				}
				else if(map[i,j].filled == true){
					filled = true;
					continue;
				}
				if(filled == true && map[i,j].filled != true){
					map[i,j] = new Tile(new Vector3(i,0,j));
				}
			}
			filled = false;
		}
		
		//Making sure player spawn area is filled... right now there are 2 fixed NPCs on the map
		if(map[1,1].filled == false){
			map[1,1] = new Tile(new Vector3(1,0,1));
			map[1,1].occupied = true;
		}
		if(map[3,3].filled == false){
			map[3,3] = new Tile(new Vector3(3,0,3));
			map[3,3].occupied = true;
		}
	}
}
