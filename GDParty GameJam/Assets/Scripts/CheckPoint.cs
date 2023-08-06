using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canMeditate;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this.canMeditate == true)
        {
            Debug.Log("Eitacomomedita");
            Player.respawnPoint = this.transform.position;
        }
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Uiuitabatendo");
            this.canMeditate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Saiu da Meditacao");
            this.canMeditate = false;
        }
    }
}
