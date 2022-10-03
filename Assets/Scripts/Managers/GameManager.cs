using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int gold;

    public int currentExp = 0;
    public int level = 1;

    public int currentCounter = 0;

    public struct quest
    {
        public int id;
        public int multiplier;
        public int difficulty;
        public string text;

        public quest(int id, int multiplier, int difficulty, string text)
        {
            this.id = id;

            
            this.multiplier = multiplier;
            this.difficulty = difficulty;
            this.text = text;
        }
    }

    
    public quest elephants = new quest(0, 1, 1, "Elephants killer");
    public quest pink = new quest(1, 3, 1, "Pinks killer");                //вторая цифра - множитель
    public quest green = new quest(2, 3, 1, "Greens killer");

    public quest currentQuest;

    public int SlonCounter = 0;
    public int PinkCounter = 0;
    public int GreenCounter = 0;



    void Awake()
    {
        if (null == Instance)
        {
            GameObject gam = gameObject;
            GameObject trams = transform.gameObject;
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        currentQuest = elephants;


    }

    void Update()
    {
    
    }
}