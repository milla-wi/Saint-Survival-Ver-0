using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{

    public GameObject Player;
    public GameObject Camera;
    public bool followPlayer = true;

    void FixedUpdate()
    {
        if (followPlayer)
        {
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2f , -2);
        }
    }
}
