using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    bool podeInteragir = false;
    public Animator anim;
    public GameObject cenaFinal;

    void Update()
    {
        if (podeInteragir && Player.almasColetadas >= 6)
        {
            StartCoroutine(TrocarCena());
        }
    }

    IEnumerator TrocarCena()
    {
        cenaFinal.SetActive(true);
        anim.Play("CenaFinal");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Final");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            podeInteragir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            podeInteragir = false;
        }
    }
}
