using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {
    [SerializeField] GameObject HealthBar; 
    [SerializeField] GameObject ManaBar; 
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] private AudioClip GameOverSound;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject SettingsScreen;
    private Resolution[] resolutions;
    private int change = 0;
    private bool Full;
    private LevelLoader LevelLoader;
    private void Awake() {
        resolutions = Screen.resolutions;
        LevelLoader = FindObjectOfType<LevelLoader>();
        HealthBar.SetActive(false);
        ManaBar.SetActive(false);
        GameOverScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SettingsScreen.SetActive(false);
        StartCoroutine(HealthMana());
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PauseScreen.activeInHierarchy) {
                PauseGame(false);
            }
            else if (SettingsScreen.activeInHierarchy) {
                SettingsScreen.SetActive(false);
                PauseGame(false);
            }
            else {
                PauseGame(true);
            }
        }
        if (Input.GetKeyDown (KeyCode.Backspace)) {
            if (SettingsScreen.activeInHierarchy) {
                Back();
            }
        }
    }
    public IEnumerator HealthMana() {
        yield return new WaitForSeconds(1);
        HealthBar.SetActive(true);
        ManaBar.SetActive(true);
    }
    public void GameOver() {
        HealthBar.SetActive(false);
        ManaBar.SetActive(false);
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
        SoundManager.instance.PlaySound(GameOverSound);
    }
    public void Restart() {
        LevelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void MainMenu() {
        LevelLoader.LoadLevel(0);
        Time.timeScale = 1;
    }
    public void Quit() {
        Application.Quit();
    }
    public void PauseGame(bool status) {
        if (GameOverScreen.activeInHierarchy == false && SettingsScreen.activeInHierarchy == false) {
            PauseScreen.SetActive(status);
            HealthBar.SetActive(!status);
            ManaBar.SetActive(!status);
        }
        if (status) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }
    public void Settings() {
        PauseScreen.SetActive(false);
        HealthBar.SetActive(false);
        ManaBar.SetActive (false);
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
        ResolutionManager.instance.ChangeResolution(resolutions[change].width, resolutions[change].height, Full);
    }
    public void SoundVolume() {
        SoundManager.instance.ChangeSoundVolume(0.1f);
    }
    public void MusicVolume() {
        SoundManager.instance.ChangeMusicVolume(0.1f);
    }
    public void Back() {
        SettingsScreen.SetActive(false);
        PauseScreen.SetActive(true);
    }
}