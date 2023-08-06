using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morte : MonoBehaviour
{
    Player player;
    public bool isMorteInstantanea = true;

    void Start()
    {
        player = Player.GetPlayer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isMorteInstantanea)
        {
            player.Morrer();
        }
    }
}
