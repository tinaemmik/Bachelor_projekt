using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    public LayerMask groundLayer;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    private BoxCollider2D boxCollider;
    //private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        //Move player
        myRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, myRigidbody.velocity.y);

        //flip player sprite
        if(horizontalInput> 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded())
        {
            Jump();
        }

        //Parameter for animator
        anim.SetBool("run",horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpPower);
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
