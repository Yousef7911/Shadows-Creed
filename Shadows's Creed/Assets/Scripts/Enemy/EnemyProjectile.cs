using UnityEngine;
public class EnemyProjectile : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator animator;
    private BoxCollider2D collider;
    private bool hit;
    private void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile() {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        collider.enabled = true;
    }
    private void Update() {
        if (hit) return;
        float Speed = speed * Time.deltaTime;
        transform.Translate(Speed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime > resetTime) {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        hit = true;
        collider.enabled = false;
        animator.SetTrigger("Explde");
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(20);
        }
    }
    private void Deactivate() {
        gameObject.SetActive(false);
    }
}