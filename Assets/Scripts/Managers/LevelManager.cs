using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    
    public int currentHealth;
    GameObject player;
    PlayerHealth playerHealth;
    public NotificationManager notificationManager;
    public PlayerStats playerStats;
    Slider expSlider;
    Text levelText;
    public Text SliderText;
    AudioSource lvlupAudio;
    public void TakeExp(int count)
    {
        GameManager.Instance.currentExp += count;
        if (GameManager.Instance.currentExp >= GameManager.Instance.level * 100)
        {
            levelup();
        }
    }

    public void levelup()
    {
        // вместо level юзать  GameManager.Instance.level и GameManager.Instance.currentExp
        GameManager.Instance.currentExp = GameManager.Instance.currentExp - GameManager.Instance.level * 100;
        GameManager.Instance.level++;
        playerHealth.currentHealth = playerHealth.maxHealth;
        lvlupAudio.Play();
        notificationManager.Notify("Level "+ GameManager.Instance.level + "!");
        playerStats.levelScore++;
    }

    void Start()
    {
        lvlupAudio = gameObject.GetComponent<AudioSource>();
        expSlider = transform.GetChild(0).GetComponent<Slider>();
        levelText = transform.GetChild(1).GetComponent<Text>();
        expSlider.value = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        expSlider.maxValue = GameManager.Instance.level * 100;
        expSlider.value = GameManager.Instance.currentExp;
        levelText.text = GameManager.Instance.level.ToString();
        SliderText.text = GameManager.Instance.currentExp + "/"+ GameManager.Instance.level * 100;
    }
}
