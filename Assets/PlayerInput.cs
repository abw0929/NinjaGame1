using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    [SerializeField]
    private KeyCode leftKey;
    [SerializeField]
    private KeyCode rightKey;
    [SerializeField]
    private KeyCode jumpKey;
    [SerializeField]
    private KeyCode downKey;

    [SerializeField]
    private MoveControl targetCharacter;


	
	void Update () {
        if (Input.GetKey(leftKey))
        {
            targetCharacter.MoveHorizontal(-1f);
        }
        else if (Input.GetKey(rightKey))
        {
            targetCharacter.MoveHorizontal(1f);
        }
        else if (Input.GetKey(downKey))
        {
            targetCharacter.Land(1f);
        }
        if (Input.GetKeyDown(jumpKey))
        {
            targetCharacter.Jump(1f);
        }
    }
}
