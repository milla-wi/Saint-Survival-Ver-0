using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public EnemyBehaviorScript EBS;
    public PlayerStatsScript PS;
    public bool LightOff;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            if (PS.blessingActive)
            {
                LightOff = true;
                EBS.NoLight();
            }
            else if (!PS.leavesActive)
            {
                EBS.RevealFound();
                EBS.RaiseSpeed();
            }
            else {
                EBS.RevealFound();
            }

        }
    }
}
