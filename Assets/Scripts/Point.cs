using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point{

	public int x;
	public int y;
	public Point(int x, int y){
		this.x = x;
		this.y = y;
	}

	public int getDifference(Point other){
		int xCompare = (int)Mathf.Abs(this.x - other.x);
		int yCompare = (int)Mathf.Abs(this.y - other.y);
		return xCompare + yCompare;
	}

	public string printPoint(){
		return "X: " + this.x + " Y: " + this.y;
	}
	
	public class pointEqualityComparer : IEqualityComparer<Point>{
		public bool Equals(Point p1, Point p2)
		{
			if (p2 == null && p1 == null){
			   return true;
			}
			else if (p1 == null | p2 == null){
			   return false;
			}
			else if(p1.x == p2.x && p1.y == p2.y){
				return true;
			}
			else{
				return false;
			}
		}
		public int GetHashCode(Point bx)
		{
			int hCode = bx.x ^ bx.y;
			return hCode.GetHashCode();
		}
	}
}
