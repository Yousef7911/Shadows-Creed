using UnityEngine;
public class Spikes : MonoBehaviour {
    [SerializeField] private float Damage;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}