using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public sealed class PlayerMovement : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;

    [Header("이동")]
    [SerializeField] private float maxRunSpeed = 7f;
    [SerializeField] private float groundAcceleration = 72f;
    [SerializeField] private float airAcceleration = 42f;

    [Header("점프")]
    [SerializeField] private float jumpImpulse = 12f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;

    [Header("착지 보정")]
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;

    [Header("지면 감지")]
    [SerializeField] private Vector2 groundCheckSize = new(0.58f, 0.12f);
    [SerializeField] private LayerMask groundLayers;

    private float moveInput;
    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool grounded;
    private bool wasGrounded;

    public bool IsGrounded => grounded;
    public float MoveInput => moveInput;
    public float HorizontalSpeed => body.linearVelocity.x;
    public float VerticalSpeed => body.linearVelocity.y;

    private void Reset()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        jumpBufferTimer = Mathf.Max(0f, jumpBufferTimer - Time.deltaTime);
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayers) != null;
        coyoteTimer = grounded ? coyoteTime : Mathf.Max(0f, coyoteTimer - Time.fixedDeltaTime);

        var targetSpeed = moveInput * maxRunSpeed;
        var acceleration = grounded ? groundAcceleration : airAcceleration;
        body.linearVelocity = new Vector2(
            Mathf.MoveTowards(body.linearVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime),
            body.linearVelocity.y);

        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpImpulse);
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }

        if (grounded && !wasGrounded)
        {
        }

        wasGrounded = grounded;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = Mathf.Clamp(context.ReadValue<Vector2>().x, -1f, 1f);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else if (context.canceled && body.linearVelocity.y > 0f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * jumpCutMultiplier);
        }
    }
}
