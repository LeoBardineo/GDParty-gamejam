using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour
{
    public PlayerRespawn player;
    public bool isDead;
    public GameObject gameOver;
    public GameObject playerDoido;
    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        if (player.gameObject.GetComponent<PlayerRespawn>().dead == false)
        {
            Debug.Log("vivo");
        }
        if (player.gameObject.GetComponent<PlayerRespawn>().dead == true)
        {
            Debug.Log("morto");
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void RestartButton()
    {
        Debug.Log("restat");
        player.gameObject.GetComponent<PlayerRespawn>().dead = false;
        gameOver.SetActive(false);
        Time.timeScale = 1;
        player.RespawnNow();
    }
}
