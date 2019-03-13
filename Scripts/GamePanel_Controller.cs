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


    public bool CheckMatch()
    {
        bool there_is_match = false;
        
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                int horizontal_match_count = 1;
                int vertical_match_count = 1;
                int square_match_count = 1;
                GameObject current_circle = circles_on_panel[j, i];
                GameObject left_1_circle = null, left_2_circle = null, right_1_circle = null, right_2_circle = null;
                GameObject top_1_circle = null, top_2_circle = null, bot_1_circle = null, bot_2_circle = null;
                Circle[] square_match_circles = new Circle[4];
                //Debug.Log("x = " + j + " y = " + i+ " width = " + width+" height = " +height+" "+circles_on_panel.Length);
                if (j > 0)
                    left_1_circle = circles_on_panel[j - 1, i];
                if (j > 1)
                    left_2_circle = circles_on_panel[j - 2, i];
                if (j < width - 1)
                    right_1_circle = circles_on_panel[j + 1, i];
                if (j < width - 2)
                    right_2_circle = circles_on_panel[j + 2, i];
                if (i < height - 1)
                    top_1_circle = circles_on_panel[j, i + 1];
                if (i < height - 2)
                    top_2_circle = circles_on_panel[j, i + 2];
                if (i > 0)
                    bot_1_circle = circles_on_panel[j, i - 1];
                if (i > 1)
                    bot_2_circle = circles_on_panel[j, i - 2];

                


                if (left_1_circle != null)
                {

                    if (left_1_circle.CompareTag(circles_on_panel[j,i].tag)) //Checking left side match
                    {
                        horizontal_match_count++;

                        if (left_2_circle != null)
                        {
                            if ((left_2_circle.CompareTag(circles_on_panel[j, i].tag)))
                            {
                                horizontal_match_count++;
                            }
                        }
                    }
                }

                if (right_1_circle != null)
                {
                    if ((right_1_circle.CompareTag(circles_on_panel[j, i].tag))) //Checking right side match
                    {
                        horizontal_match_count++;
                        if (right_2_circle != null)
                        {
                            if ((right_2_circle.CompareTag(circles_on_panel[j, i].tag)))
                            {
                                horizontal_match_count++;
                            }
                        }
                    }
                }


                if (top_1_circle != null)
                {
                    if (top_1_circle.CompareTag(circles_on_panel[j, i].tag)) //Checking top match
                    {
                        vertical_match_count++;
                        if (top_2_circle != null)
                        {
                            if (top_2_circle.CompareTag(circles_on_panel[j, i].tag))
                            {
                                vertical_match_count++;
                            }
                        }
                    }
                }

                if (bot_1_circle != null)
                {
                    if (bot_1_circle.CompareTag(circles_on_panel[j, i].tag)) //Checking bottom match
                    {
                        vertical_match_count++;
                        if (bot_2_circle != null)
                        {
                            if (bot_2_circle.CompareTag(circles_on_panel[j, i].tag))
                            {
                                vertical_match_count++;
                            }
                        }
                    }
                }

                if(left_1_circle != null) //left top and bot square matches check
                {

                    if (current_circle.CompareTag(left_1_circle.tag)) { //
                        if (current_circle.CompareTag(left_1_circle.tag))
                        {
                            square_match_count++;
                            if (top_1_circle != null) //left top square match check
                            {
                                if (current_circle.CompareTag(top_1_circle.tag)) {
                                    square_match_count++;
                                    if (circles_on_panel[j - 1, i + 1] != null) {  //can cause PROBLEM INDEX OUT OF ARRAY!!!
                                        if (current_circle.CompareTag(circles_on_panel[j - 1, i + 1].tag)) { 
                                            square_match_count++;
                                            square_match_circles[0] = current_circle.GetComponent<Circle>();
                                            square_match_circles[1] = left_1_circle.GetComponent<Circle>();
                                            square_match_circles[2] = top_1_circle.GetComponent<Circle>();
                                            square_match_circles[3] = circles_on_panel[j-1,i+1].GetComponent<Circle>();
                                        }
                                        else
                                            square_match_count = 2; // current and left1 matches
                                    }
                                }
                            }
                            if (bot_1_circle != null) //left bot square match check
                            {
                                if (current_circle.CompareTag(bot_1_circle.tag))
                                {
                                    square_match_count++;
                                    if (circles_on_panel[j - 1, i - 1] != null)
                                    {
                                        if (current_circle.CompareTag(circles_on_panel[j - 1, i - 1].tag))
                                        {
                                            square_match_count++;
                                            square_match_circles[0] = current_circle.GetComponent<Circle>();
                                            square_match_circles[1] = left_1_circle.GetComponent<Circle>();
                                            square_match_circles[2] = bot_1_circle.GetComponent<Circle>();
                                            square_match_circles[3] = circles_on_panel[j - 1, i - 1].GetComponent<Circle>();
                                        }
                                        else
                                            square_match_count = 1;
                                    }
                                }
                            }
                        }
                        

                    }
                    
                    
                }
                if (right_1_circle != null) //checking right top and bot square matches check
                {
                    if (current_circle.CompareTag(right_1_circle.tag)) // right top sqaure match check
                    {
                        square_match_count++;

                        if (top_1_circle != null)
                        {
                            if (current_circle.CompareTag(top_1_circle.tag))
                            {
                                square_match_count++;
                                if (circles_on_panel[j + 1, i + 1] != null)
                                {
                                    if (current_circle.CompareTag(circles_on_panel[j + 1, i + 1].tag))
                                    {
                                        square_match_count++;
                                        square_match_circles[0] = current_circle.GetComponent<Circle>();
                                        square_match_circles[1] = right_1_circle.GetComponent<Circle>();
                                        square_match_circles[2] = top_1_circle.GetComponent<Circle>();
                                        square_match_circles[3] = circles_on_panel[j + 1, i + 1].GetComponent<Circle>();
                                    }
                                    else
                                        square_match_count = 1;

                                }
                            }
                        }
                        if (bot_1_circle != null) // right bot square match check
                        {
                            if (current_circle.CompareTag(bot_1_circle.tag))
                            {
                                square_match_count++;
                                if (circles_on_panel[j + 1, i - 1] != null)
                                {
                                    if (current_circle.CompareTag(circles_on_panel[j + 1, i - 1].tag))
                                    {
                                        square_match_count++;
                                        square_match_circles[0] = current_circle.GetComponent<Circle>();
                                        square_match_circles[1] = right_1_circle.GetComponent<Circle>();
                                        square_match_circles[2] = bot_1_circle.GetComponent<Circle>();
                                        square_match_circles[3] = circles_on_panel[j + 1, i - 1].GetComponent<Circle>();
                                    }
                                    else
                                        square_match_count = 1;
                                }
                            }
                        }
                    }
                    
                }


                if (horizontal_match_count >= 3)
                {
                    there_is_match = true;
                    if (left_1_circle != null)
                    {
                        if (circles_on_panel[j,i].CompareTag(left_1_circle.tag))
                        {
                            left_1_circle.GetComponent<Circle>().SetMatch(true);
                            if (left_2_circle != null)
                            {
                                if (circles_on_panel[j, i].CompareTag(left_2_circle.tag))
                                {
                                    left_2_circle.GetComponent<Circle>().SetMatch(true);
                                }
                            }
                        }
                    }

                    if (right_1_circle != null)
                    {
                        if (circles_on_panel[j, i].CompareTag(right_1_circle.tag))
                        {
                            right_1_circle.GetComponent<Circle>().SetMatch(true);
                            if (right_2_circle != null)
                            {
                                if (circles_on_panel[j, i].CompareTag(right_2_circle.tag))
                                {
                                    right_2_circle.GetComponent<Circle>().SetMatch(true);
                                }
                            }
                        }
                    }
                    circles_on_panel[j, i].GetComponent<Circle>().SetMatch(true);
                }

                if (vertical_match_count >= 3)
                {
                    there_is_match = true;
                    if (top_1_circle != null)
                    {
                        if (circles_on_panel[j, i].CompareTag(top_1_circle.tag))
                        {
                            top_1_circle.GetComponent<Circle>().SetMatch(true);
                            if (top_2_circle != null)
                            {
                                if (circles_on_panel[j, i].CompareTag(top_2_circle.tag))
                                {
                                    top_2_circle.GetComponent<Circle>().SetMatch(true);
                                }
                            }
                        }
                    }
                    if (bot_1_circle != null)
                    {
                        if (circles_on_panel[j, i].CompareTag(bot_1_circle.tag))
                        {
                            bot_1_circle.GetComponent<Circle>().SetMatch(true);
                            if (bot_2_circle != null)
                            {
                                if (circles_on_panel[j, i].CompareTag(bot_2_circle.tag))
                                {
                                    bot_2_circle.GetComponent<Circle>().SetMatch(true);
                                }
                            }
                        }
                    }
                    circles_on_panel[j, i].GetComponent<Circle>().SetMatch(true);
                }


                if (square_match_count >= 4)
                {
                    for(int s = 0; s < 4; s++)
                    {
                        square_match_circles[s].SetMatch(true);
                        there_is_match = true;
                        
                    }
                }
            }
           
        }
        return there_is_match;
        
    }



    public void Init()
    {
        circles_on_panel = new GameObject[width, height];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++)
            {
                GameObject temp_tile = GameObject.Instantiate(background_tile, new Vector3(j, i,0.1f), Quaternion.identity) as GameObject;
                temp_tile.transform.parent = this.transform;
                temp_tile.name = "(" + j + " ," + i + ")";

                GameObject random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(j, i), Quaternion.identity) as GameObject;
                new_circle.GetComponent<Circle>().SetPosition(new Vector2(j, i));
                circles_on_panel[j, i] = new_circle;
                new_circle.transform.parent = this.transform;
            }
        }

        CheckMatch();

    }
}
