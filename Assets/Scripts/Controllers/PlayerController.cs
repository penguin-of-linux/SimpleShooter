using DefaultNamespace;
using UnityEngine;

public class PlayerController : UnitBaseController
{
    // Use this for initialization
    public void Start()
    {
        base.Start();
        
        var crossHair = ResourceLoader.GetCrossHairTexture();
        Cursor.SetCursor(crossHair, new Vector2(crossHair.width / 2, crossHair.height / 2), CursorMode.ForceSoftware);
    }

    public void FixedUpdate()
    {
        base.FixedUpdate();
        
        MoveToDirection(direction);
        Camera.main.SetPosition(transform.position.x, transform.position.y);
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
        var direction = (MousePosition - transform.position).normalized;
        if (direction.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(direction);
    }

    private Vector2 direction;  
    
    private Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition).AsVector2();
}