using UnityEngine;

/*
 * Η PendulumMovement χρησιμοποιείται για να δώσει κίνηση στην παγίδα εκκρεμούς (pendulum)
 * Η παγίδα αυτή εκτελεί κίνηση σε οριζόντιο άξονα και προσπερνάται μόνο αν ο παίχτης 
 * περιμένει το εκκρεμές να ανέβει στο τέτοιο ύψος όπου θα προλάβει να το προσπεράσει
 */
public class PendulumMovement : MonoBehaviour
{
    public Rigidbody2D pendulum_body;
    public float leftPushRange; // όριο από αριστερά
    public float rightPushRange; // όριο από δεξιά
    public float velocityThreshold; // γωνία εκκρεμούς

    private void Start()
    {
        pendulum_body = GetComponent<Rigidbody2D>();
        pendulum_body.angularVelocity = velocityThreshold; // ορισμένο στις 160 μοίρες
    }

    // Update is called once per frame
    private void Update()
    {
        Push(); // δώσε ώθηση στο εκκρεμές
    }
    private void Push() 
    {
        if (transform.rotation.z > 0
            && transform.rotation.z < rightPushRange
            && (pendulum_body.angularVelocity > 0)
            && pendulum_body.angularVelocity < velocityThreshold) 
        {
            pendulum_body.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0
            && transform.rotation.z > leftPushRange
            && (pendulum_body.angularVelocity < 0)
            && pendulum_body.angularVelocity > velocityThreshold * -1) 
        {
            pendulum_body.angularVelocity = velocityThreshold * -1;
        }
    }
}
