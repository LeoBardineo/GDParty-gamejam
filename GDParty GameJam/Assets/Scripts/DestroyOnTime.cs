using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float timer;
    void Update()
    {
            Destroy(this,timer);
    }
}
