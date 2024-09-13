using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update

    float defaultMusicVolume = 0.2f;
    float defaultSFXVolume = 0.3f;

    float currentMusicVolume;
    float currentSFXVolume;

    const float maxVolume = 1.0f;
    const float minVolume = 0.0f;

    public PlayerSettings PS;

    public Slider MusicSlider;
    public Slider SFXSlider;

    public Toggle RunningShoesButton;
    public Toggle herPoemButton;
    public Toggle introPlayButton;

    public AudioSource MusicSource;

    //can be used for mini script on main play page.

    void Start()
    {
        SettupStats();

        MusicSlider.minValue = SFXSlider.minValue = minVolume;
        MusicSlider.maxValue = SFXSlider.maxValue = maxVolume;

        MusicSlider.value = PS.musicVolume;
        SFXSlider.value = PS.sfxVolume;
        MusicSource.volume = PS.musicVolume;
    }

    void SettupStats() {

        if (PS.setStats) {
            currentMusicVolume = PS.musicVolume;
            currentSFXVolume = PS.sfxVolume;

            RunningShoesButton.enabled = PS.runningShoesOn;
        } else {
            currentMusicVolume = defaultMusicVolume;
            currentSFXVolume = defaultSFXVolume;

            RunningShoesButton.enabled = true;
        }
        SetToDefault();
        if (PS.runningShoes) { 
            RunningShoesButton.gameObject.SetActive(true);
        }
        if (PS.herPoem)
        {
            herPoemButton.gameObject.SetActive(true);
        }
        LoadToggles();
        SetMusic();
    }

    public void SetMusic()
    {
        currentMusicVolume = MusicSlider.value;
        currentSFXVolume = SFXSlider.value;
        MusicSource.volume = currentMusicVolume;
    }

    void SetToDefault() {
        PS.musicVolume = defaultMusicVolume;
        PS.sfxVolume = defaultSFXVolume;

        PS.runningShoesOn = true;
        PS.setStats = true;
        PS.SaveInfo();
    }

    public void SaveChanges() {
        PS.musicVolume = currentMusicVolume;
        PS.sfxVolume = currentSFXVolume;

        PS.SaveInfo();
    }

    public void LoadToggles()
    {
        RunningShoesButton.isOn = PS.runningShoesOn;
        herPoemButton.isOn = PS.herPoemOn;
        introPlayButton.isOn = PS.introPlay;
    }

    public void SetRunningShoes()
    {
        PS.runningShoesOn = !PS.runningShoesOn;
    }

    public void SetHerPoem()
    {
        PS.herPoemOn = !PS.herPoemOn;
    }

    public void SetIntroPlay()
    {
        PS.introPlay = !PS.introPlay;
    }

    public void BackToStart()
    {
        SaveChanges();
        SceneManager.LoadScene("StartMenu");
    }
}
