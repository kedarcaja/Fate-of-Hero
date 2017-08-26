using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string MASTER_BRIGHTNESS_KEY = "master_Brightness";

    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else Debug.LogError("Chybný parametr hlasitosti zvuku [flout 0-1]");
    }
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    public static void SetMasterBrightness(float Brightness)
    {
        if (Brightness >= 0f && Brightness <= 100f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, Brightness);
        }
        else Debug.LogError("Chybný parametr hlasitosti zvuku [flout 0-1]");
    }
    public static float GetMasterBrightness()
    {
        return PlayerPrefs.GetFloat(MASTER_BRIGHTNESS_KEY);
    }
}
