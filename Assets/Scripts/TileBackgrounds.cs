using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBackgrounds
{
	public GameObject BG1;
	public GameObject BG2;
	public Utils utils;
	public TileBase[] tiles;
	public int[] code_0;
	public int[] code_1;
	public int[] code_2;
	public int[] code_3;
	public int[] code_4;
	public float speed;
	public int[] counter;
	
	public TileBackgrounds(GameObject BG1, GameObject BG2){
		this.utils = new Utils();
		this.BG1 = BG1;
		this.BG2 = BG2;
		this.tiles = new TileBase[]{
			Resources.Load<Tile>("Tiles/backgrounds_0"),
			Resources.Load<Tile>("Tiles/backgrounds_1"),
			Resources.Load<Tile>("Tiles/backgrounds_2"),
			Resources.Load<Tile>("Tiles/backgrounds_3"),
			Resources.Load<Tile>("Tiles/backgrounds_4"),
			Resources.Load<Tile>("Tiles/backgrounds_5"),
			Resources.Load<Tile>("Tiles/backgrounds_6"),
			Resources.Load<Tile>("Tiles/backgrounds_7"),
			Resources.Load<Tile>("Tiles/backgrounds_8")
		};
		this.code_0 = new int[]{
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0,
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0
		};
		this.code_1 = new int[]{
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 4, 5, 5, 5, 5, 6, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0 
		};
		this.code_2 = new int[]{
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 7, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 8, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 7, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 8, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0 
		};
		this.code_3 = new int[]{
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 7, 0, 
			0, 8, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 7, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 7, 0, 
			0, 7, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 8, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0 
		};
		this.code_4 = new int[]{
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 8, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 7, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0, 
			0, 0, 1, 2, 2, 2, 2, 3, 0, 0 
		};
		this.speed = 1;
		this.counter = new int[]{0, 0};
	}
	
	public void draw_newTilemap(GameObject obj, int[] tilemap){
		int index = 0;
		for(int y = 0; y < 14; y++){
			for(int x = 0; x < 10; x++){
				obj.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tiles[tilemap[index]]);
				index += 1;
			}
		}
	}
	
	public void scroll_vertical(){
		this.utils.set_position(this.BG1, new Vector3(this.BG1.transform.position.x, this.BG1.transform.position.y+this.speed, this.BG1.transform.position.z));
		if(this.BG1.transform.position.y >= 224){
			this.counter[1] += 1;
			this.utils.set_position(this.BG1, new Vector3(this.BG1.transform.position.x, (this.BG1.transform.position.y-448), this.BG1.transform.position.z));
		}
		
		this.utils.set_position(this.BG2, new Vector3(this.BG2.transform.position.x, this.BG2.transform.position.y+this.speed, this.BG2.transform.position.z));
		if(this.BG2.transform.position.y >= 224){
			this.counter[1] += 1;
			this.utils.set_position(this.BG2, new Vector3(this.BG2.transform.position.x, (this.BG2.transform.position.y-448), this.BG2.transform.position.z));
		}
	}
	
	public void control(){
		if(this.counter[0] != this.counter[1]){
			if(this.counter[1] >= 1 && (this.counter[1] % 2) != 0){
				int[][] code = new int[][]{this.code_2, this.code_3, this.code_4}; 
				int choice = Random.Range(0, 3);
				this.draw_newTilemap(this.BG1, code[choice]);
			}
			this.counter[0] = this.counter[1];
		}
	}
}
