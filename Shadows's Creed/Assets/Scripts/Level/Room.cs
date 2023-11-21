using UnityEngine;
public class Room : MonoBehaviour {
    [SerializeField] GameObject[] Enemies;
    [SerializeField] GameObject[] Traps;
    private Vector3[] EnemiesPositions;
    private Vector3[] TrapsPositions;
    private float[] EnemiesStartingHealth;
    private void Awake() {
        EnemiesPositions = new Vector3[Enemies.Length];
        EnemiesStartingHealth = new float[Enemies.Length];
        for (int i = 0; i < Enemies.Length; i++) {
            if (Enemies[i] != null) {
                EnemiesPositions[i] = Enemies[i].transform.position;
                Health EnemyHealth = Enemies[i].GetComponent<Health>();
                if (EnemyHealth != null) {
                    EnemiesStartingHealth[i] = EnemyHealth.StartingHealth;
                }
            }
        }
        TrapsPositions = new Vector3[Traps.Length];
        for (int i = 0; i < Traps.Length; i++) {
            if (Traps[i] != null) {
                TrapsPositions[i] = Traps[i].transform.position;
            }
        }
    }
    public void ActivateRoom(bool Status) {
        for (int i = 0; i < Enemies.Length; i++) {
            if (Enemies[i] != null) {
                Health enemyHealth = Enemies[i].GetComponent<Health>();
                if (enemyHealth != null) {
                    enemyHealth.CurrentHealth = EnemiesStartingHealth[i];
                }
                Enemies[i].SetActive(Status);
                Enemies[i].transform.position = EnemiesPositions[i];
            }
        }
        for (int i = 0; i < Traps.Length; i++) {
            if (Traps[i] != null) {
                Traps[i].SetActive(Status);
                Traps[i].transform.position = TrapsPositions[i];
            }
        }
    }
}