using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverContinue : MonoBehaviour
{
    public Player player;
    public KeyCode key;

    void Awake()
    {
        player = Player.GetPlayer();
    }
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            player.Respawnar();
            Debug.Log("Reviveu mermo !");
        }
    }
}
