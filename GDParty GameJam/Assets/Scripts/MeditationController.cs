using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationController : MonoBehaviour
{
    public GameObject minigame,player;
    public KeyCode start,end;
    Object upalumpa = new Object();
    //public bool minigameOn = false;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //if(minigameOn == true){ faz oq ta aqui embaixo}
        // por enquanto ta manual
        if(Input.GetKeyDown(start))
        { 
            upalumpa = Instantiate(minigame, new Vector3(player.transform.position.x, player.transform.position.y + 4.5f, 0f), Quaternion.identity);
        }
        Destroy(upalumpa,3.5f);
    }
}
