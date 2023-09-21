using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScene : MonoBehaviour
{
	public SceneConfig SC;
	public Utils utils;
	public Player P1;
	public InputsKeys IK;
	public string[] state;
	public int[] timer;
	public bool paused;
	public TileBackgrounds TB;
	public ScreenShake SS;
	
    // Start is called before the first frame update
    void Start()
    {
		//Debug.Log(Application.persistentDataPath); // C:/Users/Adammo/AppData/LocalLow/DefaultCompany/uProject_runRunChamp
		SC = new SceneConfig(
			new int[]{64, 112}, // screen_originalResolution
			6,                  // resolution
			false,              // fullscreen
			60                  // FPS
		);
		utils = new Utils();
		IK = new InputsKeys();
		P1 = new Player(GameObject.Find("P1"));
		TB = new TileBackgrounds(GameObject.Find("tilemap1"), GameObject.Find("tilemap2"));
		state = new string[]{"", "largada"};
		timer = new int[]{0};
		paused = false;
		SS = new ScreenShake(GameObject.Find("camera"));
    }

    // Update is called once per frame
    void Update()
    {
		change_state();
		debug_setResolution();
		debug_restartScene();
		debug_text();
		SS.screen_shake();
		
		state_largada();
		state_correndo();
		state_atingido();
		state_caido();
		//zOrderControl();
    }
	
	void OnApplicationQuit(){
		setAndSave();
	}
	
	public void setAndSave(){
		Save save = new Save();
		save.screen_originalResolution = SC.screen_originalResolution;
		save.resolution = SC.resolution;
		save.fullscreen = SC.fullscreen;
		save.FPS = SC.FPS;
		
		utils.save_game(save);
	}
	
	public void debug_text(){
		string tilemaps_counter = TB.counter[0].ToString();
		GameObject.Find("tmp_debug").GetComponent<TMP_Text>().text = "tilemaps counter: "+tilemaps_counter;
	}
	
	public void debug_setResolution(){
		if(utils.inputKey(IK.alpha1, 1)){
			SC.resolution = 2;
			SC.fullscreen = false;
			utils.set_resolution(SC.screen_originalResolution, SC.resolution, SC.fullscreen);
		}
		if(utils.inputKey(IK.alpha2, 1)){
			SC.resolution = 4;
			SC.fullscreen = false;
			utils.set_resolution(SC.screen_originalResolution, SC.resolution, SC.fullscreen);
		}
		if(utils.inputKey(IK.alpha3, 1)){
			SC.resolution = 6;
			SC.fullscreen = false;
			utils.set_resolution(SC.screen_originalResolution, SC.resolution, SC.fullscreen);
		}
		if(utils.inputKey(IK.alpha4, 1)){
			SC.resolution = 8;
			SC.fullscreen = false;
			utils.set_resolution(SC.screen_originalResolution, SC.resolution, SC.fullscreen);
		}
		if(utils.inputKey(IK.alpha5, 1)){
			int new_resolution = Mathf.CeilToInt((SC.monitor_resolution.height / SC.screen_originalResolution[1]));
			SC.resolution = new_resolution - (new_resolution % 2);
			SC.fullscreen = true;
			utils.set_resolution(SC.screen_originalResolution, SC.resolution, SC.fullscreen);
		}
	}
	
	public void debug_restartScene(){
		if(utils.inputKey(IK.r, 1)){
			setAndSave();
			utils.load_scene("");
		}
	}
	
	public void pause(){
		if(utils.inputKey(IK.kreturn, 1) && paused == false){
			P1.obj.GetComponent<Animator>().enabled = false;
			P1.obj.transform.GetChild(0).GetComponent<Animator>().enabled = false;
			
			paused = true;
		}
		else if(utils.inputKey(IK.kreturn, 1) && paused == true){
			P1.obj.GetComponent<Animator>().enabled = true;
			P1.obj.transform.GetChild(0).GetComponent<Animator>().enabled = true;
			
			paused = false;
		}
	}
		
	public void change_state(){
		if(state[0] != state[1]){
			if(state[1] == "largada"){
				TB.draw_newTilemap(TB.BG1, TB.code_1);
				TB.draw_newTilemap(TB.BG2, TB.code_0);
				P1.state[1] = "largada";
			}
			else if(state[1] == "correndo"){
				P1.state[1] = "correndo";
			}
			else if(state[1] == "atingido"){
				SS.amount = 4;
				SS.triggerOnce[0] = false;
			}
			else if(state[1] == "caído"){
				P1.state[1] = "caído";
			}
			state[0] = state[1];
		}
	}
	
	public void zOrderControl(){
		if(P1.obj.transform.position.y < GameObject.Find("enemy1").transform.position.y){
			utils.set_zOrder(GameObject.Find("enemy1"), 1);
		}else{
			utils.set_zOrder(GameObject.Find("enemy1"), 3);
		}
	}
	
	public void state_largada(){
		if(state[0] == "largada"){
			if(timer[0] == 60){
				state[1] = "correndo";
				timer[0] = 0;
			}else{
				timer[0] += 1;
			}
			P1.change_state();
			P1.animationsControl();
		}
	}

	public void state_correndo(){
		if(state[0] == "correndo"){
			pause();
			if(paused == false){
				P1.update();
				if(P1.state[0] == "atingido"){
					state[1] = "atingido";
				}
				TB.scroll_vertical();
				TB.control();
			}
		}
	}
	
	public void state_atingido(){
		if(state[0] == "atingido"){
			if(timer[0] == 30){
				state[1] = "caído";
				timer[0] = 0;
			}else{
				timer[0] += 1;
			}
			P1.replaceColor();
		}
	}
	
	public void state_caido(){
		P1.change_state();
		P1.animationsControl();
	}
}
