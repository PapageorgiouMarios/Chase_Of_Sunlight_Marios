using System.Collections;
using UnityEngine;

/*
 *  EnemyLife is responsible for every enemy's health.
 *  Each enemy is given health points (hp). When they reach 0, they are eliminated
 */
public class EnemyLife : MonoBehaviour
{
    [Header("How much health the enemy has?")]
    [SerializeField] public int health;

    [Header("How long the enemy is invinsible after hit?")]
    [SerializeField] private float iFramesDuration;

    [Header("How many times the enemy flashes after hit")]
    [SerializeField] private int numberOfFlashes;

    [Header("What other behaviors the enemy has?")]
    [SerializeField] private Behaviour[] components;

    public int remainingHealth { get; private set; } // How much health the enemy has now?
    private BoxCollider2D enemy_collider;
    private Animator enemy_animator; 
    private SpriteRenderer enemy_sprite_rend;

    EnemyPatrol patrol;

    private bool isDead = false;
    public bool frames_activated = false;

    private void Awake()
    {
        remainingHealth = health;
        patrol = GetComponentInParent<EnemyPatrol>();
        enemy_animator = GetComponent<Animator>();
        enemy_collider = GetComponent<BoxCollider2D>();
        enemy_sprite_rend = GetComponent<SpriteRenderer>();
    }

    // Event used after the enemy completes death animation
    private void DeleteEnemy()
    {
        gameObject.SetActive(false);
        GameManager.instance.AddDefeatedEnemies();
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage; // usually damage = 1 from player's hit
        remainingHealth = Mathf.Clamp(remainingHealth - damage, 0, health);

        if (remainingHealth > 0) // if the enemy still has hp
        {
            enemy_animator.SetTrigger("hurt");
            StartCoroutine(iFramesActivation());
        }
        else if (remainingHealth == 0) // if it is time for the enemy to be eliminated
        {
            Debug.Log("Enemy defeated!");
            Dead();
        }
    }

    // Method used to give to enemy their IFrames (Invisibility Frames)
    private IEnumerator iFramesActivation()
    {
        frames_activated = true;
        float default_speed = patrol.speed;
        patrol.speed += 2f;

        Physics2D.IgnoreLayerCollision(7, 8, true); // ignore collision with "Player" or "Enemy"
        for (int i = 0; i < numberOfFlashes; i++)
        {
            enemy_sprite_rend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            enemy_sprite_rend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        patrol.speed = default_speed;
        frames_activated = false;
    }

    private void Dead()
    {
        if (!isDead)
        {
            isDead = true;
            enemy_collider.enabled = false;

            foreach (Behaviour component in components)
            {
                component.enabled = false; // when the enemy is eliminated all their components are gone
            }
            enemy_animator.SetTrigger("die");
        }
    }
}
