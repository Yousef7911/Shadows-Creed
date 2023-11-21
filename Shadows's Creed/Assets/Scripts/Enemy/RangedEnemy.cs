using UnityEngine;
public class RangedEnemy : MonoBehaviour {
    private Animator animator;
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float Range;
    [SerializeField] private int Damage;
    [SerializeField] private float Distance;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] LayerMask PlayerLayer;
    private float CooldownTimer = Mathf.Infinity;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] Fireballs;
    private Health PlayerHealth;
    private EnemyPatrol patrol;
    [SerializeField] private AudioClip FireballSound;
    private void Awake() {
        animator = GetComponent<Animator>();
        patrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update() {
        CooldownTimer += Time.deltaTime;
        if (PlayerInSight() && PlayerHealth.CurrentHealth > 0) {
            if (CooldownTimer >= AttackCooldown) {
                CooldownTimer = 0;
                animator.SetTrigger("RangeAttack");
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
    private void RangedAttack() {
        SoundManager.instance.PlaySound(FireballSound);
        CooldownTimer = 0;
        Fireballs[FindFireball()].transform.position = FirePoint.position;
        Fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindFireball() {
        for (int i = 0; i < Fireballs.Length; i++) {
            if (!Fireballs[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }
}