using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public GameObject obj;
	public Utils utils;
	public InputsKeys IK;
	public float speed;
	public Vector2 hvspeed;
	public string[] animation;
	public string[] animation_shadow;
	public string[] state;
	public int flash_hurt;
	public bool[] triggerOnce;
	
	public Player(GameObject obj){
		this.obj = obj;
		this.utils = new Utils();
		this.IK = new InputsKeys();
		this.speed = 1;
		this.hvspeed = new Vector2(0, 0);
		this.animation = new string[]{"", ""};
		this.animation_shadow = new string[]{"", ""};
		this.state = new string[]{"", ""};
		this.flash_hurt = 0;
		this.triggerOnce = new bool[]{false};
	}
	
	public void movements(){
		if(this.state[0] != "atingido"){
			if(this.utils.inputKey(this.IK.rightArrow, 0)){
				this.hvspeed[0] += this.speed;
			}
			if(this.utils.inputKey(this.IK.leftArrow, 0)){
				this.hvspeed[0] -= this.speed;
			}
			if(this.utils.inputKey(this.IK.upArrow, 0)){
				this.hvspeed[1] += this.speed;
				this.state[1] = "correndo devagar";
			}
			if(this.utils.inputKey(this.IK.downArrow, 0)){
				this.hvspeed[1] -= this.speed;
				this.state[1] = "correndo mais rápido";
			}
			
			if((this.utils.inputKey(this.IK.downArrow, 0) == false
			&& this.utils.inputKey(this.IK.upArrow, 0) == false)
			|| (this.utils.inputKey(this.IK.downArrow, 0)
			&& this.utils.inputKey(this.IK.upArrow, 0))){
				this.state[1] = "correndo";
			}
		}
	}
	
	public void colisions(){
		this.hvspeed = this.utils.boxColision_wall(this.obj, this.hvspeed, "tilemapColisions");
		if(this.utils.boxColision_object(this.obj, "enemy1")
		&& this.state[0] != "atingido"){
			this.state[1] = "atingido";
		}
	}	

	public void animationsControl(){
		this.utils.set_animation(this.obj, this.animation, 0);
		this.utils.set_animation(this.obj.transform.GetChild(0).gameObject, this.animation_shadow, 0);
	}

	public void replaceColor(){
		if(this.flash_hurt > 0){
			if(this.triggerOnce[0] == false){
				this.utils.replaceColor(this.obj, "_Color1", new Vector4(255, 255, 255, 0));
				this.utils.replaceColor(this.obj, "_Color2", new Vector4(255, 255, 255, 0));
				this.utils.replaceColor(this.obj, "_Color3", new Vector4(255, 255, 255, 0));
				this.utils.replaceColor(this.obj, "_Color4", new Vector4(255, 255, 255, 0));
				this.utils.replaceColor(this.obj, "_Color5", new Vector4(255, 255, 255, 0));
				this.triggerOnce[0] = true;
			}
			this.flash_hurt -= 1;
		}else{
			if(this.triggerOnce[0] == true){
				this.utils.replaceColor(this.obj, "_Color1", new Vector4(61, 61, 61, 0));
				this.utils.replaceColor(this.obj, "_Color2", new Vector4(51, 152, 75, 0));
				this.utils.replaceColor(this.obj, "_Color3", new Vector4(90, 197, 79, 0));
				this.utils.replaceColor(this.obj, "_Color4", new Vector4(255, 235, 87, 0));
				this.utils.replaceColor(this.obj, "_Color5", new Vector4(255, 255, 255, 0));
				this.triggerOnce[0] = false;
			}
		}
	}

	public void change_state(){
		if(this.state[0] != this.state[1]){
			if(this.state[1] == "largada"){
				this.animation[1] = "p1_start0";
				this.animation_shadow[1] = "shadow1_start0";
			}
			else if(this.state[1] == "correndo"){
				this.animation[1] = "p1_run0";
				this.animation_shadow[0] = "";
				this.animation_shadow[1] = "shadow1_run0";
			}
			else if(this.state[1] == "correndo mais rápido"){
				this.animation[1] = "p1_run1";
				this.animation_shadow[0] = "";
				this.animation_shadow[1] = "shadow1_run0";
			}
			else if(this.state[1] == "correndo devagar"){
				this.animation[1] = "p1_run2";
				this.animation_shadow[1] = "shadow1_run2";
			}
			else if(this.state[1] == "atingido"){
				this.animation[1] = "p1_hurt0";
				this.animation_shadow[1] = "shadow1_hurt0";
				this.flash_hurt = 6;
			}
			else if(this.state[1] == "caído"){
				this.animation[1] = "p1_down0";
				this.animation_shadow[1] = "shadow1_down0";
			}
			
			this.state[0] = this.state[1];
		}
	}

	public void update(){
		change_state();
		
		movements();
		colisions();
		this.hvspeed = this.utils.hvspeed_position(this.obj, this.hvspeed);
		animationsControl();
	}
}
