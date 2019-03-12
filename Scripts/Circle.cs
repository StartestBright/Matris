using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour  {
    int x_position, y_position;
    private Vector2 touch_position, release_position;
    private float swipe_angle;
    private GamePanel_Controller gamepanel;

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
