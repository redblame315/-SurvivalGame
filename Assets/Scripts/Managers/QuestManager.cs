using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    
    public Text outText;
    
    AudioSource questCompleteAudio;
    
    public NotificationManager notificationManager;
    public LevelManager levelManager;

    public void EnemyKilled(int type) // 0-slon, 1-pink, 2-green
    {
        if (type == 0)
        {
            GameManager.Instance.SlonCounter++;
            if (GameManager.Instance.currentQuest.id == 0) GameManager.Instance.currentCounter++;
        }
        else if (type == 1)
        {
            GameManager.Instance.PinkCounter++;
            if (GameManager.Instance.currentQuest.id == 1) GameManager.Instance.currentCounter++;
        }
        else if (type == 2)
        {
            GameManager.Instance.GreenCounter++;
            if (GameManager.Instance.currentQuest.id == 2) GameManager.Instance.currentCounter++;
        }
        if (GameManager.Instance.currentCounter >= GameManager.Instance.currentQuest.multiplier * GameManager.Instance.currentQuest.difficulty)
        {
            QuestComplete();
        }
        
    }

    public void QuestComplete()
    {
        GameManager.Instance.currentCounter = 0;
        if (GameManager.Instance.currentQuest.id == 0) GameManager.Instance.elephants.difficulty++;
        else
        if (GameManager.Instance.currentQuest.id == 1) GameManager.Instance.pink.difficulty++;
        else
        if (GameManager.Instance.currentQuest.id == 2) GameManager.Instance.green.difficulty++;
        questCompleteAudio.Play();
        levelManager.TakeExp(GameManager.Instance.currentQuest.difficulty * 50);
        nextQuest();
        notificationManager.Notify("Quest complete +" + GameManager.Instance.currentQuest.difficulty * 50 + "xp");
    }

    public void Zeroing()
    {
        GameManager.Instance.SlonCounter = 0;
        GameManager.Instance.PinkCounter = 0;
        GameManager.Instance.GreenCounter = 0;
    }

    public void nextQuest()
    {
        if (GameManager.Instance.currentQuest.id == 0) GameManager.Instance.currentQuest = GameManager.Instance.pink;
        else
        if (GameManager.Instance.currentQuest.id == 1) GameManager.Instance.currentQuest = GameManager.Instance.green;
        else
        if (GameManager.Instance.currentQuest.id == 2) GameManager.Instance.currentQuest = GameManager.Instance.elephants;
            }

    void Start()
    {
        outText = GameObject.FindGameObjectWithTag("QuestText").GetComponent<Text>();
        questCompleteAudio = GameObject.FindGameObjectWithTag("QuestText").GetComponent<AudioSource>();
        //GameManager.Instance.currentQuest = GameManager.Instance.elephants;
        
    }

    void Update()
    {
        outText.text = GameManager.Instance.currentQuest.text + "(" + GameManager.Instance.currentCounter + " / " + GameManager.Instance.currentQuest.multiplier * GameManager.Instance.currentQuest.difficulty + ")";
    }
}