using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerRespawn playerRespawn;
    void Start()
    {
        playerRespawn = GameObject.Find("Player").GetComponent<PlayerRespawn>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Uiuitabatendo");
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Eitacomomedita");
                playerRespawn.respawnPoint = transform.position;
            }
        }
    }
}
