using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelUp : MonoBehaviour {
    [SerializeField] private AudioClip FinishPointSound;
    LevelLoader LevelLoader;
    PlayerRespawn respawn;
    private void Awake() {
        LevelLoader = FindObjectOfType<LevelLoader>();
        respawn = FindObjectOfType<PlayerRespawn>();
    }
    private IEnumerator NextLevel() {
        yield return new WaitForSeconds(0); // Win Animation 
        LevelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        respawn.SaveGame();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FinishPoint") {
            SoundManager.instance.PlaySound(FinishPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            //collision.GetComponent<Animator>().SetTrigger(""); // FinishPoint Animation
            StartCoroutine(NextLevel());
        }
    }
}