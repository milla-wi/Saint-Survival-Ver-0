using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameStatusScript : MonoBehaviour
{
    public List<GameObject> enemyPositionList = new List<GameObject>();
    public List<GameObject> objectPositionList = new List<GameObject>();
    public List<GameObject> coversPositionsList = new List<GameObject>();
    public List<GameObject> blockPositionsList = new List<GameObject>();

    public GameObject Looker;
    public GameObject Gossiper;

    public GameObject comfortPoint;
    public GameObject leaf;
    public GameObject blessing;

    public string level = "undefined";

    public GameObject pointZero;

    public PlayerStatsScript PL;
    public PlayerSettings PS;
    public MiniSetingsMenu MS;

    public PlayerMovementScript PM;
    public PathScript Path;

    public bool pause = false;
    public bool countdown = true;
    public bool restart = false;

    float currentTime = 0f;
    int currentSeconds = 0;
    int currentMinutes = 0;

    public Text TimeText;

    public Text CurrentHighScore;
    public Text NewHighScore;
    public Text PastHighScore;

    public float countdownTime = 5f;
    public int countdownSeconds = 5;
    public Text countdownText;

    public bool overScreenUp = false;

    public bool GameDone = false;
    public bool Win = false;

    public GameObject LoseScreen;
    public GameObject WinGameScreen;

    public GameObject LevelSelectScreen;
    public GameObject PausePanel;
    public GameObject MiniSettingsPanel;

    public GameObject MainUI;

    public GameObject bushes;
    public GameObject flowerBush;
    public GameObject berryBush;

    public GameObject bearTrap; // this is a path blocker;
    public GameObject leafTrap; //this is a path blocker.

    string highScore = "none";
    string currentScore = "none";

    public CameraMoveScript CM;

    public GameObject PlayerSprite;

    public bool levelChoosen = false;

    public GameObject OldScorePanel;

    public bool playerSpottedByGossip;

    public List<GameObject> enemiesAround;
    public List<GameObject> trapsAround;

    public bool poemFaithGiven = false;

    public Button runningShoesButton;
    public Button herPoemButton;

    public GameObject AlertPanel;
    public Text boostDescrp;
    public Text titleBoost;
    public Image boostImage;

    public GameObject TutorialScreen;

    void Start()
    {
        PM.currentPoint = pointZero;
        if (!PS.tutorialShown)
        {
            TutorialScreen.SetActive(true);
        } else
        {
            LevelSelectScreen.SetActive(true);
        }
       
        if (PS.runningShoes)
        {
            runningShoesButton.gameObject.SetActive(true);
        } else if (PS.herPoem)
        {
            herPoemButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    //intro should be changed to a player settings bool
    void Update()
    {
        if (!countdown && !GameDone && !pause && levelChoosen)
        {
            if (!PlayerSprite.activeSelf)
            {
                PlayerSprite.SetActive(true);
            }
            currentTime += Time.deltaTime;
            currentSeconds = Mathf.RoundToInt(currentTime % 60);
            currentMinutes = Mathf.RoundToInt(currentTime / 60);
            if (PS.herPoem && (currentSeconds % 45 == 0) && !poemFaithGiven)
            {
                PL.PoemFaith();
                poemFaithGiven = true;
            } else if (poemFaithGiven)
            {
                poemFaithGiven = false;
            }
            if (currentSeconds > 9)
            {
                currentScore = currentMinutes + ":" + currentSeconds;
            }
            else if (currentSeconds <= 9)
            {
                currentScore = currentMinutes + ":0" + currentSeconds;
            }
            TimeText.text = currentScore;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !pause && !GameDone && levelChoosen)
        {
            PauseGame();
        }

        if (countdown && !GameDone && levelChoosen)
        {
            countdownTime -= Time.deltaTime;
            countdownSeconds = Mathf.RoundToInt(countdownTime);
            countdownText.text = countdownSeconds.ToString();
            if (countdownSeconds == 0)
            {
                countdown = false;
                countdownText.gameObject.SetActive(false);
                Path.BlockHolder.SetActive(true);
                Path.findSpots();
            }
        }
        if (playerSpottedByGossip)
        {
            AlertLookers();
        }
    }

    public void CloseTutorialScreen()
    {
        TutorialScreen.SetActive(false);
        LevelSelectScreen.SetActive(true);

    }

    public void PauseGame()
    {
        pause = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pause = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    void LoadHighScore()
    {
        StreamReader scoreReader = new StreamReader("Assets/TextFiles/highScore.txt");
        highScore = scoreReader.ReadLine();
        scoreReader.Close();
    }

    void SaveHighScore()
    {
        StreamWriter scoreWriter = new StreamWriter("Assets/TextFiles/highScore.txt");
        scoreWriter.Write(highScore);
        scoreWriter.Close();
    }

    void RandomizeEnemies()
    {
        bool lastTimeZero = false;
        int enemyType = 0;
        for (int i = 0; i < enemyPositionList.Count; i++)
        {
            if (level == "easy")
            {
                enemyType = Random.Range(0, 3);
            }
            else if (level == "normal")
            {
                enemyType = Random.Range(0, 4);
            }
            else if (level == "hard")
            {
                enemyType = Random.Range(1, 4);
            }
            switch (enemyType)
            {
                case 0:
                    if (!lastTimeZero)
                    {
                        lastTimeZero = true;
                    }
                    else
                    {
                        GameObject LookCloneZero = Instantiate(Looker, enemyPositionList[i].transform.position, Quaternion.identity);
                        LookCloneZero.name = "Looker";
                        enemiesAround.Add(LookCloneZero);
                        lastTimeZero = false;
                    }
                    break;
                case 1:
                    GameObject LookClone = Instantiate(Looker, enemyPositionList[i].transform.position, Quaternion.identity);
                    LookClone.name = "Looker";
                    enemiesAround.Add(LookClone);
                    break;
                case 2:
                    GameObject LookCloneTwo = Instantiate(Looker, enemyPositionList[i].transform.position, Quaternion.identity);
                    LookCloneTwo.name = "Looker";
                    enemiesAround.Add(LookCloneTwo);
                    break;
                case 3:
                    GameObject GossipClone = Instantiate(Gossiper, enemyPositionList[i].transform.position, Quaternion.identity);
                    GossipClone.name = "Gossiper";
                    enemiesAround.Add(GossipClone);
                    break;
            }
        }
    }

    void RandomizeBlockers()
    {
        int zeroBlocks = 0;
        int maxZero = 0;
        
        if (level == "easy")
        {
            maxZero = 5;
        } else if (level == "normal" || level == "hard")
        {
            maxZero = 4;
        }
        for(int i = 0; i < blockPositionsList.Count; i++)
        {
            if(zeroBlocks > maxZero)
            {
                zeroBlocks++;
            } else {
                int blockType = 0;
                if (level == "easy" || level == "normal")
                {
                    blockType = Random.Range(0, 3);
                }
                else if (level == "normal" || level == "hard")
                {
                    blockType = Random.Range(1, 3);
                }
                switch (blockType)
                {
                        case 1:
                            GameObject blockOne = Instantiate(bearTrap, objectPositionList[i].transform.position, Quaternion.identity);
                            trapsAround.Add(blockOne);
                            break;
                        case 2:
                            GameObject blockTwo = Instantiate(leafTrap, objectPositionList[i].transform.position, Quaternion.identity);
                            trapsAround.Add(blockTwo);
                            break;
                }
            }
        }
    }

    void AlertLookers()
    {
        for(int i = 0; i < enemiesAround.Count; i++)
        {
            if(enemiesAround[i].name == "Looker")
            {
                enemiesAround[i].GetComponent<EnemyBehaviorScript>().RaiseSpeed();
            }
        }
        playerSpottedByGossip = false;
    }

    void RandomizeItems()
    {
        for (int i = 0; i < objectPositionList.Count; i++)
        {
            int objectType = 0;
            if(level == "easy")
            {
                objectType = Random.Range(0, 3);
            } else if (level == "normal")
            {
                objectType = Random.Range(0, 4);
            } else if (level == "hard")
            {
                objectType = Random.Range(-1, 4);
            }
            switch (objectType)
            {
                case 1:
                    Instantiate(leaf, objectPositionList[i].transform.position, Quaternion.identity);
                    UnityEngine.Debug.Log("Position:" + i + ", Object: Leaf");
                    break;
                case 2:
                    Instantiate(blessing, objectPositionList[i].transform.position, Quaternion.identity);
                    UnityEngine.Debug.Log("Position:" + i + ", Object: Blessing");
                    break;
                case 3:
                    Instantiate(comfortPoint, objectPositionList[i].transform.position, Quaternion.identity);
                    UnityEngine.Debug.Log("Position:" + i + ", Object: Comfort Point");
                    break;
            }
        }
    }

    void RandomizeCovers()
    {
        for (int i = 0; i < coversPositionsList.Count; i++)
        {
            int coverType = Random.Range(1, 5);
            switch (coverType)
            {
                case 1:
                    Instantiate(bushes, coversPositionsList[i].transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bushes, coversPositionsList[i].transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(flowerBush, coversPositionsList[i].transform.position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(berryBush, coversPositionsList[i].transform.position, Quaternion.identity);
                    break;
            }
        }
    }

    public void GameOver()
    {
        GameDone = true;
        highScore = TimeText.text;
        if (Win)
        {
            MS.music.Stop();
            MS.music.clip = Resources.Load<AudioClip>("Enemy/Music/POL-mushroom-trail-short");
            MS.music.Play();
            WinGameScreen.SetActive(true);
            bool newScore = compareHighScore();
            if (newScore)
            {
                string pastScore = highScore;
                PastHighScore.text = pastScore;
                highScore = currentScore;
                OldScorePanel.SetActive(true);
                SaveHighScore();
                if(level == "hard")
                {
                    PS.hardLevelWin++;
                    PS.SaveInfo();
                }
                if(level == "hard" && PS.hardLevelWin == 3 && !PS.runningShoes)
                {
                    PS.runningShoes = true;
                    UnlockBoost("runningShoes");
                    PS.SaveInfo();
                } else if(level == "hard" && currentMinutes <= 1 && !PS.herPoem) {
                    PS.herPoem = true;
                    UnlockBoost("herPoem");
                    PS.SaveInfo();
                }
            }
            NewHighScore.text = highScore;
        }
        else
        {
            MS.music.Stop();
            MS.music.clip = Resources.Load<AudioClip>("Enemy/Music/POL-forgotten-woods-short");
            MS.music.Play();
            LoseScreen.SetActive(true);
            CurrentHighScore.text = highScore;
        }
        PlayerSprite.SetActive(false);
        Path.BlockHolder.SetActive(false);
    }

    public bool compareHighScore()
    {
        bool newHighScore = false;

        if (highScore == "none")
        {
            newHighScore = true;
        }
        else
        {
            int digitOneH = int.Parse(highScore[0].ToString());
            int digitOneC = int.Parse(currentScore[0].ToString());
            if (digitOneC > digitOneH)
            {
                newHighScore = true;
            }
            else if (digitOneC == digitOneH)
            {
                int digitTwoH = int.Parse(highScore.Substring(2));
                int digitTwoC = int.Parse(currentScore.Substring(2));
                if (digitTwoC > digitTwoH)
                {
                    newHighScore = true;
                }
            }
        }
        return newHighScore;
    }

    public void GameRestart()
    {
        MS.music.Stop();
        MS.music.clip = Resources.Load<AudioClip>("Audio/Music/POL-the-foyer-short");
        MS.music.Play();
        GameDone = false;
        Win = false;
        levelChoosen = false;
        OldScorePanel.SetActive(false);
        LoseScreen.SetActive(false);
        WinGameScreen.SetActive(false);
        countdownTime = 0f;
        currentTime = 0f;
        currentSeconds = 0;
        currentMinutes = 0;
        overScreenUp = false;
        pause = false;
        PL.GameRestart();
        PlayerSprite.SetActive(true);
        LevelSelectScreen.SetActive(true);
        enemiesAround.Clear();
        restart = true;

    }

    public void BackFromMiniSettings()
    {
        MiniSettingsPanel.SetActive(false);
        switch (MS.place)
        {
            case "Pause Menu":
                PausePanel.SetActive(true);
                break;
            case "Lose Screen":
                LoseScreen.SetActive(true);
                break;
            case "Win Screen":
                WinGameScreen.SetActive(true);
                break;
        }
        MS.place = "none";
    }

    public void OpenMiniSettings(string place)
    {
        switch (place)
        {
            case "Pause Menu":
                MS.place = "Pause Menu";
                PausePanel.SetActive(false);
                break;
            case "Lose Screen":
                MS.place = "Lose Screen";
                LoseScreen.SetActive(false);
                break;
            case "Win Screen":
                MS.place = "Win Screen";
                WinGameScreen.SetActive(false);
                break;
        }
        MS.showVolume();
        MiniSettingsPanel.SetActive(true);
    }

    public void LevelSelected(Text levelText)
    {
        restart = false;
        level = levelText.text.ToLower();
        LevelSelectScreen.SetActive(false);
        MainUI.SetActive(true);
        switch (level)
        {
            case "easy":
                PL.faithHealthSlider.gameObject.SetActive(false);
                PL.blessingPercentText.gameObject.SetActive(false);
                break;
        }
        levelChoosen = true;
        countdownSeconds = 5;
        countdownTime = 5f;
        countdown = true;
        countdownText.gameObject.SetActive(true);
        PL.LevelChoosen();
        RandomizeEnemies();
        RandomizeCovers();
        RandomizeItems();
    }

    public void UnlockHerPoem()
    {
        if (level == "hard")
        {
            PS.herPoem = true;
            //reveal the poem to the player in endless mode if you clear a certain amount of time.
        }
    }

    public void BackToStart()
    {
        PS.SaveInfo();
        SceneManager.LoadScene("StartMenu");
    }

    public void UnlockBoost(string boostType)
    {
        if(boostType == "runningShoes")
        {
            boostDescrp.text = "Running Shoes increase speed when hiding from enemies.";
            titleBoost.text = "You unlocked running shoes!";
            runningShoesButton.gameObject.SetActive(true);
        } else if (boostType == "herPoem")
        {
            boostDescrp.text = "Her poem increases her faith health by 1 every 45 seconds.";
            titleBoost.text = "You unlocked Her Poem!";
            herPoemButton.gameObject.SetActive(true);
        }
        AlertPanel.SetActive(true);
    }

    public void Dismiss()
    {
        AlertPanel.SetActive(false);
    }

    public void PoemOnOff()
    {
        PS.herPoemOn = !PS.herPoemOn;
    }

    public void RunningShoesOnOff()
    {
        PS.runningShoesOn = !PS.runningShoesOn;
    }

}
