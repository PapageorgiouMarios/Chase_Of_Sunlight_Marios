using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float cooldown;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask player;

    private float cooldownTimer = Mathf.Infinity;
    private Animator serpent_warrior_anim;

    private PlayerLife player_health;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        serpent_warrior_anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }


    private void Update()
    {
        int randomAttack = Random.Range(0, 100);

        cooldownTimer += Time.deltaTime;

        if (Player_Spotted())
        {
            Debug.Log("Serpent Warrior detected Player");
            if (cooldownTimer >= cooldown) 
            {
                Debug.Log("Serpent Warrior attacks");
                cooldownTimer = 0;

                if(randomAttack < 70) 
                {
                    serpent_warrior_anim.SetTrigger("attack_2");
                }
                else 
                {
                    serpent_warrior_anim.SetTrigger("attack_1");
                }
            }
        }

        if (enemyPatrol != null) 
        {
            enemyPatrol.enabled = !Player_Spotted();
        }
    }

    private bool Player_Spotted() 
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                                             Vector2.left, 0, player);
        if(hit.collider != null) 
        {
            player_health = hit.transform.GetComponent<PlayerLife>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance 
            , new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer() 
    {
        if (Player_Spotted()) 
        {
            if (!player_health.frames_activated) 
            {
                player_health.TakeDamage(damage);
            }
        }
    }
}
