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
    Vignette vignette;
    [SerializeField] float aceleracaoDaIntensidade;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Follow();
        vignette.intensity.value += aceleracaoDaIntensidade / 100 * Time.deltaTime;
        vignette.center.value = cam.WorldToViewportPoint(target.position);
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }


}
