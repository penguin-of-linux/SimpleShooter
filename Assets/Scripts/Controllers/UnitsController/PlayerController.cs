using Core.MapDto;
using UnityEngine;

// ReSharper disable PossibleLossOfFraction

namespace Controllers.UnitsController
{
    public class PlayerController : UnitBaseController
    {
        public override void Start()
        {
            base.Start();
        
            var crossHair = ResourceLoader.GetCrossHairTexture();
            Cursor.SetCursor(crossHair, new Vector2(crossHair.width / 2, crossHair.height / 2), CursorMode.ForceSoftware);

            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            Unit = map.Units[MapObjectId];
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        
            MoveToDirection(direction);
            var position = transform.position;
            Camera.main.SetPosition(position.x, position.y);
        }

        public void Update()
        {
            HandleKeyboard();
            HandleMouse();
        }

        private void HandleKeyboard()
        {
            var w = Input.GetKey(KeyCode.W) ? 1 : 0;
            var s = Input.GetKey(KeyCode.S) ? -1 : 0;
            var a = Input.GetKey(KeyCode.A) ? 1 : 0;
            var d = Input.GetKey(KeyCode.D) ? -1 : 0;

            var controlVector = new Vector2(w + s, a + d);
            var defaultAngle = 90;
            direction = controlVector.magnitude > 0.1f
                ? Geometry.GetDirection(defaultAngle + Geometry.GetAngleFromCathetuses(controlVector))
                : Vector2.zero;
        }

        private void HandleMouse()
        {
            var clickedLeft = Input.GetMouseButtonDown(0);

            var mouseRadius = MousePosition - transform.position;
            var mouseDirection = mouseRadius.normalized;

            if (clickedLeft)
            {
                Shoot(mouseDirection);
            }

            UpdatePlayerDirection();
        }

        private void UpdatePlayerDirection()
        {
            var rotateDirection = (MousePosition - transform.position).normalized;
            if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
        }

        private Vector2 direction;

        private Map map;
    
        // ReSharper disable once PossibleNullReferenceException
        private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).AsVector2();
    }
}