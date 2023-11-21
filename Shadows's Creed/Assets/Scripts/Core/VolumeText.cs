using UnityEngine;
using UnityEngine.UI;
public class VolumeText : MonoBehaviour {
    [SerializeField] private string VolumeName;
    [SerializeField] private string VolumeDescription;
    private Text text;
    private void Awake() {
        text = GetComponent<Text>();
    }
    private void Update() {
        UpdateVolume();
    }
    private void UpdateVolume() {
        float volume = PlayerPrefs.GetFloat(VolumeName) * 100;
        int Value = (int)volume;
        text.text = VolumeDescription + Value.ToString();
    }
}