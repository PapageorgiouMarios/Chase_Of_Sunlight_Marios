using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    
    [SerializeField] private float duration = 20f;
    private float timer = 0f;

    private void Start()
    {
        Debug.Log(" ----------------------------- Hello from endscene");
        timer = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        
            // Increment the timer
            timer += Time.deltaTime;

            // If the timer exceeds the duration, switch back to the first scene
            if (timer >= duration)
            {
                SceneManager.LoadScene(0);
                timer = 0f;
            }
        
    }

}
