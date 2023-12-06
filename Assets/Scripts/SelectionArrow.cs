using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int current_position;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            ChangePosition(-1);
        }
        
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            ChangePosition(1);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) 
        {
            Interact();
        }
    }

    private void ChangePosition(int change) 
    {
        current_position += change;

        if(current_position < 0) 
        {
            current_position = options.Length - 1;
        }
        else if (current_position > options.Length - 1) 
        {
            current_position = 0;
        }

        rect.position = new Vector3(rect.position.x, options[current_position].position.y, 0);
    }

    private void Interact() 
    {
        options[current_position].GetComponent<Button>().onClick.Invoke();
    }
}
