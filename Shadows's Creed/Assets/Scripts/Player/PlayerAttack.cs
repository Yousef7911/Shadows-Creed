using UnityEngine;
public class PlayerAttack : MonoBehaviour {
    private Animator animator;
    private Mana Mana;
    private PlayerMovement PlayerMovement;
    [SerializeField] private float SwordAttackCooldown;
    [SerializeField] private float FireballAttackCooldown;
    [SerializeField] LayerMask EnemyLayer;
    private Health EnemyHealth;
    [SerializeField] private float Range;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float Distance;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] Fireballs;
    private float SwordCooldownTimer = Mathf.Infinity;
    private float FireballCooldownTimer = Mathf.Infinity;
    [SerializeField] private AudioClip SwordSound;
    [SerializeField] private AudioClip FireballSound;
    private void Awake() {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Mana = GetComponent<Mana>();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && SwordCooldownTimer > SwordAttackCooldown && PlayerMovement.Attack()) {
            SwordAttack();
        }
        else if (Input.GetKeyDown(KeyCode.X) && FireballCooldownTimer > FireballAttackCooldown && PlayerMovement.Attack() && Mana.CurrentMana > 0) {
            FireballAttack();
            Mana.UseMana(10f);
        }
        SwordCooldownTimer += Time.deltaTime;
        FireballCooldownTimer += Time.deltaTime;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + (transform.right * Range) * transform.localScale.x * Distance,
                            new Vector3(boxCollider2D.bounds.size.x * Range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
    private void SwordAttack() {
        animator.SetTrigger("SwordAttack");
        SoundManager.instance.PlaySound(SwordSound);
        SwordCooldownTimer = 0;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + (transform.right * Range) * transform.localScale.x * Distance,
                           new Vector3(boxCollider2D.bounds.size.x * Range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
                           0, Vector2.left, 0, EnemyLayer);
        if (hit.collider != null) {
            EnemyHealth = hit.collider.GetComponent<Health>();
            if (EnemyHealth != null) {
                EnemyHealth.TakeDamage(20f);
            }
        }
    }
    private void FireballAttack() {
        animator.SetTrigger("FireballAttack");
        SoundManager.instance.PlaySound(FireballSound);
        FireballCooldownTimer = 0;
        Fireballs[FindFireball()].transform.position = FirePoint.position;
        Fireballs[FindFireball()].GetComponent<Projectile>().ActivateProjectile();
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