using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    // Start is called before the first frame update

    //knows if stats are set or not
    public bool setStats;

    //music volume stats
    public float musicVolume = 0.2f;
    public float sfxVolume = 0.3f;

    //poem stats
    public bool herPoem = false;
    public bool herPoemOn = true;

    //running shoe stats
    public bool runningShoes = false;
    public bool runningShoesOn = true;

    //other bools
    public bool shownIntro = false;
    public bool introPlay = false;
    public bool slideShow = false;
    public bool tutorialShown = false;

    public int hardLevelWin = 0;


    void Awake() {
            GatherPlayerInfo();
    }

    void GatherPlayerInfo() {
        StreamReader gatherReader = new StreamReader("Assets/TextFiles/PlayerSettings.txt");

        setStats = bool.Parse(gatherReader.ReadLine());
        musicVolume = float.Parse(gatherReader.ReadLine());
        sfxVolume = float.Parse(gatherReader.ReadLine());
        shownIntro = bool.Parse(gatherReader.ReadLine());
        introPlay = bool.Parse(gatherReader.ReadLine());
        slideShow = bool.Parse(gatherReader.ReadLine());
        tutorialShown = bool.Parse(gatherReader.ReadLine());

        hardLevelWin = int.Parse(gatherReader.ReadLine());

        herPoem = bool.Parse(gatherReader.ReadLine());
        herPoemOn = bool.Parse(gatherReader.ReadLine());

        runningShoes = bool.Parse(gatherReader.ReadLine());
        runningShoesOn = bool.Parse(gatherReader.ReadLine());
       
        gatherReader.Close();
    }

    public void SaveInfo() {
        StreamWriter saveWriter = new StreamWriter("Assets/TextFiles/PlayerSettings.txt");
        if (!setStats)
        {
            setStats = true;
        }
        saveWriter.WriteLine(setStats);

        saveWriter.WriteLine(musicVolume);
        saveWriter.WriteLine(sfxVolume);

        saveWriter.WriteLine(shownIntro);
        saveWriter.WriteLine(introPlay);
        saveWriter.WriteLine(slideShow);
        saveWriter.WriteLine(tutorialShown);

        saveWriter.WriteLine(hardLevelWin);

        saveWriter.WriteLine(herPoem);
        saveWriter.WriteLine(herPoemOn);
        saveWriter.WriteLine(runningShoes);
        saveWriter.WriteLine(runningShoesOn);
        

        saveWriter.Close();
    }

    public void EraseInfo()
    {
        StreamWriter saveWriter = new StreamWriter("Assets/TextFiles/PlayerSettings.txt");
        if (setStats)
        {
            setStats = false;
        }
        saveWriter.WriteLine(setStats);

        saveWriter.WriteLine(0.2);
        saveWriter.WriteLine(0.3);

        saveWriter.WriteLine(false);
        saveWriter.WriteLine(true);
        saveWriter.WriteLine(false);
        saveWriter.WriteLine(false);

        saveWriter.WriteLine(0);

        saveWriter.WriteLine(false);
        saveWriter.WriteLine(false);
        saveWriter.WriteLine(false);
        saveWriter.WriteLine(false);

        saveWriter.Close();
    }
}
