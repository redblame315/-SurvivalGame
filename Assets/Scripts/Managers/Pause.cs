using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public GameObject menu;

    void Start()
    {
        
    }


    void Update()
    {
        //Time.timeScale = timing;
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {

            isPaused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1;
        }
        menu.SetActive(isPaused);

    }

    public void ResumeButton(bool state)
    {
        isPaused = state;
    }

    public void MenuButton()
    {
        Application.LoadLevel(0);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}