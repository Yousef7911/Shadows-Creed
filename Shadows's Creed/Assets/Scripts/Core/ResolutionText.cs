using UnityEngine;
using UnityEngine.UI;

public class ResolutionText : MonoBehaviour {
    private Text Text;
    private void Awake() {
        Text = GetComponent<Text>();
    }
    private void Update() {
        UpdateResolutionText();
    }
    public void UpdateResolutionText() {
        int width = PlayerPrefs.GetInt("ScreenWidth", Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt("ScreenHeight", Screen.currentResolution.height);
        Text.text = "Resolution:\n" + width + "x" + height;
    }
}