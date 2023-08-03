using UnityEngine.Audio;
using System;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float Timer;
    private bool on = true;
    void Update()
    {
        Timer += Time.deltaTime;
        if ( Timer >= 2f)
        {
           Cursor.visible = false;
        }
        
        if (Input.GetAxis("Mouse X")<0)
        {
            Cursor.visible = true;
            Timer = 0;
        }
        if(Input.GetAxis("Mouse X")>0)
        {
            Cursor.visible = true;
            Timer = 0;
        }
    }
}
