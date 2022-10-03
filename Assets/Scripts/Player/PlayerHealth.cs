using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
	public int defense;
    public Slider healthSlider;
    public Text SliderText;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Color sliderColor;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    public LevelManager levelManager;
    // public GameManager. GameManager.;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = maxHealth;
        
        defense = 0;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        SliderText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = currentHealth;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

		currentHealth -= (int)((1 - 0.01*defense)*amount);
        SliderText.text = currentHealth + "/" + maxHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

	public void RestoreHP (int amount) {
		if (currentHealth + amount <= 100) {
			currentHealth += amount;
		} 
		else {
			currentHealth = 100;
		}
		healthSlider.value = currentHealth;
		playerAudio.Play ();
	}


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
        GameManager.Instance.gold = 0;
        GameManager.Instance.level = 1;
        GameManager.Instance.currentExp = 0;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (1);
    }
}
