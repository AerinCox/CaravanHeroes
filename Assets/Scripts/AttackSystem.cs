using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic for now.. will eventually help implement combat skills
public static class AttackSystem{

	public static void Attack(CharacterAttributes attacker, CharacterAttributes defender){
		int damage = attacker.getStr() - (int)Mathf.Ceil((float)defender.getDef()/2);
		if(damage <= 0){
			damage = 1;
		}
		defender.changeHp(0-damage);
		
		GameObject visual = new GameObject();
		visual.AddComponent<TimedDeletion>();
		TextMesh tm = visual.AddComponent<TextMesh>();
		MeshRenderer mr = visual.GetComponent<MeshRenderer>();
		mr.material.color = Color.red;
		tm.text = damage.ToString();
		tm.fontSize = 100;
		visual.transform.localScale = new Vector3(.05f,.05f,.05f);
		visual.transform.position = defender.getGameObject().transform.position + new Vector3(1.8f,0f,2.1f);
		
	}
	
}
