using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Utils
{
	public void set_FPS(int new_FPS){
		/*
		Defini valor para taxa de frames por segundo.
		
		Parâmetros:
			new_FPS (int): taxa de frames por segundo, pré-definida como 60
			
		Retorno:
			FPS definido
		*/
		
		Application.targetFrameRate = new_FPS;
	}
	
	public void set_resolution(int[] screen_originalResolution, int new_resolution, bool fullscreen){
		/*
		Atualiza resolução da tela.
		
		Parâmetros:
			screen_originalResolution (int[]) - tamanho original da tela
			new_resolution (int) - escala da nova resolução
			fullscreen (bool) - ativa/desativa tela cheia 
			
		Retorno:
			Resolução atualizada
		*/
		
		int width = screen_originalResolution[0] * new_resolution;
		int height = screen_originalResolution[1] * new_resolution;
		Screen.SetResolution(width, height, fullscreen);
	}
	
	public void set_backgroundColor(GameObject camera, Vector4 new_colorRGBA){
		/*
		Defini cor de background do objeto camera.
		
		Parâmetros:
			camera (GameObject) - o objeto camera
			new_colorRGBA (Vector4) - a nova cor no formato RGBA 0-255
			
		Retorno:
			Cor de background atualizado
		*/
		
		Color new_color = new Vector4(new_colorRGBA[0] / 255f, new_colorRGBA[1] / 255f, new_colorRGBA[2] / 255f, new_colorRGBA[3] / 255f);
		camera.GetComponent<Camera>().backgroundColor = new_color;
	}
	
	public void set_position(GameObject obj, Vector3 new_position){
		/*
		Atualiza posição de um objeto.
		
		Parâmetros:
			obj (GameObject) - o objeto
			new_position (Vector3) - nova posição
			
		Retorno:
			Posição de objeto atualizada
		*/
		
		obj.transform.position = new_position;
	}
	
	public Vector2 hvspeed_position(GameObject obj, Vector2 hvspeed){
		/*
		Atualiza posição de um objeto e retorna hvspeed.
		
		Parâmetros:
			obj (GameObject) - o objeto
			hvspeed (Vector2) - velocidade de deslocamento horizontal e vertical
		
		Retorno:
			Posição de objeto atualizada e hvspeed zerado
		*/
		
		Vector3 new_position = new Vector3(
			obj.transform.position.x + hvspeed[0],
			obj.transform.position.y + hvspeed[1],
			obj.transform.position.z
		);
		obj.transform.position = new_position;
		hvspeed[0] = 0;
		hvspeed[1] = 0;
		
		return hvspeed;
	}
	
	public bool inputKey(string key, int mode){
		/*
		Valida se a tecla de entrada e veradeira ou falsa de acordo com o modo definido.
		
		Parâmetros:
			key (string): a tecla de entrada que será validade como verdeira ou falsa
			mode (int): modo de validação da tecla. 0 para tecla mantida pressionada, 1 para click na tecla
		
		Retorno:
			Tecla de entreda validada
		*/	
		
		bool choice = false;
		if(key.Length > 1){
			KeyCode keyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), key);
			if(mode == 0){
				choice = Input.GetKey(keyCode);
			}else if(mode == 1){
				choice = Input.GetKeyDown(keyCode);
			}		
		}else{
			if(mode == 0){
				choice = Input.GetKey(key);
			}else if(mode == 1){
				choice = Input.GetKeyDown(key);
			}	
		}

		return choice;
	}
	
	public void debug_drawRect(Rect rect, Color color){
		/*
		Desenha retângulo para debug.
		
		Parâmetros:
			rect (Rect) - dados do retângulo
			color (Color) - cor do retângulo
			
		Retorno:
			Retângulo desenhado na tela
		*/
		
		Debug.DrawRay(new Vector3(rect.x, rect.y, 0), new Vector2(rect.width, 0), color); // up
		Debug.DrawRay(new Vector3(rect.x, rect.y-rect.height, 0), new Vector2(rect.width, 0), color); // down
		Debug.DrawRay(new Vector3(rect.x, rect.y, 0), new Vector2(0, -rect.height), color); // left
		Debug.DrawRay(new Vector3(rect.x+rect.width, rect.y, 0), new Vector2(0, -rect.height), color); // right
	}
	
	public Vector2 boxColision_wall(GameObject obj, Vector2 hvspeed, string colliderName){
		/*
		Colisão de quatro direções para parede
		
		Parâmetros:
			obj (GameObject) - o objeto
			hvspeed (Vector2) - velocidade horizontal e vertical atual
			colliderName (string) - nome do objeto colisor
			
		Retorno:
			hvspeed atualizado
		*/
		
		Vector3 origin = new Vector3(
			obj.transform.position.x+obj.GetComponent<BoxCollider2D>().offset.x, 
			obj.transform.position.y+obj.GetComponent<BoxCollider2D>().offset.y, 
			obj.transform.position.z
		);
		Vector2 size = new Vector2(obj.GetComponent<BoxCollider2D>().size[0]-0.1f, obj.GetComponent<BoxCollider2D>().size[1]-0.1f);
		float angle = 0;
		Vector2 h_direction = new Vector2(Mathf.Sign(hvspeed[0]), 0);
		Vector2 v_direction = new Vector2(0, Mathf.Sign(hvspeed[1]));
		float h_distance = Mathf.Abs(hvspeed[0]);
		float v_distance = Mathf.Abs(hvspeed[1]);
		RaycastHit2D[] h_boxCast = Physics2D.BoxCastAll(origin, size, angle, h_direction, h_distance);
		RaycastHit2D[] v_boxCast = Physics2D.BoxCastAll(origin, size, angle, v_direction, v_distance);
		Color color = Color.yellow;

		foreach(var hit in h_boxCast){
			if(hit.collider.gameObject.name.StartsWith(colliderName)){
				color = Color.red;
				if(hvspeed[0] < 0){
					hvspeed[0] = 0;
					set_position(obj, new Vector3(obj.transform.position.x-(hit.distance-0.1f), obj.transform.position.y, obj.transform.position.z));
				}
				if(hvspeed[0] > 0){
					hvspeed[0] = 0;
					set_position(obj, new Vector3(obj.transform.position.x+(hit.distance-0.1f), obj.transform.position.y, obj.transform.position.z));
				}
			}
		}
		foreach(var hit in v_boxCast){
			if(hit.collider.gameObject.name.StartsWith(colliderName)){
				color = Color.red;
				if(hvspeed[1] < 0){
					hvspeed[1] = 0;
					set_position(obj, new Vector3(obj.transform.position.x, obj.transform.position.y-(hit.distance-0.1f), obj.transform.position.z));
				}
				if(hvspeed[1] > 0){
					hvspeed[1] = 0;
					set_position(obj, new Vector3(obj.transform.position.x, obj.transform.position.y+(hit.distance-0.1f), obj.transform.position.z));
				}
			}
		}
		debug_drawRect(
			new Rect(
				obj.GetComponent<BoxCollider2D>().bounds.center.x-(size[0]/2), 
				obj.GetComponent<BoxCollider2D>().bounds.center.y+(size[1]/2), 
				size[0], 
				size[1]
			), 
			color
		);
		
		return hvspeed;
	}

	public bool boxColision_object(GameObject obj, string colliderName){
		/*
		Colisão de quatro direções para objeto
		
		Parâmetros:
			obj (GameObject) - o objeto
			colliderName (string) - nome do objeto colisor
			
		Retorno:
			Veradeiro ou falso para colisão
		*/
		
		bool collided = false;
		Vector3 origin = new Vector3(
			obj.transform.position.x+obj.GetComponent<BoxCollider2D>().offset.x, 
			obj.transform.position.y+obj.GetComponent<BoxCollider2D>().offset.y, 
			obj.transform.position.z
		);
		Vector2 size = new Vector2(obj.GetComponent<BoxCollider2D>().size[0]-0.1f, obj.GetComponent<BoxCollider2D>().size[1]-0.1f);
		float angle = 0;
		Vector2 h_direction = new Vector2(0, 0);
		Vector2 v_direction = new Vector2(0, 0);
		float h_distance = 0;
		float v_distance = 0;
		RaycastHit2D[] h_boxCast = Physics2D.BoxCastAll(origin, size, angle, h_direction, h_distance);
		RaycastHit2D[] v_boxCast = Physics2D.BoxCastAll(origin, size, angle, v_direction, v_distance);
		Color color = Color.yellow;

		foreach(var hit in h_boxCast){
			if(hit.collider.gameObject.name.StartsWith(colliderName)){
				color = Color.red;
				collided = true;
			}
		}
		foreach(var hit in v_boxCast){
			if(hit.collider.gameObject.name.StartsWith(colliderName)){
				color = Color.red;
				collided = true;
			}
		}
		debug_drawRect(
			new Rect(
				obj.GetComponent<BoxCollider2D>().bounds.center.x-(size[0]/2), 
				obj.GetComponent<BoxCollider2D>().bounds.center.y+(size[1]/2), 
				size[0], 
				size[1]
			), 
			color
		);
		
		return collided;
	}

	public void load_scene(string name){
		/*
		Carrega uma cena.
		
		Parâmetros:
			name (string) - nome da cena

		Retorno:
			Cena carregada
		*/
		
		if(name == ""){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}else{
			SceneManager.LoadScene(name);
		}
	}

	public void save_game(Save save){
		/*
		Cria/atualiza arquivo de save do script/classe Save.
		
		Parâmetros:
			save (Save) - classe Save
			
		Retorno:
			Retorna arquivo de save atualizado
		*/
		
		string path = Application.persistentDataPath; // C:/Users/Adammo/AppData/LocalLow/DefaultCompany/uProject_runRunChamp
		FileStream file = File.Create(path + "/saveFile.save");
		BinaryFormatter BF = new BinaryFormatter();
		BF.Serialize(file, save);
		file.Close();
	}
	
	public Save load_game(){
		/*
		Carrega dados do arquivo de save e retorna em uma variável.
		
		Parâmetros:
			Nenhum
			
		Retorno:
			Variável do tipo Save
		*/
		
		string path = Application.persistentDataPath;
		FileStream file;
		BinaryFormatter BF = new BinaryFormatter();
		
		if(File.Exists(path + "/saveFile.save")){
			file = File.Open(path + "/saveFile.save", FileMode.Open);
			Save load = (Save)BF.Deserialize(file);
			file.Close();
		
			return load;
		}
		
		return null;
	}
	
	public void set_animation(GameObject obj, string[] animation, float frame){
		/*
		Toca uma nova animação se animation[0] for diferente de animation[1].
		
		Parâmetros:
			obj (GameObject) - o objeto
			animation (string[]) - animação atual e posterior
			frame (float) - frame inicial, escala de 0 a 1
			
		Retorno:
			Inicia nova animação
		*/
		
		if(animation[0] != animation[1]){
			Animator animator = obj.GetComponent<Animator>();
			animator.Play(animation[1], 0, frame);
			animation[0] = animation[1];
		}
	}
	
	public void replaceColor(GameObject obj, string propertyName, Vector4 new_colorRGBA){
		/*
		Troca cor da propriedade do material.
		
		Parâmetros:
			obj (GameObject) - o objeto
			propertyName (string) - nome da propriedade do material
			new_colorRGBA (Vector4) - a nova cor no formato RGBA 0-255
			
		Retorno:
			Atualiza cor da propriedade
		*/
		
		SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
		Color new_color = new Vector4(new_colorRGBA[0] / 255f, new_colorRGBA[1] / 255f, new_colorRGBA[2] / 255f, new_colorRGBA[3] / 255f);
		spriteRenderer.material.SetColor(propertyName, new_color);
	}
	
	public void set_zOrder(GameObject obj, int zOrder){
		/*
		
		
		Parâmetros:
			
		
		Retorno:
			
		*/
		
		SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
		if(spriteRenderer.sortingOrder != zOrder){
			spriteRenderer.sortingOrder = zOrder;
		}
	}
}
