using General.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField]
    private GameSettings settings;
    public  GameSettings Settings { get => settings;}

    public static GameSettingsManager _GameSettingsManager { get; private set; }
    private void Awake()
    {
        _GameSettingsManager = FindObjectOfType<GameSettingsManager>();
    }
}
