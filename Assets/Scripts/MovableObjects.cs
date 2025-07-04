using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging;
    [SerializeField] private Camera cam;
    [SerializeField] private bool canDrag;

    // Define the min and max bounds for x and y movement
    [SerializeField] private Vector2 xBounds = new Vector2(-5f, 5f);
    [SerializeField] private Vector2 yBounds = new Vector2(-5f, 5f);

    private void Awake()
    {
        cam = GameObject.FindWithTag("Cam2").GetComponent<Camera>();
    }

    private void Update()
    {
        canDrag = cam.gameObject.activeSelf;
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging && canDrag)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;

            // Clamp the x and y values to the defined bounds
            float clampedX = Mathf.Clamp(newPosition.x, xBounds.x, xBounds.y);
            float clampedY = Mathf.Clamp(newPosition.y, yBounds.x, yBounds.y);

            // Update position with clamped x and y, keeping z unchanged
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
