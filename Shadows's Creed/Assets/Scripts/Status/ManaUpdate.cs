using UnityEngine;
public class ManaUpdate : MonoBehaviour {
    [SerializeField] public float UpdateValue;
    [SerializeField] private AudioClip PickupSound;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            SoundManager.instance.PlaySound(PickupSound);
            collision.GetComponent<Mana>().UpdateMana(UpdateValue);
            /*Manabar manabar = FindObjectOfType<Manabar>();
            if (manabar != null) {
                manabar.CurrentManabar.maxValue += UpdateValue;
                manabar.Update();
            }*/
            gameObject.SetActive(false);
        }
    }
}