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

    private BoxCollider2D sentiel_collider;

    void Start()
    {
        sentielAnimator = GetComponent<Animator>();
        previousPosition = transform.position;
        
        sentiel_collider = GetComponent<BoxCollider2D>();
        if (sentiel_collider != null)
        {

            sentiel_collider.enabled = false; // forever false
            sentiel_collider.isTrigger = false;
            Debug.Log("sentiel_collider.enabled: " + sentiel_collider.enabled);
            Debug.Log("sentiel_collider.isTrigger: " + sentiel_collider.isTrigger);            
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
            bool isMovingRight = movementDirection.x > 0;

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
        Debug.Log("sentiel_collider.isTrigger: " + sentiel_collider.isTrigger);
        Debug.Log("manage the collisioooooooooooooooooooooooooooooooooooooooooooooooooooon");
    }

    private void activateAttack()
    {
        Debug.Log("attack is activated");
        sentiel_collider.enabled = true;
        sentiel_collider.isTrigger = true;
    }
    
    private void deactivateAttack()
    {
        Debug.Log("attack is turned off");
        sentiel_collider.enabled = false;
        sentiel_collider.isTrigger = false;
    }
}
