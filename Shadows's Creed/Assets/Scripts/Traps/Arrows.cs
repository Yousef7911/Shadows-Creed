using UnityEngine;
public class Arrows : MonoBehaviour {
    [SerializeField] private float AttackCooldown;
    [SerializeField] Transform FirePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldown;
    [SerializeField] private AudioClip ArrowSound;
    private void Attack() {
        cooldown = 0;
        SoundManager.instance.PlaySound(ArrowSound);
        arrows[FindArrow()].transform.position = FirePoint.position;
        arrows[FindArrow()].GetComponent<ArrowProjectile>().Activate();
    }
    private int FindArrow() {
        for (int i = 0; i < arrows.Length; i++) {
            if (!arrows[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }
    private void Update() {
        cooldown += Time.deltaTime;
        if (cooldown >= AttackCooldown) {
            Attack();
        }
    }
}