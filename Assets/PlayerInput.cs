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
            targetCharacter.MoveLand(-1f);
        }
        if (Input.GetKey(jumpKey))
        {
            targetCharacter.MoveJump(1f);
        }
    }
}
