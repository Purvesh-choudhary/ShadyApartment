using UnityEngine;

public class ShadowPlatform : MonoBehaviour
{
    public GameObject wall; // Assign the wall in the Inspector
    public LayerMask wallLayer; // Set to the wall layer for raycast detection
    private GameObject shadowCollider;
    [SerializeField] Vector3 rayDirection;

    void Start()
    {
        //wall = GameObject.FindWithTag("Wall");
        // Create the shadow platform collider on the wall
        shadowCollider = new GameObject("ShadowCollider");
        shadowCollider.transform.parent = wall.transform;
        shadowCollider.AddComponent<BoxCollider2D>();
        shadowCollider.tag = "ShadowPlatform";
        shadowCollider.layer = 9;
    }

    void Update()
    {
        UpdateShadowPlatform();
    }

    void UpdateShadowPlatform()
    {
        RaycastHit hit;
             
        if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection), out hit, Mathf.Infinity, wallLayer))
      {
            // Update the collider position and size to match the shadow
            shadowCollider.transform.position = new Vector3(hit.point.x, hit.point.y, wall.transform.position.z);
            Vector3 bounds = GetComponent<Renderer>().bounds.size;
            shadowCollider.GetComponent<BoxCollider2D>().size = new Vector2(bounds.x, bounds.y);
        }
   
        
    }

    // Gizmos for visualizing raycast and platform
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw the raycast from the object towards the wall
        RaycastHit hit;
        //Vector3 localDirection = transform.TransformDirection(Vector3.back); // Local -Z axis
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(rayDirection) , out hit, Mathf.Infinity, wallLayer))
      {
            Gizmos.DrawLine(transform.position, hit.point);
            Gizmos.DrawSphere(hit.point, 0.1f); // Show the hit point as a small sphere

            // Draw the shadow platform collider
            Gizmos.color = Color.green;
            Vector3 platformPosition = new Vector3(hit.point.x, hit.point.y, wall.transform.position.z);
            Gizmos.DrawWireCube(platformPosition, new Vector3(GetComponent<Renderer>().bounds.size.x, GetComponent<Renderer>().bounds.size.y, 0.1f));
        }
    }
}
