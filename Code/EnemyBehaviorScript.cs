using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EnemyBehaviorScript : MonoBehaviour
{
    string nameOfEnemy;
    public GameObject imageOfEnemy;
    float baseSecondRotation = 3.0f;
    string rotationPosition = "none";
    
    float rotationTime = 0f;
    public float randomSecondRotation;
    //int leftRotation = -90;
    //int rightRotation = 90;
    //int upRotation = 180;
    //int baseRotation = 0;

    public GameStatusScript GS;

    public GameObject Light;
    public GameObject Found;

    public Sprite upPosition;
    public Sprite downPosition;
    public Sprite sidePosition;

    public Animator lookerAnimator;
    public Animator gossipAnimatior;
    public PlayerStatsScript PS;

    public AudioSource FoundSound;

    public bool noLight = false;

    //public bool blockedVision = false;
    //public List<string> blockedPositions;
    //public bool rotatedFully = false;
    //next time
    //for cone area only!

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        nameOfEnemy = this.gameObject.name;
        if (nameOfEnemy == "Looker")
        {
            ChooseSpeed();
            ChooseRotation();
        } else if (nameOfEnemy == "horseRider"){
            ChooseSpeed();
            //choose direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotationTime += Time.deltaTime;
        if (rotationTime >= randomSecondRotation && gameObject.name == "Looker")
        {
            RotateLookerCone();
            rotationTime = 0f;
        }
        if (GS.restart && (gameObject.name == "Looker" || gameObject.name == "Gossiper")) {
            Destroy(gameObject);
        }
        if (!FoundSound.isPlaying && nameOfEnemy == "Looker")
        {
            FoundSound.Stop();
            Found.SetActive(false);
        }
    }

    void ChooseRotation() {
        //Random random = new Random();
        int startPosition = Random.Range(1, 5);
        switch (startPosition) {
            case 1:
                rotationPosition = "up";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = upPosition;
                Light.transform.position = new Vector3(Light.transform.position.x, Light.transform.position.y + 2, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, upRotation);
                break;
            case 2:
                rotationPosition = "right";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = sidePosition;
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                Light.transform.position = new Vector3(Light.transform.position.x + 3.45f, Light.transform.position.y, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, rightRotation);
                break;
            case 3:
                rotationPosition = "down";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = downPosition;
                Light.transform.position = new Vector3(Light.transform.position.x, Light.transform.position.y - 2, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, baseRotation);
                break;
            case 4:
                rotationPosition = "left";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = sidePosition;
                Light.transform.position = new Vector3(Light.transform.position.x - 3.45f, Light.transform.position.y, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, leftRotation);
                break;
        }
    }

    void ChooseSpeed() {
        randomSecondRotation = Random.Range(0.5f, 3.1f);
        if(randomSecondRotation < 0.5f || randomSecondRotation > 3.1f)
        {
            randomSecondRotation = baseSecondRotation;
        }
    }

    void RotateLookerCone()
    {
        //this.transform.Quaternion.(0, 0, 0);
        //for cone area only!
        switch (rotationPosition)
        {
            case "left":
                rotationPosition = "up";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = upPosition;
                Light.transform.position = new Vector3(Light.transform.position.x + 3.45f, Light.transform.position.y + 2, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, rightRotation);
                break;
            case "up":
                rotationPosition = "right";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = sidePosition;
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                Light.transform.position = new Vector3(Light.transform.position.x + 3.45f, Light.transform.position.y - 2, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, baseRotation);
                break;
            case "right":
                rotationPosition = "down";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = downPosition;
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                Light.transform.position = new Vector3(Light.transform.position.x - 3.45f, Light.transform.position.y - 2, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, leftRotation);
                break;
            case "down":
                rotationPosition = "left";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = sidePosition;
                Light.transform.position = new Vector3(Light.transform.position.x - 3.45f, Light.transform.position.y + 2, Light.transform.position.z);
                //this.transform.rotation = Quaternion.Euler(0, 0, upRotation);
                break;
        }
        Light.SetActive(true);
    }

    public void RaiseSpeed()
    {
        if (randomSecondRotation > 0.5f)
        {
            randomSecondRotation -= 0.5f;
            if(randomSecondRotation < 0.5f)
            {
                randomSecondRotation = 0.5f;
            }
        }
    }

    public void NoLight()
    {
        noLight = true;
        Light.SetActive(false);
        RevealFound();
        switch (rotationPosition)
        {
            case "up":
                rotationPosition = "up";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy/Looker_Back_Shocked");
                break;
            case "right":
                rotationPosition = "right";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy/Looker_Side_Shocked");
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case "down":
                rotationPosition = "down";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy/Looker_Front_Shocked"); ;
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                break;
            case "left":
                rotationPosition = "left";
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy/Looker_Side_Shocked"); ;
                break;
        }
    }

    public void RevealFound()
    {
        Found.SetActive(true);
        FoundSound.Play();

    }

    public void HideFound()
    {
        Found.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            GossipStuff();
            FoundSound.Play();
        }
    }
    public void GossipStuff()
    {
        gossipAnimatior.SetTrigger("loud");
    }

}
