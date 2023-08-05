using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fumaca : MonoBehaviour
{
    [SerializeField] float aumentoDoEstresse;
    Estresse estresse;
    CameraControl cam;

    void Start()
    {
        estresse = FindObjectOfType<Estresse>();
        cam = FindObjectOfType<CameraControl>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            estresse.aceleracaoDoEstresse += aumentoDoEstresse;
            cam.aceleracaoDaIntensidade += aumentoDoEstresse;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            estresse.aceleracaoDoEstresse -= aumentoDoEstresse;
            cam.aceleracaoDaIntensidade += aumentoDoEstresse;
        }
    }
}
