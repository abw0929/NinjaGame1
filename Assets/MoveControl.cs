using UnityEngine;

public enum OnWallStatus
{
    None = 0,
    OnLeft = 1,
    OnRight = 2
}

public enum OnGroundStatus
{
    OnGround = 0,
    InAir = 1
}

public class MoveControl : MonoBehaviour
{
    private const float MIN_SPEED = 1f;
    private const float MAX_SPEED = 5f;
    private const float MIN_JUMP = 1f;
    private const float MAX_JUMP = 10f;

    [SerializeField]
    private float speedModifier = 3f;
    [SerializeField]
    private float jumpModifier = 6f;
    [SerializeField]
    private bool canClimbWall = false;

    private new Rigidbody2D rigidbody2D;
    private new Transform transform;

    private OnGroundStatus onGroundStatus;
    private OnWallStatus onWallStatus;

    public OnGroundStatus OnGroundStatus
    {
        get { return onGroundStatus; }
    }

    public OnWallStatus OnWallStatus
    {
        get { return onWallStatus; }
    }

    public bool IsHorizontalStill
    {
        get { return (rigidbody2D.velocity.x == 0); }
    }

    public bool IsVerticalStill
    {
        get { return (rigidbody2D.velocity.y == 0); }
    }


    private void Awake ()
    {
        speedModifier = Mathf.Clamp(speedModifier, MIN_SPEED, MAX_SPEED);
        jumpModifier = Mathf.Clamp(jumpModifier, MIN_JUMP, MAX_JUMP);

        rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        onGroundStatus = OnGroundStatus.OnGround;
        onWallStatus = OnWallStatus.None;
    }


    public void MoveHorizontal(float speed)
    {
        float leftBound = (!canClimbWall && onWallStatus == OnWallStatus.OnLeft ? 0f : -1f);
        float rightBound = (!canClimbWall && onWallStatus == OnWallStatus.OnRight ? 0f : 1f);

        speed = Mathf.Clamp(speed, leftBound, rightBound);
        rigidbody2D.AddForce(new Vector2(speed * speedModifier * 10f, 0f));

        rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x, -1f * speedModifier, 1f * speedModifier), rigidbody2D.velocity.y);
    }


    public void Jump(float speed)
    {
        if (onWallStatus == OnWallStatus.None && onGroundStatus == OnGroundStatus.InAir)
            return;

        speed = Mathf.Clamp(speed, 0f, 1f);

        if (onGroundStatus == OnGroundStatus.OnGround)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * jumpModifier);
        }
        else if(onWallStatus == OnWallStatus.OnLeft && canClimbWall)
        {
            rigidbody2D.velocity = new Vector2(1f * speedModifier, speed * jumpModifier);
        }
        else if (onWallStatus == OnWallStatus.OnRight && canClimbWall)
        {
            rigidbody2D.velocity = new Vector2(-1f * speedModifier, speed * jumpModifier);
        }
    }


    public void Land(float speed)
    {
        if (onWallStatus != OnWallStatus.None || onGroundStatus != OnGroundStatus.InAir)
            return;

        speed = -Mathf.Clamp(speed, 0f, 1f);
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * jumpModifier);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Platform")
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            onGroundStatus = OnGroundStatus.OnGround;
        }
        else if (collision.contacts[0].normal.x < 0)
        {
            onWallStatus = OnWallStatus.OnRight;
        }
        else if (collision.contacts[0].normal.x > 0)
        {
            onWallStatus = OnWallStatus.OnLeft;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Platform")
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            onGroundStatus = OnGroundStatus.InAir;
        }
        else
        {
            onWallStatus = OnWallStatus.None;
        }
    }

}

