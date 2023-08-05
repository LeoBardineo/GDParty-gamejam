using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmaResgate : MonoBehaviour
{
    public GameObject AlmaPulo, AlmaPuloDuplo, AlmaDash, AlmaDashAr, AlmaFigurante, AlmaFigurante2;
    public Player player;
    public int almasColetadas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "AlmaPulo")
        {
            almasColetadas += 1;
            player.jump = 1000;
            AlmaPulo.SetActive(false);
        }
    }
}
