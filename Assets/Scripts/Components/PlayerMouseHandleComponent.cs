using UnityEngine;

namespace Components
{
    public class PlayerMouseHandleComponent : MonoBehaviour
    {
        void Update()
        {
            var clickedLeft = Input.GetMouseButtonDown(0);

            var mouseRadius = MousePosition - transform.position;
            var mouseDirection = mouseRadius.normalized;

            if (clickedLeft)
            {
                //ShootComponent.Direction = mouseDirection;
            }

            UpdatePlayerDirection();
        }

        private void UpdatePlayerDirection()
        {
            var rotateDirection = (MousePosition - transform.position).normalized;
            if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
        }
         
        private ShootComponent ShootComponent => shootComponent ? shootComponent : shootComponent = GetComponent<ShootComponent>();
        private ShootComponent shootComponent;
        // ReSharper disable once PossibleNullReferenceException
        private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).AsVector2();
    }
}