using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Alma : MonoBehaviour
{
    [SerializeField] string skillDesbloqueada;
    [SerializeField] GameObject bolinhaParaAcender;
    [SerializeField] GameObject Interact;
    bool playerPerto = false;
    Animator anim;

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
        anim = GetComponent<Animator>();
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
            Interact.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPerto = false;
            Interact.SetActive(false);
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
        bolinhaParaAcender.SetActive(true);
        gameObject.SetActive(false);
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
        }
    }

    void terminaDialogo()
    {
        anim.Play("Soul_Freedom");
        StartCoroutine(terminaDialogoComDelay());
    }

    IEnumerator terminaDialogoComDelay()
    {
        yield return new WaitForSeconds(1f);
        Player.inDialogo = false;
        dialogo.SetActive(false);
        Estresse.lockEstresse = false;
        dialogoFinalizou = true;
        coletarAlma();
    }
}
