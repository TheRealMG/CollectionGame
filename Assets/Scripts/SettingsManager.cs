using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public Toggle fullscreenToggle;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Button backButton;

    public AudioMixer audioMixer;
    public Resolution[] resolutions;
    public List<Resolution> resolutionsList = new List<Resolution>();
    public GameSettings gameSettings;

    public GameObject MainMenu;
    public GameObject OptionsMenu;

    void OnEnable() {
        gameSettings = new GameSettings();
        // Listeners for if a setting is changed
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResoolutionChange(); });
        qualityDropdown.onValueChanged.AddListener(delegate { OnQualityChange(); });
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(volumeSlider.value); });
        backButton.onClick.AddListener(delegate { OnBackButtonClick(); });
        // Sorting through screen resolutions to only include ones with the highest refresh rate
        resolutions = Screen.resolutions;
        for (int i = 0, j = 0; i < resolutions.GetLength(0); i++) {
            if (resolutions[i].refreshRate == Screen.currentResolution.refreshRate) {
                resolutionsList.Add(resolutions[i]);
                j++;
            }
        }
        resolutions = resolutionsList.ToArray();
        // Adding resolutions to the resolution dropdown menu
        foreach (Resolution resolution in resolutions) {
            resolutionDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(resolution.ToString()));
        }
        // If gamesettings.json doesn't exist, preload a few of the settings ( prevents bugs )
        if (!File.Exists(Application.persistentDataPath + "/gamesettings.json") == true) {
            gameSettings.resolutionIndex = resolutions.Length - 1;
            gameSettings.fullscreen = true;
            SaveSettings();
        }
        LoadSettings();

    }

    public void OnFullscreenToggle() {
        gameSettings.fullscreen = fullscreenToggle.isOn;
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResoolutionChange() {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnQualityChange() {
        gameSettings.quality = qualityDropdown.value;
        QualitySettings.masterTextureLimit = qualityDropdown.value;
    }

    public void OnVolumeChange(float volume) {
        audioMixer.SetFloat("volume", volume);
        gameSettings.volume = volumeSlider.value;
    }
    // Saves the settings when the back button is clicked
    public void OnBackButtonClick() {
        SaveSettings();
    }
    // Saves settings to a file ( allows settings to stay after restarting the game )
    public void SaveSettings() {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }
    // Loads the settings file
    public void LoadSettings() {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        qualityDropdown.value = gameSettings.quality;
        volumeSlider.value = gameSettings.volume;
        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
    }
}

