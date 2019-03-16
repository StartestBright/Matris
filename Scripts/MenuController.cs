using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public GameObject settings_panel;
    public GameObject exit_panel;
    private Slider fx_volume_slider;
    private Slider bgm_volume_slider;
    private bool setting_panel_on = false;
    private bool exit_panel_on = false;
    void Start () {
        //settings_panel.SetActive(false);
        //slider.SetActive(false);
        //slider.GetComponent<SliderJoint2D>().
        fx_volume_slider = settings_panel.GetComponentsInChildren<UnityEngine.UI.Slider>()[0];
        bgm_volume_slider = settings_panel.GetComponentsInChildren<Slider>()[1];
	}
	
	void Update () {

        if (setting_panel_on)
            settings_panel.SetActive(true);
        else
            settings_panel.SetActive(false);
        if (exit_panel_on)
        {
            exit_panel.SetActive(true);
        }else
            exit_panel.SetActive(false);
    }
    public void SetSettingPanelOn()
    {
        setting_panel_on = !setting_panel_on;
    }
    public void SetExitPanelOn()
    {
        exit_panel_on = !exit_panel_on;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    
    public void ExitGame()
    {
        Application.Quit();
    }
}
