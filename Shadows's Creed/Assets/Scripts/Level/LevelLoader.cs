using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Slider Slider;
    [SerializeField] private Text Text;
    [SerializeField] private Text Loading;
    [SerializeField] private Animator animator;
    private void Awake() {
        LoadingScreen.SetActive(true);
        Slider.value = 1;
        Text.text = "100%";
        Loading.text = "Loading . . .";
        animator.SetTrigger("End");
    }
    public void LoadLevel(int levelIndex) {
        StartCoroutine(LoadingLevel(levelIndex));
    }
    private IEnumerator LoadAsync(int levelIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Slider.value = progress;
            Text.text = (int) (progress * 100f) + "%";
            if (Loading.text == "Loading") {
                Loading.text = "Loading .";
            }
            else if (Loading.text == "Loading .") {
                Loading.text = "Loading . .";
            }
            else if (Loading.text == "Loading . .") {
                Loading.text = "Loading . . .";
            }
            else if (Loading.text == "Loading . . .") {
                Loading.text = "Loading";
            }
            yield return null;
        }
    }
    private IEnumerator LoadingLevel(int levelIndex) {
        LoadingScreen.SetActive(true);
        Slider.value = 0;
        Text.text = "0%";
        Loading.text = "Loading";
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadAsync(levelIndex));
    }
}