using UnityEngine;

public class SentinelFollowsPlayer : MonoBehaviour
{
    private const float yPosition = 2.15f; // specific y to avoid vertical movement
    private float attackDistance = 3f;
    public bool playerInBounds; // Boolean to check if the player is between the edges

    [Header("What speed the boss has?")]
    [SerializeField] private float smoothSpeed = 0.5f;

    [Header("Left Edge")]
    [SerializeField] Transform leftEdge;

    [Header("Right Edge")]
    [SerializeField] Transform rightEdge;

    [Header("What object the Sentinel follows?")]
    [SerializeField] PlayerLife player;

    [Header("What is the player's position")]
    [SerializeField] Transform playerTransform;

    private void Awake()
    {
        player = GetComponent<PlayerLife>(); // Get the player
    }


    private void Update()
    {
        if (gameObject != null) // If the Sentinel object exists
        {
            float distanceFromPlayer;

            Vector2 playerPosition = playerTransform.position; // Get the player's position
            distanceFromPlayer = Vector2.Distance(gameObject.transform.position, playerPosition);

            // Keep track if the player is in bounds
            playerInBounds = playerPosition.x > leftEdge.position.x && playerPosition.x < rightEdge.position.x;

            if (playerInBounds && distanceFromPlayer > attackDistance)
            {
                FollowPlayer(playerPosition);
            }

        }
        else
        {
            Debug.LogError("Sentinel GameObject is not assigned. Please assign it in the inspector.");
        }
    }

    private void FollowPlayer(Vector2 playerPosition) 
    {
        Vector2 targetSentinelPosition = new Vector2(playerPosition.x, yPosition); // Calculate the new position for the Sentinel with the specified offsets
        Vector2 smoothedPosition = Vector2.Lerp(gameObject.transform.position, targetSentinelPosition, Time.deltaTime * smoothSpeed); // Use Vector3.Lerp to smoothly interpolate between the current position and the target position
        gameObject.transform.position = smoothedPosition; // Set the position of the Sentinel
    }
}
