using UnityEngine;
public class SoundManager : MonoBehaviour {
    public static SoundManager instance { get; private set; }
    public AudioSource audio;
    public AudioSource music;
    private void Awake() {
        audio = GetComponent<AudioSource>();
        music = transform.GetChild(0).GetComponent<AudioSource>();
        if (instance == null) {
            instance = this;
        }
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }
    public void PlaySound(AudioClip clip) {
        audio.PlayOneShot(clip); 
    }
    private void ChangeSourceVolume(float BaseVolume, string VolumeName, float Change, AudioSource Source) {
        float CurrentVolume = PlayerPrefs.GetFloat(VolumeName, 1);
        CurrentVolume += Change;
        if (CurrentVolume > 1.1) {
            CurrentVolume = 0;
        }
        else if (CurrentVolume < 0) {
            CurrentVolume = 1;
        }
        float FinalVolume = CurrentVolume * BaseVolume;
        Source.volume = FinalVolume;
        PlayerPrefs.SetFloat(VolumeName, CurrentVolume);
    }
    public void ChangeSoundVolume(float change) {
        ChangeSourceVolume(1, "SoundVolume", change, audio);
    }
    public void ChangeMusicVolume(float change) {
        ChangeSourceVolume(0.5f, "MusicVolume", change, music);
    }
}