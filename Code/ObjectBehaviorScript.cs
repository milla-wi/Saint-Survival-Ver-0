using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource SFX;
    public bool got;
    void Start()
    {
        got = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(got && !SFX.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            SFX.Play();
            got = true;
        }
    }

}
