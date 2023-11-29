using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * � PlayerAttack ��������������� ��� ��� ������� ��� ������
 * ��������������� �� �������� ���� ��� ���������, � ������� 
 * ������� �� �� ����� ���
 */
public class PlayerAttack : MonoBehaviour
{
    private Animator player_attack_animator;
    [SerializeField] private float attackCooldown; // ������ ��������� ��� ������ ��� �� ���������� ���������� ������
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        player_attack_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && // ���� � ������� ������� �������� ���� ��� ������� ���
            cooldownTimer > attackCooldown) // ��� � ������ ��������� ���� ������� 
        {
            Attack(); // ��������� �������
        }

        cooldownTimer += Time.deltaTime; // ������� ����� ���������
    }

    private void Attack() 
    {
        player_attack_animator.SetTrigger("attack"); // ��������� sword swing animation
        cooldownTimer = 0; // �������� �� ������� ������ ���������
    }
}
