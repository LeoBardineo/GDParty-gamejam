using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverContinue : MonoBehaviour
{
    public Player player;

    void Awake()
    {
        player = Player.GetPlayer();
    }
    void Update()
    {
        if (Input.anyKey)
        {
            player.Respawnar();
            Debug.Log("Reviveu mermo !");
        }
    }
}
