using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] private Transform previous;
    [SerializeField] private Transform next;
    [SerializeField] private BossRoomController cam;
    [SerializeField] private GameObject block;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            if(collision.transform.position.x < transform.position.x) 
            {
                Debug.Log("Player entered boss fight");
                cam.MoveCamera(next);
            }
            
            else if(collision.transform.position.x > block.transform.position.x) 
            {
                block.SetActive(true);
            }
        }
    }
}
