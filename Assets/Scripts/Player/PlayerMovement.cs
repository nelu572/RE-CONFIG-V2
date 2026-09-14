using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public sealed class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private BoxCollider2D bodyCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private SpriteRenderer visual;

    [Header("Movement")]
    [SerializeField, Min(0f)] private float maxRunSpeed = 7f;
    [SerializeField, Min(0f)] private float groundAcceleration = 72f;
    [SerializeField, Min(0f)] private float airAcceleration = 42f;

    [Header("Jump")]
    [SerializeField, Min(0f)] private float jumpImpulse = 12f;
    [SerializeField, Min(0f)] private float jumpCutMultiplier = 0.5f;

    [Header("Forgiveness")]
    [SerializeField, Min(0f)] private float coyoteTime = 0.12f;
    [SerializeField, Min(0f)] private float jumpBufferTime = 0.12f;

    [Header("Ground Check")]
    [SerializeField] private Vector2 groundCheckSize = new(0.58f, 0.12f);
    [SerializeField] private LayerMask groundLayers;

    private float moveInput;
    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool grounded;

    public bool IsGrounded => grounded;

    private void Reset()
    {
        body = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        visual = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (jumpBufferTimer > 0f)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
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
    }

    // PlayerInput's Send Messages mode invokes these from the existing Player action map.
    private void OnMove(InputValue value)
    {
        moveInput = Mathf.Clamp(value.Get<Vector2>().x, -1f, 1f);

        if (moveInput != 0f && visual != null)
        {
            visual.flipX = moveInput < 0f;
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else if (body.linearVelocity.y > 0f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * jumpCutMultiplier);
        }
    }

    public void Configure(
        Rigidbody2D configuredBody,
        BoxCollider2D configuredCollider,
        Transform configuredGroundCheck,
        SpriteRenderer configuredVisual,
        LayerMask configuredGroundLayers)
    {
        body = configuredBody;
        bodyCollider = configuredCollider;
        groundCheck = configuredGroundCheck;
        visual = configuredVisual;
        groundLayers = configuredGroundLayers;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
