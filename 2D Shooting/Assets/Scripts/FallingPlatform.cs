using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision enter");
        if (collision.gameObject.name == "Player")
        {
            GetComponent<Animator>().Play("FallingPlatform");
        }
    }
}
