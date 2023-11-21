using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerRespawn : MonoBehaviour {
    [SerializeField] private AudioClip CheckpointSound;
    public Transform CurrentCheckpoint;
    public Health PlayerHealth;
    public Mana PlayerMana;
    UIManager Manager;
    public int Level;
    public float HealthOnCheckpoint;
    public float ManaOnCheckpoint;
    public float MaxHealth;
    public float MaxMana;
    private void Awake() {
        PlayerHealth = GetComponent<Health>();
        PlayerMana = GetComponent<Mana>();
        Manager = FindObjectOfType<UIManager>();
        LoadGame();
    }
    public void CheckRespawn() {
        if (CurrentCheckpoint == null) {
            Manager.GameOver();
            return;
        }
        transform.position = CurrentCheckpoint.position;
        Camera.main.GetComponent<CameraController>().NewCamera(CurrentCheckpoint.parent);
        PlayerHealth.CurrentHealth = HealthOnCheckpoint;
        PlayerMana.CurrentMana = ManaOnCheckpoint;
        PlayerHealth.MaxHealth = MaxHealth;
        PlayerMana.MaxMana = MaxMana;
        CurrentCheckpoint.parent.GetComponent<Room>().ActivateRoom(true);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Checkpoint") {
            CurrentCheckpoint = collision.transform;
            HealthOnCheckpoint = PlayerHealth.CurrentHealth;
            ManaOnCheckpoint = PlayerMana.CurrentMana;
            MaxHealth = PlayerHealth.MaxHealth;
            MaxMana = PlayerMana.MaxMana;
            Level = SceneManager.GetActiveScene().buildIndex;
            SoundManager.instance.PlaySound(CheckpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
            SaveGame();
        }
    }
    public void SaveGame() {
        PlayerPrefs.SetInt("SavedLevel", Level);
        PlayerPrefs.SetFloat("SavedHealth", HealthOnCheckpoint);
        PlayerPrefs.SetFloat("SavedMana", ManaOnCheckpoint);
        PlayerPrefs.SetFloat("MaxHealth", MaxHealth);
        PlayerPrefs.SetFloat("MaxMana", MaxMana);
        PlayerPrefs.SetFloat("SavedPositionX", CurrentCheckpoint.position.x);
        PlayerPrefs.SetFloat("SavedPositionY", CurrentCheckpoint.position.y);
        PlayerPrefs.SetFloat("CameraX", CurrentCheckpoint.parent.position.x);
        PlayerPrefs.Save();
    }
    public void LoadGame() {
        float savedPositionX = PlayerPrefs.GetFloat("SavedPositionX");
        float savedPositionY = PlayerPrefs.GetFloat("SavedPositionY");
        transform.position = new Vector3(savedPositionX, savedPositionY, transform.position.z);
        CurrentCheckpoint.parent.GetComponent<Room>().ActivateRoom(true);
    }
}