using System;
using UnityEngine;
using UnityEngine.UI;
public class ResolutionManager : MonoBehaviour {
    public static ResolutionManager instance { get; private set; }
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }
    public void ChangeResolution(int width, int height, bool fullscreen) {
        Screen.SetResolution(width, height, fullscreen);
        PlayerPrefs.SetInt("ScreenWidth", width);
        PlayerPrefs.SetInt("ScreenHeight", height);
        PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
    }

    public static implicit operator ResolutionManager(LevelLoader v) {
        throw new NotImplementedException();
    }
}