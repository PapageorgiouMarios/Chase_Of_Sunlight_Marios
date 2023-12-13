using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitConfirmation : MonoBehaviour
{
    [SerializeField] private GameObject confirmationPanel;

    void Awake()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
            Debug.Log("--------------------------------------------------------- Start is not null  ========> YES");
        }
        else
        {
            Debug.Log("--------------------------------------------------------- Start not null");
        }
        
    }

    void Update()
    {
        
    }

    public void ShowExitConfirmation()
    {
        // Show the confirmation panel
        confirmationPanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void CancelExit()
    {
        confirmationPanel.SetActive(false);
        if (confirmationPanel != null)
        {
            Debug.Log("--------------------------------------------------------- CancelExit is not null  ========> YES");
            //confirmationPanel.SetActive(false);
        }
        else
        {
            Debug.Log("--------------------------------------------------------- CancelExit not null");
        }
        // 
    }
}
