using UnityEngine;

public class Boid : MonoBehaviour
{
    [Range(0f, 10f)]
    public float speed = 5f;
    [Range(0f, 10f)]
    public float rotationSpeed = 5f;
    [Range(0f, 5f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 5f)]
    public float separationRadius = 1f;
    [Range(0f, 5f)]
    public float separationWeight = 1f;

    [Header("Obstacle")]
    [Range(0f, 360f)]
    public float obstacleAvoidanceAngle = 45f;
    [Range(0f, 5f)]
    public float obstacleAvoidanceDistance = 2f;
    [Range(0f, 5f)]
    public float avoidanceForceFactor = 1.0f;
    public LayerMask obstacleMask;

    // Control variables for behavior
    [SerializeField] private bool separationEnabled = true;
    [SerializeField] private bool alignmentEnabled = true;
    [SerializeField] private bool cohesionEnabled = true;

    void Update()
    {
        UpdateSettingValues();

        // Move forward
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Find nearby boids | perception
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, neighborRadius);

        Vector2 averageHeading = Vector2.zero;
        Vector2 averagePosition = Vector2.zero;
        Vector2 avoidanceHeading = Vector2.zero;
        int numNeighbors = 0;

        // Need to know about boids around them (neighborhood)
        foreach (Collider2D collider in colliders)
        {
            if (collider != this && collider.CompareTag("Boid"))
            {
                Vector2 neighborDirection = collider.transform.position - transform.position;
                float distance = neighborDirection.magnitude;

                // Separation
                if (separationEnabled && distance < separationRadius)
                {
                    avoidanceHeading -= neighborDirection.normalized * (separationRadius - distance) / separationRadius;
                }
                else
                {
                    if (alignmentEnabled)
                        averageHeading += (Vector2)collider.transform.up; // average heading(direction), use collider as perception to detect boids

                    if (cohesionEnabled)
                        averagePosition += (Vector2)collider.transform.position; //average position, use the collider as perception to detect boids

                    numNeighbors++;
                }
            }
        }

        // Obstacle avoidance
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, obstacleAvoidanceDistance, obstacleMask);
        if (hit.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            avoidanceHeading += avoidanceDirection * avoidanceForceFactor; // Multiplying by a factor to increase the avoidance force
        }

        // Alignment, cohesion, and steering force calculation
        if (numNeighbors > 0)
        {
            // Alignment
            if (alignmentEnabled)
                averageHeading /= numNeighbors;

            // Cohesion
            if (cohesionEnabled)
                averagePosition /= numNeighbors;

            // Apply forces
            Vector2 steeringForce = Vector2.zero;

            if (alignmentEnabled)
            {
                Vector2 alignmentForce = averageHeading - (Vector2)transform.up;
                //alignment force added (accumulated forces)
                steeringForce += alignmentForce;
            }

            if (cohesionEnabled)
            {
                Vector2 cohesionForce = averagePosition - (Vector2)transform.position;
                //cohesion force added (accumulated forces)
                steeringForce += cohesionForce;
            }

            //separation force added (accumulated forces)
            steeringForce += (avoidanceHeading * separationWeight);

            // Update rotation
            transform.up = Vector2.Lerp(transform.up, (Vector2)transform.up + steeringForce, rotationSpeed * Time.deltaTime); //(0,1) (-1,-1) -> (-1, 0)
        }
        else // Apply only obstacle avoidance if there are no neighbors
        {
            transform.up = Vector2.Lerp(transform.up, (Vector2)transform.up + avoidanceHeading, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateSettingValues()
    {
        //update flock values
        separationEnabled = FlockController.Instance.GetSeperationToggleState();
        alignmentEnabled = FlockController.Instance.GetAlignmentToggleState();
        cohesionEnabled = FlockController.Instance.GetCohesionToggleState();

        //update the boid settings
        speed = FlockController.Instance.GetSpeed();
        neighborRadius = FlockController.Instance.GetNeigbourRadius();
        separationRadius = FlockController.Instance.GetSeperationRadius();
        separationWeight = FlockController.Instance.GetSeperationWeight();
    }
}
