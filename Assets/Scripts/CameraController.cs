using UnityEngine;

/*
 * CameraController: Script to object "Main Camera"
 * 
 * Parameters: "Player" Object's Transform (X,Y,Z)
 * 
 * Usage: Updates the camera's position according to the player's position
 *        The player's position is specified from the object "Player" 
 */

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // We give as a parameter the player's X,Y,Z
                                            
    //At the beginning of the game
    private void Start()
    {
        // Console message frequency: Only once at the beginning
        Debug.Log("Main Camera has been set!");
    }

    private void Update()
    {
        // The camera follows the player's position while their current health remains positive
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
