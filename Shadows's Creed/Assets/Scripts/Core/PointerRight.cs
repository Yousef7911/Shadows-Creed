using UnityEngine;
public class PointerRight : MonoBehaviour {
    [SerializeField] private RectTransform[] Options;
    private RectTransform rect;
    private int CurrentPosition;
    private void Awake() {
        rect = GetComponent<RectTransform>();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            ChangePosition(1);
        }
    }
    private void ChangePosition(int change) {
        CurrentPosition += change;
        if (CurrentPosition < 0) {
            CurrentPosition = Options.Length - 1;
        }
        else if (CurrentPosition > Options.Length - 1) {
            CurrentPosition = 0;
        }
        rect.position = new Vector3(rect.position.x, Options[CurrentPosition].position.y, 0);
    }
}