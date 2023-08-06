using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMusicaTocandoPelasScenes : MonoBehaviour
{
    public string NomeTag, NameTagPassada;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(NomeTag);
        if (objs.Length > 1)
        { 
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        Destroy(GameObject.FindGameObjectWithTag(NameTagPassada));
    }
}
