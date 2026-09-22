
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    private float moveDirection;

    [HideInInspector] public PhysicsMode physicsMode;
    [SerializeField] private float defaultGravity = 4, defaultJumpForce = 750;
    [SerializeField] private float quickSandGravity = 0.001f, quickSandJumpForce = 100;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float rayCastDistance = 0.25f;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private ParticleSystem jumpParticleSystem;

    bool canMove = true;

    private AudioSource audioSorce;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer rend;
    private Animator anim;

    public enum PhysicsMode
    {
        normal = 0,
        quickSand = 1,
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        audioSorce = GetComponent<AudioSource>();

        jump.action.started += Jump;

        physicsMode = PhysicsMode.normal;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>();

        anim.SetFloat("MoveSpeed", Mathf.Abs (rigidBody2D.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", rigidBody2D.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIsGrounded());

        if (moveDirection < 0f)
        {
            FlipSprite(true);
        }

        if (moveDirection > 0f)
        {
            FlipSprite(false);
        }

        if (physicsMode == PhysicsMode.normal)
        {
            PhysicsNormal();
        }
        else if (physicsMode == PhysicsMode.quickSand)
        {
            PhysicsQuicksand();
        }
    }

    private void FixedUpdate()
    {
        if(!canMove)
        {
            return;
        }
        rigidBody2D.linearVelocity = new Vector2(moveDirection * moveSpeed * Time.deltaTime, rigidBody2D.linearVelocity.y);
    }
    private void OnDisable()
    {
        jump.action.started -= Jump;
    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (CheckIsGrounded() || physicsMode == PhysicsMode.quickSand)
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
            jumpParticleSystem.Play();
            int randomJumpSound = Random.Range(0, jumpSounds.Length);
            print(randomJumpSound);
            audioSorce.PlayOneShot(jumpSounds[randomJumpSound]);
        }
    }

    private void PhysicsNormal()
    {
        rigidBody2D.gravityScale = defaultGravity;
        jumpForce = defaultJumpForce;
        print("Normal");
    }

    private void PhysicsQuicksand()
    {
        rigidBody2D.gravityScale = quickSandGravity;
        jumpForce = quickSandJumpForce;
        print("Quicksand");
    }
    private bool CheckIsGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayCastDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayCastDistance, whatIsGround);

        if (leftHit.collider != null && leftHit || rightHit.collider != null && rightHit)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void Takeknockback(float knockbackForce, float upwardsForce)
    {
        canMove = false;
        rigidBody2D.AddForce(new Vector2 (knockbackForce, upwardsForce));
        Invoke(nameof(CanMoveAgain), 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }
}
