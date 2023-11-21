using System.Collections;
using UnityEngine;
public class Health : MonoBehaviour {
    private Animator animator;
    [SerializeField] public float StartingHealth;
    private bool Dead;
    [SerializeField] private float IFramesDuration;
    [SerializeField] private int NumOfFlashes;
    private SpriteRenderer SpriteRenderer;
    private GameObject[] Components;
    [SerializeField] private AudioClip HurtSound;
    [SerializeField] private AudioClip DeathSound;
    public float MaxHealth { get; set; }
    public float CurrentHealth { get;  set; }
    private void Awake() {
        if (PlayerPrefs.HasKey("MaxHealth")) {
            float maxHealth = PlayerPrefs.GetFloat("MaxHealth");
            MaxHealth = maxHealth;
        }
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey("SavedHealth")) {
            float savedHealth = PlayerPrefs.GetFloat("SavedHealth");
            CurrentHealth = savedHealth;
        }
        else {
            CurrentHealth = StartingHealth;
        }
    }
    private void Update() {
        if (MaxHealth > StartingHealth) {
            StartingHealth = MaxHealth;
        }
    }
    public void TakeDamage(float Damage) {
        CurrentHealth = Mathf.Clamp(CurrentHealth - Damage, 0, StartingHealth);
        if (CurrentHealth > 0) {
            animator.SetTrigger("Hurt");
            SoundManager.instance.PlaySound(HurtSound);
            StartCoroutine(Invunerability());
            Dead = false;
        }
        else {
            if (!Dead) {
                animator.SetTrigger("Die");
                for (int i = 0; i < Components.Length; i++) {
                    if (Components[i].tag == "Enemy") {
                        Components[i].SetActive(false);
                    }
                    animator.SetBool("Grounded", true);
                    animator.SetTrigger("Die");
                }
                Dead = true;
                SoundManager.instance.PlaySound(DeathSound);
            }
        }
    }
    public void AddHealth(float Heart) {
        CurrentHealth = Mathf.Clamp(CurrentHealth + Heart, 0, StartingHealth);
    }
    public void UpdateHealth(float Update) {
        MaxHealth = StartingHealth + Update;
    }
    private IEnumerator Invunerability() {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < NumOfFlashes; i++) {
            SpriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (NumOfFlashes * 2));
            SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(IFramesDuration / (NumOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    private void Deactivate() {
        gameObject.SetActive(false);
    }
}