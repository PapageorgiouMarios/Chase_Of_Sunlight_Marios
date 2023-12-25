using UnityEngine;
using UnityEngine.UI;

public class SentinelHealthBar : MonoBehaviour
{
    public Image[] heart_images = new Image[5];

    private int how_many_hearts;

    [SerializeField] SentinelLife boss;

    private void Start()
    {
        for (int i = 0; i < boss.health; i++)
        {
            heart_images[i] = transform.GetChild(i).GetComponent<Image>();
        }

        how_many_hearts = boss.health;

        HideAllImages();
    }

    private void Update()
    {
        how_many_hearts = boss.health;
        UpdateHearts();
    }

    private void HideAllImages()
    {
        foreach (Image img in heart_images)
        {
            img.enabled = false;
        }
    }

    private void ShowImage(int imageToShow)
    {
        HideAllImages();

        if (imageToShow >= 0 && imageToShow < heart_images.Length)
        {
            heart_images[imageToShow].enabled = true;
        }
        else
        {
            Debug.Log("Invalid index! Current health: " + imageToShow);
        }
    }

    private void UpdateHearts()
    {

        if (how_many_hearts == 5)
        {
            ShowImage(0);
        }
        else if (how_many_hearts == 4)
        {
            ShowImage(1);
        }
        else if (how_many_hearts == 3)
        {
            ShowImage(2);
        }
        else if (how_many_hearts == 2)
        {
            ShowImage(3);
        }
        else if (how_many_hearts == 1)
        {
            ShowImage(4);
        }
        else
        {
            HideAllImages();
        }
    }
}
