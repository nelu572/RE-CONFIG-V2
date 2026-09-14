using UnityEngine;

public static class GameplayValues
{
    public static class Player
    {
        public const float MaxRunSpeed = 7f;
        public const float GroundAcceleration = 72f;
        public const float AirAcceleration = 42f;
        public const float JumpImpulse = 12f;
        public const float JumpCutMultiplier = 0.5f;
        public const float CoyoteTime = 0.12f;
        public const float JumpBufferTime = 0.12f;
        public static readonly Vector2 GroundCheckSize = new(0.58f, 0.12f);
    }

    public static class Camera
    {
        public const float FollowSmoothTime = 0.16f;
    }
}
