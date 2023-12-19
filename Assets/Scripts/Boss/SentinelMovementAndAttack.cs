using System.Collections;
using UnityEngine;

public class SentinelMovementAndAttack : MonoBehaviour
{
    private Animator sentinelAnimator; // Animator of Sentinel
    private Vector2 previousPosition; // Previous position
    private SpriteRenderer spriteRenderer; // Sprite Renderer
    private float nextAttackTime; // Attack cooldown
    private BoxCollider2D[] sword_colliders; // Attack colliders of Sentinel
    private bool isMovingRight; // Keep track the sentinel's scale

    [Header("What range the sword swing has?")]
    [SerializeField] public float attackRate; // attackRate in seconds

    [Header("What other role the Sentinel has?")]
    [SerializeField] SentinelFollowsPlayer following;

    private void Awake()
    {
        sentinelAnimator = GetComponent<Animator>(); // Get Animator
        previousPosition = transform.position; // Get previous position
        following = GetComponent<SentinelFollowsPlayer>(); // Make sure the Sentinel knows to follow player
        sword_colliders = GetComponents<BoxCollider2D>(); // Attack colliders

        if (sword_colliders != null) // If the sentinel has the two spaces to attack the player
        {
            sword_colliders[0].isTrigger = false;
            sword_colliders[1].isTrigger = false;
            
            sword_colliders[0].enabled = false;
            sword_colliders[1].enabled = false;
   
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the Sentinel GameObject.");
        }
    }

    private void Update()
    {
        if (following.playerInBounds) 
        {
            ChangeDirection();
        }
        else
        {
            Debug.Log("Stop walking!");
            sentinelAnimator.SetBool("walking", false);
        }
        AttackPlayer();
    }

    IEnumerator TriggerAttackAnimation(float duration) // Coroutine to handle the attack animation
    {
        sentinelAnimator.SetBool("attack", true);    // Set the attack animation state
        yield return new WaitForSeconds(duration);  // Wait for the specified duration
        sentinelAnimator.SetBool("attack", false);   // Return to the original animation state
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player got hit!!!!");
        PlayerLife.instance.TakeDamage(1);
    }

    // Event used in attack animation
    private void activateAttack()
    {
        if (isMovingRight)
        {
            sword_colliders[0].enabled = true;
            sword_colliders[0].isTrigger = true;
            Debug.Log("attack is activated at right");
        }
        else
        {
            sword_colliders[1].enabled = true;
            sword_colliders[1].isTrigger = true;
            Debug.Log("attack is activated at left");
        }
    }

    // Event used in attack animation
    private void deactivateAttack()
    {
        Debug.Log("attack is turned off");
        sword_colliders[0].isTrigger = false;
        sword_colliders[1].isTrigger = false;
        sword_colliders[0].enabled = false;
        sword_colliders[1].enabled = false;
    }

    private void ChangeDirection() 
    {
        // Calculate the movement direction based on the change in position
        Vector2 movementDirection = ((Vector2)transform.position - previousPosition).normalized;

        // Check if the Sentinel is moving
        if (movementDirection.magnitude > 0.01f && !sentinelAnimator.GetBool("attack"))
        {
            // Determine the direction (left or right) based on the x component of the movement direction
            isMovingRight = movementDirection.x > 0;

            // Flip the sprite based on the movement direction
            spriteRenderer.flipX = !isMovingRight;

            sentinelAnimator.SetBool("walking", true);
        }
        else
        {
            sentinelAnimator.SetBool("walking", false);
        }
        previousPosition = transform.position;
    }

    private void AttackPlayer() 
    {
        // ATTACK TASK
        // if the distance between sentiel and player is in x then the boss can attack automatically(every y seconds)
        bool walking = sentinelAnimator.GetBool("walking");

        if (!walking && nextAttackTime < Time.time && following.playerInBounds) // change animator and attack (for a little seconds)
        {
            StartCoroutine(TriggerAttackAnimation(1f));
            nextAttackTime = Time.time + attackRate;
            sentinelAnimator.SetBool("walking", false);
        }
    }
}
