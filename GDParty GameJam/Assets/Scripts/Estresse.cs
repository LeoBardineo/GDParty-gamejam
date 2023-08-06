using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Estresse : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] float barraDeEstresse;
    public float aceleracaoDoEstresse, aceleracaoDaMeditacao;
    public static bool isEstresseResetando;
    public static float vignetteThreshold = 70f;
    public float minEstresse = 0f;
    public static bool lockEstresse = false;
    public float segundosAteMorrerPorEstresse = 5f;
    Player player;

    void Start()
    {
        isEstresseResetando = false;
        barraDeEstresse = 0f;
        player = Player.GetPlayer();
    }

    void Update()
    {
        if (lockEstresse) return;
        if (Input.GetKeyDown(KeyCode.Home))
        {
            DescontarEstresse(0.5f);
        }

        // Se o estresse está diminuindo,
        // continue a diminuir até atingir 0f ou o minEstresse.
        // Se o estresse não está diminuindo,
        // continue aumentando até dar 100f.
        if (isEstresseResetando && barraDeEstresse >= 0f)
        {
            barraDeEstresse -= aceleracaoDaMeditacao * Time.deltaTime;
            if (barraDeEstresse <= vignetteThreshold)
            {
                CameraControl.isMuitoEstressado = false;
                if (barraDeEstresse <= minEstresse)
                {
                    minEstresse = 0f;
                    isEstresseResetando = false;
                }
            }
        }
        else if (!isEstresseResetando && barraDeEstresse <= 100f)
        {
            barraDeEstresse += aceleracaoDoEstresse * Time.deltaTime;
            if (barraDeEstresse > vignetteThreshold)
            {
                CameraControl.isMuitoEstressado = true;
                if (barraDeEstresse >= 100f)
                {
                    MorrerPorEstresse();
                }
            }
            else
            {
                CameraControl.isMuitoEstressado = false;
            }

        }
        CameraControl.vignette.intensity.value = barraDeEstresse / 100f;
    }

    public void DescontarEstresse(float porcentagemDeAcerto)
    {
        minEstresse = barraDeEstresse * (1f - porcentagemDeAcerto);
        isEstresseResetando = true;
    }

    public void ResetarOEstresse()
    {
        minEstresse = 0f;
        barraDeEstresse = 0f;
        isEstresseResetando = false;
    }

    public void MorrerPorEstresse()
    {
        StartCoroutine(Morra());
    }

    IEnumerator Morra()
    {
        yield return new WaitForSeconds(segundosAteMorrerPorEstresse);
        if (barraDeEstresse >= 100f)
        {
            player.Morrer();
        }
    }

    // void ResetarOEstresseLentamente()
    // {
    //     minEstresse = 0f;
    //     isEstresseResetando = true;
    // }
}
