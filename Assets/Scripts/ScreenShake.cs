using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake
{
	public GameObject camera;
	public Vector3 origin;
	public int amount;
	public int intensity;
	public int[] delay;
	public bool[] triggerOnce;
	
	public ScreenShake(GameObject camera){
		this.camera = camera;
		this.origin = new Vector3(this.camera.transform.position.x, this.camera.transform.position.y, this.camera.transform.position.z);
		this.amount = 0;
		this.intensity = 1;
		this.delay = new int[]{3, 3};
		this.triggerOnce = new bool[]{false};
	}
	
	public void screen_shake(){
		if(this.amount > 0){
			if(this.delay[1] == this.delay[0]){
				if((this.amount % 2) == 0){
					this.camera.transform.position = new Vector3(
						this.origin[0] + this.intensity,
						this.origin[1],
						this.origin[2]
					);
				}else{
					this.camera.transform.position = new Vector3(
						this.origin[0] - this.intensity,
						this.origin[1],
						this.origin[2]
					);
				}
				this.amount -= 1;
				this.delay[1] = 0;
			}
			this.delay[1] += 1;
		}
		
		if(this.amount == 0 
		&& this.triggerOnce[0] == false){
			this.camera.transform.position = new Vector3(
				this.origin[0],
				this.origin[1],
				this.origin[2]
			);
			this.delay[1] = this.delay[0];
			this.triggerOnce[0] = true;
		}
	}
}
