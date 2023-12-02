using UnityEngine;

public class SentielFollowsPlayer : MonoBehaviour
{
    public GameObject sentinel; // Reference to the Sentinel GameObject
    [SerializeField] private float xOffset = 0f;  // Offset in the x-axis
    [SerializeField] private float smoothSpeed = 0.15f; // Speed for smooth movement (reduced for slower movement)
    private const float yPosition = 2.15f;
    private float attackDistance = 3f;

    void Update()
    {
        if (sentinel != null)
        {
            float distanceFromPlayer;

            Vector2 playerPosition = transform.position; // Get the player's position
            distanceFromPlayer = Vector2.Distance(playerPosition, sentinel.transform.position);
            if (distanceFromPlayer > attackDistance)
            {
                Vector2 targetSentinelPosition = new Vector2(playerPosition.x + xOffset, yPosition); // Calculate the new position for the Sentinel with the specified offsets
                Vector2 smoothedPosition = Vector2.Lerp(sentinel.transform.position, targetSentinelPosition, Time.deltaTime * smoothSpeed); // Use Vector3.Lerp to smoothly interpolate between the current position and the target position
                sentinel.transform.position = smoothedPosition; // Set the position of the Sentinel
            }

        }
        else
        {
            Debug.LogError("Sentinel GameObject is not assigned. Please assign it in the inspector.");
        }
    }
}
