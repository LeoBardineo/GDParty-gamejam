using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmaResgate : MonoBehaviour
{
    public GameObject AlmaPulo, AlmaPuloDuplo, AlmaDash, AlmaJumpDash, AlmaFigurante, AlmaFigurante2;
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

        if (other.name == "AlmaPuloDuplo")
        {
            almasColetadas += 1;
            player.doubleJumpUnlocked = true;
            AlmaPuloDuplo.SetActive(false);
        }

        if (other.name == "AlmaDash")
        {
            almasColetadas += 1;
            player.dashUnlocked = true;
            AlmaDash.SetActive(false);
        }

        if (other.name == "AlmaJumpDash")
        {
            almasColetadas += 1;
            player.jumpDashUnlocked = true;
            AlmaJumpDash.SetActive(false);
        }
        if (other.name == "AlmaFigurante")
        {
            almasColetadas += 1;
            AlmaFigurante.SetActive(false);
        }
        if (other.name == "AlmaFigurante2")
        {
            almasColetadas += 1;
            player.jumpDashUnlocked = true;
            AlmaFigurante2.SetActive(false);
        }
    }
}
