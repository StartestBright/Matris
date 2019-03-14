using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour  {
    int x_position, y_position;
    private Vector2 touch_position, release_position;
    private float swipe_angle=0;
    private float swipe_resist = 5f;
    private GamePanel_Controller gamepanel;
    private bool matched = false;
    private bool can_fall = false;
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
        }
        
        if (matched)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(44, 11, 35);
            //gamepanel.DestroyMatchAt(x_position, y_position);
        }
        /*
        if (y_position > 0)
        {
            if (gamepanel.circles_on_panel[x_position, y_position - 1] == null)
                can_fall = true;
            else
            {
                can_fall = false;
            }
        }

        if (can_fall)
        {
            CircleFalling();
        }
        */
        
    }
    private void OnMouseDown()
    {
        touch_position = Camera.main.WorldToScreenPoint( Input.mousePosition);
    }
    private void OnMouseUp()
    {
        release_position = Camera.main.WorldToScreenPoint(Input.mousePosition);
        swipe_angle= Mathf.Atan2((release_position.y - touch_position.y) , (release_position.x - touch_position.x))/Mathf.PI *180;

        if (Mathf.Abs(release_position.x - touch_position.x) > swipe_resist || Mathf.Abs(release_position.y - touch_position.y) > swipe_resist)
        {

            GameObject other_circle = null;
            //Vector2 destination_point = this.transform.position;

            if (swipe_angle <= 45 && swipe_angle > -45 && x_position < gamepanel.width - 1) // swipe right
            {
                other_circle = gamepanel.circles_on_panel[x_position + 1, y_position];
                other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));

                //swapping circles in the array in the game panel
                gamepanel.circles_on_panel[x_position + 1, y_position] = gamepanel.circles_on_panel[x_position, y_position];
                gamepanel.circles_on_panel[x_position, y_position] = other_circle;

                this.x_position += 1;
            }
            else if (swipe_angle <= -45 && swipe_angle > -135 && y_position > 0) //swipe bottom
            {
                other_circle = gamepanel.circles_on_panel[x_position, y_position - 1];
                other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));
                //swapping circles in the array in the game panel
                gamepanel.circles_on_panel[x_position, y_position - 1] = gamepanel.circles_on_panel[x_position, y_position];
                gamepanel.circles_on_panel[x_position, y_position] = other_circle;

                this.y_position -= 1;
            }
            else if (swipe_angle >= Mathf.Abs(135) && x_position > 0) //swipe left
            {
                other_circle = gamepanel.circles_on_panel[x_position - 1, y_position];
                other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));

                //swapping circles in the array in the game panel
                gamepanel.circles_on_panel[x_position - 1, y_position] = gamepanel.circles_on_panel[x_position, y_position];
                gamepanel.circles_on_panel[x_position, y_position] = other_circle;

                this.x_position -= 1;
            }
            else if (swipe_angle < 135 && swipe_angle > 45 && y_position < gamepanel.height) //swipe top
            {
              

                other_circle = gamepanel.circles_on_panel[x_position, y_position + 1];

                //swapping circles in the array in the game panel
                gamepanel.circles_on_panel[x_position, y_position + 1] = gamepanel.circles_on_panel[x_position, y_position];
                gamepanel.circles_on_panel[x_position, y_position] = other_circle;

                other_circle.GetComponent<Circle>().SetPosition(new Vector2(x_position, y_position));
                this.y_position += 1;
            }

            gamepanel.CheckAllMatch();
            if (other_circle != null)
            {
                StartCoroutine(MatchCheckToReturnBack(other_circle));
            }
        }
        

        
    }
    private IEnumerator MatchCheckToReturnBack(GameObject other_circle)
    {
        yield return new WaitForSeconds(0.3f);
        if (other_circle != null)//if it is not already matched so destroied
            if (matched == false && other_circle.GetComponent<Circle>().getMatched() == false)
            {

                //gamepanel.circles_on_panel[x_position,y_position]
                float other_x_origin = other_circle.GetComponent<Circle>().GetPosition().x; //where this circle should go to
                float other_y_origin = other_circle.GetComponent<Circle>().GetPosition().y; //where this circle should go to
                float this_x_origin = GetPosition().x; //where the other circle should go to
                float this_y_origin = GetPosition().y; //where the other circle should go to

                GameObject other_temp_circle = gamepanel.circles_on_panel[(int)other_x_origin, (int)other_y_origin];

                gamepanel.circles_on_panel[(int)other_x_origin, (int)other_y_origin] = gamepanel.circles_on_panel[(int)GetPosition().x, (int)GetPosition().y]; //set this circle to it's original position in the array before swap
                gamepanel.circles_on_panel[(int)this_x_origin, (int)this_y_origin] = other_temp_circle;  //set the other circle to it's original position in the array before swap

                other_circle.GetComponent<Circle>().SetPosition(new Vector2(GetPosition().x, GetPosition().y));
                SetPosition(new Vector2(other_x_origin, other_y_origin));
            }
            else
            {
                gamepanel.DestroyMatches();
                gamepanel.RefillGamePanel();
            }
    }


    public void CircleFalling()
    {
        if (y_position > 0) { 
            if (gamepanel.circles_on_panel[x_position, y_position - 1] == null)
            {
                gamepanel.circles_on_panel[x_position, y_position - 1] = gamepanel.circles_on_panel[x_position, y_position];
                gamepanel.circles_on_panel[x_position, y_position] = null;
                y_position -= 1;
            }
            
            //gamepanel.StartCoroutine(gamepanel.RefillGamePanelAt(x_position));
            if (y_position > 0)
                if (gamepanel.circles_on_panel[x_position, y_position - 1] != null)
                {
                    gamepanel.CheckAllMatch();
                }
            }
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

    public bool getMatched()
    {
        return matched;
    }
}
