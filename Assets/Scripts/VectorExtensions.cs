using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 AsVector2(this Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }
        
    public static Vector3 ToVector3(this Vector2 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }
        
    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
        
    public static Vector2Int ToVectorInt(this Vector2 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }

    public static float GetAngle(this Vector3 angles)
    {
        return angles.z;
    }

    public static bool CloseToPoint(this Vector2Int a, Vector2 b, float e = 0.2f)
    {
        return (b - a).magnitude < e;
    }
        
    public static bool CloseToPoint(this Vector2 a, Vector2 b, float e = 0.2f)
    {
        return (b - a).magnitude < e;
    }

    public static Vector2 TileCenter(this Vector2Int tileCords)
    {
        return tileCords + new Vector2(0.5f, 0.5f);
    }
}