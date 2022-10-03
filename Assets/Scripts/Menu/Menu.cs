using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	void Start() {
		PlayerPrefs.SetString ("MyNickname", "");
		PlayerPrefs.SetInt ("IsHost", 1);
	}

	public void NewGame()
    {
		SceneManager.LoadScene ("MainScene");
    }

	public void NewMultiplayerGame()
	{
		Text field = GameObject.FindGameObjectWithTag ("NicknameField").GetComponent<Text> ();
		if (field != null) {
			PlayerPrefs.SetString ("MyNickname", field.text);
			SceneManager.LoadScene ("Multiplayer");
		}
	}
	public void JoinMultiplayerGame()
	{
		Text field = GameObject.FindGameObjectWithTag ("NicknameField").GetComponent<Text> ();
		if (field != null) {
			PlayerPrefs.SetString ("MyNickname", field.text);
			PlayerPrefs.SetInt ("IsHost", 0);
			SceneManager.LoadScene ("Multiplayer");
		}
	}
    /*public void Button
    {

    }
    public void button1
    {

    }*/
    public void ExitGame()
    {
        Application.Quit();
    }
}
