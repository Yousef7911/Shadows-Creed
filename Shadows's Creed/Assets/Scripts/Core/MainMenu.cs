using UnityEngine;
public class MainMenu : MonoBehaviour {
    [SerializeField] GameObject MainMenuScreen;
    [SerializeField] GameObject SettingsScreen;
    private Resolution[] resolutions;
    private int change = 0;
    private bool Full;
    private LevelLoader LevelLoader;
    private void Awake() {
        SettingsScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
        LevelLoader = FindObjectOfType<LevelLoader>();
        resolutions = Screen.resolutions;
        SoundManager.instance.audio.volume = 0.75f;
        SoundManager.instance.music.volume = 0.75f;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            if (SettingsScreen.activeInHierarchy) {
                Back();
            }
        }
    }
    public void NewGame() {
        PlayerPrefs.DeleteAll();
        LevelLoader.LoadLevel(1);
    }
    public void LoadGame() {
        if (PlayerPrefs.HasKey("SavedLevel")) {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");
            LevelLoader.LoadLevel(savedLevel);
        }

    }
    public void Settings() {
        MainMenuScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }
    public void FullScreen() {
        Screen.fullScreen = !Screen.fullScreen;
        Full = Screen.fullScreen;
    }
    public void Resolution() {
        change++;
        if (change >= resolutions.Length) {
            change = 0;
        }
        ResolutionManager.instance.ChangeResolution(resolutions[change].width, resolutions[change].height,Full);
    }
    public void SoundVolume() {
        SoundManager.instance.ChangeSoundVolume(0.1f);
    }
    public void MusicVolume() {
        SoundManager.instance.ChangeMusicVolume(0.1f);
    }
    public void Back() {
        SettingsScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
    public void Quit() {
        Application.Quit();
    }
}