using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 10f;

    bool isAlive = true;

    //Cached component references
    Rigidbody2D playerRigidBody2d;
    Animator playerAnimator;
    Collider2D playerCollider2d;

    void Start()
    {
        playerRigidBody2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        Climb();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody2d.velocity.y);
        playerRigidBody2d.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody2d.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (playerCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                playerRigidBody2d.velocity += jumpVelocityToAdd;
            }
        }
    }

    private void Climb()
    {
        if (playerCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(playerRigidBody2d.velocity.x, controlThrow * climbSpeed);
            playerRigidBody2d.velocity = climbVelocity;

            bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody2d.velocity.y) > Mathf.Epsilon;
            playerAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody2d.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody2d.velocity.x), 1f);
        }
    }
}
