using UnityEngine;
public class Heart : MonoBehaviour {
    [SerializeField] private float HealthValue;
    [SerializeField] private AudioClip PickupSound;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            SoundManager.instance.PlaySound(PickupSound);
            collision.GetComponent<Health>().AddHealth(HealthValue);
            gameObject.SetActive(false);
        }
    }
}