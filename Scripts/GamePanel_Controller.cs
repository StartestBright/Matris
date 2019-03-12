using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel_Controller : MonoBehaviour {
    public int width, height;
    public GameObject background_tile;
    public GameObject[] circle_types;
    public GameObject[,] circles_on_panel;
    


	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Init()
    {
        circles_on_panel = new GameObject[width, height];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++)
            {
                GameObject temp_tile = GameObject.Instantiate(background_tile, new Vector2(j, i), Quaternion.identity) as GameObject;
                temp_tile.transform.parent = this.transform;
                temp_tile.name = "(" + j + " ," + i + ")";

                GameObject random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(j, i), Quaternion.identity) as GameObject;
                new_circle.GetComponent<Circle>().SetPosition(new Vector2(j, i));
                circles_on_panel[j, i] = new_circle;
                new_circle.transform.parent = this.transform;
            }
        }
    }
}
