using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverworldControls : MonoBehaviour {

	int state; //indicates the current state of the map.. only 3 options right now
	public GameObject map1; //this is a really lazy way to do this but.. im rushing.. lol..
	public GameObject map2;
	public GameObject map3;
	public GameObject arrow1;
	public GameObject arrow2;
	public GameObject arrow3;
	public GameObject arrow4;
	public GameObject arrow5;
	public Button enterDungeon;
	bool enteringDungeon;
	bool faded;
	
	 public Texture2D fadeTexture;
 
     [Range(0.1f,1f)]
     public float fadespeed;
     public int drawDepth = -1000;
 
     private float alpha = -1f;
     private float fadeDir = 1f;
	
	// Use this for initialization
	void Start () {
		this.enteringDungeon = false;
		this.faded = false;
		this.state = 1;
		this.enterDungeon.onClick.AddListener(EnterDungeon);
	}
	
	void OnGUI(){
		if(enteringDungeon){
		alpha += fadeDir * fadespeed * Time.deltaTime;
         alpha = Mathf.Clamp01(alpha);
 
         Color newColor = GUI.color; 
         newColor.a = alpha;
 
         GUI.color = newColor;
 
         GUI.depth = drawDepth;
 
         GUI.DrawTexture( new Rect(0,0, Screen.width, Screen.height), fadeTexture);
		 if(alpha >= 1){
			this.faded = true;
		 }
		 }
	}
	
	void EnterDungeon(){
		
		 
		enteringDungeon = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(faded){
			SceneManager.LoadScene("MapTesting", LoadSceneMode.Single);
		}
		if (Input.GetMouseButtonDown (0)){
					RaycastHit rayTarget;
					Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
					bool hit = Physics.Raycast(ray, out rayTarget);
					Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.
mousePosition), rayTarget.point);
					//Human interaction input
					if(hit && rayTarget.collider != null){
						GameObject target = rayTarget.collider.gameObject;
						if(target.tag == "Arrow"){
							if(this.state == 1){
								map1.SetActive(false);
								map2.SetActive(true);
								arrow1.SetActive(false);
								arrow2.SetActive(true);
								arrow3.SetActive(true);
								arrow5.SetActive(true);
								this.state = 2;
							}
							else if(this.state == 2){
								if(target.name == "arrow3"){
									this.state = 3;
									enterDungeon.gameObject.SetActive(true);
									arrow2.SetActive(false);
									arrow3.SetActive(false);
									arrow4.SetActive(true);
									arrow5.SetActive(false);
									map2.SetActive(false);
									map3.SetActive(true);
								}
								else if(target.name == "arrow5"){
									arrow2.SetActive(false);
									arrow3.SetActive(false);
									arrow5.SetActive(false);
									arrow1.SetActive(true);
									map2.SetActive(false);
									map1.SetActive(true);
									this.state = 1;
								}
								else{
									print("srry, not implemented yet");
								}
							}
							else if(this.state == 3){
								this.state = 2;
								enterDungeon.gameObject.SetActive(false);
								arrow2.SetActive(true);
								arrow3.SetActive(true);
								arrow4.SetActive(false);
								arrow5.SetActive(true);
								map3.SetActive(false);
								map2.SetActive(true);
							}
						}
					}
		
		}
	}
}
