using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public LevelManager levelManager;
    // public GameManager GameManager;
    public NotificationManager notificationManager;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        int gold = GameManager.Instance.gold;
        int level = GameManager.Instance.level;
        int currentexp = GameManager.Instance.currentExp;

        int questId = GameManager.Instance.currentQuest.id;

        int slonDifficulty = GameManager.Instance.elephants.difficulty;
        int pinkDifficulty = GameManager.Instance.pink.difficulty;
        int greenDifficulty = GameManager.Instance.green.difficulty;


        int currentCounter = GameManager.Instance.currentCounter;
        int slonCounter = GameManager.Instance.SlonCounter;
        int pinkCounter = GameManager.Instance.PinkCounter;
        int greenCounter = GameManager.Instance.GreenCounter;



        File.WriteAllText("Save.txt", gold + Environment.NewLine + level + Environment.NewLine + currentexp + Environment.NewLine 
            + slonDifficulty + Environment.NewLine + pinkDifficulty + Environment.NewLine + greenDifficulty + Environment.NewLine 
            + questId + Environment.NewLine + currentCounter + Environment.NewLine + slonCounter + Environment.NewLine + pinkCounter + Environment.NewLine + greenCounter);
        notificationManager.Notify("Game Saved!");
    }


    public void LoadQuest(int questId) {
        if (questId == 0)
            GameManager.Instance.currentQuest = GameManager.Instance.elephants;
        else if (questId == 1)
            GameManager.Instance.currentQuest = GameManager.Instance.pink;
        else if (questId == 2)
            GameManager.Instance.currentQuest = GameManager.Instance.green;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FileStream file1 = new FileStream("Save.txt", FileMode.Open); //создаем файловый поток
        StreamReader reader = new StreamReader(file1); // создаем «потоковый читатель» и связываем его с файловым потоком 
        GameManager.Instance.gold = int.Parse(reader.ReadLine());
        GameManager.Instance.level = int.Parse(reader.ReadLine());
        GameManager.Instance.currentExp = int.Parse(reader.ReadLine());

        

        GameManager.Instance.elephants.difficulty = int.Parse(reader.ReadLine());
        GameManager.Instance.pink.difficulty = int.Parse(reader.ReadLine());
        GameManager.Instance.green.difficulty = int.Parse(reader.ReadLine());

        int questId = int.Parse(reader.ReadLine());
        LoadQuest(questId);

        GameManager.Instance.currentCounter = int.Parse(reader.ReadLine());
        GameManager.Instance.SlonCounter = int.Parse(reader.ReadLine()); ;
        GameManager.Instance.PinkCounter = int.Parse(reader.ReadLine()); ;
        GameManager.Instance.GreenCounter = int.Parse(reader.ReadLine()); ;

    }
}