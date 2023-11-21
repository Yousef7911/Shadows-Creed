using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour {
    [SerializeField] private Health PlayerHealth;
    [SerializeField] public Slider CurrentHealthbar;
    [SerializeField] private Text text;
    public void Start() {
        CurrentHealthbar.value = CurrentHealthbar.maxValue;
        text.text = CurrentHealthbar.value + "/" + CurrentHealthbar.maxValue;
    }
    public void Update() {
        if (PlayerHealth.MaxHealth > CurrentHealthbar.maxValue) {
            CurrentHealthbar.maxValue = PlayerHealth.MaxHealth;
        }
        CurrentHealthbar.value = PlayerHealth.CurrentHealth;
        text.text = CurrentHealthbar.value + "/" + CurrentHealthbar.maxValue;
    }
}