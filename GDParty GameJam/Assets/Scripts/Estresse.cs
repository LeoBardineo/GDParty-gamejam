using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Estresse : MonoBehaviour
{
    // [SerializeField] [Range(0, 100)] int intesidade;
    // [SerializeField] Vignette vignette;
    // [SerializeField] Volume volume;

    void Start()
    {
        // volume = CameraControl.gameObject.GetComponent<Volume>();
        // volume.profile.TryGet(out vignette);
    }

    void Update()
    {
        // baseado na intensidade, aproximar o vignette do player e fade no redemoinho
    }
}
