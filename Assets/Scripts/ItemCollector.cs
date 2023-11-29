using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * H ItemCollector χρησιμοποιείται για να δείχνει στον παίχτη 
 * πόσα coins έχει συλλέξει κατά τη διάρκεια του παιχνιδιού.
 * Το κείμενο Text που φαίνεται πάνω αριστερά στην οθόνη είναι στον Canvas
 */
public class ItemCollector : MonoBehaviour
{
    private int coins = 0; // Με πόσα coins ξεκινάμε
    [SerializeField] private Text HowManyCoins; // Το κείμενο του Canvas

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin")) // Όταν γίνεται collision με το Coin object
        {
            Destroy(collision.gameObject); // αφαιρούμε το αντικείμενο από το παιχνίδι
            coins++; // το προσθέτουμε μαζί με όσα έχουμε συλλέξει
            HowManyCoins.text = "Coins: " + coins; // αλλάζουμε το κείμενο του Canvas σύμφωνα με όσα coins συλλέξαμε
        }
    }
}
