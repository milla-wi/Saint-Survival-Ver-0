using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLocater : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject hidingSpotOne;
    public GameObject hidingSpotTwo;
    public GameObject hidingSpotThree;
    public GameObject hidingSpotFour;

    public PlayerStatsScript PS;
    public PlayerMovementScript PM;

    public GameObject Player;

    public GameStatusScript GS;

    public bool sentCorrectBlocks;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PM.moving && !sentCorrectBlocks) {
            PM.pathPointOne = hidingSpotOne;
            PM.pathPointTwo = hidingSpotTwo;
            PM.pathPointThree = hidingSpotThree;
            PM.pathPointFour = hidingSpotFour;
            sentCorrectBlocks = false;
        }
    }

    //this is what locates the nearest points - should be it's own script.
    /*void OnTriggerStay2D(Collider2D otherObject)
    {
        Debug.Log("Happen");
        if (!PS.moving && otherObject.tag == "hidingSpot")
        {
            GameObject tempPoint = otherObject.GetComponent<GameObject>();
            if (tempPoint != GS.pointZero)
            {
                if (tempPoint.transform.position.y == Player.transform.position.y)
                {
                    if (tempPoint.transform.position.x < Player.transform.position.x)
                    {
                        hidingSpotTwo = tempPoint;
                    }
                    else if (tempPoint.transform.position.x > Player.transform.position.x)
                    {
                        hidingSpotThree = tempPoint;
                    }
                }
                else if (tempPoint.transform.position.x == Player.transform.position.x)
                {
                    if (tempPoint.transform.position.y < Player.transform.position.y)
                    {
                        hidingSpotOne = tempPoint;
                    }
                    else if (tempPoint.transform.position.y > Player.transform.position.y)
                    {
                        hidingSpotFour = tempPoint;
                    }
                }
            }
        }

    }*/

}
