using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerSettings PS;
    public AudioSource music;

    void Start()
    {
        music.volume = PS.musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToScence(Text text) {
        switch (text.text)
        {
            case "Play":
                if(!PS.shownIntro || PS.introPlay)
                {
                    UnityEngine.Debug.Log("Showing Intro");
                    if (!PS.shownIntro)
                    {
                        PS.shownIntro = true;
                        PS.SaveInfo();
                    }
                    if (PS.slideShow)
                    {
                        PS.slideShow = false;
                        PS.SaveInfo();
                    }
                    SceneManager.LoadScene("Intro");
                } else
                {
                    UnityEngine.Debug.Log("Intro not shown");
                    SceneManager.LoadScene("Play");
                }
                break;
            case "Settings":
                SceneManager.LoadScene("Settings");
                break;
            case "How to Play":
                SceneManager.LoadScene("Settings");
                break;
            case "About":
                SceneManager.LoadScene("About");
                //going to the about page gives the player a extra powerup!
                //maybe even the endless mode!
                break;
            case "Credits":
                SceneManager.LoadScene("Credits");
                break;
        }

    }

    public void Quit()
    {
        Application.Quit();
    }
}
