using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int starting_health = 3;
    public int currentHealth { get; private set; }

    private bool dead;

    private Rigidbody2D player_body;
    private Animator death_animator;
    private BoxCollider2D player_collider;

    private void Awake()
    {
        currentHealth = starting_health;
        player_body = GetComponent<Rigidbody2D>();
        death_animator = GetComponent<Animator>();
        player_collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) 
        {
            Debug.Log("Player has been hit!");
            TakeDamage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Player has been hit!");
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage) 
    {
        Debug.Log("Player takes damage!");
        Debug.Log("Player current health: " + currentHealth);

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, starting_health);

        Debug.Log("Player health now: " + currentHealth);

        if (currentHealth > 0)
        {
            death_animator.SetTrigger("hurt");
        }
        else if (currentHealth == 0)
        {
            Die();
        }

    }

    private void Die() 
    {
        player_body.bodyType = RigidbodyType2D.Static;
        player_collider.enabled = false;
        death_animator.SetTrigger("death");
    }

    private void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
