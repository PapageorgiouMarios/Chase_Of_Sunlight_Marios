using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] private Transform previous;
    [SerializeField] private Transform next;
    [SerializeField] private BossRoomController cam;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bossName;
    [SerializeField] private GameObject bossHealth;

    private bool showAssets = false;

    private void Update()
    {
        if (showAssets) 
        {
            bossName.SetActive(true);
            bossHealth.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            if(collision.transform.position.x < transform.position.x) 
            {
                Debug.Log("Player entered boss fight");
                cam.MoveCamera(next);
                showAssets = true;
            }
            
            else if(collision.transform.position.x > block.transform.position.x) 
            {
                block.SetActive(true);
            }
        }
    }
}
