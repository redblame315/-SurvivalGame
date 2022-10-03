using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public Text damageText;
    public Text speedText;
    public Text maxHealthText;
    public Text fireRateText;
    public Text LvlScoreText;
    public int damage;
    public int speed;
    public int maxHealth;
    public double fireRate;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;
    public int levelScore;
    
    // Use this for initialization
    void Start ()
    {
        
    }

    void OnEnable()
    {
        maxHealthText.text = "Health: " + playerHealth.maxHealth;
        speedText.text = "Speed: " + playerMovement.speed;
        fireRateText.text = "Fire Rate: " + playerShooting.timeBetweenBullets;
        damageText.text = "Damage: " + playerShooting.damagePerShot;
    }

    public void HealthUP()
    {
        if (levelScore > 0)
        {
            levelScore--;
            playerHealth.maxHealth += 25;
            maxHealthText.text = "Health: " + playerHealth.maxHealth;
            playerHealth.healthSlider.maxValue = playerHealth.maxHealth;
        }
    }
    public void SpeedUP()
    {
        if (levelScore > 0)
        {
            levelScore--;
            playerMovement.speed++;
            speedText.text = "Speed: " + playerMovement.speed;
        }
    }
    public void RateUP()
    {
        if (levelScore > 0)
        {
            levelScore--;
            playerShooting.timeBetweenBullets -= 0.015f;
            fireRateText.text = "Fire Rate: " + playerShooting.timeBetweenBullets;
        }
    }
    public void DamageUP()
    {
        if (levelScore > 0)
        {
            levelScore--;
            playerShooting.damagePerShot += 3;
            damageText.text = "Damage: " + playerShooting.damagePerShot;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        LvlScoreText.text = "Level Score:      " + levelScore;
    }
}
