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

    private void Awake()
    {
        player_attack_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && // όταν ο παίχτης πατήσει αριστερό κλικ στο ποντίκι του
            cooldownTimer > attackCooldown) // και ο χρόνος χαλάρωσης έχει περάσει 
        {
            Attack(); // εκτελούμε επίθεση
        }

        cooldownTimer += Time.deltaTime; // δίνουμε χρόνο χαλάρωσης
    }

    private void Attack() 
    {
        player_attack_animator.SetTrigger("attack"); // εκτελούμε sword swing animation
        cooldownTimer = 0; // ξεκινάμε τη μέτρηση χρόνου χαλάρωσης
    }
}
