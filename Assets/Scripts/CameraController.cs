using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * � CameraController ��������������� ��� �� ��������� ��� ������ ������� 
 * �� �� ���� ��� ������. ������������ ������������ �� �,�,� ��� Player (Transform)
 * ��� ������� �� position ��� ������� �� ����
 */
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // ������� ��� Player ��� script ��� MainCamera
                                               // ��� ���������� �� Transform ���

    private void Start()
    {
        Debug.Log("Main Camera has been set!");
    }

    private void Update()
    {
        // � ���� ��� ������� ������� ������ �� �������� ��� ���������� ������� �� �� Transform ��� Player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
