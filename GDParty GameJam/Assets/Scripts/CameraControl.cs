using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraControl : MonoBehaviour
{
    Camera cam;

    public Transform target;
    public Vector3 offset;

    [Range(1, 10)]
    public float smoothFactor;

    [Header("Estresse")]
    Volume volume;
    public static Vignette vignette;
    public static bool isMuitoEstressado = false;
    public float aceleracaoDaIntensidade;
    [SerializeField] float aceleracaoDaMeditacao;
    [SerializeField] GameObject redemoinho;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        volume = GetComponent<Volume>();
        if (volume != null)
        {
            redemoinho.SetActive(true);
            volume.profile.TryGet(out vignette);
            vignette.intensity.value = 0f;
        }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Follow();
        if (volume)
        {
            VignetteFollow();
        }
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    void VignetteFollow()
    {
        // if (isMuitoEstressado && !Estresse.isEstresseResetando)
        // {
        //     vignette.intensity.value += aceleracaoDaIntensidade / 100 * Time.deltaTime;
        // }
        // else if (Estresse.isEstresseResetando && vignette.intensity.value >= 0f)
        // {
        //     vignette.intensity.value -= aceleracaoDaMeditacao / 100 * Time.deltaTime;
        // }

        vignette.center.value = cam.WorldToViewportPoint(target.position);

        float escala = 3.65714f * Mathf.Pow(vignette.intensity.value, 2) - 8.57714f * vignette.intensity.value + 6.09714f;
        redemoinho.transform.localScale = new Vector2(escala, escala);
    }

    void ChecaMortePorEstresse()
    {
        if (!isMuitoEstressado) return;
        // if(estresse != 1f) return;
        // WaitForSeconds(segundosAteMorrer);
        // if(estresse == 1f) Morrer();
    }


}
