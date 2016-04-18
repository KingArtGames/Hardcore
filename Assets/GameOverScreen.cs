using UnityEngine;
using System.Collections;
using TinyMessenger;
using Assets.Scripts.message.custom;
using Assets.Scripts.manager;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    public GameObject GameOverPanel;
    private IMessageBus _bus;

	void Start () {
        _bus = Initialiser.Instance.GetService<IMessageBus>();
        _bus.Subscribe<GameOverMessage>(OnGameOver);
	}

    public void OnGameOver(GameOverMessage msg)
    {
        GameOverPanel.SetActive(true);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
