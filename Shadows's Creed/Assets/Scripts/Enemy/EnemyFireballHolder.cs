using UnityEngine;
public class EnemyFireballHolder : MonoBehaviour {
    [SerializeField] Transform Enemy;
    private void Update() {
        transform.localScale = Enemy.localScale;
    }
}