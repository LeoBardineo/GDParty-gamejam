using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Alma : MonoBehaviour
{
    [SerializeField] string skillDesbloqueada;
    [SerializeField] Victory telaDeVitoria;
    bool playerPerto = false;

    [Header("Diálogo")]
    public GameObject dialogo;
    public TextMeshProUGUI nomeTM, textoTM;
    public string txtNome;
    [TextArea]
    public string txtDialogo;
    bool dialogoFinalizou = false;
    int paginaAtual;

    void Start()
    {

    }

    void Update()
    {
        if (!Player.Paused && playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (!Player.inDialogo)
            {
                comecaDialogo();
            }
            else if (Player.inDialogo && !dialogoFinalizou)
            {
                passaDeLinha();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPerto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPerto = false;
        }
    }

    void coletarAlma()
    {
        Player.almasColetadas += 1;
        if (skillDesbloqueada != null && skillDesbloqueada != "")
        {
            if (skillDesbloqueada == "jump")
                Player.jump = 1450;

            if (skillDesbloqueada == "doubleJump")
                Player.doubleJumpUnlocked = true;

            if (skillDesbloqueada == "dash")
                Player.dashUnlocked = true;

            if (skillDesbloqueada == "jumpDash")
                Player.jumpDashUnlocked = true;
        }
        gameObject.SetActive(false);
        telaDeVitoria.almasColetadas.text = Player.almasColetadas + "/6";
    }

    void comecaDialogo()
    {
        paginaAtual = 1;
        textoTM.pageToDisplay = 1;
        nomeTM.text = txtNome;
        textoTM.text = txtDialogo;
        Player.inDialogo = true;
        dialogo.SetActive(true);
        Estresse.lockEstresse = true;
        dialogoFinalizou = false;
    }

    void passaDeLinha()
    {
        int paginasTotais = textoTM.textInfo.pageCount;
        if (paginaAtual < paginasTotais)
        {
            paginaAtual++;
            textoTM.pageToDisplay++;
        }
        else
        {
            terminaDialogo();
            coletarAlma();
        }
    }

    void terminaDialogo()
    {
        Player.inDialogo = false;
        dialogo.SetActive(false);
        Estresse.lockEstresse = false;
        dialogoFinalizou = true;
    }
}
