using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentielMovementAndAttack : MonoBehaviour
{

    private Animator sentielAnimator;
    private Vector2 previousPosition;
    private SpriteRenderer spriteRenderer;

    public float attackingRange;
    public GameObject sword;
    public GameObject swordParent;

    public float attackRate = 3f; // attackRate in seconds
    private float nextAttackTime;

    private BoxCollider2D[] sentiel_collider;
    private bool isMovingRight;

    void Start()
    {
        sentielAnimator = GetComponent<Animator>();
        previousPosition = transform.position;

        sentiel_collider = GetComponents<BoxCollider2D>();

        // sentiel_collider2 = transform.GetChild(0).GetComponent<BoxCollider2D>(); // Assuming the second collider is a child
        if (sentiel_collider != null)
        {
            Debug.Log("SIZE TOU COLLIDER: " + sentiel_collider.Length);
            Debug.Log("SIZE TOU COLLIDER: " + sentiel_collider[0].size); // 0 = right collider
            Debug.Log("SIZE TOU COLLIDER: " + sentiel_collider[1].size); // 1 = left collider
            
            Debug.Log("isTrigger col[0]: " + sentiel_collider[0].isTrigger);
            Debug.Log("isTrigger col[1]: " + sentiel_collider[1].isTrigger);
            
            Debug.Log("enabled col[0]: " + sentiel_collider[0].enabled);
            Debug.Log("enabled col[1]: " + sentiel_collider[1].enabled);

            sentiel_collider[0].isTrigger = false;
            sentiel_collider[1].isTrigger = false;
            
            sentiel_collider[0].enabled = false;
            sentiel_collider[1].enabled = false;

            
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the Sentinel GameObject.");
        }
    }

    void Update()
    {
        // Calculate the movement direction based on the change in position
        Vector2 movementDirection = ((Vector2)transform.position - previousPosition).normalized;

        // Check if the Sentinel is moving
        if (movementDirection.magnitude > 0.01f && !sentielAnimator.GetBool("attack"))
        {
            // Determine the direction (left or right) based on the x component of the movement direction
            isMovingRight = movementDirection.x > 0;

            // Flip the sprite based on the movement direction
            spriteRenderer.flipX = !isMovingRight;
            sentielAnimator.SetBool("walking", true);
            // sentielAnimator.SetBool("attack", false);
            //Debug.Log("moving");
        }
        else
        {
            sentielAnimator.SetBool("walking", false);
            //Debug.Log("idle");
        }

        // Update the previous position for the next frame
        previousPosition = transform.position;









        // ATTACK TASK

        // @TODO
        // if the destance between sentiel and player is in x then the boss can attack automatically(every y seconds)
        bool walking = sentielAnimator.GetBool("walking");
        if (!walking && nextAttackTime < Time.time) // change animator and attack (for a little seconds)
        {
            //bool walking = sentielAnimator.GetBool("walking");
            //if (!walking) // if sentiel is not walking then it can attack
            //{
            StartCoroutine(TriggerAttackAnimation(1f));
            nextAttackTime = Time.time + attackRate;
            sentielAnimator.SetBool("walking", false);
            //}
        }

    }




    IEnumerator TriggerAttackAnimation(float duration) // Coroutine to handle the attack animation
    {
        sentielAnimator.SetBool("attack", true);    // Set the attack animation state
        yield return new WaitForSeconds(duration);  // Wait for the specified duration
        sentielAnimator.SetBool("attack", false);   // Return to the original animation state
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        // manage the collision
        Debug.Log("manage the collisioooooooooooooooooooooooooooooooooooooooooooooooooooon1");
    }

    private void activateAttack()
    {
        if (isMovingRight)
        {
            sentiel_collider[0].enabled = true;
            sentiel_collider[0].isTrigger = true;
            Debug.Log("attack is activated at right");
        }
        else
        {
            sentiel_collider[1].enabled = true;
            sentiel_collider[1].isTrigger = true;
            Debug.Log("attack is activated at left");
        }
    }
    
    private void deactivateAttack()
    {
        Debug.Log("attack is turned off");
        sentiel_collider[0].isTrigger = false;
        sentiel_collider[1].isTrigger = false;
        sentiel_collider[0].enabled = false;
        sentiel_collider[1].enabled = false;
    }
}
