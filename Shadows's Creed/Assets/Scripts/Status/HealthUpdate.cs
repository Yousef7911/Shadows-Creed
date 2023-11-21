using UnityEngine;
public class HealthUpdate : MonoBehaviour {
    [SerializeField] public float UpdateValue;
    [SerializeField] private AudioClip PickupSound;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            SoundManager.instance.PlaySound(PickupSound);
            collision.GetComponent<Health>().UpdateHealth(UpdateValue);
            /*Healthbar healthbar = FindObjectOfType<Healthbar>();
            if (healthbar != null) {
                healthbar.CurrentHealthbar.maxValue += UpdateValue;
                healthbar.Update();
            }*/
            gameObject.SetActive(false);
        }
    }
}