using UnityEngine;

/*
 * � PendulumMovement ��������������� ��� �� ����� ������ ���� ������ ��������� (pendulum)
 * � ������ ���� ������� ������ �� ��������� ����� ��� ������������ ���� �� � ������� 
 * ��������� �� �������� �� ������ ��� ������ ���� ���� �� �������� �� �� �����������
 */
public class PendulumMovement : MonoBehaviour
{
    public Rigidbody2D pendulum_body;
    public float leftPushRange; // ���� ��� ��������
    public float rightPushRange; // ���� ��� �����
    public float velocityThreshold; // ����� ���������

    private void Start()
    {
        pendulum_body = GetComponent<Rigidbody2D>();
        pendulum_body.angularVelocity = velocityThreshold; // �������� ���� 160 ������
    }

    // Update is called once per frame
    private void Update()
    {
        Push(); // ���� ����� ��� ��������
    }
    private void Push() 
    {
        if (transform.rotation.z > 0
            && transform.rotation.z < rightPushRange
            && (pendulum_body.angularVelocity > 0)
            && pendulum_body.angularVelocity < velocityThreshold) 
        {
            pendulum_body.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0
            && transform.rotation.z > leftPushRange
            && (pendulum_body.angularVelocity < 0)
            && pendulum_body.angularVelocity > velocityThreshold * -1) 
        {
            pendulum_body.angularVelocity = velocityThreshold * -1;
        }
    }
}
