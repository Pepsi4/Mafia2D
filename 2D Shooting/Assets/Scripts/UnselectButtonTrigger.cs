using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnselectButtonTrigger : MonoBehaviour
{

    GameObject myEventSystem;
    private void Start()
    {
        myEventSystem = GameObject.Find("EventSystem");
    }
    public void UnselectButton()
    {
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
