using UnityEngine;

public static class CameraExtensions
{
    public static void SetPosition(this Camera camera, float x, float y)
    {
        camera.transform.position = new Vector3(x, y, camera.transform.position.z);
    }
}