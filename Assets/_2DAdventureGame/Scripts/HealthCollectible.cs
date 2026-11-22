using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int amount = 1;

    public AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller =
        other.GetComponent<PlayerController>();
        if (controller != null
            && controller.health < controller.maxHealth)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            controller.ChangeHealth(amount);
            Destroy(gameObject);
        }
    }
}
