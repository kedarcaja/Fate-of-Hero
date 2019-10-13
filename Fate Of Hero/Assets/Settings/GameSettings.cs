using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Settings
{
    public enum ELanguage { CZ, EN }
    public enum EQualityLevel { VeryLow, Low, Medium, High, VeryHigh, Ultra }
    public enum ETextureQuality { Low, Medium, High, Ultra }
    public enum EAntiAliasing {Disabled,_2X,_4X,_8X}
    [CreateAssetMenu(menuName = "General/Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Language")]
        [SerializeField]
        private ELanguage language;
        public ELanguage Language { get => language; }

        [Header("Quality")]
        [SerializeField]
        private EQualityLevel qualityLevel;
        [SerializeField]
        private ShadowResolution shadowsResolution;
        [SerializeField]
        private ShadowQuality shadowsQuality;
        [SerializeField]
        private ETextureQuality texturesQuality;
        [SerializeField]
        private EAntiAliasing antiAliasing;
        public EQualityLevel QualityLevel { get => qualityLevel; set => qualityLevel = value; }
        public ShadowResolution ShadowsResolution { get => shadowsResolution; set => shadowsResolution = value; }
        public ShadowQuality ShadowsQuality { get => shadowsQuality; set => shadowsQuality = value; }
        public ETextureQuality TexturesQuality { get => texturesQuality; set => texturesQuality = value; }
        public EAntiAliasing AntiAliasing { get => antiAliasing; set => antiAliasing = value; }


        [SerializeField]
        private bool fullScreen  = true;
        public bool FullScreen { get => fullScreen; set => fullScreen = value; }
        [Range(0,1)]
        [SerializeField]
        [Header("Sounds")]
        [Header("Master volume")]
        private float masterVolume = 1;
        public float MasterVolume { get => masterVolume; set => masterVolume = value; }
    

        [Range(0,1)]
        [SerializeField]
        [Header("Music")]
        private float musicVolume = 1;
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
     

        [Range(0,1)]
        [SerializeField]
        [Header("Dabing")]
        private float dabingVolume = 1;
        public float DabingVolume { get => dabingVolume; set => dabingVolume = value; }

        [Range(0,1)]
        [SerializeField]
        [Header("Effects")]
        private float effectsVolume = 1;
        public float EffectsVolume { get => effectsVolume; set => effectsVolume = value; }


     
        private void Awake()
        {
            UpdateQuality();
            Screen.fullScreen = fullScreen;
        }
        public void UpdateQuality()
        {
            QualitySettings.SetQualityLevel((int)this.QualityLevel);
            QualitySettings.shadowResolution = this.ShadowsResolution;
            QualitySettings.shadows = this.ShadowsQuality;
            QualitySettings.masterTextureLimit = (int)this.TexturesQuality;
            QualitySettings.antiAliasing = (int)this.AntiAliasing;
        }

    }
}