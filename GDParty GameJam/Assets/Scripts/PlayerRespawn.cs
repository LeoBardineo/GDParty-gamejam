using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPoint;
    // Start is called before the first frame update
    public void RespawnNow()
    {
        transform.position = respawnPoint;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            Debug.Log("ue");
            RespawnNow();
        }
    }
}
