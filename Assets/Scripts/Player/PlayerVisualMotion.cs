using UnityEngine;

public sealed class PlayerVisualMotion : MonoBehaviour
{
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int FaceDirection = Animator.StringToHash("FaceDirection");

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator animator;

    private void Update()
    {
        animator.SetFloat(MoveSpeed, Mathf.Abs(movement.HorizontalSpeed));
        animator.SetFloat(VerticalSpeed, movement.VerticalSpeed);
        animator.SetBool(Grounded, movement.IsGrounded);
        animator.SetFloat(FaceDirection, movement.MoveInput);
    }

    public void Configure(PlayerMovement configuredMovement, Animator configuredAnimator)
    {
        movement = configuredMovement;
        animator = configuredAnimator;
    }

}
