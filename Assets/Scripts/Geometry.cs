using UnityEngine;

public static class Geometry
{
    public static Quaternion GetQuaternionFromCathetuses(Vector2 vector)
    {
        return Quaternion.Euler(0, 0, GetAngleFromCathetuses(vector));
    }

    public static float GetAngleFromCathetuses(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    public static Vector2 GetDirection(float angle)
    {
        var x = 1 * Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = 1 * Mathf.Sin(angle * Mathf.Deg2Rad);

        return new Vector2(x, y).normalized;
    }
}