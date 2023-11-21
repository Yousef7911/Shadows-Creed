using UnityEngine;
public class EnemyPatrol : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Transform LeftEdge;
    [SerializeField] private Transform RightEdge;
    [SerializeField] private Transform Enemy;
    [SerializeField] private float Speed;
    [SerializeField] private float IdleDuration;
    private float IdleTimer;
    private Vector3 initScale;
    private bool MovingRight;
    private void Awake() {
        initScale = Enemy.localScale;
    }
    private void Update() {
        if (MovingRight) {
            if (Enemy.position.x <= RightEdge.position.x) {
                MovingDirection(1);
            }
            else {
                DirectionChange();
            }
        }
        else {
            if (Enemy.position.x >= LeftEdge.position.x) {
                MovingDirection(-1);
            }
            else {
                DirectionChange();
            }
        }
    }
    private void OnDisable() {
        animator.SetBool("Move", false);
    }
    private void MovingDirection(int direction) {
        animator.SetBool("Move", true);
        IdleTimer = 0;
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * direction * Speed, 
                                     Enemy.position.y, Enemy.position.z);
    }
    private void DirectionChange() {
        animator.SetBool("Move", false);
        IdleTimer += Time.deltaTime;
        if (IdleTimer > IdleDuration) {
            MovingRight = !MovingRight;
        }
    }
}