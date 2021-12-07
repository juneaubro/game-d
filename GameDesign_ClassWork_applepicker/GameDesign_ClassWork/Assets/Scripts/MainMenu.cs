using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject MainMenuCanvas;
    private GameObject OptionsMenuCanvas;
    public static bool mute = false;
    private Slider volumeSlider;
    private Toggle MusicToggle;
    private void Start() {
        MainMenuCanvas = GameObject.Find("Main Menu Canvas");
        OptionsMenuCanvas = GameObject.Find("Options Canvas");
        volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();
        MusicToggle = GameObject.Find("Music Toggle").GetComponent<Toggle>();
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        PlayerPrefs.GetFloat("Volume", 1);
        PlayerPrefs.GetInt("MusicToggle", 0);
        if (PlayerPrefs.GetInt("MusicToggle") == 1)
            MusicToggle.isOn = true;
        else
            MusicToggle.isOn = false;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            MainMenuCanvas.SetActive(true);
            OptionsMenuCanvas.SetActive(false);
        }
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        if (MusicToggle.isOn)
            PlayerPrefs.SetInt("MusicToggle", 1);
        else
            PlayerPrefs.SetInt("MusicToggle", 0);
        if (PlayerPrefs.GetInt("MusicToggle") == 1)
            mute = true;
        else
            mute = false;
    }
    public void LoadEasy() {
        SceneManager.LoadScene("Ez");
        Time.timeScale = 1f;
        Apple.hits = 0f;
    }
    public void LoadMed() {
        SceneManager.LoadScene("Intermediate");
        Time.timeScale = 1f;
        Apple.hits = 0f;
    }
    public void LoadHard() {
        SceneManager.LoadScene("Hard");
        Time.timeScale = 1f;
        Apple.hits = 0f;
    }
    public void LoadMainMenu() {
        MainMenuCanvas.SetActive(true);
        OptionsMenuCanvas.SetActive(false);
    }
    public void LoadOptionsMenu() {
        MainMenuCanvas.SetActive(false);
        OptionsMenuCanvas.GetComponent<Canvas>().enabled = true;
        OptionsMenuCanvas.SetActive(true);
    }
}
