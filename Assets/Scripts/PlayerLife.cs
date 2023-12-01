using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int starting_health = 3;
    public int currentHealth { get; private set; }

    private Rigidbody2D player_body;
    private Animator death_animator;
    private BoxCollider2D player_collider;
    private SpriteRenderer player_sprite_rend;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private bool hurt = false;

    private void Awake()
    {
        currentHealth = starting_health;
        player_body = GetComponent<Rigidbody2D>();
        death_animator = GetComponent<Animator>();
        player_collider = GetComponent<BoxCollider2D>();
        player_sprite_rend = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && currentHealth > 0 && !hurt) 
        {
            Debug.Log("Player has been hit!");
            TakeDamage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && currentHealth > 0 && !hurt)
        {
            Debug.Log("Player has been hit!");
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage) 
    {
        Debug.Log("Player takes damage!");
        Debug.Log("Player current health: " + currentHealth);

        hurt = true;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, starting_health);

        Debug.Log("Player health now: " + currentHealth);

        if (currentHealth > 0)
        {
            death_animator.SetTrigger("hurt");
            StartCoroutine(iFramesActivation());
        }
        else if (currentHealth == 0)
        {
            Die();
        }

    }

    private IEnumerator iFramesActivation() 
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        for(int i = 0; i < numberOfFlashes; i++) 
        {
            player_sprite_rend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            player_sprite_rend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            hurt = false;
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);

    }

    public void AddHealth(int hp) 
    {
        currentHealth = Mathf.Clamp(currentHealth + hp, 0, starting_health);
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
