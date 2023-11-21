using UnityEngine;
public class Drop : MonoBehaviour {
    [SerializeField] private float ManaValue;
    [SerializeField] private AudioClip PickupSound;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            SoundManager.instance.PlaySound(PickupSound);
            collision.GetComponent<Mana>().AddMana(ManaValue);
            gameObject.SetActive(false);
        }
    }
}