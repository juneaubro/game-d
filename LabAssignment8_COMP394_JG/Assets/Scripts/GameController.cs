using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController CONTROLLER;

    public GameObject basketPrefab;
    public GameObject loseText;
    public int numberOfBaskets = 3; //Lives
    public Text timerText;
    public Text livesText;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        CONTROLLER = this;

        livesText = GameObject.Find("Lives").GetComponent<Text>();

        loseText = GameObject.Find("Lose");

        timerText = GameObject.Find("Timer").GetComponent<Text>();
        timerText.text = "Timer: 00:00:00";
        TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Main Menu");

        TimerStart();

        livesText.text = $"Lives: {numberOfBaskets - Apple.hits}";

        if(numberOfBaskets - Apple.hits <= 0) {
            loseText.GetComponent<Text>().text = "You Lose";
            Time.timeScale = 0.001f;
            livesText.text = $"Lives: 0";
        }
    }
    private void TimerStart()
    {
        timer += Time.deltaTime;
        timerText.text = $"Timer: {timer:00:00.00}";
    }

}
