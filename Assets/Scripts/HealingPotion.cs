using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int health_value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PlayerLife>().currentHealth < 3)
        {
            collision.GetComponent<PlayerLife>().AddHealth(health_value);
            gameObject.SetActive(false);
        }
        else if (collision.tag != "Player" || (collision.tag == "Player" && 
            (collision.GetComponent<PlayerLife>().currentHealth == 3)))
        {
            DeactivateAllColliders();
        }
    }

    private void DeactivateAllColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
