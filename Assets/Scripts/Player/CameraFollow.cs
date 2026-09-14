using UnityEngine;

public sealed class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 horizontalBounds = new(0f, 10f);

    private float horizontalVelocity;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        var desiredX = Mathf.Clamp(target.position.x, horizontalBounds.x, horizontalBounds.y);
        var nextX = Mathf.SmoothDamp(transform.position.x, desiredX, ref horizontalVelocity, GameplayValues.Camera.FollowSmoothTime);
        transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
    }

    public void Configure(Transform configuredTarget, Vector2 configuredBounds)
    {
        target = configuredTarget;
        horizontalBounds = configuredBounds;
    }
}
