using UnityEngine;
public class Mana : MonoBehaviour {
    [SerializeField] public float StartingMana;
    public float MaxMana { get; set; }
    public float CurrentMana { get; set; }
    private void Awake() {
        if (PlayerPrefs.HasKey("MaxMana")) {
            float maxMana = PlayerPrefs.GetFloat("MaxMana");
            MaxMana = maxMana;
        }
        if (PlayerPrefs.HasKey("SavedMana")) {
            float savedMana = PlayerPrefs.GetFloat("SavedMana");
            CurrentMana = savedMana;
        }
        else {
            CurrentMana = StartingMana;
        }
    }
    private void Update() {
        if (MaxMana > StartingMana) {
            StartingMana = MaxMana;
        }
    }
    public void UseMana(float mana) {
        CurrentMana = Mathf.Clamp(CurrentMana - mana, 0, StartingMana);
    }
    public void AddMana(float Drop) {
        CurrentMana = Mathf.Clamp(CurrentMana + Drop, 0, StartingMana);
    }
    public void UpdateMana(float Update) {
        MaxMana = StartingMana + Update;
    }
}