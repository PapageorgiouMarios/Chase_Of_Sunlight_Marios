using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    private Animator coin_animator;

    private void Start()
    {
        coin_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coin_animator.SetTrigger("collected");
            SoundManager.instance.PlaySound(collectSound);
        }
    }

    private void DestroyedWhenCollected() 
    {
        Destroy(gameObject);
    }
}
