using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour {

    public Slider BrightnessSlider,VolumeSlider;
    public Text BrightnessLable, VolumeLable;
    float brightnessValue, VolumeValue;
    public Dropdown ResxDropdown,QualityDropdown;
 
    Resolution[] resolutions;

    void Start()
    {
        if (!BrightnessSlider) { Debug.LogError("Objekt BrightnessSlider nebyl nalezen"); }
        if (!VolumeSlider) { Debug.LogError("Objekt VolumeSlider nebyl nalezen"); }
        if (!BrightnessLable) { Debug.LogError("Objekt BrightnessLable nebyl nalezen"); }
        if (!ResxDropdown) { Debug.LogError("Objekt resxDropdown nebyl nalezen"); }
        if (!QualityDropdown) { Debug.LogError("Objekt qualityDropdown nebyl nalezen"); }
        
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++){
            ResxDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

            ResxDropdown.value = i;

            ResxDropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[ResxDropdown.value].width, resolutions[ResxDropdown.value].height, true); });
        }
        QualityDropdown.options.Clear();
        for (int i = 0; i < QualitySettings.names.Length; i++){
            QualityDropdown.options.Add(new Dropdown.OptionData(QualitySettings.names[i]));
        }
        QualityDropdown.onValueChanged.AddListener(delegate { SetQualityLevel(); });   
    }
     
    void Update()
    {
        brightnessValue = BrightnessSlider.value;
        BrightnessLable.text = Mathf.FloorToInt(brightnessValue).ToString();
        VolumeValue = VolumeSlider.value;
        VolumeLable.text = Mathf.FloorToInt(VolumeValue).ToString();
    }
    public void bAdj(float brightnessValue)
    {
        brightnessValue = BrightnessSlider.value;
        //rgbValue = GUI.HorizontalSlider (new Rect (Screen.width / 2 - 50, 90, 100, 30), rgbValue, 0f, 1.0f);
        //RenderSettings.ambientLight = brightnessValue;
        RenderSettings.ambientLight = new Color(brightnessValue, brightnessValue, brightnessValue, 1);
    }

    public void VolumeControl(float volumeControl)
    {
        AudioListener.volume = volumeControl;
    }

    public void SetQualityLevel()
    {
        QualitySettings.SetQualityLevel(QualityDropdown.value);
    }
    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
    public void SaveAndExit()
    {
        GlobalSetting.SetMasterVolume(VolumeSlider.value);
        GlobalSetting.SetMasterBrightness(BrightnessSlider.value);
        
    }
    public void SetDefault()
    {
        VolumeSlider.value = 100f;
        BrightnessSlider.value = 50f;
    }
    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void QuitGame()
    {
        Debug.Log("Konec");
        Application.Quit();
    }
}
