using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDireita : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    public bool active = false;
    GameObject note;
    Color old;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        old = sr.color;
    }

    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
        }

        if(Input.GetKeyDown(key) && active == true)
        {
            Destroy(note);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "NotaDireita")
        {
            active = true;
            Debug.Log("Bateu na direita");
            note = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
        Destroy(note,0.5f);
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0.5f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        sr.color = old;
    }
}