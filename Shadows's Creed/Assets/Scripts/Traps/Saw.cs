using UnityEngine;
public class Saw : MonoBehaviour {
    [SerializeField] private float Move;
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    private bool MovingLeft;
    private float LeftEdge;
    private float RightEdge;
    private void Awake() {
        LeftEdge = transform.position.x - Move;
        RightEdge = transform.position.x + Move;
    }
    private void Update() {
        if (MovingLeft) {
            if (transform.position.x > LeftEdge) {
                transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else {
                MovingLeft = false;
            }
        }
        else {
            if (transform.position.x < RightEdge) {
                transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else {
                MovingLeft = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}