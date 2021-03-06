using UnityEngine;

namespace Components
{
    public class PlayerKeyboardHandleComponent : MonoBehaviour
    {
        void Update()
        {
            var w = Input.GetKey(KeyCode.W) ? 1 : 0;
            var s = Input.GetKey(KeyCode.S) ? -1 : 0;
            var a = Input.GetKey(KeyCode.A) ? 1 : 0;
            var d = Input.GetKey(KeyCode.D) ? -1 : 0;

            var controlVector = new Vector2(w + s, a + d);
            var defaultAngle = 90;

            TransformComponent.Direction = controlVector.magnitude > 0.1f
                ? Geometry.GetDirection(defaultAngle + Geometry.GetAngleFromCathetuses(controlVector))
                : Vector2.zero;
        }

        private TransformComponent TransformComponent => transformComponent ? transformComponent : transformComponent = GetComponent<TransformComponent>();
        private TransformComponent transformComponent;
    }
}