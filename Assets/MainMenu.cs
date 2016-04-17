using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    int idiotCnt;
    Text idiotTxt;
	// Use this for initialization
	void Start () {
        idiotCnt = 0;
        idiotTxt = GameObject.Find("idiotTxt").GetComponent<Text>();
	}

    public void exitGameButton()
    {
        Application.Quit();
    }


    public void startNewGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void selectLevel()
    {
        if (!idiotTxt.IsActive())
        {
            idiotTxt.gameObject.SetActive(true);
        }
        idiotTxt.text = "Idiot Counter: "+ ++idiotCnt;
    }
}
