
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{

    public PlayerStatsScript PS;
    public GameStatusScript GS;
    public PlayerSettings PPS;

    public GameObject PointZero;

    public PathScript Path;

    public GameObject currentPoint;

    public GameObject pathPointOne;
    public GameObject pathPointTwo;
    public GameObject pathPointThree;
    public GameObject pathPointFour;

    public bool leftActive;
    public bool rightActive;
    //up active not on.
    //sometime will not move
    public bool upActive;
    public bool downActive;

    public bool moveDown;
    public bool moveUp;
    public bool moveLeft;
    public bool moveRight;

    public bool moving = false;

    public Animator playerAnim;
    public AudioSource grassSteps;
    public AudioSource bushAudio;

    public GameObject Leaves;
    public Animator LeavesAnimator;

    // Update is called once per frame
    void Update()
    {
        if (!moving && !GS.GameDone && !GS.countdown)
        {

            if (Input.GetKeyDown(KeyCode.W) && upActive)
            {
                //up
                Debug.Log("Prepared to Move Up!");
                moveUp = true;
                upActive = false;
                moving = true;
                playerAnim.SetTrigger("up");
                LeavesAnimator.SetBool("walking", true);
                grassSteps.Play();
                bushAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.D) && rightActive)
            {
                //right
                Debug.Log("Prepared to Move Right!");
                moveRight = true;
                rightActive = false;
                moving = true;
                playerAnim.SetTrigger("right");
                LeavesAnimator.SetBool("walking", true);
                grassSteps.Play();
            }
            else if (Input.GetKeyDown(KeyCode.S) && downActive)
            {
                //down
                Debug.Log("Prepared to Move Down!");
                moveDown = true;
                downActive = false;
                moving = true;
                playerAnim.SetTrigger("down");
                LeavesAnimator.SetBool("walking", true);
                grassSteps.Play();
            }
            else if (Input.GetKeyDown(KeyCode.A) && leftActive)
            {
                //left
                Debug.Log("Prepared to Move Left!");
                moveLeft = true;
                leftActive = false;
                moving = true;
                playerAnim.SetTrigger("left");
                LeavesAnimator.SetBool("walking", true);
                grassSteps.Play();
            }
        }
        /*if (!moving && !GS.GameDone && !GS.countdown && !Path.foundSpots)
        {
            Path.findSpots();
        }*/
        if (moving && !GS.GameDone && !GS.countdown)
        {
            if (moveUp && transform.position.y < pathPointOne.transform.position.y)
            {
                //up
                transform.position += Vector3.up * PS.speed * Time.deltaTime;
            }
            else if (moveRight && transform.position.x < pathPointTwo.transform.position.x)
            {
                //right
                transform.position += Vector3.right * PS.speed * Time.deltaTime;
            }
            else if (moveDown && transform.position.y > pathPointThree.transform.position.y)
            {

                //down
                transform.position += Vector3.down * PS.speed * Time.deltaTime;
            }
            else if (moveLeft && transform.position.x > pathPointFour.transform.position.x)
            {

                //left
                transform.position += Vector3.left * PS.speed * Time.deltaTime;
            }
            else
            {
                Debug.Log("Done moving.");
                playerAnim.SetTrigger("hide");
                LeavesAnimator.SetBool("walking", false);
                grassSteps.Stop();
                bushAudio.Stop();
                if (moveUp)
                {
                    currentPoint = pathPointOne;
                } else if (moveRight)
                {
                    currentPoint = pathPointTwo;
                }
                else if (moveDown)
                {
                    currentPoint = pathPointThree;
                }
                else if (moveLeft)
                {
                    currentPoint = pathPointFour;
                }
                moveUp = false;
                moveRight = false;
                moveDown = false;
                moveLeft = false;
                moving = false;
                Path.findSpots();
            }
        }
    }

    public void GameRestart()
    {
        transform.position = PointZero.transform.position;
        currentPoint = PointZero;
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        playerAnim.SetTrigger("hide");
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "leaves")
        {
            Debug.Log("I got leaves!");
            PS.leaves += 1;
            PS.LeafCount.text = PS.leaves.ToString();
        }
        else if (otherObject.tag == "blessing")
        {
            Debug.Log("I got a blessing!");
            PS.blessings += 1;
            PS.BlessingText.text = PS.blessings.ToString();
        } else if (otherObject.tag == "comfort")
        {
            PS.faithHealth += 1;
            PS.faithHealthSlider.value = PS.faithHealth;
            PS.CalculateBlessingChance();
            PS.blessingPercentText.text = PS.blessingPercent.ToString();
        }
        else if (otherObject.tag == "enemy" || otherObject.tag == "gossiper")
        {

            Debug.Log("I an enemy spotted me!");
            if (PS.leavesActive)
            {
                PS.LeafCount.text = PS.leaves.ToString();
                PS.leavesActive = false;
                Leaves.SetActive(false);
                Debug.Log("Proctected by Leaves");
            }
            else if (PS.blessingActive)
            {

                PS.BlessingText.text = PS.blessings.ToString();
                PS.blessingActive = false;
                Debug.Log("Protected by Blessing.");
            }
            else if (otherObject.tag == "gossiper") {
                Debug.Log("I heard things...");
                PS.faithHealth -= 1;
                PS.faithHealthSlider.value = PS.faithHealth;
                GS.playerSpottedByGossip = true;
                if (PS.faithHealth == 0)
                {
                    GS.GameOver();
                }
            }
            else if (otherObject.tag == "enemy")
            {
                Debug.Log("I've been caught.");
                GS.GameOver();
            }
            
        }  
        else if (otherObject.tag == "endline")
        {
            Debug.Log("I made it home!");
            GS.Win = true;
            GS.GameOver();
        }
    }
}
