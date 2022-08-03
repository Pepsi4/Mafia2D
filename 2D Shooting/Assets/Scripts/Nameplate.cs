using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Nameplate : MonoBehaviour
{
    public GameObject Panel;
    //public TextAsset textInTable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        Panel.SetActive(true);
        gameObject.GetComponent<RPGTalk>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        Panel.SetActive(false);
    }
}
