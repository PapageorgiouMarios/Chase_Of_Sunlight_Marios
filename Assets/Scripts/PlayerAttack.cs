using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Η PlayerAttack χρησιμοποιείται για την επίθεση του παίχτη
 * χρησιμοποιώντας το αριστερό κλικ του ποντικιού, ο παίχτης 
 * χτυπάει με το σπαθί του
 */
public class PlayerAttack : MonoBehaviour
{
    private Animator player_attack_animator;
    [SerializeField] private float attackCooldown; // χρόνος χαλάρωσης του παίχτη για να αποφύγουμε συνεχόμενη κίνηση
    private float cooldownTimer = Mathf.Infinity;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whoIsEnemy;
    public int damage;

    private void Awake()
    {
        player_attack_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whoIsEnemy);

        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        {
            Attack();

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i] != null)
                {
                    EnemyLife enemyLife = enemiesToDamage[i].GetComponent<EnemyLife>();

                    if (enemyLife != null)
                    {
                        enemyLife.ReceiveDamage(damage);
                    }
                    else
                    {
                        Debug.LogWarning("EnemyLife component not found on enemy!");
                    }
                }
                else
                {
                    Debug.LogWarning("Enemy reference is null!");
                }
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        player_attack_animator.SetTrigger("attack"); // εκτελούμε sword swing animation
        cooldownTimer = 0; // ξεκινάμε τη μέτρηση χρόνου χαλάρωσης
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }
}
