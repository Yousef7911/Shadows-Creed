using UnityEngine;
public class CameraController : MonoBehaviour {
    private float CurrentPositionX;
    private Vector3 Velocity = Vector3.zero;
    /*Room Camera*/
    [SerializeField] private float Speed;
    /*Folow Camera*/
    /*[SerializeField] private Transform Player;
    [SerializeField] private float AheadDistance;
    [SerializeField] private float CameraSpeed;
    private float LookAhead;*/
    private void Awake() {
        if (PlayerPrefs.HasKey("CameraX")) {
            float savedCameraX = PlayerPrefs.GetFloat("CameraX");
            CurrentPositionX = savedCameraX;
        }
        else {
            CurrentPositionX = transform.position.x;
        }
    }
    private void Update() {
        /*Room Camera*/
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(CurrentPositionX, transform.position.y, transform.position.z),
                             ref Velocity, Speed);
        /*Folow Camera*/
        /* transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        LookAhead = Mathf.Lerp(LookAhead, AheadDistance * Player.localScale.x, Time.deltaTime * CameraSpeed);*/
    }
    public void NewCamera(Transform transform) {
        CurrentPositionX = transform.position.x;
    }
}