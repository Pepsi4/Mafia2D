using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private string AnimName = "ElevatorDown";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test1");
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("test2");
            GetComponent<Animator>().Play(AnimName);
        }
    }
}
