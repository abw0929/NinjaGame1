using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum InputType
{
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3,
    Jump = 4,
    Attack = 5,
    Throw = 6
}

public enum MouseDownStatus
{
    None = 0,
    MouseDown = 1
}

public class PlayerScreenInput : MonoBehaviour {


    [SerializeField]
    private InputType buttonType;
    [SerializeField]
    private bool doPress;
    [SerializeField]
    private MoveControl targetCharacter;

    private MouseDownStatus mouseDownStatus;
    private int touchID;


    void Awake()
    {
        mouseDownStatus = MouseDownStatus.None;
        touchID = -1;
    }

    void Update()
    {
        //    if(Input.touchCount > 0)
        //    {
        //        for (int i = 0; i < Input.touchCount; i++)
        //        {
        //            Touch touch = Input.GetTouch(i);
        //            if (touch.phase == TouchPhase.Began)
        //            {
        //                RaycastHit2D hitInfo = Physics2D.Raycast(
        //                    Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
        //                if (hitInfo.collider != null)
        //                {
        //                    string collidername = hitInfo.collider.name;
        //                    if (collidername == name)
        //                    {
        //                        SendMouseSignal();
        //                        mouseDownStatus = MouseDownStatus.MouseDown;
        //                        touchID = i;
        //                    }
        //                }
        //            }
        //            else if(touch.phase == TouchPhase.Ended && i == touchID)
        //            {
        //                mouseDownStatus = MouseDownStatus.None;
        //                touchID = -1;
        //            }
        //        }
        //    }

        if (doPress && mouseDownStatus == MouseDownStatus.MouseDown)
        {
            SendMouseSignal();
        }
    }

    public void OnMouseDown()
    {
        mouseDownStatus = MouseDownStatus.MouseDown;
        SendMouseSignal();
    }

    public void OnMouseUp()
    {
        mouseDownStatus = MouseDownStatus.None;
    }

    void SendMouseSignal()
    {
        switch (buttonType)
        {
            case InputType.Left:
                {
                    targetCharacter.MoveHorizontal(-1f);
                    break;
                }
            case InputType.Right:
                {
                    targetCharacter.MoveHorizontal(1f);
                    break;
                }
            case InputType.Up:
                {
                    break;
                }
            case InputType.Down:
                {
                    targetCharacter.MoveLand(-1f);
                    break;
                }
            case InputType.Jump:
                {
                    targetCharacter.MoveJump(1f);
                    break;
                }
            case InputType.Attack:
                {
                    break;
                }
            case InputType.Throw:
                {
                    break;
                }
        }
    }

}
