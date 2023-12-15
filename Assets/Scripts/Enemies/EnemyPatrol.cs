using UnityEngine;

/*
 * EnemyPatrol is used from Melee Enemies. There are two waypoints on opposite sides
 * the enemy walks between these two waypoints
 */

public class EnemyPatrol : MonoBehaviour
{
    [Header("Left edge")]
    [SerializeField] public Transform leftEdge;

    [Header("Right edge")]
    [SerializeField] public Transform rightEdge;

    [Header("Whose position it takes?")]
    [SerializeField] private Transform enemy;

    [Header("How much fast the enemy patrols?")]
    [SerializeField] public float speed;

    [Header("How long the enemy stays put when they reach waypoint?")]
    [SerializeField] private float standing_duration;

    private float standing_timer; // time counter 

    private Vector3 scale;
    private bool left;
    private Animator enemy_animator;

    private void Awake()
    {
        enemy_animator = GetComponent<Animator>();
        scale = enemy.localScale;
    }

    private void OnDisable()
    {
        enemy_animator.SetBool("moving", false);
    }

    private void Update()
    {
        if(left) 
        {
            if(enemy.position.x >= leftEdge.position.x) 
            {
                MoveToDirection(-1);
            }
            else 
            {
                ChangeDirection();
            }
        }
        else 
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveToDirection(1);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection() 
    {
        enemy_animator.SetBool("moving", false);
        standing_timer += Time.deltaTime;

        if (standing_timer > standing_duration)
        {
            left = !left;
        }
    }

    private void MoveToDirection(int direction) 
    {
        standing_timer = 0;

        enemy_animator.SetBool("moving",  true);

        enemy.localScale = new Vector3(Mathf.Abs(scale.x) * direction, scale.y, scale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
                                     enemy.position.y, enemy.position.z);
    }

}
