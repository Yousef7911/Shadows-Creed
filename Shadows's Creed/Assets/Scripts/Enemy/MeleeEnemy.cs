using UnityEngine;
public class MeleeEnemy : MonoBehaviour {
    private Animator animator;
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float Range;
    [SerializeField] private int Damage;
    [SerializeField] private float Distance;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] LayerMask PlayerLayer;
    private float CooldownTimer = Mathf.Infinity;
    private Health PlayerHealth;
    private EnemyPatrol patrol;
    [SerializeField] private AudioClip SwordSound;
    private void Awake() {
        animator = GetComponent<Animator>();
        patrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update() {
        CooldownTimer += Time.deltaTime;
        if (PlayerInSight() && PlayerHealth.CurrentHealth > 0) {
            if (CooldownTimer >= AttackCooldown) {
                CooldownTimer = 0;
                animator.SetTrigger("MeleeAttack");
                SoundManager.instance.PlaySound(SwordSound);
            }
        }
        if (patrol != null) {
            patrol.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + (transform.right * Range) * transform.localScale.x * Distance,
                           new Vector3(boxCollider2D.bounds.size.x * Range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z), 
                           0, Vector2.left, 0, PlayerLayer);
        if (hit.collider != null) {
            PlayerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + (transform.right * Range) * transform.localScale.x * Distance,
                            new Vector3(boxCollider2D.bounds.size.x * Range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
    private void DamagePlayer() {
        if (PlayerInSight()) {
            PlayerHealth.TakeDamage(Damage);
        }
    }
}