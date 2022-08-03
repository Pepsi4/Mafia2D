using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SimpleTouchController : MonoBehaviour
{

    // PUBLIC
    public delegate void TouchDelegate(Vector2 value);
    public event TouchDelegate TouchEvent;

    public delegate void TouchStateDelegate(bool touchPresent);
    public event TouchStateDelegate TouchStateEvent;

    // PRIVATE
    [SerializeField]
    private RectTransform joystickArea;
    private bool touchPresent = false;
    private Vector2 movementVector;

    public GameObject Player;


    public Vector2 GetTouchPosition
    {
        get { return movementVector; }
    }

    private void Start()
    {
        if (Application.isMobilePlatform == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void BeginDrag()
    {
        touchPresent = true;
        if (TouchStateEvent != null)
            TouchStateEvent(touchPresent);
    }

    public void EndDrag()
    {
        touchPresent = false;
        movementVector = joystickArea.anchoredPosition = Vector2.zero;

        if (TouchStateEvent != null)
            TouchStateEvent(touchPresent);

        Player.GetComponent<PlayerMovement>().horizontalMoveMobile = 0;
        Player.GetComponent<PlayerMovement>().jump = false;
    }

    public void OnValueChanged(Vector2 value)
    {
        if (touchPresent)
        {
            Debug.Log(value.x);
            //convert the value between 1 0 to -1 +1
            value.x = ((1 - value.x) - 0.5f) * 2f;
            value.y = ((1 - value.y) - 0.5f) * 2f;

            if (value.y >= 0.65f)
            {
                Player.GetComponent<PlayerMovement>().Jump();
            }

            Player.GetComponent<PlayerMovement>().horizontalMoveMobile = value.x;
            //Player.GetComponent<PlayerMovement>().crouch = true;

            if (TouchEvent != null)
            {
                TouchEvent(movementVector);
            }
        }

    }

}
