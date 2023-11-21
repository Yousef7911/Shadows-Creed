using UnityEngine;
using UnityEngine.UI;
public class Manabar : MonoBehaviour {
    [SerializeField] private Mana PlayerMana;
    [SerializeField] public Slider CurrentManabar;
    [SerializeField] private Text text;
    public void Start() {
        CurrentManabar.value = CurrentManabar.maxValue;
        text.text = CurrentManabar.value + "/" + CurrentManabar.maxValue;
    }
    public void Update() {
        if (PlayerMana.MaxMana> CurrentManabar.maxValue) {
            CurrentManabar.maxValue = PlayerMana.MaxMana;
        }
        CurrentManabar.value = PlayerMana.CurrentMana;
        text.text = CurrentManabar.value + "/" + CurrentManabar.maxValue;
    }
}