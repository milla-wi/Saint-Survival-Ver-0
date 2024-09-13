
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class PlayerStatsScript : MonoBehaviour
{
    public bool recitePoem;

    public int leaves = 0;
    public int blessings = 0;

    //blessing chance is for normal and hard mode
    public int blessingPercent;
    public int blessingNumber;

    public GameStatusScript GS;

    public bool atNewPoint = false;
    public bool blessingActive = false;
    public bool leavesActive = false;

    public Text LeafCount;
    public Text BlessingText;
    public Text blessingPercentText;
    public float blessingFloat;

    public Slider faithHealthSlider;

    //for other game mode
    public int faithHealth; /*(blessings come automatically)*/
    public int maxFaithHealth;

    public float speed;

    public bool namedProperly = false;

    public PlayerMovementScript PM;
    public PlayerSettings PS;

    public GameObject Leaves;
    public Animator LeavesAnimator;

    public AudioSource EventUpdate;

    void Start()
    {
        if(PS.runningShoes && PS.runningShoesOn)
        {
            speed = 5f;
        } else{
            speed = 3f;
        }
    }

    public void LevelChoosen()
    {
        blessingActive = false;
        blessings = 0;
        BlessingText.text = blessings.ToString();
        if (GS.level == "normal" || GS.level == "hard")
        {
            maxFaithHealth = 6;
            faithHealth = maxFaithHealth - 1;
            if(GS.level == "hard")
            {
                maxFaithHealth = 8;
            }
            faithHealthSlider.maxValue = maxFaithHealth;
            faithHealthSlider.minValue = 0;
            faithHealthSlider.value = faithHealth;
            CalculateBlessingChance();
            blessingPercentText.text = blessingPercent.ToString() + "%";
        }
        LeafCount.text = leaves.ToString();
        //recitePoem bool is active here.
    }

    // Update is called once per frame
    void Update()
    {
        //remember the mob will still be moving!
        //by point
        if (!GS.GameDone)
        {
            if (!PM.moving)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    blessingActive = true;
                    blessings--;
                    Debug.Log("I will protect you...");
                    EventUpdate.Play();
                }
                if (Input.GetKeyDown(KeyCode.E) && leaves > 0)
                {
                    leavesActive = true;
                    leaves--;
                    LeafCount.text = leaves.ToString();
                    Leaves.SetActive(true);
                    EventUpdate.Play();
                }
            }
        }
    }

    public void CalculateBlessingChance()
    {
        Debug.Log("FaithHealth;" + faithHealth + ", maxFaith:" + maxFaithHealth);
        blessingPercent = Mathf.RoundToInt(((float)faithHealth / (float)maxFaithHealth) * 100.0f);
        Debug.Log(blessingPercent);
    }
    //blessing chance...ERASE...later....
    void BlessingChance()
    {
        blessingNumber = Random.Range(1, 101);
        if (blessingNumber > blessingPercent)
        {
            blessingActive = false;
            if(faithHealth > 1)
            {
                faithHealthSlider.value = faithHealth;
                Debug.Log("Nothing happened...");
            } else
            {
                Debug.Log("Not enough faith health to pray...");
            }
        }
        else
        {
            blessingActive = true;
            if (faithHealth < 6)
            {
                faithHealth++;
            }
            faithHealthSlider.value = faithHealth;
            Debug.Log("I will protect you...");
        }
        CalculateBlessingChance();
    }

    public void GameRestart()
    {
        PM.moving = false;
        atNewPoint = false;
        blessingActive = false;
        leavesActive = false;
        leaves = 0;
        blessings = 0;
        PM.GameRestart();
        LeafCount.text = leaves.ToString();
        BlessingText.text = blessings.ToString();
        
    }

    public void ChangeStat(string type, int amount)
    {
        switch (type)
        {
            case "leaves":
                leaves += amount;
                break;
            case "blessings":
                blessings += amount;
                break;
        }
    }

    public void PoemFaith()
    {
        faithHealth++;
    }
}
