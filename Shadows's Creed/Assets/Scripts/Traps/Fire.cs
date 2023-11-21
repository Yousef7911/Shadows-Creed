using System.Collections;
using UnityEngine;
public class Fire : MonoBehaviour {
    Animator animator;
    SpriteRenderer SpriteRenderer;
    [Header("Fire Traps Timer")]
    [SerializeField] private float ActivationDelay;
    [SerializeField] private float ActiveTime;
    [SerializeField] private float Damage;
    private bool Triggered;
    private bool Active;
    private bool Load;
    private Health Player;
    [SerializeField] private AudioClip FireSound;
    private void Awake() {
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (!Triggered) { 
                Load = true;
                StartCoroutine(ActivateFireTrap());
            }
            Player = collision.GetComponent<Health>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        Player = null;
    }
    private void Update() {
        if (Active && Player != null && Load) {
            Player.TakeDamage(Damage);
            Load = false;
        }
    }
    private IEnumerator ActivateFireTrap() {
        Triggered = true;
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(ActivationDelay);
        SpriteRenderer.color = Color.white;
        Active = true;
        SoundManager.instance.PlaySound(FireSound);
        animator.SetBool("Active", true);
        yield return new WaitForSeconds(ActiveTime);
        Active = false;
        Triggered = false;
        animator.SetBool("Active", false);
    }
}