using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ShadowShapeGenerator : MonoBehaviour
{
    public Transform shadowCaster;     // The object casting the shadow
    public Light mainLight;            // The light source casting the shadow
    public int rayCount = 100;         // Number of rays for approximating the shadow shape
    public float maxRayDistance = 10f; // Max distance for each ray

    private PolygonCollider2D polygonCollider;

    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Find the main light if not assigned
        if (mainLight == null)
        {
            mainLight = FindObjectOfType<Light>();
        }
    }

    void Update()
    {
        GenerateShadowShape();
    }

    void GenerateShadowShape()
    {
        if (shadowCaster == null || mainLight == null) return;

        // List to store hit points for the shadow shape
        List<Vector2> shadowPoints = new List<Vector2>();

        // Calculate the direction from the light source to the shadow caster
        Vector3 lightDirection = (shadowCaster.position - mainLight.transform.position).normalized;

        // Calculate the starting angle for casting rays
        float angleIncrement = 360f / rayCount;

        // Loop through each ray direction
        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the direction for the current ray, rotating around the shadowCaster
            float angle = i * angleIncrement;
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * lightDirection;

            // Cast the ray from the shadow caster position toward the wall
            RaycastHit2D hit = Physics2D.Raycast(shadowCaster.position, rayDirection, maxRayDistance);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // If the ray hits the wall, add the hit point to the shadow points
                shadowPoints.Add(hit.point);
            }

            // Visualize the ray in the editor
            Debug.DrawRay(shadowCaster.position, rayDirection * maxRayDistance, Color.red);
        }

        // Update the PolygonCollider2D with the calculated shadow points
        if (shadowPoints.Count > 2) // Ensure at least 3 points for a valid polygon
        {
            polygonCollider.SetPath(0, shadowPoints.ToArray());
        }
    }
}
