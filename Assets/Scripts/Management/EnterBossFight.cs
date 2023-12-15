using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterBossFight : MonoBehaviour
{
    [SerializeField] Image darken_screen;
    public float transitionSpeed = 30f;
    public bool transitioning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !transitioning)
        {
            transitioning = true;

            Animator player_animator = collision.gameObject.GetComponent<Animator>();
            PlayerMovement player_movement = collision.gameObject.GetComponent<PlayerMovement>();
            Rigidbody2D player_body = collision.gameObject.GetComponent<Rigidbody2D>();
            
            player_animator.SetInteger("state", 0);
            player_body.constraints = RigidbodyConstraints2D.FreezePositionX;
            player_movement.enabled = false;

            StartCoroutine(DarkenScreen());
        }
    }

    IEnumerator DarkenScreen() 
    {
        while(darken_screen.color.a < 1.0f) 
        {
            Color newcolor = darken_screen.color;
            newcolor.a += Time.deltaTime * transitionSpeed;
            darken_screen.color = newcolor;
            yield return null;
        }
        BossRoom();
    }

    private void BossRoom() 
    {
        SceneManager.LoadScene(2);
    }
}
