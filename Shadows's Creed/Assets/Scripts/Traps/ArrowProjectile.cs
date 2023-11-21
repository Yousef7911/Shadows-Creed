using UnityEngine;
public class ArrowProjectile : MonoBehaviour {
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float ResetTime;
    private float LifeTime;
    public void Activate() {
        LifeTime = 0;
        gameObject.SetActive(true);
    }
    private void Update() {
        float MovementSpeed = Speed * Time.deltaTime;
        transform.Translate(MovementSpeed, 0, 0);
        LifeTime += Time.deltaTime;
        if (LifeTime > ResetTime) {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
        gameObject.SetActive(false);
    }
}