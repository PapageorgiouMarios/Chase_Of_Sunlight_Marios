using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Η CameraController χρησιμοποιείται για να τοποθετεί την κάμερα σύμφωνα 
 * με τη θέση του παίχτη. Συγκεκριμένα χρησιμοποιέι το Χ,Υ,Ζ του Player (Transform)
 * και αλλάζει το position του σύμφωνα με αυτό
 */
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // Βάζουμε τον Player στο script της MainCamera
                                               // και λαμβάνουμε το Transform του

    private void Start()
    {
        Debug.Log("Main Camera has been set!");
    }

    private void Update()
    {
        // Η θέση της κάμερας αλλάζει καθόλη τη διάρκεια του παιχνιδιού σύμφωνα με το Transform του Player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
