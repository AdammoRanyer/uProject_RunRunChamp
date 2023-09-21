using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
	public Utils utils;
	public float speed;
	public Vector2 hvspeed;
	
    // Start is called before the first frame update
    void Start()
    {
		utils = new Utils();
        speed = 2;
		hvspeed = new Vector2(0, 0);
		Debug.Log(gameObject.GetComponent<BoxCollider2D>().bounds.center);
    }

    // Update is called once per frame
    void Update()
    {
		if(GameObject.Find("camera").GetComponent<MainScene>().paused == false){
			if(gameObject.transform.position.y >= 230){
				Destroy(gameObject);
			}
			hvspeed[1] += speed;
			hvspeed = utils.hvspeed_position(gameObject, hvspeed);
		}
    }
}
