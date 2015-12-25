using UnityEngine;
using System.Collections;

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

public class MoveControl : MonoBehaviour {


    private const float minSpeed = 1f;
    private const float maxSpeed = 5f;
    private const float minJump = 1f;
    private const float maxJump = 10f;


    [SerializeField]
    private float speedModifier;
    [SerializeField]
    private float jumpModifier;

    private new Rigidbody2D rigidbody2D;
    private new Transform transform;

    
    private OnGroundStatus _onGroundStatus;
    private OnWallStatus _onWallStatus;


    public OnGroundStatus onGroundStatus
    {
        get { return _onGroundStatus; }
    }
    public OnWallStatus onWallStatus
    {
        get { return _onWallStatus; }
    }
    private bool isHorizontalStill
    {
        get { return (rigidbody2D.velocity.x == 0); }
    }
    private bool isVerticalStill
    {
        get { return (rigidbody2D.velocity.y == 0); }
    }


    void Awake ()
    {
        speedModifier = Mathf.Clamp(speedModifier, minSpeed, maxSpeed);
        jumpModifier = Mathf.Clamp(jumpModifier, minJump, maxJump);

        rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        _onGroundStatus = OnGroundStatus.OnGround;
        _onWallStatus = OnWallStatus.None;
    }


    public void MoveHorizontal(float speed)
    {
        speed = Mathf.Clamp(speed, -1f, 1f);
        rigidbody2D.AddForce(new Vector2(speed * speedModifier * 10f, 0f));

        Vector2 velocity = rigidbody2D.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -1f * speedModifier, 1f * speedModifier);
        rigidbody2D.velocity = velocity;
    }


    public void MoveJump(float speed)
    {
        if (_onWallStatus == OnWallStatus.None && _onGroundStatus == OnGroundStatus.InAir)
            return;

        speed = Mathf.Clamp(speed, 0f, 1f);

        if (_onGroundStatus == OnGroundStatus.OnGround)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * jumpModifier);
        }
        else if(_onWallStatus == OnWallStatus.OnLeft)
        {
            rigidbody2D.velocity = new Vector2(1f * speedModifier, speed * jumpModifier);
        }
        else if (_onWallStatus == OnWallStatus.OnRight)
        {
            rigidbody2D.velocity = new Vector2(-1f * speedModifier, speed * jumpModifier);
        }
    }

    public void MoveLand(float speed)
    {
        if (_onWallStatus != OnWallStatus.None || _onGroundStatus != OnGroundStatus.InAir)
            return;

        speed = Mathf.Clamp(speed, -1f, 0f);
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * jumpModifier);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _onGroundStatus = OnGroundStatus.OnGround;
        }
        if(collision.gameObject.tag == "Wall")
        {
            if(collision.contacts[0].normal.x < 0)
            {
                _onWallStatus = OnWallStatus.OnRight;
            }
            else
            {
                _onWallStatus = OnWallStatus.OnLeft;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _onGroundStatus = OnGroundStatus.InAir;
        }
        if (collision.gameObject.tag == "Wall")
        {
            _onWallStatus = OnWallStatus.None;
        }
    }

}
