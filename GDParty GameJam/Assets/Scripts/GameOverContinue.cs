using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverContinue : MonoBehaviour
{
    public Player player;
    public float timer;

    void Awake()
    {
        player = Player.GetPlayer();
    }
    void Update()
    {
        if(timer>3)
        {
            timer += Time.deltaTime;
        }
        if (Input.anyKey && timer > 3)
        {
            player.Respawnar();
            Debug.Log("Reviveu mermo !");
        }
    }
}
