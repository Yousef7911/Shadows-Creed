using UnityEngine;
public class Projectile : MonoBehaviour {
    private Animator animator;
    [SerializeField] private Transform playerTransform;
    private new BoxCollider2D collider;
    [SerializeField] private float speed;
    [SerializeField] private float ResetTime;
    private bool hit;
    private float LifeTime;
    private void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile() {
        hit = false;
        LifeTime = 0;
        gameObject.SetActive(true);
        collider.enabled = true;
        transform.rotation = playerTransform.rotation;
    }
    private void Update() {
        if (hit) return;
        float Speed = speed * Time.deltaTime;
        transform.Translate(Speed, 0, 0);
        LifeTime += Time.deltaTime;
        if (LifeTime > ResetTime) {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        hit = true;
        collider.enabled = false;
        animator.SetTrigger("Explde");
        if (collision.tag == "Enemy") {
            collision.GetComponent<Health>().TakeDamage(25f);
        }
    }
    private void Deactivate() {
        gameObject.SetActive(false);
    }
}