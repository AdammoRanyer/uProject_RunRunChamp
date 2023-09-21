using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfig
{
	public Utils utils;
	public Resolution monitor_resolution;
	public int[] screen_originalResolution;
	public int resolution;
	public bool fullscreen;
	public int FPS;
	
	public SceneConfig(int[] screen_originalResolution, int resolution, bool fullscreen, int FPS){
		this.utils = new Utils();
		this.monitor_resolution = Screen.currentResolution;
		Save load = this.utils.load_game();
		if(load == null){
			this.screen_originalResolution = screen_originalResolution;
			this.resolution = resolution;
			this.fullscreen = fullscreen;
			this.FPS = FPS;
			Save save = new Save();
			save.screen_originalResolution = this.screen_originalResolution;
			save.resolution = this.resolution;
			save.fullscreen = this.fullscreen;
			save.FPS = this.FPS;
			this.utils.save_game(save);
		}else{
			this.screen_originalResolution = load.screen_originalResolution;
			this.resolution = load.resolution;
			this.fullscreen = load.fullscreen;
			this.FPS = load.FPS;
		}
		
		this.utils.set_resolution(this.screen_originalResolution, this.resolution, this.fullscreen);
		this.utils.set_FPS(this.FPS);
	}
}
