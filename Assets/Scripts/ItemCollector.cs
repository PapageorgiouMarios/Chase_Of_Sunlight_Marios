using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * H ItemCollector ��������������� ��� �� ������� ���� ������ 
 * ���� coins ���� �������� ���� �� �������� ��� ����������.
 * �� ������� Text ��� �������� ���� �������� ���� ����� ����� ���� Canvas
 */
public class ItemCollector : MonoBehaviour
{
    private int coins = 0; // �� ���� coins ��������
    [SerializeField] private Text HowManyCoins; // �� ������� ��� Canvas

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin")) // ���� ������� collision �� �� Coin object
        {
            Destroy(collision.gameObject); // ��������� �� ����������� ��� �� ��������
            coins++; // �� ����������� ���� �� ��� ������ ��������
            HowManyCoins.text = "Coins: " + coins; // ��������� �� ������� ��� Canvas ������� �� ��� coins ���������
        }
    }
}
