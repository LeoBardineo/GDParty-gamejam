using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationController : MonoBehaviour
{
    public GameObject minigame, player;
    public Player playerScript;
    public Estresse estresse;
    public KeyCode start, end;
    public float valorDaIntensidadeAntes;
    public static float pontos = 0f;
    public static float totalPontos = 29f;
    Object medidationGame = new Object();
    //public bool minigameOn = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetPlayer();
        estresse = player.GetComponent<Estresse>();
        pontos = 0;
    }

    public void IniciarMinigame()
    {
        Estresse.lockEstresse = true;
        playerScript.isMeditando = true;
        playerScript.rb.velocity = new Vector3(0, 0, 0);
        // playerScript.ChangeAnimationState("PlayerMeditation");
        medidationGame = Instantiate(minigame, new Vector3(player.transform.position.x, player.transform.position.y + 4.5f, 0f), Quaternion.identity);
        CameraControl.vignette.intensity.value = 0f;
        TerminaMiniGame();
    }

    void TerminaMiniGame()
    {
        StartCoroutine(MiniGameEnd());
    }

    IEnumerator MiniGameEnd()
    {
        yield return new WaitForSeconds(30.5f);
        Destroy(medidationGame);
        Debug.Log(pontos);
        Debug.Log(totalPontos);
        Debug.Log(pontos / totalPontos);
        estresse.DescontarEstresse(pontos / totalPontos);
        Estresse.lockEstresse = false;
        playerScript.isMeditando = false;
        // animar "desmeditar" (play in reverse)
    }

    public static MeditationController GetMeditationController()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.Find("MeditationControl").GetComponent<MeditationController>();
    }
}
