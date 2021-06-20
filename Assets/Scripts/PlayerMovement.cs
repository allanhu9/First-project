using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;

    private void Awake()
    {//runs when the game starts
        //get references
        body = GetComponent<Rigidbody2D>();//select the rigid body
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {//runs every frame

        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(body.velocity.x) < maxSpeed)
            body.AddForce(new Vector2(horizontalInput * acceleration, 0));
        if (horizontalInput == 0)
        {
            body.AddForce(new Vector2(-body.velocity.x / 2, 0));
        }
        // body.velocity = new Vector2(currentSpeed, body.velocity.y);//vector gives speed to a rigid body.

        //Body.velocity.y just keeps it the same as before.
        //input.getAxis("Horizontal") - Left/A = -1 or Right/D = +1


        if (wallJumpCooldown < 0.2)
        {
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0.5f;
                body.velocity = new Vector2(0, body.velocity.y);
            }
            else
            {
                body.gravityScale = 2f;
            }

            if (Input.GetKey(KeyCode.Space))
            {//input.getkey checks if the key is being pressed.
                Jump();
            }
        }
        else//timer goes for wall jump
        {
            wallJumpCooldown += Time.deltaTime;
        }


        //code to set direction based on movement
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //set animator parameters

        anim.SetBool("running", horizontalInput != 0);//horizontal input is not 0, so you're moving.
        anim.SetBool("grounded", isGrounded());

    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);

        }
        else if (onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            //Mathf.sign gives the sign of a value
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, jumpSpeed);
        }

        anim.SetTrigger("jump");

    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
