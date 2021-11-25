using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Gold" && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().gold += 1;
           
            //UI._gold = UI._gold + 1;
            Destroy(gameObject);

        }

            else if (gameObject.tag == "Candy" && collision.gameObject.tag =="Player")
        {
            collision.gameObject.GetComponent<PlayerController>().heal += 1;

            Destroy(gameObject);
            
        }

    }
}
