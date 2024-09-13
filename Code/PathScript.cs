using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BlockOne;
    public GameObject BlockTwo;
    public GameObject BlockThree;
    public GameObject BlockFour;
    public GameObject BlockHolder;

    public string BlockChoosen;

    public PlayerStatsScript PL;
    public PlayerMovementScript PM;
    public GameStatusScript GS;

    public List<GameObject> HidingSpots;

    public GameObject Player;

    public bool foundSpots = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealPossibleDirections()
    {
        BlockHolder.SetActive(true);
        if (PM.pathPointOne != null)
        {
            //up
            BlockOne.SetActive(true);
            PM.upActive = true;
        }
        if (PM.pathPointTwo != null)
        {
            //right
            BlockTwo.SetActive(true);
            PM.rightActive = true;
        }
        if (PM.pathPointThree != null)
        {
            //down
            BlockThree.SetActive(true);
            PM.downActive = true;
        }
        if (PM.pathPointFour != null)
        {
            //left
            BlockFour.SetActive(true);
            PM.leftActive = true;
        }
    }

    public void DirectionChoosenScript(string option)
    {

        //up
        if (option == "one")
        {
            BlockTwo.SetActive(false);
            BlockThree.SetActive(false);
            BlockFour.SetActive(false);
            BlockChoosen = "One";
            //dissappear animation for blocks
            //choosen animation and movement...

        }
        //right
        if (option == "two")
        {
            BlockOne.SetActive(false);
            BlockThree.SetActive(false);
            BlockFour.SetActive(false);
            BlockChoosen = "Two";
            //dissappear animation for blocks
            //choosen animation and movement...

        }
        //down
        if (option == "three")
        {
            BlockOne.SetActive(false);
            BlockTwo.SetActive(false);
            BlockFour.SetActive(false);
            BlockChoosen = "Three";
            //dissappear animation for blocks
            //choosen animation and movement...
        }

        //left
        if (option == "four")
        {
            BlockOne.SetActive(false);
            BlockTwo.SetActive(false);
            BlockThree.SetActive(false);
            BlockChoosen = "Four";
            //dissappear animation for blocks
            //choosen animation and movement...
        }
        PM.downActive = false;
        PM.leftActive = false;
        PM.rightActive = false;
        PM.upActive = false;
    }
    

    void ChoosenAnimation(GameObject BlockButton)
    {
        switch (BlockChoosen)
        {
            case "One":
                BlockTwo.SetActive(false);
                BlockThree.SetActive(false);
                BlockFour.SetActive(false);
                //animate the chooseblock here;
                break;
            case "Two":
                BlockOne.SetActive(false);
                BlockThree.SetActive(false);
                BlockFour.SetActive(false);
                //animate the chooseblock here;
                break;
            case "Three":
                BlockOne.SetActive(false);
                BlockTwo.SetActive(false);
                BlockFour.SetActive(false);
                //animate the chooseblock here;
                break;
            case "Four":
                BlockOne.SetActive(false);
                BlockTwo.SetActive(false);
                BlockFour.SetActive(false);
                //animate the chooseblock here;
                break;
        }
        //when chr animation at sec ? then this statement.
        switch (BlockChoosen)
        {
            case "One":
                BlockOne.SetActive(false);
                break;
            case "Two":
                BlockTwo.SetActive(false);
                break;
            case "Three":
                BlockThree.SetActive(false);
                break;
            case "Four":
                BlockFour.SetActive(false);
                break;
        }
    }

    public void ResetSpots()
    {
        PM.pathPointOne = null;
        PM.pathPointTwo = null;
        PM.pathPointThree = null;
        PM.pathPointFour = null;
    }

    public void findSpots()
    {
        if (PM.pathPointOne != null || PM.pathPointTwo != null || PM.pathPointThree != null || PM.pathPointFour != null)
        {
            ResetSpots();
            Debug.Log("Paths reset");
        }
        for (int i = 0; i < HidingSpots.Count; i++)
        {
            if ((Player.transform.position.y <= HidingSpots[i].transform.position.y + 1f) && (Player.transform.position.y >= HidingSpots[i].transform.position.y - 1f))
            {
                float XOne = Player.transform.position.x + 7f;
                float XTwo = Player.transform.position.x - 7f;
                if ((HidingSpots[i].transform.position.x <= XOne + 1f) && (HidingSpots[i].transform.position.x >= XOne - 1f) && (HidingSpots[i] != PM.currentPoint) && (!BlockCheck(HidingSpots[i])))
                {
                    
                    PM.pathPointTwo = HidingSpots[i];
                }
                else if ((HidingSpots[i].transform.position.x <= XTwo + 1f) && (HidingSpots[i].transform.position.x >= XTwo - 1f) && (HidingSpots[i] != PM.currentPoint) && (!BlockCheck(HidingSpots[i])))
                {
                    PM.pathPointFour = HidingSpots[i];
                }
            }
            else if ((Player.transform.position.x <= HidingSpots[i].transform.position.x + 1f) && (Player.transform.position.x >= HidingSpots[i].transform.position.x - 1f))
            {
                float YOne = Player.transform.position.y + 7f;
                float YTwo = Player.transform.position.y - 7f;
                if ((HidingSpots[i].transform.position.y <= YOne + 1f) && (HidingSpots[i].transform.position.y >= YOne - 1f) && (HidingSpots[i] != PM.currentPoint) && (!BlockCheck(HidingSpots[i])))
                {
                    PM.pathPointOne = HidingSpots[i];
                }
                else if ((HidingSpots[i].transform.position.y <= YTwo + 1f) && (HidingSpots[i].transform.position.y >= YTwo - 1f) && (HidingSpots[i] != PM.currentPoint) && (!BlockCheck(HidingSpots[i])))
                {
                    PM.pathPointThree = HidingSpots[i];
                }
            }
            if (PM.pathPointOne != null && PM.pathPointTwo != null && PM.pathPointThree != null && PM.pathPointFour != null)
            {
                break;
            }
        }
        foundSpots = true;
        RevealPossibleDirections();
    }

    public bool BlockCheck(GameObject pointObject)
    {
        bool blocked = false;
        for(int i = 0; i < GS.trapsAround.Count; i++)
        {
            if(pointObject.transform.position == GS.trapsAround[i].transform.position)
            {
                blocked = true;
                break;
            }
        }
        return blocked;
    }

}
