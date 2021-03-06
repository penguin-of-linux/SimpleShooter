using UnityEngine;

namespace Components
{
    public class PlayerMouseRotationComponent : MonoBehaviour
    {
        void Update()
        {
            var rotateDirection = (MousePosition - transform.position).normalized;
            if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
        }

        // ReSharper disable once PossibleNullReferenceException
        private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).AsVector2();
    }
}