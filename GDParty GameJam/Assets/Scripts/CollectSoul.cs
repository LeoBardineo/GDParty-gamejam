using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CollectSoul : MonoBehaviour
{
    public GameObject player;
    public int almaNum;
    public TextMeshProUGUI almasColetadas;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        almaNum = player.GetComponent<AlmaResgate>().almasColetadas;
        almasColetadas.text = almaNum + "/6";
    }
}
