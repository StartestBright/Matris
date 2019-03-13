using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour  {
    int x_position, y_position;
    private Vector2 touch_position, release_position;
    private float swipe_angle;
    private GamePanel_Controller gamepanel;
    private bool matched = false;
    private int horizontal_match_count,vertical_match_count,square_match_count;

	// Use this for initialization
	void Start () {
        gamepanel = FindObjectOfType<GamePanel_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x != x_position || transform.position.y != y_position)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(x_position, y_position),.4f);
            //transform.position = new Vector2(x_position, y_position);

            //Mathf.Lerp(transform.position.x,x_position, 0.4f);
            //Mathf.Lerp(transform.position.y,y_position, 0.4f);
        }

        //transform.name = "( " + x_position + ", " + y_position + " )";

        if (matched)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0, 0, 0);
            matched = false;
            //Debug.Log("matching");
        }
        
    }
    private void OnMouseDown()
    {
        touch_position = Camera.main.WorldToScreenPoint( Input.mousePosition);
    }
    private void OnMouseUp()
    {
        release_position = Camera.main.WorldToScreenPoint(Input.mousePosition);
        swipe_angle= Mathf.Atan2((release_position.y - touch_position.y) , (release_position.x - touch_position.x))/Mathf.PI *180;

        GameObject other_circle = null;
        //Vector2 destination_point = this.transform.position;
        if (swipe_angle<=45 && swipe_angle > -45 && x_position <gamepanel.width-1) // swipe right
        {
            other_circle = gamepanel.circles_on_panel[x_position + 1, y_position];
            other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));

            //swapping circles in the array in the game panel
            gamepanel.circles_on_panel[x_position + 1, y_position] = gamepanel.circles_on_panel[x_position, y_position];
            gamepanel.circles_on_panel[x_position, y_position] = other_circle;
            
            this.x_position += 1;
            


            /*destination_point = other_circle.transform.position;
            Mathf.Lerp(other_circle.transform.position.x, transform.position.x, 0.4f);
            Mathf.Lerp(other_circle.transform.position.y, transform.position.y, 0.4f);

            Mathf.Lerp(transform.position.x, destination_point.x, 0.4f);
            Mathf.Lerp(transform.position.y, destination_point.y, 0.4f);*/

        }
        else if(swipe_angle <= -45 && swipe_angle > -135 && y_position>0) //swipe bottom
        {
            other_circle = gamepanel.circles_on_panel[x_position, y_position-1];
            other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));

            //swapping circles in the array in the game panel
            gamepanel.circles_on_panel[x_position, y_position-1] = gamepanel.circles_on_panel[x_position, y_position];
            gamepanel.circles_on_panel[x_position, y_position] = other_circle;

            this.y_position -= 1;
        }
        else if(swipe_angle >= Mathf.Abs(135) && x_position >0) //swipe left
        {
            other_circle = gamepanel.circles_on_panel[x_position - 1, y_position];
            other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));

            //swapping circles in the array in the game panel
            gamepanel.circles_on_panel[x_position - 1, y_position] = gamepanel.circles_on_panel[x_position, y_position];
            gamepanel.circles_on_panel[x_position, y_position] = other_circle;

            this.x_position -= 1;
        }
        else if(swipe_angle <135 && swipe_angle >45 && y_position < gamepanel.height) //swipe top
        {


            other_circle = gamepanel.circles_on_panel[x_position, y_position+1];

            //swapping circles in the array in the game panel
            gamepanel.circles_on_panel[x_position, y_position+1] = gamepanel.circles_on_panel[x_position, y_position];
            gamepanel.circles_on_panel[x_position, y_position] = other_circle;

            other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));
            this.y_position += 1;
        }

        CheckMatch();
    }

    private void CheckMatch()
    {
        gamepanel.CheckMatch();
        /*
        horizontal_match_count = 1;
        vertical_match_count = 1;
        square_match_count = 0;

        GameObject left_1_circle=null, left_2_circle = null, right_1_circle = null, right_2_circle = null;
        GameObject top_1_circle = null, top_2_circle = null, bot_1_circle = null, bot_2_circle = null;

        if(x_position>0)
            left_1_circle = gamepanel.circles_on_panel[x_position - 1, y_position];
        if(x_position>1)
            left_2_circle = gamepanel.circles_on_panel[x_position - 2, y_position];
        if(x_position<gamepanel.width-1)
            right_1_circle = gamepanel.circles_on_panel[x_position + 1, y_position];
        if(x_position<gamepanel.width-2)
            right_2_circle = gamepanel.circles_on_panel[x_position + 2, y_position];
        if(y_position<gamepanel.height-1)
            top_1_circle = gamepanel.circles_on_panel[x_position, y_position + 1];
        if(y_position<gamepanel.height-2)
            top_2_circle = gamepanel.circles_on_panel[x_position, y_position + 2];
        if(y_position>0)
            bot_1_circle = gamepanel.circles_on_panel[x_position, y_position - 1];
        if(y_position>1)
            bot_2_circle = gamepanel.circles_on_panel[x_position, y_position - 2];


        if (left_1_circle != null) {

            if (left_1_circle.CompareTag(this.tag)) //Checking left side match
        {
                horizontal_match_count++;

                if (left_2_circle != null)
                {
                    if ((left_2_circle.CompareTag(this.tag)))
                    {
                        horizontal_match_count++;
                    }
                }
            }
        }

        if (right_1_circle != null)
        {
                if ((right_1_circle.CompareTag(this.tag))) //Checking right side match
                {
                    horizontal_match_count++;
                    if (right_2_circle != null)
                    {
                        if ((right_2_circle.CompareTag(this.tag)))
                        {
                            horizontal_match_count++;
                        }
                    }
                }
        }


        if (top_1_circle != null)
        {
            if (top_1_circle.CompareTag(this.tag)) //Checking top match
            {
                vertical_match_count++;
                if (top_2_circle != null)
                {
                    if ( top_2_circle.CompareTag(this.tag))
                    {
                        vertical_match_count++;
                    }
                }
            }
        }

        if (bot_1_circle != null)
        {
            if (bot_1_circle.CompareTag(this.tag)) //Checking bottom match
            {
                vertical_match_count++;
                if (bot_2_circle != null)
                {
                    if (bot_2_circle.CompareTag(this.tag))
                    {
                        vertical_match_count++;
                    }
                }
            }
        }

    
        if (horizontal_match_count>=3)
        {
            if(left_1_circle != null){
                if (CompareTag(left_1_circle.tag))
                {
                    left_1_circle.GetComponent<Circle>().SetMatch(true);
                    if (left_2_circle != null)
                    {
                        if (CompareTag(left_2_circle.tag))
                        {
                            left_2_circle.GetComponent<Circle>().SetMatch(true);
                        }
                    }
                }
            }

            if (right_1_circle != null)
            {
                if (CompareTag(right_1_circle.tag))
                {
                    right_1_circle.GetComponent<Circle>().SetMatch(true);
                    if (right_2_circle != null)
                    {
                        if (CompareTag(right_2_circle.tag))
                        {
                            right_2_circle.GetComponent<Circle>().SetMatch(true);
                        }
                    }
                }
            }
            matched = true;
        }

        if (vertical_match_count >= 3)
        {
            if (top_1_circle != null)
            {
                if (CompareTag(top_1_circle.tag))
                {
                    top_1_circle.GetComponent<Circle>().SetMatch(true);
                    if (top_2_circle != null)
                    {
                        if (CompareTag(top_2_circle.tag))
                        {
                            top_2_circle.GetComponent<Circle>().SetMatch(true);
                        }
                    }
                }
            }
            if (bot_1_circle != null)
            {
                if (CompareTag(bot_1_circle.tag))
                {
                    bot_1_circle.GetComponent<Circle>().SetMatch(true);
                    if (bot_2_circle != null)
                    {
                        if (CompareTag(bot_2_circle.tag))
                        {
                            bot_2_circle.GetComponent<Circle>().SetMatch(true);
                        }
                    }
                }
            }
            matched = true;
        }
        //Debug.Log("L1 = "+left_1_circle + "\n" +"L2: "+left_2_circle + "\nT1 = " + top_1_circle + "\nT2 = " + top_2_circle+"\nR1 = " + right_1_circle + "\nR2 = " + right_2_circle + "\nB1 = " + bot_1_circle + "\nB2 = " + bot_2_circle+"\n verti = "+vertical_match_count+"\n hori = "+horizontal_match_count);
        */
    }
    

    public void SetMatch(bool match)
    {
        this.matched = match;
    }

    public void SetPosition(Vector2 pos)
    {
        this.x_position = (int)pos.x;
        this.y_position = (int)pos.y;
    }
    public Vector2 GetPosition()
    {
        return new Vector2(x_position, y_position);
    }
}
