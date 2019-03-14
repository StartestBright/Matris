using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel_Controller : MonoBehaviour {
    public int width, height;
    public GameObject background_tile;
    public GameObject[] circle_types;
    public GameObject[,] circles_on_panel;

    private bool init_match_checking = true;

    
    // Use this for initialization
    void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
	}

    

    private bool CheckMatchAt(int x,int y)
    {
        int horizontal_match_count = 1;
        int vertical_match_count = 1;
        int square_match_count = 1;
        GameObject current_circle = circles_on_panel[x, y];
        GameObject left_1_circle = null, left_2_circle = null, right_1_circle = null, right_2_circle = null;
        GameObject top_1_circle = null, top_2_circle = null, bot_1_circle = null, bot_2_circle = null;
        Circle[] square_match_circles = new Circle[4];

        if (x > 0)
            left_1_circle = circles_on_panel[x - 1, y];
        if (x > 1)
            left_2_circle = circles_on_panel[x - 2, y];
        if (x < width - 1)
            right_1_circle = circles_on_panel[x + 1, y];
        if (x < width - 2)
            right_2_circle = circles_on_panel[x + 2, y];
        if (y < height - 1)
            top_1_circle = circles_on_panel[x, y + 1];
        if (y < height - 2)
            top_2_circle = circles_on_panel[x, y + 2];
        if (y > 0)
            bot_1_circle = circles_on_panel[x, y - 1];
        if (y > 1)
            bot_2_circle = circles_on_panel[x, y - 2];


        if (left_1_circle != null &&current_circle!=null)
        {
            if (left_1_circle.CompareTag(current_circle.tag)) //Checking left side match
            {
                horizontal_match_count++;

                if (left_2_circle != null && current_circle != null)
                {
                    if ((left_2_circle.CompareTag(current_circle.tag)))
                    {
                        horizontal_match_count++;
                    }
                }
            }
        }

        if (right_1_circle != null && current_circle != null)
        {
            if ((right_1_circle.CompareTag(current_circle.tag))) //Checking right side match
            {
                horizontal_match_count++;
                if (right_2_circle != null && current_circle != null)
                {
                    if ((right_2_circle.CompareTag(current_circle.tag)))
                    {
                        horizontal_match_count++;
                    }
                }
            }
        }


        if (top_1_circle != null && current_circle != null)
        {
            if (top_1_circle.CompareTag(current_circle.tag)) //Checking top match
            {
                vertical_match_count++;
                if (top_2_circle != null && current_circle != null)
                {
                    if (top_2_circle.CompareTag(current_circle.tag))
                    {
                        vertical_match_count++;
                    }
                }
            }
        }

        if (bot_1_circle != null && current_circle != null)
        {
            if (bot_1_circle.CompareTag(current_circle.tag)) //Checking bottom match
            {
                vertical_match_count++;
                if (bot_2_circle != null && current_circle != null)
                {
                    if (bot_2_circle.CompareTag(current_circle.tag))
                    {
                        vertical_match_count++;
                    }
                }
            }
        }

        if (left_1_circle != null && current_circle != null) //left top and bot square matches check
        {

            if (current_circle.CompareTag(left_1_circle.tag))
            { //
                if (current_circle.CompareTag(left_1_circle.tag))
                {
                    square_match_count++;
                    if (top_1_circle != null && current_circle != null) //left top square match check
                    {
                        if (current_circle.CompareTag(top_1_circle.tag))
                        {
                            square_match_count++;
                            if (circles_on_panel[x - 1, y + 1] != null && current_circle != null)
                            {  //can cause PROBLEM INDEX OUT OF ARRAY!!!
                                if (current_circle.CompareTag(circles_on_panel[x - 1, y + 1].tag))
                                {
                                    square_match_count++;
                                    square_match_circles[0] = current_circle.GetComponent<Circle>();
                                    square_match_circles[1] = left_1_circle.GetComponent<Circle>();
                                    square_match_circles[2] = top_1_circle.GetComponent<Circle>();
                                    square_match_circles[3] = circles_on_panel[x - 1, y + 1].GetComponent<Circle>();
                                }
                                else
                                    square_match_count = 2; // current and left1 matches
                            }
                        }
                    }
                    if (bot_1_circle != null && current_circle != null) //left bot square match check
                    {
                        if (current_circle.CompareTag(bot_1_circle.tag))
                        {
                            square_match_count++;
                            if (circles_on_panel[x - 1, y - 1] != null && current_circle != null)
                            {
                                if (current_circle.CompareTag(circles_on_panel[x - 1, y - 1].tag))
                                {
                                    square_match_count++;
                                    square_match_circles[0] = current_circle.GetComponent<Circle>();
                                    square_match_circles[1] = left_1_circle.GetComponent<Circle>();
                                    square_match_circles[2] = bot_1_circle.GetComponent<Circle>();
                                    square_match_circles[3] = circles_on_panel[x - 1, y - 1].GetComponent<Circle>();
                                }
                                else
                                    square_match_count = 1;
                            }
                        }
                    }
                }


            }


        }
        if (right_1_circle != null && current_circle != null) //checking right top and bot square matches check
        {
            if (current_circle.CompareTag(right_1_circle.tag)) // right top sqaure match check
            {
                square_match_count++;

                if (top_1_circle != null && current_circle != null)
                {
                    if (current_circle.CompareTag(top_1_circle.tag))
                    {
                        square_match_count++;
                        if (circles_on_panel[x + 1, y + 1] != null && current_circle != null)
                        {
                            if (current_circle.CompareTag(circles_on_panel[x + 1, y + 1].tag))
                            {
                                square_match_count++;
                                square_match_circles[0] = current_circle.GetComponent<Circle>();
                                square_match_circles[1] = right_1_circle.GetComponent<Circle>();
                                square_match_circles[2] = top_1_circle.GetComponent<Circle>();
                                square_match_circles[3] = circles_on_panel[x + 1, y + 1].GetComponent<Circle>();
                            }
                            else
                                square_match_count = 1;

                        }
                    }
                }
                if (bot_1_circle != null && current_circle != null) // right bot square match check
                {
                    if (current_circle.CompareTag(bot_1_circle.tag))
                    {
                        square_match_count++;
                        if (circles_on_panel[x + 1, y- 1] != null && current_circle != null)
                        {
                            if (current_circle.CompareTag(circles_on_panel[x + 1, y- 1].tag))
                            {
                                square_match_count++;
                                square_match_circles[0] = current_circle.GetComponent<Circle>();
                                square_match_circles[1] = right_1_circle.GetComponent<Circle>();
                                square_match_circles[2] = bot_1_circle.GetComponent<Circle>();
                                square_match_circles[3] = circles_on_panel[x + 1, y- 1].GetComponent<Circle>();
                            }
                            else
                                square_match_count = 1;
                        }
                    }
                }
            }

        }

        if (!init_match_checking)
        {
            if (horizontal_match_count >= 3)
            {
                //there_is_match = true;
                if (left_1_circle != null && current_circle != null)
                {
                    if (current_circle.CompareTag(left_1_circle.tag))
                    {
                        left_1_circle.GetComponent<Circle>().SetMatch(true);
                        if (left_2_circle != null && current_circle != null)
                        {
                            if (current_circle.CompareTag(left_2_circle.tag))
                            {
                                left_2_circle.GetComponent<Circle>().SetMatch(true);
                            }
                        }
                    }
                }

                if (right_1_circle != null && current_circle != null)
                {
                    if (current_circle.CompareTag(right_1_circle.tag))
                    {
                        right_1_circle.GetComponent<Circle>().SetMatch(true);
                        if (right_2_circle != null && current_circle != null)
                        {
                            if (current_circle.CompareTag(right_2_circle.tag))
                            {
                                right_2_circle.GetComponent<Circle>().SetMatch(true);
                            }
                        }
                    }
                }
                current_circle.GetComponent<Circle>().SetMatch(true);
            }

            if (vertical_match_count >= 3)
            {
                //there_is_match = true;
                if (top_1_circle != null && current_circle != null)
                {
                    if (current_circle.CompareTag(top_1_circle.tag))
                    {
                        top_1_circle.GetComponent<Circle>().SetMatch(true);
                        if (top_2_circle != null && current_circle != null)
                        {
                            if (current_circle.CompareTag(top_2_circle.tag))
                            {
                                top_2_circle.GetComponent<Circle>().SetMatch(true);
                            }
                        }
                    }
                }
                if (bot_1_circle != null && current_circle != null)
                {
                    if (current_circle.CompareTag(bot_1_circle.tag))
                    {
                        bot_1_circle.GetComponent<Circle>().SetMatch(true);
                        if (bot_2_circle != null && current_circle != null)
                        {
                            if (current_circle.CompareTag(bot_2_circle.tag))
                            {
                                bot_2_circle.GetComponent<Circle>().SetMatch(true);
                            }
                        }
                    }
                }
                current_circle.GetComponent<Circle>().SetMatch(true);
            }


            if (square_match_count >= 4)
            {
                for (int s = 0; s < 4; s++)
                {
                    square_match_circles[s].SetMatch(true);
                    //there_is_match = true;

                }
            }
        }
        if ((horizontal_match_count >= 3 || vertical_match_count >= 3 || square_match_count >= 4))
        {
            return true;
        }
        else
            return false;
            
    }
    /*
    private void CircleFallingCheck()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                int null_count = 0;
                if (i > 0)
                {
                    int temp = i;
                    
                    if (circles_on_panel[j,temp]!=null )
                    {
                        while (temp > 0)
                        {
                            null_count++;
                            temp--;
                        }
                    }
                    
                    if(circles_on_panel[j,i]!=null &&null_count>0)
                        StartCoroutine( CircleFalling(j,i,null_count));
                }

            }
        }
    }
    */
    /*
    private IEnumerator CircleFalling(int x,int y,int nullcount)
    {
      
            while (nullcount > 0 )
            {
                yield return new WaitForSeconds(.4f);
                Debug.Log(circles_on_panel[x, y] + " " + x + " " + y);
                circles_on_panel[x, y].GetComponent<Circle>().SetPosition(new Vector2(x, y - 1));
                circles_on_panel[x, y - 1] = circles_on_panel[x, y];
                circles_on_panel[x, y] = null;
            }
        
    }
    */
    public bool CheckAllMatch()
    {
        bool match_exists = false;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (CheckMatchAt(j, i))
                    match_exists = true;
            }
        }
        return match_exists;
        
    }
    public void RefillGamePanel() //after swap -> destroy all matches -> Refill
    {
        for (int i = 0; i < width; i++)
        {
            int refill_count = 0;
            for(int j = 0; j < height; j++)
            {
                if (circles_on_panel[i, j] != null)
                {
                    Circle current_circle = circles_on_panel[i, j].GetComponent<Circle>();
                    if (j > 0)
                    {
                        int null_count = 0;
                        for (int c = 0; c < j; c++)
                        {
                            if (circles_on_panel[i, c] == null)
                            {
                                null_count++;
                            }
                        }
                        if (null_count > 0)
                        {
                            current_circle.SetPosition(new Vector2(i, j - null_count));
                            circles_on_panel[i, j - null_count] = current_circle.gameObject;
                            circles_on_panel[i, j] = null;
                        }
                    }
                }
                
                
            }
            for (int y = 0; y < height; y++)
            {
                if (circles_on_panel[i, y] == null)
                {
                    refill_count++;
                }
            }
            
            for (int n = refill_count; n >0; n--)
            {
                GameObject random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(i, height-1), Quaternion.identity) as GameObject;
                circles_on_panel[i, height - 1] = new_circle;
                new_circle.transform.parent = this.transform;
                new_circle.GetComponent<Circle>().SetPosition(new Vector2(i, height-n));
                circles_on_panel[i, height-n] = new_circle;
            }
            
        }
        //Checking matches again after refill then destroy and refill if match exists
        bool match_exist = CheckAllMatch();
        DestroyMatches();
        Debug.Log(match_exist);
        if (match_exist)
        {
            RefillGamePanel();
            Debug.Log("exist");
        }
    }
    /*
    public IEnumerator RefillGamePanelAt(int x_position)
    {
       
        if (circles_on_panel[x_position, height - 1] == null)
        { 
            GameObject random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
            circles_on_panel[x_position, height - 1] = random_type_circle;
            while (CheckMatchAt(x_position, height - 1))
            {
                random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                circles_on_panel[x_position, height - 1] = random_type_circle;
            }
            GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(x_position, height - 1), Quaternion.identity) as GameObject;
            new_circle.transform.parent = this.transform;
            new_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, height - 1));
            circles_on_panel[x_position, height - 1] = new_circle;

        }
        CheckAllMatch(); // To check if there are matches after destroy and refill the circles
        yield return new WaitForSeconds(.2f);
    }
    */

    public void DestroyMatchAt(int x,int y)
    {
        Destroy(circles_on_panel[x, y]);
        circles_on_panel[x,y] = null;
        //RefillGamePanelAt(x);
        
    }
    public void DestroyMatches()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (circles_on_panel[j, i] != null)
                    if (circles_on_panel[j, i].GetComponent<Circle>().getMatched())
                    {
                        DestroyMatchAt(j, i);
                    }
                
            }
        }
    }


    public void Init()
    {
        circles_on_panel = new GameObject[width, height];

        
        
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    init_match_checking = true;
                    GameObject temp_tile = GameObject.Instantiate(background_tile, new Vector3(j, i, 0.1f), Quaternion.identity) as GameObject;
                    temp_tile.transform.parent = this.transform;
                    temp_tile.name = "(" + j + " ," + i + ")";

                    GameObject random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                    circles_on_panel[j, i] = random_type_circle;
                    while (CheckMatchAt(j, i))
                    {
                        random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                        circles_on_panel[j, i] = random_type_circle;
                    }
                GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(j, i), Quaternion.identity) as GameObject;
                new_circle.transform.parent = this.transform;
                new_circle.GetComponent<Circle>().SetPosition(new Vector2(j, i));
                circles_on_panel[j, i] = new_circle;
                init_match_checking = false;
                }
            }
    }
    
}
