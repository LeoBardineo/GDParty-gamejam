using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementUtilitys : MonoBehaviour
{
	public string SceneName;
	public int sceneBuildIndex;
	
	public void LoadSceneString()
	{
		SceneManager.LoadScene (SceneName);
	}
	
	public void LoadSceneIndex()
	{
		SceneManager.LoadScene(sceneBuildIndex);
	}
    
	public void QuitGame()
    {
        Application.Quit();
        Debug.Log(" Fechou");
    }
}