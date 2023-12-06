using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    public int remainingHealth { get; private set; }
    private BoxCollider2D enemy_collider;
    private Animator enemy_animator;
    private SpriteRenderer enemy_sprite_rend;

    EnemyPatrol patrol;

    [SerializeField] private Behaviour[] components;

    private bool isDead = false;
    public bool frames_activated = false;

    private void Awake()
    {
        remainingHealth = health;
        Debug.Log("Enemy is alive!");
        patrol = GetComponentInParent<EnemyPatrol>();
        enemy_animator = GetComponent<Animator>();
        enemy_collider = GetComponent<BoxCollider2D>();
        enemy_sprite_rend = GetComponent<SpriteRenderer>();
    }

    private void DeleteEnemy()
    {
        gameObject.SetActive(false);
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage!");
        Debug.Log("Enemy has " + health + " hp left!");

        remainingHealth = Mathf.Clamp(remainingHealth - damage, 0, health);

        if (remainingHealth > 0)
        {
            enemy_animator.SetTrigger("hurt");
            StartCoroutine(iFramesActivation());
        }
        else if (remainingHealth == 0)
        {
            Debug.Log("Enemy defeated!");
            Dead();
        }
    }

    private IEnumerator iFramesActivation()
    {
        frames_activated = true;
        float default_speed = patrol.speed;
        patrol.speed += 2f;

        Physics2D.IgnoreLayerCollision(7, 8, true);
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
                component.enabled = false;
            }
            enemy_animator.SetTrigger("die");
        }
    }
}
