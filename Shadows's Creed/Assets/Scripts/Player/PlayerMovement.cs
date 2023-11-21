using UnityEngine;
public class PlayerMovement : MonoBehaviour {
    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private new BoxCollider2D collider;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private float Speed;
    [SerializeField] private float JumpPower;
    [SerializeField] private float CoyoteTime;
    [SerializeField] private int ExtraJumps;
    [SerializeField] private float WallJumpX;
    [SerializeField] private float WallJumpY;
    private int JumpCounter;
    private float CoyoteCounter;
    private float Horizontal;
    public bool FacingRight = true;
    [SerializeField] private AudioClip JumpingSound;
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void Update() {
        Horizontal = Input.GetAxis("Horizontal");
        if (Horizontal > 0.01f) {
            transform.localScale = new Vector2(0.85f, 0.7f);
            FacingRight = true;
        }
        else if (Horizontal < - 0.01f) { 
            transform.localScale = new Vector2(-0.85f, 0.7f);
            FacingRight = false;
        }
        animator.SetBool("Run", Horizontal != 0);
        animator.SetBool("Grounded", Grounded());
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space) && rigidbody2D.velocity.y > 0) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y / 2);
        }
        if (onWall()) {
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        else {
            rigidbody2D.gravityScale = 3;
            rigidbody2D.velocity = new Vector2(Horizontal * Speed, rigidbody2D.velocity.y);
            if (Grounded()) {
                CoyoteCounter = CoyoteTime;
                JumpCounter = ExtraJumps;
            }
            else {
                CoyoteCounter -= Time.deltaTime;
            }
        }
    }
    private void Jump() {
        if (CoyoteCounter <= 0 && !onWall() && JumpCounter <= 0) return;
        SoundManager.instance.PlaySound(JumpingSound);
        if (onWall()) {
            WallJump();
        }
        else {
            if (Grounded()) {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpPower);
            }
            else {
                if (CoyoteCounter > 0) {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpPower);
                }
                else {
                    if (JumpCounter > 0) {
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpPower);
                        JumpCounter--;
                    }
                }
            }
            CoyoteCounter = 0;
        }
    }
    private bool Grounded() {
        RaycastHit2D hit2D = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
        return hit2D.collider != null;
    }
    private bool onWall() {
        RaycastHit2D hit2D = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0,
                             new Vector2(transform.localScale.x, 0), 0.1f, WallLayer);
        return hit2D.collider != null;
    }
    private void WallJump() {
        rigidbody2D.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * WallJumpX, WallJumpY));
    }
    public bool Attack() {
        return Horizontal == 0 && Grounded() && !onWall();
    }
}