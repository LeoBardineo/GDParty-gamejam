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
    public bool lockEstresse = true;

    void Start()
    {
        isEstresseResetando = false;
        barraDeEstresse = 0f;
    }

    void Update()
    {
        // refazer, tenq ser baseado na qtd de pontos acertados
        // colocar o vignette value = barraDeEstresse
        if (lockEstresse) return;
        if (Input.GetKeyDown(KeyCode.Home))
        {
            isEstresseResetando = !isEstresseResetando;
        }

        if (isEstresseResetando && barraDeEstresse >= 0f)
        {
            barraDeEstresse -= aceleracaoDaMeditacao * Time.deltaTime;
            if (barraDeEstresse <= vignetteThreshold)
            {
                CameraControl.isMuitoEstressado = false;
                if (barraDeEstresse <= 0f)
                {
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
                    // Player.Morrer();
                }
            }
            else
            {
                CameraControl.isMuitoEstressado = false;
            }

        }

    }

    void ResetarOEstresse()
    {
        isEstresseResetando = true;
    }
}
