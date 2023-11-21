using UnityEngine;
public class Door : MonoBehaviour {
    [SerializeField] private Transform PreviousRoom;
    [SerializeField] private Transform NextRoom;
    [SerializeField] CameraController Camera;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (collision.transform.position.x < transform.position.x) {
                Camera.NewCamera(NextRoom);
                NextRoom.GetComponent<Room>().ActivateRoom(true);
                PreviousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else {
                Camera.NewCamera(PreviousRoom);
                PreviousRoom.GetComponent<Room>().ActivateRoom(true);
                NextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}