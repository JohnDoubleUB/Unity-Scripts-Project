using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SavingTest : MonoBehaviour
{
    public SaveSystem<PlayerOptions> PlayerOptionsSaveSystem = new SaveSystem<PlayerOptions>
    {
        SaveLocation = Application.persistentDataPath,
        SaveName = "PlayerOptions",
        SaveExtension = "PPO",
        VersioningMustMatch = true,
        PrefixAllXml = true,
        UseEncryption = false
    };

    public SaveSystem<GameOptions> GameOptionsSaveSystem = new SaveSystem<GameOptions>
    {
        SaveLocation = Application.persistentDataPath,
        SaveName = "GameOptions",
        SaveExtension = "PPGO",
        VersioningMustMatch = false,
        PrefixAllXml = true,
        UseEncryption = false
    };

    public bool TestSave = false;
    private void Start()
    {
        if (TestSave) 
        {
            PlayerOptionsSaveSystem.SaveDataToXMLFile(new PlayerOptions { PlayerName = "Jeff" });

            if (PlayerOptionsSaveSystem.TryLoadDataFromXMLFile(out PlayerOptions serializedObject)) 
            {
                Debug.Log($"Load was successful after Save! Player Name: {serializedObject.PlayerName}");
            }

        }
    }
}

[System.Serializable]
public class PlayerOptions
{
    public string PlayerName;

    public PlayerOptions Copy()
    {
        return new PlayerOptions { PlayerName = this.PlayerName };
    }
}


[System.Serializable]
public class GameOptions
{
    public Resolution Resolution;
    public int Framerate;
    public bool Vsync;
    public int FullscreenMode;

    public float MasterVolume;
    public float MusicVolume;
    public float SFXVolume;

    public float ControllerLookSensitivity;
    public float MouseLookSensitivity;

    public bool CameraShake = true;

    //These are essentially default settings for the game
    public static GameOptions GenerateCurrent()
    {
        return new GameOptions
        {
            Framerate = Application.targetFrameRate,
            FullscreenMode = (int)Screen.fullScreenMode,
            Resolution = new Resolution { width = Screen.width, height = Screen.height, refreshRate = Screen.currentResolution.refreshRate },
            Vsync = true,//QualitySettings.vSyncCount == 1,
            MasterVolume = 75f,
            MusicVolume = 75f,
            SFXVolume = 75f,
            ControllerLookSensitivity = 2f,
            MouseLookSensitivity = 2f,
            CameraShake = true
        };
    }
}
