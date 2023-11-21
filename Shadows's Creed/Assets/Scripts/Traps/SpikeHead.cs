using UnityEngine;
public class SpikeHead : MonoBehaviour {
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float Range;
    [SerializeField] private float Delay;
    [SerializeField] private LayerMask Player;
    private float Timer;
    private Vector3 Destination;
    private Vector3[] Directions = new Vector3[4];
    private bool Attack;
    [SerializeField] private AudioClip ImpactSound;
    private void OnEnable() {
        Stop();
    }
    private void Update() {
        if (Attack) {
            transform.Translate(Destination * Speed * Time.deltaTime);
        }
        else {
            Timer += Time.deltaTime;
            if (Timer > Delay) {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer() {
        CalculateDirections();
        for (int i = 0; i < Directions.Length; i++) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Directions[i], Range, Player);
            if (hit.collider != null && !Attack) { 
                Attack = true;
                Destination = Directions[i];
                Timer = 0;
            }
        }
    }
    private void CalculateDirections() {
        Directions[0] = transform.right * Range;
        Directions[1] = - transform.right * Range;
        Directions[2] = transform.up * Range;
        Directions[3] = - transform.up * Range;
    }
    private void Stop() {
        Destination = transform.position;
        Attack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        SoundManager.instance.PlaySound(ImpactSound);
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
        Stop();
    }
}