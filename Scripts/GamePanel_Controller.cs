using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel_Controller : MonoBehaviour {
    private bool game_over = false;
    private bool mission_clear = false;
    private bool init_match_checking = true;
    private bool can_move = true;
    private bool match_hint_on = false;
    
    private Slider fx_volume_slider;
    private Slider bgm_volume_slider;
    private bool setting_panel_on = false;
    private int move_left;
    private int goal1_left, goal2_left, goal3_left;
    private Color[] picked_goal_color;
    private GameObject[,] tiles;
    private AudioSource matching_sound;
    private AudioSource shuffle_sound;
    private AudioSource bgm;
    private float bug_fix_timer=0;
    private float match_hint_timer = 0;
    private float match_hink_timer_delay = 5f;


    int[] type_index;

    public int width, height;
    public GameObject background_tile;
    public GameObject[] circle_types;
    public GameObject[,] circles_on_panel;
    public GameObject settings_panel;
    
    

    public Text move_left_text;
    public Image goal1, goal2, goal3;
    public Text game_over_text;
    public Button restart_bt;
   


    void Start () {
        
        matching_sound = GetComponents<AudioSource>()[0];
        if (transform.tag != "Sub_Board")
        {
            shuffle_sound = GetComponents<AudioSource>()[1];
            bgm = GetComponents<AudioSource>()[2];
            if (bgm != null)
                bgm.Play();
        }
        Init();
        CheckToShuffle();
    }
	
	void Update () {
        PlayerPrefs.SetFloat("fx_volume", fx_volume_slider.value);
        PlayerPrefs.SetFloat("bgm_volume", bgm_volume_slider.value);

        if (setting_panel_on)
            settings_panel.SetActive(true);
        else
            settings_panel.SetActive(false);

        if (Input.anyKey)
        if(matching_sound!=null)
            matching_sound.volume = PlayerPrefs.GetFloat("fx_volume");
        if(shuffle_sound!=null)
            shuffle_sound.volume = PlayerPrefs.GetFloat("fx_volume");
        if(bgm!=null)
            bgm.volume = PlayerPrefs.GetFloat("bgm_volume");
        if (transform.tag != "Sub_Board")
        {
            goal1.GetComponentInChildren<Text>().text = "" + goal1_left;
            goal2.GetComponentInChildren<Text>().text = "" + goal2_left;
            goal3.GetComponentInChildren<Text>().text = "" + goal3_left;
            move_left_text.text = "Move left: " + move_left;

            if (game_over)
            {
                if (mission_clear)
                    game_over_text.text = "Great! Mission Clear!";
                else
                    game_over_text.text = "Game Over";
                game_over_text.enabled = true;
                restart_bt.enabled = true;
                restart_bt.GetComponentInChildren<Text>().enabled = true;
                restart_bt.GetComponentInChildren<Image>().enabled = true;
            }
        }
            bug_fix_timer += Time.deltaTime;
            if (bug_fix_timer > 2f)
            {
                setCanMove(true);
            }

        match_hint_timer += Time.deltaTime;
        if (match_hint_timer > match_hink_timer_delay)
        {
            match_hint_on = true;
        }
        else
            match_hint_on = false;
       
        
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
        {
            return false;
        }
            
    }
   
    public void setCanMove(bool can)
    {
        can_move = can;
    }

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
        bug_fix_timer = 0;
        return match_exists;
        
    }
    public void RefillGamePanel() 
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
                GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(i, height+5), Quaternion.identity) as GameObject;
                circles_on_panel[i, height - 1] = new_circle;
                new_circle.transform.parent = this.transform;
                new_circle.GetComponent<Circle>().SetPosition(new Vector2(i, height-n));
                circles_on_panel[i, height-n] = new_circle;
            }
            
        }
        //Checking matches again after refill then destroy and refill if match exists
        bool match_exist = CheckAllMatch();
        DestroyMatches(true);
        if (match_exist)
        {
            RefillGamePanel();
        }
        
    }
    

    public void DestroyMatchAt(int x,int y,bool shuffle)
    {
        if(!shuffle)
            ReduceGoal(circles_on_panel[x, y].tag);
        Destroy(circles_on_panel[x, y]);
        circles_on_panel[x,y] = null;
        if(!shuffle)
            matching_sound.Play();
        //RefillGamePanelAt(x);
        match_hint_timer = 0;
        

    }
    public void DestroyMatches(bool shuffle)
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (circles_on_panel[j, i] != null)
                    if (circles_on_panel[j, i].GetComponent<Circle>().getMatched())
                    {
                        DestroyMatchAt(j, i,shuffle);
                    }
                
            }
        }
        setCanMove(true);
        match_hint_timer = 0;
    }
    public bool getCanMove()
    {
        return can_move;
    }
    public bool CheckMatchExistAt(int j,int i)
    {
        init_match_checking = true;
        GameObject current_circle = circles_on_panel[j, i];
        GameObject other_circle;
        string other_circle_origin_tag = null;
        string current_circle_origin_tag = null;

        if (current_circle.GetComponent<Circle>().GetPosition().x > 0) //check left
        {
            other_circle = circles_on_panel[j - 1, i];
            other_circle_origin_tag = other_circle.tag; //new String?
            current_circle_origin_tag = current_circle.tag;

            other_circle.tag = current_circle.tag;
            current_circle.tag = other_circle_origin_tag;

            if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
            {
                other_circle.tag = other_circle_origin_tag;
                current_circle.tag = current_circle_origin_tag;
                init_match_checking = false;
                return true;
            }
            other_circle.tag = other_circle_origin_tag;
            current_circle.tag = current_circle_origin_tag;
        }
        if (current_circle.GetComponent<Circle>().GetPosition().y < height - 1)
        { //check top
            other_circle = circles_on_panel[j, i + 1];
            other_circle_origin_tag = other_circle.tag; //new String?
            current_circle_origin_tag = current_circle.tag;

            other_circle.tag = current_circle.tag;
            current_circle.tag = other_circle_origin_tag;

            if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
            {
                other_circle.tag = other_circle_origin_tag;
                current_circle.tag = current_circle_origin_tag;
                init_match_checking = false;
                return true;
            }
            other_circle.tag = other_circle_origin_tag;
            current_circle.tag = current_circle_origin_tag;
        }

        if (current_circle.GetComponent<Circle>().GetPosition().x < width - 1)
        { //check right
            other_circle = circles_on_panel[j + 1, i];
            other_circle_origin_tag = other_circle.tag; //new String?
            current_circle_origin_tag = current_circle.tag;

            other_circle.tag = current_circle.tag;
            current_circle.tag = other_circle_origin_tag;

            if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
            {
                other_circle.tag = other_circle_origin_tag;
                current_circle.tag = current_circle_origin_tag;
                init_match_checking = false;
                return true;
            }
            other_circle.tag = other_circle_origin_tag;
            current_circle.tag = current_circle_origin_tag;
        }
        if (current_circle.GetComponent<Circle>().GetPosition().y > 0)
        { //check bot
            other_circle = circles_on_panel[j, i - 1];
            other_circle_origin_tag = other_circle.tag; //new String?
            current_circle_origin_tag = current_circle.tag;

            other_circle.tag = current_circle.tag;
            current_circle.tag = other_circle_origin_tag;

            if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
            {
                other_circle.tag = other_circle_origin_tag;
                current_circle.tag = current_circle_origin_tag;
                init_match_checking = false;
                return true;
            }
            other_circle.tag = other_circle_origin_tag;
            current_circle.tag = current_circle_origin_tag;
        }
        init_match_checking = false;
        return false;
    }

    public bool CheckMatchExist() //Check if there are circles can match 3
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                init_match_checking = true;
                GameObject current_circle = circles_on_panel[j, i];
                GameObject other_circle;
                string other_circle_origin_tag = null;
                string current_circle_origin_tag = null;

                if (current_circle.GetComponent<Circle>().GetPosition().x > 0) //check left
                {
                    other_circle = circles_on_panel[j - 1, i];
                    other_circle_origin_tag = other_circle.tag; //new String?
                    current_circle_origin_tag = current_circle.tag;

                    other_circle.tag = current_circle.tag;
                    current_circle.tag = other_circle_origin_tag;

                    if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
                    {
                        other_circle.tag = other_circle_origin_tag;
                        current_circle.tag = current_circle_origin_tag;
                        init_match_checking = false;
                        return true;
                    }
                    other_circle.tag = other_circle_origin_tag;
                    current_circle.tag = current_circle_origin_tag;
                }
                if (current_circle.GetComponent<Circle>().GetPosition().y < height - 1)
                { //check top
                    other_circle = circles_on_panel[j, i + 1];
                    other_circle_origin_tag = other_circle.tag; //new String?
                    current_circle_origin_tag = current_circle.tag;

                    other_circle.tag = current_circle.tag;
                    current_circle.tag = other_circle_origin_tag;

                    if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
                    {
                        other_circle.tag = other_circle_origin_tag;
                        current_circle.tag = current_circle_origin_tag;
                        init_match_checking = false;
                        return true;
                    }
                    other_circle.tag = other_circle_origin_tag;
                    current_circle.tag = current_circle_origin_tag;
                }

                if (current_circle.GetComponent<Circle>().GetPosition().x < width - 1)
                { //check right
                    other_circle = circles_on_panel[j + 1, i];
                    other_circle_origin_tag = other_circle.tag; //new String?
                    current_circle_origin_tag = current_circle.tag;

                    other_circle.tag = current_circle.tag;
                    current_circle.tag = other_circle_origin_tag;

                    if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
                    {
                        other_circle.tag = other_circle_origin_tag;
                        current_circle.tag = current_circle_origin_tag;
                        init_match_checking = false;
                        return true;
                    }
                    other_circle.tag = other_circle_origin_tag;
                    current_circle.tag = current_circle_origin_tag;
                }
                if (current_circle.GetComponent<Circle>().GetPosition().y > 0)
                { //check bot
                    other_circle = circles_on_panel[j, i - 1];
                    other_circle_origin_tag = other_circle.tag; //new String?
                    current_circle_origin_tag = current_circle.tag;

                    other_circle.tag = current_circle.tag;
                    current_circle.tag = other_circle_origin_tag;

                    if (CheckMatchAt((int)other_circle.GetComponent<Circle>().GetPosition().x, (int)other_circle.GetComponent<Circle>().GetPosition().y))
                    {
                        other_circle.tag = other_circle_origin_tag;
                        current_circle.tag = current_circle_origin_tag;
                        init_match_checking = false;
                        return true;
                    }
                    other_circle.tag = other_circle_origin_tag;
                    current_circle.tag = current_circle_origin_tag;
                }
            }
        }
        init_match_checking = false;
        return false;
    }

    public void CheckToShuffle()
    {
        bool match_exist = CheckMatchExist();
        while (!match_exist)
        {
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    //if (circles_on_panel[j, i] != null)
                    {
                        circles_on_panel[j, i].GetComponent<Circle>().SetPosition(new Vector2(width / 2, height / 2));
                        //circles_on_panel[j,i]

                    } 
                    //yield return new WaitForSeconds(0.01f);
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    DestroyMatchAt(j, i,true);
                    circles_on_panel[j, i] = null;
                }
            }


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    init_match_checking = true;
                    GameObject random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                    circles_on_panel[j, i] = random_type_circle;
                    while (CheckMatchAt(j, i))
                    {
                        random_type_circle = circle_types[Random.Range(0, circle_types.Length)];
                        circles_on_panel[j, i] = random_type_circle;
                    }
                    GameObject new_circle = GameObject.Instantiate(random_type_circle, new Vector2(width / 2, height / 2), Quaternion.identity) as GameObject;
                    new_circle.transform.parent = this.transform;
                    new_circle.GetComponent<Circle>().SetPosition(new Vector2(j, i));
                    circles_on_panel[j, i] = new_circle;
                    init_match_checking = false;
                    //yield return new WaitForSeconds(0.01f);
                }
            }
            //yield return new WaitForSeconds(0.02f);

            match_exist = CheckMatchExist();
            if (transform.tag != "Sub_Board") 
            shuffle_sound.Play();
        }

    }

    public void ReduceGoal(string matched_tag)
    {
        if (transform.tag != "Sub_Board")
        {
            string[] t = new string[3];
            t[0] = circle_types[type_index[0]].tag;
            t[1] = circle_types[type_index[1]].tag;
            t[2] = circle_types[type_index[2]].tag;

            if (t[0].Equals(matched_tag))
            {
                goal1_left--;
                if (goal1_left <= 0)
                    goal1_left = 0;
            }
            if (t[1].Equals(matched_tag))
            {
                goal2_left--;
                if (goal2_left <= 0)
                    goal2_left = 0;
            }
            if (t[2].Equals(matched_tag))
            {
                goal3_left--;
                if (goal3_left <= 0)
                    goal3_left = 0;
            }
            if (goal1_left <= 0 && goal2_left <= 0 && goal3_left <= 0)
            {
                mission_clear = true;
                game_over = true;
            }
        }
    }
    public bool GetMatchHintOn()
    {
        return match_hint_on;
    }
    public void Move()
    {
        if (transform.tag != "Sub_Board")
        {
            move_left--;
            if (move_left <= 0)
            {
                move_left = 0;
                game_over = true;
            }
        }
    }
    public bool IsGameOver()
    {
        return game_over;
    }
    
    public void Init()
    {
        fx_volume_slider = settings_panel.GetComponentsInChildren<UnityEngine.UI.Slider>()[0];
        bgm_volume_slider = settings_panel.GetComponentsInChildren<Slider>()[1];
        if (circles_on_panel != null)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (circles_on_panel[j, i] != null)
                    {
                        Destroy(circles_on_panel[j, i]);
                        circles_on_panel[j, i] = null;
                    }
                }
            }
        }

        if (transform.tag != "Sub_Board")
        {
            game_over_text.enabled = false;
            restart_bt.enabled = false;
            restart_bt.GetComponentInChildren<Image>().enabled = false;
            restart_bt.GetComponentInChildren<Text>().enabled = false;
            picked_goal_color = new Color[3];
            type_index = new int[3];


            for (int i = 0; i < 3; i++)
            {
                type_index[i] = Random.Range(0, circle_types.Length);
                Color temp = circle_types[type_index[i]].GetComponent<SpriteRenderer>().color;
                bool already_picked = false;
                for (int j = 0; j < picked_goal_color.Length; j++)
                {
                    if (picked_goal_color[j] == temp)
                    {
                        already_picked = true;
                    }
                }
                while (already_picked)
                {
                    already_picked = false;
                    type_index[i] = Random.Range(0, circle_types.Length);
                    temp = circle_types[type_index[i]].GetComponent<SpriteRenderer>().color;
                    for (int j = 0; j < picked_goal_color.Length; j++)
                    {
                        if (picked_goal_color[j] == temp)
                        {
                            already_picked = true;
                        }
                    }
                }
                picked_goal_color[i] = temp;
            }



            move_left = Random.Range(25, 35);
            move_left_text.text = "Move left: " + move_left;
            goal1_left = Random.Range(15, 25);
            goal1.GetComponent<Image>().color = picked_goal_color[0];
            goal2.GetComponent<Image>().color = picked_goal_color[1];
            goal3.GetComponent<Image>().color = picked_goal_color[2];
            goal2_left = Random.Range(15, 25);
            goal3_left = Random.Range(15, 25);

            game_over = false;
        }
        circles_on_panel = new GameObject[width, height];
        if (tiles != null)
        {
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Destroy(tiles[j, i]);
                    tiles[j, i] = null;
                }
            }
        }
        tiles = new GameObject[width, height];

        
        
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    init_match_checking = true;
                    
                        GameObject temp_tile = GameObject.Instantiate(background_tile, new Vector3(j, i, 0.1f), Quaternion.identity) as GameObject;
                        tiles[j, i] = temp_tile;
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
    public void SetSettingPanelOn()
    {
        setting_panel_on = !setting_panel_on;
    }


}
