using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageScript: MonoBehaviour
{
    //script objects
    public PlayerSettings PS;

    //list of pages
    public List<GameObject> Pages;

    //audio sources
    public AudioSource pageSFX;
    public AudioSource music;

    //other
    public bool playingGame;
    public Button playButton;

    int pageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Pages[pageIndex].SetActive(true);
        pageSFX.volume = PS.sfxVolume;
        if(SceneManager.GetActiveScene().name != "Play")
        {
            music.volume = PS.musicVolume;
        }
        playingGame = !PS.slideShow;
        if (!playingGame)
        {
            playButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PageTurn("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           PageTurn("right");
        }
    }

    public void PageTurn(string direction)
    {
        switch (direction)
        {
            case "left":
                if (pageIndex > 0) {
                    Pages[pageIndex].SetActive(false);
                    pageIndex--;
                    Pages[pageIndex].SetActive(true);
                    pageSFX.Play();
                }
                 
                break;
            case "right":
                if (pageIndex < (Pages.Count - 1))
                {
                    Pages[pageIndex].SetActive(false);
                    pageIndex++;
                    Pages[pageIndex].SetActive(true);
                    pageSFX.Play();
                }
                break;
        }
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Play");
    }



}
