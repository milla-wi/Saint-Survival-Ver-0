using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AboutController : MonoBehaviour
{
    public PlayerSettings PS;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        SetMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMusic()
    {
        music.volume = PS.musicVolume;
    }

    public void BackToStart()
    {
        PS.SaveInfo();
        SceneManager.LoadScene("StartMenu");
    }

    public void SlideShowStory()
    {
        if (!PS.slideShow)
        {
            PS.slideShow = true;
            PS.SaveInfo();
        }
        SceneManager.LoadScene("Intro");
    }

}
