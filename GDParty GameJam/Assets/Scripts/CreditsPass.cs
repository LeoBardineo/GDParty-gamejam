using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPass : MonoBehaviour
{
    void Awake()
    {
        LoadSceneStringDelayed();
    }

    public void LoadSceneStringDelayed()
	{
		Invoke("LoadSceneString",7f);
	}
	
    public void LoadSceneString()
	{
		SceneManager.LoadScene("Menu");
	}
}
