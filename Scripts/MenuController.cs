using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public GameObject settings_panel;
    private Slider fx_volume_slider;
    private Slider bgm_volume_slider;
    private bool setting_panel_on = false;
    void Start () {
        //settings_panel.SetActive(false);
        //slider.SetActive(false);
        //slider.GetComponent<SliderJoint2D>().
        PlayerPrefs.SetFloat("fx_volume",1f);
        PlayerPrefs.SetFloat("bgm_volume", 1f);
        fx_volume_slider = settings_panel.GetComponentsInChildren<UnityEngine.UI.Slider>()[0];
        bgm_volume_slider = settings_panel.GetComponentsInChildren<Slider>()[1];
	}
	
	void Update () {
        PlayerPrefs.SetFloat("fx_volume", fx_volume_slider.value);
        PlayerPrefs.SetFloat("bgm_volume", bgm_volume_slider.value);

        if (setting_panel_on)
            settings_panel.SetActive(true);
        else
            settings_panel.SetActive(false);
    }
    public void SetSettingPanelOn()
    {
        setting_panel_on = !setting_panel_on;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
